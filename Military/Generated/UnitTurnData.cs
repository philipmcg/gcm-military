
	
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Military.IO;


using Utilities.GCSV;

namespace Military
{
	
	
	
	
	
	public partial class UnitTurnData : IMilitaryData
	{
	
	
		
/// <summary> 
/// Total change in unit men
/// </summary> 
public int MenChange { get; set; }
/// <summary> 
/// Number of wounded returned to the ranks
/// </summary> 
public int WoundedReturned { get; set; }
/// <summary> 
/// Number of wounded who died of wounds
/// </summary> 
public int WoundedDied { get; set; }
/// <summary> 
/// Number of wounded who were discharged
/// </summary> 
public int WoundedDischarged { get; set; }
/// <summary> 
/// Number of missing who were permanently lost
/// </summary> 
public int MissingLost { get; set; }
/// <summary> 
/// Number of missing who returned to the ranks
/// </summary> 
public int MissingReturned { get; set; }
/// <summary> 
/// Number of troops recruited
/// </summary> 
public int TroopsRecruited { get; set; }
/// <summary> 
/// Number of troops who deserted (went missing)
/// </summary> 
public int TroopsDeserted { get; set; }
/// <summary> 
/// Whether this unit returned from missing status this turn
/// </summary> 
public bool UnitReturned { get; set; }
/// <summary> 
/// Change in morale percentage
/// </summary> 
public int MoraleChange { get; set; }
/// <summary> 
/// Change in experience
/// </summary> 
public double ExperienceChange { get; set; }
/// <summary> 
/// Unit was deleted this turn
/// </summary> 
public bool UnitDeleted { get; set; }

		
		public string TagName { get { return XmlTag ; } } 
		public static string XmlTag { get { return "dut" ; } } 
		
		public UnitTurnData()
		{
		}
		
		public UnitTurnData(GCSVLine line)
		{
			Load(line);
		}
		
		public void Load(IGCSVLine line)
		{
			string value = null;

 if(line.TryGetValue("men_c", out value)) 
   this.MenChange = int.Parse( value );
 if(line.TryGetValue("w_r", out value)) 
   this.WoundedReturned = int.Parse( value );
 if(line.TryGetValue("w_k", out value)) 
   this.WoundedDied = int.Parse( value );
 if(line.TryGetValue("w_d", out value)) 
   this.WoundedDischarged = int.Parse( value );
 if(line.TryGetValue("m_l", out value)) 
   this.MissingLost = int.Parse( value );
 if(line.TryGetValue("m_r", out value)) 
   this.MissingReturned = int.Parse( value );
 if(line.TryGetValue("t_r", out value)) 
   this.TroopsRecruited = int.Parse( value );
 if(line.TryGetValue("t_d", out value)) 
   this.TroopsDeserted = int.Parse( value );
 if(line.TryGetValue("u_r", out value)) 
   this.UnitReturned =  value  == "1" ? true : false ;
 if(line.TryGetValue("m_c", out value)) 
   this.MoraleChange = int.Parse( value );
 if(line.TryGetValue("e_c", out value)) 
   this.ExperienceChange = double.Parse( value );
 if(line.TryGetValue("u_d", out value)) 
   this.UnitDeleted =  value  == "1" ? true : false ;
		}
		
		public IGCSVLine SaveAsGCSV(IGCSVHeader header)
		{
			IGCSVLine line = new GCSVLine(header);
			this.Save(line);
			return line;
		}
		
		private void Save(IGCSVLine line)
		{
			
 line["men_c"] =  this.MenChange .ToString();
 line["w_r"] =  this.WoundedReturned .ToString();
 line["w_k"] =  this.WoundedDied .ToString();
 line["w_d"] =  this.WoundedDischarged .ToString();
 line["m_l"] =  this.MissingLost .ToString();
 line["m_r"] =  this.MissingReturned .ToString();
 line["t_r"] =  this.TroopsRecruited .ToString();
 line["t_d"] =  this.TroopsDeserted .ToString();
 line["u_r"] =  this.UnitReturned  ? "1" : "0" ;
 line["m_c"] =  this.MoraleChange .ToString();
 line["e_c"] =  this.ExperienceChange .Truncate4();
 line["u_d"] =  this.UnitDeleted  ? "1" : "0" ;
		}
			
			
        public void XmlWriteThis(MilitaryXmlWriter writer) { this.XmlWriteData(writer); }
	}
	
}
	
