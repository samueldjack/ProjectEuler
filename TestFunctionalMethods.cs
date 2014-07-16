using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;

namespace ProjectEuler
{
    [TestFixture]
    class TestFunctionalMethods
    {
        [Test]
        public void TestSlice2()
        {
            var slices = new[] {1, 2, 3, 4, 5, 6, 7, 8, 9}.Slice(2).ToList();
            Assert.AreEqual(5, slices.Count);
            Assert.AreEqual(2, slices[0].Count());
            Assert.AreEqual(1, slices[4].Count());
            Assert.AreEqual(9, slices[4].First());
        }

        [Test]
        public void TestSlice1()
        {
            var slices = new[] {1, 2, 3, 4}.Slice(1).ToList();

            Assert.AreEqual(4, slices.Count);
            Assert.AreEqual(1, slices[0].Count());
            Assert.AreEqual(1, slices[0].First());
        }

        [Test]
        public void TestSliceEmpty()
        {
            var slices = Enumerable.Empty<int>().Slice(1).ToList();

            Assert.AreEqual(0, slices.Count);
        }

[Test]
public void SliceCakes()
{
    string[] cakeData =
        {
            "Mini Rolls Selection",
            "Cabury",
            "2.39",
            "6 Chocolate Flavour Cup Cakes",
            "Fabulous Bakin' Boys",
            "1.25",
            "Galaxy Cake Bars 5 Pack",
            "McVities",
            "1.00",
            "Apple Slices 6pk",
            "Mr Kipling",
            "1.39"
        };

    var cakes = cakeData
        .Slice(3)
        .Select(slice => new
                 {
                     Cake = slice.ElementAt(0),
                     Baker = slice.ElementAt(1),
                     Price = slice.ElementAt(2)
                 });

    foreach (var cake in cakes)
    {
        Console.WriteLine(cake);
    }
}
    }
}
