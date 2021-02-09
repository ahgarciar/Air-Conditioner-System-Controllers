
namespace GA
{
    static class ReparadorValorSolucion
    {
        public static double repararValor(double valor, double limInferior, double limSuperior)
        {
            if (valor > limSuperior){
                valor = limSuperior;
            }
            else if (valor < limInferior){
                valor = limInferior;
            }
            return valor;
        }


    }
}
