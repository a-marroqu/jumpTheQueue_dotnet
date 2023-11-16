using Devon4Net.Application.WebAPI.Domain.Database;
using Devon4Net.Application.WebAPI.Domain.Entities;
using Devon4Net.Application.WebAPI.Domain.RepositoryInterfaces;
using Devon4Net.Domain.UnitOfWork.Service;
using Devon4Net.Domain.UnitOfWork.UnitOfWork;
using Devon4Net.Infrastructure.Common;

namespace Devon4Net.Application.WebAPI.Business.AccessCode.Service
{
    /// <summary>
    /// Implementation Access code service
    /// </summary>
    public class AccessCodeService : Service<AccessCodeContext>, IAccessCodeService
    {
        private readonly IAccessCodeRepository _accessCodeRepository;

        /// <summary>
        /// Constrecteur
        /// </summary>
        /// <param name="uoW"></param>
        public AccessCodeService(IUnitOfWork<AccessCodeContext> uoW) : base(uoW) 
        {
            _accessCodeRepository = uoW.Repository<IAccessCodeRepository>();
        }

        /// <summary>
        /// Create access code
        /// </summary>
        /// <param name="accessCode"></param>
        /// <returns></returns>
        public Task<Domain.Entities.AccessCode> CreateAccessCode(Domain.Entities.AccessCode accessCode)
        {
            Devon4NetLogger.Debug($"Create Access code for username: {accessCode.Visitor.Name} in service.");
            return _accessCodeRepository.CreateAccessCode(accessCode);
        }

        /// <summary>
        /// Deletes access code
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<bool> DeleteAccessCodeById(long id) 
        {
            Devon4NetLogger.Debug($"Deletes Access code with id: {id} in service.");
            return _accessCodeRepository.DeleteAccessCodeById(id);
        }
    }
}
