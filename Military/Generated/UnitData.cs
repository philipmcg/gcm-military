
	
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Military.IO;


using Utilities.GCSV;

namespace Military
{
	
	
	
	
	
	public partial class UnitData : IMilitaryData
	{
	
	
		
/// <summary> 
/// Number of troops in unit
/// </summary> 
public int Men { get; set; }
/// <summary> 
/// Number of battles this unit has been engaged in
/// </summary> 
public int Engagements { get; set; }
public double Experience { get; set; }
public string Name { get; set; }
/// <summary> 
/// Identifier of the state this unit is from
/// </summary> 
public string State { get; set; }
public int RegimentNumber { get; set; }
public int Type { get; set; }
public double Marksmanship { get; set; }
public double Close { get; set; }
public double Open { get; set; }
public double Edged { get; set; }
public double Firearm { get; set; }
public double Horsemanship { get; set; }
/// <summary> 
/// Identifier of this unit's weapon
/// </summary> 
public int WeaponId { get; set; }
/// <summary> 
/// Identifier of this unit's troop class
/// </summary> 
public int ClassId { get; set; }
/// <summary> 
/// Whether this unit is active. Inactive units will not be present in battle.
/// </summary> 
public bool Active { get; set; }
/// <summary> 
/// Unit's unique ID
/// </summary> 
public int Id { get; set; }
public int Flag1 { get; set; }
public int Flag2 { get; set; }
/// <summary> 
/// Total enemy casualties inflicted
/// </summary> 
public int TotalKills { get; set; }
/// <summary> 
/// Men this unit started with
/// </summary> 
public int InitialMen { get; set; }
/// <summary> 
/// Experience this unit started with
/// </summary> 
public double InitialExperience { get; set; }
/// <summary> 
/// Total number ever wounded in battle
/// </summary> 
public int TotalWounded { get; set; }
/// <summary> 
/// Total number who died of wounds
/// </summary> 
public int TotalMWounded { get; set; }
/// <summary> 
/// Total number who died in battle
/// </summary> 
public int TotalKilled { get; set; }
/// <summary> 
/// Total number who went missing
/// </summary> 
public int TotalMissing { get; set; }
/// <summary> 
/// Total number permanently missing -- this should be added to the missing count in turn reports
/// </summary> 
public int PermanentlyMissing { get; set; }
/// <summary> 
/// Total number who were discharged
/// </summary> 
public int TotalDischarged { get; set; }
/// <summary> 
/// Total number who were recruited (including initial strength)
/// </summary> 
public int TotalRecruited { get; set; }
/// <summary> 
/// Number currently wounded
/// </summary> 
public int CurWounded { get; set; }
/// <summary> 
/// Number currently missing
/// </summary> 
public int CurMissing { get; set; }
/// <summary> 
/// Number currently sick (this is a portion of the current strength)
/// </summary> 
public int CurSick { get; set; }
/// <summary> 
/// Average experience of currently wounded men
/// </summary> 
public double WoundedExp { get; set; }
/// <summary> 
/// Average experience of currently missing men
/// </summary> 
public double MissingExp { get; set; }
/// <summary> 
/// Player setting, how high this unit should recruit to
/// </summary> 
public int RecruitLimit { get; set; }
/// <summary> 
/// If the unit is missing (cannon lost, regiment surrendered, etc)
/// </summary> 
public bool UnitMissing { get; set; }
/// <summary> 
/// The turn at which this unit was created
/// </summary> 
public int TurnMustered { get; set; }
/// <summary> 
/// Unit's current morale level
/// </summary> 
public int CurrentMorale { get; set; }
/// <summary> 
/// Unit's loyalty to current regiment commander
/// </summary> 
public int Loyalty { get; set; }

		
		public string TagName { get { return XmlTag ; } } 
		public static string XmlTag { get { return "du" ; } } 
		
		public UnitData()
		{
		}
		
		public UnitData(GCSVLine line)
		{
			Load(line);
		}
		
