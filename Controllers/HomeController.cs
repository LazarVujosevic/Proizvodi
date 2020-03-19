using System.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Proizvodi.Business_Objects;
using Proizvodi.Models;
using System.Text;

namespace Proizvodi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var proizvodiJsonCollection = new ProizvodiJsonCollection();
            var proizvodiJson = proizvodiJsonCollection.GetProizvoda();
            var proizvodiViewModel = new ProizvodiViewModel();
            if (proizvodiJson.proizvodi != null && proizvodiJson.proizvodi.Count() > 0)
            {
                var i = 0;
                foreach (var proizvod in proizvodiJson.proizvodi)
                {
                    var jsonModel = new ProizvodiBaseViewModel();
                    jsonModel.Id = proizvod.Id;
                    jsonModel.Naziv = proizvod.Naziv;
                    jsonModel.Opis = proizvod.Opis;
                    jsonModel.Kategorija = proizvod.Kategorija;
                    jsonModel.Proizvodjac = proizvod.Proizvodjac;
                    jsonModel.Dobavljac = proizvod.Dobavljac;
                    jsonModel.Cena = proizvod.Cena;
                    proizvodiViewModel.jsonProizvodi.Add(jsonModel);
                    i++;
                }
            }

            var proizvodDbCollection = new ProizvodiDBBOCollection();
            var proizvodiIzbaze = proizvodDbCollection.GetAll();
            if (proizvodiIzbaze != null && proizvodiIzbaze.Count > 0)
            {
                foreach (var proizvod in proizvodiIzbaze)
                {
                    var dbModel = new ProizvodiBaseViewModel();
                    dbModel.Id = proizvod.Id;
                    dbModel.Naziv = proizvod.Naziv;
                    dbModel.Opis = proizvod.Opis;
                    dbModel.Kategorija = proizvod.Kategorija;
                    dbModel.Proizvodjac = proizvod.Proizvodjac;
                    dbModel.Dobavljac = proizvod.Dobavljac;
                    dbModel.Cena = proizvod.Cena;
                    proizvodiViewModel.dbProizvodi.Add(dbModel);
                }
            }

            return View(proizvodiViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
       
        [HttpPost]
        public IActionResult Edit(int id, string type)
        {
            var proizvod = new ProizvodiBaseViewModel();
            if (type == WellKnownValues.TypeWellKnownValues.json)
            {
                var proizvodiJsonCollection = new ProizvodiJsonCollection();
                var proizvodiList = proizvodiJsonCollection.GetProizvoda();
                var jsonProizvod = proizvodiList.proizvodi.Where(a => a.Id == id).FirstOrDefault();

                proizvod.Id = jsonProizvod.Id;
                proizvod.Naziv = jsonProizvod.Naziv;
                proizvod.Opis = jsonProizvod.Opis;
                proizvod.Kategorija = jsonProizvod.Kategorija;
                proizvod.Proizvodjac = jsonProizvod.Proizvodjac;
                proizvod.Dobavljac = jsonProizvod.Dobavljac;
                proizvod.Cena = jsonProizvod.Cena;
                proizvod.Type = WellKnownValues.TypeWellKnownValues.json;
            }
            else if (type == WellKnownValues.TypeWellKnownValues.dataBase)
            {
                var proizvodDbCollection = new ProizvodiDBBOCollection();
                var proizvodDb = proizvodDbCollection.GetById(id);

                proizvod.Id = proizvodDb.Id;
                proizvod.Naziv = proizvodDb.Naziv;
                proizvod.Opis = proizvodDb.Opis;
                proizvod.Kategorija = proizvodDb.Kategorija;
                proizvod.Proizvodjac = proizvodDb.Proizvodjac;
                proizvod.Dobavljac = proizvodDb.Dobavljac;
                proizvod.Cena = proizvodDb.Cena;
                proizvod.Type = WellKnownValues.TypeWellKnownValues.dataBase;
            }
            else
            {
                this.RedirectToAction("Error");
            }

            return this.View("Edit", proizvod);
        }

        public IActionResult SaveJson(ProizvodiBaseViewModel model)
        {
            var proizvodiJsonCollection = new ProizvodiJsonCollection();
            var proizvodiJson = proizvodiJsonCollection.GetProizvoda();
            proizvodiJson.proizvodi.Where(a => a.Id == model.Id).FirstOrDefault()
                 .Naziv = model.Naziv;
            proizvodiJson.proizvodi.Where(a => a.Id == model.Id).FirstOrDefault()
                 .Opis = model.Opis;
            proizvodiJson.proizvodi.Where(a => a.Id == model.Id).FirstOrDefault()
                 .Kategorija = model.Kategorija;
            proizvodiJson.proizvodi.Where(a => a.Id == model.Id).FirstOrDefault()
                 .Proizvodjac = model.Proizvodjac;
            proizvodiJson.proizvodi.Where(a => a.Id == model.Id).FirstOrDefault()
                 .Dobavljac = model.Dobavljac;
            proizvodiJson.proizvodi.Where(a => a.Id == model.Id).FirstOrDefault()
                 .Cena = model.Cena;
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(proizvodiJson, Newtonsoft.Json.Formatting.Indented);
            proizvodiJsonCollection.UpdateProizvoda(output);

            return this.RedirectToAction("Index");
        }

        public IActionResult SaveDb(ProizvodiBaseViewModel model)
        {
            var proizvodDbCollection = new ProizvodiDBBOCollection();
            proizvodDbCollection.Update(model);
            return this.RedirectToAction("Index");
        }
    }
}
