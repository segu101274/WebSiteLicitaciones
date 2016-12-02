using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            txtUsuario.Focus();

    }
    protected void btnIngresar_Click(object sender, EventArgs e)
    {
        var usu = txtUsuario.Text;
        var pass = txtContraseña.Text;

        if (usu == "admin" && pass == "admin")
        {
            Response.Redirect("Menu.aspx", false);
        }
        else 
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Confirm value", "alert('Usuario no autorizado.')", true);
            return;
        }
    }
}