using System;

namespace NSGA2
{
    public class FuncionesObjetivo
    {          
        private int funcion { get; set; }
        private Solucion currentValues { get; set; }

        private int numObjetivos = 2;

        public FuncionesObjetivo(int funcion)
        {
            this.funcion = funcion;
        }

        public FuncionesObjetivo(int funcion, Solucion currentValues)
        {
            this.funcion = funcion;
            this.currentValues = currentValues;
        }

        public double[] getValoresFunciones(double[] val)
        {
            double[] valores = new double[numObjetivos];
            switch (funcion)
            {
                case 1: //Not Definen by this work
                    break;
                case 2: //Not Definen by this work
                    break;
                case 3: //Not Definen by this work
                    break;
                case 4: //Not Definen by this work
                    break;
                case 5: //Not Definen by this work
                    break;
                case 6: //Not Definen by this work
                    break;
                case 7: //Tesis
                    valores[0] = Poblacion.a[0] * Guc(val);

                    if (val[0] < currentValues.getVariablesDecision()[0])
                    {
                        valores[1] = Poblacion.a[1] * Ges(val);
                    }
                    else {
                        valores[1] = Poblacion.a[1] * 1.0;
                    }
                    
                    break;
            }
            return valores;
        }

        private static double Guc(double[] x)
        {

            double valor = 0;

            valor = Poblacion.b[0] * (Math.Pow(
                (Poblacion.preferencesLimits[0].Max - x[0]) /
                (Poblacion.preferencesLimits[0].Max - Poblacion.preferencesLimits[0].Min)
                , 2));

            /*
            for (int i = 1; i < x.Length; i++)
            {
                valor += Poblacion.b[i] * 
                    (1 - Math.Pow((Poblacion.preferencesLimits[i].Max - x[i]) 
                    / (Poblacion.preferencesLimits[i].Max - Poblacion.preferencesLimits[i].Min), 2));
            }
            */

            //Console.WriteLine("Guc: " +valor);

            return valor;
        }

        private static double Ges(double[] x)
        {

            double valor = 0;

            //Console.WriteLine("Emin: " + Poblacion.E[0]);
            //Console.WriteLine("Emax: " + Poblacion.E[1]);
            //Console.WriteLine("E0: " + Poblacion.E[2]);

            valor = 1 - Math.Pow((Poblacion.E[2] - Poblacion.E[0]) /
                        (Poblacion.E[1] - Poblacion.E[0]), 2);

            //Console.WriteLine("Ges: " + valor);
            
            return valor;

        }
         
    }
}
