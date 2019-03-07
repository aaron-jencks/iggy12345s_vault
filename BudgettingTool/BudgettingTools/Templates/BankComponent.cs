using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgettingTools.Templates
{
    /// <summary>
    /// An inheritable object that simply adds a string title and description,
    /// it overrides the ToString() method to return the title string.
    /// </summary>
    public class BankComponent
    {
        #region Properties

        /// <summary>
        /// The title used to describe this component
        /// </summary>
        public string Title { get; set; } = "";

        /// <summary>
        /// The description used to give this component context
        /// </summary>
        public string Description { get; set; } = "";

        public DateTime CreationDate { get; private set; } = DateTime.Now;

        /// <summary>
        /// A unique identifier for this object, it is the ComponentCount++
        /// </summary>
        public int ID { get; private set; } = ComponentCount++;

        /// <summary>
        /// Represents the current count of components that have been created.
        /// </summary>
        public static int ComponentCount { get; private set; } = 0;

        #endregion

        #region Methods

        /// <summary>
        /// Assigns a new ID to this bank component
        /// USE AT YOUR OWN RISK
        /// </summary>
        /// <param name="newID">The new integer ID to assign to this component</param>
        public void AssignID(int newID)
        {
            ID = newID;
        }

        /// <summary>
        /// Converts the class into a string
        /// </summary>
        /// <returns>Returns the title property combined with the ID property of this object</returns>
        public override string ToString()
        {
            return Title + ID;
        }

        /// <summary>
        /// Compares this bank component to an object
        /// If the object is an int, it checks the int against its own id
        /// If the object is a string it checks the string against its own title
        /// If the object is a bank component it compares the two objects IDs'
        /// If the object is something else it calls the base object .Equals function
        /// </summary>
        /// <param name="obj">Object to compare this component to</param>
        /// <returns>Returns true if the two objects are considered equal, returns false otherwise.</returns>
        public override bool Equals(object obj)
        {
            if (obj.GetType().Equals(((int)0).GetType()))
                return (int)obj == ID;
            else if (obj.GetType().Equals("".GetType()))
                return ((string)obj) == Title;
            else if (obj.GetType() != GetType())
                return base.Equals(obj);
            else
                return ID == ((BankComponent)obj).ID;
        }

        #endregion
    }
}
