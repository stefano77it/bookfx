﻿namespace BookFx.Renders
{
    using BookFx.Cores;
    using BookFx.Functional;
    using OfficeOpenXml;
    using static BookFx.Functional.ActComposition;
    using static BookFx.Functional.F;

    internal static class SheetRender
    {
        public static Act<ExcelWorksheet> Render(this SheetCore sheet) =>
            HarvestErrors(
                sheet.Box.Match(
                    none: NoAct,
                    some: SheetBoxRender.RootRender),
                SettingsRender(sheet));

        private static Act<ExcelWorksheet> SettingsRender(this SheetCore sheet) =>
            excelSheet =>
            {
                sheet.TabColor.ForEach(color => excelSheet.TabColor = color);

                sheet.PageView.ForEach(pageView =>
                {
                    excelSheet.View.PageLayoutView = pageView == PageView.Layout;
                    excelSheet.View.PageBreakView = pageView == PageView.Break;
                });

                excelSheet.PrinterSettings.FitToHeight = sheet.FitToHeight.GetOrElse(0);
                excelSheet.PrinterSettings.FitToWidth = sheet.FitToWidth.GetOrElse(0);
                excelSheet.PrinterSettings.FitToPage = sheet.FitToHeight.IsSome || sheet.FitToWidth.IsSome;

                sheet.Scale.ForEach(scale => excelSheet.View.ZoomScale = scale);

                return Unit();
            };
    }
}