using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
namespace ASP_SP.Sources.Pages
{
    public partial class FrmIndex : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (imgPublicidad != null)
            {
                if (Session["ImagenCargada"] != null)
                {
                    string urlImagen = Session["ImagenCargada"].ToString();
                    imgPublicidad.ImageUrl = urlImagen;
                }
                else
                {
                    // Consulta la imagen de la pestaña 1 en la base de datos y asígnala a la imagen
                    string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("SELECT Imagen FROM Anuncios WHERE Pestana = 1", conn))
                        {
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    byte[] imagenData = (byte[])reader["Imagen"];
                                    string base64String = Convert.ToBase64String(imagenData);
                                    string imageUrl = "data:image/jpeg;base64," + base64String;
                                    imgPublicidad.ImageUrl = imageUrl;
                                }
                            }
                        }
                    }

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("SELECT Imagen FROM Anuncios WHERE Pestana = 2", conn))
                        {
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    if (reader["Imagen"] != DBNull.Value)
                                    {
                                        byte[] imagenData = (byte[])reader["Imagen"];
                                        string base64String = Convert.ToBase64String(imagenData);
                                        string imageUrl = "data:image/jpeg;base64," + base64String;
                                        // Asignar la imagen a donde corresponda
                                        // Por ejemplo: imgPublicidad.ImageUrl = imageUrl;
                                    }
                                    else
                                    {
                                        // Manejo de caso en el que la imagen es nula
                                    }
                                }
                            }

                        }
                    }

                }

            }
        }
    }
}