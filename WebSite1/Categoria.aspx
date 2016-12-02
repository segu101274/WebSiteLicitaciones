<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Categoria.aspx.cs" Inherits="Categoria" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function numbersonly(e) {
            var unicode = e.charCode ? e.charCode : e.keyCode;
            if (unicode != 8) { //if the key isn't the backspace key (which we should allow)
                if (unicode < 48 || unicode > 57) //if not a number
                    return false //disable key press
            }
        }

        function nextControl(el) {
            if (el.value.length < el.getAttribute('maxlength')) return;

            var f = el.form;
            var els = f.elements;
            var x, nextEl;
            for (var i = 0, len = els.length; i < len; i++) {
                x = els[i];
                if (el == x && (nextEl = els[i + 1])) {
                    if (nextEl.focus) nextEl.focus();
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <div class="form-horizontal">
            <div class="text-capitalize text-left h3 bg-success">
                <strong>Mantenimiento de Categoria</strong>
            </div>
            <div class="form-group has-success has-feedback">
                <label class="control-label col-sm-3" for="inputSuccess3">Código de categoria</label>
                <div class="col-sm-2">
                    <dx:ASPxTextBox ID="txtCodCategoria" runat="server" CssClass="form-control" MaxLength="2" onkeypress="return numbersonly(event);" onkeyup="nextControl(this);">
                    </dx:ASPxTextBox>
                </div>
            </div>
            <div class="form-group has-success has-feedback">
                <label class="control-label col-sm-3" for="inputSuccess3">Descripción</label>
                <div class="col-sm-6">
                    <dx:ASPxTextBox ID="txtDescripcion" runat="server" CssClass="form-control" MaxLength="50">
                    </dx:ASPxTextBox>
                </div>
            </div>
            <div class="form-group has-success has-feedback">
                <label class="control-label col-sm-3" for="inputSuccess3">Estado</label>
                <div class="col-sm-1">
                    <asp:CheckBox ID="ckbEstado" runat="server" CssClass="form-control" Text=" " />
                </div>
            </div>
            <div class="form-group has-success has-feedback">
                <br />
                <asp:Button ID="ibtnNuevo" runat="server" OnClick="ibtnNuevo_Click" CssClass="btn btn-default" Text="Nuevo" />&nbsp;&nbsp;
                <asp:Button ID="ibtnGrabar" runat="server" OnClick="ibtnGrabar_Click" CssClass="btn btn-default" Text="Grabar" />
                <br />

            </div>

            <div class="form-group has-success has-feedback">
                <div class="col-sm-12 text-center center-block quitar-float">
                    <center>

                    <dx:ASPxGridView ID="gdvListaCategoria" runat="server" AutoGenerateColumns="False" Width="80%"
                        OnRowCommand="gdvListaActividades_RowCommand" KeyFieldName="CodCat"
                        OnBeforeColumnSortingGrouping="gdvListaActividades_BeforeColumnSortingGrouping"
                        OnBeforeGetCallbackResult="gdvListaActividades_BeforeGetCallbackResult"
                        OnBeforeHeaderFilterFillItems="gdvListaActividades_BeforeHeaderFilterFillItems"
                        OnPageIndexChanged="gdvListaActividades_PageIndexChanged"
                        OnCustomGroupDisplayText="gdvListaActividades_CustomGroupDisplayText">
                        <Columns>
                            <dx:GridViewCommandColumn ShowClearFilterButton="True" VisibleIndex="0" Width="1px">
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataTextColumn Caption="Código" VisibleIndex="1" Width="30px" FieldName="CodCat">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Descripcion" VisibleIndex="2" Width="150px" FieldName="DesCat">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Estado" VisibleIndex="4" Width="70px" FieldName="EstCat">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="5" Width="16px" Caption=" ">
                                <DataItemTemplate>
                                    <asp:ImageButton ID="IbtnEditar" runat="server" AlternateText="E" ImageUrl="~/Img/btnEditar.png"
                                        ToolTip="Editar" Width="14px" OnClientClick="" />
                                </DataItemTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="6" Width="16px" Caption=" ">
                                <DataItemTemplate>
                                    <asp:ImageButton ID="IbtnEliminar" runat="server" AlternateText="X" ImageUrl="~/Img/Delete-icon.png"
                                        ToolTip="Eliminar" Width="14px" OnClientClick="" />
                                </DataItemTemplate>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <Settings ShowGroupPanel="True" ShowFilterRow="True" />
                    </dx:ASPxGridView>
                    </center>
                    <br />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

