using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

public partial class Cliente : System.Web.UI.Page
{
    private static string _sTipoTranc;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        LimpiarControles();
    }
    protected void ibtnNuevo_Click(object sender, EventArgs e)
    {
        LimpiarControles();
    }
    protected void ibtnGrabar_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtCodCliente.Text.Trim() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Confirm value", "alert('Ingrese código de Cliente.')", true);
                return;
            }
            if (txtRUC.Text.ToString() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Confirm value", "alert('Ingrese el RUC del Cliente.')", true);
                return;
            }

            if (txtRazonSocial.Text.ToString() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Confirm value", "alert('Ingrese la Razón Social del Cliente.')", true);
                return;
            }
            if (txtUsuario.Text.ToString() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Confirm value", "alert('Ingrese el usuario del Cliente.')", true);
                return;
            }
            if (txtContraseña.Text.ToString() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Confirm value", "alert('Ingrese la contraseña del Cliente.')", true);
                return;
            }
            if (ValidarDuplicidad(Convert.ToInt32(txtCodCliente.Text.Trim())))
            {
                if (_sTipoTranc != "E")
                {
                    if (Guardar(Convert.ToInt32(txtCodCliente.Text), txtRUC.Text, txtRazonSocial.Text, txtUsuario.Text, txtContraseña.Text, ckbAbonado.Checked ? 1 : 0, ckbEstado.Checked ? 1 : 0))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Confirm value", "alert('" + (_sTipoTranc == "E" ? "Actualización " : "Registro ") + "de Cliente satisfactorio.')", true);
                        LimpiarControles();
                    }
                }
                else
                {
                    if (Update(Convert.ToInt32(txtCodCliente.Text), txtRUC.Text, txtRazonSocial.Text, txtUsuario.Text, txtContraseña.Text, ckbAbonado.Checked ? 1 : 0, ckbEstado.Checked ? 1 : 0))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Confirm value", "alert('" + (_sTipoTranc == "E" ? "Actualización " : "Registro ") + "de Cliente satisfactorio.')", true);
                        LimpiarControles();
                    }
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Confirm value", "alert('El código de la Cliente ya se encuentra registrado. Por favor verificar.')", true);
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message);
        }
    }

    #region
    internal void LimpiarControles()
    {
        txtCodCliente.Text = string.Empty;
        txtRUC.Text = string.Empty;
        txtRazonSocial.Text = string.Empty;
        txtUsuario.Text = string.Empty;
        txtContraseña.Text = string.Empty;
        ckbAbonado.Checked = false;
        ckbEstado.Checked = true;
        Listar();
        _sTipoTranc = "";
        txtCodCliente.Enabled = true;
    }
    internal void Listar()
    {
        try
        {
            var dt = new DataTable();
            MySqlConnection conn = new MySqlConnection(new Conexion().Conex().ToString());
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select CodCli,ruccli,razsocCli,corCli,pascli,fecRegCli,tipTarCli,numTarCli,flgAboCli,EstCli from Cliente";
            conn.Open();
            //cmd.ExecuteNonQuery();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            gdvListaCliente.DataSource = dt;
            gdvListaCliente.DataBind();
        }
        catch (Exception)
        {

        }
    }
    internal bool Guardar(int CodCli, string ruccli, string razsocCli, string corCli, string pascli, int flgAboCli, int EstCli)
    {
        var vlresutl = true;
        try
        {
            MySqlConnection conn = new MySqlConnection(new Conexion().Conex().ToString());
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO Cliente (CodCli,ruccli,razsocCli,corCli,pascli,fecRegCli,tipTarCli,numTarCli,flgAboCli,EstCli) value (" +
                            CodCli + ",'" + ruccli + "','" + razsocCli + "','" + corCli + "','" + pascli + "',CURRENT_DATE,NULL,NULL," + flgAboCli + "," + EstCli + ")";
            conn.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception)
        {
            vlresutl = false;
        }
        return vlresutl;
    }
    internal bool Update(int CodCli, string ruccli, string razsocCli, string corCli, string pascli, int flgAboCli, int EstCli)
    {
        var vlresutl = true;
        try
        {
            MySqlConnection conn = new MySqlConnection(new Conexion().Conex().ToString());
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "update Cliente  set ruccli = '" + ruccli + "',razsocCli ='" + razsocCli + "',corCli='" + corCli + "',pascli = '" + pascli + "',flgAboCli=" + flgAboCli + ",EstCli= " + EstCli + "  where CodCli =" + CodCli;
            conn.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception)
        {
            vlresutl = false;
        }
        return vlresutl;
    }
    internal void Edit(int CodCli)
    {
        var dt = new DataTable();
        MySqlConnection conn = new MySqlConnection(new Conexion().Conex().ToString());
        MySqlCommand cmd = conn.CreateCommand();
        cmd.CommandText = "select CodCli,ruccli,razsocCli,corCli,pascli,fecRegCli,tipTarCli,numTarCli,flgAboCli,EstCli from Cliente where CodCli = " + CodCli.ToString();
        conn.Open();
        //cmd.ExecuteNonQuery();
        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            //_CodAct = Convert.ToInt32(beActividades.CodAct.ToString());
            txtCodCliente.Text = dt.Rows[0]["CodCli"].ToString();
            txtCodCliente.Enabled = false;
            txtRUC.Text = dt.Rows[0]["ruccli"].ToString();
            txtRazonSocial.Text = dt.Rows[0]["razsocCli"].ToString();
            txtUsuario.Text = dt.Rows[0]["corCli"].ToString();
            txtContraseña.Text = dt.Rows[0]["pascli"].ToString();
            ckbAbonado.Checked = Convert.ToBoolean(dt.Rows[0]["flgAboCli"]);
            ckbEstado.Checked = Convert.ToBoolean(dt.Rows[0]["EstCli"]);
        }
    }
    internal bool ValidarDuplicidad(int CodCli)
    {
        bool vlresutl = true;
        if (_sTipoTranc != "E")
        {
            var dt = new DataTable();
            MySqlConnection conn = new MySqlConnection(new Conexion().Conex().ToString());
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select CodCli,ruccli,razsocCli,corCli,pascli,fecRegCli,tipTarCli,numTarCli,flgAboCli,EstCli from Cliente where CodCli = " + CodCli.ToString();
            conn.Open();
            //cmd.ExecuteNonQuery();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                vlresutl = false;
            }
        }
        return vlresutl;
    }
    internal bool Eliminar(int CodCli)
    {
        var vlResult = true;
        try
        {
            MySqlConnection conn = new MySqlConnection(new Conexion().Conex().ToString());
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "update Cliente set EstCli = 0 where CodCli = " + CodCli.ToString();
            conn.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception)
        {

            throw;
        }
        return vlResult;
    }
    #endregion

    protected void gdvListaActividades_BeforeColumnSortingGrouping(object sender, DevExpress.Web.ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
    {
        try
        {
            Listar();
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message);
        }
    }
    protected void gdvListaActividades_BeforeGetCallbackResult(object sender, EventArgs e)
    {
        try
        {
            Listar();
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message);
        }
    }
    protected void gdvListaActividades_BeforeHeaderFilterFillItems(object sender, DevExpress.Web.ASPxGridViewBeforeHeaderFilterFillItemsEventArgs e)
    {
        try
        {
            Listar();
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message);
        }
    }
    protected void gdvListaActividades_CustomGroupDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
    {
        try
        {
            Listar();
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message);
        }
    }
    protected void gdvListaActividades_PageIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Listar();
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message);
        }
    }
    protected void gdvListaActividades_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
    {
        try
        {
            LimpiarControles();
            var lkEditar = (ImageButton)(e.CommandSource);
            string sTipoEvento = lkEditar.AlternateText.ToString(CultureInfo.InvariantCulture);
            _sTipoTranc = sTipoEvento;
            var _CodAct = Convert.ToInt32(e.KeyValue);

            if (sTipoEvento.Trim().Equals("E"))
            {
                Edit(_CodAct);
            }
            else if (sTipoEvento.Trim().Equals("X"))
            {
                if (Eliminar(_CodAct))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Confirm value", "alert('Se ha eliminado la actividad " + _CodAct.ToString() + ".')", true);
                    LimpiarControles();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Confirm value", "alert('No se puede eliminar la actividad porque tiene datos de producción registrados. Por favor verificar.')", true);
                }
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message);
        }
    }

}
