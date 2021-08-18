using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPe.ExtendedProperties
{
    /// <summary>
    /// Defines the MAPI type of an extended property.
    /// </summary>
    public enum MapiPropertyType
    {
        /// <summary>
        /// The property is of type ApplicationTime.
        /// </summary>
        ApplicationTime = 0x7,

        /// <summary>
        /// The property is of type ApplicationTimeArray.
        /// </summary>
        ApplicationTimeArray = 0x1007,

        /// <summary>
        /// The property is of type Binary.
        /// </summary>
        Binary = 0x102,

        /// <summary>
        /// The property is of type BinaryArray.
        /// </summary>
        BinaryArray = 0x1102,

        /// <summary>
        /// The property is of type Boolean.
        /// </summary>
        Boolean = 0x0B,

        /// <summary>
        /// The property is of type CLSID.
        /// </summary>
        CLSID = 0x48,

        /// <summary>
        /// The property is of type CLSIDArray.
        /// </summary>
        CLSIDArray = 0x1048,

        /// <summary>
        /// The property is of type Currency.
        /// </summary>
        Currency = 0x6,

        /// <summary>
        /// The property is of type CurrencyArray.
        /// </summary>
        CurrencyArray = 0x1006,

        /// <summary>
        /// The property is of type Double.
        /// </summary>
        Double = 0x5,

        /// <summary>
        /// The property is of type DoubleArray.
        /// </summary>
        DoubleArray = 0x1005,

        /// <summary>
        /// The property is of type Error.
        /// </summary>
        Error = 0x0A,

        /// <summary>
        /// The property is of type Float.
        /// </summary>
        Float = 0x4,

        /// <summary>
        /// The property is of type FloatArray.
        /// </summary>
        FloatArray = 0x1004,

        /// <summary>
        /// The property is of type Integer.
        /// </summary>
        Integer = 0x3,

        /// <summary>
        /// The property is of type IntegerArray.
        /// </summary>
        IntegerArray = 0x1003,

        /// <summary>
        /// The property is of type Long.
        /// </summary>
        Long = 0x14,

        /// <summary>
        /// The property is of type LongArray.
        /// </summary>
        LongArray = 0x1014,

        /// <summary>
        /// The property is of type Null.
        /// </summary>
        Null = 0x0,

        /// <summary>
        /// The property is of type Object.
        /// </summary>
        Object = 0x0D,

        /// <summary>
        /// The property is of type ObjectArray.
        /// </summary>
        ObjectArray = 0x100D,

        /// <summary>
        /// The property is of type Short.
        /// </summary>
        Short = 0x2,

        /// <summary>
        /// The property is of type ShortArray.
        /// </summary>
        ShortArray = 0x1002,

        /// <summary>
        /// The property is of type SystemTime.
        /// </summary>
        SystemTime = 0x40,

        /// <summary>
        /// The property is of type SystemTimeArray.
        /// </summary>
        SystemTimeArray = 0x1040,

        /// <summary>
        /// The property is of type String.
        /// </summary>
        String = 0x1F,

        /// <summary>
        /// The property is of type StringArray.
        /// </summary>
        StringArray = 0x101F
    }
}
