using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Data_Layer
{
    public class clsInternationalLicensesData
    {

        public static bool GetInternaionalLicenseByLicenseID(int InternationalLicenseID, ref int ApplicationID, ref int DriverID,
     ref int IssuedUsingLocalLicenseID, ref DateTime IssueDate, ref DateTime ExpirationDate,
     ref bool IsActive, ref int CreatedByUserID)
        {
            bool result = false;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT ApplicationID, DriverID, IssuedUsingLocalLicenseID, " +
                           "IssueDate, ExpirationDate, IsActive, CreatedByUserID " +
                           "FROM InternationalLicenses WHERE InternationalLicenseID = @InternationalLicenseID";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    ApplicationID = Convert.ToInt32(reader["ApplicationID"]);
                    DriverID = Convert.ToInt32(reader["DriverID"]);
                    IssuedUsingLocalLicenseID = Convert.ToInt32(reader["IssuedUsingLocalLicenseID"]);
                    IssueDate = Convert.ToDateTime(reader["IssueDate"]);
                    ExpirationDate = Convert.ToDateTime(reader["ExpirationDate"]);
                    IsActive = Convert.ToBoolean(reader["IsActive"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);

                    result = true;
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                // Optional: Log or throw
            }
            finally
            {
                conn.Close();
            }

            return result;
        }


        public static int AddNewInterNationalLicense(int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID,
      DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {
            int InternationalLicenseID = -1;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "INSERT INTO InternationalLicenses " +
                "(ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID) " +
                "VALUES (@ApplicationID, @DriverID, @IssuedUsingLocalLicenseID, @IssueDate, @ExpirationDate, @IsActive, @CreatedByUserID); " +
                "SELECT SCOPE_IDENTITY();";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            cmd.Parameters.AddWithValue("@DriverID", DriverID);
            cmd.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            cmd.Parameters.AddWithValue("@IssueDate", IssueDate);
            cmd.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            cmd.Parameters.AddWithValue("@IsActive", IsActive);
            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                conn.Open();
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    InternationalLicenseID = Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                // Log or handle exception
            }
            finally
            {
                conn.Close();
            }

            return InternationalLicenseID;
        }

        public static bool UpdateInternationalLicense(int InternationalLicenseID, int ApplicationID, int DriverID,
    int IssuedUsingLocalLicenseID, DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {
            bool isUpdated = false;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "UPDATE InternationalLicenses SET " +
                           "ApplicationID = @ApplicationID, " +
                           "DriverID = @DriverID, " +
                           "IssuedUsingLocalLicenseID = @IssuedUsingLocalLicenseID, " +
                           "IssueDate = @IssueDate, " +
                           "ExpirationDate = @ExpirationDate, " +
                           "IsActive = @IsActive, " +
                           "CreatedByUserID = @CreatedByUserID " +
                           "WHERE InternationalLicenseID = @InternationalLicenseID";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);
            cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            cmd.Parameters.AddWithValue("@DriverID", DriverID);
            cmd.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            cmd.Parameters.AddWithValue("@IssueDate", IssueDate);
            cmd.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            cmd.Parameters.AddWithValue("@IsActive", IsActive);
            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                isUpdated = rowsAffected > 0;
            }
            catch (Exception ex)
            {
                // Log or handle error as needed
            }
            finally
            {
                conn.Close();
            }

            return isUpdated;
        }

        public static bool IsInternationalLicenseExistsByDriverID(int DriverID)
        {
            bool exists = false;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT 1 FROM InternationalLicenses WHERE DriverID = @DriverID";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                    exists = true;

                reader.Close();
            }
            catch (Exception ex)
            {
                // Optional: log exception or handle it
            }
            finally
            {
                conn.Close();
            }

            return exists;
        }


        public static DataTable GetAllInternationalLicenses()
        {
            DataTable table = new DataTable();

            SqlConnection con = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM InternationalLicenses";

            SqlCommand cmd = new SqlCommand(query, con);


            try
            {
                con.Open();

                SqlDataReader sqlDataReader = cmd.ExecuteReader();

                if (sqlDataReader.HasRows)
                {
                    table.Load(sqlDataReader);
                }
                
            }catch(Exception e)
            {

            }finally { 
                con.Close(); }

            return table;
        }

        public static DataTable GetAllInternationalLicensesByDriverID(int driverID)
        {
            DataTable table = new DataTable();

            using (SqlConnection con = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "SELECT * FROM InternationalLicenses WHERE DriverID = @DriverID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@DriverID", driverID);

                    try
                    {
                        con.Open();
                        using (SqlDataReader sqlDataReader = cmd.ExecuteReader())
                        {
                            if (sqlDataReader.HasRows)
                            {
                                table.Load(sqlDataReader);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error in GetAllInternationalLicensesByDriverID: " + e.Message);
                    }
                }
            }

            return table;
        }





    }
}
