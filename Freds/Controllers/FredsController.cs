using Freds.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.SessionState;

namespace Freds.Controllers
{
    public class FredsController : ApiController
    {
        private static Dictionary<long, Fred> freds = new Dictionary<long, Fred>();
        private static int nextFredId = 0;

        private int NextFredId
        {
            get
            {
                return nextFredId++;
            }
        }

        public IHttpActionResult Get() =>
            this.Json(freds.Values.ToList());

        public IHttpActionResult Get(int id)
        {
            if (freds.ContainsKey(id))
            {
                return this.Json(freds[id]);
            }
            else
            {
                return this.NotFound();
            }
        }

        public IHttpActionResult Post([FromBody] Fred fred)
        {
            do
            {
                fred.id = this.NextFredId;
            }
            while (freds.ContainsKey(fred.id));

            freds.Add(fred.id, fred);

            return this.Json(fred);
        }

        public IHttpActionResult Put(int id, [FromBody] Fred fred)
        {
            fred.id = id;
            freds[id] = fred;
            return this.Json(fred);
        }

        public IHttpActionResult Delete(int id)
        {
            if (freds.ContainsKey(id))
            {
                var deadFred = this.Json(freds[id]);
                freds.Remove(id);
                return deadFred;
            }
            else
            {
                return this.NotFound();
            }
        }
    }
}
