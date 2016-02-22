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

        public HttpResponseMessage GetDirectories()
        {
            var content = _di;
            HttpResponseMessage message = Request.CreateResponse(HttpStatusCode.OK, content);
            return message;
        }

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
