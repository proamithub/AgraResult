using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using RDATA.Dapper.Interfaces;
using RDATA.Dapper.Models;

namespace RDATA.Dapper.Implementations
{
    public class ProcedureHelper : IProcedureHelper
    {
        public ProcedureHelper(ISqlConnectionHelper sqlConnectionProvider)
        {
            _sqlConnectionProvider = sqlConnectionProvider;
        }
        private readonly ISqlConnectionHelper _sqlConnectionProvider;
        public async Task<dynamic> QueryMultipleAsync(string storedProcedure, IEnumerable<MapItem> mapItems = null, object parameters = null)
        {
            var data = new ExpandoObject();
            if (mapItems == null) return data;

            using (var connection = _sqlConnectionProvider.GetDbConnection())
            {
                if (parameters != null)
                {
                    var multi = await connection.QueryMultipleAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
                    foreach (var item in mapItems)
                    {
                        if (item.DataRetriveType == EnumDataRetriveType.FirstOrDefault)
                        {
                            var singleItem = multi.Read(item.Type).FirstOrDefault();
                            ((IDictionary<string, object>)data).Add(item.PropertyName, singleItem);
                        }
                        else if (item.DataRetriveType == EnumDataRetriveType.List)
                        {
                            var listItem = multi.Read(item.Type).ToList();
                            ((IDictionary<string, object>)data).Add(item.PropertyName, listItem);
                        }
                    }
                    return data;
                }
                else
                {
                    var multi = await connection.QueryMultipleAsync(storedProcedure, commandType: CommandType.StoredProcedure);
                    foreach (var item in mapItems)
                    {
                        if (item.DataRetriveType == EnumDataRetriveType.FirstOrDefault)
                        {
                            var singleItem = multi.Read(item.Type).FirstOrDefault();
                            ((IDictionary<string, object>)data).Add(item.PropertyName, singleItem);
                        }
                        else if (item.DataRetriveType == EnumDataRetriveType.List)
                        {
                            var listItem = multi.Read(item.Type).ToList();
                            ((IDictionary<string, object>)data).Add(item.PropertyName, listItem);
                        }
                    }
                    return data;
                }
            }
        }
        public async Task<IEnumerable<T>> QueryAsync<T>(string storedProcedure, object parameters = null)
        {
            using (var connection = _sqlConnectionProvider.GetDbConnection())
            {
                if (parameters != null)
                {
                    return await connection.QueryAsync<T>(storedProcedure, parameters,
                          commandType: CommandType.StoredProcedure);
                }
                else
                {
                    return await connection.QueryAsync<T>(storedProcedure,
                        commandType: CommandType.StoredProcedure);
                }
            }
        }
        public async Task<T> QueryFirstOrDefaultAsync<T>(string storedProcedure, object parameters = null)
        {
            using (var connection = _sqlConnectionProvider.GetDbConnection())
            {
                if (parameters != null)
                {
                    return await connection.QueryFirstOrDefaultAsync<T>(storedProcedure, parameters,
                          commandType: CommandType.StoredProcedure);
                }
                else
                {
                    return await connection.QueryFirstOrDefaultAsync<T>(storedProcedure,
                        commandType: CommandType.StoredProcedure);
                }
            }
        }
        public async Task<int> ExecuteCommandAsync(string storedProcedure, object parameters = null)
        {
            using (var connection = _sqlConnectionProvider.GetDbConnection())
            {
                return await connection.ExecuteAsync(storedProcedure, parameters,
                      commandType: CommandType.StoredProcedure);
            }
        }
    }
}