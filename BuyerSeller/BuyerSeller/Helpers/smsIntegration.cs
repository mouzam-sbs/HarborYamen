using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.VisualBasic;

using System.Collections;

using System.Data;
using System.Diagnostics;
using System.IO;
using System.Configuration;
using System.Net;
using System.Text;


namespace BuyerSeller.Helpers
{
    public class smsIntegration
    {



        public string SendSMS(string User, string password, string Mobile_Number, string Message, [System.Runtime.InteropServices.OptionalAttribute, System.Runtime.InteropServices.DefaultParameterValueAttribute("N")] string MType)
        {
            string stringpost = "User=" + User + "&passwd=" + password + "&mobilenumber=" + Mobile_Number + "&message=" + Message + "&mtype=" + MType;
            //Response.Write(stringpost)
            string functionReturnValue = null;
            functionReturnValue = "";

            HttpWebRequest objWebRequest = null;
            HttpWebResponse objWebResponse = null;
            StreamWriter objStreamWriter = null;
            StreamReader objStreamReader = null;

            try
            {
                string stringResult = null;

                objWebRequest = (HttpWebRequest)WebRequest.Create("http://bulksms.w2wts.com/API_SendSMS.aspx");

                objWebRequest.Method = "POST";

                // Response.Write(objWebRequest)




                // Please edit below lines if you are behind the proxy
                //You can find both the parameters in Connection settings of your internet explorer.

                //System.Net.WebProxy myProxy = new System.Net.WebProxy(IPAddress, port);
                //myProxy.BypassProxyOnLocal = true;
                //objWebRequest.Proxy = myProxy;

                objWebRequest.ContentType = "application/x-www-form-urlencoded";

                objStreamWriter = new StreamWriter(objWebRequest.GetRequestStream());
                objStreamWriter.Write(stringpost);
                objStreamWriter.Flush();
                objStreamWriter.Close();

                objWebResponse = (HttpWebResponse)objWebRequest.GetResponse();


                objWebResponse = (HttpWebResponse)objWebRequest.GetResponse();

                objStreamReader = new StreamReader(objWebResponse.GetResponseStream());
                stringResult = objStreamReader.ReadToEnd();
                objStreamReader.Close();
                return (stringResult);
            }
            catch (Exception ex)
            {
                return (ex.ToString());

            }
            finally
            {
                if ((objStreamWriter != null))
                {
                    objStreamWriter.Close();
                }
                if ((objStreamReader != null))
                {
                    objStreamReader.Close();
                }
                objWebRequest = null;
                objWebResponse = null;

            }
        }

        
    }
}