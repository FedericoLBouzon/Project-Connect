<%@ Page Title="Facturación" Language="C#" MasterPageFile="~/Sources/Pages/MP.Master" AutoEventWireup="true" CodeBehind="Facturacion.aspx.cs" Inherits="ASP_SP.Sources.Pages.Facturacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Facturación
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
      .imgPestana {
    max-height: 200px; /* Cambia el valor según el tamaño deseado */
    max-width: 200px; /* Cambia el valor según el tamaño deseado */
}
         body{
            background-color:#e6f7ff
        }
          .content {
            max-width: 600px;
            margin: 0 auto;
            padding: 20px;
            background-color: #fff;
            border: 1px solid #ddd;
            border-radius: 5px;
        }

        h2 {
            color: #333;
        }

        .form-group {
            margin-bottom: 20px;
        }

        label {
            display: block;
            font-weight: bold;
        }

        select, input[type="text"], input[type="file"] {
            width: 100%;
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 5px;
        }

        .btn-primary {
            background-color: #007bff;
            color: #fff;
            padding: 10px 20px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
        }

        .btn-primary:hover {
            background-color: #0056b3;
        }

        .btn-secondary {
            background-color: #ccc;
            color: #333;
            padding: 10px 20px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
        }

        .btn-secondary:hover {
            background-color: #b0b0b0;
        }

        .imgPestana {
            max-height: 200px;
            max-width: 200px;
        }

        .error-message {
            color: red;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <div class="content">
        <h2>Facturación</h2>
        <div class="form-group">
            <label for="ddlPlanAnuncio">Selecciona un plan:</label>
            <asp:DropDownList ID="ddlPlanAnuncio" runat="server">
                <asp:ListItem Text="Bronce - $5,000" Value="Bronce"></asp:ListItem>
                <asp:ListItem Text="Plata - $15,000" Value="Plata"></asp:ListItem>
                <asp:ListItem Text="Oro - $30,000" Value="Oro"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <!-- Otras opciones de facturación aquí -->
       <div class="form-group">
            <label for="ddlPestanaAnuncio">Selecciona la pestaña:</label>
            <asp:DropDownList ID="ddlPestanaAnuncio" runat="server">
                <asp:ListItem Text="1-Inicio Derecha(Pagina principal)" Value="1"></asp:ListItem>
                <asp:ListItem Text="2-Inicio Izquierda(Pagina principal)" Value="2"></asp:ListItem>
                <asp:ListItem Text="3-Comunidad Derecha" Value="3"></asp:ListItem>
                <asp:ListItem Text="4-Comunidad Izquierda" Value="4"></asp:ListItem>
                <asp:ListItem Text="5-Buscar Derecha" Value="5"></asp:ListItem>
                <asp:ListItem Text="6-Buscar Izquierda" Value="6"></asp:ListItem>
            </asp:DropDownList>
        </div>

    <div class="form-group">
<label for="imagenAnuncio">Selecciona una Imagen:</label>
    <asp:FileUpload ID="fileUploadImagen" runat="server" />
        <asp:Label runat="server" ID="lblError" ForeColor="Red"></asp:Label>

</div>
        <div class="form-group">
<label for="txtFechaNacimiento">Fecha de Nacimiento:</label>
    <asp:TextBox ID="txtFechaNacimiento" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
</div>

<div class="form-group">
<label for="txtTelefono">Teléfono:</label>
    <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control"></asp:TextBox>
</div>

<div class="form-group">
<label for="txtCuit">CUIT:</label>
    <asp:TextBox ID="txtCuit" runat="server" CssClass="form-control"></asp:TextBox>
</div>


        <div class="modal-footer">
            <asp:Button ID="btnComprarAnuncio" runat="server" Text="Comprar" OnClick="BtnComprarAnuncio_Click" CssClass="btn btn-primary" />
            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>

        </div>
    </div>
</asp:Content>
