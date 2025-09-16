using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Data_Layer
{
    public  class clsTestTypesData
    {

        public static DataTable GetAllTestTypes()
        {
            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM TestTypes";

            SqlCommand cmd = new SqlCommand(query, conn);

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
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

        public static bool GetTestTypeByID(int testTypeID, ref string testTypeTitle, ref string testTypeDescription, ref float testTypeFees)
        {
            bool isFound = false;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM TestTypes WHERE TestTypeID = @TestTypeID";

            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.Add("@TestTypeID", SqlDbType.Int).Value = testTypeID;

            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read()) // Move to the first row
                {
                    isFound = true;

                    testTypeTitle = Convert.ToString(reader["TestTypeTitle"]);
                    testTypeDescription = Convert.ToString(reader["TestTypeDescription"]);
                    testTypeFees = Convert.ToSingle(reader["TestTypeFees"]);
                }

                reader.Close(); // Always close the reader
            }
            catch (Exception ex)
            {
                // Log or handle the error as needed
                throw; // or log it
            }
            finally
            {
                conn.Close();
            }

            return isFound;
        }


        public static bool UpdateTestType(int ID , string Title , string Describtion , float fees)
        {
            int rowsAffected = 0;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "UPDATE TestTypes SET TestTypeTitle = @TestTypeTitle, " +
                "TestTypeDescription = @TestTypeDescription, " +
                " TestTypeFees = @TestTypeFees  WHERE TestTypeID=@TestTypeID";

            SqlCommand cmd = new SqlCommand(query,conn);

            cmd.Parameters.AddWithValue("@TestTypeID", ID);
            cmd.Parameters.AddWithValue("@TestTypeTitle", Title);
            cmd.Parameters.AddWithValue("@TestTypeDescription", Describtion);
            cmd.Parameters.AddWithValue("@TestTypeFees", fees);

            try
            {
                conn.Open();

                rowsAffected = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
            }
            finally
            {
                conn.Close();
            }

            return rowsAffected > 0;    

        }
    }
}
