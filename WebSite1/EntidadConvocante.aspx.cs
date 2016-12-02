using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

public partial class EntidadConvocante : System.Web.UI.Page
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
            if (txtCod.Text.Trim() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Confirm value", "alert('Ingrese código de categoria.')", true);
                return;
            }
            if (txtDescripcion.Text.ToString() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Confirm value", "alert('Ingrese la descripción de la categoria.')", true);
                return;
            }




            if (ValidarDuplicidad(Convert.ToInt32(txtCod.Text.Trim())))
            {
                if (_sTipoTranc != "E")
                {
                    if (Guardar(Convert.ToInt32(txtCod.Text),txtRucEntidad.Text, txtDescripcion.Text,txtDireccion.Text, ckbEstado.Checked ? 1 : 0))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Confirm value", "alert('" + (_sTipoTranc == "E" ? "Actualización " : "Registro ") + "de Entidad convocante satisfactorio.')", true);
                        LimpiarControles();
                    }
                }
                else
                {
                    if (Update(Convert.ToInt32(txtCod.Text), txtRucEntidad.Text, txtDescripcion.Text, txtDireccion.Text, ckbEstado.Checked ? 1 : 0))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Confirm value", "alert('" + (_sTipoTranc == "E" ? "Actualización " : "Registro ") + "de Entidad convocante satisfactorio.')", true);
                        LimpiarControles();
                    }
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Confirm value", "alert('El código de la Entidad convocante ya se encuentra registrado. Por favor verificar.')", true);
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
        txtCod.Text = string.Empty;
        txtRucEntidad.Text = string.Empty;
        txtDescripcion.Text = string.Empty;
        txtDireccion.Text = string.Empty;
        ckbEstado.Checked = true;
        Listar();
        _sTipoTranc = "";
        txtCod.Enabled = true;
    }
    internal void Listar()
    {
        try
        {
            var dt = new DataTable();
            MySqlConnection conn = new MySqlConnection(new Conexion().Conex().ToString());
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select   codEnt,  rucEnt,  desEnt,  dirEnt,  estEnt from ENTIDAD_CONVOCANTE";
            conn.Open();
            //cmd.ExecuteNonQuery();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            gdvLista.DataSource = dt;
            gdvLista.DataBind();
        }
        catch (Exception)
        {

        }
    }
    internal bool Guardar(int codEnt, string rucEnt, string desEnt, string dirEnt, int estEnt)
    {
        var vlresutl = true;
        try
        {
            MySqlConnection conn = new MySqlConnection(new Conexion().Conex().ToString());
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO ENTIDAD_CONVOCANTE (codEnt,  rucEnt,  desEnt,  dirEnt,  estEnt) value (" + codEnt.ToString() + ",'" + rucEnt + "','" + desEnt + "','" + dirEnt + "'," + estEnt + ")";
            conn.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception)
        {
            vlresutl = false;
        }
        return vlresutl;
    }
    internal bool Update(int codEnt, string rucEnt, string desEnt, string dirEnt, int estEnt)
    {
        var vlresutl = true;
        try
        {
            MySqlConnection conn = new MySqlConnection(new Conexion().Conex().ToString());
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "update ENTIDAD_CONVOCANTE  set rucEnt = '" + rucEnt + "',desEnt = '" + desEnt + "', dirEnt='" + dirEnt + "',EstEnt =" + estEnt + "  where codEnt =" + codEnt;
            conn.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception)
        {
            vlresutl = false;
        }
        return vlresutl;
    }
    internal void Edit(int codEnt)
    {
        var dt = new DataTable();
        MySqlConnection conn = new MySqlConnection(new Conexion().Conex().ToString());
        MySqlCommand cmd = conn.CreateCommand();
        cmd.CommandText = "select   codEnt,  rucEnt,  desEnt,  dirEnt,  estEnt from ENTIDAD_CONVOCANTE where codEnt = " + codEnt.ToString();
        conn.Open();
        //cmd.ExecuteNonQuery();
        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            //_CodAct = Convert.ToInt32(beActividades.CodAct.ToString());
            txtCod.Text = dt.Rows[0]["codEnt"].ToString();
            txtCod.Enabled = false;
            txtDescripcion.Text = dt.Rows[0]["desEnt"].ToString();
            txtRucEntidad.Text = dt.Rows[0]["rucEnt"].ToString();
            txtDireccion.Text = dt.Rows[0]["dirEnt"].ToString();
            ckbEstado.Checked = Convert.ToBoolean(dt.Rows[0]["estEnt"]);
        }
    }
    internal bool ValidarDuplicidad(int CodCat)
    {
        bool vlresutl = true;
        if (_sTipoTranc != "E")
        {
            var dt = new DataTable();
            MySqlConnection conn = new MySqlConnection(new Conexion().Conex().ToString());
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select   codEnt,  rucEnt,  desEnt,  dirEnt,  estEnt from ENTIDAD_CONVOCANTE where codEnt = " + CodCat.ToString();
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
    internal bool Eliminar(int codEnt)
    {
        var vlResult = true;
        try
        {
            MySqlConnection conn = new MySqlConnection(new Conexion().Conex().ToString());
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "update ENTIDAD_CONVOCANTE set estEnt = 0 where codEnt = " + codEnt.ToString();
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
