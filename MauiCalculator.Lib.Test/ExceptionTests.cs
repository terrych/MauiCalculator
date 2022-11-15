namespace MauiCalculator.Lib.Test
{
    public class ExceptionTests
    {
        [Fact]
        public void BackspacingOnEmptyCalculationStringDoesNotCauseException()
        {
            var calculator = new Calculator();
            try
            {
                calculator.Backspace();
            }
            catch (Exception e)
            {
                Assert.True(false, $"Exception was thrown. {e.GetType().Name}: {e.Message}");
            }
            Assert.True(true);
        }

        [Fact]
        public void UnbalancedBracketsCauseInvalidInput()
        {
            var calculator = new Calculator();
            calculator.Append("(");
            Assert.True(string.Equals(calculator.InvalidInput, calculator.Calculate()));
        }

        [Fact]
        public void CalculatorClearsOnNextClickWhenFlagSet()
        {
            var calculator = new Calculator();
            calculator.Append("(");
            calculator.Calculate(); // invalid input if UnbalancedBracketsCauseInvalidInput passes
            calculator.Append("7");
            Assert.True(string.Equals("7", calculator.GetInput()));
        }

        [Fact]
        public void MultipleBracketsParsedCorrectly()
        {
            var calculator = new Calculator();
            calculator.Append("((((7))))");
            Assert.True(string.Equals("7", calculator.Calculate()));
        }

        [Fact]
        public void MultipleBracketsParsedCorrectly7Plus3IsTen()
        {
            var calculator = new Calculator();
            calculator.Append("((((7+3))))");
            calculator.Calculate();
            Assert.True(string.Equals("10", calculator.Calculate()));
        }

        [Fact]
        public void MultipleBracketsParsedCorrectlyBr7Plus3ClbrTimes2Is20()
        {
            var calculator = new Calculator();
            calculator.Append("((((7+3)×2)))");
            calculator.Calculate();
            Assert.True(string.Equals("20", calculator.Calculate()));
        }

        [Fact]
        public void EmptyBracketsAreZero()
        {
            var calculator = new Calculator();
            calculator.Append("(((())))");
            calculator.Calculate();
            Assert.True(string.Equals("0", calculator.Calculate()));
        }

        [Fact]
        public void NoOperatorBetweenBracketAndNumberHandled()
        {
            var calculator = new Calculator();
            calculator.Append("(9+3)9");
            calculator.Calculate();
            Assert.True(string.Equals("Failed to parse input", calculator.Calculate()));
        }
    }
}