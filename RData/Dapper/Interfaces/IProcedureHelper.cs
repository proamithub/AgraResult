
using RDATA.Dapper.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RDATA.Dapper.Interfaces
{
    public interface IProcedureHelper
    {
        Task<IEnumerable<T>> QueryAsync<T>(string storedProcedure, object parameters = null);
        Task<T> QueryFirstOrDefaultAsync<T>(string storedProcedure, object parameters = null);
        Task<dynamic> QueryMultipleAsync(string storedProcedure, IEnumerable<MapItem> mapItems = null, object parameters = null);
        Task<int> ExecuteCommandAsync(string storedProcedure, object parameters);
    }
}
