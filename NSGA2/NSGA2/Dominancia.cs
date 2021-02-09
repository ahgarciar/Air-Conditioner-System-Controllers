namespace NSGA2
{
    class Dominancia
    {        
        public static bool EsDominante(Solucion S1, Solucion S2)
        {
            return EsDominante(S1.getValoresObjetivo(), S2.getValoresObjetivo());
        }
        
        public static bool EsDominante(double[] a, double[] b)
        {
            int banderaiguales = 1;

            for (int x = 0; x < a.Length; x++)
            {
                if (a[x] != b[x])
                {
                    banderaiguales = 0;
                }
            }

            if (banderaiguales == 1)
            {
                return true;
            }

            for (int x = 0; x < a.Length; x++)
            {
                if (a[x] > b[x])
                {
                    return false;
                }
            }

            return true;
        }

    }
}
