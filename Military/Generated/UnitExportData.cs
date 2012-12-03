
	
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Military.IO;


using Utilities.GCSV;

namespace Military
{
	
	
	
	
	
	public partial class UnitExportData : IMilitaryData
	{
	
	
		
/// <summary> 
/// Number of troops in unit
/// </summary> 
public int Men { get; set; }
public int Experience { get; set; }
public string Name { get; set; }
public int Marksmanship { get; set; }
public int Close { get; set; }
public int Open { get; set; }
public int Edged { get; set; }
public int Firearm { get; set; }
public int Horsemanship { get; set; }
/// <summary> 
/// Identifier of this unit's weapon
/// </summary> 
public int WeaponId { get; set; }
/// <summary> 
/// Identifier of this unit's troop class
/// </summary> 
public int ClassId { get; set; }
public int Flag1 { get; set; }
public int Flag2 { get; set; }

		
		public string TagName { get { return XmlTag ; } } 
		public static string XmlTag { get { return "due" ; } } 
		
		public UnitExportData()
		{
		}
		
		public UnitExportData(GCSVLine line)
		{
			Load(line);
		}
		
		public void Load(IGCSVLine line)
		{
			string value = null;

 if(line.TryGetValue("men", out value)) 
   this.Men = int.Parse( value );
 if(line.TryGetValue("exp", out value)) 
   this.Experience = int.Parse( value );
 if(line.TryGetValue("name", out value)) 
   this.Name =  value ;
 if(line.TryGetValue("marksmanship", out value)) 
   this.Marksmanship = int.Parse( value );
 if(line.TryGetValue("close", out value)) 
   this.Close = int.Parse( value );
 if(line.TryGetValue("open", out value)) 
   this.Open = int.Parse( value );
 if(line.TryGetValue("edged", out value)) 
   this.Edged = int.Parse( value );
 if(line.TryGetValue("firearm", out value)) 
   this.Firearm = int.Parse( value );
 if(line.TryGetValue("horsemanship", out value)) 
   this.Horsemanship = int.Parse( value );
 if(line.TryGetValue("weapon_id", out value)) 
   this.WeaponId = int.Parse( value );
 if(line.TryGetValue("class_id", out value)) 
   this.ClassId = int.Parse( value );
 if(line.TryGetValue("flag1", out value)) 
   this.Flag1 = int.Parse( value );
 if(line.TryGetValue("flag2", out value)) 
   this.Flag2 = int.Parse( value );
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
 line["exp"] =  this.Experience .ToString();
 line["name"] =  this.Name ;
 line["marksmanship"] =  this.Marksmanship .ToString();
 line["close"] =  this.Close .ToString();
 line["open"] =  this.Open .ToString();
 line["edged"] =  this.Edged .ToString();
 line["firearm"] =  this.Firearm .ToString();
 line["horsemanship"] =  this.Horsemanship .ToString();
 line["weapon_id"] =  this.WeaponId .ToString();
 line["class_id"] =  this.ClassId .ToString();
 line["flag1"] =  this.Flag1 .ToString();
 line["flag2"] =  this.Flag2 .ToString();
		}
			
			
        public void XmlWriteThis(MilitaryXmlWriter writer) { this.XmlWriteData(writer); }
	}
	
}
	
