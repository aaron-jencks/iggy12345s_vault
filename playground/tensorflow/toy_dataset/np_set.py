import numpy as np
import tensorflow as tf
from matplotlib import rc
rc('text', usetex=False)
import matplotlib.pyplot as plt


def gen_lin_data(n: int = 100, w_true: int = 5, b_true: int = 2, noise_scale: float = 0.1) -> tuple:
    """Creates a synthetic linear dataset using numpy and returns two arrays in the form of a tuple"""

    x_np = np.random.rand(n, 1)
    noise = np.random.normal(scale=noise_scale, size=(n, 1))
    y_np = np.reshape(w_true * x_np + b_true + noise, (-1))

    return x_np, y_np


def gen_log_data(n: int = 100) -> tuple:
    """Creates a synthetic logistic dataset using numpy and returns two arrays in the form of a tuple"""

    x_zeroes = np.random.multivariate_normal(
        mean=np.array((-1, -1)),
        cov=0.1*np.eye(2),
        size=(n//2,)
    )

    y_zeroes = np.zeros((n//2,))

    x_ones = np.random.multivariate_normal(
        mean=np.array((1, 1)),
        cov=0.1*np.eye(2),
        size=(n//2,)
    )

    y_ones = np.ones((n//2,))

    x_np = np.vstack([x_zeroes, x_ones])
    y_np = np.concatenate([y_zeroes, y_ones])

    return x_np, y_np


def show_data(x_np, y_np):
    """Shows the numpy data as a plt"""

    fig = plt.figure("Toy Linear Regression Data")
    fig.scatter(x_np, y_np)
    fig.xlabel("x")
    fig.ylabel("y")
    fig.xlim(0, 1)
    fig.title("Toy Linear Regression Data")
    fig.show()


def show_comparison_data(x_np_orig, y_np_orig, y_np_final, weights_final: tf.Tensor, bias_final):
    """Shows the numpy data as a plt with the final data overlayed on top"""

    fig, axs = plt.subplots(1, 2)

    # axs[0].xlabel("Y-true")
    # axs[0].ylabel("Y-pred")
    # axs[0].title("Predicted versus true values")
    axs[0].scatter(y_np_orig, y_np_final)

    # axs[1].xlabel("x")
    # axs[1].ylabel("y")
    # axs[1].title("True model versus Learned model")
    # axs[1].xlim(0, 1)
    axs[1].scatter(x_np_orig, y_np_orig)

    x_left = 0
    y_left = weights_final[0]*x_left + bias_final
    x_right = 1
    y_right = weights_final[0]*x_right + bias_final
    axs[1].plot([x_left, x_right], [y_left, y_right], color='k')

    fig.suptitle("True model versus Learned model")

    plt.show()
