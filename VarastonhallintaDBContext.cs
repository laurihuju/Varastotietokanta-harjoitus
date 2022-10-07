using Microsoft.EntityFrameworkCore;

namespace Varastotietokanta_harjoitus
{
    class VarastonhallintaDBContext : DbContext
    {
        private string address;
        private string user;
        private string password;

        public VarastonhallintaDBContext(string address, string user, string password)
        {
            this.address = address;
            this.user = user;
            this.password = password;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer($"Server={address};Database=Varastonhallinta;User Id={user};Password={password};");
        }
    }
}
