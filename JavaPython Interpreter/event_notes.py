from PyQt5.QtCore import QObject, pyqtSignal
import socket as sck
import multiprocessing as mp
from collections import deque
from multiprocessing import Process
import os

class msg():
	"""A message struct used to house anonymous data
	command indicates what the receiving module should do
	data contains any necessary data for the module to
	complete its task
	addr is an optional property which specifies which module
	should receive the message"""
	def __init__(self, command: str, data: object, addr: str):
		self.command = command
		self.data = data
		self.addr = addr

	def __str__(self):
		if len(self.addr) > 0:
			return self.addr + ":" + command
		else:
			return self.command

class CommsModule(QObject):
	"""A communication module with hooks for receiving and
	transmitting messages using events"""

	# Transmit event, triggered when transmitting a message
	Tx = pyqtSignal(object, msg, name='Tx')

	# Receive event, triggered when receiving a message
	Rx = pyqtSignal(object, msg, name='Rx')

	# Hook for a reception handler for subscribing to other modules
	def rx_handler(self, sender, message):
		self.Rx.emit(sender, message)

class CommandParser(CommsModule):
	"""This is the command parser, it is in charge of
	receiving commands from the UI/programmer and sending
	responses from the system"""

	# Update event triggered when the sourc dictionary changes
	# Contains a string with the name of the new source
	update = pyqtSignal(object, str, name="update")
	
	def __init__(self, server):
		self.sources = {}

	def rx_handler(self, sender, message):
		self.Rx.emit(sender, message)

	def subscribe(self, target: str, method):
		if target in sources:
			sources[target].Tx.connect(method)

	def unsubscribe(self, target: str, method):
		if target in sources:
			sources[target].Tx.disconnect(method)

	def register_source(self, name: str, caller: CommsModule):
		if not (name in sources):
			sources[name] = caller
			self.Rx.connect(caller.rx_handler)
			update.emit(name)

	def source_exists(self, name: str) -> bool:
		return name in sources

	def get_sources(self) -> list:
		return [x for x in list(sources.keys())]

class IOHandler(CommsModule):

	count = 0

	def __init__(self, name=""):
		super.__init__(self):
		if len(name) > 0:
			self.name = name
		else:
			self.name = "Handler" + str(count)
			count += 1

	def rx_event(self, sender, message):
		"""Called when this module receives a message
		that is addressed to it, or is global, 
		should be overriden in child classes"""
		pass

	def rx_handler(self, sender, message):
		"""Determines if the message sent to this module was actually addressed here
		or if it's not supposed to receive it."""
		if len(message.addr) > 0 and message.addr == self.name:
			# The message was addressed to this module
			rx_event(sender, message)

	def send_message(self, message):
		"""Sends a message from this module"""
		self.Tx.emit(self, message)

	def send_message(self, command: str, data: object):
		"""Sends a message from this module"""
		send_message(msg(command, data))

class Server(IOHandler):

	HOST = "127.0.0.1"

	def __init__(self, parser:CommandParser, port=-1, host = ""):
		for target in parser.get_sources():
			parser.subscribe(target, self.tx_handler)
		parser.update.connect(self.update_handler)
		self.Rx.connect(parser.rx_handler)
		self.__port = port
		self.__host = host
		self.txq = deque()
		self.rxq = deque()
		self.is_running = False
		self.__sock = Null
		self.__is_stopping = False
		self.open_connections = []

	def __enter__(self):
		connect()

	def __exit__(self):
		disconnect()

	def tx_handler(self, sender:object, message:msg):
		"""Connected to the command parser so that
		when a command is sent from a module it flows
		through here and out to the UI or programmer"""
		self.Tx.emit(sender, message)

		# This is the actual socket part
		if self.is_running:
			

	def send_message(self, message:msg):
		"""Sends a message back to the command parser
		which is then sent back to the modules"""
		self.Rx.emit(self, message)

	def update_handler(self, sender:object, name:str):
		"""Triggered everytime that a module is added
		or removed from the command parser"""
		parser = (CommandParser)sender
		if parser.source_exists(name):
			# The source was added, not removed
			# TODO need some way to determine when
			# the source was edited, but right now
			# there is no way to edit them, so it's
			# okay.
			parser.subscribe(name, self.tx_handler)

	def connection_handler(self):
		while not self.is_stopping:
			

	def disconnect(self):
		if self.is_running:
			self.__sock.close()
			self.is_running = False
			print('Server Disconnected')

	def connect(self, host="", port=-1):
		"""Connects the server to the desired port
		if host is "", uses 127.0.0.1, and if port
		is -1, finds the next available port
		returns the port that was used."""
		if not self.is_running:
			if host == "":
				if self.__host == "":
					host = HOST
				else:
					host = self.__host

			if port is -1:
				if self.__port is -1:
					port = find_available_port()
				else:
					port = self.__port

			self.__sock = sck.socket(sck.AF_INET, sck.SOCK_STREAM)
			self.__sock.bind(host, port)
			self.__sock.listen()
			self.is_running = True
			print('Server Connected')











