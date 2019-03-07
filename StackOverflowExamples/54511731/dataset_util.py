import gzip, os
import tensorflow as tf
import tensorflow_hub as hub
import numpy as np
from tfrecord_util import write_record, read_example
from random import shuffle, choices, random
from copy import copy
from display_util import printProgressBar
from multiprocessing import Pool, Process, Queue
from collections import deque

data_segment_size = 1024

module_url = "https://tfhub.dev/google/universal-sentence-encoder-large/3"

# Reduce logging output.
tf.logging.set_verbosity(tf.logging.ERROR)

# Dataset utils
g = tf.Graph()
with g.as_default():
	# Import the Universal Sentence Encoder's TF Hub module
	embed = hub.Module(module_url)
	
def embed_answer(m):
	with g.as_default():
		return embed(tuple(m))
		
# Data creation utils
def parse(path): 
    with gzip.open(path, 'r') as g:
        for l in g: 
            yield eval(l)

def create_example(k, msgs, correct_answer_prob=0.25, answer_pool_size=10):
	#k, msgs = m_cluster
	#print(len(msgs))

	k_list = list(msgs.keys())
	shuffle(k_list)
	
	# Creates a population that does not include k
	pop_list = copy(k_list)
	pop_list = pop_list[1:]
	
	# Inputs to be used for this batch
	inputs = []
	eval = False
	
	if random() <= correct_answer_prob:
		inputs.append(msgs[k]["question"])
		eval = True
		
	for c in choices(pop_list, k=((answer_pool_size - 1) if eval else answer_pool_size)):
		inputs.append(msgs[c]["answer"])
			
	shuffle(inputs)
	
	return ((";;".join(inputs)), (1 if eval else 0))
	
def gen_messages(src):
	
	print("Reading database for splitting")
	
	messages = {}
	
	for m in parse(src):
		messages[m["asin"]] = {"question":m["question"],  "answer":m["answer"]}
		
	# keys = list(messages.keys())
	# l = len(keys)
	# for i in range(0, l, data_segment_size):
		# splice = keys[i:i+data_segment_size]
		# results = embed_answer(tuple(messages[m]["answer"] for m in splice))
		# for j, m in enumerate(splice):
			# messages[m]["embedded"] = list(np.array(results[j]).tolist())
		# printProgressBar(i + 1, l, "Embedding answers: ", " {}/{} Complete".format(i + 1, l))
		
	print("Finished generating {} records".format(len(messages)))
	return messages
	
def map_example(q, start, stop, msgs):
	for m in list(msgs.keys())[start:stop]:
		(input, output) = create_example(m, msgs)
		q.put(tuple([input, output]))
		
def q_monitor(q, examps, msgs):
	l = len(msgs)
	for i in range(l):
		examps.append(q.get())
		printProgressBar(i + 1, l, "Awaiting Threads Progress:", "Completed")
	
def create_training_data(src, correct_answer_prob=0.25, answer_pool_size=10):
	global examples

	map_examples = Queue()
	
	messages = gen_messages(src)
	
	#print('Now there are {} examples'.format(len(messages)))
		
	examples = []
	
	l = len(messages)
	splice = l // os.cpu_count()
	
	processes = [Process(target=map_example, args=(map_examples, 
		None if core is 0 else core * splice, 
		None if core is os.cpu_count() - 1 else (core + 1) * splice, 
		messages)) for core in range(os.cpu_count())]
		
	processes.append(Process(target=q_monitor, args=(map_examples, examples, messages)))
			
	for i, p in enumerate(processes):
		p.start()
		printProgressBar(i + 1, len(processes), "Launching Threads Progress:", "Completed")
		
	for p in processes:
		p.join()
		
	#with Pool(None, map_init, (examples, map_examples,)) as pool:
	#	pool.map(map_example, map(lambda m: (m, messages), messages))
		
	print('Now there are {} examples'.format(len(examples)))
		
	while not map_examples.empty():
		examples.append(map_examples.get_nowait())
		printProgressBar(i + 1, l, "Progress:", "Completed")
	
	return examples
	
def create_records(src, split_ratio=0.9, correct_answer_prob=0.25, answer_pool_size=10):
	examples = create_training_data(src, correct_answer_prob, answer_pool_size)
	
	# Split for the training and testing data
	splice = int(len(examples) * split_ratio)
	print("Splicing the {} examples at index {}".format(len(examples), splice))
	train = examples[:splice]
	test = examples[splice:]
	
	print("Creating tfrecords")
	return (train, test)
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	