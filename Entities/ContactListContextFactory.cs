using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ContactListContextFactory : IDesignTimeDbContextFactory<ContactListContext>
    {
        public ContactListContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ContactListContext>();
            optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=ContactListDB;Integrated Security=true; User ID=postgres;Password=muratyasar;Pooling=true;");

            return new ContactListContext(optionsBuilder.Options);
        }
    }
}
