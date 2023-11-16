using Devon4Net.Application.WebAPI.Business.DailyQueue.Service;
using Devon4Net.Application.WebAPI.Business.VisitorManagement.Converters;
using Devon4Net.Application.WebAPI.Business.VisitorManagement.Dto;
using Devon4Net.Application.WebAPI.Domain.Database;
using Devon4Net.Application.WebAPI.Domain.Entities;
using Devon4Net.Application.WebAPI.Domain.RepositoryInterfaces;
using Devon4Net.Domain.UnitOfWork.Service;
using Devon4Net.Domain.UnitOfWork.UnitOfWork;
using Devon4Net.Infrastructure.Common;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Net.Mail;

namespace Devon4Net.Application.WebAPI.Business.VisitorManagement.Service
{
    /// <summary>
    /// Visitor service implementation
    /// </summary>
    public class VisitorService : Service<VisitorContext>, IVisitorService
    {
        private readonly IVisitorRepository _visitorRepository;

        private readonly IDailyQueueService _dailyQueueService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="uoW"></param>
        public VisitorService(IUnitOfWork<VisitorContext> uoW, IDailyQueueService dailyQueueService) : base(uoW)
        {
            _visitorRepository = uoW.Repository<IVisitorRepository>();
            _dailyQueueService = dailyQueueService;
        }

