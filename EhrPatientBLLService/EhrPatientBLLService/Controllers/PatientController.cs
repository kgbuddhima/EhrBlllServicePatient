using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessEntity;
using Utility;
using SAL;

namespace EhrPatientBLLService.Controllers
{
    public class PatientController : ApiController
    {
        SAL.IServiceProvider _document = new ServiceProvider();

        public PatientController()
        {

        }

        [Route("DeletePatient")]
        [HttpPost]
        public HttpResponseMessage DeletePatient(int id)
        {
            try
            {
                bool deactivated = _document.Deactivatepatient(id);
                if (deactivated)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, CommonUnit.oSuccess);
                }
                else
                    return Request.CreateResponse(HttpStatusCode.BadRequest, CommonUnit.oFailed);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Route("GetPatientById")]
        [HttpPost]
        public HttpResponseMessage GetPatientById(int patientId)
        {
            try
            {
                Patient patient = _document.GetPatient(patientId);
                if (patient != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, patient);
                }
                else
                    return Request.CreateResponse(HttpStatusCode.BadRequest, CommonUnit.oFailed);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Route("GetPatientByPIN")]
        [HttpPost]
        public HttpResponseMessage GetPatientByPIN(string pin)
        {
            try
            {
                Patient patient = _document.GetPatient(pin);
                if (patient != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, patient);
                }
                else
                    return Request.CreateResponse(HttpStatusCode.BadRequest, CommonUnit.oFailed);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Route("GetPatientsCollection")]
        [HttpPost]
        public string GetPatientsCollection()
        {
            return string.Empty;
        }

        [Route("SavePatient")]
        [HttpPost]
        public HttpResponseMessage SavePatient([FromBody]Patient value)
        { 
            try
            {
                Patient savedpatient = _document.SavePatient(value);
                if (savedpatient!=null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, savedpatient);
                }
                else
                    return Request.CreateResponse(HttpStatusCode.BadRequest, CommonUnit.oFailed);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
