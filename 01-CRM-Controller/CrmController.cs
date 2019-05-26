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
            Test
         */
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { });
        }

        /**
            Authentication
         */

        [HttpPost("login")]
        public IActionResult Login([FromBody] JObject userData) {
            var loginResult = app.Authenticate(userData);
            if (app.IsLoggedIn())
                return Ok(loginResult);
            else return Unauthorized(loginResult);
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