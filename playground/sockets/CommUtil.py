from PyQt5.QtCore import QObject, pyqtSignal

class Packet():
	"""Used to extrapolate data to be sent by the server"""

	def __init__(self):
		pass

	def getHTML(self):
		"""Converts this packet into html code to
		be sent by the server"""
		pass

class IOModule(QObject):
	"""contains a Tx and Rx queue for the webserver to
	communicate with.  It also houses qt events that
	trigger when a message is received or sent out."""

	# Transmit event, triggered when transmitting a message
	Tx = pyqtSignal(object, Packet, name='Tx')

	# Receive event, triggered when receiving a message
	Rx = pyqtSignal(object, Packet, name='Rx')

	# Hook for a reception handler for subscribing to other modules
	def on_rx(self, sender, packet):
		"""Triggers the Rx event using the sender and packet received"""
		self.Rx.emit(sender, packet)

	def on_tx(self, sender, packet):
		"""Triggers the Tx event using the sender and packet received"""
		self.Tx.emit(sender, packet)





