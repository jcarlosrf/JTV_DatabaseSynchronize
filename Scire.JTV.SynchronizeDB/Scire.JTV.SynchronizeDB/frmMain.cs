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

        private NotifyIcon notifyIcon;

        private DateTime dhReset { get; set; }

        #region Eventos do formulário

        public frmMain()
        {
            InitializeComponent();
            
            InitializeComponent();
            timer1 = new Timer();
            timer1.Interval = 1000; // Intervalo de 1 segundo
            timer1.Enabled = false;
            timer1.Tick += Timer1_Tick;
            
            //timer2 = new Timer();
            //timer2.Interval = 1000; // Intervalo de 1 segundo
            //timer2.Enabled = true;
            //timer2.Tick += Timer2_Tick;

            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = Properties.Resources.IconePrincipal; // Defina o ícone desejado
            notifyIcon.Text = "Synchronize DB"; // Defina o texto de dica
            notifyIcon.DoubleClick += NotifyIcon_DoubleClick;

            dhReset = new DateTime(1900, 1, 1);
        }
        
        private void frmMain_Load(object sender, EventArgs e)
        {
            //timer2.Start();            

            Testarconexoes();
        }

        private void NotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon.Visible = true;
            }
        }

        #endregion

        #region timers

        //private async void Timer2_Tick(object sender, EventArgs e)
        //{
        //    await Task.Run(() =>
        //    {
        //        Application.DoEvents();
                
        //    });
        //    Invoke(new Action(() => { this.Update(); }));
        //}               
        
        private async void Timer1_Tick(object sender, EventArgs e)
        {
            if (this.CodigoCliente <= 0 || this.TempoMinutos <= 0)
            {
                MessageBox.Show("Configuração inválida. Verfique!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
           
            timer1.Enabled = false;

            Task taskChamadas = ChamadasServicos();

            await Task.WhenAll(taskChamadas);

            timer1.Enabled = true;            
        }

        #endregion

        #region configuracoes 

        private void BuscarConfiguracoes()
        {
            cript = new Criptografia();

            CodigoCliente = Properties.Settings.Default.CodigoCliente;
            TempoMinutos = Properties.Settings.Default.TempoMinutos;            
            MyConnection = cript.Decrypt(Properties.Settings.Default.ConnectionTarget);
            FireConnection = cript.Decrypt(Properties.Settings.Default.ConnectionSource);
        }

        private void Testarconexoes()
        {
            BuscarConfiguracoes();

            Application.DoEvents();
            PessoaService servPessoa1 = new PessoaService(FireConnection, MyConnection);

            if (this.CodigoCliente <= 0 || this.TempoMinutos <= 0)
            {
                lblErro.Text = "Configuração inválida. Verfique!";
                FireTestConnection = false;
                MyTestConnection = false;
            }
            else
            {
                FireTestConnection = servPessoa1.TestarFire();
                MyTestConnection = servPessoa1.TestarMy();
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

        #endregion

        #region Servicos

        private async Task ChamadasServicos()
        {
            try
            {
                servConfiguracoes = new ConfiguracoesService(FireConnection, MyConnection);
                servPessoa = new PessoaService(FireConnection, MyConnection);
                servCheque = new ChequesService(FireConnection, MyConnection);
                servDuplicata = new DuplicatasService(FireConnection, MyConnection);

                var configuracoes = await Task.Run(() => servConfiguracoes.ConfiguracoesCliente(this.CodigoCliente));

                if (configuracoes == null)
                    throw new Exception("Cliente não configurado. Verfique!");

                DateTime dhagora = DateTime.Now.AddMinutes(-10);

                DateTime dhreset;

                DateTime.TryParse(dhagora.ToString("yyyy-MM-dd ") + configuracoes.HoraResetBd, out dhreset);
                
                Task pessoasTask ;
                Task chequesTask ;
                Task duplicatasTask ;

                if (dhagora >= dhreset &&  this.dhReset.Date < dhagora.Date)
                {
                    dhReset = dhagora;
                    pessoasTask = ChamadasServicosPessoas(new DateTime(2000,1,1,1,1,1), dhagora);
                    chequesTask = ChamadasServicosCheques(new DateTime(2000, 1, 1, 1, 1, 1), dhagora);
                    duplicatasTask = ChamadasServicosDuplicadas(new DateTime(2000, 1, 1, 1, 1, 1), dhagora);
                }
                else
                {
                     pessoasTask = ChamadasServicosPessoas(configuracoes.DataHoraPessoas.Value, dhagora);
                     chequesTask = ChamadasServicosCheques(configuracoes.DataHoraCheques.Value, dhagora);
                     duplicatasTask = ChamadasServicosDuplicadas(configuracoes.DataHoraDuplicatas.Value, dhagora);
                }

                Application.DoEvents();
                await Task.WhenAll(pessoasTask, chequesTask, duplicatasTask);
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
                timer1.Start();
                Application.DoEvents();
            }
        }

        private async Task ChamadasServicosPessoas(DateTime dhAlteracao, DateTime dhAtual)
        {
            try
            {
                Mensa1 = "Inicio: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                Invoke(new Action(() => {
                    lblPessoas.Text = Mensa1;
                    pictureBox1.Visible = true;
                    this.Update();
                    Application.DoEvents();
                }));

                int Resultadofinal = 0;

                if (dhAlteracao  <= new DateTime(2001,1,1,1,1,1))
                {
                    dhAlteracao = servPessoa.GetDataMinima();
                }

                // processo unico da empresas
                int retEmpresas = await servPessoa.ImportarEmpresas(CodigoCliente);

                for (DateTime dhbase = dhAlteracao; dhbase <= dhAtual; dhbase = dhbase.AddDays(1))
                {
                    servPessoa = new PessoaService(FireConnection, MyConnection);
                    
                    DateTime dhini, dhfin;

                    if (dhbase.Date < dhAlteracao)
                        dhini = dhAlteracao;
                    else
                        dhini = dhbase.Date;

                    TimeSpan diff = dhAtual - dhbase;

                    if (diff.TotalHours < 24)
                        dhfin = dhAtual;
                    else
                        dhfin = dhini.Date.AddDays(1);

                    Task<int> pessoasTask = servPessoa.ImportarPessoas(dhini, dhfin, CodigoCliente);

                    Task<int> pessoasClientesTask = servPessoa.ImportarPessoasClientes(dhini, dhfin, CodigoCliente);

                    Task<int> pessoasFisicasTask = servPessoa.ImportarPessoasFisicas(dhini, dhfin, CodigoCliente);

                    Task<int> pessoasJuridicasTask = servPessoa.ImportarPessoasJuricas(dhini, dhfin, CodigoCliente);

                    Task<int> pessoasReferenciasTask = servPessoa.ImportarPessoasReferencias(dhini, dhfin, CodigoCliente);

                    Task<int> pessoasTelefonesTask = servPessoa.ImportarPessoasTelefones(dhini, dhfin, CodigoCliente);
                                        

                    Application.DoEvents();
                    await Task.WhenAll(pessoasTask, pessoasClientesTask, pessoasFisicasTask
                        , pessoasJuridicasTask, pessoasReferenciasTask, pessoasTelefonesTask);

                    int resultado1 = pessoasTask.Result;
                    int resultado2 = pessoasClientesTask.Result;
                    int resultado3 = pessoasFisicasTask.Result;
                    int resultado4 = pessoasJuridicasTask.Result;
                    int resultado5 = pessoasReferenciasTask.Result;
                    int resultado6 = pessoasTelefonesTask.Result;

                    Resultadofinal += resultado1 + resultado2 + resultado3 + resultado4 + resultado5 + resultado6;

                    if ((resultado1 + resultado2 + resultado3 + resultado4 + resultado5 + resultado6) > 0)
                        servConfiguracoes.UpdateDhAlteracao(CodigoCliente, servPessoa.DhAtualizar.AddSeconds(1), Infra.Data.MySql.EmpresaImportacaoRepository.Servico.Pessoa);


                    Invoke(new Action(() =>
                    {
                        lblPessoas.Text = string.Format("Pessoas - Data: {0} - Registros: {1}", dhini.ToString("dd/MM/yyyy")
                            , resultado1 + resultado2 + resultado3 + resultado4 + resultado5 + resultado6);
                        this.Refresh();
                        Application.DoEvents();
                    }));
                   
                }

                Mensa1 += " Fim: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "  Registros: "
                                   + Resultadofinal.ToString();

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
                Mensa2 = "Inicio: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                Invoke(new Action(() =>
                {
                    lblCheques.Text = Mensa2;
                    pictureBox2.Visible = true;
                    this.Update();
                    Application.DoEvents();
                }));


                if (dhAlteracao <= new DateTime(2001, 1, 1, 1, 1, 1))
                {
                    dhAlteracao = servCheque.GetDataMinima();
                }

                int Resultadofinal = 0;

                for (DateTime dhbase = dhAlteracao; dhbase <= dhAtual; dhbase = dhbase.AddDays(1))
                {
                    servCheque = new ChequesService(FireConnection, MyConnection);
                    
                    DateTime dhini, dhfin;

                    if (dhbase.Date < dhAlteracao)
                        dhini = dhAlteracao;
                    else
                        dhini = dhbase.Date;

                    TimeSpan diff = dhAtual - dhbase;

                    if (diff.TotalHours < 24)
                        dhfin = dhAtual;
                    else
                        dhfin = dhini.Date.AddDays(1);

                    Task<int> chequesTask = servCheque.ImportarCheques(dhini, dhfin, CodigoCliente);

                    Task<int> chequesbaixasTask = servCheque.ImportarChequesBaixas(dhini, dhfin, CodigoCliente);

                    Task<int> chequesdevolvidosTask = servCheque.ImportarChequesDevolvidos(dhini, dhfin, CodigoCliente);

                    Application.DoEvents();
                    await Task.WhenAll(chequesTask, chequesbaixasTask, chequesdevolvidosTask);

                    int resultado1 = chequesTask.Result;
                    int resultado2 = chequesbaixasTask.Result;
                    int resultado3 = chequesdevolvidosTask.Result;

                    Resultadofinal += resultado1 + resultado2 + resultado3;

                    if ((resultado1 + resultado2+resultado3) > 0)
                        servConfiguracoes.UpdateDhAlteracao(CodigoCliente, servCheque.DhAtualizar.AddSeconds(1), Infra.Data.MySql.EmpresaImportacaoRepository.Servico.Cheques);

                    Invoke(new Action(() =>
                    {
                        lblCheques.Text = string.Format("Cheques - Data: {0} - Registros: {1}", dhini.ToString("dd/MM/yyyy"), resultado1 + resultado2 + resultado3);
                        this.Refresh();
                        Application.DoEvents();
                    }));                                       
                  
                }
                Mensa2 += " Fim: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "  Registros: "
                          + (Resultadofinal).ToString();

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
                Invoke(new Action(() =>
                {
                    pictureBox2.Visible = false;
                }));
                Application.DoEvents();
            }
        }

        private async Task ChamadasServicosDuplicadas(DateTime dhAlteracao, DateTime dhAtual)
        {
            Mensa3 = "Inicio: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            Invoke(new Action(() =>
            {
                lblDuplicatas.Text = Mensa3;
                pictureBox3.Visible = true;
            }));


            try
            {

                if (dhAlteracao <= new DateTime(2001, 1, 1, 1, 1, 1))
                {
                    dhAlteracao = servDuplicata.GetDataMinima();
                }

                int Resultadofinal = 0;

                for (DateTime dhbase = dhAlteracao; dhbase <= dhAtual; dhbase = dhbase.AddDays(1))
                {
                    servDuplicata = new DuplicatasService(FireConnection, MyConnection);

                    DateTime dhini, dhfin;

                    if (dhbase.Date < dhAlteracao)
                        dhini = dhAlteracao;
                    else
                        dhini = dhbase.Date;

                    TimeSpan diff = dhAtual - dhbase;

                    if (diff.TotalHours < 24)
                        dhfin = dhAtual;
                    else
                        dhfin = dhini.Date.AddDays(1);


                    Task<int> duplicataTask = servDuplicata.ImportarDuplicatas(dhini, dhfin, CodigoCliente);

                    Task<int> duplicatabaixasTask = servDuplicata.ImportarDuplicatasBaixas(dhini, dhfin, CodigoCliente);

                    Application.DoEvents();
                    await Task.WhenAll(duplicataTask, duplicatabaixasTask);

                    int resultado1 = duplicataTask.Result;
                    int resultado2 = duplicatabaixasTask.Result;
                    Resultadofinal += resultado1 + resultado2;

                    if ((resultado1 + resultado2) > 0)
                        servConfiguracoes.UpdateDhAlteracao(CodigoCliente, servDuplicata.DhAtualizar.AddSeconds(1), Infra.Data.MySql.EmpresaImportacaoRepository.Servico.Duplicatas);

                    Invoke(new Action(() =>
                    {
                        lblDuplicatas.Text = string.Format("Duplicatas - Data: {0} - Registros: {1}", dhini.ToString("dd/MM/yyyy"), resultado1);
                        lblDuplicatasBaixas.Text = string.Format("Dup Baixas - Data: {0} - Registros: {1}", dhini.ToString("dd/MM/yyyy"), resultado2);
                        this.Refresh();
                        
                    }));
                }

                Mensa3 += " Fim: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "  Registros: "
                       + Resultadofinal.ToString();

                Invoke(new Action(() =>
                {
                    lblDuplicatas.Text = Mensa3;
                    lblDuplicatasBaixas.Text = "";

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
                Invoke(new Action(() =>
                {
                    pictureBox3.Visible = false;
                }));
                Application.DoEvents();
            }
        }

        #endregion


        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer1.Enabled = false;


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
