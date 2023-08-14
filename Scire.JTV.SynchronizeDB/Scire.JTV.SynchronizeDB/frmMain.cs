using System;
using System.Drawing;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Scire.JTV.Domain.Services;
using System.IO;

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

        private NotifyIcon notifyIcon;

        private bool Resetou { get; set; }
        private bool StopServico { get; set; }

        public string PathLog { get; set; }

        #region Eventos do formulário

        public frmMain()
        {
            InitializeComponent();

            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            string path = AppDomain.CurrentDomain.BaseDirectory;

            PathLog = Path.Combine(path, "Log");

            if (!Directory.Exists(PathLog))
                Directory.CreateDirectory(PathLog);


            // Exibe a versão na janela ou faz o que desejar
            this.Text = $"Sincronizar Banco de Dados - Versão : {version}";

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

            Resetou = false;
            StopServico = false;
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
            lblErro.Text = "";
            
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

                DateTime dhAgora = DateTime.Now;

                DateTime dhReset;

                DateTime.TryParse(dhAgora.ToString("yyyy-MM-dd ") + configuracoes.HoraResetBd, out dhReset);
                                
                TimeSpan diferenca = dhAgora - dhReset;

                if (dhAgora >= dhReset && diferenca.TotalMinutes <= 60)
                {
                    if (!Resetou)
                    {
                        Resetou = servConfiguracoes.ResetBancoDados(CodigoCliente);

                        configuracoes = await Task.Run(() => servConfiguracoes.ConfiguracoesCliente(this.CodigoCliente));
                    }
                }
                else
                {
                    Resetou = false;                    
                }

                dhAgora = DateTime.Now.AddMinutes(-5);

                Task pessoasTask;
                Task chequesTask;
                Task duplicatasTask;

                pessoasTask = ChamadasServicosPessoas(configuracoes.DataHoraPessoas.Value, dhAgora);
                await Task.WhenAll(pessoasTask);

                chequesTask = ChamadasServicosCheques(configuracoes.DataHoraCheques.Value, dhAgora);
                await Task.WhenAll(chequesTask);

                duplicatasTask = ChamadasServicosDuplicadas(configuracoes.DataHoraDuplicatas.Value, dhAgora);
                await Task.WhenAll(duplicatasTask);

                servConfiguracoes.UpdateDhExecucao(CodigoCliente, dhAgora);
            }
            
            catch (Exception ex)
            {
                Invoke(new Action(() =>
                {
                    Sapiens.Library.Log.LogError log = new Sapiens.Library.Log.LogError(PathLog)
                    log.Log(ex, true, false);

                }));
            }
            finally
            {
                timer1.Interval = TempoMinutos * 60 * 1000;
                timer1.Enabled = true;
                timer1.Start();
                StopServico = false;
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
                }));

                int Resultadofinal = 0;

                if (dhAlteracao  <= new DateTime(2001,1,1,1,1,1))
                {
                    dhAlteracao = servPessoa.GetDataMinima();
                }

                if (StopServico)
                    return;

                // processo unico da empresas
                int retEmpresas = await servPessoa.ImportarEmpresas(CodigoCliente);
                
                DateTime dhbase = dhAlteracao.Date;
                while (dhbase <= dhAtual)
                {
                    if (StopServico)
                        break;

                    servPessoa = new PessoaService(FireConnection, MyConnection);
                    
                    DateTime dhini, dhfin;

                    dhini = dhbase;

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
                                        
                    await Task.WhenAll(pessoasTask, pessoasClientesTask, pessoasFisicasTask
                        , pessoasJuridicasTask, pessoasReferenciasTask, pessoasTelefonesTask);

                    int resultado1 = pessoasTask.Result;
                    int resultado2 = pessoasClientesTask.Result;
                    int resultado3 = pessoasFisicasTask.Result;
                    int resultado4 = pessoasJuridicasTask.Result;
                    int resultado5 = pessoasReferenciasTask.Result;
                    int resultado6 = pessoasTelefonesTask.Result;

                    Resultadofinal += resultado1 + resultado2 + resultado3 + resultado4 + resultado5 + resultado6;
    
                    Invoke(new Action(() =>
                    {
                        lblPessoas.Text = string.Format("Pessoas - Data: {0} - Registros: {1}", dhini.ToString("dd/MM/yyyy")
                            , resultado1 + resultado2 + resultado3 + resultado4 + resultado5 + resultado6);
                        this.Refresh();
                        
                    }));

                    if ((resultado1 + resultado2 + resultado3 + resultado4 + resultado5 + resultado6) > 0)
                    {
                        servConfiguracoes.UpdateDhAlteracao(CodigoCliente, servPessoa.DhAtualizar.AddMilliseconds(1), Infra.Data.MySql.EmpresaImportacaoRepository.Servico.Pessoa);
                        dhbase = servPessoa.DhAtualizar;
                    }
                    else
                    {
                        dhbase = dhbase.Date.AddDays(1);  // Incrementa a data em um dia
                    }
                }

                Mensa1 += " Fim: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "  Registros: "
                                   + Resultadofinal.ToString();

                Invoke(new Action(() =>
                {
                    lblPessoas.Text = Mensa1;
                    this.Update();
                    
                }));
            }
            catch (Exception ex)
            {
                Invoke(new Action(() =>
                {
                    Sapiens.Library.Log.LogError log = new Sapiens.Library.Log.LogError(PathLog)
                    log.Log(ex, true, false);
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
                    
                }));


                if (dhAlteracao <= new DateTime(2001, 1, 1, 1, 1, 1))
                {
                    dhAlteracao = servCheque.GetDataMinima();
                }

                int Resultadofinal = 0;

                DateTime dhbase = dhAlteracao.Date;
                while (dhbase <= dhAtual)
                {
                    if (StopServico)
                        break;

                    servCheque = new ChequesService(FireConnection, MyConnection);
                    
                    DateTime dhini, dhfin;

                    dhini = dhbase;

                    TimeSpan diff = dhAtual - dhbase;

                    if (diff.TotalHours < 24)
                        dhfin = dhAtual;
                    else
                        dhfin = dhini.Date.AddDays(1);

                    Task<int> chequesTask = servCheque.ImportarCheques(dhini, dhfin, CodigoCliente);

                    Task<int> chequesbaixasTask = servCheque.ImportarChequesBaixas(dhini, dhfin, CodigoCliente);

                    Task<int> chequesdevolvidosTask = servCheque.ImportarChequesDevolvidos(dhini, dhfin, CodigoCliente);
                                        
                    await Task.WhenAll(chequesTask, chequesbaixasTask, chequesdevolvidosTask);

                    int resultado1 = chequesTask.Result;
                    int resultado2 = chequesbaixasTask.Result;
                    int resultado3 = chequesdevolvidosTask.Result;

                    Resultadofinal += resultado1 + resultado2 + resultado3;
    
                    Invoke(new Action(() =>
                    {
                        lblCheques.Text = string.Format("Cheques - Data: {0} - Registros: {1}", dhini.ToString("dd/MM/yyyy"), resultado1 + resultado2 + resultado3);
                        this.Refresh();
                        
                    }));

                    if ((resultado1 + resultado2 + resultado3) > 0)
                    {
                        servConfiguracoes.UpdateDhAlteracao(CodigoCliente, servCheque.DhAtualizar.AddSeconds(1), Infra.Data.MySql.EmpresaImportacaoRepository.Servico.Cheques);
                        dhbase = servCheque.DhAtualizar;
                    }
                    else
                    {
                        dhbase = dhbase.Date.AddDays(1);  // Incrementa a data em um dia
                    }

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
                    Sapiens.Library.Log.LogError log = new Sapiens.Library.Log.LogError(PathLog)
                    log.Log(ex, true, false);

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

                DateTime dhbase = dhAlteracao.Date;
                while (dhbase <= dhAtual)
                {
                    if (StopServico)
                        break;

                    servDuplicata = new DuplicatasService(FireConnection, MyConnection);

                    DateTime dhini, dhfin;
                                       
                    dhini = dhbase;

                    TimeSpan diff = dhAtual - dhbase;

                    if (diff.TotalHours < 24)
                        dhfin = dhAtual;
                    else
                        dhfin = dhini.Date.AddDays(1);


                    Task<int> duplicataTask = servDuplicata.ImportarDuplicatas(dhini, dhfin, CodigoCliente);
                                        
                    await Task.WhenAll(duplicataTask);

                    Task<int> duplicatabaixasTask = servDuplicata.ImportarDuplicatasBaixas(dhini, dhfin, CodigoCliente);

                    await Task.WhenAll(duplicatabaixasTask);

                    int resultado1 = duplicataTask.Result;
                    int resultado2 = duplicatabaixasTask.Result;
                    Resultadofinal += resultado1 + resultado2;
                                        
                    Invoke(new Action(() =>
                    {
                        lblDuplicatas.Text = string.Format("Duplicatas - Data: {0} - Registros: {1}", dhini.ToString("dd/MM/yyyy"), resultado1);
                        lblDuplicatasBaixas.Text = string.Format("Dup Baixas - Data: {0} - Registros: {1}", dhini.ToString("dd/MM/yyyy"), resultado2);
                        this.Refresh();
                        
                    }));

                    if ((resultado1 + resultado2 ) > 0)
                    {
                        servConfiguracoes.UpdateDhAlteracao(CodigoCliente, servDuplicata.DhAtualizar.AddSeconds(1), Infra.Data.MySql.EmpresaImportacaoRepository.Servico.Duplicatas);
                        dhbase = servDuplicata.DhAtualizar;
                    }
                    else
                    {
                        dhbase = dhbase.Date.AddDays(1);  // Incrementa a data em um dia
                    }
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
                    Sapiens.Library.Log.LogError log = new Sapiens.Library.Log.LogError(PathLog)
                    log.Log(ex, true, false);

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
            this.StopServico = true;

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
                this.StopServico = false;
                timer1.Interval = 1000; // Intervalo de 1 segundo                
                Testarconexoes();

                servPessoa = new PessoaService(FireConnection, MyConnection);
                servConfiguracoes = new ConfiguracoesService(FireConnection, MyConnection);
                servCheque = new ChequesService(FireConnection, MyConnection);
                servDuplicata = new DuplicatasService(FireConnection, MyConnection);
            }
        }

        
    }

}
