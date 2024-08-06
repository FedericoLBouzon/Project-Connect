using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ASP_SP.Sources.Pages
{
    public partial class Comunidad : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (imgPublicidad3 != null)
            {
                if (Session["ImagenCargada"] != null)
                {
                    string urlImagen = Session["ImagenCargada"].ToString();
                    imgPublicidad3.ImageUrl = urlImagen;
                }
                else
                {
                    // Consulta la imagen de la pestaña 1 en la base de datos y asígnala a la imagen
                    string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("SELECT Imagen FROM Anuncios WHERE Pestana = 3", conn))
                        {
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    byte[] imagenData = (byte[])reader["Imagen"];
                                    string base64String = Convert.ToBase64String(imagenData);
                                    string imageUrl = "data:image/jpeg;base64," + base64String;
                                    imgPublicidad3.ImageUrl = imageUrl;
                                }
                            }
                        }
                    }

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("SELECT Imagen FROM Anuncios WHERE Pestana = 4", conn))
                        {
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    byte[] imagenData = (byte[])reader["Imagen"];
                                    string base64String = Convert.ToBase64String(imagenData);
                                    string imageUrl = "data:image/jpeg;base64," + base64String;
                                    ImgPublicidad4.ImageUrl = imageUrl;
                                }
                            }
                        }
                    }

                }

            }

            if (!IsPostBack)
            {
                CargarUsuarios();

            }
        }

        private void CargarUsuarios()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT U.Nombres, U.Apellidos, U.Id AS IdUsuario " +
                               "FROM Usuarios U";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        RepeaterUsuarios.DataSource = dataTable;
                        RepeaterUsuarios.DataBind();
                    }
                }
            }
        }

    }
}
