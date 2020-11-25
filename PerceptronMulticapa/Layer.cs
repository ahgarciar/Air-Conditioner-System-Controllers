namespace Backpropagation
{
    public class Layer
    {
        public int numberOfNeurones
        {
            get
            {
                return (neurones.Length);
            }
        }
        protected Neurone[] neurones;
        public float[] outputs;

        public Layer(int numberOfInputs, int numberOfNeurones)
        {
            int i;
            neurones = new Neurone[numberOfNeurones];
            outputs = new float[numberOfNeurones];
            for (i = 0; i < neurones.Length; i++)
            {
                neurones[i] = new Neurone(numberOfInputs);
            }
        }

        public virtual void Calculate(float[] input)
        {
            int i;
            for (i = 0; i < this.neurones.Length; i++)
            {
                outputs[i] = neurones[i].Calculate(input);
            }
        }

        public virtual void CalculateDelta(Layer nextLayer)
        {            
        }

        public virtual void CalculateDelta(float[] targets)
        {            
        }

        public float AccumulateDelta(int i, Layer nextLayer)
        {
            int j;
            float tot = 0;
            for (j = 0; j < nextLayer.numberOfNeurones; j++)
            {
                tot += nextLayer.neurones[j].delta * nextLayer.neurones[j].weights[i];
            }
            return (tot);
        }

        public void UpdateWTheta(float[] inputs)
        {
            int i, j;
            for (i = 0; i < neurones.Length; i++)
            {
                neurones[i].theta += -.2f * neurones[i].delta;
                for (j = 0; j < neurones[i].weights.Length; j++)
                {
                    neurones[i].weights[j] += -.2f * neurones[i].delta * inputs[j];
                }
            }
        }
    }
}
