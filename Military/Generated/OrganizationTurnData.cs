
	
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Military.IO;


using Utilities.GCSV;

namespace Military
{
	
	
	
	
	
	public partial class OrganizationTurnData : IMilitaryData
	{
	
	
		

		
		public string TagName { get { return XmlTag ; } } 
		public static string XmlTag { get { return "dot" ; } } 
		
		public OrganizationTurnData()
		{
		}
		
		public OrganizationTurnData(GCSVLine line)
		{
			Load(line);
		}
		
		public void Load(IGCSVLine line)
		{
			string value = null;

		}
		
		public IGCSVLine SaveAsGCSV(IGCSVHeader header)
		{
			IGCSVLine line = new GCSVLine(header);
			this.Save(line);
			return line;
		}
		
		private void Save(IGCSVLine line)
		{
			
		}
			
			
        public void XmlWriteThis(MilitaryXmlWriter writer) { this.XmlWriteData(writer); }
	}
	
}
	
