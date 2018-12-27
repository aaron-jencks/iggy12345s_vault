import numpy as np
import cv2
from collections import deque
import threading
import time

class Pop_Up(threading.Thread):
	"""A simple class for showing the frames of the image queue in real-time"""
	
	def __init__(self, given_queue):
		threading.Thread.__init__(self)
		self.imgQueue = given_queue
		self.isStarted = False
		
	def run(self):
		self.isStarted = True
		while self.isStarted:
			time.sleep(0.05)
			while imgQueue and len(self.imgQueue) > 0:
				show_frame()
	
	def show_frame(self):
		"""Reads and displays a single frame"""
		if imgQueue and len(self.imgQueue > 0:
			frame = self.imgQueue.popleft()
			cv2.imshow('frame', frame)
			cv2.waitKey(1)
			
	def dispose(self):
		if self.isStarted:
			self.isStarted = False