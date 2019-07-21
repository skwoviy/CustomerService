using System;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();

        Task SaveAsync();
    }
}
