import cv2
import threading
import label_image_mod as model
from collections import deque

class Processor(threading.Thread):
	"""A simple class used to process image classification in real time"""
	
	def __init(self, given_queue, frozen_graph_filename):
		threading.Thread.__init__(self)
		self.imgQueue = given_queue
		self.isStarted = False
		self.output_queue = deque()
		self.graph_path = frozen_graph_filename
		self.__graph = None
		load_graph(self.graph_path)
		
	def run(self):
		self.isStarted = True
		while self.isStarted:
			while self.imgQueue and len(self.imgQueue) > 0:
				# Pulls the next frame out of the queue
				frame = self.imgQueue.popleft()
				# Processes the image
				
				
	def load_graph(self, graph_path):
		"""Loads a new graph into the current tf session"""
		self.__graph =  tf.Graph()
		graph_def = tf.GraphDef()
		
		with open(graph_path, 'rb') as f:
			graph_def.ParseFromString(f.read())
		with graph.as_default():
			tf.import_graph_def(graph_def)