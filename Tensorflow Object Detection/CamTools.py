import cv2

def get_snapshot(device_id=0):
	"""Does exactly as it sounds like, uses the device id specified or 0 by default and captures a snapshot image."""
	cam = cv2.VideoCapture(device_id)
	ret, frame = cam.read()
	cam.release()
	return frame