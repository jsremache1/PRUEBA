using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace Tarea_02.Models
{
    public partial class matricula
    {
        // Formato: 23 - octubre - 2026
        public string FechaFormateada
        {
            get
            {
                // Si 'fecha' es DateTime normal (no nullable), usa this.fecha.ToString(...)
                if (this.fecha == null) return "";
                return this.fecha.Value.ToString("dd - MMMM - yyyy", new CultureInfo("es-EC"));
            }
        }
    }
}