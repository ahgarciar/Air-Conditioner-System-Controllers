using System;

namespace GA
{
    class FuncionesObjetivo {
       

        public static double getValorObjetivo(double []val, Solucion currentValues)
        {

            double vo, obj1, obj2;


            obj1 = Poblacion.a[0] * Guc(val);

            if (val[0] < currentValues.VariablesDecision[0])
            {
                obj2 = Poblacion.a[1] * Ges(val);
            }
            else
            {
                obj2 = Poblacion.a[1] * 1.0;
            }


            vo =  obj1 + obj2;

            //Console.WriteLine("vo: " + vo);

            return vo;
        }
        
        private static double Guc(double[] x){

            double valor = 0;

            valor = Poblacion.b[0] * (Math.Pow(
                (Poblacion.preferencesLimits[0].Max - x[0]) /
                (Poblacion.preferencesLimits[0].Max - Poblacion.preferencesLimits[0].Min)
                , 2));

            /*
            double valor = 0 ;

            valor = Poblacion.b[0] * (Math.Pow(
                (Poblacion.preferencesLimits[0].Max - x[0]) / 
                (Poblacion.preferencesLimits[0].Max - Poblacion.preferencesLimits[0].Min)
                , 2));

            for (int i = 1; i < x.Length; i++){
                valor += Poblacion.b[i] * (1 - Math.Pow((Poblacion.preferencesLimits[i].Max-x[i]) / (Poblacion.preferencesLimits[i].Max- Poblacion.preferencesLimits[i].Min),2));
            }
            */

            //Console.WriteLine("Guc: " +valor);

            return valor;
        }

        private static double Ges(double[] x){

            double valor = 0;

            //Console.WriteLine("Emin: " + Poblacion.E[0]);
            //Console.WriteLine("Emax: " + Poblacion.E[1]);
            //Console.WriteLine("E0: " + Poblacion.E[2]);

            valor = 1 - Math.Pow((Poblacion.E[2] - Poblacion.E[0]) /
                        (Poblacion.E[1] - Poblacion.E[0]), 2);

            /*
            double valor = 0;
            
            //Console.WriteLine("Emin: " + Poblacion.E[0]);
            //Console.WriteLine("Emax: " + Poblacion.E[1]);
            //Console.WriteLine("E0: " + Poblacion.E[2]);

            valor = 1 - Math.Pow((Poblacion.E[2] - Poblacion.E[0]) / (Poblacion.E[1] - Poblacion.E[0]) , 2);
            */
            //Console.WriteLine("Ges: " + valor);

            return valor;

        }

        

    }


}
