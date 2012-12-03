using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;
using System.Xml.Linq;
using Utilities.GCSV;

namespace Military.IO
{

    /// <summary>
    /// Implements IMilitaryReader, providing functionality for reading
    /// a MilitaryGroup from an xml file.
    /// </summary>
    public class MilitaryReader : AbstractMilitary.IMilitaryReader<Organization, MilitaryGroup>
    {
        static readonly char s_delimiter = ',';

        /// <summary>
        /// Contains the headers from the current file being read.
        /// </summary>
        Dictionary<string, IGCSVHeader> Headers { get; set; }

        /// <summary>
        /// Creates a MilitaryGroup from the xml file at the given path.
        /// </summary>
        /// <param name="path">The path of the xml file to load</param>
        /// <returns>The MilitaryGroup object</returns>
        public MilitaryGroup ReadFromFile(string path)
        {
            XDocument doc = LoadXml(path);

            Headers = GetHeaders(doc);
            var mil = doc.Descendants().First(d => d.Name.LocalName == "mil");

            var organizations = mil.Children(Organization.XmlTag).Select(e => LoadOrganization(e)).ToList();

            Headers = null;

            return new MilitaryGroup(organizations);
        }

        /// <summary>
        /// Loads an XDocument from the given path.
        /// </summary>
        XDocument LoadXml(string path)
        {
            using (var reader = new XmlTextReader(path))
            {
                return XDocument.Load(reader);
            }
        }

        /// <summary>
        /// Gets the GCSV headers from this document.
        /// </summary>
        Dictionary<string, IGCSVHeader> GetHeaders(XDocument doc)
        {
            return doc.Descendants()
                .Where(e => e.Parent != null && e.Parent.Name == "head")
                .ToDictionary(e => e.Name.LocalName.Length == 3 ? e.Name.LocalName[2].ToString() : e.Name.LocalName.Substring(1), e => GCSVMain.CreateHeader(e.Value.Split(s_delimiter)));
        }


        /// <summary>
        /// Recursively loads an Organization from this XElement, and all child organizations.
        /// </summary>
        Organization LoadOrganization(XElement element)
        {
            Organization org = new Organization();
            org.Data = new OrganizationData(GetData(element));

            foreach (var child in element.Children(Commander.XmlTag))
            {
                org.SetCommander(LoadCommander(child));
            }

            foreach (var child in element.Children(Unit.XmlTag))
            {
                org.AddUnit(LoadUnit(child));
            }

            foreach (var child in element.Children(Organization.XmlTag))
            {
                org.AddOrganization(LoadOrganization(child));
            }

            org.OnLoad(this, element);

            return org;
        }

        /// <summary>
        /// Loads an Unit from this XElement
        /// </summary>
        Unit LoadUnit(XElement element)
        {
            Unit unit = new Unit();
            unit.Data = new UnitData(GetData(element));

            foreach (var child in element.Children(Commander.XmlTag))
            {
                unit.SetCommander(LoadCommander(child));
            }

            unit.OnLoad(this, element);

            return unit;
        }

        /// <summary>
        /// Loads an Commander from this XElement
        /// </summary>
        Commander LoadCommander(XElement element)
        {
            Commander cdr = new Commander();
            cdr.Data = new CommanderData(GetData(element));

            cdr.OnLoad(this, element);

            return cdr;
        }

        /// <summary>
        /// Creates a GCSVLine from the contents of this XElement.
        /// </summary>
        GCSVLine GetData(XElement me)
        {
            var data = me.Children("d" + me.Name.LocalName).First();
            return new GCSVLine(Headers[me.Name.LocalName], data.Value.Split(s_delimiter));
        }

        internal T GetData<T>(XElement me, string name) where T : class, IMilitaryData, new()
        {
            if (me.Elements().Any(e => e.Name.LocalName == name))
            {
                var data = new T();
                var line = me.Children(data.TagName).First();
                data.Load(new GCSVLine(Headers[data.TagName], line.Value.Split(s_delimiter)));
                return data;
            }
            else
                return null;
        }
    }

    /// <summary>
    /// Provides extension methods for XElement which are used by MilitaryReader
    /// </summary>
    internal static class MilitaryXmlExtensions
    {
        /// <summary>
        /// Returns all children of this XElement having this localName
        /// </summary>
        public static IEnumerable<XElement> Children(this XElement me, string localName)
        {
            return me.Elements().Where(e => e.Name.LocalName == localName);
        }

    }

}
