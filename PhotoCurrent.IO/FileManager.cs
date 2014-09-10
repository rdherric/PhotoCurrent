using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using PhotoCurrent.IO.Enums;

namespace PhotoCurrent.IO
{
    /// <summary>
    /// FileManager takes an Array of File Paths and contains an Array
    /// of FileBase objects to be used by the Spectrum window.
    /// </summary>
    public class FileManager
    {
        #region Member Variables
        private List<SpectrumFile> _files = new List<SpectrumFile>();
        private SpectrumType _type = SpectrumType.Invalid;
        #endregion


        #region Add Spectrum Methods
        /// <summary>
        /// AddSpectrumFiles checks to make sure that the 
        /// files are all compatible, then parses and
        /// exposes the SpectrumFile objects for the Spectrum
        /// Window.
        /// </summary>
        /// <param name="filePaths">The Paths to the files to add</param>
        public void AddSpectrumFiles(String[] filePaths)
        {
            //Declare a List of files that don't get opened
            List<String> badPaths = new List<String>();

            //Iterate through the file paths and figure out
            //if they are all compatible
            foreach (String path in filePaths)
            {
                //Create a SpectrumFile for the File Path.  This 
                //will determine the complete Type of the file.
                SpectrumFile sf = new SpectrumFile(path);

                //If the Type of the FileManager is currently
                //blank, save the value of the first file in 
                //the member variable.  Otherwise, check to make
                //sure that the current Type is compatible with 
                //the Type that has already been set.
                if (this._type == SpectrumType.Invalid)
                {
                    //If this is a two-dimensional file, set the Type
                    //to the generic TWOD type.  Otherwise, set it
                    //to the generic THREED type.
                    if ((sf.Type & SpectrumType.TWOD) > 0)
                        this._type = SpectrumType.TWOD;
                    else
                        this._type = SpectrumType.THREED;
                }

                //If this is a two-D file, check to make sure that the
                //Data Type is valid with the rest of the SpectrumFiles.
                //Otherwise, this is a three-D file and no are other 
                //files already loaded may be loaded.
                if ((sf.Type & SpectrumType.TWOD) > 0)
                {
                    //If this is a RAW file, check the Data Type
                    if ((sf.Type & SpectrumType.RAW) > 0)
                    {
                        //Figure out the Data Type of the file
                        SpectrumType dataType = SpectrumType.Wavelength;
                        if ((sf.Type & SpectrumType.Time) > 0)
                            dataType = SpectrumType.Time;

                        //If the Data Type hasn't been set, set it
                        //and keep the file.  Otherwise, make sure
                        //that the Data Types match
                        if ((this._type & SpectrumType.Time) == 0 &&
                            (this._type & SpectrumType.Wavelength) == 0)
                        {
                            //Save the Data Type in the FileManager type
                            this._type |= dataType;
                        }
                        else
                        {
                            //If the Data Types don't match, ignore this file
                            if ((this._type & dataType) == 0)
                            {
                                //Add the path to the bad paths and continue
                                badPaths.Add(path);
                                continue;
                            }
                        }
                    }
                }
                else
                {
                    //Check for other files
                    if (this._files.Count > 0)
                    {
                        //Add the path to the bad paths and continue
                        badPaths.Add(path);
                        continue;
                    }
                }

                //If we got this far, then the SpectrumFile is
                //valid, so add it to the File List
                this._files.Add(sf);
            }

            //If there were any files that weren't imported, report them
            if (badPaths.Count > 0)
            {
                //Build the string
                String exc = "The following files were either of a different type or have already been imported:\n";

                //Add the File Paths
                foreach (String filePath in badPaths)
                    exc += filePath + "\n";

                //throw the Exception
                throw new ArgumentException(exc);
            }
        }


        /// <summary>
        /// AddNewSpectrum checks the type of the SpectrumFile
        /// and adds it to the List.
        /// </summary>
        /// <param name="sf"></param>
        public void AddNewSpectrum(SpectrumFile sf)
        {
            //Throw an Exception if the types don't match
            if (this._type != SpectrumType.Invalid && (this._type & sf.Type) == 0)
                throw new ArgumentException("Could not add New Spectrum because the types do not match.");

            //Add the SpectrumFile to the List
            this._files.Add(sf);
        }
        #endregion


        #region Public Properties
        /// <summary>
        /// SpectrumFiles returns the List so that the Spectrum
        /// window can iterate through them and add them to the
        /// the Chart.
        /// </summary>
        public List<SpectrumFile> SpectrumFiles
        {
            get { return this._files; }
        }


        /// <summary>
        /// FilePaths returns all of the paths for the files
        /// currently being managed.
        /// </summary>
        public List<String> FilePaths
        {
            get
            {
                //Declare a List to hold the FilePaths
                List<String> paths = new List<String>();

                //Iterate through the SpectrumFiles and get
                //the file paths
                foreach (SpectrumFile sf in this._files)
                    paths.Add(sf.FilePath);

                //Return the Array
                return paths;
            }
        }
        #endregion
    }
}
