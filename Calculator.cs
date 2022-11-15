using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiCalculator
{
    public class Calculator
    {
        private string _toCompute;
        private string _result;
        private bool _clearOnNextClick;

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

        public string Append(string value)
        {
            _toCompute += value;
            return _toCompute;
        }

        public string Backspace()
        {
            _toCompute = _toCompute.Remove(_toCompute.Length - 1);
            return _toCompute;
        }

        public string Clear()
        {
            _toCompute = "";
            return _toCompute;
        }

        public string Calculate() 
        {
            bool balancedBrackets = CheckBrackets(_toCompute);
            if (!balancedBrackets)
            {
                _toCompute = string.Empty;
            }

            return _toCompute;
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
