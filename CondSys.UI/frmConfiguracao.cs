using CondSys.Henry;
using CondSys.Model;
using CondSys.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CondSys.UI
{
    public partial class frmConfiguracao : Form
    {
        public EventHandler frm_Closing;
        HenryClass _henry = null;
        public frmConfiguracao()
        {
            InitializeComponent();
        }

        private async void frmConfiguracao_Load(object sender, EventArgs e)
        {
            var config = await BuscarConfiguracao();
            txtIpEntrada.Text = config.IPEntradaVisitante;
            txtPortaEntrada.Text = config.PortaEntradaVisitante.ToString();
            txtIpSaida.Text = config.IPSaidaVisitante;
            txtPortaSaida.Text = config.PortaSaidaVisitante.ToString();
            txtIpEntradaMorador.Text = config.IPEntradaMorador;
            txtPortaEntradaMorador.Text = config.PortaEntradaMorador.ToString();
            txtIpSaidaMorador.Text = config.IPSaidaMorador;
            txtPortaSaidaMorador.Text = config.PortaSaidaMorador.ToString();
            txtIpEntradaPedestre.Text = config.IPEntradaPedestre;
            txtPortaEntradaPedestre.Text = config.PortaEntradaPedestre.ToString();
            txtIpSaidaPedestre.Text = config.IPSaidaPedestre;
            txtPortaSaidaPedestre.Text = config.PortaSaidaPedestre.ToString();
        }

        private void frmConfiguracao_FormClosing(object sender, FormClosingEventArgs e)
        {
            frm_Closing?.Invoke(this, null);
        }

        private async Task<Configuracao> BuscarConfiguracao()
        {
            string Uri = $"{ConfigurationManager.AppSettings["rest"]}/BuscarConfiguracao";
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(Uri))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();

                        Configuracao config = SerializerJson.JsonToObject<Configuracao>(json);
                        return config;
                    }
                    else
                    {
                        MessageBox.Show(response.ReasonPhrase, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return new Configuracao();
                    }
                }
            }
        }

        private async void btnSalvar_Click(object sender, EventArgs e)
        {
            if (txtIpEntrada.Text == txtIpSaida.Text)
            {
                MessageBox.Show("Configuração de entrada e saída são iguais", "Configuração", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string Uri = $"{ConfigurationManager.AppSettings["rest"]}/SalvarConfiguracao";
            var config = new
            {
                IPEntradaVisitante = txtIpEntrada.Text,
                PortaEntradaVisitante = string.IsNullOrEmpty(txtPortaEntrada.Text) ? 0 : int.Parse(txtPortaEntrada.Text),
                IPSaidaVisitante = txtIpSaida.Text,
                PortaSaidaVisitante = string.IsNullOrEmpty(txtPortaSaida.Text) ? 0 : int.Parse(txtPortaSaida.Text),
                IPEntradaMorador = txtIpEntradaMorador.Text,
                PortaEntradaMorador = string.IsNullOrEmpty(txtPortaEntradaMorador.Text) ? 0 : int.Parse(txtPortaEntradaMorador.Text),

                IPSaidaMorador = txtIpSaidaMorador.Text,
                PortaSaidaMorador = string.IsNullOrEmpty(txtPortaSaidaMorador.Text) ? 0 : int.Parse(txtPortaSaidaMorador.Text),

                IPEntradaPedestre = txtIpEntradaPedestre.Text,
                PortaEntradaPedestre = string.IsNullOrEmpty(txtPortaEntradaPedestre.Text) ? 0 : int.Parse(txtPortaEntradaPedestre.Text),

                IPSaidaPedestre = txtIpSaidaPedestre.Text,
                PortaSaidaPedestre = string.IsNullOrEmpty(txtPortaSaidaPedestre.Text) ? 0 : int.Parse(txtPortaSaidaPedestre.Text)
            };
            using (var client = new HttpClient())
            {
                using (var response = await client.PostAsJsonAsync(Uri, config))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        MessageBox.Show(response.ReasonPhrase, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private async Task TestarConexao(string ip, int porta)
        {
            Cursor.Current = Cursors.WaitCursor;
            await _henry.Connect(ip, porta);
            Cursor.Current = Cursors.Default;
        }

        private async void btnTestarEntrada_Click(object sender, EventArgs e)
        {
            await TestarConexao(txtIpEntrada.Text, int.Parse(txtPortaEntrada.Text));
        }

        private async void btnTestarSaida_Click(object sender, EventArgs e)
        {
            await TestarConexao(txtIpSaida.Text, int.Parse(txtPortaSaida.Text));
        }
    }
}
