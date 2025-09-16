using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Data_Layer
{
    public class CountryData
    {
        public static bool GetCountryInfoByID(int ID , ref string countryName)
        {
            bool isFound = false;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Countries WHERE CountryID=@CountryID";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@CountryID",ID);

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read()) { 

                    isFound = true;

                    countryName =(string) reader["CountryName"];

                    reader.Close();
                
                }
            }
            catch (Exception ex) {

            }
            finally
            {
                conn.Close();
            }


            return isFound;
        }

        public static DataTable GetAllCountries()
        {
            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Countries";

            SqlCommand command = new SqlCommand(query, conn);

            try
            {
                conn.Open();
                 SqlDataReader reader = command.ExecuteReader();
                if (reader.Read()) {

                    dt.Load(reader);
                
                }
                reader.Close();

            }
            catch (Exception ex) { 
            
            }
            finally
            {
                conn.Close();
            }

            return dt;
        }

        public static bool GetCountryInfoByName(string CountryName , ref int ID)
        {
            bool isFound = false;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Countries WHERE CountryName =@CountryName";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@CountryName", CountryName);

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read()) { 

                    isFound = true;

                    ID = (int)reader["CountryID"];
                
                }
            }
            catch (Exception ex)
            {

            }
            finally { 

                conn.Close();
            
            }

            return isFound;
        }
        

        
    }
}
