using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace apiCRUD.Controller
{
    public class HelloController : ApiController
    {
     
        [HttpGet]
        public string hello(int id)
        {
            return "hello";
        }

    }
}
