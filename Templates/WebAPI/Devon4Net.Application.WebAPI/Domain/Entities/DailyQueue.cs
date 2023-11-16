using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Devon4Net.Application.WebAPI.Domain.Entities
{
    /// <summary>
    /// Entity class for Daily queue
    /// </summary>
    public partial class DailyQueue
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Logo
        /// </summary>
        public string Logo { get; set; }
        
        /// <summary>
        /// Attention time 
        /// </summary>
        public DateTime AttentionTime { get; set; }

        /// <summary>
        /// minimum attention time 
        /// </summary>
        public DateTime MinAttentionTime { get; set; }

        /// <summary>
        /// Active
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// List of codes that make the queue
        /// </summary>
        public List<AccessCode> CodeList { get; set; }
    }
}
