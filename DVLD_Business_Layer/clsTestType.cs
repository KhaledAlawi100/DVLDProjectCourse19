using DVLD_Data_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Layer
{
    public class clsTestType
    {

        public int TestTypeID {  get; set; }
        public string TestTypeTitle { get; set; }

        public string TestTypeDescription { get; set; }

        public float TestTypeFees {  get; set; }

        public clsTestType() { 
        
        }    

        private clsTestType(int testTypeID, string testTypeTitle, string testTypeDescription, float testTypeFees)
        {
            TestTypeID = testTypeID;
            TestTypeTitle = testTypeTitle;
            TestTypeDescription = testTypeDescription;
            TestTypeFees = testTypeFees;
        }

        public static clsTestType Find(int TestTypeID) {

            string TestTypeTitle = "", TestTypeDescription = "";
            float TestTypeFees = -1;

            if(clsTestTypesData.GetTestTypeByID(TestTypeID, ref TestTypeTitle, ref TestTypeDescription,ref TestTypeFees))
            {
                return new clsTestType(TestTypeID, TestTypeTitle, TestTypeDescription, TestTypeFees);
            }

            return null;


        }
        public static DataTable GetAllTest()
        {
            return clsTestTypesData.GetAllTestTypes();
        }

        public static bool UpdateTestType(int ID , string title , string description,float fees)
        {
            return clsTestTypesData.UpdateTestType(ID , title , description, fees);
        }
    }
}
