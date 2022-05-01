namespace Calc
{
    public partial class CalcForm : Form
    {
        public CalcForm()
        {
            InitializeComponent();
        }

        private void CalcForm_Load(object sender, EventArgs e)
        {

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
        private void buttonMinus_Click(object sender, EventArgs e)
        {
            //Proverka
            textBoxResult.Text += '-';
        }

        private void buttonMultiply_Click(object sender, EventArgs e)
        {
            //Proverka
            textBoxResult.Text += '*';
        }
        private void buttonDiv_Click(object sender, EventArgs e)
        {
            //Proverka
            textBoxResult.Text += '/';
        }

        private void buttonPlus_Click(object sender, EventArgs e)
        {
            //Proverka
            textBoxResult.Text += '+';
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

        private void buttonPI_Click(object sender, EventArgs e)
        {
            //Proverka
            textBoxResult.Text += "PI";
        }

        private void buttonChangeSign_Click(object sender, EventArgs e)
        {
            //Proverka
            textBoxResult.Text = '-'+textBoxResult.Text;
        }

        private void buttonPoint_Click(object sender, EventArgs e)
        {
            //Proverka
            textBoxResult.Text += '.';
        }

        private void buttonResult_Click(object sender, EventArgs e)
        {
            string text = textBoxResult.Text;
            if (string.IsNullOrEmpty(text))
                return;
            textBoxResult.Clear();
            listBoxHistory.Items.Add(text);
        }

        private void textBoxResult_TextChanged(object sender, EventArgs e)
        {

        }
    }
}