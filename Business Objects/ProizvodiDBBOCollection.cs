using Microsoft.Data.SqlClient;
using Proizvodi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Proizvodi.Business_Objects
{
    public class ProizvodiDBBOCollection 
    {
        private const string connString = "Data Source=sql5052.site4now.net;Initial Catalog=DB_A5619B_Proizvodi;User ID=DB_A5619B_Proizvodi_admin;Password=proizvodi123";

        #region Get All
        public List<ProizvodiBase> GetAll()
        {
            var proizvodList = new List<ProizvodiBase>();
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
                        var proizvod = new ProizvodiBase();
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
                conn.Close();
            }
            return proizvodList;
        }
        #endregion

        #region Get by id
        public ProizvodiBase GetById(int? id)
        {
            var proizvod = new ProizvodiBase();
            if (id != null)
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    string query = $"SELECT * FROM Proizvodi WHERE id = '{id}'";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            proizvod.Id = dr.GetInt32(0);
                            proizvod.Naziv = dr.GetString(1);
                            proizvod.Opis = dr.GetString(2);
                            proizvod.Kategorija = dr.GetString(3);
                            proizvod.Proizvodjac = dr.GetString(4);
                            proizvod.Dobavljac = dr.GetString(5);
                            proizvod.Cena = dr.GetDouble(6);
                        }
                    }
                    conn.Close();
                }
            }

            return proizvod;
        }


        #endregion

        #region Update

        public void Update(ProizvodiBaseViewModel model)
        {
            if (model != null)
            {
                string query = $"UPDATE Proizvodi SET Naziv = '{model.Naziv}', " +
                        $"Opis = '{model.Opis}', kategorija = '{model.Kategorija}', " +
                        $"Proizvodjac = '{model.Proizvodjac}', Dobavljac = '{model.Dobavljac}', " +
                        $"Cena = '{model.Cena}' WHERE Id = {model.Id}";
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
            }
        }

        #endregion
    }

}
