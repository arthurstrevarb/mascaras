using System.Globalization;
using System.Text.RegularExpressions;

namespace TrabalhandoComMascaras
{
    public partial class Frm_Mascara : Form
    {
        public Frm_Mascara()
        {
            InitializeComponent();
        }

        private void Btn_Hora_Click(object sender, EventArgs e)
        {
            Msk_TextBox.UseSystemPasswordChar = false;
            Lbl_Conteudo.Text = "";
            Msk_TextBox.Mask = "00:00";
            Lbl_MascaraAtiva.Text = Msk_TextBox.Mask;
            Msk_TextBox.Text = "";
            Msk_TextBox.Focus();
        }

        private void Btn_Data_Click(object sender, EventArgs e)
        {
            Msk_TextBox.UseSystemPasswordChar = false;
            Lbl_Conteudo.Text = "";
            Msk_TextBox.Mask = "00/00/0000";
            Lbl_MascaraAtiva.Text = Msk_TextBox.Mask;
            Msk_TextBox.Text = "";
            Msk_TextBox.Focus();
        }

        private void Btn_Telefone_Click(object sender, EventArgs e)
        {
            Msk_TextBox.UseSystemPasswordChar = false;
            Msk_TextBox.PasswordChar = '\0';
            Lbl_Conteudo.Text = "";
            Msk_TextBox.Mask = "";
            Lbl_MascaraAtiva.Text = "Telefone";
            Msk_TextBox.Text = "";
            Msk_TextBox.Tag = "";
            Msk_TextBox.Focus();
        }

        private void Btn_VerConteudo_Click(object sender, EventArgs e)
        {
            Lbl_Conteudo.Text = Msk_TextBox.Text;
        }

        private void Btn_CEP_Click(object sender, EventArgs e)
        {
            Msk_TextBox.UseSystemPasswordChar = false;
            Lbl_Conteudo.Text = "";
            Msk_TextBox.Mask = "0000-000";
            Lbl_MascaraAtiva.Text = Msk_TextBox.Mask;
            Msk_TextBox.Text = "";
            Msk_TextBox.Focus();
        }

        private void Btn_Moeda_Click(object sender, EventArgs e)
        {
            Msk_TextBox.PasswordChar = '\0';
            Msk_TextBox.UseSystemPasswordChar = false;
            Lbl_Conteudo.Text = "";
            Msk_TextBox.Mask = "";
            Lbl_MascaraAtiva.Text = "Moeda";
            Msk_TextBox.Text = "";
            Msk_TextBox.Focus();
        }

        private void Msk_TextBox_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void Msk_TextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Msk_TextBox.Text.Replace("/", "").Replace(":", "").Trim())) return;

            if (Msk_TextBox.Mask == "00:00")
            {
                if (!DateTime.TryParseExact(Msk_TextBox.Text, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                {
                    MessageBox.Show("Digite uma hora válida!");
                    e.Cancel = true;
                }
            }
            if (Msk_TextBox.Mask == "00/00/0000")
            {
                if (!DateTime.TryParseExact(Msk_TextBox.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                {
                    MessageBox.Show("Digite uma data válida!");
                    e.Cancel = true;
                }
            }
            if (Lbl_MascaraAtiva.Text == "Senha")
            {
                string senha = Msk_TextBox.Text;
                bool temTamanhoMinimo = senha.Length >= 8;
                bool temMaiuscula = senha.Any(char.IsUpper);
                bool temMinuscula = senha.Any(char.IsLower);
                bool temNumero = senha.Any(char.IsDigit);
                bool temEspecial = senha.Any(ch => !char.IsLetterOrDigit(ch));

                if (!temTamanhoMinimo || !temMaiuscula || !temMinuscula || !temNumero || !temEspecial)
                {
                    MessageBox.Show("A senha deve ter pelo menos 8 caracteres, incluindo letras maiúsculas, minúsculas, números e caracteres especiais.");
                    e.Cancel = true;
                }
            }

        }

        private void Msk_TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Lbl_MascaraAtiva.Text == "Moeda")
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                    return;
                }
                e.Handled = true;

                string numeros = Regex.Replace(Msk_TextBox.Text, @"[^\d]", "");

                if (e.KeyChar == (char)Keys.Back)
                {
                    if (numeros.Length > 0) numeros = numeros.Substring(0, numeros.Length - 1);
                }
                else
                {
                    numeros += e.KeyChar;
                }

                if (double.TryParse(numeros, out double resultado))
                {
                    Msk_TextBox.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C2}", resultado / 100);
                }
                Msk_TextBox.SelectionStart = Msk_TextBox.Text.Length;

            }
            if (Lbl_MascaraAtiva.Text == "Telefone")
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                    return;
                }

                e.Handled = true;

                string numeros = Regex.Replace(Msk_TextBox.Text, @"[^\d]", "");

                if (e.KeyChar == (char)Keys.Back)
                {
                    if (numeros.Length > 0) numeros = numeros.Substring(0, numeros.Length - 1);
                }
                else
                {
                    if (numeros.Length < 11) numeros += e.KeyChar;
                }
                if (numeros.Length > 0)
                {
                    long.TryParse(numeros, out long num);
                    if (numeros.Length <= 9)
                        Msk_TextBox.Text = string.Format("{0:#####-####}", num);
                    else
                        Msk_TextBox.Text = string.Format("{0:(##) #####-####}", num);
                }
                else
                {
                    Msk_TextBox.Text = "";
                }
                Msk_TextBox.SelectionStart = Msk_TextBox.Text.Length;
            }
        }

        private void Btn_Senha_Click(object sender, EventArgs e)
        {
            Msk_TextBox.UseSystemPasswordChar = true;
            Lbl_Conteudo.Text = "";
            Msk_TextBox.Mask = "";
            Lbl_MascaraAtiva.Text = "Senha";
            Msk_TextBox.Text = "";
            Msk_TextBox.Focus();
        }
    }
}
