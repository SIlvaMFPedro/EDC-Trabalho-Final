using EDC_Trabalho_Final.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EDC_Trabalho_Final
{
    public partial class ChangeLanguage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string language = "pt";

                try
                {
                    language = Request["Language"];
                    if (!Languages.domains.ContainsKey(language))
                    {
                        language = "pt";
                    }
                }
                catch (Exception)
                {
                    language = "pt";
                }

                HttpCookie language_cookie = new HttpCookie("UserLanguage");
                language_cookie.Value = language;
                language_cookie.Expires = DateTime.Now.AddDays(15d);
                Response.Cookies.Add(language_cookie);

                Response.Redirect(Request.UrlReferrer.ToString());

            }
        }
    }
}