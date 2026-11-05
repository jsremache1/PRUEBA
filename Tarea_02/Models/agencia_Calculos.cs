using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace Tarea_02.Models
{
    public partial class agencia
    {
        public string HoraInicioAmPm { get { return FormatearHora(this.horainicio); } }
        public string HoraFinAmPm { get { return FormatearHora(this.horafin); } }

        // Si la columna es de tipo "time" en SQL Server, EF la mapea a TimeSpan
        private static string FormatearHora(TimeSpan? hora)
        {
            if (hora == null) return "";
            return DateTime.Today.Add(hora.Value).ToString("hh:mm tt", CultureInfo.InvariantCulture);
        }

        // Si la columna es datetime, se usa esta sobrecarga
        private static string FormatearHora(DateTime? hora)
        {
            if (hora == null) return "";
            return hora.Value.ToString("hh:mm tt", CultureInfo.InvariantCulture);
        }
    }
}