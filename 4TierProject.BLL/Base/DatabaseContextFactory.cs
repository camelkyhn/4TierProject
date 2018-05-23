using _4TierProject.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4TierProject.BLL.Base
{
    public class DatabaseContextFactory : DatabaseContext
    {
        protected DatabaseContext context = new DatabaseContext();

        public static DatabaseContext CreateDB() { return new DatabaseContextFactory(); }
    }
}
