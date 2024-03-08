using AutoMapper;
using Data.Interfaces.CoreInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.CoreInterfaces
{
    /// <summary>
    /// Defines a list of generalized methods to implement CRUD/paging operations with database context.
    /// </summary>
    /// <remarks>
    /// This interface is mainly supposed to be implemented by <see cref="DataService"/> class in order to provide
    /// default and comprehensive implementation of CRUD/paging capabilities.
    /// </remarks>
    /// <typeparam name="TContext">Database context type.</typeparam>
    public partial interface IDataService<TContext>
        where TContext : IApiDbContext
    {
        /// <summary>
        /// Database context that the service works with.
        /// </summary>
        TContext Context { get; }

        /// <summary>
        /// Entities and DTO mapper that the service works with.
        /// </summary>
        IMapper Mapper { get; }

        /// <summary>
        /// Creates entity by DTO.
        /// </summary>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <typeparam name="TDTO">DTO type.</typeparam>
        /// <param name="dto">DTO of entity for creating.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>DTO of created entity.</returns>
        Task<TDTO> Create<TEntity, TDTO>(TDTO dto, CancellationToken ct)
            where TEntity : class
            where TDTO : class;

        /// <summary>
        /// Creates entity by DTO with ability to run additional actions before submitting.
        /// </summary>
        /// <remarks>
        /// Code sample:
        /// <code>
        /// dataService.Create&lt;Order, OrderDTO&gt;(dto, (entity, ctx) => { entity.CreatedOn = DateTime.Now; }, ct);
        /// </code>
        /// </remarks>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <typeparam name="TDTO">DTO type.</typeparam>
        /// <param name="dto">DTO of entity for creating.</param>
        /// <param name="beforeSave">Callback method to run additional actions before submitting.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>DTO of created entity.</returns>
        Task<TDTO> Create<TEntity, TDTO>(TDTO dto, Action<TEntity, TContext> beforeSave, CancellationToken ct)
            where TEntity : class
            where TDTO : class;

        /// <summary>
        /// Updates entity by DTO.
        /// </summary>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <typeparam name="TDTO">DTO type.</typeparam>
        /// <typeparam name="TKey">ID field type of entity.</typeparam>
        /// <param name="dto">DTO of entity for updating.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>DTO of updated entity.</returns>
        Task<TDTO> Update<TEntity, TDTO, TKey>(TDTO dto, CancellationToken ct)
            where TKey : IEquatable<TKey>
            where TEntity : class, IEntity<TKey>
            where TDTO : class, IDTO<TKey>;

        /// <summary>
        /// Updates entity by DTO with ability to run additional actions before submitting.
        /// </summary>
        /// <remarks>
        /// <i>Code sample:
        /// <code>
        /// dataService.Update&lt;User, UserDTO, string&gt;(dto, (entity, ctx) => { entity.UpdatedOn = DateTime.Now; }, ct);
        /// </code></i>
        /// </remarks>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <typeparam name="TDTO">DTO type.</typeparam>
        /// <typeparam name="TKey">ID field type of entity.</typeparam>
        /// <param name="dto">DTO of entity for updating.</param>
        /// <param name="beforeSave">Callback method to run additional actions before submitting.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>DTO of updated entity.</returns>
        Task<TDTO> Update<TEntity, TDTO, TKey>(TDTO dto, Action<TEntity, TContext> beforeSave, CancellationToken ct)
            where TKey : IEquatable<TKey>
            where TEntity : class, IEntity<TKey>
            where TDTO : class, IDTO<TKey>;

        /// <summary>
        /// Updates entity by DTO.
        /// Method variation for entities with integer ID field.
        /// </summary>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <typeparam name="TDTO">DTO type.</typeparam>
        /// <param name="dto">DTO of entity for updating.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>DTO of updated entity.</returns>
        //Task<TDTO> Update<TEntity, TDTO>(TDTO dto, CancellationToken ct)
        //    where TEntity : class, IEntity
        //    where TDTO : class, IDTO;

        /// <summary>
        /// Updates entity by DTO with ability to run additional actions before submitting.
        /// Method variation for entities with integer ID field.
        /// </summary>
        /// <remarks>
        /// <i>Code sample:
        /// <code>
        /// dataService.Update&lt;Order, OrderDTO&gt;(dto, (entity, ctx) => { entity.UpdatedOn = DateTime.Now; }, ct);
        /// </code></i>
        /// </remarks>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <typeparam name="TDTO">DTO type.</typeparam>
        /// <param name="dto">DTO of entity for updating.</param>
        /// <param name="beforeSave">Callback method to run additional actions before submitting.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>DTO of updated entity.</returns>
        //Task<TDTO> Update<TEntity, TDTO>(TDTO dto, Action<TEntity, TContext> beforeSave, CancellationToken ct)
        //    where TEntity : class, IEntity
        //    where TDTO : class, IDTO;

        /// <summary>
        /// Gets entity by ID.
        /// </summary>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <typeparam name="TDTO">DTO type.</typeparam>
        /// <typeparam name="TKey">ID field type of entity.</typeparam>
        /// <param name="id">ID of requested entity.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>DTO of requested entity.</returns>
        Task<TDTO> Get<TEntity, TDTO, TKey>(TKey id, CancellationToken ct = default)
            where TKey : IEquatable<TKey>
            where TEntity : class, IEntity<TKey>
            where TDTO : class, IDTO<TKey>;

        /// <summary>
        /// Gets entity by ID with ability to customize query via handler which implements <c>IEntityQuery</c>.
        /// </summary>
        /// <remarks>
        /// <i>Code sample:
        /// <code>
        /// dataService.Get&lt;User, UserDTO, string&gt;(id, userDataService, ct);
        /// </code></i>
        /// </remarks>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <typeparam name="TDTO">DTO type.</typeparam>
        /// <typeparam name="TKey">ID field type of entity.</typeparam>
        /// <param name="id">ID of requested entity.</param>
        /// <param name="queryHandler">Handler which implements <c>IEntityQuery</c> to customize query.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>DTO of requested entity.</returns>
        //Task<TDTO> Get<TEntity, TDTO, TKey>(TKey id, IEntityQuery<TEntity> queryHandler, CancellationToken ct = default)
        //    where TKey : IEquatable<TKey>
        //    where TEntity : class, IEntity<TKey>
        //    where TDTO : class, IDTO<TKey>;

        /// <summary>
        /// Gets entity by ID with ability to customize query.
        /// </summary>
        /// <remarks>
        /// <i>Code sample:
        /// <code>
        /// dataService.Get&lt;User, UserDTO, string&gt;(id, query => query.Include(o => o.Company), ct);
        /// </code></i>
        /// </remarks>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <typeparam name="TDTO">DTO type.</typeparam>
        /// <typeparam name="TKey">ID field type of entity.</typeparam>
        /// <param name="id">ID of requested entity.</param>
        /// <param name="setQuery">Function method which customizes query.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>DTO of requested entity.</returns>
        Task<TDTO> Get<TEntity, TDTO, TKey>(TKey id, Func<IQueryable<TEntity>, IQueryable<TEntity>> setQuery, CancellationToken ct = default)
            where TKey : IEquatable<TKey>
            where TEntity : class, IEntity<TKey>
            where TDTO : class, IDTO<TKey>;

        /// <summary>
        /// Gets entity by ID.
        /// Method variation for entities with integer ID field.
        /// </summary>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <typeparam name="TDTO">DTO type.</typeparam>
        /// <param name="id">ID of requested entity.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>DTO of requested entity.</returns>
        //Task<TDTO> Get<TEntity, TDTO>(int id, CancellationToken ct = default)
        //    where TEntity : class, IEntity
        //    where TDTO : class, IDTO;

        /// <summary>
        /// Gets entity by ID with ability to customize query via handler which implements <c>IEntityQuery</c>.
        /// Method variation for entities with integer ID field.
        /// <para>In particular, this method is used by base data controller to automate CRUD/paging. In most other cases it maybe easier to
        /// use Get(Func... setQuery) using function method to customize the query.</para>
        /// </summary>
        /// <remarks>
        /// <i>Code sample:
        /// <code>
        /// dataService.Get&lt;Order, OrderDTO&gt;(id, orderDataService, ct);
        /// </code></i>
        /// </remarks>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <typeparam name="TDTO">DTO type.</typeparam>
        /// <param name="id">ID of requested entity.</param>
        /// <param name="queryHandler">Handler which implements <c>IEntityQuery</c> to customize query.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>DTO of requested entity.</returns>
        //Task<TDTO> Get<TEntity, TDTO>(int id, IEntityQuery<TEntity> queryHandler, CancellationToken ct = default)
        //    where TEntity : class, IEntity
        //    where TDTO : class, IDTO;

        /// <summary>
        /// Gets entity by ID with ability to customize query.
        /// Method variation for entities with integer ID field.
        /// </summary>
        /// <remarks>
        /// <i>Code sample:
        /// <code>
        /// dataService.Get&lt;Order, OrderDTO&gt;(id, query => query.Include(o => o.OrderDetails), ct);
        /// </code></i>
        /// </remarks>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <typeparam name="TDTO">DTO type.</typeparam>
        /// <param name="id">ID of requested entity.</param>
        /// <param name="setQuery">Function method which customizes query.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>DTO of requested entity.</returns>
        //Task<TDTO> Get<TEntity, TDTO>(int id, Func<IQueryable<TEntity>, IQueryable<TEntity>> setQuery, CancellationToken ct = default)
        //    where TEntity : class, IEntity
        //    where TDTO : class, IDTO;

        /// <summary>
        /// Gets entity by customized query. The query is fully responsible for finding the entity record.
        /// </summary>
        /// <remarks>
        /// <i>Code sample:
        /// <code>
        /// dataService.Get&lt;User, UserDTO&gt;(query => query.Where(o => o.Email == email), ct);
        /// </code></i>
        /// </remarks>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <typeparam name="TDTO">DTO type.</typeparam>
        /// <param name="setQuery">Function method which customizes query. The query is fully responsible for finding the entity record.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>DTO of requested entity.</returns>
        Task<TDTO> Get<TEntity, TDTO>(Func<IQueryable<TEntity>, IQueryable<TEntity>> setQuery, CancellationToken ct = default)
            where TEntity : class
            where TDTO : class;

        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <typeparam name="TDTO">DTO type.</typeparam>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>A list of all entities DTOs.</returns>
        Task<IEnumerable<TDTO>> GetAll<TEntity, TDTO>(CancellationToken ct = default)
            where TEntity : class
            where TDTO : class;

        /// <summary>
        /// Gets all entities with ability to filter records.
        /// </summary>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <typeparam name="TDTO">DTO type.</typeparam>
        /// <param name="filter">A filter with settings that determine the requested filtered entities.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>A list of all entities DTOs that match the filter.</returns>
        //Task<IEnumerable<TDTO>> GetAll<TEntity, TDTO>(Filter filter, CancellationToken ct = default)
        //    where TEntity : class
        //    where TDTO : class;

        /// <summary>
        /// Gets all entities with ability to customize query.
        /// </summary>
        /// <remarks>
        /// <i>Code sample:
        /// <code>
        /// dataService.GetAll&lt;Order, OrderDTO&gt;(query => query.Include(o => o.OrderDetails), ct);
        /// </code></i>
        /// </remarks>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <typeparam name="TDTO">DTO type.</typeparam>
        /// <param name="setQuery">Function method which customizes query.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>A list of all entities DTOs.</returns>
        Task<IEnumerable<TDTO>> GetAll<TEntity, TDTO>(
                Func<IQueryable<TEntity>, IQueryable<TEntity>> setQuery, CancellationToken ct = default)
            where TEntity : class
            where TDTO : class;

        /// <summary>
        /// Deletes entity by ID.
        /// </summary>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <typeparam name="TKey">ID field type of entity.</typeparam>
        /// <param name="id">ID of entity.</param>
        /// <param name="ct">Cancellation token.</param>
        Task Delete<TEntity, TKey>(TKey id, CancellationToken ct = default)
            where TKey : IEquatable<TKey>
            where TEntity : class, IEntity<TKey>;

        /// <summary>
        /// Deletes entity by ID with ability to run additional actions before submitting.
        /// </summary>
        /// <remarks>
        /// <i>Code sample:
        /// <code>
        /// dataService.Delete&lt;User, string&gt;(id,
        ///     (entity, ctx) => { if (entity.IsReadonly) throw new BusinessException("Can't delete!"); }, ct);
        /// </code></i>
        /// </remarks>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <typeparam name="TKey">ID field type of entity.</typeparam>
        /// <param name="id">ID of entity.</param>
        /// <param name="beforeSave">Callback method to run additional actions.</param>
        /// <param name="ct">Cancellation token.</param>
        Task Delete<TEntity, TKey>(TKey id, Action<TEntity, TContext> beforeSave, CancellationToken ct = default)
            where TKey : IEquatable<TKey>
            where TEntity : class, IEntity<TKey>;

        /// <summary>
        /// Deletes entity by ID.
        /// Method variation for entities with integer ID field.
        /// </summary>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <param name="id">ID of entity.</param>
        /// <param name="ct">Cancellation token.</param>
        //Task Delete<TEntity>(int id, CancellationToken ct = default)
        //    where TEntity : class, IEntity;

        /// <summary>
        /// Deletes entity by ID with ability to run additional actions before submitting.
        /// Method variation for entities with integer ID field.
        /// </summary>
        /// <remarks>
        /// <i>Code sample:
        /// <code>
        /// dataService.Delete&lt;Order&gt;(id,
        ///     (entity, ctx) => { if (entity.IsReadonly) throw new BusinessException("Can't delete!"); }, ct);
        /// </code></i>
        /// </remarks>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <param name="id">ID of entity.</param>
        /// <param name="beforeSave">Callback method to run additional actions.</param>
        /// <param name="ct">Cancellation token.</param>
        //Task Delete<TEntity>(int id, Action<TEntity, TContext> beforeSave, CancellationToken ct = default)
        //    where TEntity : class, IEntity;

        /// <summary>
        /// Deletes all entities.
        /// </summary>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <param name="ct">Cancellation token.</param>
        Task DeleteAll<TEntity>(CancellationToken ct = default)
            where TEntity : class;

        /// <summary>
        /// Deletes all entities by customized query. The query is fully responsible for filtering records for deleting.
        /// </summary>
        /// <remarks>
        /// <i>Code sample:
        /// <code>
        /// dataService.DeleteAll&lt;Order&gt;(query => query.Where(o => o.IsPaid), ct);
        /// </code></i>
        /// </remarks>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <param name="setQuery">Function method which customizes query. The query is fully responsible for filtering records for deleting.</param>
        /// <param name="ct">Cancellation token.</param>
        Task DeleteAll<TEntity>(Func<IQueryable<TEntity>, IQueryable<TEntity>> setQuery, CancellationToken ct = default)
            where TEntity : class;

        /// <summary>
        /// Gets a paged list of entities by query command with ability to customize query via handler which implements <c>IEntityQuery</c>.
        /// <para>In particular, this method is used by base data controller to automate CRUD/paging. In most other cases it maybe easier to
        /// use GetPage(Func... setQuery) using function method to customize the query.</para>
        /// </summary>
        /// <remarks>
        /// A paged list usually requested for data grids binding.
        /// Paged list's records are determined by page number, page size, filtering and sorting settings.
        /// <para><i>Code sample:
        /// <code>
        /// dataService.GetPage&lt;Order, OrderDTO&gt;(command, orderDataService, ct);
        /// </code></i></para>
        /// </remarks>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <typeparam name="TDTO">DTO type.</typeparam>
        /// <param name="command">A query command with settings that determine the requested paged list.</param>
        /// <param name="queryHandler">Handler which implements <c>IEntityQuery</c> to customize query.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <param name="mappingOnDatabase">When set to <c>True</c>, the method is supposed to use query projecting (<c>ProjectTo()</c>).
        /// <para>The projecting tells AutoMapper’s mapping engine to emit a select clause to the IQueryable that will inform entity
        /// framework that it only needs to query mapped columns of the table, same as if you manually projected your
        /// IQueryable to an DTO with a Select clause.</para>
        /// <para>Finally, that means that you query only mapped fields from the database, but not the whole DTO object and so
        /// you may significantly reduce requested data size for complex DTOs.</para>
        /// </param>
        /// <returns>Paged list of DTOs of entities.</returns>
        //Task<PageResult<TDTO>> GetPage<TEntity, TDTO>(
        //        QueryCommand command,
        //        IEntityQuery<TEntity> queryHandler,
        //        CancellationToken ct = default,
        //        bool mappingOnDatabase = false)
        //    where TEntity : class
        //    where TDTO : class;

        /// <summary>
        /// Gets a paged list of entities by query command with ability to customize query, filter and sorter.
        /// </summary>
        /// <remarks>
        /// A paged list usually requested for data grids binding.
        /// Paged list's records are determined by page number, page size, filtering and sorting settings.
        /// <para><i>Code sample:
        /// <code>
        /// dataService.GetPage&lt;Order, OrderDTO&gt;(command,
        ///     query => query.Include(o => o.OrderDetails),
        ///     queryFilter => queryFilter.Handle("isOld", (query, filter) => ...customize query's WHERE...),
        ///     (query, sorter) => ...customize query's ORDER BY...,
        ///     ct);
        /// </code></i></para>
        /// </remarks>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <typeparam name="TDTO">DTO type.</typeparam>
        /// <param name="command">A query command with settings that determine the requested paged list.</param>
        /// <param name="setQuery">Function method which customizes query.</param>
        /// <param name="filter">Function method which customizes entities filtering.</param>
        /// <param name="sorter">Function method which customizes entities sorting.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <param name="mappingOnDatabase">When set to <c>True</c>, the method is supposed to use query projecting (<c>ProjectTo()</c>).
        /// <para>The projecting tells AutoMapper’s mapping engine to emit a select clause to the IQueryable that will inform entity
        /// framework that it only needs to query mapped columns of the table, same as if you manually projected your
        /// IQueryable to an DTO with a Select clause.</para>
        /// <para>Finally, that means that you query only mapped fields from the database, but not the whole DTO object and so
        /// you may significantly reduce requested data size for complex DTOs.</para>
        /// </param>
        /// <returns>Paged list of DTOs of entities.</returns>
        //Task<PageResult<TDTO>> GetPage<TEntity, TDTO>(
        //        QueryCommand command,
        //        Func<IQueryable<TEntity>, IQueryable<TEntity>> setQuery = null,
        //        Func<QueryFilter<TEntity>, QueryFilter<TEntity>> filter = null,
        //        Func<IQueryable<TEntity>, ISorter, IQueryable<TEntity>> sorter = null,
        //        CancellationToken ct = default,
        //        bool mappingOnDatabase = false)
        //    where TEntity : class
        //    where TDTO : class;

        /// <summary>
        /// Gets a paged list of entities by query command with ability to customize query.
        /// </summary>
        /// <remarks>
        /// A paged list usually requested for data grids binding.
        /// Paged list's records are determined by page number, page size, filtering and sorting settings.
        /// <para><i>Code sample:
        /// <code>
        /// dataService.GetPage&lt;Order, OrderDTO&gt;(command, query => query.Include(o => o.OrderDetails), ct);
        /// </code></i></para>
        /// </remarks>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <typeparam name="TDTO">DTO type.</typeparam>
        /// <param name="command">A query command with settings that determine the requested paged list.</param>
        /// <param name="setQuery">Function method which customizes query.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <param name="mappingOnDatabase">When set to <c>True</c>, the method is supposed to use query projecting (<c>ProjectTo()</c>).
        /// <para>The projecting tells AutoMapper’s mapping engine to emit a select clause to the IQueryable that will inform entity
        /// framework that it only needs to query mapped columns of the table, same as if you manually projected your
        /// IQueryable to an DTO with a Select clause.</para>
        /// <para>Finally, that means that you query only mapped fields from the database, but not the whole DTO object and so
        /// you may significantly reduce requested data size for complex DTOs.</para>
        /// </param>
        /// <returns>Paged list of DTOs of entities.</returns>
        //Task<PageResult<TDTO>> GetPage<TEntity, TDTO>(
        //        QueryCommand command,
        //        Func<IQueryable<TEntity>, IQueryable<TEntity>> setQuery,
        //        CancellationToken ct = default,
        //        bool mappingOnDatabase = false)
        //    where TEntity : class
        //    where TDTO : class;

        /// <summary>
        /// Gets a paged list of entities by query command.
        /// </summary>
        /// <remarks>
        /// A paged list usually requested for data grids binding.
        /// Paged list's records are determined by page number, page size, filtering and sorting settings.
        /// </remarks>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <typeparam name="TDTO">DTO type.</typeparam>
        /// <param name="command">A query command with settings that determine the requested paged list.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <param name="mappingOnDatabase">When set to <c>True</c>, the method is supposed to use query projecting (<c>ProjectTo()</c>).
        /// <para>The projecting tells AutoMapper’s mapping engine to emit a select clause to the IQueryable that will inform entity
        /// framework that it only needs to query mapped columns of the table, same as if you manually projected your
        /// IQueryable to an DTO with a Select clause.</para>
        /// <para>Finally, that means that you query only mapped fields from the database, but not the whole DTO object and so
        /// you may significantly reduce requested data size for complex DTOs.</para>
        /// </param>
        /// <returns>Paged list of DTOs of entities.</returns>
        //Task<PageResult<TDTO>> GetPage<TEntity, TDTO>(
        //        QueryCommand command,
        //        CancellationToken ct = default,
        //        bool mappingOnDatabase = false)
        //    where TEntity : class
        //    where TDTO : class;

        /// <summary>
        /// Determines whether entities list contains any element that matches the condition defined by query.
        /// </summary>
        /// <remarks>
        /// <i>Code sample:
        /// <code>
        /// dataService.Any&lt;User&gt;(query => query.Where(o => o.Email == email), ct);
        /// </code></i>
        /// </remarks>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <param name="setQuery">Function method which customizes query to define the matching condition.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>The result contains <c>true</c> if the source sequence contains any elements; otherwise, <c>false</c>.</returns>
        Task<bool> Any<TEntity>(Func<IQueryable<TEntity>, IQueryable<TEntity>> setQuery, CancellationToken ct = default)
            where TEntity : class;
    }

    public interface IDataService: IDataService<IDataContext>
    { }
}
