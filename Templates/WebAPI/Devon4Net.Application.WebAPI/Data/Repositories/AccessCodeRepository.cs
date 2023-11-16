using Devon4Net.Application.WebAPI.Domain.Database;
using Devon4Net.Application.WebAPI.Domain.Entities;
using Devon4Net.Application.WebAPI.Domain.RepositoryInterfaces;
using Devon4Net.Domain.UnitOfWork.Repository;
using Devon4Net.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Devon4Net.Application.WebAPI.Data.Repositories
{
    /// <summary>
    /// Repository implementation for Access code
    /// </summary>
    public class AccessCodeRepository : Repository<AccessCode>, IAccessCodeRepository
    {
        /// <summary>
        /// Constrecteur
        /// </summary>
        /// <param name="context"></param>
        public AccessCodeRepository(AccessCodeContext context) : base(context)
        {
        }

        /// <summary>
        /// Creates access code
        /// </summary>
        /// <param name="accessCode"></param>
        /// <returns></returns>
        public Task<AccessCode> CreateAccessCode(AccessCode accessCode)
        {
            Devon4NetLogger.Debug($"Creating new access code with Id: {accessCode.Id}, and visitor name: {accessCode.Visitor.Name} in the repository.");
            return Create(accessCode);
        }

        /// <summary>
        /// Deletes access code
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<bool> DeleteAccessCodeById(long id)
        {
            Devon4NetLogger.Debug($"The access code with id: {id} is going to be deleted in the repository.");
            return Delete(accessCode => accessCode.Id == id);
        }
    }
}
