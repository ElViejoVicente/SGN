using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using GPS.Negocio.Operativa;

namespace GPB.Web
{
    /// <summary>
    /// Descripción breve de SessionHeartbeat
    /// </summary>
    public class SessionHeartbeat : IHttpHandler, IRequiresSessionState
    {
        public bool IsReusable { get { return true; } }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            try
            {
                DatosUsuario datosUsuario = new DatosUsuario();                 

                string strJson = new StreamReader(context.Request.InputStream).ReadToEnd();
                userInfo objUsr = Deserialize<userInfo>(strJson);

                if (objUsr != null)
                {
                    int usuario = objUsr.usuario;

                    datosUsuario.actualizaHoraBloqueo(idUsuario: usuario);
                    // mantiene viva la session del iis que por defecto es ASP.NET_SessionId
                    context.Session["Heartbeat"] = DateTime.Now;      
                }
                else
                {
                    context.Response.Write("No Data");
                }

            }
            catch (Exception ex)
            {
                context.Response.Write("Error :" + ex.Message);
            }
        }

        public class userInfo
        {
            public int usuario { get; set; }

        }


        // Convierte un JSON string tipo de objeto T
        public T Deserialize<T>(string context)
        {
            string jsonData = context;
            var obj = (T)new JavaScriptSerializer().Deserialize<T>(jsonData);
            return obj;
        }


    }
}