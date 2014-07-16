using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;

namespace ProjectEuler
{
    [TestFixture]
    public class ConvertToWordsTest
    {
        [RowTest]
        [Row(0, "zero")]
        [Row(1, "one")]
        [Row(-1, "negative one")]
        [Row(10, "ten")]
        [Row(15, "fifteen")]
        [Row(20, "twenty")]
        [Row(21, "twenty-one")]
        [Row(32, "thirty-two")]
        [Row(43, "forty-three")]
        [Row(54, "fifty-four")]
        [Row(65, "sixty-five")]
        [Row(76, "seventy-six")]
        [Row(87, "eighty-seven")]
        [Row(98, "ninety-eight")]
        [Row(99, "ninety-nine")]
        [Row(100, "one hundred")]
        [Row(101, "one hundred and one")]
        [Row(221, "two hundred and twenty-one")]
        [Row(999, "nine hundred and ninety-nine")]
        [Row(1000, "one thousand")]
        [Row(123456, "one hundred and twenty-three thousand, four hundred and fifty-six")]
        [Row(1234567, "one million, two hundred and thirty-four thousand, five hundred and sixty-seven")]
        [Row(987654321, "nine hundred and eighty-seven million, six hundred and fifty-four thousand, three hundred and twenty-one")]
        [Row(int.MaxValue, "two billion, one hundred and forty-seven million, four hundred and eighty-three thousand, six hundred and forty-seven")]
        [Row(int.MinValue, "negative two billion, one hundred and forty-seven million, four hundred and eighty-three thousand, six hundred and forty-eight")]
        [Row(long.MaxValue, "nine quintillion, two hundred and twenty-three quadrillion, three hundred and seventy-two trillion, thirty-six billion, eight hundred and fifty-four million, seven hundred and seventy-five thousand, eight hundred and seven")]
        public void RowTests(long number, string expectedResult)
        {
            StringAssert.AreEqualIgnoreCase(number.ConvertToWords(), expectedResult);
        }
    }
}
