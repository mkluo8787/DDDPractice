using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace CRM.Controllers {

    [ApiController]
    [Route("[controller]")]
    public class CrmController : ControllerBase {

        readonly ICrmSession session;
        readonly Apps.ICrmApp app;

        public CrmController(ICrmSession session, Apps.ICrmApp app) {
            this.session = session;
            this.app = app;
        }

        /**
            Authentication
         */

        [HttpPost("login")]
        public IActionResult Login([FromBody] JObject userData) {
            if (app.Authenticate(userData))            
                return Ok(new {
                    session.UserId,
                    session.TimeCreated
                });
            else 
                return Unauthorized();
        }

        /**
            Services
         */

        // GET crm/services
        [HttpGet("services")]
        public IActionResult GetServices() {
            return new JsonResult(app.GetServices(session.UserId).ToString());
        }

        // POST crm
        // [HttpPost]
        // public void Post([FromBody] string value) { }
    }
}