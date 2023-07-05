using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Scire.JTV.Domain.Services;

namespace Scire.JTV.SynchronizeDB
{
    public partial class frmMain : Form
    {
        private bool MyTestConnection { get; set; }
        private bool FireTestConnection { get; set; }
        PessoaService servPessoa;
        ConfiguracoesService servConfiguracoes;

        private int CodigoCliente { get; set; }
        private string MyConnection { get; set; }
        private string FireConnection { get; set; }
        private int TempoMinutos { get; set; }


        public frmMain()
        {
            InitializeComponent();           
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            CodigoCliente = Properties.Settings.Default.CodigoCliente;
            MyConnection = Properties.Settings.Default.ConnectionTarget;
            FireConnection = Properties.Settings.Default.ConnectionSource;
            TempoMinutos = Properties.Settings.Default.TempoMinutos;


            servPessoa = new PessoaService(FireConnection, MyConnection);
            servConfiguracoes = new ConfiguracoesService(FireConnection, MyConnection);

            Testarconexoes();
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Update();
            if (MyTestConnection && FireTestConnection)
            {
                timer1.Enabled = false;
                ChamadasServicos();
            }
        }

        private async void ChamadasServicos()
        {
            try
            {
                var configuracoes = await Task.Run(() => servConfiguracoes.ConfiguracoesCliente(this.CodigoCliente));

                if (configuracoes == null)
                    throw new Exception("Cliente não configurado. Verfique!");

                DateTime dhalteracao = configuracoes.DataHoraImportacao.Value;
                DateTime dhagora = DateTime.Now.AddMinutes(-10);

                Task pessoas = ChamadasServicosPessoas(dhalteracao, dhagora);

                Application.DoEvents();
                await Task.WhenAll(pessoas);
                
                dhagora = new DateTime(1980, 1, 1);
                servConfiguracoes.UpdateDhAlteracao(CodigoCliente, dhagora);

            }
            catch (Exception ex)
            {
                lblErro.Text = ex.Message;
            }
            finally
            {
                timer1.Interval = TempoMinutos * 60 * 1000;
                timer1.Enabled = true;
                Application.DoEvents();
            }
        }

        private async Task ChamadasServicosPessoas(DateTime dhAlteracao, DateTime dhAtual)
        {
            try
            {
                lblPessoas.Text = "Pessoas - Inicio: " + DateTime.Now.ToString("dd/mm/yyyy HH:mm:ss");
                
                Task<int> pessoasTask = servPessoa.ImportarPessoas(dhAlteracao, dhAtual, CodigoCliente);

                Task<int> pessoasClientesTask = servPessoa.ImportarPessoasClientes(dhAlteracao, dhAtual, CodigoCliente);

                Task<int> pessoasFisicasTask = servPessoa.ImportarPessoasFisicas(dhAlteracao, dhAtual, CodigoCliente);

                Application.DoEvents();
                await Task.WhenAll(pessoasTask, pessoasClientesTask, pessoasFisicasTask);
                
                int resultado1= pessoasTask.Result; 
                int resultado2 = pessoasClientesTask.Result; 
                int resultado3 = pessoasFisicasTask.Result;

                // Agora você pode usar os resultados como necessário
                lblPessoas.Text += " Fim: " + DateTime.Now.ToString("dd/mm/yyyy HH:mm:ss") + "  Registros: " 
                    + (resultado1 + resultado2 + resultado3).ToString();

            }
            catch (Exception ex)
            {
                lblErro.Text = ex.Message;
            }
            finally
            {
                Application.DoEvents();
            }
        }

        private void Testarconexoes()
        {
            Application.DoEvents();
            PessoaService servPessoa = new PessoaService(FireConnection, MyConnection);

            FireTestConnection = servPessoa.TestarFire();
            MyTestConnection = servPessoa.TestarMy();

            if (FireTestConnection)
            {
                label3.Text = "Conectado";
                label3.ForeColor = Color.Green;
                errorProvider1.SetError(label3, "");
            }
            else
            {

                label3.Text = "Erro";
                label3.ForeColor = Color.Red;
                errorProvider1.SetError(label3, "Verificar");
            }


            if (MyTestConnection)
            {
                label4.Text = "Conectado";
                label4.ForeColor = Color.Green;
                errorProvider1.SetError(label4, "");
            }
            else
            {

                label4.Text = "Erro";
                label4.ForeColor = Color.Red;
                errorProvider1.SetError(label4, "Verificar");
            }

            // timer1.Interval = Properties.Settings.Default.TempoMinutos * 60 * 1000;

            if (FireTestConnection && MyTestConnection)
            {
                timer1.Enabled = true;
                timer1.Start();
            }
            else
            {
                timer1.Stop();
                timer1.Enabled = false;
            }
        }


    }

}
