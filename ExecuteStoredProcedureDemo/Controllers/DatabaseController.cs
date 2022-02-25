using ExecuteStoredProcedureDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ExecuteStoredProcedureDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DatabaseController : ControllerBase
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

        [HttpPut]
        public void Put(string cakeType, int cost, string description, int weight, int storeAvalibility)
        {
            SqlConnection connection = new SqlConnection();

            connection.ConnectionString = "Data Source=DESKTOP-8DRN3BN;Initial Catalog=StatusCakeDemo;Integrated Security=True";
            connection.Open();

            string procedureName = "[dbo].[save_a_cake]";

            using (SqlCommand command = new SqlCommand(procedureName, connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@CakeType", cakeType));
                command.Parameters.Add(new SqlParameter("@Cost", cost));
                command.Parameters.Add(new SqlParameter("@Description", description));
                command.Parameters.Add(new SqlParameter("@Weight", weight));
                command.Parameters.Add(new SqlParameter("@StoreAvalibility", storeAvalibility));

                command.ExecuteNonQuery();
            }

        }

        [HttpDelete]
        public void Delete(string cakeType)
        {
            SqlConnection connection = new SqlConnection();

            connection.ConnectionString = "Data Source=DESKTOP-8DRN3BN;Initial Catalog=StatusCakeDemo;Integrated Security=True";
            connection.Open();

            string procedureName = "[dbo].[delete_a_cake]";

            using (SqlCommand command = new SqlCommand(procedureName, connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@CakeType", cakeType));
                command.ExecuteNonQuery();
            }
        }
    }
}
