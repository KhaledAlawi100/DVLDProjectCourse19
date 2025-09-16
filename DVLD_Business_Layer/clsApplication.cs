using DVLD_Data_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Layer
{
    public class clsApplication
    {
        //public enum enMode { AddNew = 0 , Update=1};

        protected clsGloabalBusiness.enMode Mode = clsGloabalBusiness.enMode.add;

        public int ApplicationID {  get; set; }

        public int ApplicationPersonID {  get; set; }

        public DateTime ApplicationDate {  get; set; }

        public int ApplicationTypeID { get; set; }

        public byte ApplicationStatus {  get; set; }

        public DateTime LastStatusDate {  get; set; }

        public int CreatedByUserID { get; set; }

        public float PaidFees { get; set; }

        public clsApplication()
        {
            ApplicationID = 0;
            ApplicationPersonID = 0;
            ApplicationDate = DateTime.Now;
            ApplicationTypeID = 0;
            ApplicationStatus = 0;
            LastStatusDate = DateTime.Now;
            PaidFees = 0;
            CreatedByUserID = 0;
            Mode = clsGloabalBusiness.enMode.add;

        }

        protected clsApplication(int ApplicationID, int ApplicantPersonID , DateTime ApplicationDate ,
            int ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate, float PaidFees, int CreatedByUserID)
        {
            this.ApplicationID = ApplicationID;

            this.ApplicationPersonID = ApplicantPersonID;

            this.ApplicationDate = ApplicationDate;

            this.ApplicationTypeID = ApplicationTypeID;

            this.ApplicationStatus = ApplicationStatus;

            this.LastStatusDate = LastStatusDate;

            this.PaidFees = PaidFees;

            this.CreatedByUserID = CreatedByUserID;

            Mode = clsGloabalBusiness.enMode.update;
        }

        public static clsApplication Find(int ApplicationID)
        {
            int ApplicationPersonID = -1, ApplicationTypeID = -1, CreatedByUserID = -1;
            float PaidFees = -1 ;

            DateTime ApplicationDate = DateTime.Now, LastStatusDate = DateTime.Now;

            byte ApplicationStatus = 0 ;

            if(clsApplicationData.GetApplicationInfoByID(ApplicationID,ref ApplicationPersonID, ref ApplicationDate,
               ref ApplicationTypeID, ref ApplicationStatus , ref LastStatusDate, ref PaidFees , ref CreatedByUserID))
            {
                return new clsApplication(ApplicationID,ApplicationPersonID,ApplicationDate,ApplicationTypeID, ApplicationStatus,
                    LastStatusDate,PaidFees,CreatedByUserID);
            }

            return null; 
        
        }

        

        public static DataTable GetAllApplications()
        {
            return clsApplicationData.GetAllApplication();
        }

        public bool Save()
        {
            clsGloabalBusiness.AddNewAction addNewApplication = AddNewApplication;
            clsGloabalBusiness.UpdateAction updateApplication = UpdateApplication;
            return clsGloabalBusiness.Save(this.Mode, addNewApplication,updateApplication );
        }

        private bool AddNewApplication()
        {
            this.ApplicationID = clsApplicationData.AddNewApplication(this.ApplicationPersonID,
                this.ApplicationDate, this.ApplicationTypeID, this.ApplicationStatus,
                this.LastStatusDate, this.PaidFees, this.CreatedByUserID);

            return this.ApplicationID != -1;
        }

        private bool UpdateApplication()
        {
            return clsApplicationData.UpdateApplication(this.ApplicationID,this.ApplicationPersonID,this.ApplicationDate,
                this.ApplicationTypeID,this.ApplicationStatus,this.LastStatusDate,this.PaidFees,this.CreatedByUserID);
        }


        






    }
}
