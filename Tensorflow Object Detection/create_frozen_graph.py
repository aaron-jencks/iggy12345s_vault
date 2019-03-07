import os
import tensorflow as tf

from tensorflow.python.tools.freeze_graph import freeze_graph
from tensorflow import keras

if __name__ == '__main__':
	parser = argparse.ArgumentParser()
	parser.add_argument("--model_dir", type=str, default="", help="Model folder to export")
	#parser.add_argument("--output_node_names", type=str, default="", help="The name of the output nodes, comma separated.")
	args = parser.parse_args()
	
	# The reading in of the original model to find the output node name
	with keras.backend.get_session() as sess:
		# Imports the saved model
		saver = tf.train.import_meta_graph(args.model_dir + "/model.ckpt.meta")
		saver.restore(sess, args.model_dir + "/model.ckpt")
		
		# Finds the output node names and saves them as a string
		output_nodes = ""
		for i in range(len(model.outputs)):
			output_nodes += model.outputs[i].op.name
			if i < len(model.outputs) - 1:
				output_nodes += ", "
		
		# Saves the frozen graph to file
		freeze_graph(args.model_dir, output_nodes)