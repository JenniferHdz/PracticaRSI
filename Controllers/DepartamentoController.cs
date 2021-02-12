using PracticaRSI_Web.Models;
using PracticaRSI_Web.Models.ModelsView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PracticaRSI_Web.Controllers
{
    public class DepartamentoController : Controller
    {
        public readonly PracticaRSIEntities database = new PracticaRSIEntities();

        // GET: Departamento
        public ActionResult Index()
        {
            List<DepartamentoView> DepartamentList;

            using (database)
            {
                DepartamentList = (from depto in database.Departamento
                       
                       orderby depto.DepartamentoID descending
                       select new DepartamentoView
                       {
                           DepartamentoID = depto.DepartamentoID,
                           Depto = depto.Depto,
                        }).ToList();
            }
            return View(DepartamentList);
        }

        public ActionResult DepartamentCreate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DepartamentCreate(DepartamentoView model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (database)
                    {
                        var depto = new Departamento();
                        depto.Depto = model.Depto;

                        database.Departamento.Add(depto);
                        database.SaveChanges();
                    }
                    return Redirect("~/Departamento");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ActionResult DepartamentUpdate(int id)
        {
            DepartamentoView model = new DepartamentoView();
            using (database)
            {
                var depto = database.Departamento.Find(id);
                model.DepartamentoID = depto.DepartamentoID;
                model.Depto = depto.Depto;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult DepartamentUpdate(DepartamentoView model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (database)
                    {
                        var depto = database.Departamento.Find(model.DepartamentoID);
                        depto.Depto = model.Depto;

                        database.Entry(depto).State = System.Data.Entity.EntityState.Modified;
                        database.SaveChanges();
                    }
                    return Redirect("~/Departamento");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}