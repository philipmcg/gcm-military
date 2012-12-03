using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;

namespace Military.IO
{
    /// <summary>
    /// Represents a Military object that can be written as Xml.
    /// </summary>
    public interface IXmlWriteable
    {
        string TagName { get; }
        void XmlWriteThis(MilitaryXmlWriter writer);
    }


    internal static class XmlExtensionMethods
    {
        /// <summary>
        /// Writes this object to Xml, using the XmlElementName attribute of the class
        /// as the outer tag.
        /// </summary>
        public static void XmlWrite<T>(this T me, MilitaryXmlWriter writer) where T : IXmlWriteable
        {
            writer.WriteStartElement(me.TagName);
            me.XmlWriteThis(writer);
            writer.WriteEndElement();
        }

        /// <summary>
        /// Writes this Military Data object to Xml, using the XmlElementName attribute of the class
        /// as the outer tag.
        /// </summary>
        public static void XmlWriteData<T>(this T me, MilitaryXmlWriter writer) where T : IMilitaryData
        {
            writer.WriteString(me.SaveAsGCSV(writer.GetHeader(me.TagName)).ToString());
        }
    }
}
