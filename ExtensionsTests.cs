using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;

namespace ProjectEuler
{
    [TestFixture]
    public class ExtensionsTests
    {
        [RowTest]
        [Row(1, true)]
        [Row(2, false)]
        [Row(3, true)]
        [Row(4, false)]
        [Row(5, true)]
        [Row(585, true)]
        public void TestIsPalindromicBase2(int number, bool isPalindromic)
        {
            Assert.AreEqual(isPalindromic, number.IsPalindromicBase2());
        }
    }
}
