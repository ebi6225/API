using Base.Web.API.Tools;
using Microsoft.EntityFrameworkCore;

namespace API.Entities.Model
{
    public class QueryMaker : QueryHelper
    {
        public static QueryMaker Instance
        {
            get
            {
                return Nested.QueryMaker;
            }
        }

        private class Nested
        {
            internal static readonly QueryMaker QueryMaker;

            static Nested()
            {
                QueryMaker = new QueryMaker();
            }
        }
        //Following all need adding table names
        public DbSet<ContactInfo> ContactInfo { get; set; }        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}