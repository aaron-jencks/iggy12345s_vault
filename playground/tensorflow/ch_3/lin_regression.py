import tensorflow as tf
from toy_dataset.np_set import *
from lin_regression.example_3_11 import *
from lin_regression.accuracy_meas import *

x, y = gen_data()

# show_data(x, y)

graph = tf.Graph()
x_p, y_p = gen_placeholders(graph)
W_p, b_p = gen_weights(graph)
y_pred_p = gen_basic_neuron(graph, x_p, W_p, b_p)
l_p = gen_basic_loss(graph, y_p, y_pred_p)
with graph.as_default():
    with tf.name_scope("optim"):
        train_op = tf.train.AdamOptimizer(0.001).minimize(l_p)

merged_p = gen_summary_loss(graph, l_p)

train_writer = tf.summary.FileWriter('/tmp/lr-train', graph)

loss_r = 100
y_pred_np = None
w_final = None
b_final = None

while loss_r > 5:
    y_pred_np, w_final, b_final, loss_r = train(graph, x_p, x, y_p, y, l_p, W_p, b_p, y_pred_p,
                                                train_op, merged_p, train_writer)

r2 = pearson_r2_score(y, y_pred_np)
print("Pearson R^2: %f" % r2)
rms = rms_score(y, y_pred_np)
print("RMS: %f" % rms)

show_comparison_data(x, y, y_pred_np, w_final, b_final)