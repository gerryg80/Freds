using Freds.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.SessionState;

namespace Karens.Controllers
{
    public class KarensController : ApiController
    {
        private static Dictionary<long, Karen> karens = new Dictionary<long, Karen>();
        private static int nextKarenId = 0;

        private int NextKarenId
        {
            get
            {
                return nextKarenId++;
            }
        }

        public IHttpActionResult Get() =>
            this.Json(karens.Values.ToList());

        public IHttpActionResult Get(int id)
        {
            if (karens.ContainsKey(id))
            {
                return this.Json(karens[id]);
            }
            else
            {
                return this.NotFound();
            }
        }

        public IHttpActionResult Post([FromBody] Karen karen)
        {
            do
            {
                karen.id = this.NextKarenId;
            }
            while (karens.ContainsKey(karen.id));

            karens.Add(karen.id, karen);

            return this.Json(karen);
        }

        public IHttpActionResult Put(int id, [FromBody] Karen karen)
        {
            karen.id = id;
            karens[id] = karen;
            return this.Json(karen);
        }

        public IHttpActionResult Delete(int id)
        {
            if (karens.ContainsKey(id))
            {
                var deadKaren = this.Json(karens[id]);
                karens.Remove(id);
                return deadKaren;
            }
            else
            {
                return this.NotFound();
            }
        }
    }
}
