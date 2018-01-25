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
using SAL.SvcURLs;

namespace SAL
{
    public class ServiceProvider : IServiceProvider
    {
        ServiceResponseBE response;

        /// <summary>
        /// Deactivate patient
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public bool DeletePatient(int patientId)
        {
            bool deleted = false;
            try
            {
                response = ServiceHelper.GetPOSTResponse(
                    new Uri(SvcUrls.urlDeletePatient), UtilityLibrary.GetValueString(patientId));
                if (response.HttpStatusCode == HttpStatusCode.OK)
                {
                    string resp = JsonConvert.DeserializeObject<string>(response.ResponseMessage);
                    if (resp == CommonUnit.oSuccess)
                    {
                        deleted = true;
                    }
                }
                return deleted;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

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
                        return GetPatient(UtilityLibrary.GetValueInt(value, 0));
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
                    new Uri(SvcUrls.urlGetPatientByID), UtilityLibrary.GetValueString(patientId));
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
                if (!string.IsNullOrWhiteSpace(pin))

                    response = ServiceHelper.GetPOSTResponse(
                        new Uri(SvcUrls.urlGetPatientByPIN), UtilityLibrary.GetValueString(pin));
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
        /// Get Patient Collection
        /// </summary>
        /// <returns></returns>
        public List<Patient> GetPatientCollection()
        {
            try
            {
                response = ServiceHelper.GetPOSTResponse(
                    new Uri(SvcUrls.urlGetPatientCollection), UtilityLibrary.GetValueString(""));
                if (response.HttpStatusCode == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<List<Patient>>(response.ResponseMessage);
                }
                else
                {
                    throw new NullReferenceException();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Insert/Update patinet and return full atient objact
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public Patient SavePatient(Patient patient)
        {
            try
            {
                if (patient == null) throw new ArgumentNullException();

                if (patient.PatientId > 0)
                {
                   return InsertPatient(patient);
                }
                else
                {
                    return UpdatePatient(patient); 
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Private Methods

        /// <summary>
        /// Get Next patient ID
        /// </summary>
        /// <param name="pin"></param>
        /// <returns></returns>
        private int GetNextPatientID()
        {
            try
            {
                response = ServiceHelper.GetPOSTResponse(
                    new Uri(SvcUrls.urlGetNextPatientID), string.Empty);
                if (response.HttpStatusCode == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<int>(response.ResponseMessage);
                }
                else
                {
                    return 0;
                }
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
        private Patient InsertPatient(Patient patient)
        {
            Patient _patinet = null;
            try
            { 
                patient.PatientId = GetNextPatientID();
                patient.PIN = string.Format("P{0}", UtilityLibrary.GetValueString(patient.PatientId).PadLeft(9, '0'));
                Genaralizepatient(patient);

                if (patient.PatientId > 0)
                {
                    string msg = JsonConvert.SerializeObject(patient);
                    response = ServiceHelper.GetPOSTResponse(
                        new Uri(SvcUrls.urlInsertPatient), UtilityLibrary.GetValueString(msg));
                    if (response.HttpStatusCode == HttpStatusCode.OK)
                    {
                        _patinet = JsonConvert.DeserializeObject<Patient>(response.ResponseMessage);
                    }
                    else
                    {
                        _patinet = null;
                    }
                }
                else throw new NotImplementedException();

                return _patinet;
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
        private Patient UpdatePatient(Patient patient)
        {
            try
            {
                Genaralizepatient(patient);

                string msg = JsonConvert.SerializeObject(patient);
                response = ServiceHelper.GetPOSTResponse(
                    new Uri(SvcUrls.urlUpdatePatient), UtilityLibrary.GetValueString(msg));
                if (response.HttpStatusCode == HttpStatusCode.OK)
                {
                    return GetPatient(patient.PatientId);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Trim and correct null values in patient
        /// </summary>
        /// <param name="patient"></param>
        private static void Genaralizepatient(Patient patient)
        {
            patient.NIC = UtilityLibrary.GetValueString(patient.NIC);
            patient.PatientName = UtilityLibrary.GetValueString(patient.PatientName);
            patient.Gender = UtilityLibrary.GetValueString(patient.Gender); 

            if (patient.Address != null)
            {
                patient.Address.AddressL1 = UtilityLibrary.GetValueString(patient.Address.AddressL1);
                patient.Address.AddressL2 = UtilityLibrary.GetValueString(patient.Address.AddressL2);
                patient.Address.AddressL3 = UtilityLibrary.GetValueString(patient.Address.AddressL3);
                patient.Address.PostCode = UtilityLibrary.GetValueString(patient.Address.PostCode);
                patient.Address.City = UtilityLibrary.GetValueString(patient.Address.City);
                patient.Address.Country = UtilityLibrary.GetValueString(patient.Address.Country);
            }
        }

        #endregion
    }
}
 