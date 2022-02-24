using ExecuteStoredProcedureDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ExecuteStoredProcedureDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DatabaseController : Controller
    {
        [HttpGet]
        public List<Cake> Get()
        {
            SqlConnection connection = new SqlConnection();

            connection.ConnectionString = "Data Source=DESKTOP-8DRN3BN;Initial Catalog=StatusCakeDemo;Integrated Security=True";
            connection.Open();

            string procedureName = "[dbo].[get_all_cakes]";
            var result = new List<Cake>();

            using (SqlCommand command = new SqlCommand(procedureName, connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string CakeType = reader[0].ToString();
                        int Cost = (int)reader[1];
                        string Description = reader[2].ToString();
                        int Weight = (int)reader[3];
                        int StoreAvalibility = (int)reader[4];

                        result.Add(new Cake(CakeType, Cost, Description, Weight, StoreAvalibility));

                    }
                }
            }
            return result;
        }

        //public Cake GetAllCakesFromDatabase()
        //{

        //}

    }
}
