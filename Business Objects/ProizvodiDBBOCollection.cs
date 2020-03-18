using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proizvodi.Business_Objects
{
    public class ProizvodiDBBOCollection
    {
        private const string connString = "Data Source=sql5052.site4now.net;Initial Catalog=DB_A5619B_Proizvodi;User ID=DB_A5619B_Proizvodi_admin;Password=proizvodi123";

        #region Get All
        public List<ProizvodDbBO> GetAll()
        {

            var proizvodList = new List<ProizvodDbBO>();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "SELECT * FROM Proizvodi";

                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var proizvod = new ProizvodDbBO();
                        proizvod.Id = dr.GetInt32(0);
                        proizvod.Naziv = dr.GetString(1);
                        proizvod.Opis = dr.GetString(2);
                        proizvod.Kategorija = dr.GetString(3);
                        proizvod.Proizvodjac = dr.GetString(4);
                        proizvod.Dobavljac = dr.GetString(5);
                        proizvod.Cena = dr.GetDouble(6);
                        proizvodList.Add(proizvod);
                    }
                }
            }
            return proizvodList;
        }
        #endregion

        #region Get by id

        

        #endregion
    }

    public class ProizvodDbBO : ProizvodiJsonBaseClass
    {
    }
}
