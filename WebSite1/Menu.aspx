<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Menu.aspx.cs" Inherits="Menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

     <br />
    <br />
  
   
    <p class="inline-block">&nbsp;&nbsp;</p>
    <div class="col-md-4 center-block inline-block text-center modal-content">
        <br />
        <h4 class="modal-title bg-success"><strong>Menú mantenimientos</strong></h4>
        <br />
        <div class="col-md-6 center-block quitar-float inline-block bg-warning">
           <a href="Categoria.aspx"> <img class="menu-img img-responsive btn" src="img/Menu-Principal/maintenance-icon.png" alt="Menu1" /></a>
            <h5>Mant. Categoria</h5>
           <a href="Cliente.aspx">  <img class="menu-img img-responsive btn" src="img/Menu-Principal/Actions-view-process-system-icon.png" alt="Menu1" /></a>
            <h5>Mant. Cliente</h5>
        </div>
        <div class="col-md-6 center-block quitar-float inline-block bg-warning">
             <a href="EntidadConvocante.aspx"> <img class="menu-img img-responsive btn" src="img/Menu-Principal/SEO-icon.png" alt="Menu1" /></a>
            <h5>Mant. Entidad Convocante</h5>
            <a href="Licitacion.aspx">  <img class="menu-img img-responsive btn" src="img/Menu-Principal/Network-Statistics-icon.png" alt="Menu1" /></a>
            <h5>Mant. Licitación</h5>
        </div>
        <div class="col-md-6 center-block quitar-float inline-block bg-warning">
             <a href="Suscripcion.aspx"> <img class="menu-img img-responsive btn" src="img/Menu-Principal/Actividades.png" alt="Menu1" /></a>
            <h5>Mant. Suscripcion</h5>
            
        </div>
        <br />
        <br />
    </div>
</asp:Content>

