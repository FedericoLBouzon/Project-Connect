<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Buscando.aspx.cs" Inherits="ASP_SP.Sources.Pages.Buscando" MasterPageFile="~/Sources/Pages/MP.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    <!-- Título de la página -->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <!-- Agrega enlaces a Bootstrap CSS -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <style>
        body{
            background-color:#e6f7ff
        }
    .card{
        background-color:lightblue;

}

    </style>
    <div class="container">
       <div class="row">
    <div class="col-md-12 text-center mb-4">
        <asp:Button ID="btnCargarPropuesta" runat="server" Text="Cargar tu propuesta" OnClick="BtnCargarPropuesta_Click" CssClass="btn btn-primary" style="padding: 10px 20px;" />
    </div>
</div>

        </div>
      <div class="publicidad-right">
    <asp:Image ID="ImgPublicidad5" runat="server" AlternateText="Publicidad 5" />
        </div>
       <div class="publicidad-left">
    <asp:Image ID="ImgPublicidad6" runat="server" AlternateText="Publicidad 6" />
        </div>


        <div class="card-deck" style="width: 30%; margin-left: 35%;">
     <asp:GridView ID="GridViewOfertasBuscando" runat="server" AutoGenerateColumns="false" CssClass="grid-view">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <div class="card mb-4">
                    <div class="card-header"><%# Eval("Nombres") %> <%# Eval("Apellidos") %></div>
                    <div class="card-body">
                        <h5 class="card-title">Nombre de la Empresa: <%# Eval("NombreEmpresa") %></h5>
                        <p class="card-text">Puesto de Trabajo: <%# Eval("PuestoTrabajo") %></p>
                        <p class="card-text">Teléfono: <%# Eval("Telefono") %></p>
                        <p class="card-text">Horas a Trabajar: <%# Eval("HorasTrabajar") %></p>
                        <p class="card-text">Descripción de la Empresa: <%# Eval("DescripcionEmpresa") %></p>
                        <p class="card-text">Trabajo Remoto: <%# Eval("TrabajoRemoto") %></p>
                        <p class="card-text">Rubro de la Empresa: <%# Eval("RubroEmpresa") %></p>
                    </div>
                </div>
            </ItemTemplate>

        </asp:TemplateField>

    </Columns>

</asp:GridView>


        </div>

    <!-- Agrega referencias a Bootstrap JS y jQuery al final de la página -->
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>

    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.3/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
     <footer>
            <p>Derechos reservados a Federico Leonel Bouzón</p>
        </footer>
</asp:Content>
