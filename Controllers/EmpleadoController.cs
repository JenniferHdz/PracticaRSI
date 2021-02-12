using PracticaRSI_Web.AutoComplete;
using PracticaRSI_Web.Models;
using PracticaRSI_Web.Models.ModelsView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PracticaRSI_Web.Controllers
{
    public class EmpleadoController : Controller
    {
        public readonly PracticaRSIEntities database = new PracticaRSIEntities();
        public AutoCompleting complete = new AutoCompleting();

        // GET: Empleado
        public ActionResult Index()
        {
            List<EmpleadoView> empleadoList;
            using (database)
            {
                empleadoList = (from e in database.Empleado
                       join d in database.Departamento on e.DepartamentoID equals d.DepartamentoID
                       orderby e.EmpleadoID descending
                       select new EmpleadoView
                       {
                           EmpleadoID = e.EmpleadoID,
                           Nombre = e.Nombre,
                           Direccion = e.Direccion,
                           FechaNac = e.FechaNac,
                           FechaIngreso = e.FechaIngreso,
                           Departamento = d.Depto,
                       }).ToList();
            }
            return View(empleadoList);
        }

        public ActionResult EmpleadoCreate()
        {
            ViewBag.Departamento = complete.Departamento();
            return View();
        }

        [HttpPost]
        public ActionResult EmpleadoCreate(EmpleadoCreate model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (database)
                    {
                        var emp = new Empleado();
                        emp.Nombre = model.Nombre;
                        emp.Direccion = model.Direccion;
                        emp.FechaNac = model.FechaNac;
                        emp.FechaIngreso = DateTime.Now;
                        emp.DepartamentoID = model.DepartamentoID;

                        database.Empleado.Add(emp);
                        database.SaveChanges();
                    }
                    return Redirect("~/Empleado");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ActionResult EmpleadoUpdate(int id)
        {
            ViewBag.Departamento = complete.Departamento();

            EmpleadoCreate model = new EmpleadoCreate();
            using (database)
            {
                var emp = database.Empleado.Find(id);
                model.EmpleadoID = emp.EmpleadoID;
                model.Nombre = emp.Nombre;
                model.Direccion = emp.Direccion;
                model.FechaNac = emp.FechaNac;
                model.DepartamentoID = emp.DepartamentoID;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult EmpleadoUpdate(EmpleadoCreate model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (database)
                    {
                        var emp = database.Empleado.Find(model.EmpleadoID);
                        emp.Nombre = model.Nombre;
                        emp.Direccion = model.Direccion;
                        emp.FechaNac = model.FechaNac;
                        emp.DepartamentoID = model.DepartamentoID;

                        database.Entry(emp).State = System.Data.Entity.EntityState.Modified;
                        database.SaveChanges();
                    }
                    return Redirect("~/Empleado");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ActionResult EmpleadoDelete(int id)
        {
            try
            {
                using (database)
                {
                    return View(database.Empleado.Where(x => x.EmpleadoID == id).FirstOrDefault());
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult EmpleadoDelete(int id, FormCollection collection)
        {
            try
            {
                using (database)
                {
                    var Emp = database.Empleado.Find(id);
                    database.Empleado.Remove(Emp);
                    database.SaveChanges();
                }
                return Redirect("~/Empleado");
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}