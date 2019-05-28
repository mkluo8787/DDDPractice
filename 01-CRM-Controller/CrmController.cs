using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace CRM.Controllers {

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
            var loginResult = app.Authenticate(userData).ToJson();
            if (!app.IsLoggedIn()) return Unauthorized(loginResult);

            return Ok(loginResult);
        }

        /**
            Services
         */

        [HttpGet("services")]
        public IActionResult GetServices() {
            if (!app.IsLoggedIn()) return Unauthorized();

            return Ok(app.GetServices().ToJson());
        }

        [HttpGet("services/{id}/info")]
        public IActionResult GetServiceInfo([FromRoute] int id) {
            if (!app.IsLoggedIn()) return Unauthorized();

            return Ok(new { id });
        }
    }
}