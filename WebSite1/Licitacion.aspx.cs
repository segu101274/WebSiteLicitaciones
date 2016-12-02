using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

public partial class Licitacion : System.Web.UI.Page
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
            if (txtcodLic.Text.Trim() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Confirm value", "alert('Ingrese código de licitación.')", true);
                return;
            }
            if (txtnomLic.Text.ToString() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Confirm value", "alert('Ingrese el nombre de la licitación.')", true);
                return;
            }

            if (txtdesLic.Text.ToString() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Confirm value", "alert('Ingrese la descripción de la licitación.')", true);
                return;
            }

            if (ValidarDuplicidad(Convert.ToInt32(txtcodLic.Text.Trim())))
            {
                if (_sTipoTranc != "E")
                {
                    if (Guardar(Convert.ToInt32(txtcodLic.Text), txtnomLic.Text, txtdesLic.Text, txtnorAplLic.Text, Convert.ToDouble(txtvalRef.Text), Convert.ToDateTime(txtfecPubLic.Text), Convert.ToDateTime(txtfecTerLic.Text), txtmonLic.Text, Convert.ToInt32(txtverSeaLic.Text), ckbEstado.Checked ? 1 : 0, Convert.ToInt32(dpdcodEnt.Value.ToString()), Convert.ToInt32(dpdcodCat.Value.ToString())))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Confirm value", "alert('" + (_sTipoTranc == "E" ? "Actualización " : "Registro ") + "de licitación satisfactorio.')", true);
                        LimpiarControles();
                    }
                }
                else
                {
                    if (Update(Convert.ToInt32(txtcodLic.Text), txtnomLic.Text, txtdesLic.Text, txtnorAplLic.Text, Convert.ToDouble(txtvalRef.Text), Convert.ToDateTime(txtfecPubLic.Text), Convert.ToDateTime(txtfecTerLic.Text), txtmonLic.Text, Convert.ToInt32(txtverSeaLic.Text), ckbEstado.Checked ? 1 : 0, Convert.ToInt32(dpdcodEnt.Value.ToString()), Convert.ToInt32(dpdcodCat.Value.ToString())))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Confirm value", "alert('" + (_sTipoTranc == "E" ? "Actualización " : "Registro ") + "de licitación satisfactorio.')", true);
                        LimpiarControles();
                    }
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Confirm value", "alert('El código de la licitación ya se encuentra registrado. Por favor verificar.')", true);
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
        txtcodLic.Text = string.Empty;
        txtdesLic.Text = string.Empty;
        txtfecPubLic.Text = string.Empty;
        txtfecTerLic.Text = string.Empty;
        txtmonLic.Text = string.Empty;
        txtnomLic.Text = string.Empty;
        txtnorAplLic.Text = string.Empty;
        txtvalRef.Text = string.Empty;
        txtverSeaLic.Text = string.Empty;
        ckbEstado.Checked = true;
        Listar();
        _sTipoTranc = "";
        txtcodLic.Enabled = true;
        ListarCategoria();
        if (dpdcodCat.Items.Count > 0)
        {
            dpdcodCat.SelectedIndex = 0;
        }

        ListarEntidad();
        if (dpdcodEnt.Items.Count > 0)
        {
            dpdcodEnt.SelectedIndex = 0;
        }
    }
    internal void Listar()
    {
        try
        {
            var dt = new DataTable();
            MySqlConnection conn = new MySqlConnection(new Conexion().Conex().ToString());
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select  codLic,  nomLic,  desLic,  norAplLic,  valRef,  fecPubLic,  fecTerLic,  monLic,  verSeaLic,  estLic,  codEnt,  codCat from LICITACION";
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
    internal bool Guardar(int codLic, string nomLic, string desLic, string norAplLic, double valRef, DateTime fecPubLic, DateTime fecTerLic, string monLic, int verSeaLic, int estLic, int codEnt, int codCat)
    {
        var vlresutl = true;
        try
        {
            MySqlConnection conn = new MySqlConnection(new Conexion().Conex().ToString());
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO LICITACION (codLic,  nomLic,  desLic,  norAplLic,  valRef,  fecPubLic,  fecTerLic,  monLic,  verSeaLic,  estLic,  codEnt,  codCat) value (" +
                              codLic + ",'" + nomLic + "','" + desLic + "','" + norAplLic + "'," + valRef + ",CURRENT_DATE,CURRENT_DATE,'" + monLic + "'," + verSeaLic + "," + estLic + "," + codEnt + "," + codCat + ")";
            conn.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception)
        {
            vlresutl = false;
        }
        return vlresutl;
    }
    internal bool Update(int codLic, string nomLic, string desLic, string norAplLic, double valRef, DateTime fecPubLic, DateTime fecTerLic, string monLic, int verSeaLic, int estLic, int codEnt, int codCat)
    {
        var vlresutl = true;
        try
        {
            MySqlConnection conn = new MySqlConnection(new Conexion().Conex().ToString());
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "update LICITACION  set nomLic = '" + nomLic + "', desLic= '" + desLic + "',  norAplLic='" + norAplLic + "',  valRef=" + valRef + ",  fecPubLic = CURRENT_DATE,  fecTerLic=CURRENT_DATE,  monLic ='" + monLic + "',  verSeaLic=" + verSeaLic + ",  estLic=" + estLic + ",  codEnt=" + codEnt + ",  codCat=" + codCat + "  where codLic =" + codLic;
            conn.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception)
        {
            vlresutl = false;
        }
        return vlresutl;
    }
    internal void Edit(int codLic)
    {
        var dt = new DataTable();
        MySqlConnection conn = new MySqlConnection(new Conexion().Conex().ToString());
        MySqlCommand cmd = conn.CreateCommand();
        cmd.CommandText = "select  codLic,  nomLic,  desLic,  norAplLic,  valRef,  fecPubLic,  fecTerLic,  monLic,  verSeaLic,  estLic,  codEnt,  codCat from LICITACION where codLic = " + codLic.ToString();
        conn.Open();
        //cmd.ExecuteNonQuery();
        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            //_CodAct = Convert.ToInt32(beActividades.CodAct.ToString());
            txtcodLic.Text = dt.Rows[0]["codLic"].ToString();
            txtcodLic.Enabled = false;
            txtdesLic.Text = dt.Rows[0]["desLic"].ToString();
            txtfecPubLic.Date = Convert.ToDateTime(dt.Rows[0]["fecPubLic"].ToString());
            txtfecTerLic.Date = Convert.ToDateTime(dt.Rows[0]["fecTerLic"].ToString());
            txtmonLic.Text = dt.Rows[0]["monLic"].ToString();
            txtnomLic.Text = dt.Rows[0]["nomLic"].ToString();
            txtnorAplLic.Text = dt.Rows[0]["norAplLic"].ToString();
            txtvalRef.Text = dt.Rows[0]["valRef"].ToString();
            txtverSeaLic.Text = dt.Rows[0]["verSeaLic"].ToString();
            ListarCategoria();
            dpdcodCat.Value = dt.Rows[0]["codCat"].ToString();
            ListarEntidad();
            dpdcodEnt.Value = dt.Rows[0]["codEnt"].ToString();
            ckbEstado.Checked = Convert.ToBoolean(dt.Rows[0]["estLic"]);
        }
    }
    internal bool ValidarDuplicidad(int codLic)
    {
        bool vlresutl = true;
        if (_sTipoTranc != "E")
        {
            var dt = new DataTable();
            MySqlConnection conn = new MySqlConnection(new Conexion().Conex().ToString());
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select  codLic,  nomLic,  desLic,  norAplLic,  valRef,  fecPubLic,  fecTerLic,  monLic,  verSeaLic,  estLic,  codEnt,  codCat from LICITACION where codLic = " + codLic.ToString();
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
    internal void ListarEntidad()
    {
        var dt = new DataTable();
        dt.Columns.Add("codEnt");
        dt.Columns.Add("desEnt");

        DataRow dr;
        //dr = dt.NewRow();
        //dr["codCat"] = "0";
        //dr["desCat"] = "<--Seleccione-->";
        //dt.Rows.Add(dr);
        var dts = new DataTable();
        MySqlConnection conn = new MySqlConnection(new Conexion().Conex().ToString());
        MySqlCommand cmd = conn.CreateCommand();
        cmd.CommandText = "select  codEnt,desEnt from Entidad_convocante where estEnt=1";
        conn.Open();
        //cmd.ExecuteNonQuery();
        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
        da.Fill(dts);

        foreach (DataRow item in dts.Rows)
        {
            dr = dt.NewRow();
            dr["codEnt"] = item["codEnt"].ToString();
            dr["desEnt"] = item["desEnt"].ToString();
            dt.Rows.Add(dr);
        }
        dpdcodEnt.DataSource = dt;
        dpdcodEnt.ValueField = "codEnt";
        dpdcodEnt.TextField = "desEnt";
        dpdcodEnt.DataBind();
    }

    internal bool Eliminar(int codLic)
    {
        var vlResult = true;
        try
        {
            MySqlConnection conn = new MySqlConnection(new Conexion().Conex().ToString());
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "update LICITACION set estLic = 0 where codLic = " + codLic.ToString();
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
