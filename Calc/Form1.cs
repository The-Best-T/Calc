using Contracts;
using System.Globalization;
using System.Media;

namespace Calc
{
    public partial class CalcForm : Form
    {
        private bool _point = false;
        private readonly ICalculator _calculator;
        private readonly string[] _sounds;
        public CalcForm()
        {
            InitializeComponent();
            _calculator = new Calculator();
            _sounds = Directory.GetFiles(Directory.GetCurrentDirectory() + @$"\sounds\");
        }

        private void CalcForm_Load(object sender, EventArgs e)
        {

        }

        private void ActionSound()
        {
            if (checkBoxMute.Checked)
                return;
            int number = new Random().Next(_sounds.Length);
            SoundPlayer player = new SoundPlayer(_sounds[number]);
            player.Play();
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
            if (text.Last() == ',') _point = false;
            textBoxResult.Text = text.Remove(text.Length - 1);
        }

        private void buttonAllClear_Click(object sender, EventArgs e)
        {
            textBoxResult.Clear();
            _point = false;
        }

        private async void buttonRound_Click(object sender, EventArgs e)
        {
            ActionSound();
            string text = textBoxResult.Text;
            if (String.IsNullOrEmpty(text))
                return;
            try
            {
                if ("+-*/,".Contains(text.Last())) text += '0';
                if (text[0] == '-') text = '0' + text;

                int result = await Task.Run(() => _calculator.Round(text));
                ResultActions(text, result);
            }
            catch (Exception ex)
            {
                textBoxResult.Text = "Error";
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

            text += ',';
            _point = true;

            textBoxResult.Text = text;
        }

        private void ResultActions(string text,double result)
        {
            string resultString = result.ToString("F9", CultureInfo.CreateSpecificCulture("ru-RU"));
            resultString = resultString.TrimEnd('0');
            if (resultString.Last()==',') 
                resultString=resultString.Remove(resultString.Length-1);
            listBoxHistory.Items.Add(text + '=');
            listBoxHistory.Items.Add(resultString);

            _point = false;
            textBoxResult.Text = resultString;
        }
        private async void buttonResult_Click(object sender, EventArgs e)
        {
            ActionSound();
            string text = textBoxResult.Text;
            if (String.IsNullOrEmpty(text))
                return;
            try
            {
                if ("+-*/,".Contains(text.Last())) text += '0';
                if (text[0] == '-') text = '0' + text;
                double result = await Task.Run(() => _calculator.Calculate(text));
                ResultActions(text, result);
            }
            catch (Exception ex)
            {
                textBoxResult.Text = "Error";
            }
        }

        private void listBoxHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            string? text = listBoxHistory.SelectedItem.ToString();
            if (string.IsNullOrEmpty(text))
                return;
            text = text.TrimEnd('=');
            foreach(var symb in text)
                if (symb==',') _point = true;
            textBoxResult.Text = text;
        }

        private void button10_Click(object sender, EventArgs e)
        {

        }
    }
}