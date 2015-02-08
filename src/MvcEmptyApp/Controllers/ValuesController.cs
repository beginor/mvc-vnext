using Microsoft.AspNet.Mvc;
using System.Collections.Generic;

namespace MvcApp.Controllers {

    [Route("api/[controller]")]
    public class ValuesController : Controller {

        // GET api/values
        [HttpGet("")]
        public IEnumerable<string> Get() {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id:int}")]
        public string Get(int id) {
            return "value";
        }

        // POST api/values/5
        [HttpPost("{id:int}")]
        public void Post(int id, [FromBody]string value) {
        }

        // PUT api/values
        [HttpPut("")]
        public void Put([FromBody]string value) {
        }

        // DELETE api/values/5
        [HttpDelete("{id:int}")]
        public void Delete(int id) {
        }
    }
}
