using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Xml;

namespace edc_trabalho_final
{
    /// <summary>
    /// Summary description for PrezisWebServices
    /// </summary>
    [WebService(Namespace = "https://localhost:44362/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class PrezisWebServices : System.Web.Services.WebService
    {

        [WebMethod]
        public XmlDocument Prezis()
        {
            XmlDocument xml = new XmlDocument();
            xml.Load("C:/Users/hugof/Documents/Visual Studio 2015/Projects/edc-trabalho-final/edc-trabalho-final/App_Data/presidentes_ws.xml");
            return xml;
            //return xml.OuterXml;

            //return  System.IO.File.ReadAllText("C:/Users/hugof/Documents/Visual Studio 2015/Projects/edc-trabalho-final/edc-trabalho-final/App_Data/presidentes_ws.xml");
        }
    }
}
