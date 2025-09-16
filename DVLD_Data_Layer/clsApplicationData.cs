using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Data_Layer
{
    public class clsApplicationData
    {

        public static DataTable GetAllApplication()
        {
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Applications";

            SqlCommand cmd = new SqlCommand(query, con);

            try
            {
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    dt.Load(reader);
                }

            }
            catch (Exception ex) { 
            
            }
            finally
            {
                con.Close();
            }

            return dt;
        }

        public static int AddNewApplication(
     int applicantPersonID,
     DateTime applicationDate,
     int applicationTypeID,
     byte applicationStatus,
     DateTime lastStatusDate,
     float paidFees,
     int createdByUserID)
        {
            int applicationID = -1;

            string sql = @"
        INSERT INTO Applications
            (ApplicantPersonID, ApplicationDate, ApplicationTypeID,
             ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID)
        VALUES
            (@ApplicantPersonID, @ApplicationDate, @ApplicationTypeID,
             @ApplicationStatus, @LastStatusDate, @PaidFees, @CreatedByUserID);
        SELECT CAST(SCOPE_IDENTITY() AS int);";

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.Add("@ApplicantPersonID", SqlDbType.Int).Value = applicantPersonID;
            cmd.Parameters.Add("@ApplicationDate", SqlDbType.DateTime2).Value = applicationDate;
            cmd.Parameters.Add("@ApplicationTypeID", SqlDbType.Int).Value = applicationTypeID;
            cmd.Parameters.Add("@ApplicationStatus", SqlDbType.TinyInt).Value = applicationStatus;
            cmd.Parameters.Add("@LastStatusDate", SqlDbType.DateTime2).Value = lastStatusDate;
            cmd.Parameters.Add("@PaidFees", SqlDbType.Decimal).Value = paidFees;
            cmd.Parameters.Add("@CreatedByUserID", SqlDbType.Int).Value = createdByUserID;

            try
            {
                conn.Open();
                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    applicationID = Convert.ToInt32(result);
                }
            }
            catch (Exception)
            {
                // You can log the exception or handle it as needed
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }

            return applicationID;
        }

        public static bool UpdateApplication(
    int applicationID,
    int applicantPersonID,
    DateTime applicationDate,
    int applicationTypeID,
    byte applicationStatus,
    DateTime lastStatusDate,
    float paidFees,
    int createdByUserID)
        {
            bool isUpdated = false;

            string sql = @"
    UPDATE Applications
    SET
        ApplicantPersonID = @ApplicantPersonID,
        ApplicationDate = @ApplicationDate,
        ApplicationTypeID = @ApplicationTypeID,
        ApplicationStatus = @ApplicationStatus,
        LastStatusDate = @LastStatusDate,
        PaidFees = @PaidFees,
        CreatedByUserID = @CreatedByUserID
    WHERE
        ApplicationID = @ApplicationID";

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@ApplicationID", SqlDbType.Int).Value = applicationID;
                cmd.Parameters.Add("@ApplicantPersonID", SqlDbType.Int).Value = applicantPersonID;
                cmd.Parameters.Add("@ApplicationDate", SqlDbType.DateTime2).Value = applicationDate;
                cmd.Parameters.Add("@ApplicationTypeID", SqlDbType.Int).Value = applicationTypeID;
                cmd.Parameters.Add("@ApplicationStatus", SqlDbType.TinyInt).Value = applicationStatus;
                cmd.Parameters.Add("@LastStatusDate", SqlDbType.DateTime2).Value = lastStatusDate;
                cmd.Parameters.Add("@PaidFees", SqlDbType.Decimal).Value = paidFees;
                cmd.Parameters.Add("@CreatedByUserID", SqlDbType.Int).Value = createdByUserID;

                try
                {
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    isUpdated = rowsAffected > 0;
                }
                catch (Exception)
                {
                    // Log or handle the exception as needed
                }
            }

            return isUpdated;
        }


        public static bool GetApplicationInfoByID(int ApplicationID, ref int ApplicantPersonID,ref DateTime ApplicationDate,
            ref int ApplicationTypeID, ref byte ApplicationStatus, ref DateTime LastStatusDate,
            ref float PaidFees, ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Applications WHERE ApplicationID = @ApplicationID ";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                conn.Open() ;

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read()) {

                    isFound = true ;
                    ApplicantPersonID = (int)reader["ApplicantPersonID"];
                    ApplicationDate = (DateTime)reader["ApplicationDate"];
                    ApplicationTypeID = (int)reader["ApplicationTypeID"];
                    ApplicationStatus = (byte)reader["ApplicationStatus"];
                    LastStatusDate = (DateTime)reader["LastStatusDate"];
                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];


                }

            }
            catch (Exception ex) {
            
            }
            finally { conn.Close(); }


            return isFound;

        }
        public static DataTable GetApplicationsInfoByPersonID(int ApplicantPersonID)
        {
            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Applications WHERE ApplicantPersonID = @ApplicantPersonID ";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);

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
            finally { conn.Close(); }


            return dt;

        }
    }
}
