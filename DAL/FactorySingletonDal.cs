using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class FactorySingletonDal
    {
        private static Idal instance = null;

        public static Idal GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Dal_imp();
                }
                return instance;
            }
        }
    }
}
