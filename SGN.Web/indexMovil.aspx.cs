﻿using SGN.Web.Controles.Servidor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGN.Web
{
    public partial class indexMovil : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Response.Expires = 0;

                //if (Session["usuario"] == null)
                //{
                //    Response.Redirect("login.aspx");
                //}
            }
        }
    }
}