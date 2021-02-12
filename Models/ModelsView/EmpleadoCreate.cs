using System;
using System.ComponentModel.DataAnnotations;

namespace PracticaRSI_Web.Models.ModelsView
{
    public class EmpleadoCreate
    {
        public int EmpleadoID { get; set; }
        public string Nombre { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaNac { get; set; }
        public string Direccion { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaIngreso { get; set; }
        public int DepartamentoID { get; set; }
    }
}