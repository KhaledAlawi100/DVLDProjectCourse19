using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Layer
{
    public class clsUtility
    {

        public enum enMode { add = 1, update = 2 };

      
        public static bool Save(enMode mode, Func<bool> AddNew, Func<bool> Update)
        {
            switch (mode)
            {
                case enMode.add:
                    mode = enMode.update;
                    if (AddNew != null)
                    {
                        if (AddNew())
                        {
                            mode = enMode.update;
                            return true;
                        }
                        else return false;
                    }
                    else return false;
                case enMode.update:
                    if (Update != null)
                    {
                        return Update();
                    }
                    else
                        return false;

            }
            return false;

        }
    }
}
