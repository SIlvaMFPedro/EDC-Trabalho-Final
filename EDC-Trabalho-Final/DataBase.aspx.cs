using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;

namespace EDC_Trabalho_Final
{
    public partial class DataBase : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //Populating a Datatable from database;
                DataTable dt = this.GetData();

                //Building an HTML string;
                StringBuilder html = new StringBuilder();

                //Table start
                html.Append("<table border = '1' class = 'table table-bordered' >");

                //Building the header row
                html.Append("<tr>");
                foreach(DataColumn col in dt.Columns)
                {
                    html.Append("<th>");
                    html.Append(col.ColumnName);
                    html.Append("</th>");

                }
                //Table End;
                html.Append("</table>");

                //Append the Html string to PlaceHolder;
                PlaceHolder1.Controls.Add(new Literal { Text = html.ToString() });
            }
        }
        private DataTable GetData()
        {
            SqlConnection connect = DBconnection.getConnection();

            string CmdString = "SELECT * FROM udf_show_presidents()";
            SqlCommand command = new SqlCommand(CmdString, connect);
            SqlDataAdapter sda = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }
    }
}