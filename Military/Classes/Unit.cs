using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Military.IO;

namespace Military
{

    partial class Unit : IComparable<Unit>
    {
        public UnitBattleResultData BattleResultData { get; set; }
        public IForceBattleResultData ForceBattleResultData { get { return BattleResultData; } }
        public UnitTurnData TurnData { get; set; }
        public UnitExportData ExportData { get; set; }
        public PreviousCommanderData PreviousCommanderData { get; set; }

        internal void OnLoad(MilitaryReader reader, System.Xml.Linq.XElement element)
        {
            this.BattleResultData = reader.GetData<UnitBattleResultData>(element, UnitBattleResultData.XmlTag);
            this.TurnData = reader.GetData<UnitTurnData>(element, UnitTurnData.XmlTag);
            this.PreviousCommanderData = reader.GetData<PreviousCommanderData>(element, PreviousCommanderData.XmlTag);
        }

        int IComparable<Unit>.CompareTo(Unit other)
        {
            int cs = this.Data.State.CompareTo(other.Data.State);
            if(cs != 0)
                return cs;
            else
                return this.Data.RegimentNumber.CompareTo(other.Data.RegimentNumber);
        }

        public override string ToString()
        {
            return Data != null ? this.Data.Type + " " + this.Data.Name : "Unit";
        }
    }
}
