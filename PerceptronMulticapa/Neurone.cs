using System;

namespace Backpropagation
{
    public class Neurone
    {
        private Random rand = new Random();
        internal float[] weights;
        internal float theta;
        private float output;
        internal float delta;
        private delegate float _actFun(float x);
        private delegate float _actFunDer(float x);
        private _actFun actFun;
        private _actFunDer actFunDer;

        public Neurone(int numberOfInputs)
        {
            int i;

            weights = new float[numberOfInputs];
            for (i = 0; i < weights.Length; i++)
            {
                weights[i] = (float)rand.NextDouble();
            }
            theta = (float)rand.NextDouble();
            actFun = Sigmoid;
            actFunDer = SigmoidDerivative;
        }

        public float Sigmoid(float x)
        {
            return (float)(1 / (1 + Math.Exp(-x)));
        }

        public float SigmoidDerivative(float x)
        {
            return (x * (1 - x));
        }

        public float Calculate(float[] input)
        {
            int i;
            float total = 0;
            for (i = 0; i < input.Length; i++)
            {
                total += input[i] * weights[i];
            }

            total += theta;
            output = actFun(total);
            return (output);
        }

        public void CalculateDeltaOutput(float target)
        {
            delta = actFunDer(output) * (output - target);
        }

        public void CalculateDeltaHidden(float accumulatedDelta)
        {
            delta = actFunDer(output) * accumulatedDelta;
        }

    }
}
