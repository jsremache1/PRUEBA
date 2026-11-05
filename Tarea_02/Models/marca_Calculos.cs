using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Tarea_02.Models
{
    public partial class marca
    {
        public static List<string> ObtenerPaises(MatriculacionFinalBD_Context db)
        {
            return db.marca
                     .Where(m => m.pais != null)
                     .Select(m => m.pais)
                     .Distinct()
                     .OrderBy(p => p)
                     .ToList();
        }

        // Si pais viene vacío (opción "Todos"), devuelve todas las marcas
        public static List<marca> FiltrarPorPais(MatriculacionFinalBD_Context db, string pais)
        {
            IQueryable<marca> consulta = db.marca;

            if (!string.IsNullOrEmpty(pais))
            {
                consulta = consulta.Where(m => m.pais == pais);
            }

            return consulta.OrderBy(m => m.descripcion).ToList();
        }
    }
}