using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using BusinessEntity;
using Utility;
using Newtonsoft.Json;
 
namespace SAL
{
    public class ServiceProvider : IServiceProvider
    {
        ServiceResponseBE response;

        /// <summary>
        /// Get patient by PatientId or PIn
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Patient GetPatientByAny(string value)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (value.Length <= 10)
                    {
                        return GetPatient(UtilityLibrary.GetValueInt(value,0));
                    }
                    else
                    {
                        return GetPatient(value);
                    }
                }
                else return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get patient by PatientId
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public Patient GetPatient(int patientId)
        {
            Patient patinet = null;
            try
            {
                response = ServiceHelper.GetPOSTResponse(
                    new Uri(ServiceHelper.urlGetPatientByID),UtilityLibrary.GetValueString(patientId));
                if (response.HttpStatusCode == HttpStatusCode.OK)
                {
                    patinet = JsonConvert.DeserializeObject<Patient>(response.ResponseMessage);
                }
                else
                {
                    patinet = null;
                }
                return patinet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get patient by PIN
        /// </summary>
        /// <param name="pin"></param>
        /// <returns></returns>
        public Patient GetPatient(string pin)
        {
            Patient patinet = null;
            try
            {
                if(!string.IsNullOrWhiteSpace(pin))

                response = ServiceHelper.GetPOSTResponse(
                    new Uri(ServiceHelper.urlGetPatientByIPIN), UtilityLibrary.GetValueString(pin));
                if (response.HttpStatusCode == HttpStatusCode.OK)
                {
                    patinet = JsonConvert.DeserializeObject<Patient>(response.ResponseMessage);
                }
                else
                {
                    patinet = null;
                }
                return patinet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Insert new patinet and return full atient objact
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public Patient SavePatient(Patient patient)
        {
            Patient patinet = null;
            try
            {
                string msg = JsonConvert.SerializeObject(patinet);
                response = ServiceHelper.GetPOSTResponse(
                    new Uri(ServiceHelper.urlSavePatient), UtilityLibrary.GetValueString(msg));
                if (response.HttpStatusCode == HttpStatusCode.OK)
                {
                    patinet = JsonConvert.DeserializeObject<Patient>(response.ResponseMessage);
                }
                else
                {
                    patinet = null;
                }
                return patinet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Update patient
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public bool UpdatePatient(Patient patient)
        {
            try
            {
                string msg = JsonConvert.SerializeObject(patient);
                response = ServiceHelper.GetPOSTResponse(
                    new Uri(ServiceHelper.urlSavePatient), UtilityLibrary.GetValueString(msg));
                if (response.HttpStatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
