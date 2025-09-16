using DVLD_Data_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Layer
{
    public class clsDriver
    {


        public int DriverID { get; set; }

        public int PersonID {  get; set; }

        public int CreatedByUserID {  get; set; }
        public DateTime CreatedDate { get; set; }

        public clsDriver() { }

        private clsDriver(int driverID, int personID, int createdByUserID, DateTime createdDate)
        {
            DriverID = driverID;
            PersonID = personID;
            CreatedByUserID = createdByUserID;
            CreatedDate = createdDate;
        }

        public static clsDriver FindbyPersonID(int PersonID) {

            int DriverID = -1, CreatedByUserID = -1;

            DateTime CreatedDate = DateTime.Now;

            if(clsDriverData.GetDriverInfoByPersonID(PersonID,ref DriverID,ref CreatedByUserID,ref CreatedDate))
            {
                return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);
            }

            return null;


        }

        public static clsDriver Find(int DriverID)
        {

            int PersonID = -1, CreatedByUserID = -1;

            DateTime CreatedDate = DateTime.Now;

            if (clsDriverData.GetDriverInfoByDriverID(DriverID, ref PersonID, ref CreatedByUserID, ref CreatedDate))
            {
                return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);
            }

            return null;


        }

        public static DataTable GetAllDrivers()
        {
            return clsDriverData.GetAllDrivers();
        }

        public bool AddNewDriver()
        {
            this.DriverID=clsDriverData.AddNewDriver(this.PersonID,this.CreatedByUserID,this.CreatedDate);

            return this.DriverID != -1;
        }
    }
}
