using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Data_Layer
{
    public class clsDetainedLicensesData
    {

        public static bool GetDetainedLicenseByDetain(int DetainID, ref int LicenseID, ref DateTime DetainDate,
    ref float FineFees, ref int CreatedByUserID, ref bool IsReleased, ref DateTime ReleaseDate,
    ref int ReleasedByUserID, ref int ReleaseApplicationID)
        {
            bool IsFound = false;

            SqlConnection con = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM DetainedLicenses WHERE DetainID = @DetainID";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@DetainID", DetainID);

            try
            {
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;

                    LicenseID = (int)reader["LicenseID"];
                    DetainDate = (DateTime)reader["DetainDate"];
                    FineFees = Convert.ToSingle(reader["FineFees"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsReleased = (bool)reader["IsReleased"];

                    // Nullable fields
                    ReleaseDate = reader["ReleaseDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)reader["ReleaseDate"];
                    ReleasedByUserID = reader["ReleasedByUserID"] == DBNull.Value ? -1 : (int)reader["ReleasedByUserID"];
                    ReleaseApplicationID = reader["ReleaseApplicationID"] == DBNull.Value ? -1 : (int)reader["ReleaseApplicationID"];
                }
            }
            catch (Exception ex)
            {
                // Log or handle exception if needed
            }
            finally
            {
                con.Close();
            }

            return IsFound;
        }

        public static int AddDetainedLicense(int LicenseID, DateTime DetainDate,
    float FineFees, int CreatedByUserID, bool IsReleased,
    DateTime? ReleaseDate, int? ReleasedByUserID, int? ReleaseApplicationID)
        {
            int DetainID = -1;

            SqlConnection con = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO DetainedLicenses 
        (LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID)
        VALUES (@LicenseID, @DetainDate, @FineFees, @CreatedByUserID, @IsReleased, @ReleaseDate, @ReleasedByUserID, @ReleaseApplicationID);
        SELECT SCOPE_IDENTITY();";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@LicenseID", LicenseID);
            cmd.Parameters.AddWithValue("@DetainDate", DetainDate);
            cmd.Parameters.AddWithValue("@FineFees", FineFees);
            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            cmd.Parameters.AddWithValue("@IsReleased", IsReleased);

            // Nullable columns
            if (ReleaseDate.HasValue)
                cmd.Parameters.AddWithValue("@ReleaseDate", ReleaseDate.Value);
            else
                cmd.Parameters.AddWithValue("@ReleaseDate", DBNull.Value);

            if (ReleasedByUserID.HasValue)
                cmd.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID.Value);
            else
                cmd.Parameters.AddWithValue("@ReleasedByUserID", DBNull.Value);

            if (ReleaseApplicationID.HasValue)
                cmd.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID.Value);
            else
                cmd.Parameters.AddWithValue("@ReleaseApplicationID", DBNull.Value);

            try
            {
                con.Open();
                object result = cmd.ExecuteScalar();
                if (result != null)
                    DetainID = Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                con.Close();
            }

            return DetainID;
        }

        public static bool UpdateDetainedLicense(ref string ErrorMsg,int DetainID, int LicenseID, DateTime DetainDate,
    float FineFees, int CreatedByUserID, bool IsReleased,
    DateTime? ReleaseDate, int? ReleasedByUserID, int? ReleaseApplicationID)
        {
            int rowsAffected = 0;

            SqlConnection con = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE DetainedLicenses 
        SET LicenseID=@LicenseID, DetainDate=@DetainDate, FineFees=@FineFees,
            CreatedByUserID=@CreatedByUserID, IsReleased=@IsReleased,
            ReleaseDate=@ReleaseDate, ReleasedByUserID=@ReleasedByUserID,
            ReleaseApplicationID=@ReleaseApplicationID
        WHERE DetainID=@DetainID";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@DetainID", DetainID);
            cmd.Parameters.AddWithValue("@LicenseID", LicenseID);
            cmd.Parameters.AddWithValue("@DetainDate", DetainDate);
            cmd.Parameters.AddWithValue("@FineFees", FineFees);
            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            cmd.Parameters.AddWithValue("@IsReleased", IsReleased);

            // Nullable columns
            if (ReleaseDate.HasValue)
                cmd.Parameters.AddWithValue("@ReleaseDate", ReleaseDate.Value);
            else
                cmd.Parameters.AddWithValue("@ReleaseDate", DBNull.Value);

            if (ReleasedByUserID.HasValue)
                cmd.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID.Value);
            else
                cmd.Parameters.AddWithValue("@ReleasedByUserID", DBNull.Value);

            if (ReleaseApplicationID.HasValue)
                cmd.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID.Value);
            else
                cmd.Parameters.AddWithValue("@ReleaseApplicationID", DBNull.Value);

            try
            {
                con.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.Message;
            }
            finally
            {
                con.Close();
            }

            return (rowsAffected > 0);
        }

        public static DataTable GetAllDetainedLicenses()
        {
            DataTable table = new DataTable();

            SqlConnection con = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM DetainedLicenses";

            SqlCommand cmd = new SqlCommand(query, con);

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    table.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                con.Close();
            }

            return table;
        }

        public static bool GetDetainedLicenseByLicenseID(int LicenseID, ref int DetainID, ref DateTime DetainDate,
    ref float FineFees, ref int CreatedByUserID, ref bool IsReleased, ref DateTime ReleaseDate,
    ref int ReleasedByUserID, ref int ReleaseApplicationID)
        {
            bool IsFound = false;

            using (SqlConnection con = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                // Select the latest record by DetainDate
                string query = @"
            SELECT TOP 1 *
            FROM DetainedLicenses
            WHERE LicenseID = @LicenseID
            ORDER BY DetainDate DESC";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@LicenseID", LicenseID);

                    try
                    {
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            IsFound = true;

                            DetainID = (int)reader["DetainID"];
                            DetainDate = (DateTime)reader["DetainDate"];
                            FineFees = Convert.ToSingle(reader["FineFees"]);
                            CreatedByUserID = (int)reader["CreatedByUserID"];
                            IsReleased = (bool)reader["IsReleased"];

                            // Handle nullable fields
                            ReleaseDate = reader["ReleaseDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)reader["ReleaseDate"];
                            ReleasedByUserID = reader["ReleasedByUserID"] == DBNull.Value ? -1 : (int)reader["ReleasedByUserID"];
                            ReleaseApplicationID = reader["ReleaseApplicationID"] == DBNull.Value ? -1 : (int)reader["ReleaseApplicationID"];
                        }
                    }
                    catch (Exception ex)
                    {
                        // log or handle exception
                    }
                }
            }

            return IsFound;
        }






    }
}
