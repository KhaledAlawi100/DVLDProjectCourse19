using DVLD_Data_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Layer
{
    public class clsInternationalLicense
    {

        public int InternationalLicenseID { get; set; }

        public int ApplicationID {  get; set; }

        public int DriverID {  get; set; }

        public int IssuedUsingLocalLicenseID {  get; set; }

        public DateTime IssueDate {  get; set; }

        public DateTime ExpirationDate {  get; set; }

        public bool IsActive {  get; set; }

        public int CreatedByUserID {  get; set; }

        //public enum enMode { AddNew=0, Update=1 }

        clsUtility.enMode _Mode = clsUtility.enMode.add;

        public clsInternationalLicense() { 

            _Mode = clsUtility.enMode.add;
        
        }

        private clsInternationalLicense(int internationalLicenseID, int applicationID, int driverID, int issuedUsingLocalLicenseID, DateTime issueDate, DateTime expirationDate, bool isActive, int createdByUserID)
        {
            InternationalLicenseID = internationalLicenseID;
            ApplicationID = applicationID;
            DriverID = driverID;
            IssuedUsingLocalLicenseID = issuedUsingLocalLicenseID;
            IssueDate = issueDate;
            ExpirationDate = expirationDate;
            IsActive = isActive;
            CreatedByUserID = createdByUserID;

            _Mode= clsUtility.enMode.update;

        }


        public static clsInternationalLicense Find(int InternationalLicenseID)
        {
            int ApplicationID = -1, DriverID = -1, IssuedUsingLocalLicenseID = -1;
            DateTime IssueDate =DateTime.Now , ExpirationDate=DateTime.Now ;
            bool IsActive = false;
            int CreatedByUserID = -1;


            if (clsInternationalLicensesData.GetInternaionalLicenseByLicenseID(InternationalLicenseID,ref ApplicationID,ref DriverID,
                ref IssuedUsingLocalLicenseID,ref IssueDate, ref ExpirationDate,ref IsActive , ref CreatedByUserID))
            {
                return new clsInternationalLicense(InternationalLicenseID,ApplicationID,DriverID,IssuedUsingLocalLicenseID,IssueDate,
                    ExpirationDate,IsActive,CreatedByUserID);
            }

            return null;

        }

        private bool _AddNewInterNaionalLicense()
        {
            this.InternationalLicenseID = clsInternationalLicensesData.AddNewInterNationalLicense(this.ApplicationID,this.DriverID,this.IssuedUsingLocalLicenseID,
                this.IssueDate,this.ExpirationDate,this.IsActive,this.CreatedByUserID);

            return this.InternationalLicenseID != -1;

  
        }



        private bool _UpdateInterNationalLicense()
        {
            return clsInternationalLicensesData.UpdateInternationalLicense(this.InternationalLicenseID,this.ApplicationID,this.DriverID,
                this.IssuedUsingLocalLicenseID,this.IssueDate,this.ExpirationDate,this.IsActive,this.CreatedByUserID);
        }


        public bool Save()
        {
            return clsUtility.Save(_Mode, _AddNewInterNaionalLicense, _UpdateInterNationalLicense);
        }

        public static bool IsExist(int DriverID)
        {
            return clsInternationalLicensesData.IsInternationalLicenseExistsByDriverID(DriverID);
        }

        public static DataTable GetAllInternationalLicense()
        {
            return clsInternationalLicensesData.GetAllInternationalLicenses();  
        }
        public static DataTable GetAllInternationalLicenseByDriverID(int DriverID)
        {
            return clsInternationalLicensesData.GetAllInternationalLicensesByDriverID(DriverID);
        }
    }
}
