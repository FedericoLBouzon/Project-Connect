using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ASP_SP.Sources.Pages
{
    public partial class FrmImagen : System.Web.UI.Page
    {
        readonly SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuariologueado"] == null)
            {
                Response.Redirect("/Sources/Pages/FrmLogin.aspx");
            }
            else
            {
                using (con)
                {
                    using (SqlCommand cmd = new SqlCommand("CargarImagen", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Request.QueryString["id"];
                        con.Open();

                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            byte[] imagen = dr["Imagen"] as byte[];

                            if (imagen != null && imagen.Length > 0)
                            {
                                // Si se encuentra una imagen, la enviamos en la respuesta
                                Response.BinaryWrite(imagen);
                            }
                            else
                            {
                                // Si no se encuentra una imagen, puedes mostrar una imagen de "imagen no encontrada" o cualquier otro contenido
                                MostrarImagenNoEncontrada();
                            }
                        }
                        else
                        {
                            // Si no se encuentra una imagen, puedes mostrar una imagen de "imagen no encontrada" o cualquier otro contenido
                            MostrarImagenNoEncontrada();
                        }
                    }
                }
            }
        }

        private void MostrarImagenNoEncontrada()
        {
            // Aquí puedes mostrar una imagen de "imagen no encontrada" o cualquier otro contenido
            // Por ejemplo, puedes cargar una imagen predeterminada en lugar de dejar la página en blanco
        }
    }
}
