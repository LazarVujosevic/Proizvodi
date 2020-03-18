using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Proizvodi.Business_Objects;
using Proizvodi.Models;

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
            var i = 0;
            if (proizvodiJson.proizvodi != null && proizvodiJson.proizvodi.Count() > 0)
            {
                foreach (var proizvod in proizvodiJson.proizvodi)
                {
                    var jsonModel = new JsonProizvodiBaseViewModel();
                    jsonModel.Id = proizvod.Id;
                    jsonModel.Naziv = proizvod.Naziv;
                    jsonModel.Opis = proizvod.Opis;
                    jsonModel.Kategorija = proizvod.Kategorija;
                    jsonModel.Proizvodjac = proizvod.Proizvodjac;
                    jsonModel.Dobavljac = proizvod.Dobavljac;
                    jsonModel.Cena = proizvod.Cena;
                    proizvodiViewModel.jsonProizvodi.proizvodi.Add(jsonModel);
                    i++;
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
    }
}
