using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using PhotoCurrent.IO;

namespace PhotoCurrent.Scaling
{
    /// <summary>
    /// The IPCE class performs the scaling of Photocurrent
    /// data to IPCE, including complete RAW files.
    /// </summary>
    public class IPCE
    {
        #region Member variables
        private String _bkgPath = String.Empty;
        private UInt32 _wavelength = UInt32.MinValue;
        private Double _power = Double.MinValue;
        private Enums.PowerUnit _powerUnit = Enums.PowerUnit.Invalid;
        private Boolean _bkgSetup = false;

        //Dictionary of Background points -- used as a Lookup Table 
        //during the scaling of RAW files
        Dictionary<UInt32, Double> _lookup = new Dictionary<UInt32, Double>();

        //Constants for calculating IPCE
        private const Double _joulesPerNanometer = 1.98645E-16;
        private const Double _electronsPerAmpere = 6.242E18;
        #endregion


        #region Lookup Table Methods
        /// <summary>
        /// SetupBackground takes the input parameters and validates
        /// that all of the information is correct, then creates a 
        /// lookup table (Dictionary) from which scaling values can
        /// be taken.
        /// </summary>
        /// <param name="bkgPath">The Path to the BKG file used for scaling</param>
        /// <param name="scaleWavelength">The Wavelength that corresponds to the scaling Power</param>
        /// <param name="scalePower">The Power of the light at the Wavelength above</param>
        /// <param name="scalePowerUnit">The Units of the Power above</param>
        public void SetupBackground(String bkgPath, UInt32 scaleWavelength, Double scalePower, Enums.PowerUnit scalePowerUnit)
        {
            //If the file path is valid, open the file and parse it.
            //Otherwise, this could be a TXT file scale.
            SpectrumFile sf = null;
            Int32 scaleIndex = -1;
            if (bkgPath != String.Empty)
            {
                //Open and parse the file
                sf = new SpectrumFile(bkgPath);

                //Make sure the scaleWavelength is one of the XValues
                scaleIndex = sf.XValues.IndexOf(Convert.ToDouble(scaleWavelength));
                if (scaleIndex < 0)
                    throw new System.ArgumentException("Scaling Wavelength was not found in BKG File '" + bkgPath + "'.");
            }

            //Calculate the actual power specified
            Double realPower = scalePower * Enums.PowerExponent.UnitToDivisor(scalePowerUnit);

            //Calculate the photon energy from Wavelength
            Double photonEnergy = IPCE._joulesPerNanometer / Convert.ToDouble(scaleWavelength);

            //Calculate total photon flux as photons / sec
            Double photonsPerSec = realPower / photonEnergy;

            //Iterate through the background values and create a Dictionary 
            //with normalized power values
            if (sf != null)
            {
                for (Int32 i = 0; i < sf.XValues.Count; i++)
                    this._lookup[Convert.ToUInt32(sf.XValues[i])] = (sf.YValues[i] / sf.YValues[scaleIndex]) * photonsPerSec;
            }
            else
                this._lookup[scaleWavelength] = photonsPerSec;

            //Save the values in the member variables
            this._bkgPath = bkgPath;
            this._wavelength = scaleWavelength;
            this._power = scalePower;
            this._powerUnit = scalePowerUnit;

            //Set the flag to indicate that the Background
            //lookup table is ready to go
            this._bkgSetup = true;
        }
        #endregion


