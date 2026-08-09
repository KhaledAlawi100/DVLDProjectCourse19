using DVLD_Data_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Layer
{
    public class clsLicense
    {

        public int LicenseID {  get; set; }

        public int ApplicationID {get; set; }

        public int DriverID {  get; set; }

        public int LicenseClass {  get; set; }

        public DateTime IssueDate {  get; set; }

        public DateTime ExpirationDate {  get; set; }

        public string Notes {  get; set; }

        public float PaidFees {  get; set; }

        public bool IsActive {  get; set; }

        public byte IssueReason {  get; set; }

        public int CreatedByUserID {  get; set; }

        //public enum enMode { AddNew=0, Update=1 }

        private clsUtility.enMode _Mode = clsUtility.enMode.add;

        

        public clsLicense() { 

        }


        private clsLicense(int LicenseID, int ApplicationID,int DriverID,
            int LicenseClass, DateTime IssueDate , DateTime ExpirationDate,
          string Notes , float PaidFees, bool IsActive , byte IssueReason,
         int CreatedByUserID)
        {
            this.LicenseID = LicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.LicenseClass = LicenseClass;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;
            this.IssueReason = IssueReason;
            this.CreatedByUserID = CreatedByUserID;

            this._Mode= clsUtility.enMode.update;

        }

        public static clsLicense FindByAppID(int ApplicationID)
        {
            int LicenseID = -1, DriverID = -1, LicenseClass = -1;
            DateTime IssueDate=DateTime.Now , ExpirationDate= DateTime.Now ;
            string Notes = "";
            float PaidFees = -1;
            bool IsActive = false;
            byte IssueReason = 0;
            int CreatedByUserID = -1;

            if (clsLicensesData.GetLicenseByApplicationID(ApplicationID, ref LicenseID,ref DriverID,ref LicenseClass,
                ref IssueDate,ref ExpirationDate , ref Notes , ref PaidFees , ref IsActive ,ref IssueReason,ref CreatedByUserID))
            {

                return new clsLicense(LicenseID,ApplicationID,DriverID,LicenseClass,IssueDate,ExpirationDate,Notes,PaidFees,IsActive,IssueReason,CreatedByUserID);
            }


            return null;
        }

        public static clsLicense Find(int LicenseID)
        {
            int ApplicationID = -1, DriverID = -1, LicenseClass = -1;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            string Notes = "";
            float PaidFees = -1;
            bool IsActive = false;
            byte IssueReason = 0;
            int CreatedByUserID = -1;

            if (clsLicensesData.GetLicenseByLicenseID(LicenseID,ref ApplicationID,ref DriverID,ref LicenseClass ,
                ref IssueDate , ref ExpirationDate, ref Notes , ref PaidFees , ref IsActive,ref IssueReason,ref CreatedByUserID) )
            {
                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReason, CreatedByUserID);


            }

            return null;
        }



        public static string Message = "";
        private bool AddNewLicense()
        {
            this.LicenseID = clsLicensesData.AddNewLicense(ref Message,this.ApplicationID,this.DriverID,this.LicenseClass,this.IssueDate,
                this.ExpirationDate,this.Notes,this.PaidFees,this.IsActive,this.IssueReason,this.CreatedByUserID);

            return this.LicenseID != -1;
        }

        private bool UpdateLicense()
        {
            return clsLicensesData.UpdateLicense(ref Message,this.LicenseID,this.ApplicationID,this.DriverID, this.LicenseClass,this.IssueDate,this.ExpirationDate,this.Notes,
                this.PaidFees,this.IsActive,this.IssueReason, this.CreatedByUserID);
        }

        
        public bool Save()
        {
            return clsUtility.Save(_Mode, AddNewLicense, UpdateLicense);
        }

        public static DataTable GetAllLicensesByDriverID(int DriverID)
        {
            return clsLicensesData.GetAllLicensesByDriverID(DriverID);
        }

       
    }
}
