using DVLD_Data_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Layer
{
    public class clsLDLApp : clsApplication
    {

        public int LocalDrivingLicenseApplicationID {  get; set; }

        public int LicenseClassID { get; set; }

        public clsLDLApp() { 

            this.LicenseClassID = 0;
            this.ApplicationID = 0;
            this.LicenseClassID = 0;

            Mode = clsUtility.enMode.add;

        
        }

        private clsLDLApp(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate,
            int ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate, 
            float PaidFees, int CreatedByUserID, int LocalDrivingLicenseApplicationID  , int LicenseClassID)
            :base(ApplicationID, ApplicantPersonID, ApplicationDate, ApplicationTypeID, ApplicationStatus, LastStatusDate,
               PaidFees , CreatedByUserID)
        {
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.LicenseClassID = LicenseClassID;
            Mode = clsUtility.enMode.update;

        }

        private clsLDLApp(int LocalDrivingLicenseApplicationID , int AppID , int ClassID)
        {
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID ;
            this.LicenseClassID = ClassID;
            this.ApplicationID= AppID;

        }

        private bool _AddNewLDLApp()
        {
            this.ApplicationID = clsApplicationData.AddNewApplication(this.ApplicationPersonID, this.ApplicationDate,
                this.ApplicationTypeID, this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);
          
            if (this.ApplicationID == -1) return false;

            this.LocalDrivingLicenseApplicationID = clsLDLAppData.AddNewLDLApp(this.ApplicationID, this.LicenseClassID);
            

            return (this.LicenseClassID != -1 );


        }

        public static DataTable GetAllLDLApps()
        {
            return clsLDLAppData.GetAllLDLApps();

        }

        public static bool haveSameApp(int PersonID, int LicenseClassID)
        {
            DataTable dt = clsApplicationData.GetApplicationsInfoByPersonID(PersonID);

            foreach(DataRow dr in dt.Rows)
            {
                int status = (byte)dr["ApplicationStatus"];
                int appID = (int)dr["ApplicationID"];

                int OldLicenseClassID = -1, LocalDrivingLicenseApplicationID = -1;

                if(clsLDLAppData.GetLDLInfoByAppID(appID, ref LocalDrivingLicenseApplicationID,ref OldLicenseClassID))
                {
                    if(LicenseClassID == OldLicenseClassID && (status == 1 || status ==3) )
                        return true;

                }
            }

            return false;
        }
        public static clsLDLApp Find(int ID)
        {
            int AppID = -1, LicenseClassID = -1;

            if(clsLDLAppData.GetLDLInfoByID(ID,ref AppID , ref LicenseClassID))
            {
                return new clsLDLApp(ID,AppID,LicenseClassID);
            }
            return null;


        }
        public bool Save()
        {
            return clsUtility.Save(this.Mode, _AddNewLDLApp, null);
        }
        public static string ErrorMessage;
        public static bool Delete(int ID) { 

            return clsLDLAppData.DeleteLDLApp(ref ErrorMessage,ID);
        
        }

    }
}