        #region Photocurrent to IPCE Scaling Methods
        /// <summary>
        /// ScaleFile takes a RAW or TXT PhotoCurrent file and scales it
        /// to IPCE in the output file path.  
        /// </summary>
        /// <param name="inputPath">The File to scale</param>
        /// <param name="outputPath">The File to write</param>
        /// <param name="overwrite">Boolean to determine if the file should be overwritten</param>
        public void ScaleFile(String inputPath, String outputPath, Boolean overwrite)
        {
            //If the Background hasn't been set up yet, throw an Exception
            if (this._bkgSetup == false)
                throw new System.InvalidOperationException("Cannot Scale Photocurrent File: Background lookup table has not been set up.");

            //If the file exists and the overwrite value is set to FALSE,
            //throw an Exception
            if (File.Exists(outputPath) == true && overwrite == false)
                throw new System.ArgumentException("File '" + outputPath + "' already exists and will not be overwritten.");

            //Setup a SpectrumFile for the RAW or TXT file
            SpectrumFile photocurrent = new SpectrumFile(inputPath);

            //Setup a SpectrumFile for the IPCE file
            SpectrumFile ipce = new SpectrumFile();
            ipce.Comment = photocurrent.Comment;
            ipce.DelaySeconds = photocurrent.DelaySeconds;
            ipce.EndWavelength = photocurrent.EndWavelength;
            ipce.FileCreateDate = photocurrent.FileCreateDate;
            ipce.ScalePower = this._power * Enums.PowerExponent.UnitToDivisor(this._powerUnit);
            ipce.ScaleWavelength = this._wavelength;
            ipce.StartWavelength = photocurrent.StartWavelength;
            ipce.WavelengthIncrement = photocurrent.WavelengthIncrement;

            //Determine the file Dimension and Type
            PhotoCurrent.IO.Enums.SpectrumType type = PhotoCurrent.IO.Enums.SpectrumType.IPCE;
            if ((photocurrent.Type & PhotoCurrent.IO.Enums.SpectrumType.TABFILE) > 0)
                type = PhotoCurrent.IO.Enums.SpectrumType.TAB3D;
            else
            {
                //Set the Dimension
                if ((photocurrent.Type & PhotoCurrent.IO.Enums.SpectrumType.TWOD) > 0)
                    type |= PhotoCurrent.IO.Enums.SpectrumType.TWOD;
                else
                    type |= PhotoCurrent.IO.Enums.SpectrumType.THREED;

                //Set the file Type
                type |= PhotoCurrent.IO.Enums.SpectrumType.CSVFILE;

                //Set the time or Wavelength type
                if ((photocurrent.Type & PhotoCurrent.IO.Enums.SpectrumType.Time) > 0)
                    type |= PhotoCurrent.IO.Enums.SpectrumType.Time;
                else
                    type |= PhotoCurrent.IO.Enums.SpectrumType.Wavelength;
            }

            //Set the Type
            ipce.Type = type;

            //Do the scaling based on the dimension of the file
            if ((ipce.Type & PhotoCurrent.IO.Enums.SpectrumType.TWOD) > 0)
            {
                //Create the Arrays in the SpectrumFile
                ipce.SetupXYArrays(photocurrent.XValues.Count);

                //Iterate through the Two-D file and scale to IPCE
                for (Int32 i = 0; i < photocurrent.XValues.Count; i++)
                {
                    //Figure out the wavelength used from the Type.  If this
                    //is a Time-based file, use the StartWavelength.
                    UInt32 wavelength = Convert.ToUInt32(photocurrent.XValues[i]);
                    if ((photocurrent.Type & PhotoCurrent.IO.Enums.SpectrumType.Time) > 0)
                        wavelength = photocurrent.StartWavelength;

                    //Add the new point to the SpectrumFile
                    ipce.XValues.Add(photocurrent.XValues[i]);
                    ipce.YValues.Add(this.ScalePhotocurrent(wavelength, photocurrent.YValues[i]));
                }
            }
            else
            {
                //Create the Double Arrays in the SpectrumFile
                ipce.Setup3DArray(photocurrent.ThreeDValues.Count, photocurrent.ThreeDValues[0].Count);

                //Iterate throught the file and scale to IPCE
                for (Int32 i = 0; i < photocurrent.ThreeDValues.Count; i++)
                {
                    //If the row doesn't exist yet, create it
                    if (ipce.ThreeDValues.Count <= i)
                        ipce.ThreeDValues.Add(new List<Double>());

                    for (Int32 j = 0; j < photocurrent.ThreeDValues[i].Count; j++)
                    {
                        //Add the new value
                        ipce.ThreeDValues[i].Add(this.ScalePhotocurrent(this._wavelength, photocurrent.ThreeDValues[i][j]));
                    }
                }
            }

            //Tell the SpectrumFile to save itself out to disk
            ipce.Save(outputPath, overwrite);
        }


        /// <summary>
        /// ScalePhotocurrent performs the lookup of the wavelength for
        /// photons / sec from the Lookup Table, then calculates the 
        /// IPCE for the given photocurrent.
        /// </summary>
        /// <param name="wavelength">The Wavelength at which the photocurrent was measured</param>
        /// <param name="photoCurrent">The photocurrent to scale -- must be in Ampereres</param>
        /// <returns></returns>
        public Double ScalePhotocurrent(UInt32 wavelength, Double photoCurrent)
        {
            //Try to lookup the Wavelength in the Lookup Table
            Double photons = -1;
            try
            {
                photons = this._lookup[wavelength];
            }
            catch
            {
                //Throw a new Exception with a better message
                throw new System.ArgumentException("Could not find Wavelength '" + wavelength.ToString() + "' in Lookup Table.");
            }

            //Calculate the number of electrons passed from the current
            Double electrons = photoCurrent * IPCE._electronsPerAmpere;

            //Return the IPCE
            return electrons / photons;
        }
        #endregion


        #region Public Properties
        /// <summary>
        /// BKGPath specifies the BKG file that will be used as a
        /// lookup table for the scaling.
        /// </summary>
        public String BKGPath
        {
            get { return this._bkgPath; }
        }


        /// <summary>
        /// ScaleWavelength is the base wavelength in nm for which
        /// the Power is defined.  It is used to create a relative
        /// power curve from the BKG data.
        /// </summary>
        public UInt32 ScaleWavelength
        {
            get { return this._wavelength; }
        }


        /// <summary>
        /// ScalePower is the floating-point representation of the
        /// value divided by ScalePowerUnit used as the base power
        /// of the ScaleWavelength.
        /// </summary>
        public Double ScalePower
        {
            get { return this._power; }
        }


        /// <summary>
        /// ScalePowerUnit is the Enum representation of the 
        /// divisor for the ScalePower.
        /// </summary>
        public Enums.PowerUnit ScalePowerUnit
        {
            get { return this._powerUnit; }
        }
        #endregion
    }
}
