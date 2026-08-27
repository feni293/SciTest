using SCITest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCITest.Application.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<Product> CreateAsync(Product product, CancellationToken cancellationToken);

        Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken);

        Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken);

        Task<bool> UpdateAsync(Product product, CancellationToken cancellationToken);

        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
