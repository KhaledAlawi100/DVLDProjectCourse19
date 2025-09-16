using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DVLD_Data_Layer
{
    public class clsPeopleDataAccess
    {
        public static bool GetPersonInfoByID(int ID , ref string NationalNumber,ref string FirstName ,ref string SecondName,
           ref string ThirdName , ref string LastName , ref DateTime DateOfBirth , ref byte Gender , ref string Address ,
           ref string Phone , ref string Email , ref int CountryID , ref string imagePath )
        {
            bool isFound = false;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM People WHERE PersonID = @PersonID";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@PersonID", ID);

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read()) { 

                    isFound = true;

                    NationalNumber = (string)reader["NationalNo"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];

                    if (reader["ThirdName"] != DBNull.Value)
                        ThirdName = (string)reader["ThirdName"];
                    else
                        ThirdName = "";

                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gender = (byte)reader["Gendor"];
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];

                    if (reader["Email"] != DBNull.Value)
                        Email = (string)reader["Email"];
                    else
                        Email = "";

                    CountryID = (int)reader["NationalityCountryID"];

                    if (reader["ImagePath"] != DBNull.Value)
                        imagePath = (string)reader["ImagePath"];
                    else
                        imagePath = "";

                
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

        public static bool GetPersonInfoByNationalNo(  string NationalNumber, ref int ID, ref string FirstName, ref string SecondName,
           ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref byte Gender, ref string Address,
           ref string Phone, ref string Email, ref int CountryID, ref string imagePath)
        {
            bool isFound = false;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM People WHERE NationalNo = @NationalNo";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@NationalNo", NationalNumber);

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {

                    isFound = true;

                    ID = (int)reader["PersonID"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];

                    if (reader["ThirdName"] != DBNull.Value)
                        ThirdName = (string)reader["ThirdName"];
                    else
                        ThirdName = "";

                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gender = (byte)reader["Gendor"];
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];

                    if (reader["Email"] != DBNull.Value)
                        Email = (string)reader["Email"];
                    else
                        Email = "";

                    CountryID = (int)reader["NationalityCountryID"];

                    if (reader["ImagePath"] != DBNull.Value)
                        imagePath = (string)reader["ImagePath"];
                    else
                        imagePath = "";


                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();

            }





            return isFound;

        }


        public static DataTable GetAllPeople()
        {
            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM People";

            SqlCommand cmd = new SqlCommand(query, conn);

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
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

        public static bool isPersonExist(string nationalNo)
        {
            bool isFound = false;
            string query = "SELECT 1 FROM People WHERE NationalNo = @NationalNo";

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@NationalNo", nationalNo);

                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        isFound = reader.HasRows;
                    }
                }
                catch (Exception ex)
                {
                    // Log or rethrow if needed
                   // Console.WriteLine("Error: " + ex.Message);
                }
            }

            return isFound;
        }

        public static int AddNewPerson(string NationalNumber, string FirstName, string SecondName, string ThirdName, string LastName,
            DateTime DateOfBirth, byte Gender, string Address, string Phone, string Email, int CountryID, string ImagePath)
        {
            int PersonID = -1;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "INSERT INTO People (NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gendor, " +
                " Address, Phone, Email , NationalityCountryID, ImagePath)  " +
                " VALUES(@NationalNo, @FirstName, @SecondName, @ThirdName, @LastName, @DateOfBirth, @Gendor, @Address, @Phone, @Email, @NationalityCountryID," +
                " @ImagePath);SELECT SCOPE_IDENTITY(); ";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@NationalNo", NationalNumber);

            cmd.Parameters.AddWithValue("@FirstName", FirstName);
            cmd.Parameters.AddWithValue("@SecondName", SecondName);

            if (!string.IsNullOrEmpty(ThirdName))
                cmd.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
                cmd.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);

            cmd.Parameters.AddWithValue("@LastName", LastName);

            cmd.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            cmd.Parameters.AddWithValue("@Gendor", Gender);
            cmd.Parameters.AddWithValue("@Address", Address);

            cmd.Parameters.AddWithValue("@Phone", Phone);

            if (!string.IsNullOrEmpty(Email))
                cmd.Parameters.AddWithValue("@Email", Email);
            else
                cmd.Parameters.AddWithValue("@Email", System.DBNull.Value);

            cmd.Parameters.AddWithValue("@NationalityCountryID", CountryID);

            if (!string.IsNullOrEmpty(ImagePath))
                cmd.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                cmd.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);

            try
            {
                conn.Open();

                object result = cmd.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(),out int insertedID)) { 
                
                    PersonID = insertedID;
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();

            }

            return PersonID;

        }


        public static bool UpdatePerson(int ID, string NationalNumber, string FirstName, string SecondName, string ThirdName, string LastName,
    DateTime DateOfBirth, byte Gender, string Address, string Phone, string Email, int CountryID, string ImagePath)
        {
            int rowsAffected = 0;

            string query = @"UPDATE People SET NationalNo = @NationalNo, FirstName = @FirstName, 
                     SecondName = @SecondName, ThirdName = @ThirdName, LastName = @LastName, 
                     DateOfBirth = @DateOfBirth, Gendor = @Gendor, Address = @Address, 
                     Phone = @Phone, Email = @Email, NationalityCountryID = @NationalityCountryID, 
                     ImagePath = @ImagePath WHERE PersonID = @PersonID";

            try
            {
                using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PersonID", ID);
                    cmd.Parameters.AddWithValue("@NationalNo", NationalNumber);
                    cmd.Parameters.AddWithValue("@FirstName", FirstName);
                    cmd.Parameters.AddWithValue("@SecondName", SecondName);
                    cmd.Parameters.AddWithValue("@ThirdName", string.IsNullOrEmpty(ThirdName) ? (object)DBNull.Value : ThirdName);
                    cmd.Parameters.AddWithValue("@LastName", LastName);
                    cmd.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                    cmd.Parameters.AddWithValue("@Gendor", Gender);
                    cmd.Parameters.AddWithValue("@Address", Address);
                    cmd.Parameters.AddWithValue("@Phone", Phone);
                    cmd.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(Email) ? (object)DBNull.Value : Email);
                    cmd.Parameters.AddWithValue("@NationalityCountryID", CountryID);
                    cmd.Parameters.AddWithValue("@ImagePath", string.IsNullOrEmpty(ImagePath) ? (object)DBNull.Value : ImagePath);

                    conn.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
               // Console.WriteLine("Error in UpdateContact: " + ex.Message);
                // Optionally log to file/db or rethrow
            }

            return rowsAffected > 0;
        }

        public static bool DeletePerson(int PersondID,ref int exceptionNumber)
        {
            int rowsAffected = 0;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"DELETE People WHERE  PersonID=@PersonID";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@PersonID", PersondID);

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();

            }
            catch (SqlException ex) { 

                exceptionNumber = ex.Number;
            
            }
            finally
            {
                conn.Close();
            }


            return (rowsAffected > 0);
        }

        

    }
}
