using DVLD_Data_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Layer
{
    public class clsUser
    {

        //public enum enMode { AddNew = 0, Update = 1 };

        clsUtility.enMode _Mode = clsUtility.enMode.add;
        public int UserID {  get; set; }

        public int PersonID {  get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public bool IsActive {  get; set; }

        

        public clsUser() { 
        
            IsActive = false;
            _Mode = clsUtility.enMode.add;
            UserID = -1;
            PersonID = -1;
            UserName ="";
            Password = "";


        }

        private clsUser(int userID, int personID, string userName,string password, bool isActive)
        {
            UserID = userID;
            PersonID = personID;
            UserName = userName;
            IsActive = isActive;
            this.Password = password;

            _Mode = clsUtility.enMode.update;
        }

        

        public static clsUser Find(int userID) {
            int PersonID = -1;
            bool IsActive = false;
            string UserName = "";
            string Password = "";

            if (clsUsersData.GetUserInfoByID(userID,ref PersonID,ref UserName ,ref Password, ref IsActive))
            {
                return new clsUser(userID,PersonID,UserName,Password,IsActive); 

            }
            return null;
        
        }

        string HashedPassword;

        public static string messageOfhashedPassword;
        private bool _AddNewUser()
        {

            this.HashedPassword = ComputeHash(this.Password);

            //messageOfhashedPassword = $"The original password is {this.Password} The Hash of the Password is:"+this.HashedPassword;


            this.UserID = clsUsersData.AddNewUser(this.PersonID,this.UserName,HashedPassword,this.IsActive);

            return (this.UserID != -1);
        }

        private bool _UpdateUser()
        {

            return clsUsersData.UpdateUser(this.UserID,this.PersonID,this.UserName ,this.HashedPassword,this.IsActive);

        }
        public static clsUser Find(string UserName)
        {
            int UserID = -1;
            int PersonID = -1;
            string Password = "";
            bool IsActive = false;

            if(clsUsersData.GetUserInfoByUserName(UserName,ref UserID,ref PersonID,ref Password,ref IsActive))
            {
                return new clsUser(UserID,PersonID,UserName,Password,IsActive);
            }

            return null;
        }

        public static DataTable GetAllUsers()
        {
            return clsUsersData.GetAllUsers();
        }

        public static bool isUserExists(string UserName)
        {

            return clsUsersData.isUserExist(UserName);
        }

        public static bool isUserExists(string UserName,string Password)
        {
            string HashedPassword = ComputeHash(Password);
            return clsUsersData.IsUserExist(UserName,HashedPassword);
        }

       

        public bool Save()
        {
            return clsUtility.Save(_Mode, _AddNewUser, _UpdateUser);
        }

        public static bool DeleteUser(int UserID,ref int ExceptionNumber) { 
        
            return clsUsersData.DeleteUser(UserID,ref ExceptionNumber);
        }

        static string ComputeHash(string input)
        {
            //SHA is Secutred Hash Algorithm.
            // Create an instance of the SHA-256 algorithm
            using (SHA256 sha256 = SHA256.Create())
            {
                // Compute the hash value from the UTF-8 encoded input string
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Convert the byte array to a lowercase hexadecimal string
                return BitConverter.ToString(hashBytes).Replace("-", "");
            }
        }


    }
}
