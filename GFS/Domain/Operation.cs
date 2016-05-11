using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GFS.Domain
{
    public class Operation
    {
        public string Gender(string id)
        {
            string gnd = id.Substring(6,1);
            int var = Convert.ToInt16(gnd);
            string gender = "";
            if(var>4)
            {
                gender = "Male";
            }
            else
            {
                gender = "Female";
            }
            return gender;
        }

        public int age(string id)
        {
            string yrr = id.Substring(0, 2);
            int var = Convert.ToInt16(yrr);
            string year = "";
            if (var <99)
            {
                year = "20"+yrr;
            }
            else
            {
                year = "19"+yrr;
            }
            int
             cyyr = (int)DateTime.Now.Year;
            int Age = cyyr - Convert.ToInt16(year);


            return Age;
        }
        public string DOB(string id)
        {
            return id.Substring(0, 5);
        }

    }
}