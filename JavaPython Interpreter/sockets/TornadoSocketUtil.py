import tornado.ioloop
import tornado.web
import tornado.websocket

# For parallelism
from collections import deque
from multiprocessing.managers import BaseManager

# For events
from PyQt5.QtCore import QObject, pyqtSignal

class QueueManager(BaseManager):
    pass

class DequeProxy():
    """Used to house a multithreaded queue"""

    def __init__(self):
        self.q = deque()

    def __len__(self):
        return len(self.q)

    def append(self, thing):
        self.q.append(thing)

    def pop(self)
        if len(self.q) > 0:
            return self.q.pop()
        return None

    def clear(self):
        self.q.clear()

    def get(self):
        return self.q

QueueManager.register('DequeProxy', DequeProxy, exposed=['__len__', 'get', 'append', 'pop', 'clear'])

class WebSocket(tornado.websocket.WebSocketHandler):
    clients = set()

    def Initialization(self, rx_q, tx_sock):
        self.rx_q = rx_q
        tx_sock.connect(on_tx)
        self.is_stopping = False

    def on_tx(self, obj:object):
        self.write_message(obj, str=True)
        

    def check_origin(self, origin):
        # Allow access from every origin
        return True

    def open(self):
        WebSocket.clients.add(self)
        print("WebSocket opened from: " + self.request.remote_ip)

    def on_message(self, message):
        self.rx_q.append(message)

    def on_close(self):
        print("WebSocket closed from: " + self.request.remote_ip)
        self.is_stopping = True  
        

class SocketServer():
    """Wraps the tornado.websocket.WebSocketHandler class
    allowing for easy sending and receiving of strings"""

    Tx = pyqtSignal(object, name='Tx')

    def __init__(self, socket, static_path):
        """Creates a new SocketServer for the given socket
        
        socket is a callable for type 
        tornado.websocket.WebSocketHandler

        static_path is the path to the index.html for the site"""

        self.__q_manager = QueueManager()
        self.__q_manager.start()

        self.__socket = socket
        self.__path = static_path
        self.port = None
        self.is_started = False
        self.rx_q = self.__q_manager.DequeProxy()
        self.tx_q = self.__q_manager.DequeProxy()

    def start(self, port=None):
        """Starts the server on the given port"""

        if not self.is_started:

            if port:
                self.port = port

            self.__app = tornado.web.Application([
                (r"/websocket", self.__socket, {'rx_q': self.rx_q, 'tx_sock': self.tx_q}),
                (r"/(.*)", tornado.web.StaticFileHandler, {'path': self.__path, 'default_filename': 'index.html'}),
            ])
            self.__app.listen(self.port)

            tornado.ioloop.IOLoop.current().start()

            self.is_started = True

            return (self.rx_q, self.tx_q)

        else:
            print('Cannot start the server, it is already started')

    def send_message(self, message):
        """Sends a message from the socket to all clients"""

        self.__app.










