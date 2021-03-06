﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proizvodi.Models
{
    public class ProizvodiBaseViewModel
    {
        public int Id { get; set; }

        public string Naziv { get; set; }

        public string Opis { get; set; }

        public string Kategorija { get; set; }

        public string Proizvodjac { get; set; }

        public string Dobavljac { get; set; }

        public double? Cena { get; set; }

        public string Type { get; set; }
    }
}
