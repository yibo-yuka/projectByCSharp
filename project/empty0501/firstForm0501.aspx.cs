using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace empty0501
{
    public partial class firstForm0501 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("Hello.Today is May 1.");
            string tt = "21:00";
            Response.Write($"There is a holiday party in {tt}");
        }
    }
}