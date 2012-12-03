using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Military
{
    public interface IForce
    {
        void SetCommander(Commander cdr);
        Commander Commander { get; }
        bool HasCommander { get; }
        void SetNoCommander();
        IForceBattleResultData ForceBattleResultData { get; }
        PreviousCommanderData PreviousCommanderData { get; set; }
    }

}
