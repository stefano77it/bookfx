﻿namespace BookFx.Tests.Functional
{
    using System;
    using System.Collections.Immutable;
    using System.Linq;
    using BookFx.Functional;
    using FluentAssertions;
    using FsCheck.Xunit;
    using Xunit;

    public class ScCompositionTests
    {
        [Property]
        public void Compose_MaxOfScs_Expected(int[] list, object state) =>
            list
                .Map(Sc<object>.ScOf)
                .Compose(int.MinValue, Math.Max)
                .Run(state)
                .Should()
                .Be(list.DefaultIfEmpty(int.MinValue).Max());

        [Property]
        public void Compose_MaxOfLogs_StateEqualList(int[] list)
        {
            var sc =
                from unused in list.Map(LogSc).Compose(int.MinValue, Math.Max)
                from log in Sc<ImmutableList<int>>.GetState
                select log;

            var result = sc.Run(ImmutableList<int>.Empty);

            result.Should().Equal(list);
        }

        [Fact]
        public void Compose_100K_NoStackOverflow() =>
            Enumerable
                .Range(1, 100_000)
                .Map(Sc<int>.ScOf)
                .Compose(0, Math.Max)
                .Run(0)
                .Should()
                .Be(100_000);

        private static Sc<ImmutableList<int>, int> LogSc(int value) =>
            from log in Sc<ImmutableList<int>>.GetState
            from unused in Sc<ImmutableList<int>>.PutState(log.Add(value))
            select value;
    }
}