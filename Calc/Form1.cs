using Contracts;
using System.Media;

namespace Calc
{
    public partial class CalcForm : Form
    {
        private bool _point = false;
        private ICalculator _calculator;

        public CalcForm()
        {
            InitializeComponent();
            _calculator = new Calculator();
        }

        private void CalcForm_Load(object sender, EventArgs e)
        {

        }

        private void ActionSound(object sender,EventArgs e)
        {
            int number=new Random().Next(6);
            SoundPlayer sound=new SoundPlayer(Directory.GetCurrentDirectory()+@$"\sounds\sound{number}.wav");
            sound.Play();
        }
        private void button0_Click(object sender, EventArgs e)
        {
            textBoxResult.Text += '0';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBoxResult.Text += '1';
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBoxResult.Text += '2';
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBoxResult.Text += '3';
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBoxResult.Text += '4';
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBoxResult.Text += '5';
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBoxResult.Text += '6';
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBoxResult.Text += '7';
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBoxResult.Text += '8';
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBoxResult.Text += '9';
        }

        private string AddSign(string text, char sign)
        {
            char last = text.Last();
            if (last == '.')
            {
                text += '0';

            }
            else if ("+-*/".Contains(last))
            {
                text = text.Remove(text.Length - 1);
            }
            text += sign;
            _point = false;
            return text;
        }

        private void buttonMinus_Click(object sender, EventArgs e)
        {
            string text = textBoxResult.Text;
            if (string.IsNullOrEmpty(text))
            {
                textBoxResult.Text = "0-";
                return;
            }
            string newText = AddSign(text, '-');
            textBoxResult.Text = newText;
        }

        private void buttonMultiply_Click(object sender, EventArgs e)
        {
            string text = textBoxResult.Text;
            if (string.IsNullOrEmpty(text))
            {
                textBoxResult.Text = "0*";
                return;
            }
            string newText = AddSign(text, '*');
            textBoxResult.Text = newText;
        }
        private void buttonDiv_Click(object sender, EventArgs e)
        {
            string text = textBoxResult.Text;
            if (string.IsNullOrEmpty(text))
            {
                textBoxResult.Text = "0/";
                return;
            }
            string newText = AddSign(text, '/');
            textBoxResult.Text = newText;
        }

        private void buttonPlus_Click(object sender, EventArgs e)
        {
            string text = textBoxResult.Text;
            if (string.IsNullOrEmpty(text))
            {
                textBoxResult.Text = "0+";
                return;
            }
            string newText = AddSign(text, '+');
            textBoxResult.Text = newText;
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            string text = textBoxResult.Text;
            if (string.IsNullOrEmpty(text))
                return;
            textBoxResult.Text = text.Remove(text.Length - 1);
        }

        private void buttonAllClear_Click(object sender, EventArgs e)
        {
            textBoxResult.Clear();
        }

        private async void buttonRound_Click(object sender, EventArgs e)
        {
            string text=textBoxResult.Text;
            if (String.IsNullOrEmpty(text))
                return;
            try
            {
                int result = await Task.Run(() => _calculator.Round(text));
                listBoxHistory.Items.Add(text + '=');
                listBoxHistory.Items.Add(result);

                textBoxResult.Text=result.ToString();
            }
            catch (Exception ex)
            {
                textBoxResult.Text=ex.Message;
            }
        }

        private void buttonChangeSign_Click(object sender, EventArgs e)
        {
            string text = textBoxResult.Text;
            if (string.IsNullOrEmpty(text))
                return;

            string newText = ChangeSign(text);
            textBoxResult.Text = newText;
        }
        private string ChangeSign(string text)
        {
            if (text[0] != '-')
                text = '-' + text;
            else
                text = text.Substring(1);
            return text;
        }

        private void buttonPoint_Click(object sender, EventArgs e)
        {
            if (_point)
                return;

            string text = textBoxResult.Text;
            if (string.IsNullOrEmpty(text))
            {
                text += '0';
            }
            else if ("+-*/".Contains(text.Last()))
            {
                text += '0';
            }

            text += '.';
            _point = true;

            textBoxResult.Text = text;
        }

        private async void buttonResult_Click(object sender, EventArgs e)
        {
            string text = textBoxResult.Text;
            if (String.IsNullOrEmpty(text))
                return;
            try
            {
                double result = await Task.Run(() => _calculator.Calculate(text));
                listBoxHistory.Items.Add('='+text);
                listBoxHistory.Items.Add(result);

                textBoxResult.Text = result.ToString();
            }
            catch (Exception ex)
            {
                textBoxResult.Text = ex.Message;
            }
        }

        private void listBoxHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            string? text = listBoxHistory.SelectedItem.ToString();
            if (string.IsNullOrEmpty(text))
                return;
            text = text.TrimStart('=');
            textBoxResult.Text = text;
        }
    }
}