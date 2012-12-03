using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Military.IO
{
    /// <summary>
    /// Provides an interface for grouping top-level Organizations which are by definition not part of the same tree.
    /// </summary>
    public class MilitaryGroup : AbstractMilitary.IMilitaryGroup<Organization>
    {
        List<Organization> m_organizations;

        /// <summary>
        /// Returns the Organizations in this MilitaryGroup.
        /// </summary>
        public IEnumerable<Organization> Organizations { get { return m_organizations; } }

        /// <summary>
        /// Returns the first Organization in this MilitaryGroup.
        /// </summary>
        public Organization FirstOrganization { get { return Organizations.First(); } }

        /// <summary>
        /// Creates a MilitaryGroup with multiple Organizations
        /// </summary>
        public MilitaryGroup(IEnumerable<Organization> organizations)
        {
            m_organizations = new List<Organization>(organizations);
        }

        /// <summary>
        /// Creates a MilitaryGroup with one Organization
        /// </summary>
        public MilitaryGroup(Organization organization)
        {
            m_organizations = new List<Organization>(){organization};
        }
    }
}
