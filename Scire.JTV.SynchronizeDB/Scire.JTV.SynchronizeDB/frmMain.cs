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
        ChequesService servCheque;
        DuplicatasService servDuplicata;
        ConfiguracoesService servConfiguracoes;
        Criptografia cript;

        private int CodigoCliente { get; set; }
        private string MyConnection { get; set; }
        private string FireConnection { get; set; }
        private int TempoMinutos { get; set; }

        public string Mensa1 { get; set; }
        public string Mensa2 { get; set; }
        public string Mensa3 { get; set; }

        private Timer timer1;
        private Timer timer2;

        public frmMain()
        {
            InitializeComponent();
            
            InitializeComponent();
            timer1 = new Timer();
            timer1.Interval = 1000; // Intervalo de 1 segundo
            timer1.Enabled = false;
            timer1.Tick += Timer1_Tick;
            
            timer2 = new Timer();
            timer2.Interval = 1000; // Intervalo de 1 segundo
            timer2.Enabled = true;
            timer2.Tick += Timer2_Tick;  
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            timer2.Start();

            cript = new Criptografia();
            
            CodigoCliente = Properties.Settings.Default.CodigoCliente;           
            TempoMinutos = Properties.Settings.Default.TempoMinutos;
            MyConnection = cript.Decrypt( Properties.Settings.Default.ConnectionTarget);
            FireConnection = cript.Decrypt(Properties.Settings.Default.ConnectionSource);

            servPessoa = new PessoaService(FireConnection, MyConnection);
            servConfiguracoes = new ConfiguracoesService(FireConnection, MyConnection);
            servCheque = new ChequesService(FireConnection, MyConnection);
            servDuplicata = new DuplicatasService(FireConnection, MyConnection);

            Testarconexoes();
        }

        private async void Timer2_Tick(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                Application.DoEvents();
                
            });
            Invoke(new Action(() => { this.Update(); }));
        }
               
        
        private async void Timer1_Tick(object sender, EventArgs e)
        {
            if (this.CodigoCliente <= 0)
                MessageBox.Show("Cliente não configurado. Verfique!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (this.TempoMinutos <= 0)
                MessageBox.Show("Configuração inválida. Verfique!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            await Task.Run(() =>
            {
                if (MyTestConnection && FireTestConnection)
                {
                    timer1.Enabled = false;
                    ChamadasServicos();
                }              
            });
           
            Invoke(new Action(() => { this.Update(); }));
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

                Task pessoasTask = ChamadasServicosPessoas(dhalteracao, dhagora);
                Task chequesTask = ChamadasServicosCheques(dhalteracao, dhagora);
                Task duplicatasTask = ChamadasServicosDuplicadas(dhalteracao, dhagora);

                Application.DoEvents();
                await Task.WhenAll(pessoasTask, chequesTask, duplicatasTask);
                
                //dhagora = new DateTime(1980, 1, 1);
                servConfiguracoes.UpdateDhAlteracao(CodigoCliente, dhagora);

            }
            catch (Exception ex)
            {
                Invoke(new Action(() =>
                {
                    lblErro.Text = ex.Message;

                }));
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
                Mensa1 = "Inicio: " + DateTime.Now.ToString("dd/mm/yyyy HH:mm:ss");

                Invoke(new Action(() => {
                    lblPessoas.Text = Mensa1;
                    pictureBox1.Visible = true;
                    this.Update();
                    Application.DoEvents();
                }));

                Task<int> pessoasTask = servPessoa.ImportarPessoas(dhAlteracao, dhAtual, CodigoCliente);

                Task<int> pessoasClientesTask = servPessoa.ImportarPessoasClientes(dhAlteracao, dhAtual, CodigoCliente);

                Task<int> pessoasFisicasTask = servPessoa.ImportarPessoasFisicas(dhAlteracao, dhAtual, CodigoCliente);

                Task<int> pessoasJuridicasTask = servPessoa.ImportarPessoasJuricas(dhAlteracao, dhAtual, CodigoCliente);

                Task<int> pessoasReferenciasTask = servPessoa.ImportarPessoasReferencias(dhAlteracao, dhAtual, CodigoCliente);

                Task<int> pessoasTelefonesTask = servPessoa.ImportarPessoasTelefones(dhAlteracao, dhAtual, CodigoCliente);

                Task<int> empresasTask = servPessoa.ImportarEmpresas(CodigoCliente);

                Application.DoEvents();
                await Task.WhenAll(pessoasTask, pessoasClientesTask, pessoasFisicasTask
                    , pessoasJuridicasTask, pessoasReferenciasTask, pessoasTelefonesTask, empresasTask);

                int resultado1 = pessoasTask.Result;
                int resultado2 = pessoasClientesTask.Result;
                int resultado3 = pessoasFisicasTask.Result;
                int resultado4 = pessoasJuridicasTask.Result;
                int resultado5 = pessoasReferenciasTask.Result;
                int resultado6 = pessoasTelefonesTask.Result;
                int resultado7 = empresasTask.Result;

                Mensa1 += " Fim: " + DateTime.Now.ToString("dd/mm/yyyy HH:mm:ss") + "  Registros: "
                                + (resultado1 + resultado2 + resultado3 + resultado4 + resultado5 + resultado6 ).ToString();

                Invoke(new Action(() =>
                {
                    lblPessoas.Text = Mensa1;
                    this.Update();
                    Application.DoEvents();
                }));
            }
            catch (Exception ex)
            {
                Invoke(new Action(() =>
                {
                    lblErro.Text = ex.Message;
                }));
            }
            finally
            {
                Invoke(new Action(() => {
                    pictureBox1.Visible = false;
                }));
                Application.DoEvents();
            }
        }

        private async Task ChamadasServicosCheques(DateTime dhAlteracao, DateTime dhAtual)
        {
            try
            {
                Mensa2 = "Inicio: " + DateTime.Now.ToString("dd/mm/yyyy HH:mm:ss");
                Invoke(new Action(() =>
                {
                    lblCheques.Text = Mensa2;
                    pictureBox2.Visible = true;
                    this.Update();
                    Application.DoEvents();
                }));

                Task<int> chequesTask = servCheque.ImportarCheques(dhAlteracao, dhAtual, CodigoCliente);

                Task<int> chequesbaixasTask = servCheque.ImportarChequesBaixas(dhAlteracao, dhAtual, CodigoCliente);

                Task<int> chequesdevolvidosTask = servCheque.ImportarChequesDevolvidos(dhAlteracao, dhAtual, CodigoCliente);

                Application.DoEvents();
                await Task.WhenAll(chequesTask, chequesbaixasTask, chequesdevolvidosTask);

                int resultado1 = chequesTask.Result;
                int resultado2 = chequesbaixasTask.Result;
                int resultado3 = chequesdevolvidosTask.Result;

                Mensa2 += " Fim: " + DateTime.Now.ToString("dd/mm/yyyy HH:mm:ss") + "  Registros: "
                        + (resultado1 + resultado2 + resultado3).ToString();
                Invoke(new Action(() =>
                {
                    lblCheques.Text = Mensa2;
                }));
                

            }
            catch (Exception ex)
            {
                Invoke(new Action(() =>
                {
                    lblErro.Text = ex.Message;

                }));
            }
            finally
            {
                Invoke(new Action(() => {
                    pictureBox2.Visible = false;
                }));
                Application.DoEvents();
            }
        }

        private async Task ChamadasServicosDuplicadas(DateTime dhAlteracao, DateTime dhAtual)
        {
            try
            {
                Mensa3 = "Inicio: " + DateTime.Now.ToString("dd/mm/yyyy HH:mm:ss");
                Invoke(new Action(() =>
                {
                    lblDuplicatas.Text = Mensa3;
                    pictureBox3.Visible = true;
                }));

                Task<int> duplicataTask = servDuplicata.ImportarDuplicatas(dhAlteracao, dhAtual, CodigoCliente);

                Task<int> duplicatabaixasTask = servDuplicata.ImportarDuplicatasBaixas(dhAlteracao, dhAtual, CodigoCliente);


                Application.DoEvents();
                await Task.WhenAll(duplicataTask, duplicatabaixasTask);

                int resultado1 = duplicataTask.Result;
                int resultado2 = duplicatabaixasTask.Result;
                Mensa3 += " Fim: " + DateTime.Now.ToString("dd/mm/yyyy HH:mm:ss") + "  Registros: "
                   + (resultado1 + resultado2).ToString();
                Invoke(new Action(() =>
                {
                    lblDuplicatas.Text = Mensa3;
                }));
               

            }
            catch (Exception ex)
            {
                Invoke(new Action(() =>
                {
                    lblErro.Text = ex.Message;

                }));
            }
            finally
            {
                Invoke(new Action(() => {
                    pictureBox3.Visible = false;
                }));
                Application.DoEvents();
            }
        }

        private void Testarconexoes()
        {
            CodigoCliente = Properties.Settings.Default.CodigoCliente;           
            TempoMinutos = Properties.Settings.Default.TempoMinutos;
            MyConnection = cript.Decrypt(Properties.Settings.Default.ConnectionTarget);
            FireConnection = cript.Decrypt(Properties.Settings.Default.ConnectionSource);

            Application.DoEvents();
            PessoaService servPessoa = new PessoaService(FireConnection, MyConnection);

            if (this.CodigoCliente <= 0 || this.TempoMinutos <= 0)
            {
                lblErro.Text = "Configuração inválida. Verfique!";
                FireTestConnection = false;
                MyTestConnection = false;                
            }
            else
            {
                FireTestConnection = servPessoa.TestarFire();
                MyTestConnection = servPessoa.TestarMy();
            }

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


        private void button1_Click(object sender, EventArgs e)
        {
            frmConfiguracoes configuracoesForm = new frmConfiguracoes();

            configuracoesForm.TempoMinutos = this.TempoMinutos;
            configuracoesForm.CodigoCliente = this.CodigoCliente;
            configuracoesForm.MyConnection = this.MyConnection;
            configuracoesForm.FireConnection = this.FireConnection;
                     

            // Exibe o formulário como um diálogo e aguarda até que ele seja fechado
            DialogResult result = configuracoesForm.ShowDialog();

            // Verifica se o formulário foi fechado pelo botão "Salvar" (ou qualquer outro critério desejado)
            if (result == DialogResult.OK)
            {
                Testarconexoes();

                servPessoa = new PessoaService(FireConnection, MyConnection);
                servConfiguracoes = new ConfiguracoesService(FireConnection, MyConnection);
                servCheque = new ChequesService(FireConnection, MyConnection);
                servDuplicata = new DuplicatasService(FireConnection, MyConnection);
            }
        }
    }

}
