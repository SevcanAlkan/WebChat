using System;
using System.Collections.Generic;
using System.Text;

namespace NGA.Core
{
    public static class AppStatusCode
    {

        /* //-SAMPLE-// 

          /// <summary>
          /// Desc
          /// </summary>
          public const string ERR01001 = "ERR01001";

          */

        /*
        -----------------------------------------------
        ERR - Error Codes
            01 - General
        WRG - Warning Codes
            01 - General
        INF - Information Codes
            01 - General
        -----------------------------------------------          
        */


        #region ERR

        /// <summary>
        /// General Error Code
        /// </summary>
        public const string ERR01001 = "ERR01001";

        #endregion

        #region WRG

        /// <summary>
        /// Record not found on this ID
        /// </summary>
        public const string WRG01001 = "WRG01001";

        /// <summary>
        /// Invalid parameter Or parameters
        /// </summary>
        public const string WRG01002 = "WRG01002";

        /// <summary>
        /// User not found
        /// </summary>
        public const string WRG01003 = "WRG01003";

        #endregion

        #region INF


        #endregion
    }
}
