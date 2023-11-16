using Devon4Net.Application.WebAPI.Business.AccessCode.Service;
using Devon4Net.Application.WebAPI.Data.Repositories;
using Devon4Net.Application.WebAPI.Domain.Database;
using Devon4Net.Application.WebAPI.Domain.Entities;
using Devon4Net.Application.WebAPI.Domain.RepositoryInterfaces;
using Devon4Net.Domain.UnitOfWork.Service;
using Devon4Net.Domain.UnitOfWork.UnitOfWork;
using Devon4Net.Infrastructure.Common;
using Microsoft.IdentityModel.Tokens;

namespace Devon4Net.Application.WebAPI.Business.DailyQueue.Service
{
    public class DailyQueueService : Service<DailyQueueContext>, IDailyQueueService
    {

        private readonly IDailyQueueRepository _dailyQueueRepository;

        private readonly IAccessCodeService _accessCodeService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="uoW"></param>
        /// <param name="accessCodeService"></param>
        public DailyQueueService(IUnitOfWork<DailyQueueContext> uoW, IAccessCodeService accessCodeService) : base(uoW)
        {
            _dailyQueueRepository = uoW.Repository<IDailyQueueRepository>();
            _accessCodeService = accessCodeService;
        }

        /// <summary>
        /// Checks if there is a queue created
        /// </summary>
        /// <returns></returns>
        public async Task<bool> FirstQueueOfTheDay()
        {
            Devon4NetLogger.Debug("Check in database if the queue has been created.");
            var created = await _dailyQueueRepository.GetDailyQueue().ConfigureAwait(false);

            if (created == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Creates the first queue of the day
        /// </summary>
        /// <returns></returns>
        public Domain.Entities.DailyQueue CreatesDailyQueue()
        {
            Devon4NetLogger.Debug("Create first queue.");
            return CreateDailyQueue();
        }

        public Task<Domain.Entities.DailyQueue> GetDailyQueue()
        {
            Devon4NetLogger.Debug("Gets queue for the day.");
            return _dailyQueueRepository.GetDailyQueue();
        }

        /// <summary>
        /// Adds an existing user to the queue, creates access code for set user
        /// </summary>
        /// <param name="visitor"></param>
        /// <param name="dailyQueue"></param>
        /// <returns></returns>
        public async Task<Domain.Entities.AccessCode> AddVisitorToQueue(Visitor visitor, Domain.Entities.DailyQueue dailyQueue)
        {
            Devon4NetLogger.Debug($"Create access code for visitor: {visitor.Username}.");
            var accessCode = CreateAccessCode(dailyQueue, visitor);

            //if its the first queue, creates it, if not, add visitors to is
            if (dailyQueue.CodeList.IsNullOrEmpty())
            {
                Devon4NetLogger.Debug("Create code list.");
                dailyQueue.CodeList = new List<Domain.Entities.AccessCode>();
                Devon4NetLogger.Debug("Add access code to queue.");
                dailyQueue.CodeList.Add(accessCode);
            }
            else 
            {
                Devon4NetLogger.Debug("Add access code to queue.");
                dailyQueue.CodeList.Add(accessCode);
            } 
            Devon4NetLogger.Debug("Saves modification of daily queue to database");
            await _dailyQueueRepository.CreateQueue(dailyQueue).ConfigureAwait(false);

            Devon4NetLogger.Debug("Add access code to visitor.");
            visitor.AccessCode = accessCode;
            Devon4NetLogger.Debug("Save access code to database.");
            await _accessCodeService.CreateAccessCode(accessCode).ConfigureAwait(false);

            return visitor.AccessCode;
        }

        /// <summary>
        /// Checks place of set visitor in the queue
        /// </summary>
        /// <param name="visitor"></param>
        /// <returns></returns>
        public async Task<int> CheckPlaceOfVisitorInQueue(Visitor visitor) 
        {
            var dailyQueue = await GetDailyQueue().ConfigureAwait(false);
            int positionInQueue = dailyQueue.CodeList.FindIndex(accessCode => accessCode.TicketNumber == visitor.AccessCode.TicketNumber);
            return positionInQueue;
        }

        public async Task<Visitor> LeavePlaceInQueue(Visitor visitor) 
        {
            //erase the access code from the visitor
            long accessCodeId = visitor.AccessCode.Id;
            visitor.AccessCode = null;

            //erase the access code from the daily queue
            var dailyQueue = await GetDailyQueue().ConfigureAwait(false);
            Domain.Entities.AccessCode accessCode = dailyQueue.CodeList.FirstOrDefault(accessCode => accessCode.Id == accessCodeId);
            dailyQueue.CodeList.Remove(accessCode);
            //update it in the database
            await _dailyQueueRepository.Update(dailyQueue).ConfigureAwait(false);

            //erase the access code from the database
            bool erased = await _accessCodeService.DeleteAccessCodeById(accessCodeId).ConfigureAwait(false);

            if (erased)
            {
                Devon4NetLogger.Debug($"The access code with id: {accessCodeId} was erase successfully");
            }
            else 
            {
                throw new InvalidOperationException($"Something went wrong while erasing the access code with id: {accessCodeId}");
            }

            //update the visitor in the database
            return visitor;
        }

        /// <summary>
        /// Create the first queue for the day
        /// </summary>
        /// <returns></returns>
        private Domain.Entities.DailyQueue CreateDailyQueue()
        {
            var dailyQueue = new Domain.Entities.DailyQueue();
            dailyQueue.Name = "daily queue";
            dailyQueue.Logo = "logo of the daily queue";
            dailyQueue.AttentionTime = DateTime.Now;
            dailyQueue.MinAttentionTime = DateTime.Now.AddSeconds(20);
            dailyQueue.Active = true;

            return dailyQueue;
        }

        /// <summary>
        /// Create the access code for visitor
        /// </summary>
        /// <param name="dailyQueue"></param>
        /// <param name="visitor"></param>
        /// <returns></returns>
        private Domain.Entities.AccessCode CreateAccessCode(Domain.Entities.DailyQueue dailyQueue, Visitor visitor)
        {
            var accessCode = new Domain.Entities.AccessCode();
            accessCode.StartTime = DateTime.Now;
            accessCode.EndTime = DateTime.Now.AddSeconds(20);
            accessCode.VisitorId = visitor.Id;
            accessCode.Visitor = visitor;
            accessCode.DailyQueueId = dailyQueue.Id;
            accessCode.DailyQueue = dailyQueue;

            return accessCode;
        }
    }
}