        /// <summary>
        /// Create visitor
        /// </summary>
        /// <param name="visitor"></param>
        /// <returns></returns>
        public async Task<Visitor> CreateVisitor(VisitorDto visitor)
        {
            VisitorDto check = await GetVisitorByUsername(visitor.Username).ConfigureAwait(false);

            if (check.Username != null)
            {
                Devon4NetLogger.Error("There is already a user with that username.");
                throw new InvalidOperationException("There is already a user with that username.");
            }

            Devon4NetLogger.Debug($"Create Visitor from service: {visitor}");
            try
            {
                VerifyVisitor(visitor);
            }
            catch (Exception ex)
            {
                Devon4NetLogger.Error(ex.Message);
                throw new Exception(ex.Message);
            }

            return await _visitorRepository.CreateVisitor(VisitorConverter.DtoToModel(visitor)).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete visitor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<long> DeleteVisitor(long id)
        {
            Devon4NetLogger.Debug($"The user with id: {id} is going to be deleted.");
            var visitor = await _visitorRepository.GetFirstOrDefault(visitor => visitor.Id == id).ConfigureAwait(false);

            if (visitor == null)
            {
                throw new ArgumentException($"The provided id: {id} doesn't retrieves any user.");
            }

            return await _visitorRepository.DeleteVisitoryById(id).ConfigureAwait(false);
        }

        /// <summary>
        /// Get a visitor by its username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<VisitorDto> GetVisitorByUsername(string username)
        {
            Devon4NetLogger.Debug($"Get Visitor with username: {username}");
            var existingUser = await _visitorRepository.GetVisitorByUsername(username).ConfigureAwait(false);
            return VisitorConverter.ModelToDto(existingUser);
        }

        /// <summary>
        /// Updates a visitor
        /// </summary>
        /// <param name="visitor"></param>
        /// <returns></returns>
        private Task<Visitor> UpdateVisitor(Visitor visitor)
        {
            Devon4NetLogger.Debug($"Update visitor with name: {visitor.Name}");
            return _visitorRepository.UpdateVisitor(visitor);
        }

        /// <summary>
        /// Visitor joins a queue
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<int> JoinQueue(string username)
        {
            Devon4NetLogger.Debug($"Check if there is already a queue created.");
            bool created = await _dailyQueueService.FirstQueueOfTheDay().ConfigureAwait(false);
            var existingUser = await _visitorRepository.GetVisitorByUsername(username).ConfigureAwait(false);

            if (existingUser == null)
            {
                Devon4NetLogger.Error($"There is no user with the username: {username}");
                throw new InvalidOperationException($"There is no user with the username: {username}");
            }
            else if (existingUser.AccessCode != null)
            {
                Devon4NetLogger.Error($"The user with the username: {username} already has an access code: {existingUser.AccessCode.TicketNumber}");
                throw new InvalidOperationException($"The user with the username: {username} already has an access code: {existingUser.AccessCode.TicketNumber}");
            }
            else
            {
                Domain.Entities.DailyQueue dailyQueue;
                if (!created)
                {
                    //Creates queue of the day
                    dailyQueue = _dailyQueueService.CreatesDailyQueue();
                }
                else
                {
                    //Gets queue created
                    dailyQueue = await _dailyQueueService.GetDailyQueue().ConfigureAwait(false);
                }

                //Creates access code and returns updates visitor
                //creo que aquí debería updatearse y devolverse el existingUser, no crear otro var con
                //los valores que hay en existing user
                var createdAccessCode = await _dailyQueueService.AddVisitorToQueue(existingUser, dailyQueue).ConfigureAwait(false);
                existingUser.AccessCode = createdAccessCode;
                //Save updated visitor
                await UpdateVisitor(existingUser).ConfigureAwait(false);
                return existingUser.AccessCode.TicketNumber;
            }
        }

        /// <summary>
        /// Checks the place of set visitor in the queue
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<int> CheckPlaceInQueue(string username)
        {
            Devon4NetLogger.Debug("Check if the user exist");
            var existingUser = await _visitorRepository.GetVisitorByUsername(username).ConfigureAwait(false);

            if (existingUser == null)
            {
                Devon4NetLogger.Error($"There is no user with the username: {username}");
                throw new InvalidOperationException($"There is no user with the username: {username}");
            }
            else if (existingUser.AccessCode != null)
            {
                Devon4NetLogger.Error($"The user with the username: {username} already has an access code: {existingUser.AccessCode.TicketNumber}");
                throw new InvalidOperationException($"The user with the username: {username} already has an access code: {existingUser.AccessCode.TicketNumber}");
            }

            return await _dailyQueueService.CheckPlaceOfVisitorInQueue(existingUser).ConfigureAwait(false);
        }

        /// <summary>
        /// Visitor quits the queue
        /// </summary>
        /// <param name="username"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public async void LeavePlaceInQueue(string username)
        {
            Devon4NetLogger.Debug("Check if the user exist");
            var existingUser = await _visitorRepository.GetVisitorByUsername(username).ConfigureAwait(false);

            if (existingUser == null)
            {
                Devon4NetLogger.Error($"There is no user with the username: {username}");
                throw new InvalidOperationException($"There is no user with the username: {username}");
            }
            else if (existingUser.AccessCode != null)
            {
                Devon4NetLogger.Error($"The user with the username: {username} already has an access code: {existingUser.AccessCode.TicketNumber}");
                throw new InvalidOperationException($"The user with the username: {username} already has an access code: {existingUser.AccessCode.TicketNumber}");
            }

            await _dailyQueueService.LeavePlaceInQueue(existingUser).ConfigureAwait(false);
            await UpdateVisitor(existingUser).ConfigureAwait(false);
        }

        /// <summary>
        /// Of all the streets I see, no one will ever be Comparable to you
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<IList<Visitor>> GetVisitorsByCriteria()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Validate visitor before saving it on DDBB
        /// </summary>
        /// <param name="visitor"></param>
        /// <exception cref="ArgumentException"></exception>
        private void VerifyVisitor(VisitorDto visitor)
        {
            //verify name with lastname
            if (!string.IsNullOrEmpty(visitor.Name) || !string.IsNullOrWhiteSpace(visitor.Name))
            {
                int space = visitor.Name.IndexOf(' ');
                if (space < 0) throw new ArgumentException("Please enter your last name.");
            }
            else
            {
                throw new ArgumentException("Please fill the 'name' field.");
            }

            //verify username
            if (string.IsNullOrEmpty(visitor.Username) || string.IsNullOrWhiteSpace(visitor.Name))
            {
                throw new ArgumentException("Please fill the 'username' field.");
            }

            //verify phone number
            if (!string.IsNullOrEmpty(visitor.PhoneNumber) || !string.IsNullOrWhiteSpace(visitor.PhoneNumber))
            {
                int countNumbers = 0;
                foreach (char c in visitor.PhoneNumber)
                {
                    countNumbers++;
                }

                if (countNumbers < 9)
                {
                    throw new ArgumentException("Please enter a valid 'phone number.'");
                }
            }
            else
            {
                throw new ArgumentException("Please fill the 'phone number' field.");
            }

            //verify mail
            if (!string.IsNullOrEmpty(visitor.Mail) || !string.IsNullOrWhiteSpace(visitor.Mail))
            {
                try
                {
                    var email = new MailAddress(visitor.Mail);
                }
                catch (FormatException)
                {
                    throw new FormatException("Please write a valid email adress.");
                }
            }
            else
            {
                throw new ArgumentException("Please fill the 'email' field.");
            }

            //verify password
            if (string.IsNullOrEmpty(visitor.Password) || string.IsNullOrWhiteSpace(visitor.Password))
            {
                throw new ArgumentException("Please enter a 'password' field.");
            }

            //verify terms
            if (visitor.Terms == false)
            {
                throw new ArgumentException("You have to accept the 'Terms and Conditions'.");
            }
        }
    }
}
