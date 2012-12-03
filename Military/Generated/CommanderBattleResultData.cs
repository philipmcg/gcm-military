
	
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Military.IO;


using Utilities.GCSV;

namespace Military
{
	
	
	
	
	
	public partial class CommanderBattleResultData : IMilitaryData
	{
	
	
		
public int Status { get; set; }
public int OldRank { get; set; }
public string OldFirstName { get; set; }
public string OldMiddleInitial { get; set; }
public string OldLastName { get; set; }

		
		public string TagName { get { return XmlTag ; } } 
		public static string XmlTag { get { return "dcbr" ; } } 
		
		public CommanderBattleResultData()
		{
		}
		
		public CommanderBattleResultData(GCSVLine line)
		{
			Load(line);
		}
		
		public void Load(IGCSVLine line)
		{
			string value = null;

 if(line.TryGetValue("status", out value)) 
   this.Status = int.Parse( value );
 if(line.TryGetValue("old_rank", out value)) 
   this.OldRank = int.Parse( value );
 if(line.TryGetValue("old_fname", out value)) 
   this.OldFirstName =  value ;
 if(line.TryGetValue("old_mname", out value)) 
   this.OldMiddleInitial =  value ;
 if(line.TryGetValue("old_lname", out value)) 
   this.OldLastName =  value ;
		}
		
		public IGCSVLine SaveAsGCSV(IGCSVHeader header)
		{
			IGCSVLine line = new GCSVLine(header);
			this.Save(line);
			return line;
		}
		
		private void Save(IGCSVLine line)
		{
			
 line["status"] =  this.Status .ToString();
 line["old_rank"] =  this.OldRank .ToString();
 line["old_fname"] =  this.OldFirstName ;
 line["old_mname"] =  this.OldMiddleInitial ;
 line["old_lname"] =  this.OldLastName ;
		}
			
			
        public void XmlWriteThis(MilitaryXmlWriter writer) { this.XmlWriteData(writer); }
	}
	
}
	
