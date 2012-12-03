using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Military
{
    public interface IForceBattleResultData
    {
        bool CommanderReplaced { get; set; }
        int CommanderStatus { get; set; }
    }
    partial class OrganizationBattleResultData : IForceBattleResultData
    { 
    }
    partial class UnitBattleResultData : IForceBattleResultData
    {
    }

}
