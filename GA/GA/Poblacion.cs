using System.Collections.Generic;
using System.Linq;

namespace GA
{
    class Poblacion
    {
        private List<Solucion> soluciones;

        public static List<SetPoint> preferencesLimits;
        public static double[] a, b, p, E;        

        public void calc_Eminmax(Solucion values) {

            E[0] = p[0] + p[0] * (values.VariablesDecision[0] - preferencesLimits[0].Max);
            E[1] = p[0] + p[0] * (values.VariablesDecision[0] - preferencesLimits[0].Min);

            /*
            //E[0] = 0;
            //E[1] = 0;

            E[0] = p[0] * (values.VariablesDecision[0] - preferencesLimits[0].Max);
            E[1] = p[0] * (values.VariablesDecision[0] - preferencesLimits[0].Min);

            //Sin TEMP
            for (int i = 1; i < p.Length; i++)
            {
                //min
                E[0] += p[i] * (preferencesLimits[i].Min - values.VariablesDecision[i]);

                //max
                E[1] += p[i] * (preferencesLimits[i].Max - values.VariablesDecision[i]);
            }
                        
           // Console.WriteLine("Emin: " + Poblacion.E[0]);
           // Console.WriteLine("Emax: " + Poblacion.E[1]);
           */
        }


        public Poblacion(int tamPoblacion, List<SetPoint> preferencesLimits, double []a, double []b, double []p)
        {
            E = new double[3];

            Poblacion.a = a;
            Poblacion.b = b;
            Poblacion.p = p;

            Poblacion.preferencesLimits = preferencesLimits;

            this.soluciones = new List<Solucion>();

            for (int i = 0; i < tamPoblacion; i++)
            {
                soluciones.Add(new Solucion());
            }

        }

        public void restartPoblacion() {
            int t = soluciones.Count();
            soluciones.Clear();

            for (int i = 0; i < t; i++)
            {
                soluciones.Add(new Solucion());
            }
        }


        public int evaluarPoblacion(Solucion currentValues)
        {            

            //
            for (int i = 0; i < soluciones.Count(); i++)
            {
                //E0 para cada solucion
                //E[2] = 0;

                E[2] = p[0] + p[0] *
                        (currentValues.VariablesDecision[0] -
                         soluciones[i].VariablesDecision[0]);
                
                /*
                //SIN CONTAR TEMP
                for (int j = 1; j < p.Length; j++)
                {
                    E[2] += p[j] * (soluciones[i].VariablesDecision[j] - currentValues.VariablesDecision[j]);                                                    
                }                
                //Console.WriteLine(E[2]);
                */

                soluciones[i].evalSolucion(currentValues);

                //Console.WriteLine("Val Obj: " + soluciones[i].ValorObjetivo);
            }

            return soluciones.Count();
        }

        public int evaluarHijos(List<Solucion> hijos, Solucion currentValues)
        {

            for (int i = 0; i < hijos.Count(); i++)
            {
                //E[2] = 0;

                E[2] = p[0] + p[0] *
                        (currentValues.VariablesDecision[0] -
                         hijos[i].VariablesDecision[0]);

                /*
                //E0 para cada hijo
                //SIN CONTAR TEMP
                for (int j = 1; j < p.Length; j++)
                {
                    E[2] += p[j] * (hijos[i].VariablesDecision[j] - currentValues.VariablesDecision[j]);                          
                } 
                */

                hijos[i].evalSolucion(currentValues);
            }

            return hijos.Count();
        }
       

        public List<Solucion> getSoluciones()
        {
            return soluciones;
        }

        public int getTotalSoluciones()
        {
            return soluciones.Count();
        }

       
        public void recuperarMejoresSoluciones(int solucionesRequeridas){
            List<Solucion> mejoresSoluciones = new List<Solucion>();
            int k = 0;
            while(k <= soluciones.Count && mejoresSoluciones.Count < solucionesRequeridas){ 
              //n mejores
              //ruleta 
                mejoresSoluciones.Add(soluciones[k++]);                   
            }
           
            soluciones = mejoresSoluciones;
        }


        public void fusionarPadresHijos(List<Solucion> hijos)
        {
          for (int i = 0; i < hijos.Count(); i++)
            {
                soluciones.Add(hijos[i]);
            }

        }

        internal void ordenarSoluciones()
        {
            soluciones.Sort();
        }
    }
}
