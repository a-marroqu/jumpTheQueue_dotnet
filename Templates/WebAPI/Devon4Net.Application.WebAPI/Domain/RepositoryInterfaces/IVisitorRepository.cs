using Devon4Net.Application.WebAPI.Domain.Entities;
using Devon4Net.Domain.UnitOfWork.Repository;

namespace Devon4Net.Application.WebAPI.Domain.RepositoryInterfaces
{
    /// <summary>
    /// Repository of the visitor
    /// </summary>
    public interface IVisitorRepository : IRepository<Visitor>
    {
        /// <summary>
        /// Create a visitor
        /// </summary>
        /// <param name="visitor"></param>
        /// <returns></returns>
        Task<Visitor> CreateVisitor(Visitor visitor);

        /// <summary>
        /// Delete visitor by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<long> DeleteVisitoryById(long id);

        /// <summary>
        /// Gets a visitor by its username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<Visitor> GetVisitorByUsername(string username);

        /// <summary>
        /// Updates visitor
        /// </summary>
        /// <param name="visitor"></param>
        /// <returns></returns>
        Task<Visitor> UpdateVisitor(Visitor visitor);

        /// <summary>
        /// Pagenates by a search criteria 
        /// </summary>
        /// <returns></returns>
        Task<IList<Visitor>> GetVisitorsByCriteria();
    }
}
