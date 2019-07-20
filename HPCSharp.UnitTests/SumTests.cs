﻿using System;
using System.Numerics;
using HPCsharp.Algorithms;
using HPCsharp.ParallelAlgorithms;
using NUnit.Framework;

namespace HPCSharp.UnitTests
{
    [TestFixture]
    public sealed class SumTests
    {
        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void ShouldThrowOverflowExceptionLong(int whichTestCase)
        {
            long[] arrLong  = new long[] { 5, 7, 16, 3, Int64.MaxValue, 1 };

            if (whichTestCase == 0)
            {
                Assert.Throws<OverflowException>(() => arrLong.SumHpc());
            }
            else if (whichTestCase == 1)
            {
                Assert.Throws<OverflowException>(() => arrLong.SumSse());
            }
            else if (whichTestCase == 2)
            {
                ulong[] arrLong1 = new ulong[] { 5, 7, 16, 4, 2, 0, UInt64.MaxValue, 3, 1 };
                Assert.Throws<OverflowException>(() => arrLong1.SumCheckedSse());
            }
        }
        [Test]
        [TestCase(0)]
        [TestCase(1)]
        public void ShouldNotThrowOverflowExceptionLong(int whichTestCase)
        {
            long[] arrLong = new long[] { 5, 7, 16, 3, Int64.MaxValue, 1 };

            if (whichTestCase == 1)
            {
                // This test demonstrates that SSE doesn't throw overflow exception
                long[] arrLong1 = new long[] { 5, 7, 16, 4, 2, 0, Int64.MaxValue, 3, 1 };
                Assert.DoesNotThrow(() => arrLong1.SumSse());
            }
            else if (whichTestCase == 2)
            {
                ulong[] arrLong1 = new ulong[] { 5, 7, 16, 4, 2, 0, UInt64.MaxValue, 3, 1 };
                Assert.Throws<OverflowException>(() => arrLong1.SumCheckedSse());
            }
        }
        [Test]
        [TestCase(Int64.MaxValue, 1)]
        public void CorrectnessOfSumOfLongArrayToBigIntegerFaster(long input0, long input1)
        {
            long[] arrLong = new long[] { input0, input1 };
            BigInteger result = new BigInteger(input0) + new BigInteger(input1);

            Assert.AreEqual(result, arrLong.SumToBigIntegerFaster());
        }
    }
}
