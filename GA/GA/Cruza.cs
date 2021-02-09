using System;
using System.Collections.Generic;
using System.Linq;

namespace GA
{
    class Cruza
    {        
        public static List<Solucion> SBX(List<Solucion> padres){

            Random rnd = new Random(10);
            
            double EPS = 1.0e-14; //EPS defines the minimum difference allowed between real values

            List<Solucion> hijos = new List<Solucion>();
            double[] varDecisionPadre1;
            double[] varDecisionPadre2;

            double[] varDecisionHijo1;
            double[] varDecisionHijo2;

            int totalVariables;

            double x_1, x_2, beta_1, beta_2, alpha_1, alpha_2, eta = 20.0, u, beta_q_1, beta_q_2;
            double limInferior, limSuperior;

            for (int i = 0; i < padres.Count() - 1; i += 2)
            { //Por cada conjunto de padres

                varDecisionPadre1 = padres[i].VariablesDecision;
                varDecisionPadre2 = padres[i + 1].VariablesDecision;

                totalVariables = varDecisionPadre1.Length;

                varDecisionHijo1 = new double[totalVariables];
                varDecisionHijo2 = new double[totalVariables];

                for (int j = 0; j < totalVariables; j++){

                    limInferior = Poblacion.preferencesLimits[j].Min;
                    limSuperior = Poblacion.preferencesLimits[j].Max;
                    
                        if (Math.Abs(varDecisionPadre1[j] - varDecisionPadre2[j]) > EPS){

                            if (varDecisionPadre1[j] > varDecisionPadre2[j]){
                                x_1 = varDecisionPadre1[j]; //Max
                                x_2 = varDecisionPadre2[j]; //Min
                            }
                            else{
                                x_1 = varDecisionPadre2[j]; //Max
                                x_2 = varDecisionPadre1[j]; //Min
                            }

                            beta_1 = 1.0 + (2.0 * (x_2 - limInferior) / (x_1 - x_2));
                            beta_2 = 1.0 + (2.0 * (limSuperior - x_1) / (x_1 - x_2));

                            alpha_1 = 2.0 - Math.Pow(beta_1, -1.0 * (eta + 1.0));
                            alpha_2 = 2.0 - Math.Pow(beta_2, -1.0 * (eta + 1.0));

                            u = rnd.NextDouble(); //[0-1]

                            if (u <= (1.0 / alpha_1)){
                                beta_q_1 = Math.Pow(u * alpha_1, 1.0 / (eta + 1.0));
                            }
                            else{
                                beta_q_1 = Math.Pow(1.0 / (2.0 - u * alpha_1), 1.0 / (eta + 1.0));
                            }

                            if (u <= (1.0 / alpha_2)){
                                beta_q_2 = Math.Pow(u * alpha_2, 1.0 / (eta + 1.0));
                            }
                            else{
                                beta_q_2 = Math.Pow(1.0 / (2.0 - u * alpha_2), 1.0 / (eta + 1.0));
                            }

                            varDecisionHijo1[j] = ReparadorValorSolucion.repararValor(0.5 * (x_1 + x_2 - beta_q_1 * (x_1 - x_2)), limInferior, limSuperior);
                            varDecisionHijo2[j] = ReparadorValorSolucion.repararValor(0.5 * (x_1 + x_2 + beta_q_2 * (x_1 - x_2)), limInferior, limSuperior);

                        }
                        else{ //Los genes son heredados sin variacion
                            varDecisionHijo1[j] = ReparadorValorSolucion.repararValor(varDecisionPadre1[j], limInferior, limSuperior);
                            varDecisionHijo2[j] = ReparadorValorSolucion.repararValor(varDecisionPadre2[j], limInferior, limSuperior);
                        }                                
                }

                hijos.Add(new Solucion(varDecisionHijo1));
                hijos.Add(new Solucion(varDecisionHijo2));
            }

            return hijos;
        }              

    }
}
