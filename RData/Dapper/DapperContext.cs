using RDATA.Dapper.Implementations;
using RDATA.Dapper.Interfaces;

namespace RDATA.Dapper
{
    public class DapperContext : IDapperContext
    {
        private readonly ISqlConnectionHelper _sqlConnectionHelper;
        public DapperContext(string connectionString)
        {
            _sqlConnectionHelper = new SqlConnectionHelper(connectionString);
        }

        private IProcedureHelper _ProcedureHelper;
        public IProcedureHelper ProcedureHelper
        {
            get
            {
                if (_ProcedureHelper == null)
                    _ProcedureHelper = new ProcedureHelper(_sqlConnectionHelper);
                return _ProcedureHelper;

            }
        }
        private IQueryHelper _QueryHelper;
        public IQueryHelper QueryHelper
        {
            get
            {
                if (_QueryHelper == null)
                    _QueryHelper = new QueryHelper(_sqlConnectionHelper);
                return _QueryHelper;

            }
        }
    }
}
