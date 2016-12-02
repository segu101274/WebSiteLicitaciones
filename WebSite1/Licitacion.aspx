﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Licitacion.aspx.cs" Inherits="Licitacion" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <div class="form-horizontal">
            <div class="text-capitalize text-left h3 bg-success">
                <strong>Mantenimiento de Licitación</strong>
            </div>
            <div class="form-group has-success has-feedback">
                <label class="control-label col-sm-3" for="inputSuccess3">Código de cliente</label>
                <div class="col-sm-2">
                    <dx:ASPxTextBox ID="txtcodLic" runat="server" CssClass="form-control" MaxLength="2" onkeypress="return numbersonly(event);" onkeyup="nextControl(this);">
                    </dx:ASPxTextBox>
                </div>
            </div>
            <div class="form-group has-success has-feedback">
                <label class="control-label col-sm-3" for="inputSuccess3">Nombre de Lincitación</label>
                <div class="col-sm-6">
                    <dx:ASPxTextBox ID="txtnomLic" runat="server" CssClass="form-control" MaxLength="50">
                    </dx:ASPxTextBox>
                </div>
            </div>
            <div class="form-group has-success has-feedback">
                <label class="control-label col-sm-3" for="inputSuccess3">Descripción de licitación</label>
                <div class="col-sm-6">
                    <dx:ASPxTextBox ID="txtdesLic" runat="server" CssClass="form-control" MaxLength="500">
                    </dx:ASPxTextBox>
                </div>
            </div>
            <div class="form-group has-success has-feedback">
                <label class="control-label col-sm-3" for="inputSuccess3">norAplLic</label>
                <div class="col-sm-6">
                    <dx:ASPxTextBox ID="txtnorAplLic" runat="server" CssClass="form-control" MaxLength="200">
                    </dx:ASPxTextBox>
                </div>
            </div>
            <div class="form-group has-success has-feedback">
                <label class="control-label col-sm-3" for="inputSuccess3">valRef</label>
                <div class="col-sm-6">
                    <dx:ASPxTextBox ID="txtvalRef" runat="server" CssClass="form-control" MaxLength="50">
                    </dx:ASPxTextBox>
                </div>
            </div>
            <div class="form-group has-success has-feedback">
                <label class="control-label col-sm-3" for="inputSuccess3">fecPubLic</label>
                <div class="col-sm-3">
                    <dx:ASPxDateEdit ID="txtfecPubLic" runat="server" CssClass="form-control"></dx:ASPxDateEdit>
                    <%--<dx:ASPxTextBox ID="txtfecPubLic" runat="server" CssClass="form-control" MaxLength="50">
                    </dx:ASPxTextBox>--%>
                </div>
            </div>

            <div class="form-group has-success has-feedback">
                <label class="control-label col-sm-3" for="inputSuccess3">fecTerLic</label>
                <div class="col-sm-3">
                    <dx:ASPxDateEdit ID="txtfecTerLic" runat="server" CssClass="form-control"></dx:ASPxDateEdit>
                    <%-- <dx:ASPxTextBox ID="txtfecTerLic" runat="server" CssClass="form-control" MaxLength="50">
                    </dx:ASPxTextBox>--%>
                </div>
            </div>
            <div class="form-group has-success has-feedback">
                <label class="control-label col-sm-3" for="inputSuccess3">monLic</label>
                <div class="col-sm-6">
                    <dx:ASPxTextBox ID="txtmonLic" runat="server" CssClass="form-control" MaxLength="50">
                    </dx:ASPxTextBox>
                </div>
            </div>
            <div class="form-group has-success has-feedback">
                <label class="control-label col-sm-3" for="inputSuccess3">verSeaLic</label>
                <div class="col-sm-6">
                    <dx:ASPxTextBox ID="txtverSeaLic" runat="server" CssClass="form-control" MaxLength="50">
                    </dx:ASPxTextBox>
                </div>
            </div>
            <div class="form-group has-success has-feedback">
                <label class="control-label col-sm-3" for="inputSuccess3">codEnt</label>
                <div class="col-sm-3">
                    <dx:ASPxComboBox ID="dpdcodEnt" runat="server" ValueType="System.String" CssClass="form-control"></dx:ASPxComboBox>
                </div>
            </div>
            <div class="form-group has-success has-feedback">
                <label class="control-label col-sm-3" for="inputSuccess3">codCat</label>
                <div class="col-sm-3">
                    <dx:ASPxComboBox ID="dpdcodCat" runat="server" ValueType="System.String" CssClass="form-control"></dx:ASPxComboBox>
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

                    <dx:ASPxGridView ID="gdvLista" runat="server" AutoGenerateColumns="False" Width="80%"
                        OnRowCommand="gdvListaActividades_RowCommand" KeyFieldName="codLic"
                        OnBeforeColumnSortingGrouping="gdvListaActividades_BeforeColumnSortingGrouping"
                        OnBeforeGetCallbackResult="gdvListaActividades_BeforeGetCallbackResult"
                        OnBeforeHeaderFilterFillItems="gdvListaActividades_BeforeHeaderFilterFillItems"
                        OnPageIndexChanged="gdvListaActividades_PageIndexChanged"
                        OnCustomGroupDisplayText="gdvListaActividades_CustomGroupDisplayText">
                        <Columns>
                            <dx:GridViewCommandColumn ShowClearFilterButton="True" VisibleIndex="0" Width="1px">
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataTextColumn Caption="codLic" VisibleIndex="1" Width="30px" FieldName="codLic">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="nomLic" VisibleIndex="1" Width="30px" FieldName="nomLic">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="desLic" VisibleIndex="2" Width="150px" FieldName="desLic">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="norAplLic" VisibleIndex="2" Width="150px" FieldName="norAplLic">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="fecPubLic" VisibleIndex="2" Width="150px" FieldName="fecPubLic">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="monLic" VisibleIndex="2" Width="150px" FieldName="monLic">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="verSeaLic" VisibleIndex="2" Width="150px" FieldName="verSeaLic">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Estado" VisibleIndex="4" Width="70px" FieldName="estLic">
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

