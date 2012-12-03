using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Military.IO
{
    /// <summary>
    /// Represents an object whose contents can be saved as a GCSV line.
    /// </summary>
    public interface IGCSVWriteable
    {
        Utilities.GCSV.IGCSVLine SaveAsGCSV(Utilities.GCSV.IGCSVHeader header);
    }
}
