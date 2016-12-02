using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

public partial class Suscripcion : System.Web.UI.Page
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
            if (txtcodSus.Text.Trim() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Confirm value", "alert('Ingrese código de suscripción.')", true);
                return;
            }


            if (ValidarDuplicidad(Convert.ToInt32(txtcodSus.Text.Trim())))
            {
                if (_sTipoTranc != "E")
                {
                    if (Guardar(Convert.ToInt32(txtcodSus.Text), Convert.ToInt32( dpdcodCli.Value.ToString()), Convert.ToInt32(dpdcodCat.Value.ToString()),ckbEstado.Checked ? 1 : 0))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Confirm value", "alert('" + (_sTipoTranc == "E" ? "Actualización " : "Registro ") + "de suscripción satisfactorio.')", true);
                        LimpiarControles();
                    }
                }
                else
                {
                    if (Update(Convert.ToInt32(txtcodSus.Text), Convert.ToInt32(dpdcodCli.Value.ToString()), Convert.ToInt32(dpdcodCat.Value.ToString()), ckbEstado.Checked ? 1 : 0))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Confirm value", "alert('" + (_sTipoTranc == "E" ? "Actualización " : "Registro ") + "de suscripción satisfactorio.')", true);
                        LimpiarControles();
                    }
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Confirm value", "alert('El código de la suscripción ya se encuentra registrado. Por favor verificar.')", true);
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
        txtcodSus.Text = string.Empty;
        ckbEstado.Checked = true;
        Listar();
        _sTipoTranc = "";
        txtcodSus.Enabled = true;
        ListarCategoria();
        if (dpdcodCat.Items.Count > 0)
        {
            dpdcodCat.SelectedIndex = 0;
        }

        ListarCliente();
        if (dpdcodCli.Items.Count > 0)
        {
            dpdcodCli.SelectedIndex = 0;
        }
    }
    internal void Listar()
    {
        try
        {
            var dt = new DataTable();
            MySqlConnection conn = new MySqlConnection(new Conexion().Conex().ToString());
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select  codSus,codCli,codCat,estSus from suscripcion";
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
    internal bool Guardar(int codSus, int codCli, int codCat, int estSus)
    {
        var vlresutl = true;
        try
        {
            MySqlConnection conn = new MySqlConnection(new Conexion().Conex().ToString());
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO suscripcion (codSus,codCli,codCat,estSus) value (" +
                              codSus + "," + codCli + "," + codCat + "," + estSus + ")";
            conn.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception)
        {
            vlresutl = false;
        }
        return vlresutl;
    }
    internal bool Update(int codSus, int codCli, int codCat, int estSus)
    {
        var vlresutl = true;
        try
        {
            MySqlConnection conn = new MySqlConnection(new Conexion().Conex().ToString());
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "update suscripcion  set codCli = " + codCli + ", codCat= " + codCat + ",estSus =" + estSus + "  where codSus =" + codSus;
            conn.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception)
        {
            vlresutl = false;
        }
        return vlresutl;
    }
    internal void Edit(int codSus)
    {
        var dt = new DataTable();
        MySqlConnection conn = new MySqlConnection(new Conexion().Conex().ToString());
        MySqlCommand cmd = conn.CreateCommand();
        cmd.CommandText = "select  codSus,codCli,codCat,estSus from suscripcion where codSus = " + codSus.ToString();
        conn.Open();
        //cmd.ExecuteNonQuery();
        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            //_CodAct = Convert.ToInt32(beActividades.CodAct.ToString());
            txtcodSus.Text = dt.Rows[0]["codSus"].ToString();
            txtcodSus.Enabled = false;
            ListarCategoria();
            dpdcodCat.Value = dt.Rows[0]["codCat"].ToString();
            ListarCliente();
            dpdcodCli.Value = dt.Rows[0]["codCli"].ToString();
            ckbEstado.Checked = Convert.ToBoolean(dt.Rows[0]["estSus"]);
        }
    }
    internal bool ValidarDuplicidad(int codSus)
    {
        bool vlresutl = true;
        if (_sTipoTranc != "E")
        {
            var dt = new DataTable();
            MySqlConnection conn = new MySqlConnection(new Conexion().Conex().ToString());
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select  codSus,codCli,codCat,estSus from suscripcion where codSus = " + codSus.ToString();
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


    internal void ListarCategoria()
    {
        var dt = new DataTable();
        dt.Columns.Add("codCat");
        dt.Columns.Add("desCat");

        DataRow dr;
        //dr = dt.NewRow();
        //dr["codCat"] = "0";
        //dr["desCat"] = "<--Seleccione-->";
        //dt.Rows.Add(dr);
        var dts = new DataTable();
        MySqlConnection conn = new MySqlConnection(new Conexion().Conex().ToString());
        MySqlCommand cmd = conn.CreateCommand();
        cmd.CommandText = "select  codCat,desCat from Categoria where estCat=1";
        conn.Open();
        //cmd.ExecuteNonQuery();
        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
        da.Fill(dts);

        foreach (DataRow item in dts.Rows)
        {
            dr = dt.NewRow();
            dr["codCat"] = item["codCat"].ToString();
            dr["desCat"] = item["desCat"].ToString();
            dt.Rows.Add(dr);
        }
        dpdcodCat.DataSource = dt;
        dpdcodCat.ValueField = "codCat";
        dpdcodCat.TextField = "desCat";

        dpdcodCat.DataBind();
    }
    internal void ListarCliente()
    {
        var dt = new DataTable();
        dt.Columns.Add("codCli");
        dt.Columns.Add("razSocCli");

        DataRow dr;
        //dr = dt.NewRow();
        //dr["codCat"] = "0";
        //dr["desCat"] = "<--Seleccione-->";
        //dt.Rows.Add(dr);
        var dts = new DataTable();
        MySqlConnection conn = new MySqlConnection(new Conexion().Conex().ToString());
        MySqlCommand cmd = conn.CreateCommand();
        cmd.CommandText = "select  codCli,razSocCli from Cliente where estCli=1";
        conn.Open();
        //cmd.ExecuteNonQuery();
        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
        da.Fill(dts);

        foreach (DataRow item in dts.Rows)
        {
            dr = dt.NewRow();
            dr["codCli"] = item["codCli"].ToString();
            dr["razSocCli"] = item["razSocCli"].ToString();
            dt.Rows.Add(dr);
        }
        dpdcodCli.DataSource = dt;
        dpdcodCli.ValueField = "codCli";
        dpdcodCli.TextField = "razSocCli";
        dpdcodCli.DataBind();
    }

    internal bool Eliminar(int codSus)
    {
        var vlResult = true;
        try
        {
            MySqlConnection conn = new MySqlConnection(new Conexion().Conex().ToString());
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "update suscripcion set estSus = 0 where codSus = " + codSus.ToString();
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
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Confirm value", "alert('Se ha eliminado la suscripción " + _CodAct.ToString() + ".')", true);
                    LimpiarControles();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Confirm value", "alert('No se puede eliminar la suscripción porque tiene datos de producción registrados. Por favor verificar.')", true);
                }
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message);
        }
    }

}
