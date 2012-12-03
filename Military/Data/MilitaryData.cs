using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Utilities.GCSV;

using Military.IO;

namespace Military
{
    public interface IMilitaryData : IXmlWriteable, IGCSVWriteable
    {
        void Load(IGCSVLine line);
    }
}
