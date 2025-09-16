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
    public class clsLicensClass
    {
       public int ClassID {  get; set; }
       public string ClassName {  get; set; }

       public string ClassDescribtion {  get; set; }

       public byte MinimumAge {  get; set; }

       public byte ValidityLength {  get; set; }

        public float Fees {  get; set; }

        public clsLicensClass()
        {
            
        }

        private clsLicensClass(int classID, string className, string classDescribtion, byte minimumAge, byte validityLength, float fees)
        {
            ClassID = classID;
            ClassName = className;
            ClassDescribtion = classDescribtion;
            MinimumAge = minimumAge;
            ValidityLength = validityLength;
            Fees = fees;
        }

        public static clsLicensClass Find(int ID)
        {
            string ClassName = "", ClassDescribtion = "";
            byte MinimumAge = 0, ValidityLength = 1;
            float Fees = -1;

            if(LicenseClassData.GetClassInfoByClassID(ID,ref ClassName ,ref ClassDescribtion, ref MinimumAge , 
                ref ValidityLength , ref Fees))
            {
                return new clsLicensClass(ID,ClassName,ClassDescribtion,MinimumAge,ValidityLength,Fees);

            }
            return null;
        }
        public static clsLicensClass Find(string ClassName)
        {
            int ClassID = -1; string ClassDescribtion = "";
            byte MinimumAge = 0, ValidityLength = 0;
            float Fees = 0;
            if (LicenseClassData.GetClassInfoByClassName(ClassName, ref ClassID, ref ClassDescribtion, ref MinimumAge,
                ref ValidityLength, ref Fees))
            {
                return new clsLicensClass(ClassID, ClassName, ClassDescribtion, MinimumAge, ValidityLength, Fees);

            }
            return null;
        }

        public static DataTable GetAllClassLicenses()
        {
            return LicenseClassData.GetAllLicenseClasses();
        }


    }
}
