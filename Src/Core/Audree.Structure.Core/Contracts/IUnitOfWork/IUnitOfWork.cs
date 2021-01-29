using System;
using System.Collections.Generic;
using System.Text;

namespace Audree.Structure.Core.Contracts.IUnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
