﻿namespace BookFx.Tests
{
    using BookFx.Functional;
    using FluentAssertions;
    using FsCheck;
    using FsCheck.Xunit;
    using Xunit;
    using static BookFx.Functional.F;
    using Unit = System.ValueTuple;

    public class ValueBoxTests
    {
        [Fact]
        public void Empty_Always_Empty() => ValueBox.Empty.Should().BeSameAs(ValueBox.Empty);

        [Fact]
        public void Create_Always_Empty() => Make.Value().Should().BeSameAs(ValueBox.Empty);

        [Property]
        public void CreateValue_NonNull_ValueIsValue(NonNull<object> value) =>
            Make.Value(value.Get).Get.Value.ValueUnsafe().Should().Be(value.Get);

        [Fact]
        public void CreateValue_Null_ValueIsUnit() =>
            Make.Value(null).Get.Value.ValueUnsafe().Should().BeOfType<Unit>();

        [Fact]
        public void CreateValue_None_ValueIsUnit() =>
            Make.Value(None).Get.Value.ValueUnsafe().Should().BeOfType<Unit>();

        [Property]
        public void Name_NonNull_NameIsName(NonNull<string> name) =>
            Make.Value().Name(name.Get).Get.Name.ValueUnsafe().Should().Be(name.Get);
    }
}