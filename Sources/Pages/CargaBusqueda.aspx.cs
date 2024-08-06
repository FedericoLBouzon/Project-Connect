using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ASP_SP.Sources.Pages
{
    public partial class CargaBusqueda : System.Web.UI.Page
    {
        public static int id; // Este será el ID del usuario logueado.

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuariologueado"] == null)
            {
                Response.Redirect("/Sources/Pages/FrmLogin.aspx");
            }
            else
            {
                // Obtener el ID del usuario logueado
                id = int.Parse(Session["usuariologueado"].ToString());

                if (!IsPostBack)
                {
                    CargarDatos();
                }

            }
        }

        private void CargarDatos()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string query = "SELECT OT.NombreEmpresa, OT.PuestoTrabajo, OT.Telefono, OT.HorasTrabajar, OT.DescripcionEmpresa, OT.TrabajoRemoto, OT.RubroEmpresa, U.Nombres, U.Apellidos, I.Imagen " +
                    "FROM OfertasTrabajo OT " +
                    "INNER JOIN Usuarios U ON OT.Id = U.Id " +
                    "LEFT JOIN Imagenes I ON U.Id = I.IdUsuario";

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


    protected void BtnGuardarOferta_Click(object sender, EventArgs e)
        {
            string nombreEmpresa = txtNombreEmpresa.Text;
            string puestoTrabajo = ddlPuestoTrabajo.SelectedValue;
            string telefono = txtTelefono.Text;
            string horasTrabajar = txtHorasTrabajar.Text;
            string descripcionEmpresa = txtDescripcionEmpresa.Text;
            bool trabajoRemoto = rblTrabajoRemoto.SelectedValue == "1";
            string rubroEmpresa = ddlRubroEmpresa.SelectedValue;

            string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string query = "INSERT INTO OfertasTrabajo (NombreEmpresa, PuestoTrabajo, Telefono, HorasTrabajar, DescripcionEmpresa, TrabajoRemoto, RubroEmpresa, Id) " +
                               "VALUES (@NombreEmpresa, @PuestoTrabajo, @Telefono, @HorasTrabajar, @DescripcionEmpresa, @TrabajoRemoto, @RubroEmpresa, @id)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@NombreEmpresa", nombreEmpresa);
                    cmd.Parameters.AddWithValue("@PuestoTrabajo", puestoTrabajo);
                    cmd.Parameters.AddWithValue("@Telefono", telefono);
                    cmd.Parameters.AddWithValue("@HorasTrabajar", horasTrabajar);
                    cmd.Parameters.AddWithValue("@DescripcionEmpresa", descripcionEmpresa);
                    cmd.Parameters.AddWithValue("@TrabajoRemoto", trabajoRemoto);
                    cmd.Parameters.AddWithValue("@RubroEmpresa", rubroEmpresa);
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
                Response.Redirect("Buscando.aspx");


            }

            LimpiarCampos();
            CargarDatos();
        }

        private void LimpiarCampos()
        {
            txtNombreEmpresa.Text = "";
            ddlPuestoTrabajo.SelectedIndex = 0;
            txtTelefono.Text = "";
            txtHorasTrabajar.Text = "";
            txtDescripcionEmpresa.Text = "";
            rblTrabajoRemoto.SelectedIndex = 0;
            ddlRubroEmpresa.SelectedIndex = 0;
        }
    }
}
