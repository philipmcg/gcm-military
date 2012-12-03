
	
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Military.IO;


using Utilities.GCSV;

namespace Military
{
	
	
	
	
	
	public partial class CommanderData : IMilitaryData
	{
	
	
		
public int Id { get; set; }
public int Rank { get; set; }
public string FirstName { get; set; }
public string MiddleInitial { get; set; }
public string LastName { get; set; }
public int Engagements { get; set; }
public double Experience { get; set; }
public double Ability { get; set; }
public double Command { get; set; }
public double Control { get; set; }
public double Leadership { get; set; }
public double Style { get; set; }
public string Portrait { get; set; }
public int Morale { get; set; }
/// <summary> 
/// Commander's political power
/// </summary> 
public double Politics { get; set; }

		
		public string TagName { get { return XmlTag ; } } 
		public static string XmlTag { get { return "dc" ; } } 
		
		public CommanderData()
		{
		}
		
		public CommanderData(GCSVLine line)
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
 if(line.TryGetValue("fname", out value)) 
   this.FirstName =  value ;
 if(line.TryGetValue("mname", out value)) 
   this.MiddleInitial =  value ;
 if(line.TryGetValue("lname", out value)) 
   this.LastName =  value ;
 if(line.TryGetValue("engagements", out value)) 
   this.Engagements = int.Parse( value );
 if(line.TryGetValue("exp", out value)) 
   this.Experience = double.Parse( value );
 if(line.TryGetValue("ability", out value)) 
   this.Ability = double.Parse( value );
 if(line.TryGetValue("command", out value)) 
   this.Command = double.Parse( value );
 if(line.TryGetValue("control", out value)) 
   this.Control = double.Parse( value );
 if(line.TryGetValue("leadership", out value)) 
   this.Leadership = double.Parse( value );
 if(line.TryGetValue("style", out value)) 
   this.Style = double.Parse( value );
 if(line.TryGetValue("portrait", out value)) 
   this.Portrait =  value ;
 if(line.TryGetValue("morale", out value)) 
   this.Morale = int.Parse( value );
 if(line.TryGetValue("pl", out value)) 
   this.Politics = double.Parse( value );
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
 line["fname"] =  this.FirstName ;
 line["mname"] =  this.MiddleInitial ;
 line["lname"] =  this.LastName ;
 line["engagements"] =  this.Engagements .ToString();
 line["exp"] =  this.Experience .Truncate4();
 line["ability"] =  this.Ability .Truncate4();
 line["command"] =  this.Command .Truncate4();
 line["control"] =  this.Control .Truncate4();
 line["leadership"] =  this.Leadership .Truncate4();
 line["style"] =  this.Style .Truncate4();
 line["portrait"] =  this.Portrait ;
 line["morale"] =  this.Morale .ToString();
 line["pl"] =  this.Politics .Truncate4();
		}
			
			
        public void XmlWriteThis(MilitaryXmlWriter writer) { this.XmlWriteData(writer); }
	}
	
}
	
