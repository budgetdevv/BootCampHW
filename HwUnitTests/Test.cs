using System;
using FluentAssertions;
using HWCuzImBored;
using Xunit;
using Xunit.Abstractions;

namespace HwUnitTests
{
    public class Test
    {
        private readonly ITestOutputHelper TestConsole;

        public Test(ITestOutputHelper testConsole)
        {
            TestConsole = testConsole;
        }

        [Fact]
        public void ShouldBeSorted()
        {
            var Arr = new int[4] {1, 8, 2, 5};
            
            HW.QuickSort(Arr);

            Arr.Should().BeInAscendingOrder();
        }

        [Fact]
        public void AppendStartShouldWork()
        {
            var Arr = new int[4] { 1, 2, 5, 8 };
            
            HW.AppendStart(ref Arr, 69);

            Arr.Should().HaveElementAt(0, 69);
        }
        
        [Fact]
        public void AppendEndShouldWork()
        {
            var Arr = new int[4] { 1, 2, 5, 8 };
            
            HW.AppendEnd(ref Arr, 69);

            Arr.Should().HaveElementAt(4, 69);
        }

        [Fact]
        public void InsertShouldWork()
        {
            var Arr = new int[4] { 1, 2, 5, 8 };
            
            HW.InsertAt(ref Arr, 69, 2);
            
            Arr.Should().HaveElementAt(2, 69);
        }

        [Fact]
        public void RemoveStartShouldWork()
        {
            var Arr = new int[4] { 1, 2, 5, 8 };
            
            HW.RemoveStart(ref Arr);

            Arr.Should().BeEquivalentTo(new int[] { 2, 5, 8 });
        }
        
        [Fact]
        public void RemoveEndShouldWork()
        {
            var Arr = new int[4] { 1, 2, 5, 8 };
            
            HW.RemoveEnd(ref Arr);

            Arr.Should().BeEquivalentTo(new int[] { 1, 2, 5 });
        }
        
        [Fact]
        public void RemoveAtShouldWork()
        {
            var Arr = new int[4] { 1, 2, 5, 8 };
            
            HW.RemoveAt(ref Arr, 1);

            Arr.Should().BeEquivalentTo(new int[] { 1, 5, 8 });
        }
        
        [Fact]
        public void RemoveAtShouldWorkWithSizeTwo()
        {
            var Arr = new int[2] { 1, 2 };
            
            HW.RemoveAt(ref Arr, 1);

            Arr.Should().BeEquivalentTo(new int[] { 1 });
        }
        
        [Fact]
        public void RemoveAtShouldWorkWithSizeOne()
        {
            var Arr = new int[1] { 1 };
            
            HW.RemoveAt(ref Arr, 0);

            Arr.Should().BeEquivalentTo(Array.Empty<int>());
        }
        
        [Fact]
        public void RemoveAtShouldThrowIfOutOfBounds()
        {
            var Arr = new int[1] { 1 };
            
            HW.RemoveAt(ref Arr, 0);

            Arr.Should().BeEquivalentTo(Array.Empty<int>());
        }

        [Fact]
        public void LogMeInPls()
        {
            HW.LogMeInHamachi("BudgetDevv", "Dumbass").Should().BeTrue();

            HW.LogMeInHamachi("feLix", "tRoll").Should().BeFalse();

            HW.LogMeInHamachi("feLix", "Troll").Should().BeTrue();
        }
    }
}