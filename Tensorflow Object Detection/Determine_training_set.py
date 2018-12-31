from random import shuffle
import os, fnmatch
import pandas as pd
import xml.etree.ElementTree as ET

def read_train_val():
	"""Reads the names of all image files from the ./images/raw/ directory and then
	splits it up into multiple parts to be used by training and testing
	int the typical 90%/10% ratio."""
	
	# Maps the directory for image files and reads in all of the lines and then shuffles them
	files = list(map(lambda x: "./images/raw/" + x, fnmatch.filter(os.listdir("./images/raw/"), "*.jpg")))
	shuffle(files)
	
	for name in files:
		print(name)
	
	# Splits the files into two arrays of train values, and test values
	split_loc = int(len(files) * 0.9)
	train_values = files[:split_loc]
	test_values = files[split_loc:]
	return (train_values, test_values)
	
if __name__ == '__main__':
	"""Separates the image files in the ./images/raw/ directory into two categories
	then moves them into two directories, ./images/train/, and ./images/test/."""

	# Reads in the trainval.txt file and shuffles its contents
	# Then splits it into training and testing values
	(train, test) = read_train_val()
	
	