
	
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Military.IO;


using Utilities.GCSV;

namespace Military
{
	
	
	
	
	
	public partial class PreviousCommanderData : IMilitaryData
	{
	
	
		
public int Id { get; set; }
public int Rank { get; set; }
public string FirstName { get; set; }
public string MiddleInitial { get; set; }
public string LastName { get; set; }
public int Engagements { get; set; }
public string CommandName { get; set; }
public double Experience { get; set; }

		
		public string TagName { get { return XmlTag ; } } 
		public static string XmlTag { get { return "dpcd" ; } } 
		
		public PreviousCommanderData()
		{
		}
		
		public PreviousCommanderData(GCSVLine line)
		{
			Load(line);
		}
		
		public void Load(IGCSVLine line)
		{
			string value = null;

 if(line.TryGetValue("id", out value)) 
   this.Id = int.Parse( value );
 if(line.TryGetValue("rank", out value)) 
   this.Rank = int.Parse( value );
 if(line.TryGetValue("fn", out value)) 
   this.FirstName =  value ;
 if(line.TryGetValue("mn", out value)) 
   this.MiddleInitial =  value ;
 if(line.TryGetValue("ln", out value)) 
   this.LastName =  value ;
 if(line.TryGetValue("eng", out value)) 
   this.Engagements = int.Parse( value );
 if(line.TryGetValue("command_name", out value)) 
   this.CommandName =  value ;
 if(line.TryGetValue("exp", out value)) 
   this.Experience = double.Parse( value );
		}
		
		public IGCSVLine SaveAsGCSV(IGCSVHeader header)
		{
			IGCSVLine line = new GCSVLine(header);
			this.Save(line);
			return line;
		}
		
		private void Save(IGCSVLine line)
		{
			
 line["id"] =  this.Id .ToString();
 line["rank"] =  this.Rank .ToString();
 line["fn"] =  this.FirstName ;
 line["mn"] =  this.MiddleInitial ;
 line["ln"] =  this.LastName ;
 line["eng"] =  this.Engagements .ToString();
 line["command_name"] =  this.CommandName ;
 line["exp"] =  this.Experience .Truncate4();
		}
			
			
        public void XmlWriteThis(MilitaryXmlWriter writer) { this.XmlWriteData(writer); }
	}
	
}
	
