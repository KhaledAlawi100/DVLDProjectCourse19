using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Data_Layer
{
    public class clsApplicatinTypeData
    {

        public static DataTable GetAllApplicationTypes()
        {

            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM ApplicationTypes";

            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                con.Open();

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
                con.Close();
            }


            return dt;


        }

        public static bool UpdateApplicationType(int ID,string title,float fees)
        {
            int rowsAffected = 0;

            SqlConnection con = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "UPDATE ApplicationTypes SET ApplicationTypeTitle = @ApplicationTypeTitle, " +
                " ApplicationFees = @ApplicationFees  WHERE ApplicationTypeID = @ApplicationTypeID";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@ApplicationTypeTitle", title);
            cmd.Parameters.AddWithValue("@ApplicationFees",fees);
            cmd.Parameters.AddWithValue("@ApplicationTypeID",ID);

            try
            {
                con.Open();

                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { 
            
            }
            finally { con.Close(); }



            return rowsAffected > 0;
        }

        public static bool GetApplicationTypeInfoByID(int ID, ref string title, ref float fees)
        {
            bool IsFound = false;

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "SELECT * FROM ApplicationTypes WHERE ApplicationTypeID = @ApplicationTypeID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ApplicationTypeID", ID);

                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            IsFound = true;
                            title = reader["ApplicationTypeTitle"].ToString();
                            fees = Convert.ToSingle(reader["ApplicationFees"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.Message);
                }
            }

            return IsFound;
        }

    }
}
