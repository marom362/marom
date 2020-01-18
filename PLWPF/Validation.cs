using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PLWPF
{
    public static class Validation
    {
        public static bool IsValideID(int id)
        {
            if (id >= 1000000000)
                return false;
            int i;
            int sum = 0;
            for (i = 0; i < 9; i++)
            {
                int num1;
                int num2 = 2 - (i + 1) % 2;
                if (id < 1)
                    num1 = 0;
                else
                {
                    num1 = id % 10;
                    id = id / 10;
                }
                num1 = num1 * num2;
                num1 = num1 / 10 + num1 % 10;
                sum += num1;
            }
            return sum % 10 == 0;
        }
        public static bool EmailIsValid(string email)
        {
            string expression = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";

            if (Regex.IsMatch(email, expression))
            {
                if (Regex.Replace(email, expression, string.Empty).Length == 0)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool IsValidePhoneNumber(string number)
        {
            if (number.Length != 9)
                return false;
            try
            {
                int.Parse(number);
            }
            catch (FormatException)
            {
                return false;
            }
            return true;
        }

    }
}
