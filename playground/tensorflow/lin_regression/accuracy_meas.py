import numpy as np
from scipy.stats import pearsonr
from sklearn.metrics import mean_squared_error


def pearson_r2_score(y, y_pred):
    """Computes Pearson R^2 (square of Pearson correlation)."""
    return pearsonr(y, y_pred)[0]**2


def rms_score(y_true, y_pred):
    """Computes RMS error."""
    return np.sqrt(mean_squared_error(y_true, y_pred))
