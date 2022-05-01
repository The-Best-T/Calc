using CalculateMethods;
using Contracts;
using System.Text;

namespace Calc
{
    public class Calculator : ICalculator
    {
        private readonly List<double> _numbers;
        private readonly List<char> _signs;
        private ICalculateMethod _method;

        public Calculator()
        {
            _numbers = new();
            _signs = new();
            _method = new ReversePolandMethod();
        }
        public double Calculate(string text)
        {
            SplitText(text);

            double result = Calculate();

            _numbers.Clear();
            _signs.Clear();

            return result;
        }

        private void SplitText(string text)
        {
            StringBuilder builder = new StringBuilder();
            foreach (char c in text)
            {
                if ("+-*/".Contains(c))
                {
                    _signs.Add(c);
                    if (builder.Length != 0)
                    {
                        Double.TryParse(builder.ToString(), out double value);
                        _numbers.Add(value);
                        builder.Clear();
                    }
                }
                else
                    builder.Append(c);
            }
        }

        private double Calculate()
        {
            //Method
            return _method.Calculate(_numbers,_signs);
        }

        public int Round(string text)
        {
            SplitText(text);

            double result = Calculate();

            _numbers.Clear();
            _signs.Clear();

            return (int)Math.Round(result);
        }
    }
}
