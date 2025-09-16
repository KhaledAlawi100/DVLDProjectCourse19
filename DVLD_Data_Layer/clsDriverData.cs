using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Data_Layer
{
    public class clsDriverData
    {

        public static DataTable GetAllDrivers()
        {
            DataTable table = new DataTable();

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Drivers";

            SqlCommand cmd = new SqlCommand(query, conn);

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows) { 

                    table.Load(reader);
                
                
                }

            }
            catch (Exception ex) {

            }
            finally
            {
                conn.Close();
            }

            return table;
        }

        public static int AddNewDriver(int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            int driverID = -1;

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "INSERT INTO Drivers (PersonID, CreatedByUserID, CreatedDate) " +
                               "VALUES (@PersonID, @CreatedByUserID, @CreatedDate); " +
                               "SELECT SCOPE_IDENTITY();";

                SqlCommand command = new SqlCommand(query, conn);

                command.Parameters.AddWithValue("@PersonID", PersonID);
                command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                command.Parameters.AddWithValue("@CreatedDate", CreatedDate);

                try
                {
                    conn.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int newID))
                    {
                        driverID = newID;
                    }
                }
                catch (Exception ex)
                {
                    
                }
                finally
                {
                    conn.Close();
                }
            }

            return driverID;
        }


        public static bool GetDriverInfoByPersonID(int PersonID, ref int DriverID, ref int CreatedByUserID, ref DateTime CreatedDate)
        {
            bool isFound = false;

            string query = "SELECT DriverID, CreatedByUserID, CreatedDate FROM Drivers WHERE PersonID = @PersonID";

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@PersonID", PersonID);

                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) // <-- You must call Read()
                        {
                            isFound = true;
                            DriverID = reader.GetInt32(reader.GetOrdinal("DriverID"));
                            CreatedByUserID = reader.GetInt32(reader.GetOrdinal("CreatedByUserID"));
                            CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate"));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in GetDriverInfoByPersonID: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }

            return isFound;
        }

        public static bool GetDriverInfoByDriverID(int DriverID, ref int PersonID, ref int CreatedByUserID, ref DateTime CreatedDate)
        {
            bool isFound = false;

            string query = "SELECT PersonID, CreatedByUserID, CreatedDate FROM Drivers WHERE DriverID = @DriverID";

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@DriverID", DriverID);

                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            PersonID = reader.GetInt32(reader.GetOrdinal("PersonID"));
                            CreatedByUserID = reader.GetInt32(reader.GetOrdinal("CreatedByUserID"));
                            CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate"));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in GetDriverInfoByDriverID: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }

            return isFound;
        }


    }
}
