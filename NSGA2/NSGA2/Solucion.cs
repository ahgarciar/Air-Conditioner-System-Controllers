using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSGA2
{
    public class Solucion : IEquatable<Solucion>
    {
        static Random rnd = new Random();

        public double[] variablesDecision { get; set; } //definido por la cantidad de variables que constituyen a cada objetivo

        public double[] valoresObjetivo { get; set; } //solución - Aplicación de la funcion objetivo por cada objetivo;    

        private int vecesDominada { get; set; } //cantidad de veces que la valoresObjetivo ha sido dominada    
        private List<Solucion> Sp { get; set; } //soluciones dominadas   
        private double distancia { get; set; }
        private int frente { get; set; }
        private int tipo { get; set; }           

        public Solucion()
        {
            tipo = 7;  //Para saber que funcion objtivo utilizar

            int cantVariables = 3;

            variablesDecision = new double[cantVariables];

            for (int i = 0; i < cantVariables; i++)
            {
                //VALORES ALEATORIOS PARA LAS DISTINTAS VARIABLES DE DECISIÓN [T L A]
                variablesDecision[i] = rnd.NextDouble() * (Poblacion.preferencesLimits[i].Max - Poblacion.preferencesLimits[i].Min) + Poblacion.preferencesLimits[i].Min;
            }

            vecesDominada = 0; //Comienza sin ser dominada                 
            Sp = new List<Solucion>(); //Comienza sin dominar a alguna solución            
            distancia = 0; //su distancia crowling comienza en 0            
            frente = -1; //En un inicio la solución no dispone de ningún frente
        }

        public Solucion(double[] varDecision)
        {
            tipo = 7;  //Para saber que funcion objtivo utilizar
           
            variablesDecision = varDecision;
            vecesDominada = 0;
            Sp = new List<Solucion>();
            distancia = 0;
            frente = -1;
        }     

        public double[] getVariablesDecision()
        {
            return variablesDecision;
        }

        public void evaluarSolucion(FuncionesObjetivo tipo)
        {
            valoresObjetivo = tipo.getValoresFunciones(variablesDecision);
        }

        public double[] getValoresObjetivo()
        {
            return valoresObjetivo;
        }

        public int getCantObjetivos()
        {
            return valoresObjetivo.Length;
        }

        public void reiniciarVecesDominada()
        {
            vecesDominada = 0;
        }

        public int getVecesDominada()
        {
            return vecesDominada;
        }

        public void esDominada(bool valor)
        {
            vecesDominada = valor ? vecesDominada + 1 : vecesDominada - 1;
        }

        public void addSp(Solucion p)
        {
            Sp.Add(p);
        }

        public Solucion getSolucionSp(int i)
        {
            return Sp[i];
        }

        public void reiniciarSp()
        {
            Sp = new List<Solucion>();
        }

        public int getTotalSolucionesSp()
        {
            return Sp.Count;
        }


        public void setFrente(int i)
        {
            this.frente = i;
        }

        public int getFrente()
        {
            return this.frente;
        }

        public void setDistancia(double i)
        {
            this.distancia = i;
        }

        public double getDistancia()
        {
            return this.distancia;
        }      

        public override bool Equals(object obj)
        {
            return Equals(obj as Solucion);
        }

        public bool Equals(Solucion other)
        {
            return other != null &&
                   EqualityComparer<double[]>.Default.Equals(valoresObjetivo, other.valoresObjetivo);
        }

        public override int GetHashCode()
        {
            return -1915131987 + EqualityComparer<double[]>.Default.GetHashCode(valoresObjetivo);
        }

    }
}
