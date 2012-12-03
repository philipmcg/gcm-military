using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbstractMilitary
{

    /// <summary>
    /// Represents an abstract military structure having child or subordinate forces
    /// </summary>
    public interface IMilitaryOwner<TC, TO, TU, TF>
    #region Generics
        where TC : AbstractCommander<TC, TO, TU>
        where TO : AbstractOrganization<TC, TO, TU>
        where TU : AbstractUnit<TC, TO, TU>
        where TF : AbstractForce<TC, TO, TU>
    #endregion
    {

        /// <summary>
        /// Recursively yields all child Commanders.
        /// </summary>
        IEnumerable<TC> AllCommanders { get; }

        /// <summary>
        /// Recursively yields all child Organizations.
        /// </summary>
        IEnumerable<TO> AllOrganizations { get; }

        /// <summary>
        /// Recursively yields all child Units.
        /// </summary>
        IEnumerable<TU> AllUnits { get; }

        /// <summary>
        /// Recursively yields all child Forces.
        /// </summary>
        IEnumerable<TF> AllForces { get; }
    }

    /// <summary>
    /// Top-level interface for the Commander, Organization and Unit classes.
    /// </summary>
    public interface IMilitaryConstruct
    {
        object Tag { get; set; }
    }


    /// <summary>
    /// Represents an abstract military force, having a commander and a parent organization
    /// </summary>
    public abstract class AbstractForce<TC, TO, TU> : IMilitaryConstruct
    #region Generics
        where TC : AbstractCommander<TC, TO, TU>
        where TO : AbstractOrganization<TC, TO, TU>
        where TU : AbstractUnit<TC, TO, TU>
    #endregion
    {
        public object Tag { get; set; }

        /// <summary>
        /// This Force's Commander
        /// </summary>
        public TC Commander { get; protected set; }

        /// <summary>
        /// This Force's parent Organization
        /// </summary>
        public TO Parent { get; protected set; }

        /// <summary>
        /// Returns true if this Force has a parent Organization
        /// </summary>
        public bool HasParent { get { return Parent != null; } }

        /// <summary>
        /// Returns true if this Force has a Commander
        /// </summary>
        public bool HasCommander { get { return Commander != null; } }

        /// <summary>
        /// If the force has a commander, sets the commander's command to null.  Also sets this command's commander to null.
        /// </summary>
        public void SetNoCommander()
        {
            if (this.HasCommander)
                Commander.Command = null;
            this.Commander = null;
        }

        /// <summary>
        /// Sets this Force's Commander, and sets the Commander's Command to this Force.
        /// </summary>
        /// <param name="commander">The new Commander for this Force</param>
        public void SetCommander(TC commander)
        {
            Commander = commander;
            commander.Command = this;
        }

        /// <summary>
        /// Detaches this force from its parent Organization.
        /// </summary>
        internal void SetNoParent()
        {
            Parent = null;
        }
    }

    /// <summary>
    /// Represents an abstract military organization having a commander, child units, and child organizations
    /// </summary>
    public abstract class AbstractOrganization<TC, TO, TU>
    #region Generics
 : AbstractForce<TC, TO, TU>, IMilitaryOwner<TC, TO, TU, AbstractForce<TC, TO, TU>>
        where TC : AbstractCommander<TC, TO, TU>
        where TO : AbstractOrganization<TC, TO, TU>
        where TU : AbstractUnit<TC, TO, TU>
    #endregion
    {
        protected List<TU> m_units;
        protected List<TO> m_organizations;

        /// <summary>
        /// Yields all direct child Units.
        /// </summary>
        public IEnumerable<TU> Units 
        { 
            get { foreach (var unit in m_units) yield return unit; } 
            set 
            {
                while (m_units.Any())
                    this.RemoveUnit(m_units.First());
                foreach (var unit in value) { this.AddUnit(unit); } 
            } 
        }

        public void SortUnits()
        {
            m_units.Sort();
        }

        public void SortOrganizations()
        {
            m_organizations.Sort();
        }

        /// <summary>
        /// Yields all direct child Organizations.
        /// </summary>
        public IEnumerable<TO> Organizations { get { foreach (var organization in m_organizations) yield return organization; } }

        #region IMilitaryOwner Methods

        public IEnumerable<TU> AllUnits
        {
            get
            {
                Stack<AbstractOrganization<TC, TO, TU>> stack = new Stack<AbstractOrganization<TC, TO, TU>>();
                stack.Push(this);

                while (stack.Count > 0)
                {
                    var org = stack.Pop();

                    foreach (var so in org.m_organizations)
                    {
                        stack.Push(so);
                    }

                    foreach (var unit in org.m_units)
                    {
                        yield return unit;
                    }
                }
            }
        }


        public IEnumerable<TC> AllCommanders
        {
            get
            {
                Stack<AbstractOrganization<TC, TO, TU>> stack = new Stack<AbstractOrganization<TC, TO, TU>>();
                stack.Push(this);

                while (stack.Count > 0)
                {
                    var org = stack.Pop();
                    yield return org.Commander;

                    foreach (var so in org.m_organizations)
                    {
                        stack.Push(so);
                    }

                    foreach (var unit in org.m_units)
                    {
                        yield return unit.Commander;
                    }
                }
            }
        }

        public IEnumerable<TO> ThisAndAllOrganizations
        {
            get
            {
                Stack<TO> stack = new Stack<TO>();
                stack.Push(this as TO);

                while (stack.Count > 0)
                {
                    var org = stack.Pop();
                    yield return org;

                    foreach (var so in org.m_organizations)
                    {
                        stack.Push(so);
                    }
                }
            }
        }

        public IEnumerable<TO> AllOrganizations
        {
            get
            {
                Stack<TO> stack = new Stack<TO>();
                stack.Push(this as TO);

                while (stack.Count > 0)
                {
                    var org = stack.Pop();

                    foreach (var so in org.m_organizations)
                    {
                        yield return so;
                        stack.Push(so);
                    }
                }
            }
        }

        /// <summary>
        /// Recursively yields all child Forces.
        /// </summary>
        public IEnumerable<AbstractForce<TC, TO, TU>> AllForces
        {
            get
            {
                foreach (var unit in m_units)
                {
                    yield return unit;
                }
                foreach (var organization in m_organizations)
                {
                    yield return organization;
                    foreach (var force in organization.AllForces)
                    {
                        yield return force;
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// Sets this Organization's parent.  
        /// This method does not add the organization to the parent's list, and should only be called from the parent object itself.
        /// </summary>
        /// <param name="parent">The new parent for this Organization</param>
        public void SetParent(TO parent)
        {
            if (HasParent)
                Parent.RemoveOrganization((TO)this);

            Parent = parent;
        }

        #region Add and Remove methods

        /// <summary>
        /// Detaches unit from its current parent,
        /// sets its parent to be this Organization, and adds it
        /// to this Organization's list of units.
        /// </summary>
        /// <param name="org">The unit to add</param>
        public void AddUnit(TU unit)
        {
            if (unit.Parent == this || this.m_units.Contains(unit))
                throw new ArgumentException("Organization is already parent of argument", "unit");

            unit.SetParent((TO)this);
            m_units.Add(unit);
        }

        /// <summary>
        /// Detaches unit from this Organization.
        /// </summary>
        /// <param name="org">The unit to remove</param>
        public void RemoveUnit(TU unit)
        {
            if (unit.Parent != this || !this.m_units.Contains(unit))
                throw new ArgumentException("Organization is not parent of argument", "unit");

            unit.SetNoParent();
            m_units.Remove(unit);
        }

        /// <summary>
        /// Detaches org from its current parent,
        /// sets its parent to be this Organization, and adds it
        /// to this Organization's list of suborganizations.
        /// </summary>
        /// <param name="org">The organization to add</param>
        public void AddOrganization(TO org)
        {
            if (org.Parent == this || this.m_organizations.Contains(org))
                throw new ArgumentException("Organization is already parent of argument", "org");

            org.SetParent((TO)this);
            m_organizations.Add(org);
        }

        /// <summary>
        /// Detaches org from this Organization.
        /// </summary>
        /// <param name="org">The organization to remove</param>
        public void RemoveOrganization(TO org)
        {
            if (org.Parent != this || !this.m_organizations.Contains(org))
                throw new ArgumentException("Organization is not parent of argument", "org");

            org.SetNoParent();
            m_organizations.Remove(org);
        }
        #endregion

    }

    /// <summary>
    /// Represents an abstract military commander having a command
    /// </summary>
    public abstract class AbstractCommander<TC, TO, TU>
    #region Generics
 : IMilitaryOwner<TC, TO, TU, AbstractForce<TC, TO, TU>>, IMilitaryConstruct
        where TC : AbstractCommander<TC, TO, TU>
        where TO : AbstractOrganization<TC, TO, TU>
        where TU : AbstractUnit<TC, TO, TU>
    #endregion
    {
        public object Tag { get; set; }

        /// <summary>
        /// The Force under this Commander
        /// </summary>
        public AbstractForce<TC, TO, TU> Command { get; set; }

        /// <summary>
        /// Returns true if the Command is a Unit
        /// </summary>
        public bool CommandIsUnit { get { return Command is TU; } }

        /// <summary>
        /// Returns true if the Command is an Organization
        /// </summary>
        public bool CommandIsOrganization { get { return Command is TO; } }

        /// <summary>
        /// Returns the Force under this Commander, if it is a Unit
        /// </summary>
        public TU Unit { get { return (TU)Command; } }

        /// <summary>
        /// Returns the Force under this Commander, if it is an Organization
        /// </summary>
        public TO Organization { get { return (TO)Command; } }

        #region IMilitaryOwner Methods


        /// <summary>
        /// Recursively yields all child Commanders.
        /// </summary>
        public IEnumerable<TC> AllCommanders
        {
            get
            {
                foreach (var force in Organization.AllForces) yield return force.Commander;
            }
        }

        /// <summary>
        /// Recursively yields all child Organizations.
        /// </summary>
        public IEnumerable<TO> AllOrganizations
        {
            get
            {
                if (CommandIsUnit)
                    yield break;
                else
                {
                    yield return Organization;
                    foreach (var organization in Organization.AllOrganizations) yield return organization;
                }
            }
        }

        /// <summary>
        /// Recursively yields all child Units.
        /// </summary>
        public IEnumerable<TU> AllUnits
        {
            get
            {
                if (CommandIsUnit)
                    yield return Unit;
                else
                    foreach (var unit in Organization.AllUnits) yield return unit;
            }
        }

        /// <summary>
        /// Recursively yields all child Forces.
        /// </summary>
        public IEnumerable<AbstractForce<TC, TO, TU>> AllForces
        {
            get
            {
                if (CommandIsUnit)
                    yield return Unit;
                else
                {
                    yield return Organization;
                    foreach (var force in Organization.AllForces) yield return force;
                }
            }
        }

        #endregion
    }

    /// <summary>
    /// Represents an abstract military unit having a parent organization and commander
    /// </summary>
    public abstract class AbstractUnit<TC, TO, TU>
    #region Generics
 : AbstractForce<TC, TO, TU>
        where TC : AbstractCommander<TC, TO, TU>
        where TO : AbstractOrganization<TC, TO, TU>
        where TU : AbstractUnit<TC, TO, TU>
    #endregion
    {
        /// <summary>
        /// Detaches this unit from its previous parent Organization, and attaches it to the new Organization.
        /// </summary>
        /// <param name="parent">The new parent Organization</param>
        public void SetParent(TO parent)
        {
            if (HasParent)
                Parent.RemoveUnit((TU)this);

            Parent = parent;
        }
    }

}
