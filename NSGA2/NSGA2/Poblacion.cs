using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSGA2
{
    class Poblacion
    {
        private List<Solucion> soluciones;
        private int funcion;
        public static List<SetPoint> preferencesLimits;
        public static double[] a, b, p, E;

        public void calc_Eminmax(Solucion values)
        {
                        
            E[0] = p[0] + p[0] * (values.variablesDecision[0] - preferencesLimits[0].Max);
            E[1] = p[0] + p[0] * (values.variablesDecision[0] - preferencesLimits[0].Min);

            /*
            //Sin TEMP
            for (int i = 1; i < p.Length; i++)
            {
                //min
                E[0] += p[i] * (preferencesLimits[i].Min - values.variablesDecision[i]);

                //max
                E[1] += p[i] * (preferencesLimits[i].Max - values.variablesDecision[i]);
            }*/

            // Console.WriteLine("Emin: " + Poblacion.E[0]);
            // Console.WriteLine("Emax: " + Poblacion.E[1]);                        

        }

        public Poblacion(int tamPoblacion, List<SetPoint> preferencesLimits, double[] a, double[] b, double[] p)
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

        public void restartPoblacion()
        {
            int t = soluciones.Count();
            soluciones.Clear();

            for (int i = 0; i < t; i++)
            {
                soluciones.Add(new Solucion());
            }
        }

        public int evaluarPoblacion(Solucion currentValues)
        {
            funcion = 7;
            //            
            for (int i = 0; i < soluciones.Count(); i++)
            {
                //E0 para cada solucion                

                E[2] = p[0] + p[0] *
                        (currentValues.variablesDecision[0]-
                         soluciones[i].variablesDecision[0]);

                /*
                //SIN CONTAR TEMP
                for (int j = 1; j < p.Length; j++)
                {
                    E[2] += p[j] * 
                        (soluciones[i].variablesDecision[j] - 
                        currentValues.variablesDecision[j]);
                }
                */

                //Console.WriteLine(E[2]);
                
                soluciones[i].evaluarSolucion(new FuncionesObjetivo(funcion, currentValues));

            }

            return soluciones.Count();
        }

        public int evaluarHijos(List<Solucion> hijos, Solucion currentValues)
        {
            funcion = 7;            

            for (int i = 0; i < hijos.Count(); i++)
            {
                //E0 para cada hijo
                
                E[2] = p[0] + p[0] *
                        (currentValues.variablesDecision[0] -
                         hijos[i].variablesDecision[0]);
                
                /*
                //SIN CONTAR TEMP
                for (int j = 1; j < p.Length; j++)
                {
                    E[2] += p[j] * (hijos[i].variablesDecision[j] - currentValues.variablesDecision[j]);
                }
                */

                hijos[i].evaluarSolucion(new FuncionesObjetivo(funcion, currentValues));
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

        public List<FrentePareto> Fast_NonDominated_Sort()
        { //Fast-NonDominated-Sort(P)
            List<FrentePareto> frentes = new List<FrentePareto>();
            frentes.Add(new FrentePareto());                                              

            int i, j, k;

            for (i = 0; i < soluciones.Count() - 1; i++)
            { //Por cada p en P             
                for (j = 0; j < soluciones.Count(); j++)
                { //por cada q en P
                    if (i != j)
                    {

                        if (Dominancia.EsDominante(soluciones[i], soluciones[j]))
                        {
                            soluciones[i].addSp(soluciones[j]);
                        }
                        else if (Dominancia.EsDominante(soluciones[j], soluciones[i]))
                        {
                            soluciones[i].esDominada(true); 
                        }
                    }
                }
                if (soluciones[i].getVecesDominada() == 0)
                {                    
                    frentes[0].agregarSolucion(soluciones[i]);
                    soluciones[i].setFrente(0);
                }
            }            
            
            i = 0;
            FrentePareto H;
            FrentePareto F;
            int pos;
            while (i < frentes.Count() && frentes[i].totalSoluciones() != 0)
            {
                H = new FrentePareto(); 
                F = frentes[i];
                for (j = 0; j < F.totalSoluciones(); j++)
                {                    
                    for (k = 0; k < F.getCantSolucionesDominadas(j); k++)
                    { 
                        pos = soluciones.IndexOf(F.getSolucionDominada(j, k));
                        soluciones[pos].esDominada(false); 
                                                           
                        if (soluciones[pos].getVecesDominada() == 0)
                        {
                            H.agregarSolucion(soluciones[pos]);
                            soluciones[pos].setFrente(i + 1); 
                        }
                    }
                }
                i++;
                if (H.totalSoluciones() > 0)
                {
                    frentes.Add(H);
                }

            }
            
            H = new FrentePareto();
            for (int l = 0; l < soluciones.Count(); l++)
            {
                if (soluciones[l].getFrente() == -1)
                {
                    H.agregarSolucion(soluciones[l]);
                }
            }

            if (H.totalSoluciones() != 0)
            {
                frentes.Add(H);
            }            

            return frentes;
        }

        public void recuperarMejoresSoluciones(List<FrentePareto> frentes, int solucionesRequeridas)
        {
            List<Solucion> mejoresSoluciones;
            mejoresSoluciones = new List<Solucion>();
            for (int i = 0; i < frentes.Count(); i++)
            {
                for (int j = 0; j < frentes[i].totalSoluciones(); j++)
                {

                    if (mejoresSoluciones.Count() < solucionesRequeridas)
                    {
                        mejoresSoluciones.Add(frentes[i].getSolucion(j));
                    }
                    else
                    {
                        break;
                    }
                }
            }

            soluciones = mejoresSoluciones;
        }

        public void fusionarPadresHijos(List<Solucion> hijos)
        {
            for (int i = 0; i < soluciones.Count(); i++)
            {
                soluciones[i].setDistancia(0);
                soluciones[i].setFrente(-1);
                soluciones[i].reiniciarSp();
                soluciones[i].reiniciarVecesDominada();
            }

            for (int i = 0; i < hijos.Count(); i++)
            {
                soluciones.Add(hijos[i]);
            }

        }

    }
}
