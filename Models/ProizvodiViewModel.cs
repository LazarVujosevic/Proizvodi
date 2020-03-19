using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proizvodi.Models
{
    public class ProizvodiViewModel
    {
        public List<ProizvodiBaseViewModel> jsonProizvodi = new List<ProizvodiBaseViewModel>();

        public List<ProizvodiBaseViewModel> dbProizvodi = new List<ProizvodiBaseViewModel>();
    }
}
