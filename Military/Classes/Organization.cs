using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Military.IO;

namespace Military
{

    partial class Organization : IComparable<Organization>
    {
        /// <summary>
        /// Returns this unit's place in the organization
        /// </summary>
        public int IndexOfUnit(Unit unit)
        {
            return this.m_units.IndexOf(unit);
        }

        public override string ToString()
        {
            return Data.Name;
        }

        internal void OnLoad(MilitaryReader reader, System.Xml.Linq.XElement element)
        {
            this.BattleResultData = reader.GetData<OrganizationBattleResultData>(element, OrganizationBattleResultData.XmlTag);
            this.TurnData = reader.GetData<OrganizationTurnData>(element, OrganizationTurnData.XmlTag);
            this.PreviousCommanderData = reader.GetData<PreviousCommanderData>(element, PreviousCommanderData.XmlTag);
        }

        public OrganizationBattleResultData BattleResultData { get; set; }
        public IForceBattleResultData ForceBattleResultData { get { return BattleResultData; } }

        public OrganizationTurnData TurnData { get; set; }
        public PreviousCommanderData PreviousCommanderData { get; set; }

        int IComparable<Organization>.CompareTo(Organization other)
        {
            var unit = this.AllUnits.FirstOrDefault();
            if (unit == null)
                return 1;
            var unit2 = other.AllUnits.FirstOrDefault();
            if (unit2 == null)
                return -1;

            return unit.Data.Type.CompareTo(unit2.Data.Type);
        }


        /// <summary>
        /// ONLY call this if the organizations being passed in are the SAME as the ones in the organization.  
        /// </summary>
        public void ReorderOrganizations(IEnumerable<Organization> orgs)
        {
            this.m_organizations = orgs.ToList();
        }

        public int NumOrganizations { get { return m_organizations.Count; } }
        public int NumUnits { get { return m_units.Count; } }
    }
}
