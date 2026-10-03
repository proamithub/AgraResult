using System;

namespace RRepo
{
    public interface IUnitOfWork : IDisposable
    {
        AdminMasterRepository IAdminMaster { get; }
        int SaveChanges();
    }
}