using DVLD_Data_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DVLD_Business_Layer
{
    public class clsDetainedLicense
    {

        
        
            public int DetainID { get; set; }
            public int LicenseID { get; set; }
            public DateTime DetainDate { get; set; }
            public float FineFees { get; set; }
            public int CreatedByUserID { get; set; }
            public bool IsReleased { get; set; }
            public DateTime? ReleaseDate { get; set; }  
            public int? ReleasedByUserID { get; set; }   // nullable because Allow Nulls = true
            public int? ReleaseApplicationID { get; set; }
            
            //public enum enMode { AddNew =0, Update = 1 }

            clsUtility.enMode mode = clsUtility.enMode.add;



       public clsDetainedLicense() { 
        

        }

        private clsDetainedLicense(int DetainID ,int LicenseID , DateTime DetainDate , float FineFees , 
           int  CreatedByUserID , bool IsReleased, DateTime? ReleaseDate , int? ReleasedByUserID , int? ReleaseApplicationID)
        
        {

            this.DetainID = DetainID;
            this.LicenseID = LicenseID;

            this.mode = clsUtility.enMode.update;

            this.DetainDate = DetainDate;

            this.FineFees = FineFees;

            this.CreatedByUserID = CreatedByUserID;

            this.IsReleased = IsReleased;

            this.ReleaseDate = ReleaseDate;

            this.ReleasedByUserID= ReleasedByUserID;

            this.ReleaseApplicationID = ReleaseApplicationID;



        }


        public static clsDetainedLicense Find(int DetainID)
        {
            int  LicenseID =-1 ;

            DateTime DetainDate= DateTime.Now ;

            float FineFees = -1;

            int CreatedByUserID = -1 ;

            bool IsReleased = false ;

            DateTime ReleaseDate = DateTime.Now ;

            int ReleasedByUserID = -1 ;

            int ReleaseApplicationID = -1 ;

            if(clsDetainedLicensesData.GetDetainedLicenseByDetain(DetainID,ref LicenseID,ref DetainDate,
                ref FineFees, ref CreatedByUserID,ref IsReleased , ref ReleaseDate,ref ReleasedByUserID,ref ReleaseApplicationID))
            {
                return new clsDetainedLicense(DetainID,  LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID,ReleaseApplicationID);
            }

            return null;

        }

        public static clsDetainedLicense FindByLicenseID(int LicenseID)
        {
            int DetainID = -1;

            DateTime DetainDate = DateTime.Now;

            float FineFees = -1;

            int CreatedByUserID = -1;

            bool IsReleased = false;

            DateTime ReleaseDate = DateTime.Now;

            int ReleasedByUserID = -1;

            int ReleaseApplicationID = -1;

            if (clsDetainedLicensesData.GetDetainedLicenseByLicenseID(LicenseID, ref DetainID, ref DetainDate,
                ref FineFees, ref CreatedByUserID, ref IsReleased, ref ReleaseDate, ref ReleasedByUserID, ref ReleaseApplicationID))
            {
                return new clsDetainedLicense(DetainID, LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID);
            }

            return null;

        }

        public static DataTable GetAllDetainedLicenses()
        {
            return clsDetainedLicensesData.GetAllDetainedLicenses();
        }

        private bool AddNewDetainLicense()
        {

            this.DetainID = clsDetainedLicensesData.AddDetainedLicense(this.LicenseID,this.DetainDate,this.FineFees,
                this.CreatedByUserID,this.IsReleased,this.ReleaseDate,this.ReleasedByUserID,this.ReleaseApplicationID);

            return this.DetainID != -1;
        }

        public static string ErrorMessage = "";
        private bool UpdateDetainLicense()
        {
            return clsDetainedLicensesData.UpdateDetainedLicense(ref ErrorMessage,this.DetainID,
                this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID, this.IsReleased,
                this.ReleaseDate,this.ReleasedByUserID, this.ReleaseApplicationID);
        }


        public bool Save()
        {
            return clsUtility.Save(this.mode, AddNewDetainLicense, UpdateDetainLicense);
        }








    }
}
