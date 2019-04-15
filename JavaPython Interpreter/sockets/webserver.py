from PyQt5.QtCore import QObject, pyqtSignal
import socket as sck
import multiprocessing as mp
from collections import deque
from multiprocessing import Process
import os

class Packet():
	"""Used to extrapolate data to be sent by the server"""

	def __init__(self):
		pass

	def getHTML(self):
		"""Converts this packet into html code to
		be sent by the server"""
		pass
