#!/bin/bash

# Execute this program from the directory that you are currently working in
# Pass in your Github repo directory as the first argument

echo "If you haven't run the installations_helper.sh script, you should probably do that"
echo "Creating file directory"
mkdir "data"
mkdir "models"
mkdir "models/model"
mkdir "models/model/train"
mkdir "models/model/eval"
echo "Downloading pet images and annotations"
wget http://www.robots.ox.ac.uk/~vgg/data/pets/data/images.tar.gz
wget http://www.robots.ox.ac.uk/~vgg/data/pets/data/annotations.tar.gz
tar -xf images.tar.gz
tar -xf annotations.tar.gz
echo "Copying label map"
cp "$1/object_detection/data/pet_label_map.pbtxt" "data"
echo "Creating TFRecords"
echo $1
p="${1}""/object_detection/dataset_tools/create_pet_tf_record.py"
echo $p
sudo python3 $p --label_map_path="./data/pet_label_map.pbtxt" --data_dir='.' --output_dir='./data'
echo "Downloading the pretrained model"
model_name="ssdlite_mobilenet_v2_coco_2018_05_09"
#http://download.tensorflow.org/models/object_detection/ssdlite_mobilenet_v2_coco_2018_05_09.tar.gz
wget "http://storage.googleapis.com/download.tensorflow.org/models/object_detection/${model_name}.tar.gz"
tar -xvf "${model_name}.tar.gz" -C ./models
cp "./models/${model_name}/*.ckpt" ./models/model
echo "Copying the config file"
cp "$1/object_detection/samples/configs/faster_rcnn_resnet101_pets.config" "models/model"
echo "Configuring the config file"
data_dir="$(pwd)""/data"
sed -i "s|PATH_TO_BE_CONFIGURED|${data_dir}|g" models/model/faster_rcnn_resnet101_pets.config
sed -i "s|${data_dir}/model.ckpt|$(pwd)/models/model/model.ckpt|g" models/model/faster_rcnn_resnet101_pets.config
echo "You should be good to go!"
