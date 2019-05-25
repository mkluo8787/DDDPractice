using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace CRM.Controllers {

    // TODO: Unify response format (class Response)    

    [ApiController]
    [Route("[controller]")]
    public class CrmController : ControllerBase {

        readonly Apps.ICrmApp app;

        public CrmController(Apps.ICrmApp app) {
            this.app = app;
        }

        /**
            Authentication
         */

        [HttpPost("login")]
        public IActionResult Login([FromBody] JObject userData) {
            if (app.Authenticate(userData))
                return Ok(new { username = userData["username"] });
            else return Unauthorized();
        }

        /**
            Services
         */

        // GET crm/services
        [HttpGet("services")]
        public IActionResult GetServices() {
            if (app.IsLoggedIn())
                return Ok(app.GetServices());
            else return Unauthorized();
        }

        [HttpGet("services/{id}/info")]
        public IActionResult GetServiceInfo([FromRoute] int id) {
            if (app.IsLoggedIn())
                return Ok(new { id });
            else return Unauthorized();
        }
    }
}