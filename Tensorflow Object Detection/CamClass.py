import cv2
import threading
from collections import deque

# Used to create a camera instance
class CamClass(threading.Thread):
	"""A simple class for handling the camera"""
	
	def __init__(self, given_queue, given_frame_delay=120, use_frame_cap=True):
		threading.Thread.__init__(self)
		self.__cam = cv2.VideoCapture(0)
		self.isStarted = False
		self.imgQueue = given_queue
		self.max_frame_cap = given_frame_delay
		self.use_frame_cap = use_frame_cap
		
	def run(self):
		self.isStarted = True
		
		# Starts the camera if it's not already started
		if not self.__cam.isOpened():
			self.__cam.open()
			
		print('Reading camera data')
		
		while self.isStarted:
			# Read in the camera frames, one at a time
			ret, frame = self.__cam.read()
			
			if imgQueue:
				# Appends the image frame to the image queue
				self.imgQueue.append(frame)
				if self.use_frame_cap and len(self.imgQueue) > self.max_frame_cap:
					#TODO: There's no reason to clear the whole queue, this is just a quick and dirty method
					self.imgQueue.clear()
				
	def read_frame(self):
		"""Reads and enqueues a single frame"""
		if not self.__cam.isOpened():
			self.__cam.open()
		ret, frame = self.__cam.read()
		self.imgQueue.append(frame)
		
	def dispose(self):
		"""Disposes of the camera object, stopping collection if necessary"""
		if self.isStarted:
			# Stops the camera collection if it's running
			self.isStarted = False
		# Releases the resources held by the camera
		self.__cam.release()
		