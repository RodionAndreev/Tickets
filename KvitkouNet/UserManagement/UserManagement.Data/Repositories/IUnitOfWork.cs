using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Data.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        Task SaveChangesAsync();
        IAccountRepository Accounts { get; }
        IProfileRepository Profiles { get; }
        IUserRepository Users { get; }
        IGroupRepository Groups { get; }
    }
}
