using Devon4Net.Application.WebAPI.Domain.Entities;
using Devon4Net.Domain.UnitOfWork.Repository;

namespace Devon4Net.Application.WebAPI.Domain.RepositoryInterfaces
{
    /// <summary>
    /// Repository of the queue
    /// </summary>
    public interface IDailyQueueRepository : IRepository<DailyQueue>
    {
        /// <summary>
        /// Visitor creates the first queue
        /// </summary>
        Task<DailyQueue> CreateQueue(DailyQueue dailyQueue);

        /// <summary>
        /// Gets the queue for the day
        /// </summary>
        /// <returns></returns>
        Task<DailyQueue> GetDailyQueue();
    }
}
