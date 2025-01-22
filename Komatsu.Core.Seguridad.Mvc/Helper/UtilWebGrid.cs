using System;
using Komatsu.Core.Seguridad.Common;

namespace Komatsu.Core.Seguridad.Mvc.Helpers
{
    public class UtilWebGrid
    {
        public static string ContadorRegistrosWebGrid(int pageIndex, int pageSize, int rowCount)
        {
            string strContador;
            int registroInicio, registroFin;
            int pageCount;
            if (pageIndex > 0) pageIndex--;
            if (pageIndex == -1) pageIndex = 0;
            decimal temp = Convert.ToDecimal(rowCount) / Convert.ToDecimal(pageSize);
            if (temp > Convert.ToInt32(rowCount / pageSize))
            {
                pageCount = Convert.ToInt32(rowCount / pageSize) + 1;
            }
            else
            {
                pageCount = Convert.ToInt32(rowCount / pageSize);
            }

            registroInicio = (pageIndex * pageSize) + 1;

            if (rowCount <= 0)
            {
                strContador = "";
            }
            else if (rowCount == 1)
            {
                strContador = " Un registro encontrado.";
            }
            else
            {
                registroInicio = (pageIndex * pageSize) + 1;

                if (pageIndex + 1 < pageCount)
                    registroFin = (pageIndex + 1) * pageSize;
                else
                    registroFin = rowCount;

                strContador = " [" + rowCount.ToString() + "]" + "Registros encontrados, mostrando del" + " [" + registroInicio.ToString() + "] al [" + registroFin.ToString() + "]";
            }
            return strContador;
        }


    }
}