import tensorflow as tf
from tqdm import tqdm
import numpy as np


def gen_placeholders(graph: tf.Graph, n: int = 100, type_x=tf.float32, type_y=tf.float32) -> tuple:
    """Creates placeholder variables for x and y variables, returns pointers to the placeholders as (x, y)
    Creates the placeholders in their own namespace called 'placeholders'"""

    with graph.as_default():
        with tf.name_scope("placeholders"):
            x = tf.placeholder(type_x, (n, 1))
            y = tf.placeholder(type_y, (n,))

    return x, y


def gen_weights(graph: tf.Graph) -> tuple:
    """Creates weight and bias variables for the given graph, puts them into their own namespace 'weights'
    returns pointers to the weight variable and the bias variable as (W, b)"""

    with graph.as_default():
        with tf.name_scope("weights"):
            W = tf.Variable(tf.random_normal((2, 1)))
            b = tf.Variable(tf.random_normal((1,)))

    return W, b


def gen_basic_neuron(graph: tf.Graph, x: tf.Tensor, w: tf.Tensor, b: tf.Tensor) -> tuple:
    """Creates a basic neuron function Wx+b and returns a pointer to the operation"""
    # TODO rewrite documentation

    with graph.as_default():
        with tf.name_scope("prediction"):
            y_logit = tf.squeeze(tf.matmul(x, w) + b)
            y_one_prob = tf.sigmoid(y_logit)
            y_pred = tf.round(y_one_prob)

    return y_logit, y_one_prob, y_pred

# TODO Continue from here
def gen_basic_loss(graph: tf.Graph, y: tf.Tensor, y_logits: tf.Tensor) -> tf.Tensor:
    """Generates a basic loss function and returns a pointer to it"""

    with graph.as_default():
        with tf.name_scope("loss"):
            entropy = tf.nn.sigmoid_cross_entropy_with_logits(logits=y_logits, labels=y)
            loss = tf.reduce_sum(entropy)

    return loss


def gen_summary_loss(graph: tf.Graph, loss: tf.Tensor) -> tf.Tensor:
    """Generates a summary of the loss tensor scalar and returns a pointer to the merged summaries"""

    with graph.as_default():
        with tf.name_scope("summaries"):
            tf.summary.scalar("loss", loss)
            merged = tf.summary.merge_all()

    return merged


def train(graph: tf.Graph, x: tf.Tensor, x_np, y: tf.Tensor, y_np,
          loss: tf.Tensor, weights: tf.Tensor, bias: tf.Tensor, y_pred: tf.Tensor,
          train_op: tf.Tensor, merged_summary: tf.Tensor, train_writer: tf.summary.FileWriter,
          n_steps: int = 8000) -> tuple:
    """Trains the given graph for the given number of steps
    returns the final weights, bias, final loss, and prediction as (y_pred, w, b, l)"""

    with graph.as_default():
        with tf.Session() as sess:
            sess.run(tf.global_variables_initializer())

            for i in tqdm(range(n_steps)):
                feed_dict = {x: x_np, y: y_np}
                _, summary, loss_r = sess.run([train_op, merged_summary, loss], feed_dict=feed_dict)
                # print("step {}, loss: {}".format(i, loss_r))
                train_writer.add_summary(summary, i)

            print("Final loss: {}".format(loss_r))

            # Get the final weights and biases
            w_final, b_final = sess.run([weights, bias])

            # Make predictions
            y_pred_np = sess.run(y_pred, feed_dict={x: x_np})

    return np.reshape(y_pred_np, -1), w_final, b_final, loss_r
