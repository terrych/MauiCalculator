using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiCalculator.Lib.Test
{
    public class CalculationTests
    {
        private int _precision = 4;

        [Fact]
        public void SevenIs7()
        {
            var calc = new Calculator();
            calc.Append("7");
            Assert.Equal(7, double.Parse(calc.Calculate()), _precision);
        }

        [Fact]
        public void TwoPlusTwoIsFour()
        {
            var calc = new Calculator();
            calc.Append("2+2");
            Assert.Equal(4, double.Parse(calc.Calculate()), _precision);
        }

        [Fact]
        public void OneMinusTwoIsMinus1()
        {
            var calc = new Calculator();
            calc.Append("1-2");
            Assert.Equal(-1, double.Parse(calc.Calculate()), _precision);
        }

        [Fact]
        public void TwoPlus3Times2Is8()
        {
            var calc = new Calculator();
            calc.Append("2+3×2");
            Assert.Equal(8, double.Parse(calc.Calculate()), _precision);
        }

        [Fact]
        public void Br2Plus3ClBrTimes2Is10()
        {
            var calc = new Calculator();
            calc.Append("(2+3)×2");
            Assert.Equal(10, double.Parse(calc.Calculate()), _precision);
        }

        [Fact]
        public void Br2Pt5Minus3ClbrTimes2Plus1Divideby6IsNegative5Sixths()
        {
            var calc = new Calculator();
            calc.Append("(2.5-3)×2+1÷6");
            Assert.Equal((double)(-5)/6, double.Parse(calc.Calculate()), _precision);
        }

        [Fact]
        public void OnePlusBr2Pt5Minus3ClbrTimes2Plus1Divideby6Is1Sixth()
        {
            var calc = new Calculator();
            calc.Append("1+(2.5-3)×2+1÷6");
            Assert.Equal((double)(1) / 6, double.Parse(calc.Calculate()), _precision);
        }

        [Fact]
        public void SimplifiesTo1PlusBr5Times7Add1ClbrDivideBr1Plus4Times2ClBr()
        {
            var calc = new Calculator();
            calc.Append("1+((2+3)×(1+2×3)+1)÷(1+4×2)");
            Assert.Equal(5, double.Parse(calc.Calculate()), _precision);
        }

        [Fact]
        public void OnePlus2Plus3Plus4TimesBr5Plus6Clbr()
        {
            var calc = new Calculator();
            calc.Append("1+2+3+4×(5+6)");
            Assert.Equal(50, double.Parse(calc.Calculate()), _precision);
        }

        [Fact]
        public void ThisWouldBeQuiteLongToWriteOut()
        {
            var calc = new Calculator();
            calc.Append("1+(((2+3)×(1+3×2)-15÷(1+2×2))÷(2+2)×2-1)÷5");
            Assert.Equal(4, double.Parse(calc.Calculate()), _precision);
        }
    }
}
