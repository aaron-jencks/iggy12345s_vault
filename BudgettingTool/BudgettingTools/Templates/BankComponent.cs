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
        /// <summary>
        /// The title used to describe this component
        /// </summary>
        public string Title { get; set; } = "";

        /// <summary>
        /// The description used to give this component context
        /// </summary>
        public string Description { get; set; } = "";

        /// <summary>
        /// A unique identifier for this object, it is the ComponentCount++
        /// </summary>
        public int ID { get; private set; } = ComponentCount++;

        /// <summary>
        /// Represents the current count of components that have been created.
        /// </summary>
        public static int ComponentCount { get; private set; } = 0;

        /// <summary>
        /// Converts the class into a string
        /// </summary>
        /// <returns>Returns the title property combined with the ID property of this object</returns>
        public override string ToString()
        {
            return Title + ID;
        }
    }
}
