using Audree.Structure.Core.Contracts.IUnitOfWork;
using Audree.Structure.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Audree.Structure.Infrastructure.UnitOfWork
{
     public class UnitOfWork : IUnitOfWork
    {

        private readonly Context _databaseContext;
        private bool _disposed = false;
        public UnitOfWork(Context databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public void Commit()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }
            _databaseContext.SaveChanges();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing && _databaseContext != null)
            {
                _databaseContext.Dispose();
            }
            _disposed = true;
        }
    }
}
    