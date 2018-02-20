﻿using NeuralNetworkFundamentals;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Debugging_Application_2
{
    class Program
    {
        static bool IsTraining;
        static int iterations;

        static List<List<double>> sampleIn;

        static List<List<double>> sampleOut;

        static void Main(string[] args)
        {
            Console.WriteLine("Setting up test...");
            iterations = 2000;
            List<int> layerInfo = new List<int>() { 2, 2, 1 };
            Random rnd = new Random();

            NeuralNetwork net = new NeuralNetwork(layerInfo, learningRate: 0.5, momentum: rnd.NextDouble());   // Creates a network with 2 inputs, 1 hidden layer of 2, and 2 outputs

            // Sets the weights and biases of the network prior to training
            
            /*
            net.Biases = new List<List<double>>()
            {
                new List<double>(){0,0 },
                new List<double>(){0.35, 0.35 },
                new List<double>(){0.6, 0.6 }
            };

            net.Weights = new List<List<List<double>>>()
            {
                new List<List<double>>()
                {
                    new List<double>(){0.15, 0.2 },
                    new List<double>(){0.25, 0.3 }
                },
                new List<List<double>>()
                {
                    new List<double>(){0.4, 0.45 },
                    new List<double>(){0.5, 0.55 }
                }
            };
            */
            
            net.GenWeightsAndBiases();

            net.TrainingUpdateEvent += OnTrainingUpdateEvent;
            net.TrainingFinishEvent += OnTrainingFinishEvent;

            // Creates the samples and outputs
            
            sampleIn = new List<List<double>>()
            {
                new List<double>(){0, 0 },
                new List<double>(){0, 1 },
                new List<double>(){1, 0 },
                new List<double>(){1, 1 }
            };

            sampleOut = new List<List<double>>()
            {
                new List<double>(){0},
                new List<double>(){1},
                new List<double>(){1},
                new List<double>(){0}
            };
            

            //sampleIn = new List<List<double>>() { new List<double>() { 0.05, 0.1 } };
            //sampleOut = new List<List<double>>() { new List<double>() { 0.01, 0.99 } };

            // Trains the network
            IsTraining = true;
            net.Train(iterations, sampleIn, sampleOut);
            while (IsTraining) ;

            Console.Clear();
            Console.WriteLine("Training Complete!");
            Console.WriteLine("Testing inputs");
            foreach (List<double> sample in sampleIn)
            {
                List<double> temp = net.Calc(sample);
                Console.WriteLine("For Inputs [{0}, {1}]:", sample[0], sample[1]);
                //Console.WriteLine("The output is: {0}, {1}\n", temp[0], temp[1]);
                Console.WriteLine("The output is: {0}\n", temp[0]);
            }

            Console.ReadKey();
        }

        public static void OnTrainingUpdateEvent(object sender, TrainingUpdateEventArgs result)
        {
            // Executes every time the network finishes a training sample
            Console.WriteLine("Finished Iteration " + result.Iteration);
            Console.WriteLine("Sample [{0}, {1}] XOR [{2}]", result.Layers[0][0], result.Layers[0][1], result.Layers.Last()[0].Activation);
            //Console.WriteLine("Error: {0}", result.Error);
        }

        public static void OnTrainingFinishEvent(object sender)
        {
            IsTraining = false;
        }
    }
}
