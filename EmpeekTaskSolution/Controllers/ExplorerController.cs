using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmpeekTaskSolution.Datalayer;
using System.Web.Script.Serialization;

namespace EmpeekTaskSolution.Controllers
{
    public class ExplorerController : ApiController
    {
        private DirectoryInformation _di = new DirectoryInformation();
        /// <summary>
        /// Returns HTTP 200 and JSON-serialized DirectoryInformation object
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage GetDirectories()
        {
            var content = _di;
            HttpResponseMessage message = Request.CreateResponse(HttpStatusCode.OK, content);
            return message;
        }

        /// <summary>
        /// Returns HTTP 200 and JSON-serialized DirectoryInformation object with changed Current Directory
        /// </summary>
        /// <param name="curPath">Current path</param>
        /// <param name="folder">Directory selected by user</param>
        /// <returns></returns>
        public HttpResponseMessage GetDirectories(string curPath, string folder)
        {
            if (folder == "..")
                _di.MoveUp(curPath);
            else if (curPath == "main")
                _di.CurrentDirectory = folder;
            else
                _di.CurrentDirectory = curPath + folder + "\\";
            HttpResponseMessage message = Request.CreateResponse(HttpStatusCode.OK, _di);
            return message;
        }



    }
}
