using System;

namespace GA
{
    class Solucion : IEquatable<Solucion>, IComparable<Solucion>
    {
        static Random rnd = new Random(5);
        static int cantVariables = 3;

        public double[] VariablesDecision { get; set; }

        public double ValorObjetivo { get; set; }
      
        public Solucion(){
            
            VariablesDecision = new double[cantVariables];

            for (int i = 0; i < cantVariables; i++){
                //VALORES ALEATORIOS PARA LAS DISTINTAS VARIABLES DE DECISIÓN [T L A]
                VariablesDecision[i] = rnd.NextDouble() * (Poblacion.preferencesLimits[i].Max - Poblacion.preferencesLimits[i].Min) + Poblacion.preferencesLimits[i].Min;
            }            
        }

        public Solucion(double[] varDecision){            
            VariablesDecision = varDecision;            
        }
        
        public void evalSolucion(Solucion currentValues)
        {            
            ValorObjetivo = FuncionesObjetivo.getValorObjetivo(VariablesDecision, currentValues);

            //Console.WriteLine("Valor Objetivo: " + ValorObjetivo);
        }
        

        public override bool Equals(object obj)
        {
            return Equals(obj as Solucion);
        }

        public bool Equals(Solucion other)
        {
            return other != null &&
                   ValorObjetivo == other.ValorObjetivo;
        }

        public override int GetHashCode()
        {
            return 1229929353 + ValorObjetivo.GetHashCode();
        }

        public int CompareTo(Solucion other)
        {
            return ValorObjetivo.CompareTo(other.ValorObjetivo) * -1; //mayor a menor
        }

    }
}
