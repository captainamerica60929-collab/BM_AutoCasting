using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_App.Common
{
    public class StateInfo
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }

        public StateInfo(string code, string name, string shortName)
        {
            Code = code;
            Name = name;
            ShortName = shortName;
        }

        
    }
    public static class IndianStates
    {
        public static readonly List<StateInfo> states = new List<StateInfo>
{
    new StateInfo("01", "Jammu and Kashmir", "JK"),
    new StateInfo("02", "Himachal Pradesh", "HP"),
    new StateInfo("03", "Punjab", "PB"),
    new StateInfo("04", "Chandigarh", "CH"),
    new StateInfo("05", "Uttarakhand", "UK"),
    new StateInfo("06", "Haryana", "HR"),
    new StateInfo("07", "Delhi", "DL"),
    new StateInfo("08", "Rajasthan", "RJ"),
    new StateInfo("09", "Uttar Pradesh", "UP"),
    new StateInfo("10", "Bihar", "BR"),
    new StateInfo("11", "Sikkim", "SK"),
    new StateInfo("12", "Arunachal Pradesh", "AR"),
    new StateInfo("13", "Nagaland", "NL"),
    new StateInfo("14", "Manipur", "MN"),
    new StateInfo("15", "Mizoram", "MZ"),
    new StateInfo("16", "Tripura", "TR"),
    new StateInfo("17", "Meghalaya", "ML"),
    new StateInfo("18", "Assam", "AS"),
    new StateInfo("19", "West Bengal", "WB"),
    new StateInfo("20", "Jharkhand", "JH"),
    new StateInfo("21", "Odisha", "OD"),
    new StateInfo("22", "Chhattisgarh", "CT"),
    new StateInfo("23", "Madhya Pradesh", "MP"),
    new StateInfo("24", "Gujarat", "GJ"),
    new StateInfo("26", "Dadra & Nagar Haveli and Daman & Diu", "DH"),
    new StateInfo("27", "Maharashtra", "MH"),
    new StateInfo("29", "Karnataka", "KA"),
    new StateInfo("30", "Goa", "GA"),
    new StateInfo("31", "Lakshadweep", "LD"),
    new StateInfo("32", "Kerala", "KL"),
    new StateInfo("33", "Tamil Nadu", "TN"),
    new StateInfo("34", "Puducherry", "PY"),
    new StateInfo("35", "Andaman & Nicobar Islands", "AN"),
    new StateInfo("36", "Telangana", "TS"),
    new StateInfo("37", "Andhra Pradesh", "AP"),
    new StateInfo("38", "Ladakh", "LA")
};
    }
}
