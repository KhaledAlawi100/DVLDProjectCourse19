using DVLD_Data_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Layer
{
    public class clsTest
    {
        public int TestID {  get; set; }

        public int TestAppointmentID { get; set; }

        public bool TestResult {  get; set; }

        public string Notes {  get; set; }

        public int CreatedByUserID {  get; set; }

        

        public clsTest() { 
        
        }

        private clsTest(int testID, int testAppointmentID, bool testResult, string notes, int createdByUserID)
        {
            TestID = testID;
            TestAppointmentID = testAppointmentID;
            TestResult = testResult;
            Notes = notes;
            CreatedByUserID = createdByUserID;
        }


        public static clsTest Find(int TestAppointmentID)
        {
            int TestID = -1;
            bool TestResult = false;
            string Notes = "";
            int CreatedByUserID = -1;

            if (clsTestsData.GetTestInfoByAppointmentID(TestAppointmentID,ref TestID,
                ref TestResult,ref Notes,ref CreatedByUserID))
            {
                return new clsTest(TestID, TestAppointmentID, TestResult, Notes, CreatedByUserID);

            }

            return null;

        }

        public bool AddNewTest()
        {
            this.TestID = clsTestsData.AddNewTest(this.TestAppointmentID,this.TestResult,this.Notes,this.CreatedByUserID);

            return this.TestID != -1;
        }
    }
}
