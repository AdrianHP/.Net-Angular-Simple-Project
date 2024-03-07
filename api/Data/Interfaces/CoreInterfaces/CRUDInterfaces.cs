using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace PetHealth.Core.Interfaces.CoreInterfaces
{
    /*
   ========= CRUD Interfaces =========    

   Interfaces are to connect the base data controller (DataControllerBase) to CRUD/paging implementing service.
   Presented by:
   -	IEntityCreate
   -	IEntityUpdate
   -	IEntityGet
   -	IEntityDelete
   -	IEntityPage

   They give the flexibility to override CRUD/paging behavior at the service level.

   Also, there is additional interfaces to customize Get/GetPage methods:
   -	IEntityQuery

    */

    /// <summary>
    /// A base interface all CRUD/paging interfaces are inherited from.
    /// </summary>
    /// <remarks>
    /// Doing so, all service classes that customize CRUD API method(s) have a common base interface
    /// and therefore can be used as a CRUD data handler in the controller.
    /// <i><para>For example, order service customizes CRUD's Create():</para>
    /// <code>
    ///     IOrderService: IEntityCreate...{}
    /// </code>
    /// where <c>IEntityCreate</c> inherits <c>IDataHandler</c> and therefore orderService class can be used as data handler:
    /// <code>
    ///     OrderController: DataControllerBase... {
    ///         OrderController(IOrderService orderService): base(orderService) {}
    ///     }
    /// </code></i>
    /// </remarks>
    public interface IDataHandler
    {
    }

    #region Interfaces for services to customize default implementation of CRUD/paging methods

    /// <summary>
    /// Defines a generalized method to create entity.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <typeparam name="TDTO">DTO type.</typeparam>
    public interface IEntityCreate<TDTO> : IDataHandler
        where TDTO : class
    {
        /// <summary>
        /// Creates entity by DTO.
        /// </summary>
        /// <param name="dto">DTO of entity for creating.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>DTO of created entity.</returns>
        Task<TDTO> Create(TDTO dto, CancellationToken ct = default);
    }

    /// <summary>
    /// Defines a generalized method to update entity.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <typeparam name="TDTO">DTO type.</typeparam>
    public interface IEntityUpdate<TDTO> : IDataHandler
        where TDTO : class
    {
        /// <summary>
        /// Updates entity by DTO.
        /// </summary>
        /// <param name="dto">DTO of entity for updating.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>DTO of updated entity.</returns>
        Task<TDTO> Update(TDTO dto, CancellationToken ct = default);
    }

    /// <summary>
    /// Defines a generalized method to get entity.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <typeparam name="TDTO">DTO type.</typeparam>
    /// <typeparam name="TKey">ID field type of entity.</typeparam>
    public interface IEntityGet<TDTO, TKey> : IDataHandler
        where TKey : IEquatable<TKey>
        where TDTO : class, IDTO<TKey>
    {
        /// <summary>
        /// Gets entity by ID.
        /// </summary>
        /// <param name="id">ID of requested entity.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>DTO of requested entity.</returns>
        Task<TDTO> Get(TKey id, CancellationToken ct = default);
    }


    /// <summary>
    /// Defines a generalized method to get entity.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <typeparam name="TDTO">DTO type.</typeparam>
    /// <typeparam name="TKey">ID field type of entity.</typeparam>
    public interface IEntityGetAll<TDTO,TKey> : IDataHandler
        where TKey : IEquatable<TKey>
        where TDTO : class, IDTO<TKey>
    {
        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>DTO of requested entity.</returns>
        Task<IEnumerable<TDTO>> GetAll(CancellationToken ct = default);
    }



    /// <summary>
    /// Defines a generalized method to delete entity.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <typeparam name="TKey">ID field type of entity.</typeparam>
    public interface IEntityDelete<TKey> : IDataHandler
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Deletes entity by ID.
        /// </summary>
        /// <param name="id">ID of entity.</param>
        /// <param name="ct">Cancellation token.</param>
        Task Delete(TKey id, CancellationToken ct = default);
    }


    #endregion

    #region Aggregated CRUD interfaces
    /// <summary>
    /// Defines a full list of generalized methods for CRUD/paging operations.
    /// </summary>
    /// <remarks>
    /// Instead of inheriting from number of single-method interfaces, a service can inherit this aggregated
    /// interface when it's supposed to fully customize all CRUD/paging operations.
    /// </remarks>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    /// <typeparam name="TDTO">Entity's DTO type.</typeparam>
    /// <typeparam name="TKey">ID field type of entity.</typeparam>
    public interface ICrudHandler<TEntity, TDTO, TKey> :
        IEntityGet<TDTO, TKey>,
        IEntityCreate<TDTO>,
        IEntityUpdate<TDTO>,
        IEntityDelete<TKey>

        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
        where TDTO : class, IDTO<TKey>
    {
    }

    /// <summary>
    /// Defines a full list of generalized methods for CRUD/paging operations.
    /// Interface variation for entities with integer ID field.
    /// </summary>
    /// <remarks>
    /// Instead of inheriting from number of single-method interfaces, a service can inherit this aggregated
    /// interface when it's supposed to fully customize all CRUD/paging operations.
    /// </remarks>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    /// <typeparam name="TDTO">Entity's DTO type.</typeparam>
    public interface ICrudHandler<TEntity, TDTO>
        : ICrudHandler<TEntity, TDTO, int>
        where TEntity : class, IEntity<int>
        where TDTO : class, IDTO<int>
    {
    }
    #endregion

    /// <summary>
    /// Defines a generalized method to customize a query to get a single entity or entities for a paged list.    
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    public interface IEntityQuery<TEntity> : IDataHandler
            where TEntity : class
    {
        /// <summary>
        /// Customizes a query to get a single entity or entities for a paged list.
        /// </summary>
        /// <remarks>
        /// Default implementations of API's <c>Get()</c> and <c>GetPage()</c> methods triggered by base data controller
        /// (<c>DataControllerBase</c>) attempt to use the data handler service as provider of entity's query.
        /// If the data handler service implements <c>IEntityQuery</c> interface then it's called from within
        /// the default Get() and GetPage() of <c>DataService</c> class and so you can customize a query for CRUD operations without implementing
        /// custom Get() and GetPage().
        /// <i>
        /// <para>For example, order service customizes entity's query by including OrderDetails navigation property:</para>
        /// Order service inherits the interface:
        /// <code>
        ///     public interface IOrderService: IEntityQuery... { }
        /// </code>
        /// then order service implements the interface and customizes the query avoiding implementing Get() and GetPage():
        /// <code>
        ///     public class OrderService : IOrderService {
        ///         public IQueryable... GetEntityQuery(IQueryable... baseQuery) => baseQuery.Include(x => x.Customer).Include(x => x.OrderDetails);
        ///     }
        /// </code>
        /// </i>
        /// </remarks>
        /// <param name="baseQuery">A base query to customize.</param>
        /// <returns>Customized query.</returns>
        IQueryable<TEntity> GetEntityQuery(IQueryable<TEntity> baseQuery);
    }
}
