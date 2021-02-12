using PracticaRSI_Web.Models;
using PracticaRSI_Web.Models.ModelsView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PracticaRSI_Web.AutoComplete
{
    public class AutoCompleting
    {
        private readonly PracticaRSIEntities database = new PracticaRSIEntities();

        public List<DropDown> Departamento()
        {
            return (from D in database.Departamento select new DropDown { Id = D.DepartamentoID, Value = D.Depto }).ToList();
        }

    }
}