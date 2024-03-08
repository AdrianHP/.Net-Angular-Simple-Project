using AutoMapper;
using Data.Exceptions;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces.CoreInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Clases
{
    public class DataCRUDService<TContext> : IDataCRUDService<TContext>
        where TContext : IDataDbContext
    {
        private readonly TContext _context;
        private readonly IMapper _mapper;

        public TContext Context => _context;
        public IMapper Mapper => _mapper;

        public DataCRUDService(TContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TDTO> Create<TEntity, TDTO>(TDTO dto, CancellationToken ct)
            where TEntity : class
            where TDTO : class
        {
            var entity = _mapper.Map<TEntity>(dto);
            await _context.Set<TEntity>().AddAsync(entity, ct);
            await _context.SaveChangesAsync(ct);
            return _mapper.Map<TDTO>(entity);
        }

        public async Task<TDTO> Create<TEntity, TDTO>(TDTO dto, Action<TEntity, TContext> beforeSave, CancellationToken ct)
            where TEntity : class
            where TDTO : class
        {
            var entity = _mapper.Map<TEntity>(dto);
            await _context.Set<TEntity>().AddAsync(entity, ct);

            beforeSave?.Invoke(entity, _context);

            await _context.SaveChangesAsync(ct);
            return _mapper.Map<TDTO>(entity);
        }

        public async Task<TDTO> Update<TEntity, TDTO, TKey>(TDTO dto, CancellationToken ct)
            where TKey : IEquatable<TKey>
            where TEntity : class, IEntity<TKey>
            where TDTO : class, IDTO<TKey>
        {
            var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id.Equals(dto.Id), ct);
            if (entity == null)
                throw new EntityNotFoundException();

            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync(ct);
            return _mapper.Map<TDTO>(entity);
        }

        public async Task<TDTO> Update<TEntity, TDTO, TKey>(TDTO dto, Action<TEntity, TContext> beforeSave, CancellationToken ct)
            where TKey : IEquatable<TKey>
            where TEntity : class, IEntity<TKey>
            where TDTO : class, IDTO<TKey>
        {
            var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id.Equals(dto.Id), ct);
            if (entity == null)
                throw new EntityNotFoundException();

            _mapper.Map(dto, entity);

            beforeSave?.Invoke(entity, _context);

            await _context.SaveChangesAsync(ct);
            return _mapper.Map<TDTO>(entity);
        }

        //public async Task<TDTO> Get<TEntity, TDTO, TKey>(TKey id, IEntityQuery<TEntity> queryHandler, CancellationToken ct = default)
        //    where TKey : IEquatable<TKey>
        //    where TEntity : class, IEntity<TKey>
        //    where TDTO : class, IDTO<TKey>
        //{
        //    var query = queryHandler?.GetEntityQuery(_context.Set<TEntity>()) ?? _context.Set<TEntity>();
        //    return _mapper.Map<TDTO>(await query.FirstOrDefaultAsync(o => o.Id.Equals(id), ct));
        //}

        public async Task<TDTO> Get<TEntity, TDTO, TKey>(TKey id, CancellationToken ct = default)
            where TKey : IEquatable<TKey>
            where TEntity : class, IEntity<TKey>
            where TDTO : class, IDTO<TKey>
            => _mapper.Map<TDTO>(await _context.Set<TEntity>().FirstOrDefaultAsync(o => o.Id.Equals(id), ct));

        public async Task<TDTO> Get<TEntity, TDTO, TKey>(TKey id, Func<IQueryable<TEntity>, IQueryable<TEntity>> setQuery, CancellationToken ct = default)
            where TKey : IEquatable<TKey>
            where TEntity : class, IEntity<TKey>
            where TDTO : class, IDTO<TKey>
            => _mapper.Map<TDTO>(await setQuery(_context.Set<TEntity>()).FirstOrDefaultAsync(o => o.Id.Equals(id), ct));

        public async Task<TDTO> Get<TEntity, TDTO>(
            Func<IQueryable<TEntity>, IQueryable<TEntity>> setQuery, CancellationToken ct = default)
            where TEntity : class
            where TDTO : class
            => _mapper.Map<TDTO>(await setQuery(_context.Set<TEntity>()).FirstOrDefaultAsync(ct));

        //public Task<TDTO> Get<TEntity, TDTO>(int id, IEntityQuery<TEntity> queryHandler, CancellationToken ct = default)
        //    where TEntity : class, IEntity
        //    where TDTO : class, IDTO
        //    => Get<TEntity, TDTO, int>(id, queryHandler, ct);

        public async Task<IEnumerable<TDTO>> GetAll<TEntity, TDTO>(CancellationToken ct = default)
            where TEntity : class
            where TDTO : class
            => _mapper.Map<IEnumerable<TDTO>>(await _context.Set<TEntity>().ToListAsync(ct));

        public async Task<IEnumerable<TDTO>> GetAll<TEntity, TDTO>(
                Func<IQueryable<TEntity>, IQueryable<TEntity>> setQuery, CancellationToken ct = default)
            where TEntity : class
            where TDTO : class
            => _mapper.Map<IEnumerable<TDTO>>(await setQuery(_context.Set<TEntity>()).ToListAsync(ct));

        public async Task Delete<TEntity, TKey>(TKey id, Action<TEntity, TContext> beforeSave, CancellationToken ct = default)
            where TKey : IEquatable<TKey>
            where TEntity : class, IEntity<TKey>
        {
            var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(o => o.Id.Equals(id), ct);
            if (entity != null)
            {
                _context.Set<TEntity>().Remove(entity);
                beforeSave?.Invoke(entity, _context);
                await _context.SaveChangesAsync(ct);
            }
        }

        public Task Delete<TEntity, TKey>(TKey id, CancellationToken ct = default)
            where TKey : IEquatable<TKey>
            where TEntity : class, IEntity<TKey>
            => Delete<TEntity, TKey>(id, null, ct);

        public async Task DeleteAll<TEntity>(CancellationToken ct = default)
            where TEntity : class
        {
            _context.Set<TEntity>().RemoveRange(_context.Set<TEntity>());
            await _context.SaveChangesAsync(ct);
        }

        public async Task DeleteAll<TEntity>(
                Func<IQueryable<TEntity>, IQueryable<TEntity>> setQuery, CancellationToken ct = default)
            where TEntity : class
        {
            _context.Set<TEntity>().RemoveRange(setQuery(_context.Set<TEntity>()));
            await _context.SaveChangesAsync(ct);
        }

        public Task<bool> Any<TEntity>(Func<IQueryable<TEntity>, IQueryable<TEntity>> setQuery, CancellationToken ct = default)
            where TEntity : class
            => setQuery(_context.Set<TEntity>()).AnyAsync(ct);
    }
}
