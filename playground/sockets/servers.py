from PyQt5.QtCore import QObject, pyqtSignal
import socket as sck
import multiprocessing as mp
from collections import deque
from multiprocessing import Process
import os

# Local packages
from .CommUtil import IOModule, Packet

class AServer(IOModule):
	
