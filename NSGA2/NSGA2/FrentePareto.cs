using System.Collections.Generic;
using System.Linq;

namespace NSGA2
{
    class FrentePareto
    {

        private List<Solucion> soluciones { get; set; }

        public FrentePareto() 
        {
            soluciones = new List<Solucion>();
        }

        public void agregarSolucion(Solucion sol)
        {
            soluciones.Add(sol);
        }

        public Solucion getSolucion(int i)
        {
            return soluciones[i];
        }

        public Solucion getSolucionDominada(int i, int j)
        {
            return soluciones[i].getSolucionSp(j);
        }

        public int getCantSolucionesDominadas(int i)
        {
            return soluciones[i].getTotalSolucionesSp();
        }


        public int totalSoluciones()
        {
            return soluciones.Count;
        }

        public double MaxValue(int objetivo)
        {
            double valorMax = double.MinValue;
            for (int i = 0; i < soluciones.Count(); i++)
            {
                if (soluciones[i].getValoresObjetivo()[objetivo] > valorMax)
                {
                    valorMax = soluciones[i].getValoresObjetivo()[objetivo];
                }
            }
            return valorMax;
        }


        public double MinValue(int objetivo)
        {
            double valorMin = double.MaxValue;
            for (int i = 0; i < soluciones.Count(); i++)
            {
                if (soluciones[i].getValoresObjetivo()[objetivo] < valorMin)
                {
                    valorMin = soluciones[i].getValoresObjetivo()[objetivo];
                }
            }
            return valorMin;
        }


        public void Crowding_Distance_Assigment()
        {            
            if(soluciones.Count==0)
            {
                return;
            }

            for (int i = 0; i < soluciones[0].getCantObjetivos(); i++)
            { //For each objetive

                double valorMaximoObjetivo = MaxValue(i);
                double valorMínimoObjetivo = MinValue(i);

                if (i == 0)
                {                    
                    soluciones.Sort(delegate (Solucion sol1, Solucion sol2)
                    {                 
                        return sol1.getValoresObjetivo()[0].CompareTo(sol2.getValoresObjetivo()[0]);
                    });

                }
                else
                {                    
                    soluciones.Sort(delegate (Solucion sol1, Solucion sol2)
                    {                        
                        return sol1.getValoresObjetivo()[1].CompareTo(sol2.getValoresObjetivo()[1]);
                    });

                }
                
                soluciones[0].setDistancia(soluciones[0].getDistancia() + valorMaximoObjetivo);
                soluciones[soluciones.Count() - 1].setDistancia(soluciones[soluciones.Count() - 1].getDistancia() + valorMaximoObjetivo);

                for (int j = 1; j < soluciones.Count() - 1; j++)
                {
                    soluciones[j].setDistancia(
                     soluciones[j].getDistancia() +
                     ((soluciones[j + 1].getValoresObjetivo()[i] - soluciones[j - 1].getValoresObjetivo()[i])
                             / (valorMaximoObjetivo - valorMínimoObjetivo))
                    );
                }

            }

        }

        public void ordenarSoluciones()
        {            
            soluciones.Sort(delegate (Solucion sol1, Solucion sol2)
            {         
                return sol1.getDistancia().CompareTo(sol2.getDistancia()) * -1;
            });

        }


    }
}
