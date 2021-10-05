using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ProjectMVC.Context;

namespace ProjectMVC.Models
{
    public class SpecialityADO
    {
        private string connectionstring = @"Data Source=HP;Initial Catalog=HMS;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";

        // performs all operations on the database . get all, create, delete, get one , search etc.

    public List<Specialty> FetchAll_Speciality(int HID)
        {
            List<Specialty> specialties = new List<Specialty>();

            //access the database
            using(SqlConnection connection = new SqlConnection(connectionstring))
            {
                string SQLQuery = "select distinct Specialty.ID, Specialty.Name from Doctor , Specialty where Doctor.HospitalID =" + HID;
                SqlCommand command = new SqlCommand( SQLQuery, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Specialty specialty = new Specialty()
                        {
                            ID = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        };
                        specialties.Add(specialty);
                    }
                }
                connection.Close();
                return specialties;
            }
        }


        
     }
}