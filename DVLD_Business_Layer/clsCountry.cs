using DVLD_Data_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Layer
{
    public class clsCountry
    {
        public int id {  get; set; }
        public string countryName { get; set; }

        private clsCountry(int id , string countryName) { 

            this.id = id;
            this.countryName = countryName;
        
        }

        public static clsCountry Find(int ID)
        {
            string countryName = "";
            if(CountryData.GetCountryInfoByID(ID,ref countryName))
            {
                return new clsCountry(ID,countryName);

            }

            return null;

        }

        public static DataTable GetAllCountries()
        {
            return CountryData.GetAllCountries();
        }

        public static  clsCountry Find(string countryName) {

            int ID = -1;
            if (CountryData.GetCountryInfoByName(countryName, ref ID)) 
                return new clsCountry(ID,countryName);
            return null;
        
        }
    }
}
