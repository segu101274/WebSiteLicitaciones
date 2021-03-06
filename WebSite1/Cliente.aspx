﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Cliente.aspx.cs" Inherits="Cliente" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <div class="form-horizontal">
            <div class="text-capitalize text-left h3 bg-success">
                <strong>Mantenimiento de Cliente</strong>
            </div>
            <div class="form-group has-success has-feedback">
                <label class="control-label col-sm-3" for="inputSuccess3">Código de cliente</label>
                <div class="col-sm-2">
                    <dx:ASPxTextBox ID="txtCodCliente" runat="server" CssClass="form-control" MaxLength="2" onkeypress="return numbersonly(event);" onkeyup="nextControl(this);">
                    </dx:ASPxTextBox>
                </div>
            </div>
            <div class="form-group has-success has-feedback">
                <label class="control-label col-sm-3" for="inputSuccess3">RUC</label>
                <div class="col-sm-6">
                    <dx:ASPxTextBox ID="txtRUC" runat="server" CssClass="form-control" MaxLength="11">
                    </dx:ASPxTextBox>
                </div>
            </div>
            <div class="form-group has-success has-feedback">
                <label class="control-label col-sm-3" for="inputSuccess3">Razón Social</label>
                <div class="col-sm-6">
                    <dx:ASPxTextBox ID="txtRazonSocial" runat="server" CssClass="form-control" MaxLength="50">
                    </dx:ASPxTextBox>
                </div>
            </div>
            <div class="form-group has-success has-feedback">
                <label class="control-label col-sm-3" for="inputSuccess3">Usuario</label>
                <div class="col-sm-6">
                    <dx:ASPxTextBox ID="txtUsuario" runat="server" CssClass="form-control" MaxLength="50">
                    </dx:ASPxTextBox>
                </div>
            </div>
            <div class="form-group has-success has-feedback">
                <label class="control-label col-sm-3" for="inputSuccess3">Contraseña</label>
                <div class="col-sm-6">
                    <dx:ASPxTextBox ID="txtContraseña" runat="server" CssClass="form-control" MaxLength="8">
                    </dx:ASPxTextBox>
                </div>
            </div>
            <div class="form-group has-success has-feedback">
                <label class="control-label col-sm-3" for="inputSuccess3">Abonado</label>
                <div class="col-sm-1">
                    <asp:CheckBox ID="ckbAbonado" runat="server" CssClass="form-control" Text=" " />
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

                    <dx:ASPxGridView ID="gdvListaCliente" runat="server" AutoGenerateColumns="False" Width="80%"
                        OnRowCommand="gdvListaActividades_RowCommand" KeyFieldName="CodCli"
                        OnBeforeColumnSortingGrouping="gdvListaActividades_BeforeColumnSortingGrouping"
                        OnBeforeGetCallbackResult="gdvListaActividades_BeforeGetCallbackResult"
                        OnBeforeHeaderFilterFillItems="gdvListaActividades_BeforeHeaderFilterFillItems"
                        OnPageIndexChanged="gdvListaActividades_PageIndexChanged"
                        OnCustomGroupDisplayText="gdvListaActividades_CustomGroupDisplayText">
                        <Columns>
                            <dx:GridViewCommandColumn ShowClearFilterButton="True" VisibleIndex="0" Width="1px">
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataTextColumn Caption="Código" VisibleIndex="1" Width="30px" FieldName="CodCli">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="RUC" VisibleIndex="2" Width="150px" FieldName="ruccli">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Razón Social" VisibleIndex="2" Width="150px" FieldName="razsocCli">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Usuario" VisibleIndex="2" Width="150px" FieldName="corCli">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Contraseña" VisibleIndex="2" Width="150px" FieldName="pascli">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Fec. Registro" VisibleIndex="2" Width="150px" FieldName="fecRegCli">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Estado" VisibleIndex="4" Width="70px" FieldName="EstCli">
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

