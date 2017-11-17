using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Globalization;

namespace PruebaFOrm
{
    public partial class MasterEmpleado : Form
    {

        private string strConnectionString = DB.ConfiguracionBd.Default.CadenaConexion;
        private SqlCommand _sqlCommand;
        private SqlDataAdapter _sqlDataAdapter;
        DataSet _dtSet;

        public MasterEmpleado()
        {
            InitializeComponent();
        }


        public void CreateConnection()
        {
            SqlConnection _sqlConnection = new SqlConnection(strConnectionString);
            _sqlCommand = new SqlCommand();
            _sqlCommand.Connection = _sqlConnection;
        }
        public void OpenConnection()
        {
            _sqlCommand.Connection.Open();
        }
        public void CloseConnection()
        {
            _sqlCommand.Connection.Close();
        }
        public void DisposeConnection()
        {
            _sqlCommand.Connection.Dispose();
        }



        private void MasterEmpleado_Load(object sender, EventArgs e)
        {
            LlenarComboCargoEMpleado();
            LLenarGridEmpleado();
        }

        private void LlenarComboCargoEMpleado()
        {

            string Conexion = @"Data Source=FISICA\MSSQLSERVER2014;Initial Catalog=FormCrud;Integrated Security=True";
            SqlConnection dataConnection = new SqlConnection(Conexion);
            dataConnection.Open();
            SqlCommand cmd = new SqlCommand("Sp_GridCrud", dataConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Event", SqlDbType.VarChar, 10).Value = "Combobox";


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataConnection.Close();
            cboCargo.ValueMember = "IdCargo";
            cboCargo.DisplayMember = "Cargo";

            cboCargo.DataSource = dt;
        }

        /*Probar para Asignar*/
        /*
         int index = farmRegion.FindString(myText);
farmRegion.SelectedIndex = index;

 */
        private void LLenarGridEmpleado()
        {

            string Conexion = @"Data Source=FISICA\MSSQLSERVER2014;Initial Catalog=FormCrud;Integrated Security=True";
            SqlConnection dataConnection = new SqlConnection(Conexion);
            try
            {


                dataConnection.Open();
                SqlCommand cmd = new SqlCommand("Sp_GridCrud", dataConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Event", SqlDbType.VarChar, 10).Value = "Select";


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataConnection.Close();
                gridEmpleado.DataSource = dt;

                /*

                SqlDataReader reader;



                CreateConnection();
                OpenConnection();
                _sqlCommand.CommandText = "Sp_GridCrud";
                _sqlCommand.CommandType = CommandType.StoredProcedure;
                _sqlCommand.Parameters.AddWithValue("@Event", "Select");
                reader = _sqlCommand.ExecuteReader();
                _sqlDataAdapter = new SqlDataAdapter(_sqlCommand);
                _dtSet = new DataSet();
                _sqlDataAdapter.Fill(_dtSet);
                gridEmpleado.DataSource = _dtSet;*/
                //    gridEmpleado.DataBind();
            }
            catch (Exception ex)
            {
                MessageBox.Show("The Error is " + ex);
            }
            finally
            {
                dataConnection.Close();
                dataConnection.Dispose();
            }
        }

        private void Guardar()
        {

        }

        private void Cancelar()
        {

        }

        private void NuevoEmpleado()
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            string Conexion = @"Data Source=FISICA\MSSQLSERVER2014;Initial Catalog=FormCrud;Integrated Security=True";
            SqlConnection dataConnection = new SqlConnection(Conexion);

            int idcargo = cboCargo.SelectedIndex;





            dataConnection.Open();
            SqlCommand cmd = new SqlCommand("Sp_GridCrud", dataConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Event", "Add");
            cmd.Parameters.AddWithValue("@FirstName", Convert.ToString(txtNombreE.Text.Trim()));
            cmd.Parameters.AddWithValue("@LastName", Convert.ToString(txtApellidoE.Text.Trim()));
            cmd.Parameters.AddWithValue("@PhoneNumber", Convert.ToString(txtTelefono.Text.Trim()));
            cmd.Parameters.AddWithValue("@IdCargo", Convert.ToInt32(idcargo));
            int result = Convert.ToInt32(cmd.ExecuteNonQuery());
            if (result > 0)
            {
                MessageBox.Show("Se ha Guardado Correctamente");
            }
            else
            {

                MessageBox.Show("Ha Ocurrido un Error");
            }

            dataConnection.Close();
        }

        private void gridEmpleado_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtBuscarEmp_KeyPress(object sender, KeyPressEventArgs e)
        {



        }

        private void txtBuscarEmp_TextChanged(object sender, EventArgs e)
        {
            (gridEmpleado.DataSource as DataTable).DefaultView.RowFilter = string.Format("ApellidoE LIKE '%{0}%'", txtBuscarEmp.Text);
        }

        private void gridEmpleado_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void gridEmpleado_DoubleClick(object sender, EventArgs e)
        {



     
            /*



                string Cargo = "";
            if (gridEmpleado.SelectedRows.Count < 1)
            {
                MessageBox.Show("Please select a client");
                return;
            }

            else
            {
                int ro = gridEmpleado.SelectedIndex;
                txtIdEmpleado.Text = gridEmpleado.SelectedRows[0].Cells["IdEmpleado"].Value.ToString();
                txtNombreE.Text = gridEmpleado.SelectedRows[0].Cells["NombreE"].Value.ToString();
                txtApellidoE.Text = gridEmpleado.SelectedRows[0].Cells["ApellidoE"].Value.ToString();
                txtTelefono.Text = gridEmpleado.SelectedRows[0].Cells["Telefono"].Value.ToString();
                Cargo = gridEmpleado.SelectedRows[0].Cells["Cargo"].Value.ToString();

                int index = cboCargo.FindString(Cargo);
                cboCargo.SelectedIndex = index;
            }*/
        }

        private void gridEmpleado_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            string Cargo = "";
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.gridEmpleado.Rows[e.RowIndex];
                //Eid_txt.Text = row.Cells["Employee ID"].Value.ToString();
                txtIdEmpleado.Text = row.Cells["IdEmpleado"].Value.ToString();
                txtNombreE.Text = row.Cells["NombreE"].Value.ToString();
                txtApellidoE.Text = row.Cells["ApellidoE"].Value.ToString();
                txtTelefono.Text = row.Cells["Telefono"].Value.ToString();
                Cargo = row.Cells["Cargo"].Value.ToString();

                int index = cboCargo.FindString(Cargo);
                cboCargo.SelectedIndex = index;

            }
        }

        private void Update ()
        {
            string UpdateCommand = "sptimeupdate";
            using (SqlConnection sqlConnectionCmdString = new SqlConnection(......))
            using (SqlCommand sqlRenameCommand = new SqlCommand(UpdateCommand, sqlConnectionCmdString))
            {
                DateTime td = Convert.ToDateTime(toolDate.Text);
                sqlRenameCommand.CommandType = CommandType.StoredProcedure;
                sqlRenameCommand.Parameters.Add("@id", SqlDbType.NVarChar).Value = Clientid[up].ToString();
                sqlRenameCommand.Parameters.Add("@date", SqlDbType.DateTime).Value = td;
                sqlConnectionCmdString.Open();
                sqlRenameCommand.ExecuteNonQuery();
            }
        }



    }
}
