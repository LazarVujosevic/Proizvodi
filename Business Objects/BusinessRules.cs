using Proizvodi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proizvodi.Business_Objects
{
    public static class BusinessRules
    {
        public static bool CanUpdate(this ProizvodiBaseViewModel model)
        {
            if (model.Id == 0)
            {
                return false;
            }

            return true;
        }
    }
}
