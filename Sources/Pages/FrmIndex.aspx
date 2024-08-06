<%@ Page Title="" Language="C#" MasterPageFile="~/Sources/Pages/MP.Master" AutoEventWireup="true" CodeBehind="FrmIndex.aspx.cs" Inherits="ASP_SP.Sources.Pages.FrmIndex" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Inicio
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">

    <style>
           body{
            background-color:#e6f7ff
        }
        .nuevo-en-la-app-container {
            flex: 1;
            display: flex;
            justify-content: center;
            align-items: center;
            padding: 20px;
        }

        .nuevo-en-la-app {
            text-align: center;
        }
    </style>
    <div class="content">
        <div class="publicidad-section">
<div class="publicidad-left">
    <asp:Image ID="imgPublicidad" runat="server" AlternateText="Publicidad 1" />
</div>      
        <img src="/Sources/Images/FrmIndexImg.JPG" alt="Alternate Text" />
             <div class="nuevo-en-la-app-container"> <!-- Contenedor para centrar la sección -->
            <div class="nuevo-en-la-app">
                <h2>Nuevo en la APP??</h2>
                <p>Quedate trankilo que es una interfaz super sencilla con modalidades simples que se adaptan a tus necesadides</p>
                
            </div>
        </div>
        <div class="publicidad-right">
                <asp:Image ID="imgPublicidad2" runat="server" AlternateText="Publicidad 2" />
        </div>
    
    </div>
    
    <!-- Sección 2: Desarrollada por -->
    <div class="desarrollada-por">
        <p class="app-developed-text">Sabias Que?.. La app fue desarrollada por</p>
        <div class="creador-info">
            <img src="/Sources/Images/User.jpg" alt="Creador" class="ImgYo" />
            <p>Federico Leonel, un estudiante de informática de 5to año de Secundaria</p>
        </div>
    </div>
    


         
        <footer>
            <p>Derechos reservados a Federico Leonel Bouzón</p>
        </footer>
    </div>
</asp:Content>

