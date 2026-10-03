
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RDATA.Dapper.Interfaces
{
    public interface IQueryHelper
    {
        Task<T> QueryFirstOrDefaultAsync<T>(string sql, object parameters = null);
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object parameters = null);
    }
}
