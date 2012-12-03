using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Utilities.GCSV;

namespace Military.IO
{
    /// <summary>
    /// Provides access to IO operations for reading/writing Military objects in xml format.
    /// </summary>
    public static class MilitaryIO
    {
        static Dictionary<string, IGCSVHeader> s_headers;

        public static Dictionary<string, IGCSVHeader> Headers
        {
            get { return s_headers; }
            set { s_headers = value; Writer = new MilitaryWriter(s_headers); }
        }

        /// <summary>
        /// A global MilitaryWriter
        /// </summary>
        public static MilitaryWriter Writer { get; private set; }

        /// <summary>
        /// A global MilitaryReader
        /// </summary>
        public static readonly MilitaryReader Reader = new MilitaryReader();
    }
}
