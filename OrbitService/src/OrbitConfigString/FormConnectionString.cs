using ConnectionStringGenerator.Application.ConnectionString;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConnectionStringGenerator
{
    public partial class FormConnectionString : Form
    {
        private BindingSource bindingSource1 = new BindingSource();

        public FormConnectionString()
        {
            InitializeComponent();
            FormStyle();
            txtFile.Text = @"C:\\Windows\\System32";

            
        }

        public void FormStyle()
        {
            #region txtPassword
            txtPassword.Text = "";
            txtPassword.PasswordChar = '*';
            txtPassword.MaxLength = 14;
            #endregion
        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fdb = new FolderBrowserDialog())
            {
                if (fdb.ShowDialog() == DialogResult.OK)
                {
                    txtFile.Text = fdb.SelectedPath;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var fileSavePath = txtFile.Text;

            if (string.IsNullOrEmpty(fileSavePath))
            {
                MessageBox.Show("Favor preencher o local onde o arquivo será salvo.");
            }
            else
            {
                ConnectionString connectionString = new ConnectionString();
                string encryptedString = connectionString.EncryptConnectionString(cbDbType.SelectedIndex.ToString(), txtServer.Text, txtUser.Text, txtPassword.Text, dataGridView1);
                
                StreamWriter sw = new StreamWriter($@"{fileSavePath}\\connector.json", false, Encoding.ASCII);
                sw.Write(encryptedString);
                sw.Close();

                MessageBox.Show("Connection String gerada com sucesso !.");
            }
        }

        private void InitializeDataGridView()
        {
            try
            {
                // Set up the data source.
                bindingSource1.DataSource = GetDataTableFromAdapter(@"select ""dbName"" AS ""DATABASE"", ""cmpName"" as ""Nome"", 'N' as ""ATIVO"" from ""SBOCOMMON"".""SRGC""", txtServer.Text, txtUser.Text, txtPassword.Text);
                dataGridView1.DataSource = bindingSource1;

                // Automatically resize the visible rows.
                dataGridView1.AutoSizeRowsMode =
                    DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;

                // Set the DataGridView control's border.
                dataGridView1.BorderStyle = BorderStyle.Fixed3D;

                // Put the cells in edit mode when user enters them.
                dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
            }
            catch (OdbcException)
            {
                MessageBox.Show("To run this sample replace connection.ConnectionString" +
                    " with a valid connection string to a Northwind" +
                    " database accessible to your system.", "ERROR",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                System.Threading.Thread.CurrentThread.Abort();
            }
        }

        public DataTable GetDataTableFromAdapter(string queryString, string Server, string Login, string Senha)
        {
            string connectionHANA = @"driver={HDBODBC};UID="+Login+ ";PWD=" + Senha + ";serverNode=" + Server;
            DataTable dt = new DataTable();
            using (OdbcConnection connection =
                        new OdbcConnection(connectionHANA))
            {
                using (OdbcDataAdapter adapter =
                        new OdbcDataAdapter(queryString, connection))
                {
                    connection.Open();
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnTstCon_Click(object sender, EventArgs e)
        {
            this.Controls.Add(dataGridView1);
            InitializeDataGridView();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
