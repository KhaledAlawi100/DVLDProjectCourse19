using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Data_Layer
{
    public class LicenseClassData
    {
        public static DataTable GetAllLicenseClasses()
        {
            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT *  FROM LicenseClasses";

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
            finally
            {
                conn.Close();
            }


            return dt;
        }

        public static bool GetClassInfoByClassID(int ClassID, ref string ClassName, ref string ClassDescription,
     ref byte MinimumAge, ref byte DefaultValidityLength, ref float ClassFees)
        {
            bool IsFound = false;

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "SELECT ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees " +
                               "FROM LicenseClasses WHERE LicenseClassID = @LicenseClassID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@LicenseClassID", ClassID);

                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;

                                ClassName = reader["ClassName"].ToString();
                                ClassDescription = reader["ClassDescription"].ToString();
                                MinimumAge = Convert.ToByte(reader["MinimumAllowedAge"]);
                                DefaultValidityLength = Convert.ToByte(reader["DefaultValidityLength"]);
                                ClassFees = Convert.ToSingle(reader["ClassFees"]);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error in GetClassInfoByClassID: " + ex.Message);
                    }
                }
            }

            return IsFound;
        }


        public static bool GetClassInfoByClassName(  string ClassName , ref int ClassID, ref string ClassDescribtion,
           ref byte MinimumAge, ref byte DefaultValdityLength, ref float ClassFees)
        {

            bool IsFound = false;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM LicenseClasses WHERE ClassName=@ClassName";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@ClassName", ClassName);

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;

                    ClassID = (int)reader["LicenseClassID"];
                    ClassDescribtion = (string)reader["ClassDescription"];
                    MinimumAge = (byte)reader["MinimumAllowedAge"];
                    DefaultValdityLength = (byte)reader["DefaultValidityLength"];
                    ClassFees = (float)reader["ClassFees"];

                }
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
    }
}