		public void Load(IGCSVLine line)
		{
			string value = null;

 if(line.TryGetValue("men", out value)) 
   this.Men = int.Parse( value );
 if(line.TryGetValue("engagements", out value)) 
   this.Engagements = int.Parse( value );
 if(line.TryGetValue("exp", out value)) 
   this.Experience = double.Parse( value );
 if(line.TryGetValue("name", out value)) 
   this.Name =  value ;
 if(line.TryGetValue("state", out value)) 
   this.State =  value ;
 if(line.TryGetValue("regiment_number", out value)) 
   this.RegimentNumber = int.Parse( value );
 if(line.TryGetValue("type", out value)) 
   this.Type = int.Parse( value );
 if(line.TryGetValue("marksmanship", out value)) 
   this.Marksmanship = double.Parse( value );
 if(line.TryGetValue("close", out value)) 
   this.Close = double.Parse( value );
 if(line.TryGetValue("open", out value)) 
   this.Open = double.Parse( value );
 if(line.TryGetValue("edged", out value)) 
   this.Edged = double.Parse( value );
 if(line.TryGetValue("firearm", out value)) 
   this.Firearm = double.Parse( value );
 if(line.TryGetValue("horsemanship", out value)) 
   this.Horsemanship = double.Parse( value );
 if(line.TryGetValue("weapon_id", out value)) 
   this.WeaponId = int.Parse( value );
 if(line.TryGetValue("class_id", out value)) 
   this.ClassId = int.Parse( value );
 if(line.TryGetValue("active", out value)) 
   this.Active =  value  == "1" ? true : false ;
 if(line.TryGetValue("id", out value)) 
   this.Id = int.Parse( value );
 if(line.TryGetValue("flag1", out value)) 
   this.Flag1 = int.Parse( value );
 if(line.TryGetValue("flag2", out value)) 
   this.Flag2 = int.Parse( value );
 if(line.TryGetValue("t_k", out value)) 
   this.TotalKills = int.Parse( value );
 if(line.TryGetValue("i_men", out value)) 
   this.InitialMen = int.Parse( value );
 if(line.TryGetValue("i_exp", out value)) 
   this.InitialExperience = double.Parse( value );
 if(line.TryGetValue("t_w", out value)) 
   this.TotalWounded = int.Parse( value );
 if(line.TryGetValue("t_mw", out value)) 
   this.TotalMWounded = int.Parse( value );
 if(line.TryGetValue("t_k2", out value)) 
   this.TotalKilled = int.Parse( value );
 if(line.TryGetValue("t_m", out value)) 
   this.TotalMissing = int.Parse( value );
 if(line.TryGetValue("p_m", out value)) 
   this.PermanentlyMissing = int.Parse( value );
 if(line.TryGetValue("t_d", out value)) 
   this.TotalDischarged = int.Parse( value );
 if(line.TryGetValue("t_r", out value)) 
   this.TotalRecruited = int.Parse( value );
 if(line.TryGetValue("c_w", out value)) 
   this.CurWounded = int.Parse( value );
 if(line.TryGetValue("c_m", out value)) 
   this.CurMissing = int.Parse( value );
 if(line.TryGetValue("c_s", out value)) 
   this.CurSick = int.Parse( value );
 if(line.TryGetValue("e_w", out value)) 
   this.WoundedExp = double.Parse( value );
 if(line.TryGetValue("e_m", out value)) 
   this.MissingExp = double.Parse( value );
 if(line.TryGetValue("r_l", out value)) 
   this.RecruitLimit = int.Parse( value );
 if(line.TryGetValue("g_m", out value)) 
   this.UnitMissing =  value  == "1" ? true : false ;
 if(line.TryGetValue("turn_m", out value)) 
   this.TurnMustered = int.Parse( value );
 if(line.TryGetValue("c_morale", out value)) 
   this.CurrentMorale = int.Parse( value );
 if(line.TryGetValue("ly", out value)) 
   this.Loyalty = int.Parse( value );
		}
		
		public IGCSVLine SaveAsGCSV(IGCSVHeader header)
		{
			IGCSVLine line = new GCSVLine(header);
			this.Save(line);
			return line;
		}
		
		private void Save(IGCSVLine line)
		{
			
 line["men"] =  this.Men .ToString();
 line["engagements"] =  this.Engagements .ToString();
 line["exp"] =  this.Experience .Truncate4();
 line["name"] =  this.Name ;
 line["state"] =  this.State ;
 line["regiment_number"] =  this.RegimentNumber .ToString();
 line["type"] =  this.Type .ToString();
 line["marksmanship"] =  this.Marksmanship .Truncate4();
 line["close"] =  this.Close .Truncate4();
 line["open"] =  this.Open .Truncate4();
 line["edged"] =  this.Edged .Truncate4();
 line["firearm"] =  this.Firearm .Truncate4();
 line["horsemanship"] =  this.Horsemanship .Truncate4();
 line["weapon_id"] =  this.WeaponId .ToString();
 line["class_id"] =  this.ClassId .ToString();
 line["active"] =  this.Active  ? "1" : "0" ;
 line["id"] =  this.Id .ToString();
 line["flag1"] =  this.Flag1 .ToString();
 line["flag2"] =  this.Flag2 .ToString();
 line["t_k"] =  this.TotalKills .ToString();
 line["i_men"] =  this.InitialMen .ToString();
 line["i_exp"] =  this.InitialExperience .Truncate4();
 line["t_w"] =  this.TotalWounded .ToString();
 line["t_mw"] =  this.TotalMWounded .ToString();
 line["t_k2"] =  this.TotalKilled .ToString();
 line["t_m"] =  this.TotalMissing .ToString();
 line["p_m"] =  this.PermanentlyMissing .ToString();
 line["t_d"] =  this.TotalDischarged .ToString();
 line["t_r"] =  this.TotalRecruited .ToString();
 line["c_w"] =  this.CurWounded .ToString();
 line["c_m"] =  this.CurMissing .ToString();
 line["c_s"] =  this.CurSick .ToString();
 line["e_w"] =  this.WoundedExp .Truncate4();
 line["e_m"] =  this.MissingExp .Truncate4();
 line["r_l"] =  this.RecruitLimit .ToString();
 line["g_m"] =  this.UnitMissing  ? "1" : "0" ;
 line["turn_m"] =  this.TurnMustered .ToString();
 line["c_morale"] =  this.CurrentMorale .ToString();
 line["ly"] =  this.Loyalty .ToString();
		}
			
			
        public void XmlWriteThis(MilitaryXmlWriter writer) { this.XmlWriteData(writer); }
	}
	
}
	
