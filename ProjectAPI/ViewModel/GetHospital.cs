using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProjectAPI.ViewModel
{
    public class GetHospital
    {

        private string connectionstring = @"Data Source=HP;Initial Catalog=HMS;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";

        // performs all operations on the database . get all, create, delete, get one , search etc.

        public List<Hospitals> FetchAll_hospitals()
        {
            List<Hospitals> hospitals = new List<Hospitals>();

            //access the database
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                string SQLQuery = "select * from Hospital";
                //string SQLQuery2 = "SELECT count(*), Hospital.ID from Reservation , Hospital where Reservation.HospitalID = Hospital.ID and Reservation.AdmitOrDecline = 1 group by (Hospital.ID)";
                SqlCommand Hospitals = new SqlCommand(SQLQuery, connection);
                //SqlCommand Count = new SqlCommand(SQLQuery2, connection);
                connection.Open();
                SqlDataReader reader_Hospital = Hospitals.ExecuteReader();
                //SqlDataReader reader_Count = Hospitals.ExecuteReader();
               // int count = 0;
                if (reader_Hospital.HasRows)
                {
                    while (reader_Hospital.Read())
                    {
                        Hospitals hospital = new Hospitals()
                        {
                            ID = reader_Hospital.GetInt32(0),
                            Name = reader_Hospital.GetString(1),
                            Address = reader_Hospital.GetString(2),
                            Phone = reader_Hospital.GetString(3),
                            HospitalLongitude = reader_Hospital.GetString(4),
                            HospitalLatitude = reader_Hospital.GetString(6),
                            NumOfAllBed = reader_Hospital.GetInt32(7)

                        };
                        //if (reader_Count.HasRows)
                        //{
                        //    while (reader_Count.Read())
                        //    {
                        //        if (reader_Count.GetInt32(1) == reader_Hospital.GetInt32(0))
                        //        {
                        //            count = reader_Count.GetInt32(2);
                        //            break;
                        //        }
                        //    }
                        //}
                        //hospital.NumOfAvailableBed = count;
                        //count = 0;
                        hospitals.Add(hospital);
                    }
                }
                connection.Close();
            }

            using (SqlConnection connection1 = new SqlConnection(connectionstring))
                {
                    string SQLQuery2 = "SELECT count(*), Hospital.ID from Reservation , Hospital where Reservation.HospitalID = Hospital.ID and Reservation.AdmitOrDecline = 1 group by (Hospital.ID)";
                //SqlCommand Hospital = new SqlCommand(SQLQuery, connection);
                SqlCommand Count = new SqlCommand(SQLQuery2, connection1);
                    connection1.Open();
                    SqlDataReader reader_Count = Count.ExecuteReader();
                
                    if (reader_Count.HasRows)
                    {
                        while (reader_Count.Read())
                        {
                        for(int i=0;i< hospitals.Count(); i++)
                        {
                            if (reader_Count.GetInt32(1) == hospitals[i].ID)
                            {
                                hospitals[i].NumOfAvailableBed = reader_Count.GetInt32(0);
                                
                                break;
                            }
                        }
                        }
                    }

                }
                return hospitals;
            
          
        }

    }
}


