using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace SAL
{
    public static class ServiceHelper
    {
        public static ServiceResponseBE GetPOSTResponse(Uri uri, string data)
        {
            ServiceResponseBE respMsg = new ServiceResponseBE();
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);

                request.Method = "POST";
                request.ContentType = "application/json;charset=utf-8";

                System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
                byte[] bytes = encoding.GetBytes(data);

                request.ContentLength = bytes.Length;

                using (Stream requestStream = request.GetRequestStream())
                {
                    // Send the data.
                    requestStream.Write(bytes, 0, bytes.Length);
                }

                request.BeginGetResponse((x) =>
                {
                    using (HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(x))
                    {
                        respMsg.HttpStatusCode = response.StatusCode;
                        Stream responseStream = response.GetResponseStream();
                        StreamReader sr = new StreamReader(responseStream);
                        respMsg.ResponseMessage = sr.ReadToEnd();
                    }
                }, null);
            }
            catch (Exception ex)
            {
                respMsg.Error = ex.Message;
            }
            return respMsg;
        }

    }

    /*
    public ServiceResponseBE GetResponseMessage(string url,string requestString)
    {
        ServiceResponseBE respMsg = new ServiceResponseBE();
        try
        {

        WebRequest request = null;
        request = WebRequest.Create(url);
        request.Timeout = 2 * (1000 * 60);
        request.ContentType = "application/json";
        request.Method = "POST";

        using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
        {
            writer.Write(requestString);
        }

        using (WebResponse response = request.GetResponse())
        {
            Stream responseStream = response.GetResponseStream();
            StreamReader sr = new StreamReader(responseStream);
            respMsg.ResponseMessage = sr.ReadToEnd();
             //   respMsg.HttpStatusCode= response.
        }

        }
        catch (Exception ex)
        {
            respMsg.Error = ex.Message;
        }
        return respMsg;
    }*/
}