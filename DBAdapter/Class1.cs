using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAdapter
{
    class Class1
    {
        public void DO()
        {
            using (var context = new WalletContext())
            {
                context.Users.Where(t => t.Guid == Guid.Empty).OrderBy(t => t.FirstName);
            }
        }
    }
}
