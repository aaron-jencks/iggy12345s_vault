#!/bin/bash

# To use this, simply pass in the directoy that you want the github repositories downloaded into in as the first argument

echo "Installing git, if you don't already have it"
sudo apt install git
echo "Need to make sure you have python 3.6 and pip3"
echo "Installing python 3.6"
sudo apt-get update
sudo apt-get install python3.6
echo "Installing pip3"
sudo apt-get install python3-pip
echo "Upgrading the default python 3.5 to 3.6"
sudo update-alternatives --install /usr/bin/python3 python3 /usr/bin/python3.5 1
sudo update-alternatives --install /usr/bin/python3 python3 /usr/bin/python3.6 2
sudo update-alternatives --config python3
echo "Creating aliases for python->python3 and pip->pip3"
sudo sh -c 'echo "alias python=python3" >> ~/.bash_aliases'
sudo sh -c 'echo "alias pip=pip3" >> ~/.bash_aliases'
source ~/.bash_aliases
echo "Installing pip packages"
sudo apt-get install protobuf-compiler
pip3 install tensorflow
pip3 install --user Cython
pip3 install --user contextlib2
pip3 install --user pillow
pip3 install --user lxml
pip3 install --user jupyter
pip3 install --user matplotlib
pip3 install --user opencv-python
echo "Moving to your desired github download location"
cd "$1"
echo "We need to download the github repos for tensorflow/models and coco"
echo "Downloading coco"
mkdir tensorflow
#git clone https://github.com/cocodataset/cocoapi.git
echo "Downloading tensorflow/models"
cd tensorflow
#git clone https://github.com/tensorflow/models.git
echo "Building cocoapi for python and copying to tensorflow/models/research"
cd ../cocoapi/PythonAPI
make
cp -r pycocotools ../../tensorflow/models/research
echo "Compiling protobufs"
cd ../../tensorflow/models/research
protoc object_detection/protos/*.proto --python_out=.
echo "Compiling and installing in general"
python3 setup.py build
python3 setup.py install
echo "Adding libraries to PYTHONPATH"
py_path="$(pwd)"
export PYTHONPATH=$PYTHONPATH:${py_path}:${py_path}"/slim"
sudo sh -c 'echo "export PYTHONPATH=$PYTHONPATH:$1:$1/slim" >> ~/.bashrc'
echo "Testing the installation"
python3 object_detection/builders/model_builder_test.py
echo "Installation complete!"
