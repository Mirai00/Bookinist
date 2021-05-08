using Bookinist.DAL.Context;
using Bookinist.DAL.Entities;
using Bookinist.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bookinist.DAL
{
    internal class DbRepository<T> : IRepository<T> where T : EntityBase, IEntity, new()
    {
        private readonly BookinistDB _db;
        private readonly DbSet<T> _set;

        public bool AutoSaveChanges { get; set; } = true;

        public DbRepository(BookinistDB db)
        {
            _db = db;
            _set = db.Set<T>();
        }

        public virtual IQueryable<T> Items => _set;
        public T Get(int id) => Items.SingleOrDefault(item => item.Id == id);

        public async Task<T> GetAsync(int id, CancellationToken cancel = default) => 
            await Items
                .SingleOrDefaultAsync(item => item.Id == id, cancel)
                .ConfigureAwait(false);

        public T Add(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _db.Entry(item).State = EntityState.Added;
            if(AutoSaveChanges)
                _db.SaveChanges();
            return item;
        }

        public async Task<T> AddAsync(T item, CancellationToken cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _db.Entry(item).State = EntityState.Added;
            if (AutoSaveChanges)
                await _db.SaveChangesAsync(cancel).ConfigureAwait(false);
            return item;
        }

        public void Update(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _db.Entry(item).State = EntityState.Modified;
            if (AutoSaveChanges)
                _db.SaveChanges();
        }

        public async Task UpdateAsync(T item, CancellationToken cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _db.Entry(item).State = EntityState.Modified;
            if (AutoSaveChanges)
                await _db.SaveChangesAsync(cancel).ConfigureAwait(false);
        }

        public void Remove(int id)
        {
            //var item = Get(id);
            //if (item is null) return;
            //_db.Entry(item);

            _db.Remove(new T {Id = id});
            if (AutoSaveChanges)
                _db.SaveChanges();


        }

        public async Task RemoveAsync(int id, CancellationToken cancel = default)
        {
            _db.Remove(new T { Id = id });
            if (AutoSaveChanges)
                await _db.SaveChangesAsync(cancel).ConfigureAwait(false);
        }
    }
    class BooksRepository : DbRepository<Book>
    {
        public override IQueryable<Book> Items => base.Items.Include(Items => Items.Category);

        public BooksRepository(BookinistDB db) : base(db)
        {

        }
    }
    class DealsRepository : DbRepository<Deal>
    {
        public override IQueryable<Deal> Items => base.Items
            .Include(Items => Items.Book)
            .Include(Items => Items.Seller)
            .Include(Items => Items.Buyer)
        ;

        public DealsRepository(BookinistDB db) : base(db)
        {

        }
    }
}
