using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AbstractMilitary;

namespace Military
{
    static class InnerExtensions
    {
        public static string Truncate4(this double me)
        {
            return ((int)(me * 10000) / 10000d).ToString();
        }
        public static string Truncate4(this float me)
        {
            return ((int)(me * 10000) / 10000d).ToString();
        }
    }


    public sealed partial class Commander : AbstractCommander<Commander, Organization, Unit>
    {
        /// <summary>
        /// The data storage object for this Commander
        /// </summary>
        public CommanderData Data { get; set; }

        public Commander()
        {
            Data = new CommanderData();
        }
    }

    public sealed partial class Organization : AbstractOrganization<Commander, Organization, Unit>, IForce
    {
        /// <summary>
        /// The data storage object for this Organization
        /// </summary>
        public OrganizationData Data { get; set; }

        public Organization()
        {
            Data = new OrganizationData();
            m_units = new List<Unit>();
            m_organizations = new List<Organization>();
        }

    
    
        /// <summary>
        /// Emits the units in the proper order so they will be arranged from left to right in scourge of war.
        /// 
        /// For example, if there are 6 units, they should be ordered like this: 3,4,2,5,1,6
        /// </summary>
        public IEnumerable<Unit> UnitsInAlternatingOrder
        {
            get
            {
                if (m_units.Count == 0)
                    yield break;
                else if (m_units.Count == 1)
                    yield return m_units[0];

                int mid = m_units.Count / 2;

                int left = mid;
                int right = mid + 1;

                while (left >= 0)
                {
                    if (left >= 0)
                        yield return m_units[left];
                    left--;
                    if (right < m_units.Count)
                        yield return m_units[right];
                    right++;
                }
            }
        }
    }

    public sealed partial class Unit : AbstractUnit<Commander, Organization, Unit>, IForce
    {
        /// <summary>
        /// The data storage object for this Unit
        /// </summary>
        public UnitData Data { get; set; }

        public Unit()
        {
            Data = new UnitData();
        }
    }
}