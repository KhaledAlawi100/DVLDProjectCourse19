using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_Data_Layer;

namespace DVLD_Business_Layer
{
    public class clsPerson
    {
        //public enum enMode {AddNew=0 , Update=1 };

        clsUtility.enMode mode = clsUtility.enMode.add;

        public int PersonID { get; set; }

        public string NationalNumber { get; set; }

        public string FirstName {  get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public byte Gender {  get; set; }

        public string Address {  get; set; }

        public string Phone {  get; set; }

        public string Email { get; set; }

        public int NationalityCountryID { get; set; }

       public string ImagePath { get; set; }

        public clsPerson() { 

            this.PersonID = -1;
            this.NationalNumber = "";
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";

            this.DateOfBirth = DateTime.Now;

            this.Gender = 0;

            this.Address = "";

            this.Phone = "";

            this.Email = "";

            this.NationalityCountryID = -1;

            this.ImagePath = "";

            //this.mode = enMode.AddNew;

            this.mode = clsUtility.enMode.add;



           

        
        }

        private clsPerson(int ID , string NationalNumber,string FirstName , string SecondName , string ThirdName,string LastName, 
            DateTime DateOfBirth , byte Gender, string Address , string Phone , string Email , int NationalityCountryID , string ImagePath)
        {
            this.PersonID =ID;
            this.NationalNumber = NationalNumber;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Gender = Gender;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.NationalityCountryID = NationalityCountryID;
            this.ImagePath = ImagePath;

            //this.mode = enMode.Update;
            this.mode = clsUtility.enMode.update;

        }

        public static clsPerson Find(int ID) {

            string NationalNumber = "", FirstName = "", SecondName = "", ThirdName = "", LastName = "", Address = "", Phone = "", Email = "", ImagePath = "";

            int NationalityCountryID = -1;
            byte gender = 0; 
            DateTime DateOfBirth = DateTime.Now;



            if (clsPeopleDataAccess.GetPersonInfoByID(ID , ref NationalNumber , ref FirstName , ref SecondName , ref ThirdName , 
                ref LastName , ref DateOfBirth , ref gender , ref Address , ref Phone , ref Email ,ref NationalityCountryID , ref ImagePath) ) {

                return new clsPerson(ID , NationalNumber , FirstName , SecondName , ThirdName , LastName , DateOfBirth , gender, Address , Phone , Email , NationalityCountryID , ImagePath);

            }
            return null;
        
        }
        
        public static clsPerson Find(string NationalNo)
        {
            string  FirstName = "", SecondName = "", ThirdName = "", LastName = "", Address = "", Phone = "", Email = "", ImagePath = "";

            int NationalityCountryID = -1,ID=-1;
            byte gender = 0;
            DateTime DateOfBirth = DateTime.Now;

            if (clsPeopleDataAccess.GetPersonInfoByNationalNo(NationalNo,ref ID , ref FirstName , ref SecondName,ref ThirdName,
                ref LastName,ref DateOfBirth,ref gender,ref Address,ref Phone,ref Email,ref NationalityCountryID,ref ImagePath))
            {
                return new clsPerson(ID, NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, gender, Address, Phone, Email, NationalityCountryID, ImagePath);
            }


            return null;
        
        }  


        public static DataTable GetAllPeople()
        {
            return clsPeopleDataAccess.GetAllPeople();
        }

        public static bool IsPersonExists(string NationalNO) { 
        
            return clsPeopleDataAccess.isPersonExist(NationalNO);
        }

        private bool _AddNewPerson()
        {
            this.PersonID = clsPeopleDataAccess.AddNewPerson(this.NationalNumber,this.FirstName,
                this.SecondName,this.ThirdName,this.LastName,this.DateOfBirth,this.Gender,this.Address,
                this.Phone,this.Email,this.NationalityCountryID,this.ImagePath);

            return this.PersonID != -1;
        }

        private bool _UpdatePerson()
        {
            return clsPeopleDataAccess.UpdatePerson(this.PersonID,this.NationalNumber,this.FirstName,this.SecondName,
                this.ThirdName,this.LastName, this.DateOfBirth,this.Gender, this.Address,this.Phone,this.Email,
                this.NationalityCountryID, this.ImagePath);
        }


        public bool Save()
        {
            //clsUtility.AddNewAction addNew = _AddNewPerson;

            //clsUtility.UpdateAction updatePerson = _UpdatePerson;

            return clsUtility.Save(this.mode, _AddNewPerson, _UpdatePerson);

            //switch (mode)
            //{
            //    case enMode.AddNew:
            //        if (_AddNewPerson()) {
            //            mode = enMode.Update;
            //            return true;
            //        }
            //        else
            //            { return false; }

            //        case enMode.Update:
            //        return _UpdatePerson();
            //}
            //return false
            //

        }

        public static bool DeletePerson(int PersonID,ref int exceptionNumber)
        {
            return clsPeopleDataAccess.DeletePerson(PersonID,ref exceptionNumber);  
        }

        

    }
}
