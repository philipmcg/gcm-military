using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;
using Utilities.GCSV;

namespace Military.IO
{
    /// <summary>
    /// Wrapper for XmlTextWriter.  This wrapper provides access to GCSV headers 
    /// that the data being written will need to know, and records which headers
    /// were used so they can be written at the end of the file.
    /// </summary>
    public class MilitaryXmlWriter : XmlTextWriter
    {
        Dictionary<string, IGCSVHeader> m_headers;
        Dictionary<string, IGCSVHeader> m_usedHeaders;
        public MilitaryWriterMode Mode { get; private set; }

        /// <summary>
        /// Creates an instance of the MilitaryXmlWriter class using the specified file and GCSV headers.
        /// </summary>
        /// <param name="path">The filename to write to.</param>
        /// <param name="headers">The GCSV headers to use for writing data elements</param>
        public MilitaryXmlWriter(string path, Dictionary<string, IGCSVHeader> headers, MilitaryWriterMode mode) : base(path, Encoding.UTF8)
        {
            m_headers = headers;
            m_usedHeaders = new Dictionary<string, IGCSVHeader>();
            Mode = mode;
        }

        /// <summary>
        /// Get the GCSV header this writer is using for this data element.
        /// </summary>
        public IGCSVHeader GetHeader(string name)
        {
            if (!m_usedHeaders.ContainsKey(name))
                m_usedHeaders[name] = m_headers[name];
            return m_headers[name];
        }

        /// <summary>
        /// Writes the GCSV headers as xml inside <head></head> tags.
        /// </summary>
        public void WriteUsedHeaders()
        {
            base.WriteStartElement("head");
            foreach (var header in m_usedHeaders)
            {
                base.WriteElementString("h" + header.Key, header.Value.ToString());
            }
            base.WriteEndElement();
        }
    }

    /// <summary>
    /// Implements IMilitaryReader, providing functionality for writing
    /// a MilitaryGroup to an xml file.
    /// </summary>
    public class MilitaryWriter : AbstractMilitary.IMilitaryWriter<Organization, MilitaryGroup>
    {
        Dictionary<string, IGCSVHeader> m_headers;
        MilitaryWriterMode m_mode;
        Action<MilitaryGroup> m_hook;

        /// <summary>
        /// Creates an instance of MilitaryWriter using the given GCSV headers.
        /// </summary>
        /// <param name="headers">The GCSVHeaders to be used</param>
        public MilitaryWriter(Dictionary<string, IGCSVHeader> headers)
        {
            m_headers = headers;
            m_mode = MilitaryWriterMode.Default;
        }

        /// <summary>
        /// Creates an instance of MilitaryWriter using the given GCSV headers and mode.
        /// </summary>
        /// <param name="headers">The GCSVHeaders to be used</param>
        public MilitaryWriter(Dictionary<string, IGCSVHeader> headers, MilitaryWriterMode mode, Action<MilitaryGroup> hook = null)
        {
            m_headers = headers;
            m_mode = mode;
            m_hook = hook;
        }

        /// <summary>
        /// Writes the MilitaryGroup to a file in xml format.
        /// </summary>
        public void WriteToFile(string path, MilitaryGroup military)
        {
            if (m_hook != null)
                m_hook(military);

            // Ensure that all units have IDs set.
            foreach (var unit in military.Organizations.SelectMany(o => o.AllUnits))
            {
                if (unit.Data.Id == 0)
                    throw new Exception("Unit has no ID: " + unit.Data.Name);
            }

            foreach (var cdr in military.Organizations.SelectMany(o => o.AllCommanders))
            {
                if (cdr.Data.Id == 0)
                    throw new Exception("Commander has no ID: " + cdr.Data.LastName);
            }

            using (MilitaryXmlWriter writer = new MilitaryXmlWriter(path, m_headers, m_mode))
            {
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 0;
                writer.IndentChar = ' ';

                writer.WriteStartElement("xml");
                writer.WriteStartElement("mil");
                foreach (var organization in military.Organizations)
                {
                    organization.XmlWrite(writer);
                }
                writer.WriteEndElement();
                writer.WriteUsedHeaders();
                writer.WriteEndElement();
                writer.Close();
            }
        }

    }

    public class MilitaryWriterMode
    {
        public Func<Unit, IMilitaryData[]> GetUnitData;
        public Func<Commander, IMilitaryData[]> GetCommanderData;
        public Func<Organization, IMilitaryData[]> GetOrganizationData;

        public static readonly MilitaryWriterMode Default = new MilitaryWriterMode()
        {
            GetUnitData = u => new IMilitaryData[] { u.Data },
            GetCommanderData = c => new IMilitaryData[] { c.Data },
            GetOrganizationData = o => new IMilitaryData[] { o.Data },
        };

        public static readonly MilitaryWriterMode BattleResults = new MilitaryWriterMode()
        {
            GetUnitData = u => new IMilitaryData[] { u.Data, u.BattleResultData, u.PreviousCommanderData },
            GetCommanderData = c => new IMilitaryData[] { c.Data, c.BattleResultData },
            GetOrganizationData = o => new IMilitaryData[] { o.Data, o.BattleResultData, o.PreviousCommanderData },
        };

        public static readonly MilitaryWriterMode TurnReports = new MilitaryWriterMode()
        {
            GetUnitData = u => new IMilitaryData[] { u.Data, u.TurnData },
            GetCommanderData = c => new IMilitaryData[] { c.Data, c.TurnData },
            GetOrganizationData = o => new IMilitaryData[] { o.Data, o.TurnData },
        };
    }
}
