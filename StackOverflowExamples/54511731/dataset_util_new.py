import gzip, os
import tensorflow as tf
import tensorflow_hub as hub
from tfrecord_util import write_record, read_example
from random import shuffle, choices, random, randint
from copy import copy
from display_util import printProgressBar
from multiprocessing import Pool, Process, Queue
from collections import deque
from mp_util import DequeManager, DictManager

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
			
messages = {}
map_examples = []
examples = []

def create_example(k, msgs, correct_answer_prob=0.25, answer_pool_size=10):

	k_list = list(msgs.keys())
	shuffle(k_list)
	
	# Creates a population that does not include k
	pop_list = copy(k_list)
	pop_list = pop_list[1:]
	
	# Inputs to be used for this batch
	inputs = []
	eval = False
	
	if random() <= correct_answer_prob:
		inputs.append(msgs.get(k)["question"])
		eval = True
		
	answer_pool_size = randint((answer_pool_size // 2), answer_pool_size)
	for c in choices(pop_list, k=((answer_pool_size - 1) if eval else answer_pool_size)):
		inputs.append(msgs.get(c)["answer"])
			
	shuffle(inputs)
	
	return ((";;".join(inputs)), (1 if eval else 0))
	
def gen_messages(m):
	global messages
	
	messages.set(m["asin"], {"question":m["question"],  "answer":m["answer"]})
	
def map_example(m):
	global map_examples
	global messages
	
	(input, output) = create_example(m, messages)
	map_examples.append(tuple([input, output]))
		
def map_init(q, msgs, examps):
	global map_examples
	global messages
	global examples
	
	map_examples = q
	messages = msgs
	examples = examps
		
def q_monitor(q, msgs, examps):
	l = len(msgs)
	for i in range(l):
		printProgressBar(i + 1, l, "Awaiting Threads Progress:", "Completed")
		while len(q) is 0:
			pass
		examps.append(q.pop())
	
def create_training_data(src, correct_answer_prob=0.25, answer_pool_size=10):
	global messages
	global map_examples
	global examples

	manager = DequeManager()
	manager.start()
	
	dmanager = DictManager()
	dmanager.start()
	
	map_examples = manager.DequeProxy()
	examples = manager.DequeProxy()
	messages = dmanager.DictProxy()
	
	print("Reading database for splitting")
	
	messages.clear()
	
	with Pool(None, map_init, (map_examples, messages, examples,)) as pool:
	
		pool.map(gen_messages, parse(src))
		
		# keys = list(messages.keys())
		# l = len(keys)
		# for i in range(0, l, data_segment_size):
			# splice = keys[i:i+data_segment_size]
			# results = embed_answer(tuple(messages.get(m)["answer"] for m in splice))
			# for j, m in enumerate(splice):
				# l = messages.get(m)
				# l["embedded"] = list(np.array(results[j]).tolist())
				# message.set(m, l)
			# printProgressBar(i + 1, l, "Embedding answers: ", " {}/{} Complete".format(i + 1, l))
		
		print("Finished generating {} records".format(len(messages)))
	
		mon = Process(target=q_monitor, args=(map_examples, messages, examples,))
		mon.start()

		print("Starting example creation")
		pool.map(map_example, messages.keys())
		
		mon.join()
		
	print('Now there are {} examples'.format(len(examples)))
	
	return examples
	
def create_records(src, split_ratio=0.9, correct_answer_prob=0.25, answer_pool_size=10):
	# all method converts it to a list
	examples = create_training_data(src, correct_answer_prob, answer_pool_size).all()
	
	# Split for the training and testing data
	splice = int(len(examples) * split_ratio)
	print("Splicing the {} examples at index {}".format(len(examples), splice))
	train = examples[:splice]
	test = examples[splice:]
	
	print("Creating tfrecords")
	return (train, test)
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	