using Devon4Net.Application.WebAPI.Domain.Database;
using Devon4Net.Application.WebAPI.Domain.Entities;
using Devon4Net.Application.WebAPI.Domain.RepositoryInterfaces;
using Devon4Net.Domain.UnitOfWork.Repository;
using Devon4Net.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Devon4Net.Application.WebAPI.Data.Repositories
{
    /// <summary>
    /// Repository implementation for Daily queue
    /// </summary>
    public class DailyQueueRepository : Repository<DailyQueue> ,  IDailyQueueRepository
    {
        public DailyQueueRepository(DailyQueueContext context) : base(context)
        {
        }

        public Task<DailyQueue> CreateQueue(DailyQueue dailyQueue)
        {
            Devon4NetLogger.Debug($"Create new Daily Queue from repository with name: {dailyQueue.Name}");
            return Create(dailyQueue);
        }

        public Task<DailyQueue> GetDailyQueue()
        {
            Devon4NetLogger.Debug($"Gets the queue for the day.");
            return GetFirstOrDefault(dailyQueue => dailyQueue.Id == 1);
        }
    }
}
