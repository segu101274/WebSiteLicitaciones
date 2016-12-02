<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Mantenimientos</title>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/StyleIndexMenu.css" rel="stylesheet" />
</head>
<body>
     <form id="form1" runat="server">
        <div>
            <header>
                <div class="img-cabecera">
                    <img src="img/Menu-Principal/Cabecera-titulo.png" alt="Sistema de adquisición de datos" style="width: 100%;" />
                    <div class="img-Text modal-title ">
                        <h1><strong>Sistema de mantenimiento</strong></h1>
                    </div>
                </div>
            </header>
            <div class="row col-md-12 center-block" style="background-color: #F3F3F3; position: relative;">
                <div class="col-md-8 center-block quitar-float " style="background-color: white; position: relative;">
                    <div class="col-md-4"></div>
                    <div class="col-md-3 center-block quitar-float inline-block">
                        <div style="margin-top: 10%; margin-left: 10%; position: inherit; width: 100%; height: 100%; margin-bottom: 15%;">
                            <h4><span>Usuario</span></h4>
                            <div class="text-success">
                                <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <h4><span>Contraseña</span></h4>
                            <div>
                                <asp:TextBox ID="txtContraseña" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                            </div>
                            <div style="height: 10%">&nbsp;</div>
                            <div>
                                <asp:Button ID="btnIngresar" runat="server" Text="Aceptar" CssClass="btn btn-default btn-lg" OnClick="btnIngresar_Click" />
                            </div>
                        </div>
                    </div>
                    <br />
                </div>
            </div>
            <footer class="panel-footer center-block context-footer text-left">
                <address>
                    <strong>HappyBox</strong><br />
                    Av. Nicolás Ayllón 2890<br />
                    Ate Vitarte Lima 3 - Perú<br />
                    <a class="text-muted" style="color: white; font-style: oblique" href="mailto:#">svigoc@gmail.com</a>
                </address>

            </footer>

        </div>
    </form>
</body>
</html>
