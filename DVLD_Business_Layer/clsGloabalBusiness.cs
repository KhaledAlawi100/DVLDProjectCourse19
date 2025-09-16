using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Layer
{
    public class clsGloabalBusiness
    {

        public enum enMode { add = 1, update = 2 };

        public  delegate bool AddNewAction();
        public  delegate bool UpdateAction();

        public static bool Save(enMode mode, AddNewAction AddNew, UpdateAction Update)
        {
            switch (mode)
            {
                case enMode.add:
                    mode = enMode.update;
                    if (AddNew())
                        return true;
                    else return false;
                case enMode.update:
                    return Update();

            }
            return false;

        }
    }
}
