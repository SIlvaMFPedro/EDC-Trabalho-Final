using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace edc_trabalho_final
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            System.Xml.XmlDocument xdoc = new System.Xml.XmlDocument();
            xdoc.Load("C:/Users/hugof/Documents/Visual Studio 2015/Projects/edc-trabalho-final/edc-trabalho-final/App_Data/presidents.xml");
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["presidentesConnectionString1"].ToString());
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "insert into dbo.PresidentSet(xml_presidentes) values (@theXmlDoc)";
            cmd.Parameters.Add(new SqlParameter()
            {
                ParameterName = "theXmlDoc",
                Value = xdoc.OuterXml
            });
            cmd.Connection = con;
            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();
            */
         
        }
    }
}