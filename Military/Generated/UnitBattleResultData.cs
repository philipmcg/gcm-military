
	
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Military.IO;


using Utilities.GCSV;

namespace Military
{
	
	
	
	
	
	public partial class UnitBattleResultData : IMilitaryData
	{
	
	
		
/// <summary> 
/// Number of troops in unit before battle
/// </summary> 
public int TotalMen { get; set; }
/// <summary> 
/// Number of unit's troops present in battle
/// </summary> 
public int InvolvedMen { get; set; }
/// <summary> 
/// Number of unit's troops present after battle
/// </summary> 
public int RemainingMen { get; set; }
/// <summary> 
/// Total casualties
/// </summary> 
public int TotalLost { get; set; }
public bool Captured { get; set; }
public bool Routed { get; set; }
/// <summary> 
/// Whether this unit was at the battle.
/// </summary> 
public bool Present { get; set; }
public int Killed { get; set; }
public int Wounded { get; set; }
public int Missing { get; set; }
public int Returned { get; set; }
public int Inflicted { get; set; }
public double ExperienceGain { get; set; }
public bool CommanderReplaced { get; set; }
public int CommanderStatus { get; set; }

		
		public string TagName { get { return XmlTag ; } } 
		public static string XmlTag { get { return "dubr" ; } } 
		
		public UnitBattleResultData()
		{
		}
		
		public UnitBattleResultData(GCSVLine line)
		{
			Load(line);
		}
		
		public void Load(IGCSVLine line)
		{
			string value = null;

 if(line.TryGetValue("total_men", out value)) 
   this.TotalMen = int.Parse( value );
 if(line.TryGetValue("involved_men", out value)) 
   this.InvolvedMen = int.Parse( value );
 if(line.TryGetValue("remaining_men", out value)) 
   this.RemainingMen = int.Parse( value );
 if(line.TryGetValue("total_lost", out value)) 
   this.TotalLost = int.Parse( value );
 if(line.TryGetValue("captured", out value)) 
   this.Captured =  value  == "1" ? true : false ;
 if(line.TryGetValue("routed", out value)) 
   this.Routed =  value  == "1" ? true : false ;
 if(line.TryGetValue("present", out value)) 
   this.Present =  value  == "1" ? true : false ;
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
 if(line.TryGetValue("experience_gain", out value)) 
   this.ExperienceGain = double.Parse( value );
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
			
 line["total_men"] =  this.TotalMen .ToString();
 line["involved_men"] =  this.InvolvedMen .ToString();
 line["remaining_men"] =  this.RemainingMen .ToString();
 line["total_lost"] =  this.TotalLost .ToString();
 line["captured"] =  this.Captured  ? "1" : "0" ;
 line["routed"] =  this.Routed  ? "1" : "0" ;
 line["present"] =  this.Present  ? "1" : "0" ;
 line["killed"] =  this.Killed .ToString();
 line["wounded"] =  this.Wounded .ToString();
 line["missing"] =  this.Missing .ToString();
 line["returned"] =  this.Returned .ToString();
 line["inflicted"] =  this.Inflicted .ToString();
 line["experience_gain"] =  this.ExperienceGain .Truncate4();
 line["commander_replaced"] =  this.CommanderReplaced  ? "1" : "0" ;
 line["commander_status"] =  this.CommanderStatus .ToString();
		}
			
			
        public void XmlWriteThis(MilitaryXmlWriter writer) { this.XmlWriteData(writer); }
	}
	
}
	
