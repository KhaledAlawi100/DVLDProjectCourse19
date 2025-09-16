using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Data_Layer
{
    public class clsUsersData
    {
        public static bool IsUserExist(string Username,string Password)
        {
            bool IsFound=false;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT 1 FROM Users WHERE UserName=@UserName AND Password =@Password";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@UserName", Username);
            cmd.Parameters.AddWithValue("@Password", Password);

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                IsFound = reader.HasRows;
                

            }
            catch (Exception ex) {

            }
            finally
            {
                conn.Close();
            }


            return IsFound;
        }

        public static bool GetUserInfoByID(int UserID, ref int PersonID, ref string UserName,
        ref string Password, ref bool IsActive)
        {
            bool IsFound = false;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Users WHERE UserID=@UserID";

            SqlCommand command = new SqlCommand(query, conn);

            command.Parameters.AddWithValue("@UserID",UserID);

            try
            {
                conn.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read()) { 

                    IsFound= true;

                    PersonID = (int)reader["PersonID"];
                    UserName = (string)reader["UserName"];
                    Password = (string)reader["Password"];
                    IsActive = (bool)reader["IsActive"];
                
                }

            }
            catch (Exception ex) { 
            
            }
            finally { conn.Close(); }


            return IsFound;
        }

        public static bool GetUserInfoByUserName(string UserName, ref int UserID, ref int PersonID,
    ref string Password, ref bool IsActive)
        {
            bool IsFound = false;
            string query = "SELECT * FROM Users WHERE UserName=@UserName";

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                command.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = UserName;

                try
                {
                    conn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            IsFound = true;

                            UserID = reader["UserID"] != DBNull.Value ? (int)reader["UserID"] : 0;
                            PersonID = reader["PersonID"] != DBNull.Value ? (int)reader["PersonID"] : 0;
                            Password = reader["Password"] != DBNull.Value ? (string)reader["Password"] : "";
                            IsActive = reader["IsActive"] != DBNull.Value && (bool)reader["IsActive"];
                        }
                    }
                }
                catch (Exception ex)
                {
                  
                }
                finally {conn.Close();}
            }

            return IsFound;
        }

        public static DataTable GetAllUsers()
        {
            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Users";

            SqlCommand cmd = new SqlCommand(query, conn);

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
            finally {
                conn.Close();
            }

            return dt;

        }

        public static bool isUserExist(string Username)
        {
            bool isFound = false;
            string query = "SELECT 1 FROM Users WHERE UserName = @UserName";

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@UserName", Username);

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

        public static int AddNewUser(int PersonID,string Username,string password , bool IsActive)
        {
            int UserID = -1;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "INSERT INTO Users(PersonID,UserName,Password,IsActive) " +
                "VALUES(@PersonID,@UserName,@Password,@IsActive); SELECT SCOPE_IDENTITY();";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@PersonID",PersonID);
            cmd.Parameters.AddWithValue("@UserName",Username);
            cmd.Parameters.AddWithValue ("@Password", password);
            cmd.Parameters.AddWithValue("IsActive",IsActive);

            try
            {
                conn.Open();

                object result = cmd.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(),out int insertedID)) { 

                    UserID = insertedID;
                
                }

            } catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }

            return UserID; 
        
        }

        public static bool UpdateUser(int UserID, int PersonID, string Username, string password, bool IsActive)
        {
            int rowsAffected = 0;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "UPDATE Users SET PersonID = @PersonID, UserName = @UserName, Password = @Password, " +
                           "IsActive = @IsActive WHERE UserID = @UserID";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@UserID", UserID);
            cmd.Parameters.AddWithValue("@PersonID", PersonID);
            cmd.Parameters.AddWithValue("@UserName", Username);
            cmd.Parameters.AddWithValue("@Password", password);
            cmd.Parameters.AddWithValue("@IsActive", IsActive);

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error: " + ex.Message); // Better than empty catch
            }
            finally
            {
                conn.Close();
            }

            return (rowsAffected > 0);
        }

        public static bool DeleteUser(int UserID , ref int  ExceptionNumber) { 
        
            int rowsAffected = 0;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "DELETE Users WHERE UserID=@UserID";

            SqlCommand command = new SqlCommand(query, conn);

            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                conn.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (SqlException ex) { 
                ExceptionNumber = ex.Number;
            }
            finally
            {
                conn.Close();
            }
            return (rowsAffected > 0);
        }


    }


}
