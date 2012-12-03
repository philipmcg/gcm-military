
	
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Military.IO;


using Utilities.GCSV;

namespace Military
{
	
	
	
	
	
	public partial class CommanderTurnData : IMilitaryData
	{
	
	
		
/// <summary> 
/// Commander's rank before turn
/// </summary> 
public int OldRank { get; set; }
/// <summary> 
/// Commander's rank after turn
/// </summary> 
public int NewRank { get; set; }

		
		public string TagName { get { return XmlTag ; } } 
		public static string XmlTag { get { return "dct" ; } } 
		
		public CommanderTurnData()
		{
		}
		
		public CommanderTurnData(GCSVLine line)
		{
			Load(line);
		}
		
		public void Load(IGCSVLine line)
		{
			string value = null;

 if(line.TryGetValue("o_r", out value)) 
   this.OldRank = int.Parse( value );
 if(line.TryGetValue("n_r", out value)) 
   this.NewRank = int.Parse( value );
		}
		
		public IGCSVLine SaveAsGCSV(IGCSVHeader header)
		{
			IGCSVLine line = new GCSVLine(header);
			this.Save(line);
			return line;
		}
		
		private void Save(IGCSVLine line)
		{
			
 line["o_r"] =  this.OldRank .ToString();
 line["n_r"] =  this.NewRank .ToString();
		}
			
			
        public void XmlWriteThis(MilitaryXmlWriter writer) { this.XmlWriteData(writer); }
	}
	
}
	
