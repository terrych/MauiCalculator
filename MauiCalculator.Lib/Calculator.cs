using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiCalculator.Lib
{

    public class Calculator
    {
        private string _toCompute;
        private string _result;
        private bool _clearOnNextClick;

        public string InvalidInput { get { return "Invalid Input"; } }

        public Calculator()
        {
            _toCompute = string.Empty;
            _result = string.Empty;
            _clearOnNextClick = false;
        }

        public string GetInput()
        {
            return _toCompute;
        }

        public string GetOutput()
        {
            return _result;
        }

        public string Append(string value)
        {
            if (_clearOnNextClick)
            {
                Clear();
                _clearOnNextClick = false;
            }
            _toCompute += value;
            return _toCompute;
        }

        public string Backspace()
        {
            if (_clearOnNextClick)
            {
                Clear();
                _clearOnNextClick = false;
            }

            if (!string.IsNullOrEmpty(_toCompute))
            {
                _toCompute = _toCompute.Remove(_toCompute.Length - 1);
            }

            return _toCompute;
        }

        public string Clear()
        {
            _toCompute = "";
            return _toCompute;
        }

        public string Calculate()
        {
            if (_clearOnNextClick)
            {
                Clear();
                _clearOnNextClick = false;
            }
            if (string.IsNullOrEmpty(_toCompute)) return "0";

            bool balancedBrackets = CheckBrackets(_toCompute);
            if (!balancedBrackets)
            {
                _result = InvalidInput;
                _toCompute= string.Empty;
                _clearOnNextClick = true;
                return _result;
            }

            var calculation = new OperatorNode(_toCompute);

            if (!string.IsNullOrEmpty(calculation.Error)) 
            {
                _clearOnNextClick = true;
                return calculation.Error; 
            }
            if (calculation.NodeValue.HasValue) 
                return calculation.NodeValue.ToString();

            return "Node value null";
        }

        private bool CheckBrackets(string toCompute)
        {
            Stack<char> stack = new Stack<char>();
            foreach (char item in toCompute)
            {
                if (item == '(') stack.Push(item);
                if (item == ')')
                {
                    if (stack.Any() && stack.Peek() == '(')
                    {
                        stack.Pop();
                    }
                    else
                    {
                        _clearOnNextClick = true;
                        return false;
                    }
                }
            }

            return !stack.Any();
        }
    }
}
