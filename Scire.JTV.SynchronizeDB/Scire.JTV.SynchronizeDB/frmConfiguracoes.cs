using System;
using System.Drawing;
using System.Windows.Forms;
using Scire.JTV.Domain.Services;
using Scire.JTV.SynchronizeDB.Properties;

namespace Scire.JTV.SynchronizeDB
{
    public partial class frmConfiguracoes : Form
    {
        public string MyConnection { get; set; }
        public string FireConnection { get; set; }
        public int CodigoCliente { get; set; }
        public int TempoMinutos { get; set; }
        public string ResetBD { get; set; }

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
            txtReset.Text = ResetBD;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var fireconn = string.Format("User={0};Password={1};Database={2};DataSource={3};Port={4}"
                    , fireUSer.Text, firePassword.Text, fireDb.Text, fireServ.Text, firePort.Text);

                var myconn = string.Format("server={0};port={1};database={2};user id={3};password={4}"
                    , myServer.Text, myPorta.Text, myDb.Text, myUser.Text, myPassword.Text);


                Criptografia cript = new Criptografia();
                fireconn = cript.Encrypt(fireconn);
                myconn = cript.Encrypt(myconn);


                Settings.Default.ConnectionSource = fireconn;
                Settings.Default.ConnectionTarget = myconn;

                Settings.Default.CodigoCliente = int.Parse(txtCodigoCliente.Text);
                Settings.Default.TempoMinutos = int.Parse(txtTempo.Text);
                Settings.Default.ResetBD = txtReset.Text.Trim();
                
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

        private void button3_Click(object sender, EventArgs e)
        {
            var fireconn = string.Format("User={0};Password={1};Database={2};DataSource={3};Port={4}"
                   , fireUSer.Text, firePassword.Text, fireDb.Text, fireServ.Text, firePort.Text);

            var myconn = string.Format("server={0};port={1};database={2};user id={3};password={4}"
                , myServer.Text, myPorta.Text, myDb.Text, myUser.Text, myPassword.Text);

            PessoaService servPessoa1 = new PessoaService(fireconn, myconn);
            
            bool  FireTestConnection = servPessoa1.TestarFire();

            if (FireTestConnection)
            {
                lblFire.Text = "Conectado";
                lblFire.ForeColor = Color.Green;                
            }
            else
            {
                lblFire.Text = "Erro";
                lblFire.ForeColor = Color.Red;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var fireconn = string.Format("User={0};Password={1};Database={2};DataSource={3};Port={4}"
                   , fireUSer.Text, firePassword.Text, fireDb.Text, fireServ.Text, firePort.Text);

            var myconn = string.Format("server={0};port={1};database={2};user id={3};password={4}"
                , myServer.Text, myPorta.Text, myDb.Text, myUser.Text, myPassword.Text);

            PessoaService servPessoa1 = new PessoaService(fireconn, myconn);
            
            bool MyTestConnection = servPessoa1.TestarMy();

            if (MyTestConnection)
            {
                lblMy.Text = "Conectado";
                lblMy.ForeColor = Color.Green;
            }
            else
            {

                lblMy.Text = "Erro";
                lblMy.ForeColor = Color.Red;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "Scire2023")
            {
                groupBox1.Visible = true;
                groupBox2.Visible = true;
                groupBox4.Visible = true;
                groupBox3.Visible = false;

                CArregardados();
            }
            else
                this.Close();
        }
    }    
}
