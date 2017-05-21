using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeatServer.Managers
{
    public class Utils
    {
        public static List<int> CommaSeparatedStringToIntList(string list)
        {
            return list.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
        }
    }
}