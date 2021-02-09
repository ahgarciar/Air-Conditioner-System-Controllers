using System;
using System.Collections.Generic;
using System.Linq;

namespace GA
{
    class Muta
    {            
        public static void mPolinomial(List<Solucion> hijos){
            Random rnd = new Random(10);

            double limInferior;
            double limSuperior;
            double probabilidadMuta; //   1.0/n                                     

            probabilidadMuta = 0.10;

            double diferenciaLimites; //limSuperior-limInferior;
            double eta = 20.0; //indice de distribución
            double u; //valor aleatorio
            double delta_q;

            double[] valores;

            double delta1;
            double delta2;
            double potencia;
            double xy;
            double val;

            for (int i = 0; i < hijos.Count(); i++){
                valores = hijos[i].VariablesDecision;

                for (int j = 0; j < valores.Length; j++){

                    limInferior = Poblacion.preferencesLimits[j].Min;
                    limSuperior = Poblacion.preferencesLimits[j].Max;

                    if (rnd.NextDouble() <= probabilidadMuta){ 

                        diferenciaLimites = limSuperior - limInferior;

                        delta1 = (valores[j] - limInferior) / diferenciaLimites;
                        delta2 = (limSuperior - valores[j]) / diferenciaLimites;

                        u = rnd.NextDouble(); //[0-1]

                        potencia = 1.0 / (eta + 1.0);

                        if (u <= 0.5){
                            xy = 1.0 - delta1;
                            val = 2.0 * u + (1.0 - 2.0 * u) * (Math.Pow(xy, eta + 1));
                            delta_q = Math.Pow(val, potencia) - 1;
                        }
                        else{
                            xy = 1.0 - delta2;
                            val = 2.0 * (1.0 - u) + 2.0 * (u - 0.5) * (Math.Pow(xy, eta + 1));
                            delta_q = 1.0 - Math.Pow(val, potencia);
                        }

                        valores[j] = valores[j] + diferenciaLimites * delta_q;
                        valores[j] = ReparadorValorSolucion.repararValor(valores[j], limInferior, limSuperior);
                    }                   
                }

            }
        }

    }



}
