using DVLD_Data_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Layer
{
    public class clsApplicationType
    {
        public int ID {  get; set; }
        public string Title {  get; set; }
        public float Fees  { get; set; }

        public static bool UpdateAppliactionType(int ID , string title , float fees) { 

            return clsApplicatinTypeData.UpdateApplicationType(ID , title , fees);
        }

        public clsApplicationType() { 
        
        }

        private clsApplicationType(int ID,string title , float fees)
        {
            this.ID = ID;
            this.Title = title;
            this.Fees = fees;

        }

        public static clsApplicationType Find(int ID)
        {
            string title = "";
            float fees = -1;
            if(clsApplicatinTypeData.GetApplicationTypeInfoByID(ID,ref title,ref fees))
            {
                return new clsApplicationType(ID,title,fees);
            }
            return null;

        }
        public static DataTable GetAllAppTypes()
        {
            return  clsApplicatinTypeData.GetAllApplicationTypes();
        }
    }
}
