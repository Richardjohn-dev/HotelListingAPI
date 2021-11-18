using HotelListingAPI.Data;
using HotelListingAPI.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListingAPI.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _ctx;
        private IGenericRepository<Country> _countries;
        private IGenericRepository<Hotel> _hotels;

        public UnitOfWork(DatabaseContext context)
        {
            _ctx = context;
        }

        // any new entites etc need a representation here in IUnitOfWork and UnitOfWork.cs
        public IGenericRepository<Country> Countries => _countries ??= new GenericRepository<Country>(_ctx);

        public IGenericRepository<Hotel> Hotels => _hotels ??= new GenericRepository<Hotel>(_ctx);

        public void Dispose()
        {
            // like garbage collector
            _ctx.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _ctx.SaveChangesAsync();
        }
    }
}
