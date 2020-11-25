namespace Backpropagation
{
    public class HiddenLayer : Layer
    {
        public HiddenLayer(int numberOfInputs, int numberOfNeurones)
            : base(numberOfInputs, numberOfNeurones)
        {
        }

        public override void CalculateDelta(Layer nextLayer)
        {
            int i;
            for (i = 0; i < this.neurones.Length; i++)
            {
                neurones[i].CalculateDeltaHidden(AccumulateDelta(i, nextLayer));
            }
        }
    }
    }
