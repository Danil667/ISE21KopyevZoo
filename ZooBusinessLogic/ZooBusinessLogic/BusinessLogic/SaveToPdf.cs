using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using ZooBusinessLogic.Enums;
using ZooBusinessLogic.HelperModels;

namespace ZooBusinessLogic.BusinessLogic
{
    public class SaveToPdf
    {
        public static void CreateDoc(PdfInfo info)
        {
            Document document = new Document();
            DefineStyles(document);
            Section section = document.AddSection();
            Paragraph paragraph = section.AddParagraph(info.Title);
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.Style = "NormalTitle";
            foreach (var excursion in info.Excursions)
            {
                var edLabel = section.AddParagraph("Маршрут №" + excursion.Id + " от " + excursion.ExcursionCreate.ToShortDateString());
                edLabel.Style = "NormalTitle";
                edLabel.Format.SpaceBefore = "1cm";
                edLabel.Format.SpaceAfter = "0,25cm";
                var routesLabel = section.AddParagraph("Маршруты:");
                routesLabel.Style = "NormalTitle";
                var routeTable = document.LastSection.AddTable();
                List<string> headerWidths = new List<string> { "1cm", "3cm", "2cm", "3cm", "3cm", "3cm", "2,5cm" };
                foreach (var elem in headerWidths)
                {
                    routeTable.AddColumn(elem);
                }
                CreateRow(new PdfRowParameters
                {
                    Table = routeTable,
                    Texts = new List<string> { "Маршрут №", "Название маршрута", "Дата начала маршрута", "Цена" },
                    Style = "NormalTitle",
                    ParagraphAlignment = ParagraphAlignment.Center
                });
                int i = 1;
                foreach (var route in excursion.RouteForExcursions)
                {
                    CreateRow(new PdfRowParameters
                    {
                        Table = routeTable,
                        Texts = new List<string> { i.ToString(), route.RouteName, route.StartRoute.ToString(), route.Cost.ToString() },
                        Style = "Normal",
                        ParagraphAlignment = ParagraphAlignment.Left
                    });
                    i++;
                }

                CreateRow(new PdfRowParameters
                {
                    Table = routeTable,
                    Texts = new List<string> { "", "", "", "", "", "Итого:", excursion.Cost.ToString() },
                    Style = "Normal",
                    ParagraphAlignment = ParagraphAlignment.Left
                });
                if (excursion.Status == ExcursionStatus.Принят)
                {
                    CreateRow(new PdfRowParameters
                    {
                        Table = routeTable,
                        Texts = new List<string> { "", "", "", "", "", "К оплате:", excursion.Remain.ToString() },
                        Style = "Normal",
                        ParagraphAlignment = ParagraphAlignment.Left
                    });
                }
                else
                {
                    CreateRow(new PdfRowParameters
                    {
                        Table = routeTable,
                        Texts = new List<string> { "", "", "", "", "", "К оплате:", excursion.Remain.ToString() },
                        Style = "Normal",
                        ParagraphAlignment = ParagraphAlignment.Left
                    });
                }
                if (info.Orders[excursion.Id].Count == 0)
                {
                    continue;
                }
                var paysLabel = section.AddParagraph("Платежи:");
                paysLabel.Style = "NormalTitle";
                var payTable = document.LastSection.AddTable();
                headerWidths = new List<string> { "1cm", "3cm", "3cm", "3cm" };
                foreach (var elem in headerWidths)
                {
                    payTable.AddColumn(elem);
                }
                CreateRow(new PdfRowParameters
                {
                    Table = payTable,
                    Texts = new List<string> { "№", "Дата", "Сумма" },
                    Style = "NormalTitle",
                    ParagraphAlignment = ParagraphAlignment.Center
                });
                i = 1;
                foreach (var pay in info.Orders[excursion.Id])
                {
                    CreateRow(new PdfRowParameters
                    {
                        Table = payTable,
                        Texts = new List<string> { i.ToString(), pay.DateCreate.ToString(), pay.Sum.ToString() },
                        Style = "Normal",
                        ParagraphAlignment = ParagraphAlignment.Left
                    });
                    i++;
                }
            }
            PdfDocumentRenderer renderer = new PdfDocumentRenderer(true)
            {
                Document = document
            };
            renderer.RenderDocument();
            renderer.PdfDocument.Save(info.FileName);
        }
        private static void DefineStyles(Document document)
        {
            Style style = document.Styles["Normal"];
            style.Font.Name = "Times New Roman";
            style.Font.Size = 14;
            style = document.Styles.AddStyle("NormalTitle", "Normal");
            style.Font.Bold = true;
        }
        private static void CreateRow(PdfRowParameters rowParameters)
        {
            Row row = rowParameters.Table.AddRow();
            for (int i = 0; i < rowParameters.Texts.Count; ++i)
            {
                FillCell(new PdfCellParameters
                {
                    Cell = row.Cells[i],
                    Text = rowParameters.Texts[i],
                    Style = rowParameters.Style,
                    BorderWidth = 0.5,
                    ParagraphAlignment = rowParameters.ParagraphAlignment
                });
            }
        }
        private static void FillCell(PdfCellParameters cellParameters)
        {
            cellParameters.Cell.AddParagraph(cellParameters.Text);
            if (!string.IsNullOrEmpty(cellParameters.Style))
            {
                cellParameters.Cell.Style = cellParameters.Style;
            }
            cellParameters.Cell.Borders.Left.Width = cellParameters.BorderWidth;
            cellParameters.Cell.Borders.Right.Width = cellParameters.BorderWidth;
            cellParameters.Cell.Borders.Top.Width = cellParameters.BorderWidth;
            cellParameters.Cell.Borders.Bottom.Width = cellParameters.BorderWidth;
            cellParameters.Cell.Format.Alignment = cellParameters.ParagraphAlignment;
            cellParameters.Cell.VerticalAlignment = VerticalAlignment.Center;
        }
    }
}
