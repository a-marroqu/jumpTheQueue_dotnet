using Devon4Net.Application.WebAPI.Domain.Entities;

namespace Devon4Net.Application.WebAPI.Business.DailyQueue.Service
{
    public interface IDailyQueueService
    {
        /// <summary>
        /// Checks if queue is created for the day
        /// </summary>
        /// <returns></returns>
        Task<bool> FirstQueueOfTheDay();

        /// <summary>
        /// Creates a daily queue
        /// </summary>
        /// <returns></returns>
        Domain.Entities.DailyQueue CreatesDailyQueue();

        /// <summary>
        /// Get daily queue
        /// </summary>
        /// <returns></returns>
        Task<Domain.Entities.DailyQueue> GetDailyQueue();

        /// <summary>
        /// Adds visitor to queue
        /// </summary>
        /// <param name="visitor"></param>
        Task<Domain.Entities.AccessCode> AddVisitorToQueue(Visitor visitor, Domain.Entities.DailyQueue dailyQueue);

        /// <summary>
        /// Checks place of set visitor in the queue
        /// </summary>
        /// <param name="visitor"></param>
        /// <returns></returns>
        Task<int> CheckPlaceOfVisitorInQueue(Visitor visitor);

        /// <summary>
        /// Erases access code from queue and the table of access code
        /// </summary>
        /// <param name="visitor"></param>
        /// <returns></returns>
        Task<Visitor> LeavePlaceInQueue(Visitor visitor);
    }
}
