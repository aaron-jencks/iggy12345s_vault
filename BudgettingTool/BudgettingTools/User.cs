using BudgettingTools.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgettingTools
{
    public class User
    {
        public List<IAccount> Accounts { get; protected set; } = new List<IAccount>();

        public List<IExpense> Expenses { get; protected set; } = new List<IExpense>();
    }
}
