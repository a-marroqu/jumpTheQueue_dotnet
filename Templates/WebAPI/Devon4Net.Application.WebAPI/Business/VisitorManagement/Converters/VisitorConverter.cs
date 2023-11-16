using Devon4Net.Application.WebAPI.Business.VisitorManagement.Dto;
using Devon4Net.Application.WebAPI.Domain.Entities;

namespace Devon4Net.Application.WebAPI.Business.VisitorManagement.Converters
{
    /// <summary>
    /// Visitor converter
    /// </summary>
    public class VisitorConverter
    {
        /// <summary>
        /// Transform Visitor entity to Visitor Dto
        /// </summary>
        /// <param name="visitor"></param>
        /// <returns></returns>
        public static VisitorDto ModelToDto(Visitor visitor)
        {
            if (visitor == null) return new VisitorDto();

            var visitorDto = new VisitorDto
            {
                Id = visitor.Id,
                Name = visitor.Name,
                Username = visitor.Username,
                Mail = visitor.Mail,
                PhoneNumber = visitor.PhoneNumber,
                Password = visitor.Password,
                Terms = visitor.AcceptedTerms
            };

            return visitorDto;
        }

        public static Visitor DtoToModel(VisitorDto visitorDto)
        {
            if (visitorDto == null) return new Visitor();

            var Visitor = new Visitor
            {
                Name = visitorDto.Name,
                Username = visitorDto.Username,
                Mail = visitorDto.Mail,
                PhoneNumber = visitorDto.PhoneNumber,
                Password = visitorDto.Password,
                AcceptedTerms = visitorDto.Terms,
                AcceptedCommercial = visitorDto.Commercial,
                UserType = false
                //AccessCode = new Domain.Entities.AccessCode()
            };
            return Visitor;
        }
    }
}
