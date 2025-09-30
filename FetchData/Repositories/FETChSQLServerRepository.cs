using FetchData.Data;
using FetchData.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FetchData.Repositories
{
    public class FETChSQLServerRepository : BaseSQLServrRepository<FETChDbContext>, IFETChRepository
    {
        public FETChSQLServerRepository(FETChDbContext db) : base(db)
        {
            Db = db;
        }
    }
}
