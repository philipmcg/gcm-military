
	
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Military.IO;


using Utilities.GCSV;

namespace Military
{
	
	
	
	
	
	public partial class OrganizationData : IMilitaryData
	{
	
	
		
public string Name { get; set; }
public int Level { get; set; }
public int Side { get; set; }
public int OrganizationNumber { get; set; }
public string State { get; set; }

		
		public string TagName { get { return XmlTag ; } } 
		public static string XmlTag { get { return "do" ; } } 
		
		public OrganizationData()
		{
		}
		
		public OrganizationData(GCSVLine line)
		{
			Load(line);
		}
		
		public void Load(IGCSVLine line)
		{
			string value = null;

 if(line.TryGetValue("name", out value)) 
   this.Name =  value ;
 if(line.TryGetValue("level", out value)) 
   this.Level = int.Parse( value );
 if(line.TryGetValue("side", out value)) 
   this.Side = int.Parse( value );
 if(line.TryGetValue("org_num", out value)) 
   this.OrganizationNumber = int.Parse( value );
 if(line.TryGetValue("state", out value)) 
   this.State =  value ;
		}
		
		public IGCSVLine SaveAsGCSV(IGCSVHeader header)
		{
			IGCSVLine line = new GCSVLine(header);
			this.Save(line);
			return line;
		}
		
		private void Save(IGCSVLine line)
		{
			
 line["name"] =  this.Name ;
 line["level"] =  this.Level .ToString();
 line["side"] =  this.Side .ToString();
 line["org_num"] =  this.OrganizationNumber .ToString();
 line["state"] =  this.State ;
		}
			
			
        public void XmlWriteThis(MilitaryXmlWriter writer) { this.XmlWriteData(writer); }
	}
	
}
	
