from PyQt5.QtCore import QObject, pyqtSignal
import socket as sck
import multiprocessing as mp
from collections import deque
from multiprocessing import Process
from multiprocessing.managers import BaseManager
import os

class ConnectionManager(BaseManager):
	pass

class ConnectionProxy(sck.socket):
	"""Represents a connection to an external computer"""
	pass
