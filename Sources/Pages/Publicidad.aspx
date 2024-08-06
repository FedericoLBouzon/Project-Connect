<%@ Page Title="" Language="C#" MasterPageFile="~/Sources/Pages/MP.Master" AutoEventWireup="true" CodeBehind="Publicidad.aspx.cs" Inherits="ASP_SP.Sources.Pages.FrmIndex" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Adquiere anuncios de la empresa
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            background-color: #e6f7ff;
        }

        .plan-card {
            border: 1px solid #ccc;
            padding: 20px;
            margin: 10px;
            border-radius: 5px;
            width: 30%;
            display: inline-block;
            background-color: #f8f8f8;
            text-align: center;
            box-shadow: 0px 0px 10px 0px #aaa;
            color: #333;
            background-color:#7C7C7C;
        }
        .content{
            background-color:#e6f7ff;
        }

        .plan-card h3 {
            font-size: 24px;
 
        }

        .plan-card p {
            font-size: 18px;
            color: #555;
        }

        .footer {
            text-align: center;
            padding: 10px;
            background-color: #f5f5f5;
        }
        .comprar-btn{
            color:black;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <div class="content">
        <h2>Adquiere anuncios de la empresa</h2>
       <div class="plan-card">
            <h3>Plan Bronce</h3>
            <p>1 Anuncio X 1 semana</p>
            <p>5.000$</p>
            <a href="Facturacion.aspx?plan=Bronce" class="comprar-btn">Comprar</a>
        </div>
        <div class="plan-card">
            <h3>Plan Plata</h3>
            <p>1 Anuncio X 2 Semanas</p>
            <p>15.000$</p>
            <a href="Facturacion.aspx?plan=Plata" class="comprar-btn">Comprar</a>
        </div>
        <div class="plan-card">
            <h3>Plan Oro</h3>
            <p>1 Anuncio X 1 MES</p>
            <p>30.000$</p>
            <a href="Facturacion.aspx?plan=Oro" class="comprar-btn">Comprar</a>
        </div>

            <div class="desarrollada-por">
        <p class="app-developed-text">Asi se ve un anuncio</p>
        <div class="creador-info">
            <img src="/Sources/Images/PublicidadEjemplo.png" alt="Creador" class="ImgYo" />
            <p>Por el momento la APP esta en beta, asi que para ayudar a la misma si usted desea adquirir un anuncio solamente debe de disponer de una imagen de su empresa</p>
        </div>
    </div>
        <footer>
            <p>Derechos reservados a Federico Leonel Bouzón</p>
        </footer>
    </div>
    
</asp:Content>
