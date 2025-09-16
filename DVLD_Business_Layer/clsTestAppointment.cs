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
    public class clsTestAppointment
    {

        //public enum enMode { Addnew=0 , Update=1};

        private clsGloabalBusiness.enMode _Mode = clsGloabalBusiness.enMode.add;

        public int TestAppointmentID { get; set; }

        public int TestTypeID { get; set; }

        public int LocalDrivingLicenseApplicationID {  get; set; }

        public DateTime AppointmentDate {  get; set; }

        public float PaidFees { get; set; }

        public int CreatedByUserID { get; set; }

        public bool IsLocked { get; set; }

        public int RetakeTestApplicationID {  get; set; }


        public clsTestAppointment()
        {
            _Mode = clsGloabalBusiness.enMode.add;

        }

        private clsTestAppointment( int testAppointmentID, int testTypeID, int localDrivingLicenseApplicationID, DateTime appointmentDate, float paidFees, int createdByUserID, bool isLocked, int retakeTestApplicationID)
        {
            
            TestAppointmentID = testAppointmentID;
            TestTypeID = testTypeID;
            LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            AppointmentDate = appointmentDate;
            PaidFees = paidFees;
            CreatedByUserID = createdByUserID;
            IsLocked = isLocked;
            RetakeTestApplicationID = retakeTestApplicationID;

            _Mode = clsGloabalBusiness.enMode.update;
        }

        public static clsTestAppointment Find(int TestAppointmentID )
        {
            int TestTypeID = -1, LocalDrivingLicenseApplicationID = -1;
            DateTime AppointmentDate = DateTime.Now;
            float PaidFees = -1;
            int CreatedByUserID = -1;
            bool IsLocked = false;

            int RetakeTestApplicationID = -1;


            if (clsTestAppointmentsData.GetTestAppointmentByID(TestAppointmentID, ref TestTypeID, ref LocalDrivingLicenseApplicationID ,
               ref AppointmentDate ,ref PaidFees,ref CreatedByUserID, ref IsLocked , ref RetakeTestApplicationID) )
            {
                return new clsTestAppointment( TestAppointmentID,TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate,
                  PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);
            }

         
            return null;
        
        }

        public static DataTable GetAlltTestAppointments(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            return clsTestAppointmentsData.GetAllTestAppointmentsByLDLandTestType(LocalDrivingLicenseApplicationID, TestTypeID);
        }

        public static DataTable GetAlltTestAppointments(int LocalDrivingLicenseApplicationID)
        {
            return clsTestAppointmentsData.GetAllTestAppointmentsByLDL(LocalDrivingLicenseApplicationID);
        }

        public static string errorMessage = "";


        private bool _AddNewTestAppointment()
        {
            errorMessage = "";
            bool Iserror=false; 


            this.TestAppointmentID = clsTestAppointmentsData.AddNewTestAppointment(
                ref errorMessage,this.TestTypeID,
                this.LocalDrivingLicenseApplicationID,
                this.AppointmentDate,
                this.PaidFees,
                this.CreatedByUserID,
                this.IsLocked,
                this.RetakeTestApplicationID
            );

            if (Iserror) {

                errorMessage = "There is an exception error";
            
            }

            return (this.TestAppointmentID > 0);
        }


        private bool _UpdateTestAppointment()
        {
            return clsTestAppointmentsData.UpdateTestAppointments(this.TestAppointmentID,this.TestTypeID,
                this.LocalDrivingLicenseApplicationID,this.AppointmentDate,this.PaidFees,this.CreatedByUserID,this.IsLocked,this.RetakeTestApplicationID);
        }

        public bool Save()
        {
            clsGloabalBusiness.AddNewAction addNewTestAppointment = _AddNewTestAppointment;
            clsGloabalBusiness.UpdateAction updateTestAppointment = _UpdateTestAppointment;
            return clsGloabalBusiness.Save(_Mode, addNewTestAppointment, updateTestAppointment);
        }

    }
}
