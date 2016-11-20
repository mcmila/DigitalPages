using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DesafioMarvel.Infra
{
    public static class Paginacao
    {
        public static void ObterPaginacao(int pPaginaAtual, int pQtdPaginas, out int pPaginaInicial, out int pPaginaFinal)
        {
            int auxpagInicial = pPaginaAtual - 5;

            if (auxpagInicial <= 0)
            {
                pPaginaInicial = 1;
                auxpagInicial = -auxpagInicial;
                pPaginaFinal = pPaginaAtual + 5 + auxpagInicial;
            }
            else
            {
                pPaginaInicial = auxpagInicial + 1;
                pPaginaFinal = pPaginaAtual + 5;
            }

            if (pPaginaFinal > pQtdPaginas)
                pPaginaFinal = pQtdPaginas;
        }
    }
}