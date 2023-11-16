using System.ComponentModel.DataAnnotations;

namespace Devon4Net.Application.WebAPI.Business.VisitorManagement.Dto
{
    /// <summary>
    /// Visitor DTO
    /// </summary>
    public class VisitorDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// name
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// username
        /// </summary>
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// phone number
        /// </summary>
        [Required]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// mail / username
        /// </summary>
        [Required]
        public string Mail { get; set; }

        /// <summary>
        /// password
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// terms and conditions
        /// </summary>
        [Required]
        public bool Terms { get; set; }

        /// <summary>
        /// commercial
        /// </summary>
        public bool Commercial { get; set; }
    }
}
