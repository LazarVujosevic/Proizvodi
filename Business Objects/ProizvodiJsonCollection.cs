using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Proizvodi.Business_Objects
{
    public class ProizvodiJsonCollection
    {
        private readonly string jsonLokacija = "../Proizvodi/Proizvodi.json";
        public ProizvodiJsonBO GetProizvoda()
        {
            string json = File.ReadAllText(jsonLokacija);
            ProizvodiJsonBO jsonObjekat = JsonConvert.DeserializeObject<ProizvodiJsonBO>(json);
            return jsonObjekat;
        }

        public void UpdateProizvoda(string output)
        {
            File.WriteAllText(jsonLokacija, output);
        }
    }
}
