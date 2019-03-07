import cv2
import tensorflow as tf
import numpy as np
from CamClass import CamClass
from collections import deque

# Sets up the image queues
img_queue = deque()

# Sets up the camera
cam = CamClass(img_queue)