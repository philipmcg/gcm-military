
	
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Military.IO;


using Utilities.GCSV;

namespace Military
{
	
	
	
	
	
	public partial class OrganizationBattleResultData : IMilitaryData
	{
	
	
		
public int Killed { get; set; }
public int Wounded { get; set; }
public int Missing { get; set; }
public int Returned { get; set; }
public int Inflicted { get; set; }
public bool CommanderReplaced { get; set; }
public int CommanderStatus { get; set; }

		
		public string TagName { get { return XmlTag ; } } 
		public static string XmlTag { get { return "dobr" ; } } 
		
		public OrganizationBattleResultData()
		{
		}
		
		public OrganizationBattleResultData(GCSVLine line)
		{
			Load(line);
		}
		
		public void Load(IGCSVLine line)
		{
			string value = null;

 if(line.TryGetValue("killed", out value)) 
   this.Killed = int.Parse( value );
 if(line.TryGetValue("wounded", out value)) 
   this.Wounded = int.Parse( value );
 if(line.TryGetValue("missing", out value)) 
   this.Missing = int.Parse( value );
 if(line.TryGetValue("returned", out value)) 
   this.Returned = int.Parse( value );
 if(line.TryGetValue("inflicted", out value)) 
   this.Inflicted = int.Parse( value );
 if(line.TryGetValue("commander_replaced", out value)) 
   this.CommanderReplaced =  value  == "1" ? true : false ;
 if(line.TryGetValue("commander_status", out value)) 
   this.CommanderStatus = int.Parse( value );
		}
		
		public IGCSVLine SaveAsGCSV(IGCSVHeader header)
		{
			IGCSVLine line = new GCSVLine(header);
			this.Save(line);
			return line;
		}
		
		private void Save(IGCSVLine line)
		{
			
 line["killed"] =  this.Killed .ToString();
 line["wounded"] =  this.Wounded .ToString();
 line["missing"] =  this.Missing .ToString();
 line["returned"] =  this.Returned .ToString();
 line["inflicted"] =  this.Inflicted .ToString();
 line["commander_replaced"] =  this.CommanderReplaced  ? "1" : "0" ;
 line["commander_status"] =  this.CommanderStatus .ToString();
		}
			
			
        public void XmlWriteThis(MilitaryXmlWriter writer) { this.XmlWriteData(writer); }
	}
	
}
	
