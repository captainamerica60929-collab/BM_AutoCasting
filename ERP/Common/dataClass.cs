using System;
using System.Collections.Generic;

using System.Text;

namespace Maintanence_Printing_Tool
{
    class dataClass
    {
        #region Private members
        private static string _printerName;
        #endregion

        #region Public Properties
        public static string printerName
        {
            get
            {
                return _printerName;
            }
            set
            {
                _printerName = value;
            }
        }
        #endregion

    }
}
