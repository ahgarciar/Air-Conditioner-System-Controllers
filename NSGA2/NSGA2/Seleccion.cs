using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSGA2
{
    class Seleccion
    {
        private static Random RND = new Random();

        //Torneo Binario para Seleccionar a los padres
        public static List<Solucion> TorneoBinario(Poblacion poblacion, int totalPadres)
        {

            List<Solucion> padres;
            padres = new List<Solucion>();

            for (int i = 0; i < totalPadres; i++)
            {
                padres.Add(obtenerPadre(poblacion.getSoluciones()));
            }

            return padres;

        }

        private static Solucion obtenerPadre(List<Solucion> soluciones)
        {
            int totalSoluciones = soluciones.Count();
            int indice1 = RND.Next(totalSoluciones);
            int indice2 = calculaIndice2(totalSoluciones, indice1);

            Solucion P1 = soluciones[indice1];
            Solucion P2 = soluciones[indice2];

            if (P1.getFrente() != P2.getFrente())
            { 
                if (P1.getFrente() < P2.getFrente())
                { 
                    return P1;
                }
                else
                { 
                    return P2;
                }
            }
            else if (P1.getDistancia() != P1.getDistancia())
            {
                if (P1.getDistancia() > P2.getDistancia())
                {
                    return P1;
                }
                else
                {
                    return P2;
                }
            }
            else
            {
                return P1; 
            }

        }      

        private static int calculaIndice2(int totalSoluciones, int indicepadre1)
        {
            int indice;
            while ((indice = RND.Next(totalSoluciones)) == indicepadre1)
            {
            } 
            return indice;
        }



    }
}
