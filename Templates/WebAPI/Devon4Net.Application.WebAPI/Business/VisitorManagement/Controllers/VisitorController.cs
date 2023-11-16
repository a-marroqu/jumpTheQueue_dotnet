using Devon4Net.Application.WebAPI.Business.EmployeeManagement.Dto;
using Devon4Net.Application.WebAPI.Business.VisitorManagement.Dto;
using Devon4Net.Application.WebAPI.Business.VisitorManagement.Service;
using Devon4Net.Application.WebAPI.Domain.Entities;
using Devon4Net.Infrastructure.Common;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Devon4Net.Application.WebAPI.Business.VisitorManagement.Controllers
{
    /// <summary>
    /// Employees controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [EnableCors("CorsPolicy")]
    public class VisitorController : ControllerBase
    {
        private readonly IVisitorService _visitorService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="visitorService"></param>
        public VisitorController(IVisitorService visitorService)
        {
            _visitorService = visitorService;
        }

        /// <summary>
        /// Create Visitor
        /// </summary>
        /// <param name="visitorDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(VisitorDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateVisitor(VisitorDto visitorDto)
        {
            Devon4NetLogger.Debug("Creating a visitor from the controller.");
            var visitor = await _visitorService.CreateVisitor(visitorDto).ConfigureAwait(false);
            return StatusCode(StatusCodes.Status201Created, visitor);
        }

        /// <summary>
        /// Delete visitor
        /// </summary>
        /// <param name="visitorId"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(VisitorDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteVisitor([Required] long visitorId)
        {
            Devon4NetLogger.Debug($"Delete the visitor with id :{visitorId} from the controller.");
            return Ok(await _visitorService.DeleteVisitor(visitorId).ConfigureAwait(false));
        }

        /// <summary>
        /// Get visitor by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/getVisitorByUsername")]
        [ProducesResponseType(typeof(VisitorDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetVisitorByUsername([Required] string username)
        {
            Devon4NetLogger.Debug($"Get Visitor with username: {username} from controller.");
            return Ok(await _visitorService.GetVisitorByUsername(username).ConfigureAwait(false));
        }

        /// <summary>
        /// Get visitor by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/joinQueue")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> JoinQueue([Required] string username) 
        {
            Devon4NetLogger.Debug($"The user with username: {username} desires to join the queue from the controller.");
            int placeInQueue = await _visitorService.JoinQueue(username).ConfigureAwait(false);
            Devon4NetLogger.Debug($"His place in queue is: {placeInQueue}.");
            return StatusCode(StatusCodes.Status200OK);
        }

        /// <summary>
        /// Get visitor by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/checkPlaceInQueue")]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CheckPlaceInQueue([Required] string username)
        {
            Devon4NetLogger.Debug($"The user with username: {username} desires to join the queue from the controller.");
            int placeInQueue = await _visitorService.CheckPlaceInQueue(username).ConfigureAwait(false);
            Devon4NetLogger.Debug($"The user's place in queue is: {placeInQueue}.");
            return StatusCode(StatusCodes.Status201Created, placeInQueue);
        }

        /// <summary>
        /// Get visitor by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/leavePlace")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> LeavePlaceInQueue([Required] string username)
        {
            Devon4NetLogger.Debug($"The user with username: {username} desires to join the queue from the controller.");
            _visitorService.LeavePlaceInQueue(username);
            Devon4NetLogger.Debug($"The user has left the queue.");
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
