using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AuditCheckList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AuditCheckListController : ControllerBase
    {


        readonly log4net.ILog _log4net;

        public AuditCheckListController()
        {
            _log4net = log4net.LogManager.GetLogger(typeof(AuditCheckListController));
        }
        [HttpGet]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public  ActionResult<List<string>> GetAuditCheckListQuestions(string AuditType)
        {
            List<string> Question = new List<string>();

            if (AuditType != "Internal" && AuditType!="SOX")
            {
                _log4net.Info($"AuditChceklist Get Method invoked with Invalid AuditType");                                  //Invalid Audit TYpe
                return BadRequest("Invalid Input");                                                                 // return BadRequest
            }

            _log4net.Info($"AuditChceklist Get Method invoked with {AuditType} AuditType");
           
            if (AuditType == "Internal")
            {
                Question.Add("Have all Change requests followed SDLC before PROD move? ");
                Question.Add("Have all Change requests been approved by the application owner?");                            //Internal Audit TYpe
                Question.Add("Are all artifacts like CR document, Unit test cases available?");
                Question.Add("Is the SIT and UAT sign-off available?");
                Question.Add("Is data deletion from the system done with application owner approval?");
            }
            else if (AuditType == "SOX")
            {
                Question.Add("Have all Change requests followed SDLC before PROD move?");
                Question.Add("Have all Change requests been approved by the application owner?");
                Question.Add("For a major change, was there a database backup taken before and after PROD move?");              //SOX Audit TYpe
                Question.Add("Has the application owner approval obtained while adding a user to the system?");
                Question.Add("Is data deletion from the system done with application owner approval?");
            }
            return Ok(Question);                                              //return 200 status code 

        }
    }
}