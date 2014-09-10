using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoCurrent.IO.Enums
{
    /// <summary>
    /// SpectrumExtension holds the Extensions and Filters
    /// for all of the different types of Spectrum.
    /// </summary>
    public class SpectrumExtension
    {
        #region Const Members
        public const String RawExt = ".RAW";
        public const String RawFilter = "RAW Photocurrent Files (*.RAW)|*.RAW";
        public const String IpceExt = ".CSV";
        public const String IpceFilter = "IPCE Files (*.CSV)|*.CSV";
        public const String BkgExt = ".BKG";
        public const String BkgFilter = "Background Files (*.BKG)|*.BKG";
        public const String TxtExt = ".TXT";
        public const String TxtFilter = "3D Photocurrent Files (*.TXT)|*.TXT";
        public const String Raw3DExt = ".R3D";
        public const String Raw3DFilter = "3D RAW Photocurrent Files (*.R3D)|*.R3D";
        public const String Ipce3DExt = ".I3D";
        public const String Ipce3DFilter = "3D IPCE Photocurrent Files (*.I3D)|*.I3D";
        #endregion


        #region ToString Overrides
        /// <summary>
        /// The default ToString returns the complete list of File Types
        /// and Extensions.
        /// </summary>
        /// <returns>FileDialog Filter String</returns>
        public static new String ToString()
        {
            //Format a string with all of the Extensions
            return SpectrumExtension.ToString(SpectrumType.CSV2D, SpectrumType.CSV3D, SpectrumType.TAB3D);
        }


        /// <summary>
        /// This override of ToString returns only the relevant 
        /// Filter strings
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static String ToString(params SpectrumType[] types)
        {
            //Declare a string to return
            String rtn = String.Empty;

            //Iterate through the parameter list and 
            //add up the Filters
            foreach (SpectrumType type in types)
            {
                //Get the Filters
                List<String> refExt = new List<String>();
                List<String> refFilter = new List<String>();
                SpectrumExtension.TranslateType(type, ref refExt, ref refFilter);

                //Add the Filters if anything matched
                foreach (String currFilter in refFilter)
                {
                    //If there are any filters applied yet, add the separator
                    if (rtn != String.Empty)
                        rtn += "|";

                    //Add the current Filter
                    rtn += currFilter;
                }
            }

            //Return the result
            return rtn;
        }


        /// <summary>
        /// ToExtension changes the SpectrumType into a file extension.
        /// </summary>
        /// <param name="type">The SpectrumType of the file</param>
        /// <returns>String Extension of the desired file</returns>
        public static String ToExtension(SpectrumType type)
        {
            //Declare a variable to return
            String rtn = String.Empty;

            //Get the Filter
            List<String> refExt = new List<String>();
            List<String> refFilter = new List<String>();
            SpectrumExtension.TranslateType(type, ref refExt, ref refFilter);

            //Return the result if anything matched
            if (refExt.Count > 0)
                rtn = refExt[0];

            //Return the result
            return rtn;
        }


        /// <summary>
        /// ToType translates from a File Extension to a SpectrumType.
        /// </summary>
        /// <param name="fileExt">The Extension to translate</param>
        /// <returns>The SpectrumType of the Extension</returns>
        public static SpectrumType ToType(String fileExt)
        {
            return SpectrumExtension.TranslateExtFilter(fileExt, String.Empty);
        }
        #endregion


        #region Translation Methods
        /// <summary>
        /// TranslateType takes a SpectrumType and fills it with
        /// the appropriate Extension and Filter.
        /// </summary>
        /// <param name="type">The SpectrumType of the file to translate</param>
        /// <param name="refExt">The ref parameter to the Extension</param>
        /// <param name="refFilter">The ref parameter to the Filter</param>
        private static void TranslateType(SpectrumType type, ref List<String> refExt, ref List<String> refFilter)
        {
            //Clear the values on the ref parameters
            refExt.Clear();
            refFilter.Clear();

            //Add the desired filters
            if ((type & SpectrumType.TWOD) > 0)
            {
                if ((type & SpectrumType.RAW) > 0 || (type & SpectrumType.Time) > 0 || (type & SpectrumType.Wavelength) > 0)
                {
                    refExt.Add(SpectrumExtension.RawExt);
                    refFilter.Add(SpectrumExtension.RawFilter);
                }

                if ((type & SpectrumType.IPCE) > 0)
                {
                    refExt.Add(SpectrumExtension.IpceExt);
                    refFilter.Add(SpectrumExtension.IpceFilter);
                }

                if ((type & SpectrumType.BKG) > 0)
                {
                    refExt.Add(SpectrumExtension.BkgExt);
                    refFilter.Add(SpectrumExtension.BkgFilter);
                }
            }
            else if ((type & SpectrumType.THREED) > 0)
            {
                if ((type & SpectrumType.RAW) > 0)
                {
                    refExt.Add(SpectrumExtension.Raw3DExt);
                    refFilter.Add(SpectrumExtension.Raw3DFilter);
                }

                if ((type & SpectrumType.IPCE) > 0)
                {
                    refExt.Add(SpectrumExtension.Ipce3DExt);
                    refFilter.Add(SpectrumExtension.Ipce3DFilter);
                }

                if ((type & SpectrumType.TABFILE) > 0)
                {
                    refExt.Add(SpectrumExtension.TxtExt);
                    refFilter.Add(SpectrumExtension.TxtFilter);
                }
            }
        }


        /// <summary>
        /// TranslateExtension turns a file extension into a SpectrumType
        /// </summary>
        /// <param name="fileExt">The Extension or Filter to translate</param>
        /// <returns>The SpectrumType of the file</returns>
        private static SpectrumType TranslateExtFilter(String fileExt, String fileFilter)
        {
            //Declare a variable to return 
            SpectrumType rtn = SpectrumType.Invalid;

            //Make the Strings uppercase
            String ucExt = fileExt.ToUpper();
            String ucFilter = fileFilter.ToUpper();

            //Choose the SpectrumType based on the Strings
            if (ucExt == SpectrumExtension.BkgExt.ToUpper() || ucFilter == SpectrumExtension.BkgFilter.ToUpper())
                rtn = SpectrumType.TWOD | SpectrumType.CSVFILE | SpectrumType.BKG;
            else if (ucExt == SpectrumExtension.IpceExt.ToUpper() || ucFilter == SpectrumExtension.IpceFilter.ToUpper())
                rtn = SpectrumType.TWOD | SpectrumType.CSVFILE | SpectrumType.IPCE;
            else if (ucExt == SpectrumExtension.RawExt.ToUpper() || ucFilter == SpectrumExtension.RawFilter.ToUpper())
                rtn = SpectrumType.TWOD | SpectrumType.CSVFILE | SpectrumType.RAW;
            else if (ucExt == SpectrumExtension.Raw3DExt.ToUpper() || ucFilter == SpectrumExtension.Raw3DFilter.ToUpper())
                rtn = SpectrumType.THREED | SpectrumType.CSVFILE | SpectrumType.RAW;
            else if (ucExt == SpectrumExtension.Ipce3DExt.ToUpper() || ucFilter == SpectrumExtension.Ipce3DFilter.ToUpper())
                rtn = SpectrumType.THREED | SpectrumType.CSVFILE | SpectrumType.IPCE;
            else if (ucExt == SpectrumExtension.TxtExt.ToUpper() || ucFilter == SpectrumExtension.TxtFilter.ToUpper())
                rtn = SpectrumType.TAB3D;

            //Return the result
            return rtn;
        }
        #endregion
    }
}
