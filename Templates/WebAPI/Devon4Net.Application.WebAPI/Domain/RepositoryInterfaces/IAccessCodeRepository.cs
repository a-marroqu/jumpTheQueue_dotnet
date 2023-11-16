using Devon4Net.Application.WebAPI.Domain.Entities;
using Devon4Net.Domain.UnitOfWork.Repository;

namespace Devon4Net.Application.WebAPI.Domain.RepositoryInterfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAccessCodeRepository : IRepository<AccessCode>
    {
        /// <summary>
        /// Create access code for their respective visitor
        /// </summary>
        /// <param name="username"></param>
        Task<AccessCode> CreateAccessCode(AccessCode accessCode);

        /// <summary>
        /// Delete access code for the visitor by the Id
        /// </summary>
        /// <param name="username"></param>
        Task<bool> DeleteAccessCodeById(long id);
    }
}
