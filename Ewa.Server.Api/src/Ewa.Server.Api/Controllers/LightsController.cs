using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ewa.Server.API.Operators;
using Ewa.MessageObjects.Commands;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Ewa.Server.API.Controllers
{
    [Route("api/[controller]")]
    public class LightsController : Controller
    {
        [HttpPost]
        [Route("{lightid}/{onoff}")]
        public async Task<IActionResult> Post([FromRoute]string lightId, [FromRoute]OnOffSwitch onoff)
        {
            if (string.IsNullOrEmpty(lightId))
            {
                return BadRequest();
            }
            string messageId = await LightsOperator.OperateLight(lightId, onoff);
            return Created("", messageId);
        }


        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
