using System;
using System.Collections.Generic;
using System.Linq;

namespace GA
{
    class Seleccion
    {
    
        private static readonly Random RND = new Random(10);

        //Torneo Binario para Seleccionar a los padres
        public static List<Solucion> TorneoBinario(Poblacion poblacion, int totalPadres)
        {
            List<Solucion> padres;
            padres = new List<Solucion>();

            for (int i = 0; i < totalPadres; i++)
            {
                padres.Add(getPadre(poblacion.getSoluciones()));
            }

            return padres;
        }

        private static Solucion getPadre(List<Solucion> soluciones)
        {
            int totalSoluciones = soluciones.Count();
            int indice1 = RND.Next(totalSoluciones);
            int indice2;

            while ((indice2 = RND.Next(totalSoluciones)) == indice1) {  }            

            Solucion P1 = soluciones[indice1];
            Solucion P2 = soluciones[indice2];

            if (P1.ValorObjetivo > P2.ValorObjetivo){
                return P1;              
            }            
            else{
                return P2; 
            }
        }
        





    }
}
