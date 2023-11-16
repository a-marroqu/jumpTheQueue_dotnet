namespace Devon4Net.Application.WebAPI.Business.AccessCode.Service
{
    /// <summary>
    /// Repository for Access code service
    /// </summary>
    public interface IAccessCodeService
    {
        /// <summary>
        /// Create access code 
        /// </summary>
        /// <param name="accessCode"></param>
        /// <returns></returns>
        Task<Domain.Entities.AccessCode> CreateAccessCode(Domain.Entities.AccessCode accessCode);

        /// <summary>
        /// Deletes access code by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<bool> DeleteAccessCodeById(long id);
    }
}
