using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Devon4Net.Application.WebAPI.Domain.Entities
{
    /// <summary>
    /// Entity class for AccessCode
    /// </summary>
    public partial class AccessCode
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Ticket number
        /// </summary>
        public int TicketNumber { get; set; }

        /// <summary>
        /// Start time
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// End time
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Visitor Id / Foreign key
        /// </summary>
        public long VisitorId { get; set; }

        /// <summary>
        /// Visitor
        /// </summary>
        public Visitor Visitor { get; set; }

        /// <summary>
        /// Daily queue Id / Foreign key
        /// </summary>
        public long DailyQueueId { get; set; }

        /// <summary>0
        /// Daily queue
        /// </summary>
        public DailyQueue DailyQueue { get; set; }
    }
}
