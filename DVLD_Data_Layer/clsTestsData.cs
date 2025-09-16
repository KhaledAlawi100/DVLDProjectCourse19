using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Data_Layer
{
    public class clsTestsData
    {

        public static bool GetTestInfoByAppointmentID(int TestAppointmentID ,ref int TestID ,
            ref bool TestResult , ref string Notes ,ref  int CreatedByUserID)
        {
            bool isFound=false;

            SqlConnection con = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Tests WHERE TestAppointmentID = @TestAppointmentID";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            try
            {
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read()) { 
                    isFound = true;

                    TestID = (int)reader["TestID"];

                    TestResult = (bool)reader["TestResult"];

                    if (reader["Notes"] == System.DBNull.Value)
                    {

                        Notes = "";
                    }
                    else
                        Notes = (string)reader["Notes"];

                    CreatedByUserID = (int)reader["CreatedByUserID"];

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


        public static int AddNewTest(int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            int TestID = -1;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "INSERT INTO Tests (TestAppointmentID, TestResult, Notes, CreatedByUserID) " +
                           "VALUES (@TestAppointmentID, @TestResult, @Notes, @CreatedByUserID); " +
                           "SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, conn);

            // Add all required parameters
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", TestResult);
            
            if(!string.IsNullOrEmpty(Notes))
                command.Parameters.AddWithValue("@Notes", Notes);
            else
                command.Parameters.AddWithValue("@Notes", System.DBNull.Value);

            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                conn.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    TestID = insertedID;
                }
            }
            catch (Exception ex)
            {
                // Optional: log or re-throw exception
            }
            finally
            {
                conn.Close();
            }

            return TestID;
        }

    }
}
