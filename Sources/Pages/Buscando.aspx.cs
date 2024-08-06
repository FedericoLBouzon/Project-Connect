using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI;

namespace ASP_SP.Sources.Pages
{
    public partial class Buscando : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarDatos();
            if (!IsPostBack)
            {
                // Verifica si existe la variable de sesión con los datos de las ofertas de trabajo
                if (Session["OfertasDeTrabajo"] != null)
                {
                    // Obtén los datos de la variable de sesión y llénalos en el GridView
                    var ofertasDataTable = (System.Data.DataTable)Session["OfertasDeTrabajo"];
                    GridViewOfertasBuscando.DataSource = ofertasDataTable;
                    GridViewOfertasBuscando.DataBind();
                }
                if (ImgPublicidad5 != null)
                {
                    if (Session["ImagenCargada"] != null)
                    {
                        string urlImagen = Session["ImagenCargada"].ToString();
                        ImgPublicidad5.ImageUrl = urlImagen;
                    }
                    else
                    {
                        // Consulta la imagen de la pestaña 1 en la base de datos y asígnala a la imagen
                        string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            conn.Open();
                            using (SqlCommand cmd = new SqlCommand("SELECT Imagen FROM Anuncios WHERE Pestana = 5", conn))
                            {
                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        byte[] imagenData = (byte[])reader["Imagen"];
                                        string base64String = Convert.ToBase64String(imagenData);
                                        string imageUrl = "data:image/jpeg;base64," + base64String;
                                        ImgPublicidad5.ImageUrl = imageUrl;
                                    }
                                }
                            }
                        }

                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            conn.Open();
                            using (SqlCommand cmd = new SqlCommand("SELECT Imagen FROM Anuncios WHERE Pestana = 6", conn))
                            {
                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        byte[] imagenData = (byte[])reader["Imagen"];
                                        string base64String = Convert.ToBase64String(imagenData);
                                        string imageUrl = "data:image/jpeg;base64," + base64String;
                                        ImgPublicidad6.ImageUrl = imageUrl;
                                    }
                                }
                            }
                        }

                    }

                }

            }
        }

        private void CargarDatos()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT OT.NombreEmpresa, OT.PuestoTrabajo, OT.Telefono, OT.HorasTrabajar, OT.DescripcionEmpresa, OT.TrabajoRemoto, OT.RubroEmpresa, U.Nombres, U.Apellidos " +
                    "FROM OfertasTrabajo OT " +
                    "INNER JOIN Usuarios U ON OT.Id = U.Id " +
                    "ORDER BY OT.IdOferta DESC";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Guarda los datos en una variable de sesión
                        Session["OfertasDeTrabajo"] = dataTable;
                    }
                }
            }
        }

        protected void BtnCargarPropuesta_Click(object sender, EventArgs e)
        {
            // Aquí puedes agregar la lógica que se debe ejecutar cuando se hace clic en el botón.
            // Por ejemplo, puedes redirigir a la página de carga de propuestas.
            Response.Redirect("CargaBusqueda.aspx");
        }
    }

}
