<%@ Page Title="" Language="C#" MasterPageFile="~/Sources/Pages/MP.Master" AutoEventWireup="true" CodeBehind="CargaBusqueda.aspx.cs" Inherits="ASP_SP.Sources.Pages.CargaBusqueda" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Inicio
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
      
          body{
            background-color:#e6f7ff
        }
        .content {
            max-width: 500px;
            margin: 0 auto;
            padding: 20px;
            background-color: #808080ff;
            border: 1px solid #ddd;
            border-radius: 5px;
        }

        .content h2 {
            font-size: 24px;
            margin-bottom: 20px;
        }

        .form-group {
            margin-bottom: 20px;
        }

        .form-group label {
            display: block;
            font-weight: bold;
            margin-bottom: 5px;
        }

        .form-group input[type="text"],
        .form-group input[type="tel"],
        .form-group select {
            width: 100%;
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 5px;
        }

        .form-group textarea {
            width: 100%;
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 5px;
        }

        .radio-group label {
            display: inline-block;
            margin-right: 20px;
        }

        .btn-primary {
            background-color: #007bff;
            color: #fff;
            border: none;
            padding: 10px 20px;
            border-radius: 5px;
            cursor: pointer;
        }

        .btn-primary:hover {
            background-color: #0056b3;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <div class="content">
        <h2>Crea tu oferta</h2>
        <div class="form-group">
            <label for="txtNombreEmpresa">Nombre de la empresa</label>
            <asp:TextBox ID="txtNombreEmpresa" runat="server" placeholder="Nombre de la empresa" Required="true"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="ddlPuestoTrabajo">Puesto de Trabajo</label>
            <asp:DropDownList ID="ddlPuestoTrabajo" runat="server">
                <asp:ListItem Text="Programador" Value="Programador" />
                <asp:ListItem Text="Diseñador" Value="Diseñador" />
            </asp:DropDownList>
        </div>
        <div class="form-group">
            <label for="txtTelefono">Teléfono</label>
            <asp:TextBox ID="txtTelefono" runat="server" placeholder="Teléfono" Required="true" type="tel"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="txtHorasTrabajar">Horas a trabajar</label>
            <asp:TextBox ID="txtHorasTrabajar" runat="server" placeholder="Horas a trabajar" Required="true" type="number"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="txtDescripcionEmpresa">Descripción de la empresa</label>
            <asp:TextBox ID="txtDescripcionEmpresa" runat="server" TextMode="MultiLine" placeholder="Descripción de la empresa" Required="true"></asp:TextBox>
        </div>
        <div class="form-group radio-group">
            <label>Trabajo Remoto:</label>
            <asp:RadioButtonList ID="rblTrabajoRemoto" runat="server">
                <asp:ListItem Text="Sí" Value="1" />
                <asp:ListItem Text="No" Value="0" />
            </asp:RadioButtonList>
        </div>
        <div class="form-group">
            <label for="ddlRubroEmpresa">Rubro de la Empresa</label>
            <asp:DropDownList ID="ddlRubroEmpresa" runat="server">
                <asp:ListItem Text="Videojuegos" Value="Videojuegos" />
                <asp:ListItem Text="Cine" Value="Cine" />
            </asp:DropDownList>
        </div>
        <div class="form-group">
            <asp:Button ID="BtnGuardarOferta" runat="server" Text="Guardar oferta" OnClick="BtnGuardarOferta_Click" CssClass="btn-primary" />
        </div>
    </div>
    <asp:GridView ID="GridViewOfertas" runat="server" AutoGenerateColumns="true"></asp:GridView>
     <footer>
            <p>Derechos reservados a Federico Leonel Bouzón</p>
        </footer>
</asp:Content>
