using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using ServicesThesis.Data;
using System.Web.Http;
using System.Web.Http.Cors;
using Microsoft.Win32;


namespace ServicesThesis.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CebimdekiBahcivanController : ApiController
    {
        [HttpGet]
        public IHttpActionResult IlGetir()
        {
            return Json(CebimdekiBahcivanData.IlGetir());
        }

        [HttpGet]
        public IHttpActionResult IlceGetir()
        {
            return Json(CebimdekiBahcivanData.IlceGetir());
        }

        [HttpGet]
        public IHttpActionResult BlogYazisiGetir()
        {
            return Json(CebimdekiBahcivanData.BlogYazisiGetir());
        }
    }
}