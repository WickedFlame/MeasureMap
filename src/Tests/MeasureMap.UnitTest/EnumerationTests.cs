using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using NUnit.Framework;

namespace MeasureMap.UnitTest
{
#pragma warning disable L123
    public class EnumerationTests
    {
        [Test]
        public void Enumeration_Id()
        {
            TestEnum.Test.Id.Should().Be(1);
        }

        [Test]
        public void Enumeration_Name()
        {
            TestEnum.Test.Name.Should().Be("Test");
        }

        [Test]
        public void Enumeration_ToString()
        {
            TestEnum.Test.ToString().Should().Be("Test");
        }

        [Test]
        public void Enumeration_ToString_Name()
        {
            TestEnum.Test.ToString().Should().BeSameAs(TestEnum.Test.Name);
        }

        [Test]
        public void Enumeration_DirectCast()
        {
            ((string)TestEnum.Test).Should().BeEquivalentTo(TestEnum.Test.Name);
        }

        [Test]
        public void Enumeration_ToLower()
        {
            TestEnum.Test.ToLower().Should().Be("test");
        }

        [Test]
        public void Enumeration_CompareTo()
        {
            TestEnum.Test.CompareTo(TestEnum.Test).Should().Be(0);
        }

        [Test]
        public void Enumeration_CompareTo_Unequal()
        {
            TestEnum.Test.CompareTo(TestEnum.Two).Should().Be(-1);
        }

        [Test]
        public void Enumeration_CompareTo_NewSame()
        {
            TestEnum.Test.CompareTo(new TestEnum(1, "Test")).Should().Be(0);
        }

        [Test]
        public void Enumeration_CompareTo_UnequalId()
        {
            TestEnum.Test.CompareTo(new TestEnum(2, "Test")).Should().Be(-1);
        }

        [Test]
        public void Enumeration_CompareTo_UnequalName()
        {
            TestEnum.Test.CompareTo(new TestEnum(1, "a")).Should().Be(-1);
        }

        [Test]
        public void Enumeration_Compare()
        {
            TestEnum.Compare(TestEnum.Test, TestEnum.Test).Should().Be(0);
        }

        [Test]
        public void Enumeration_Compare_Unequal()
        {
            TestEnum.Compare(TestEnum.Test, TestEnum.Two).Should().Be(-1);
        }

        [Test]
        public void Enumeration_Compare_NewInstance()
        {
            TestEnum.Compare(TestEnum.Test, new TestEnum(1, "Test")).Should().Be(0);
        }


        [Test]
        public void Enumeration_Equals()
        {
            TestEnum.Test.Equals(TestEnum.Test).Should().BeTrue();
        }

        [Test]
        public void Enumeration_Equals_Unequal()
        {
            TestEnum.Test.Equals(TestEnum.Two).Should().BeFalse();
        }

        [Test]
        public void Enumeration_Equals_NewInstance()
        {
            TestEnum.Test.Equals(new TestEnum(1, "Test")).Should().BeTrue();
        }

        [Test]
        public void Enumeration_GetHashCode()
        {
            TestEnum.Test.GetHashCode().Should().NotBe(0);
        }

        [Test]
        public void Enumeration_Equals_Operator()
        {
            (TestEnum.Test == TestEnum.Test).Should().BeTrue();
        }

        [Test]
        public void Enumeration_Equals_Operator_Invert()
        {
            (TestEnum.Test == TestEnum.Two).Should().BeFalse();
        }

        [Test]
        public void Enumeration_Unequals_Operator()
        {
            (TestEnum.Test != TestEnum.Two).Should().BeTrue();
        }

        [Test]
        public void Enumeration_Unequals_Operator_Invert()
        {
            (TestEnum.Test != TestEnum.Test).Should().BeFalse();
        }

        [Test]
        public void Enumeration_Smaller_Operator()
        {
            (TestEnum.Test < TestEnum.Two).Should().BeTrue();
        }

        [Test]
        public void Enumeration_Smaller_Operator_Invert()
        {
            (TestEnum.Two < TestEnum.Test).Should().BeFalse();
        }

        [Test]
        public void Enumeration_SmallerEquals_Operator()
        {
            (TestEnum.Test <= TestEnum.Two).Should().BeTrue();
        }

        [Test]
        public void Enumeration_SmallerEquals_Operator_Invert()
        {
            (TestEnum.Two <= TestEnum.Test).Should().BeFalse();
        }

        [Test]
        public void Enumeration_SmallerEquals_Operator_Same()
        {
            (TestEnum.Test <= TestEnum.Test).Should().BeTrue();
        }

        [Test]
        public void Enumeration_Greater_Operator()
        {
            (TestEnum.Two > TestEnum.Test).Should().BeTrue();
        }

        [Test]
        public void Enumeration_Greater_Operator_Invert()
        {
            (TestEnum.Test > TestEnum.Two).Should().BeFalse();
        }

        [Test]
        public void Enumeration_GreaterEquals_Operator()
        {
            (TestEnum.Two >= TestEnum.Test).Should().BeTrue();
        }

        [Test]
        public void Enumeration_GreaterEquals_Operator_Invert()
        {
            (TestEnum.Test >= TestEnum.Two).Should().BeFalse();
        }

        [Test]
        public void Enumeration_GreaterEquals_Operator_Same()
        {
            (TestEnum.Test >= TestEnum.Test).Should().BeTrue();
        }

        public class TestEnum : Enumeration
        {
            public static readonly TestEnum Test = new TestEnum(1, "Test");

            public static readonly TestEnum Two = new TestEnum(2, "Two");

            public TestEnum(int id, string name) : base(id, name)
            {
            }
        }
    }
#pragma warning restore L123
}
