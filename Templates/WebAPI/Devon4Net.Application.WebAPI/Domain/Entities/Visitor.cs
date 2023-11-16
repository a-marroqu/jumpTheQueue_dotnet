using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Devon4Net.Application.WebAPI.Domain.Entities
{
    /// <summary>
    /// Entity class for Visitor
    /// </summary>
    public partial class Visitor
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
        /// Username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Mail { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Telephone number
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Accepted terms
        /// </summary>
        public bool AcceptedTerms { get; set; }

        /// <summary>
        /// Accepted commercial
        /// </summary>
        public bool AcceptedCommercial { get; set; }

        /// <summary>
        /// User type
        /// </summary>
        public bool UserType { get; set; }

        /// <summary>
        /// Access code
        /// </summary>
        public AccessCode AccessCode { get; set; } 
    }
}
