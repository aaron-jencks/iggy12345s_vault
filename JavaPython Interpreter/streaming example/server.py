from autobahn.twisted.websocket import WebSocketServerProtocol, WebSocketServerFactory
import base64
import sys
from twisted.python import log
from twisted.internet import reactor
from PIL import Image
from mss import mss
import io
#app = wx.App()  # Need to create an App instance before doing anything
#screen = wx.ScreenDC()
#size = screen.GetSize()
#bmp = wx.EmptyBitmap(size[0], size[1])
stream = io.BytesIO()
m = mss()

class MyServerProtocol(WebSocketServerProtocol):

	def onConnect(self, request):
		print("Client connecting: {}".format(request.peer))

	def onOpen(self):
		print("WebSocket connection open.")

		def hello():
			#screen = wx.ScreenDC()
			#mem = wx.MemoryDC(bmp)
			#mem.Blit(0, 0, size[0], size[1], screen, 0, 0)
			#del mem  # Release bitmap
			#with open("./var/img/image.png", "rb") as image_file:
			#   encoded_string = base64.b64encode(image_file.read())
			#print(dir(bmp.ConvertToImage().GetDataBuffer()))
			#print("Converting to image")
			#img = bmp.ConvertToImage()
			#bmp.CopyToBuffer(stream)
			#print("Converting to buffer")
			#print(img.SaveFile(stream, wx.BITMAP_TYPE_PNG))
			#print("Getting bytes")
			#print(dir(img.GetData()))
			img = m.shot()#ImageGraph.grab() #wx2PIL(bmp)
			img.save(stream, "JPEG", quality=self.quality, optimize=True)
			encoded_string = stream.getvalue() #base64.b64encode(stream.getvalue()) #base64.b64encode(img.GetData().encode('utf8')) #stream.read() #base64.b64encode(stream.read())
			del mem  # Release bitmap
			#stream.flush()
			#print(encoded_string)
			self.sendMessage(encoded_string)#.encode('utf8'))
			self.factory.reactor.callLater(0.2, hello)

		# start sending messages every 20ms ..
		hello()

	def onMessage(self, payload, isBinary):
		if isBinary:
			print("Binary message received: {} bytes".format(len(payload)))
		else:
			print("Text message received: {}".format(payload.decode('utf8')))

		# echo back message verbatim
		self.sendMessage(payload, isBinary)

	def onClose(self, wasClean, code, reason):
		print("WebSocket connection closed: {}".format(reason))


if __name__ == '__main__':
    log.startLogging(sys.stdout)

    factory = WebSocketServerFactory(u"ws://127.0.0.1:50007")
    factory.protocol = MyServerProtocol
    # factory.setProtocolOptions(maxConnections=2)

    # note to self: if using putChild, the child must be bytes...

    reactor.listenTCP(50007, factory)
    reactor.run()
    stream.close()
