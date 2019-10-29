﻿namespace BookFx
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BookFx.Cores;
    using JetBrains.Annotations;
    using static BookFx.Functional.F;
    using static BookFx.Make;

    [PublicAPI]
    public sealed class ValueBox : Box
    {
        public static readonly ValueBox Empty = BoxCore.Create(BoxType.Value);

        private ValueBox(BoxCore core)
            : base(core)
        {
        }

        [Pure]
        public static implicit operator ValueBox(BoxCore core) => new ValueBox(core);

        [Pure]
        public static implicit operator ValueBox(string? value) => Value(value);

        [Pure]
        public static implicit operator ValueBox(bool value) => Value(value);

        [Pure]
        public static implicit operator ValueBox(int value) => Value(value);

        [Pure]
        public static implicit operator ValueBox(long value) => Value(value);

        [Pure]
        public static implicit operator ValueBox(double value) => Value(value);

        [Pure]
        public static implicit operator ValueBox(decimal value) => Value(value);

        [Pure]
        public static implicit operator ValueBox(DateTime value) => Value(value);

        [Pure]
        public new ValueBox Name(string name) => Get.With(name: Some(name));

        [Pure]
        public new ValueBox Style(BoxStyle style) => Get.With(style: style.Get);

        [Pure]
        public ValueBox SpanRows(int count) => Get.With(rowSpan: count);

        [Pure]
        public ValueBox SpanCols(int count) => Get.With(colSpan: count);

        [Pure]
        public ValueBox Span(int rows, int cols) => Get.With(rowSpan: rows, colSpan: cols);

        [Pure]
        public ValueBox Merge() => Get.With(merge: true);

        [Pure]
        public new ValueBox SizeRows(IEnumerable<TrackSize> sizes) => Get.With(rowSizes: sizes);

        [Pure]
        public new ValueBox SizeRows(TrackSize size, params TrackSize[] others) =>
            Get.With(rowSizes: others.Prepend(size));

        [Pure]
        public new ValueBox SizeCols(IEnumerable<TrackSize> sizes) => Get.With(colSizes: sizes);

        [Pure]
        public new ValueBox SizeCols(TrackSize size, params TrackSize[] others) =>
            Get.With(colSizes: others.Prepend(size));

        [Pure]
        public new ValueBox SetPrintArea() => Get.With(isPrintArea: true);

        [Pure]
        public new ValueBox HideRows() => Get.With(isRowsHidden: true);

        [Pure]
        public new ValueBox HideCols() => Get.With(isColsHidden: true);

        [Pure]
        public new ValueBox FreezeRows() => Get.With(isRowsFrozen: true);

        [Pure]
        public new ValueBox FreezeCols() => Get.With(isColsFrozen: true);
    }
}