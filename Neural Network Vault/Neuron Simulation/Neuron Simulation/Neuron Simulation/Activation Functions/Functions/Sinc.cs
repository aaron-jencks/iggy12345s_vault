﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuron_Simulation.Activation_Functions.Functions
{
    class Sinc : ActivationFunction
    {
        // Rectified Linear Unit
        public override double Activate(double x, ActivationParameters Params)
        {
            return (x == 0) ? 1 : Math.Sin(x) / x;
        }

        public override double Derivate(double x, ActivationParameters Params)
        {
            return (x == 0) ? 0 : (Math.Cos(x) / x) - (Math.Sin(x) / x);
        }
    }

    public class SincParams : ActivationParameters
    {
    }
}
