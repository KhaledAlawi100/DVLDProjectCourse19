using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_Data_Layer
{
    public class clsLDLAppData
    {

        public static int AddNewLDLApp(int applicationID, int licenseClassID)
        {
            int ldlID = -1;

            const string sql = @"
        INSERT INTO LocalDrivingLicenseApplications
            (ApplicationID, LicenseClassID)
        VALUES
            (@ApplicationID, @LicenseClassID);
        SELECT CAST(SCOPE_IDENTITY() AS int);";

            SqlConnection con = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, con);

            // Explicit types avoid AddWithValue surprises
            cmd.Parameters.Add("@ApplicationID", SqlDbType.Int).Value = applicationID;
            cmd.Parameters.Add("@LicenseClassID", SqlDbType.Int).Value = licenseClassID;

            try
            {
                con.Open();
                object result = cmd.ExecuteScalar();
                if (result != null)
                    ldlID = Convert.ToInt32(result);  // SCOPE_IDENTITY() comes back as decimal
            }
            catch (Exception ex)
            {
                // TODO: log or re‑throw; swallowing exceptions hides real problems
                throw;
            }
            finally
            {
                cmd.Dispose();
                con.Close();
                con.Dispose();
            }

            return ldlID;
        }

        public static bool DeleteLDLApp(ref string ErrorMessage,int localDrivingLicenseApplicationID)
        {
            bool isDeleted = false;

            const string sql = @"
DELETE FROM LocalDrivingLicenseApplications
WHERE LocalDrivingLicenseApplicationID = @LDLAppID;";

            using (SqlConnection con = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.Add("@LDLAppID", SqlDbType.Int).Value = localDrivingLicenseApplicationID;

                try
                {
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    isDeleted = (rowsAffected > 0);
                }
                catch (Exception ex)
                {
                    ErrorMessage = ex.Message;
                    
                }
            }

            return isDeleted;
        }




        public static bool GetLDLInfoByAppID(int ApplicationID, ref int LocalDrivingLicenseApplicationID,
            ref int LicenseClassID)
        {
            bool isFound = false;

            SqlConnection con = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM LocalDrivingLicenseApplications WHERE ApplicationID = @ApplicationID ";

            SqlCommand sqlCommand = new SqlCommand(query, con);

            sqlCommand.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                con.Open();

                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                    LicenseClassID = (int)reader["LicenseClassID"];

                }

            }
            catch (Exception ex) { 


            
            }
            finally
            {
                con.Close();
            }

            return isFound;

        }

        public static bool GetLDLInfoByID( int LocalDrivingLicenseApplicationID, ref int ApplicationID,
           ref int LicenseClassID)
        {
            bool isFound = false;

            SqlConnection con = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM LocalDrivingLicenseApplications WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID ";

            SqlCommand sqlCommand = new SqlCommand(query, con);

            sqlCommand.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            try
            {
                con.Open();

                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    ApplicationID = (int)reader["ApplicationID"];
                    LicenseClassID = (int)reader["LicenseClassID"];

                }

            }
            catch (Exception ex)
            {



            }
            finally
            {
                con.Close();
            }

            return isFound;

        }

        public static DataTable GetAllLDLApps()
        {
            DataTable dt = new DataTable();


            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM LocalDrivingLicenseApplications";

            SqlCommand sqlCommand = new SqlCommand( query, conn);

            try
            {
                conn.Open();

                SqlDataReader reader= sqlCommand.ExecuteReader();

                if (reader.HasRows) {

                    dt.Load(reader);
                
                }

            }
            catch (Exception ex) {

            }
            finally
            {
                conn.Close();
            }

            return dt;
        }



    }
}
