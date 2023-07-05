using System;
using System.Windows.Forms;
using Scire.JTV.SynchronizeDB.Properties;

namespace Scire.JTV.SynchronizeDB
{
    public partial class frmConfiguracoes : Form
    {
        public string MyConnection { get; set; }
        public string FireConnection { get; set; }
        public int CodigoCliente { get; set; }
        public int TempoMinutos { get; set; }

        public frmConfiguracoes()
        {
            InitializeComponent();
        }

        private void frmConfiguracoes_Load(object sender, EventArgs e)
        {
            CArregardados();
        }

        private void CArregardados() { 
            var dados = MyConnection.Split(';');

            myServer.Text = dados[0].Split('=')[1].ToString();
            myPorta.Text = dados[1].Split('=')[1].ToString();
            myDb.Text = dados[2].Split('=')[1].ToString();
            myUser.Text = dados[3].Split('=')[1].ToString();
            myPassword.Text = dados[4].Split('=')[1].ToString();

            // User=SYSDBA;Password=masterkey;Database=E:\Projetos\Workana\DadosMC\DadosMC.fdb;DataSource=localhost;Port=3050

            dados = FireConnection.Split(';');

            fireUSer.Text = dados[0].Split('=')[1].ToString();
            firePassword.Text = dados[1].Split('=')[1].ToString();
            fireDb.Text = dados[2].Split('=')[1].ToString();
            fireServ.Text = dados[3].Split('=')[1].ToString();
            firePort.Text = dados[4].Split('=')[1].ToString();

            txtCodigoCliente.Text = CodigoCliente.ToString();
            txtTempo.Text = TempoMinutos.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var fireconn = string.Format("User={0};Password={1};Database={2};DataSource={3};Port={4}"
                    , fireUSer.Text, firePassword.Text, fireDb.Text, fireServ.Text, firePort.Text);

                var myconn = string.Format("server={0};port={1};database={2};user id={3};password={4}"
                    , myServer.Text, myPorta.Text, myDb.Text, myUser.Text, myPassword.Text);

                Settings.Default.ConnectionSource = fireconn;
                Settings.Default.ConnectionTarget = myconn;

                Settings.Default.CodigoCliente = int.Parse(txtCodigoCliente.Text);
                Settings.Default.TempoMinutos = int.Parse(txtTempo.Text);
                
                // Salvar as alterações nas configurações
                Settings.Default.Save();
                this.DialogResult = DialogResult.OK;
                this.Close();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text == "Scire2023")
            {
                groupBox1.Visible = true;
                groupBox2.Visible = true;
                groupBox3.Visible = false;
                CArregardados();
            }
            else
                this.Close();
        }
    }
}
