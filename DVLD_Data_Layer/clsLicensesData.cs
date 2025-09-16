using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Data_Layer
{
    public class clsLicensesData
    {

        public static int AddNewLicense(ref string Message,
    int ApplicationID,
    int DriverID,
    int LicenseClass,
    DateTime IssueDate,
    DateTime ExpirationDate,
    string Notes,
    float PaidFees,
    bool IsActive,
    byte IssueReason,
    int CreatedByUserID)
        {
            int LicenseID = -1;

            string query = @"
INSERT INTO Licenses (
    ApplicationID,
    DriverID,
    LicenseClass,
    IssueDate,
    ExpirationDate,
    Notes,
    PaidFees,
    IsActive,
    IssueReason,
    CreatedByUserID)
VALUES (
    @ApplicationID,
    @DriverID,
    @LicenseClass,
    @IssueDate,
    @ExpirationDate,
    @Notes,
    @PaidFees,
    @IsActive,
    @IssueReason,
    @CreatedByUserID);
SELECT SCOPE_IDENTITY();";

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                // Add parameters
                command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                command.Parameters.AddWithValue("@DriverID", DriverID);
                command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
                command.Parameters.AddWithValue("@IssueDate", IssueDate);
                command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
                command.Parameters.AddWithValue("@Notes", string.IsNullOrEmpty(Notes) ? (object)DBNull.Value : Notes);
                command.Parameters.AddWithValue("@PaidFees", PaidFees);
                command.Parameters.AddWithValue("@IsActive", IsActive);
                command.Parameters.AddWithValue("@IssueReason", IssueReason);
                command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

                try
                {
                    conn.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int newID))
                    {
                        LicenseID = newID;
                    }
                }
                catch (Exception ex)
                {
                    // Optional: Replace with your logging mechanism
                    Message = ex.Message;
                }
            }

            return LicenseID;
        }


        public static bool UpdateLicense(ref string Message, int LicenseID, int ApplicationID, int DriverID, int LicenseClass, DateTime IssueDate,
    DateTime ExpirationDate, string Notes, float PaidFees, bool IsActive, byte IssueReason, int CreatedByUserID)
        {
            bool isUpdated = false;

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"UPDATE Licenses 
                         SET ApplicationID = @ApplicationID,
                             DriverID = @DriverID,
                             LicenseClass = @LicenseClass,
                             IssueDate = @IssueDate,
                             ExpirationDate = @ExpirationDate,
                             Notes = @Notes,
                             PaidFees = @PaidFees,
                             IsActive = @IsActive,
                             IssueReason = @IssueReason,
                             CreatedByUserID = @CreatedByUserID
                         WHERE LicenseID = @LicenseID";

                SqlCommand command = new SqlCommand(query, conn);

                command.Parameters.AddWithValue("@LicenseID", LicenseID);
                command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                command.Parameters.AddWithValue("@DriverID", DriverID);
                command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
                command.Parameters.AddWithValue("@IssueDate", IssueDate);
                command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
                command.Parameters.AddWithValue("@PaidFees", PaidFees);
                command.Parameters.AddWithValue("@IsActive", IsActive);
                command.Parameters.AddWithValue("@IssueReason", IssueReason);
                command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

                // Handle nullable Notes
                if (string.IsNullOrEmpty(Notes))
                    command.Parameters.AddWithValue("@Notes", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@Notes", Notes);

                try
                {
                    conn.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    isUpdated = rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Message = ex.Message;
                    

                }
                finally
                {
                    conn.Close();
                }
            }

            return isUpdated;
        }

        public static bool GetLicenseByApplicationID( int ApplicationID, ref int LicenseID,  ref int DriverID,
     ref int LicenseClass, ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes,
     ref float PaidFees, ref bool IsActive, ref byte IssueReason, ref int CreatedByUserID)
        {
            bool IsFound = false;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Licenses WHERE ApplicationID = @ApplicationID";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;

                    LicenseID = Convert.ToInt32(reader["LicenseID"]);
                    DriverID = Convert.ToInt32(reader["DriverID"]);
                    LicenseClass = Convert.ToInt32(reader["LicenseClass"]);
                    IssueDate = Convert.ToDateTime(reader["IssueDate"]);
                    ExpirationDate = Convert.ToDateTime(reader["ExpirationDate"]);

                    // Nullable Notes
                    if (reader["Notes"] != DBNull.Value)
                        Notes = reader["Notes"].ToString();
                    else
                        Notes ="";

                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    IsActive = Convert.ToBoolean(reader["IsActive"]);
                    IssueReason = Convert.ToByte(reader["IssueReason"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                conn.Close();
            }

            return IsFound;
        }

        public static bool GetLicenseByLicenseID(int licenseID, ref int ApplicationID, ref int DriverID,
    ref int LicenseClass, ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes,
    ref float PaidFees, ref bool IsActive, ref byte IssueReason, ref int CreatedByUserID)
        {
            bool IsFound = false;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Licenses WHERE LicenseID = @LicenseID";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@LicenseID", licenseID);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;

                    ApplicationID = Convert.ToInt32(reader["ApplicationID"]);
                    DriverID = Convert.ToInt32(reader["DriverID"]);
                    LicenseClass = Convert.ToInt32(reader["LicenseClass"]);
                    IssueDate = Convert.ToDateTime(reader["IssueDate"]);
                    ExpirationDate = Convert.ToDateTime(reader["ExpirationDate"]);

                    Notes = reader["Notes"] != DBNull.Value ? reader["Notes"].ToString() : "";
                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    IsActive = Convert.ToBoolean(reader["IsActive"]);
                    IssueReason = Convert.ToByte(reader["IssueReason"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                // Log or handle exception as needed
            }
            finally
            {
                conn.Close();
            }

            return IsFound;
        }


        public static DataTable GetAllLicensesByDriverID(int DriverID)
        {
            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Licenses WHERE DriverID = @DriverID ";

            SqlCommand cmd= new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

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
