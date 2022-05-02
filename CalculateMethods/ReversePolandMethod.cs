using Contracts;

namespace CalculateMethods
{
    public class ReversePolandMethod : ICalculateMethod
    {
        private readonly List<string> _reversePolandOrder;
        private List<double> _numbers;
        private List<char> _signs;
        private readonly Stack<double> _numbersStack;
        private readonly Stack<char> _signsStack;
        private readonly Dictionary<char, int> _priorities;
        public ReversePolandMethod()
        {
            _reversePolandOrder = new();
            _numbers = new();
            _signs = new();
            _numbersStack = new();
            _signsStack = new();
            _priorities = new()
            {
                { '+', 1 },
                { '-', 1 },
                { '*', 2 },
                { '/', 2 }
            };
        }
        public double Calculate(List<double> numbers, List<char> signs)
        {
            _numbers = numbers;
            _signs = signs;
            ToOrder();
            return Answer();
        }

        private void ToOrder()
        {
            _reversePolandOrder.Clear();
            _signsStack.Clear();

            int size = _numbers.Count;
            for (int i = 0; i < size - 1; i++)
            {
                _reversePolandOrder.Add($"{i}");

                while (_signsStack.Count > 0 &&
                    _priorities[_signsStack.Peek()] >= _priorities[_signs[i]])
                {
                    _reversePolandOrder.Add(_signsStack.Pop().ToString());
                }
                _signsStack.Push(_signs[i]);
            }

            _reversePolandOrder.Add($"{size-1}");
            while (_signsStack.TryPop(out char value))
            {
                _reversePolandOrder.Add(value.ToString());
            }
        }

        private double Answer()
        {
            _numbersStack.Clear();
            foreach (var pos in _reversePolandOrder)
                if ("+-*/".Contains(pos))
                {
                    double x = _numbersStack.Pop();
                    double y = _numbersStack.Pop();
                    double result = 0;
                    switch (pos)
                    {
                        case "+":
                            result = x + y;
                            break;
                        case "-":
                            result = y - x;
                            break;
                        case "*":
                            result = x * y;
                            break;
                        case "/":
                            result = y / x;
                            break;
                    }
                    _numbersStack.Push(result);
                }
                else
                {
                    _numbersStack.Push(_numbers[Int32.Parse(pos)]);
                }
            return _numbersStack.Pop();
        }
    }
}
