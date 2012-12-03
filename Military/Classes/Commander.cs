using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Military.IO;

namespace Military
{

    partial class Commander
    {

        internal void OnLoad(MilitaryReader reader, System.Xml.Linq.XElement element)
        {
            this.BattleResultData = reader.GetData<CommanderBattleResultData>(element, CommanderBattleResultData.XmlTag);
            this.TurnData = reader.GetData<CommanderTurnData>(element, CommanderTurnData.XmlTag);
        }

        public CommanderBattleResultData BattleResultData { get; set; }
        public CommanderTurnData TurnData { get; set; }

        public Rank Rank { get { return (Rank)Data.Rank; } set { Data.Rank = (int)value; } }

        public override string ToString()
        {
            return Data != null ? this.Data.Rank + " " + this.Data.LastName : "Commander";
        }
    }
}
