using Devon4Net.Application.WebAPI.Domain.Database;
using Devon4Net.Application.WebAPI.Domain.Entities;
using Devon4Net.Application.WebAPI.Domain.RepositoryInterfaces;
using Devon4Net.Domain.UnitOfWork.Repository;
using Devon4Net.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Devon4Net.Application.WebAPI.Data.Repositories
{
    /// <summary>
    /// Repository implementation for Visitor
    /// </summary>
    public class VisitoryRepository : Repository<Visitor>, IVisitorRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public VisitoryRepository(VisitorContext context) : base(context)
        {
        }

        /// <summary>
        /// Creates a visitor
        /// </summary>
        /// <param name="visitor"></param>
        /// <returns></returns>
        public Task<Visitor> CreateVisitor(Visitor visitor)
        {
            Devon4NetLogger.Debug($"Create new Visitor from repository with name: {visitor.Name}");
            return Create(visitor);
        }

        /// <summary>
        /// Deletes the Visitor by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<long> DeleteVisitoryById(long id)
        {
            Devon4NetLogger.Debug($"Delete Visotory from repository with id: {id}");
            var delete = await Delete(visitor => visitor.Id == id).ConfigureAwait(false);

            if (delete) { return id; }

            throw new ArgumentException($"The Visitor with the Id: {id}, has not been deleted.");
        }

        /// <summary>
        /// Get Visitory by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Task<Visitor> GetVisitorByUsername(string username)
        {
            Devon4NetLogger.Debug("Get Visitor from repository by username");
            return GetFirstOrDefault(visitor => visitor.Username == username);
        }

        /// <summary>
        /// Updates visitor
        /// </summary>
        /// <param name="visitor"></param>
        /// <returns></returns>
        public async Task<Visitor> UpdateVisitor(Visitor visitor) 
        {
            Devon4NetLogger.Debug("Update Visitor from repository");
            return await Update(visitor).ConfigureAwait(false);
        }

        /// <summary>
        /// TODO: ESTA POR VER ESTO
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<IList<Visitor>> GetVisitorsByCriteria()
        {
            throw new NotImplementedException();
        }
    }
}
