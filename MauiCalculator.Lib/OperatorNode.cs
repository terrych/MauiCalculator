namespace MauiCalculator.Lib
{
    internal class OperatorNode // should probably test this directly instead of calculator, would then need to change visibility
    {
        public string Calculation { get; set; }
        public OperatorType OperatorType { get; private set; }
        public OperatorNode Left { get; private set; }
        public OperatorNode Right { get; private set; }
        public double? NodeValue { get; set; }

        public OperatorNode(string input)
        {
            Calculation = input;
            PopulateProperties(input);
            NodeValue = ComputeValue();
        }

        /// <summary>
        /// Dfs iteration. 
        /// Using recursion since I can't see an obvious way 
        /// of using a stack since I need to return the child 
        /// operation values to the parent without adding a parent property.
        /// </summary>
        protected double ComputeValue()
        {
            if (NodeValue == null)
            {
                switch (OperatorType)
                {
                    case OperatorType.None:
                        return Left.ComputeValue();
                    case OperatorType.Plus:
                        return Left.ComputeValue() + Right.ComputeValue();
                    case OperatorType.Minus:
                        return Left.ComputeValue() - Right.ComputeValue();
                    case OperatorType.Multiply:
                        return Left.ComputeValue() * Right.ComputeValue();
                    case OperatorType.Divide:
                        return Left.ComputeValue() / Right.ComputeValue();
                    default:
                        return 0; // should never happen
                }
            }

            return NodeValue.Value;
        }

        private void PopulateProperties(string input)
        {
            int? splitIndex = FindOperatorIndex(input);

            if (splitIndex.HasValue)
            {
                SplitAtIndex(input, splitIndex);
            }
            else if (input.Any() && input[0] == '(' && input[input.Length - 1] == ')') // should be equivalent to only first condition if everything is behaving since first if block should capture any case with an open bracket at start and a closing bracket at end that doesn't match
            {
                input = input.Substring(1, input.Length-2);
                OperatorType = OperatorType.None;
                Left = new OperatorNode(input);
            }
            else
            {
                OperatorType = OperatorType.None;
                if (string.IsNullOrEmpty(input)) input = "0";
                NodeValue = double.Parse(input);
            }
        }

        private void SplitAtIndex(string input, int? splitIndex)
        {
            switch (input[splitIndex.Value])
            {
                case '+':
                    OperatorType = OperatorType.Plus;
                    break;
                case '-':
                    OperatorType = OperatorType.Minus;
                    break;
                case '×':
                    OperatorType = OperatorType.Multiply;
                    break;
                case '÷':
                    OperatorType = OperatorType.Divide;
                    break;
                default:
                    break;
            }

            Left = new OperatorNode(input.Substring(0, splitIndex.Value));
            Right = new OperatorNode(input.Substring(splitIndex.Value + 1));
        }

        private int? FindOperatorIndex(string input)
        {
            int? toReturn = null;

            int bracketDepth = 0;
            bool breakOut = false;
            for (int i = 0; i < input.Length; i++)
            {
                if (breakOut) break;

                switch (input[i])
                {
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                    case '0':
                    case '.':
                        continue;
                    case '+':
                    case '-':
                        if (bracketDepth == 0)
                        {
                            toReturn = i;
                            breakOut = true;
                        }
                        break;
                    case '×':
                    case '÷':
                        if (bracketDepth == 0)
                        {
                            toReturn = i;
                        }
                        break;
                    case '(':
                        bracketDepth++;
                        break;
                    case ')':
                        if (bracketDepth > 0) bracketDepth--;
                        // else should never happen but probably would defensively handle if I were to spend more time on this
                        break;
                    default:
                        break;
                }
            }

            return toReturn;
        }
    }
}
