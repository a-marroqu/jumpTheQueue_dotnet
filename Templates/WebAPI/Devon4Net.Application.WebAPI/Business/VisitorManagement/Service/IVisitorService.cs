using Devon4Net.Application.WebAPI.Business.VisitorManagement.Dto;
using Devon4Net.Application.WebAPI.Domain.Entities;

namespace Devon4Net.Application.WebAPI.Business.VisitorManagement.Service
{
    /// <summary>
    /// Repository of Visitor service
    /// </summary>
    public interface IVisitorService
    {
        /// <summary>
        /// Creates a visitor
        /// </summary>
        /// <param name="visitor"></param>
        /// <returns></returns>
        Task<Visitor> CreateVisitor(VisitorDto visitor);

        /// <summary>
        /// Deletes a visitor by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<long> DeleteVisitor(long id);

        /// <summary>
        /// Get visitor by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<VisitorDto> GetVisitorByUsername(string username);

        /// <summary>
        /// Visitor tries to join the queue
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<int> JoinQueue(string username);

        /// <summary>
        /// Visitor checks place in queue
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<int> CheckPlaceInQueue(string username);

        /// <summary>
        /// Visitor leaves place in queue
        /// </summary>
        /// <param name="username"></param>
        void LeavePlaceInQueue(string username);

        /// <summary>
        /// And even though you're gone, they will still live on
        /// In a memory or two
        /// </summary>
        /// <returns></returns>
        Task<IList<Visitor>> GetVisitorsByCriteria();
    }
}
