using System;
using System.Web.UI;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Xml.Linq;

namespace ASP_SP.Sources.Pages
{
    public partial class Facturacion : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarPestanasDisponibles();

            }
        }

        private void CargarPestanasDisponibles()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT Id, NombrePestana FROM AnunciosDisponibles", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        ddlPestanaAnuncio.DataSource = reader;
                        ddlPestanaAnuncio.DataTextField = "NombrePestana";
                        ddlPestanaAnuncio.DataValueField = "Id"; // Utiliza el ID para el DataValueField
                        ddlPestanaAnuncio.DataBind();
                    }
                }
            }
        }





        protected string GetImageUrl(object pestana)
        {
            string pestanaId = pestana.ToString();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT Imagen FROM Anuncios WHERE Pestana = @Pestana", conn))
                {
                    cmd.Parameters.AddWithValue("@Pestana", pestanaId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            byte[] imagenData = (byte[])reader["Imagen"];
                            string base64String = Convert.ToBase64String(imagenData);
                            string imageUrl = "data:image/jpeg;base64," + base64String;

                            return imageUrl;
                        }
                    }
                }
            }

            return "~/ruta/de/imagen/predeterminada.jpg";
        }

        protected void BtnComprarAnuncio_Click(object sender, EventArgs e)
        {
            int idAnuncioDisponible = int.Parse(ddlPestanaAnuncio.SelectedValue);
            string plann = ddlPlanAnuncio.SelectedValue;
            decimal precio = CalcularPrecio(plann);

            int idUsuario = int.Parse(Session["usuariologueado"].ToString());

         
            if (fileUploadImagen.HasFile)
            {
                byte[] imagen = fileUploadImagen.FileBytes;
                string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Anuncios (IdUsuario, Plann, Precio, Imagen, FechaCompra, Disponible, Pestana) VALUES (@IdUsuario, @Plann, @Precio, @Imagen, @FechaCompra, 0, @Pestana)", conn))
                    {
                        cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                        cmd.Parameters.AddWithValue("@Plann", plann);
                        cmd.Parameters.AddWithValue("@Precio", precio);
                        cmd.Parameters.AddWithValue("@Pestana", idAnuncioDisponible);
                        cmd.Parameters.Add("@Imagen", SqlDbType.Image).Value = imagen;
                        cmd.Parameters.AddWithValue("@FechaCompra", DateTime.Now);
                        cmd.ExecuteNonQuery();
                    }
                    // Elimina el registro de "AnunciosDisponibles" basado en el número de pestaña
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM AnunciosDisponibles WHERE Id = @Pestana", conn))
                    {
                        cmd.Parameters.AddWithValue("@Pestana", idAnuncioDisponible); // Utiliza el número de pestaña seleccionado
                        cmd.ExecuteNonQuery();
                    }
                    CargarPestanasDisponibles();




                    string fechaNacimiento = txtFechaNacimiento.Text;
                    string telefono = txtTelefono.Text;
                    string cuit = txtCuit.Text;

                    string cae = GenerarNumeroCAE();

                    // Ahora se insertan los datos en la tabla Facturas
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Facturas (CAE, Nombre, Apellidos, FechaNacimiento, Telefono, Cuit, CorreoElectronico, IdProducto, NombrePlan, NumeroDeExistentes, CategoriaRubro) VALUES (@CAE, @Nombre, @Apellidos, @FechaNacimiento, @Telefono, @Cuit,  @CorreoElectronico, @IdProducto, @NombrePlan, 6, 'Servicio Web')", conn))
                    {
                        cmd.Parameters.AddWithValue("@CAE", cae);
                        cmd.Parameters.AddWithValue("@Nombre", ObtenerNombreUsuario(idUsuario));
                        cmd.Parameters.AddWithValue("@Apellidos", ObtenerApellidosUsuario(idUsuario, conn));
                        cmd.Parameters.AddWithValue("@FechaNacimiento", fechaNacimiento);
                        cmd.Parameters.AddWithValue("@Telefono", telefono);
                        cmd.Parameters.AddWithValue("@Cuit", cuit);
                        cmd.Parameters.AddWithValue("@CorreoElectronico", ObtenerCorreoUsuario(idUsuario, conn));
                        cmd.Parameters.AddWithValue("@IdProducto", idAnuncioDisponible);
                        cmd.Parameters.AddWithValue("@NombrePlan", plann);
                        cmd.ExecuteNonQuery();
                    }
                    GenerarFacturaPDF();

                }
            }
            else
            {
                lblError.Text = "Por favor, complete todos los campos y cargue una imagen.";
            }
        }

        private void GenerarFacturaPDF()
        {
            int idUsuario = int.Parse(Session["usuariologueado"].ToString());

            // Obtén el precio del plan y calcula el IVA
            string plann = ddlPlanAnuncio.SelectedValue;
            decimal precioPlan = CalcularPrecio(plann);
            decimal iva = precioPlan * 0.21m;

            // Obtén la fecha actual
            DateTime fechaActual = DateTime.Now;

            // Calcula la fecha de vencimiento (fecha actual + 10 días)
            DateTime fechaVencimiento = fechaActual.AddDays(10);

            Document doc = new Document();
            string nombreArchivo = $"~/Factura_{idUsuario}.pdf";
            string rutaFisica = Server.MapPath(nombreArchivo);

            PdfWriter.GetInstance(doc, new FileStream(rutaFisica, FileMode.Create));
            doc.Open();

            // Configurar el estilo de fuente para el cliente y el tipo de factura
            Font fontCliente = FontFactory.GetFont(FontFactory.HELVETICA, 16);
            Font fontClienteTitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
            Font fontTipoFactura = FontFactory.GetFont(FontFactory.HELVETICA, 12);

            // Agregar el cliente con fuente grande
            string nombreCliente = "Nombre del Cliente: " + ObtenerNombreUsuario(idUsuario);
            Paragraph clienteTituloParagraph = new Paragraph(nombreCliente, fontClienteTitulo);
            Paragraph clienteParagraph = new Paragraph(ObtenerNombreUsuario(idUsuario), fontCliente);

            // Agregar el CAE al lado del nombre del cliente
            string cae = "CAE: " + GenerarNumeroCAE();
            Paragraph caeParagraph = new Paragraph(cae, fontCliente);

            doc.Add(clienteTituloParagraph);
            doc.Add(clienteParagraph);

            // Agregar el CAE al lado del nombre del cliente
            doc.Add(caeParagraph);

            // Agregar la leyenda con el tipo de factura
            Paragraph tipoFacturaParagraph = new Paragraph("Tipo de Factura: A", fontTipoFactura);
            doc.Add(tipoFacturaParagraph);

            // Agregar espacio entre el cliente y la fecha de generación
            doc.Add(new Paragraph(" "));

            // Agregar la fecha de generación
            doc.Add(new Paragraph("Fecha de Generación: " + fechaActual.ToString("dd/MM/yyyy")));

            // Añadir espacio entre la fecha de generación y la tabla
            doc.Add(new Paragraph(" "));

            // Crear una tabla para los datos de la factura
            PdfPTable table = new PdfPTable(3);
            table.WidthPercentage = 100; // Ocupa el ancho completo de la página
            table.DefaultCell.Border = Rectangle.BOX;
            table.DefaultCell.BorderColor = BaseColor.BLACK;
            table.DefaultCell.Padding = 5;

            // Agregar las celdas con los datos
            table.AddCell("Nombre del Plan");
            table.AddCell("Cantidad");
            table.AddCell("Precio");
            table.AddCell(plann);
            table.AddCell("1");
            table.AddCell(precioPlan.ToString("C")); // Formatea como moneda

            // Añadir la tabla al documento PDF
            doc.Add(table);

            // Agregar espacio después de la tabla
            doc.Add(new Paragraph(" "));

            // Crear una tabla para el Subtotal, IVA y Total con un recuadro
            PdfPTable totalTable = new PdfPTable(2);
            totalTable.WidthPercentage = 100;
            totalTable.DefaultCell.Border = Rectangle.BOX;
            totalTable.DefaultCell.BorderColor = BaseColor.BLACK;
            totalTable.DefaultCell.Padding = 5;

            // Agregar las celdas con los datos
            totalTable.AddCell("Subtotal:");
            totalTable.AddCell(precioPlan.ToString("C"));
            totalTable.AddCell("IVA (21%):");
            totalTable.AddCell(iva.ToString("C"));
            totalTable.AddCell("Total:");
            totalTable.AddCell((precioPlan + iva).ToString("C"));

            // Añadir la tabla de Subtotal, IVA y Total al documento PDF
            doc.Add(totalTable);

            // Agregar espacio entre la tabla y la Fecha de Vencimiento
            doc.Add(new Paragraph(" "));

            // Agregar la Fecha de Vencimiento del CAE
            doc.Add(new Paragraph("Fecha de Vencimiento del CAE: " + fechaVencimiento.ToString("dd/MM/yyyy")));

            doc.Close();

            // Realizar la descarga del archivo
            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + nombreArchivo);
            Response.TransmitFile(rutaFisica);
            Response.End();
        }


        private decimal CalcularPrecio(string plan)
        {
            switch (plan)
            {
                case "Bronce":
                    return 5000;
                case "Plata":
                    return 15000;
                case "Oro":
                    return 30000;
                default:
                    return 0;
            }
        }

        private string GenerarNumeroCAE()
        {
            // Implementa la lógica para generar un número CAE aleatorio
            // Puedes utilizar algún algoritmo o método que genere un número único
            // y aleatorio, dependiendo de tus requerimientos.
            Random random = new Random();
            int cae = random.Next(100000, 999999);
            return cae.ToString();
        }

        private string ObtenerNombreUsuario(int idUsuario)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT Nombres FROM Usuarios WHERE Id = @Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", idUsuario); // Utiliza el ID del usuario logeado
                    return cmd.ExecuteScalar()?.ToString();
                }
            }
        }


        private string ObtenerApellidosUsuario(int idUsuario, SqlConnection connection)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT Apellidos FROM Usuarios WHERE Id = @Id", connection)) // Corregido a "Id"
            {
                cmd.Parameters.AddWithValue("@Id", idUsuario); // Utiliza el ID del usuario logeado
                return cmd.ExecuteScalar()?.ToString();
            }
        }

        private string ObtenerCorreoUsuario(int idUsuario, SqlConnection connection)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT Correo FROM Usuarios WHERE Id = @Id", connection)) // Corregido a "Id"
            {
                cmd.Parameters.AddWithValue("@Id", idUsuario); // Utiliza el ID del usuario logeado
                return cmd.ExecuteScalar()?.ToString();
            }

        }

    }
}