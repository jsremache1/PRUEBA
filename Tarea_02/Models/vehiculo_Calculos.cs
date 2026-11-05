using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Tarea_02.Models
{
    public partial class vehiculo
    {
        // Avalúo: 1000 * (YearActual - YearVehiculo) + IMPUESTO
        public decimal CalcularAvaluo()
        {
            if (this.anio == null)
                return 0m;

            int yearActual = DateTime.Now.Year;
            int yearVehiculo = this.anio.Value;

            string pais = this.marca1 != null ? this.marca1.pais : null;

            return (1000m * (yearActual - yearVehiculo)) + ObtenerImpuesto(pais);
        }

        // Impuesto según el país de origen de la marca
        private decimal ObtenerImpuesto(string pais)
        {
            string p = Normalizar(pais);

            if (p == "ECUADOR")
                return 0m;
            if (p.StartsWith("JAP"))                       // Japón / Japon / Jap?n
                return 500m;
            if (p == "USA" || p == "EEUU" || p == "ESTADOS UNIDOS")
                return 1000m;

            return 2000m; // Cualquier otro país
        }

        // Quita espacios, tildes y pasa a mayúsculas ("Japón " -> "JAPON")
        private static string Normalizar(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return string.Empty;

            string descompuesto = texto.Trim().ToUpperInvariant().Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();
            foreach (char c in descompuesto)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }
            return sb.ToString();
        }

        // Combustibles distintos que existen (para dibujar los checkbox)
        public static List<string> ObtenerCombustibles(MatriculacionFinalBD_Context db)
        {
            return db.vehiculo
                     .Where(v => v.combustible != null)
                     .Select(v => v.combustible)
                     .Distinct()
                     .OrderBy(c => c)
                     .ToList();
        }

        // Si no se marca ningún checkbox, devuelve todos
        public static List<vehiculo> Filtrar(MatriculacionFinalBD_Context db, string[] combustibles)
        {
            IQueryable<vehiculo> consulta = db.vehiculo.Include("color1").Include("marca1");

            if (combustibles != null && combustibles.Length > 0)
            {
                consulta = consulta.Where(v => combustibles.Contains(v.combustible));
            }

            return consulta.ToList();
        }
    }
}