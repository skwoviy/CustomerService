using DAL.Contracts;
using DAL.Repositories;
using DbContext.Models;
using System;
using System.Threading.Tasks;

namespace DAL.UnitOfWork
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly CustomerServiceContext _dbContext;

        private CurrencyRepository _currencyRepository;
        private CustomerRepository _customerRepository;
        private StatusRepository _statusRepository;
        private TransactionRepository _transactionRepository;

        public EFUnitOfWork(CustomerServiceContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IRepository<Currency> Currencies
        {
            get
            {
                if (_currencyRepository == null)
                {
                    _currencyRepository = new CurrencyRepository(_dbContext);
                }

                return _currencyRepository;
            }
        }

        public IRepository<Customer> Customers
        {
            get
            {
                if (_customerRepository == null)
                {
                    _customerRepository = new CustomerRepository(_dbContext);
                }

                return _customerRepository;
            }
        }

        public IRepository<Status> Statuses
        {
            get
            {
                if (_statusRepository == null)
                {
                    _statusRepository = new StatusRepository(_dbContext);
                }

                return _statusRepository;
            }
        }

        public IRepository<Transaction> Transactions
        {
            get
            {
                if (_transactionRepository == null)
                {
                    _transactionRepository = new TransactionRepository(_dbContext);
                }

                return _transactionRepository;
            }
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
