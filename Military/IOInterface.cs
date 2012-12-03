using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;
using Military.IO;

namespace Military
{
    // This class provides the implementation of IXmlWriteable for all classes below.
    // It should not contain anything else.
    partial class Commander : IXmlWriteable
    {
        public string TagName { get { return XmlTag; } }
        public static string XmlTag { get { return "c"; } }
        
        public void XmlWriteThis(MilitaryXmlWriter writer)
        {
            foreach (var data in writer.Mode.GetCommanderData(this).Where(d => d != null))
            {
                data.XmlWrite(writer);
            }
        }
    }

    partial class Organization : IXmlWriteable
    {
        public string TagName { get { return XmlTag; } }
        public static string XmlTag { get { return "o"; } }

        public void XmlWriteThis(MilitaryXmlWriter writer)
        {
            foreach (var data in writer.Mode.GetOrganizationData(this).Where(d => d != null))
            {
                data.XmlWrite(writer);
            }

            if (this.HasCommander)
                Commander.XmlWrite(writer);

            foreach (var unit in this.Units)
            {
                unit.XmlWrite(writer);
            }

            foreach (var org in this.Organizations)
            {
                org.XmlWrite(writer);
            }
        }
    }

    partial class Unit : IXmlWriteable
    {
        public string TagName { get { return XmlTag; } }
        public static string XmlTag { get { return "u"; } }
        
        public void XmlWriteThis(MilitaryXmlWriter writer)
        {
            foreach (var data in writer.Mode.GetUnitData(this).Where(d => d != null))
            {
                data.XmlWrite(writer);
            }

            if (this.HasCommander)
                Commander.XmlWrite(writer);
        }
    }
}
