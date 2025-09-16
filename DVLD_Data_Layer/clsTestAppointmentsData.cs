using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Data_Layer
{
    public class clsTestAppointmentsData
    {

        public static DataTable GetAllTestAppointmentsByLDLandTestType(int LocalDrivingLicenseApplicationID,int TestTypeID)
        {
            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM TestAppointments WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID " +
                "AND TestTypeID = @TestTypeID";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            cmd.Parameters.AddWithValue("@TestTypeID", TestTypeID);

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

        public static DataTable GetAllTestAppointmentsByLDL(int LocalDrivingLicenseApplicationID)
        {
            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM TestAppointments WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID " ;

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {

                    dt.Load(reader);

                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }

            return dt;
        }


        public static int AddNewTestAppointment(ref string errorMessage,
     int TestTypeID,
     int LocalDrivingLicenseApplicationID,
     DateTime AppointmentDate,
     float PaidFees,
     int CreatedByUserID,
     bool IsLocked,
     int RetakeTestApplicationID)
        {
            int TestAppointmentID = -1;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "INSERT INTO TestAppointments " +
                           "(TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, " +
                           "PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID) " +
                           "VALUES (@TestTypeID, @LocalDrivingLicenseApplicationID, @AppointmentDate, " +
                           "@PaidFees, @CreatedByUserID, @IsLocked, @RetakeTestApplicationID); " +
                           "SELECT SCOPE_IDENTITY();";

            SqlCommand cmd = new SqlCommand(query, conn);

            // Add parameters (with leading @ and in correct order)
            cmd.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            cmd.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            cmd.Parameters.AddWithValue("@PaidFees", PaidFees);
            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            cmd.Parameters.AddWithValue("@IsLocked", IsLocked);

            if(RetakeTestApplicationID != -1)
                cmd.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);
            else
                cmd.Parameters.AddWithValue("@RetakeTestApplicationID", System.DBNull.Value);



            try
            {
                conn.Open();

                object result = cmd.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    TestAppointmentID = insertedID;
                }
            }
            catch (Exception ex)
            {
              errorMessage = ex.Message+ " - This is the LocalDrivingLicenseApplicationID " +LocalDrivingLicenseApplicationID;
            }
            finally
            {
                conn.Close();
            }

            return TestAppointmentID;
        }

        public static bool UpdateTestAppointments(
    int TestAppointmentID,
    int TestTypeID,
    int LocalDrivingLicenseApplicationID,
    DateTime AppointmentDate,
    float PaidFees,
    int CreatedByUserID,
    bool IsLocked,
    int RetakeTestApplicationID)
        {
            int rowsAffected = 0;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "UPDATE TestAppointments SET " +
                "TestTypeID = @TestTypeID, " +
                "LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID, " +
                "AppointmentDate = @AppointmentDate, " +
                "PaidFees = @PaidFees, " +
                "CreatedByUserID = @CreatedByUserID, " +
                "IsLocked = @IsLocked, " +
                "RetakeTestApplicationID = @RetakeTestApplicationID " +
                "WHERE TestAppointmentID = @TestAppointmentID";

            SqlCommand cmd = new SqlCommand(query, conn);

            // Add all parameters
            cmd.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            cmd.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            cmd.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            cmd.Parameters.AddWithValue("@PaidFees", PaidFees);
            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            cmd.Parameters.AddWithValue("@IsLocked", IsLocked);
           
            if (RetakeTestApplicationID!=-1)
                cmd.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);
            else
                cmd.Parameters.AddWithValue("@RetakeTestApplicationID", System.DBNull.Value);


            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Optional: Log the exception
            }
            finally
            {
                conn.Close();
            }

            return (rowsAffected > 0);
        }


        public static bool GetTestAppointmentByID(
    int TestAppointmentID,
    ref int TestTypeID,
    ref int LocalDrivingLicenseApplicationID,
    ref DateTime AppointmentDate,
    ref float PaidFees,
    ref int CreatedByUserID,
    ref bool IsLocked,
    ref int RetakeTestApplicationID)
        {
            bool isFound = false;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM TestAppointments WHERE TestAppointmentID = @TestAppointmentID";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    TestTypeID = Convert.ToInt32(reader["TestTypeID"]);
                    LocalDrivingLicenseApplicationID = Convert.ToInt32(reader["LocalDrivingLicenseApplicationID"]);
                    AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]);
                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                    IsLocked = Convert.ToBoolean(reader["IsLocked"]);

                    // Handle NULL for RetakeTestApplicationID
                    if (reader["RetakeTestApplicationID"] != DBNull.Value)
                        RetakeTestApplicationID = Convert.ToInt32(reader["RetakeTestApplicationID"]);
                    else
                        RetakeTestApplicationID = -1; // or any sentinel value to represent null
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                // Optional: log or handle the exception
            }
            finally
            {
                conn.Close();
            }

            return isFound;
        }




    }
}
