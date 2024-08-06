<%@ Page Title="" Language="C#" MasterPageFile="~/Sources/Pages/MP.Master" AutoEventWireup="true" CodeBehind="Comunidad.aspx.cs" Inherits="ASP_SP.Sources.Pages.Comunidad" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Comunidad
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">

      <style>
               body{
            background-color:#e6f7ff
        }
        .card {
            background-color:dodgerblue;
            border: 1px solid #ccc;
            border-radius: 5px;
            padding: 10px;
            margin: 10px;
            width: 25%;
            height:25%;
            display: inline-block; /* Esto permite que las tarjetas se muestren una al lado de la otra */
        }
        .Container{
            Background-color: #e6f7ff;
        }

        
   
    </style>
        <div class="Container">


    <h2>Estos son todos los usuarios de la APP</h2>
              <div class="publicidad-right">
    <asp:Image ID="imgPublicidad3" runat="server" AlternateText="Publicidad 3" />
        </div>
               <div class="publicidad-left">
    <asp:Image ID="ImgPublicidad4" runat="server" AlternateText="Publicidad 4" />
        </div>
    <div>
<asp:Repeater ID="RepeaterUsuarios" runat="server">
    <ItemTemplate>
        <div class="card">
            <div class="card-body">
                <asp:Image runat="server" ID="imgPerfil" Width="300" Height="200" CssClass="rounded-circle img-thumbnail"
                ImageUrl='<%# "/Sources/Pages/FrmImagen.aspx?id=" + Eval("IdUsuario") %>' AlternateText="Imagen de perfil" />

                <h5 class="card-title"><%# Eval("Nombres") %> <%# Eval("Apellidos") %></h5>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>
        



    </div>
        </div>
       <footer>
            <p>Derechos reservados a Federico Leonel Bouzón</p>
        </footer>
</asp:Content>
