using System;

namespace PhotoCurrent.IO.Enums
{
    public enum SpectrumType
    {
        //Base Spectrum Types
        Invalid = 0,
        IPCE = 1,
        RAW = 2,
        BKG = 4,

        //RAW Data Types
        Wavelength = 8,
        Time = 16,

        //File Types
        CSVFILE = 32,
        TABFILE = 64,

        //Spectrum Dimension
        TWOD = 128,
        THREED = 256,

        //Compound Spectrum Types
        CSV2D = TWOD | CSVFILE | IPCE | RAW | BKG,
        CSV3D = THREED | CSVFILE | IPCE | RAW,
        TAB3D = THREED | TABFILE
    }
}
