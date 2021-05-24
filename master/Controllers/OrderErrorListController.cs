using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Toyota.Common.Database;
using GPPSU.Models.OrderAcceptanceExportOrderBatch;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using GPPSU.Commons.Controllers;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Action;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.IO;
using iText.Layout.Borders;
using iText.Kernel.Geom;
using GPPSU.Commons.Models;
using Toyota.Common.Web.Platform;

namespace GPPSU.Controllers
{
    public class OrderErrorListController
    {

        /////////   FOR REPORT U01-002 START

        public void generatePDFError(List<DataErrorReport> listError)
        {
            int fontSize = 8;
            string startupPath = AppDomain.CurrentDomain.BaseDirectory;     //C:\\Users\\Whiteopen\\Documents\\GPPS_U_DEV\\
            string folder = startupPath + "//Content//Documents//PDF//";

            // string folder = Server.MapPath("~//Content//Documents//PDF//");
            string path = folder + "Template - OrderAcceptanceExportOrderBatch.pdf";

            if (!System.IO.Directory.Exists(folder))
            {
                System.IO.Directory.CreateDirectory(folder);
            };

            FileStream a = new FileStream(path, FileMode.Create);
            a.Close();
            a.Dispose();

            PdfWriter writer = new PdfWriter(path);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf, PageSize.A4.Rotate());
            try
            {
                // Header
                Table table = new Table(3, false).SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.RIGHT).UseAllAvailableWidth();

                Cell head1 = new Cell(0, 0)
                .SetTextAlignment(TextAlignment.LEFT).SetBorderRight(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER).SetBorderBottom(Border.NO_BORDER).SetBorderTop(Border.NO_BORDER).SetFontSize(13)
                .Add(new Paragraph("Order Control No").SetBold());
                table.AddCell(head1);

                Cell head2 = new Cell(0, 0)
               .SetTextAlignment(TextAlignment.CENTER).SetBorderRight(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER).SetBorderBottom(Border.NO_BORDER).SetBorderTop(Border.NO_BORDER).SetFontSize(13)
               .Add(new Paragraph("**     Order Error List      **").SetBold());
                table.AddCell(head2);

                Cell head3 = new Cell(0, 0)
               .SetTextAlignment(TextAlignment.RIGHT).SetBorderRight(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER).SetBorderBottom(Border.NO_BORDER).SetBorderTop(Border.NO_BORDER).SetFontSize(13)
               .Add(new Paragraph(listError[0].LOCALDATE).SetBold());
                table.AddCell(head3);

                Cell head4 = new Cell(0, 0)
               .SetTextAlignment(TextAlignment.LEFT).SetBorderRight(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER).SetBorderBottom(Border.NO_BORDER).SetBorderTop(Border.NO_BORDER).SetFontSize(13)
               .Add(new Paragraph(listError[0].OCN).SetBold());
                table.AddCell(head4);

                document.Add(table);

                Paragraph newline = new Paragraph(new Text("\n"));

                document.Add(newline);

                //////////////////TABLE DESCRIPTION///////////////
                Table table1 = new Table(9, false).SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.RIGHT).UseAllAvailableWidth();

                Cell CarFamilyCode = new Cell(0, 0)
                .SetTextAlignment(TextAlignment.CENTER).SetBorderTop(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER).SetFontSize(fontSize)
                .Add(new Paragraph("Car Family Code").SetBold());
                table1.AddCell(CarFamilyCode);

                Cell PartNo = new Cell(0, 0)
                .SetTextAlignment(TextAlignment.CENTER).SetBorderTop(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER).SetFontSize(fontSize)
                .Add(new Paragraph(new Text("Part No")).SetBold());
                table1.AddCell(PartNo);

                Cell OrderLOTSize = new Cell(0, 0)
                .SetTextAlignment(TextAlignment.CENTER).SetBorderTop(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER).SetFontSize(fontSize)
                .Add(new Paragraph(new Text("Order LOT Size")).SetBold());
                table1.AddCell(OrderLOTSize);

                Cell STATUSCode = new Cell(0, 0)
                .SetTextAlignment(TextAlignment.CENTER).SetBorderTop(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER).SetFontSize(fontSize)
                .Add(new Paragraph(new Text("STATUS Code")).SetBold());
                table1.AddCell(STATUSCode);

                Cell N = new Cell(0, 0)
                .SetTextAlignment(TextAlignment.CENTER).SetBorderTop(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER).SetFontSize(fontSize)
                .Add(new Paragraph(new Text("N")).SetBold());
                table1.AddCell(N);

                Cell NPLUS1 = new Cell(0, 0)
                .SetTextAlignment(TextAlignment.CENTER).SetBorderTop(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER).SetFontSize(fontSize)
                .Add(new Paragraph(new Text("N+1")).SetBold());
                table1.AddCell(NPLUS1);

                Cell NPLUS2 = new Cell(0, 0)
                .SetTextAlignment(TextAlignment.CENTER).SetBorderTop(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER).SetFontSize(fontSize)
                .Add(new Paragraph(new Text("N+2")).SetBold());
                table1.AddCell(NPLUS2);

                Cell NPLUS3 = new Cell(0, 0)
                .SetTextAlignment(TextAlignment.CENTER).SetBorderTop(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER).SetFontSize(fontSize)
                .Add(new Paragraph(new Text("N+3")).SetBold());
                table1.AddCell(NPLUS3);

                Cell ErrorMessage = new Cell(0, 0)
                .SetTextAlignment(TextAlignment.CENTER).SetBorderTop(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER).SetFontSize(fontSize)
                .Add(new Paragraph(new Text("Error Message")).SetBold());
                table1.AddCell(ErrorMessage);

                Cell DataCarFamilyCode = null;
                Cell DataPartNo = null;
                Cell DataOrderLOTSize = null;
                Cell DataSTATUSCode = null;
                Cell DataN = null;
                Cell DataNPLUS1 = null;
                Cell DataNPLUS2 = null;
                Cell DataNPLUS3 = null;
                Cell DataErrorMessage = null;

                DataCarFamilyCode = new Cell(0, 0)
                .SetTextAlignment(TextAlignment.LEFT).SetBorderTop(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER).SetFontSize(fontSize)
                ;

                DataPartNo = new Cell(0, 0)
                .SetTextAlignment(TextAlignment.LEFT).SetBorderTop(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER).SetFontSize(fontSize)
                ;

                DataOrderLOTSize = new Cell(0, 0)
                .SetTextAlignment(TextAlignment.LEFT).SetBorderTop(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER).SetFontSize(fontSize)
                ;

                DataSTATUSCode = new Cell(0, 0)
                .SetTextAlignment(TextAlignment.LEFT).SetBorderTop(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER).SetFontSize(fontSize)
                ;

                DataN = new Cell(0, 0)
                .SetTextAlignment(TextAlignment.LEFT).SetBorderTop(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER).SetFontSize(fontSize)
                ;

                DataNPLUS1 = new Cell(0, 0)
                .SetTextAlignment(TextAlignment.LEFT).SetBorderTop(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER).SetFontSize(fontSize)
                ;

                DataNPLUS2 = new Cell(0, 0)
                .SetTextAlignment(TextAlignment.LEFT).SetBorderTop(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER).SetFontSize(fontSize)
                ;


                DataNPLUS3 = new Cell(0, 0)
                .SetTextAlignment(TextAlignment.LEFT).SetBorderTop(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER).SetFontSize(fontSize)
                ;

                DataErrorMessage = new Cell(0, 0)
                .SetTextAlignment(TextAlignment.LEFT).SetBorderTop(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER).SetFontSize(fontSize)
                ;

                foreach (DataErrorReport data in listError)
                {
                    DataCarFamilyCode.Add(new Paragraph(data.CFC).SetBold());
                    DataPartNo.Add(new Paragraph(new Text(data.PARTNO)).SetBold());
                    DataOrderLOTSize.Add(new Paragraph(new Text(data.OCN)));
                    DataSTATUSCode.Add(new Paragraph(new Text(data.STATUSCODE)));
                    DataN.Add(new Paragraph(new Text(data.N)));
                    DataNPLUS1.Add(new Paragraph(new Text(data.NPLUS1)));
                    DataNPLUS2.Add(new Paragraph(new Text(data.NPLUS2)));
                    DataNPLUS3.Add(new Paragraph(new Text(data.NPLUS3)));
                    DataErrorMessage.Add(new Paragraph(new Text(data.ERR_MESG)));
                }

                table1.AddCell(DataCarFamilyCode);
                table1.AddCell(DataPartNo);
                table1.AddCell(DataOrderLOTSize);
                table1.AddCell(DataSTATUSCode);
                table1.AddCell(DataN);
                table1.AddCell(DataNPLUS1);
                table1.AddCell(DataNPLUS2);
                table1.AddCell(DataNPLUS3);
                table1.AddCell(DataErrorMessage);

                document.Add(table1);

                // Close document
                document.Close();

            }
            catch (Exception ae) { document.Close(); }
        }

        public void generatePDFNoError(DataErrorReport Data)
        {
           // DataErrorReport Data = new DataErrorReport();
           // Data = ToRepo.GetOCNAndDate();

            int fontSize = 8;
            string startupPath = AppDomain.CurrentDomain.BaseDirectory;     //C:\\Users\\Whiteopen\\Documents\\GPPS_U_DEV\\
            string folder = startupPath + "//Content//Documents//PDF//";

          //  string folder = Server.MapPath("~//Content//Documents//PDF//");
            string path = folder + "Template - OrderAcceptanceExportOrderBatch.pdf";

            if (!System.IO.Directory.Exists(folder))
            {
                System.IO.Directory.CreateDirectory(folder);
            };

            FileStream a = new FileStream(path, FileMode.Create);
            a.Close(); a.Dispose();

            PdfWriter writer = new PdfWriter(path);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf, PageSize.A4.Rotate());

            try
            {
                // Header

                Table table = new Table(3, false).SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.RIGHT).UseAllAvailableWidth();

                Cell head1 = new Cell(0, 0)
                .SetTextAlignment(TextAlignment.LEFT).SetBorderTop(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER).SetBorderBottom(Border.NO_BORDER).SetFontSize(13)
                .Add(new Paragraph("Order Control No").SetBold());
                table.AddCell(head1);

                Cell head2 = new Cell(0, 0)
               .SetTextAlignment(TextAlignment.CENTER).SetBorderTop(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER).SetBorderBottom(Border.NO_BORDER).SetFontSize(13)
               .Add(new Paragraph("Order Error List").SetBold());
                table.AddCell(head2);

                Cell head3 = new Cell(0, 0)
               .SetTextAlignment(TextAlignment.RIGHT).SetBorderTop(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER).SetBorderBottom(Border.NO_BORDER).SetFontSize(13)
               .Add(new Paragraph(Data.LOCALDATE).SetBold());
                table.AddCell(head3);

                Cell head4 = new Cell(0, 0)
               .SetTextAlignment(TextAlignment.LEFT).SetBorderTop(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER).SetBorderBottom(Border.NO_BORDER).SetFontSize(13)
               .Add(new Paragraph(Data.OCN).SetBold());
                table.AddCell(head4);

                document.Add(table);

                Paragraph newline = new Paragraph(new Text("\n")); document.Add(newline);

                document.Add(newline);

                //////////////////TABLE DESCRIPTION///////////////
                Table table1 = new Table(9, false).SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.RIGHT).UseAllAvailableWidth();

                Cell CarFamilyCode = new Cell(0, 0)
                .SetTextAlignment(TextAlignment.CENTER).SetBorderTop(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER).SetFontSize(fontSize)
                .Add(new Paragraph("Car Family Code").SetBold());
                table1.AddCell(CarFamilyCode);

                Cell PartNo = new Cell(0, 0)
                .SetTextAlignment(TextAlignment.CENTER).SetBorderTop(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER).SetFontSize(fontSize)
                .Add(new Paragraph(new Text("Part No")).SetBold());
                table1.AddCell(PartNo);

                Cell OrderLOTSize = new Cell(0, 0)
                .SetTextAlignment(TextAlignment.CENTER).SetBorderTop(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER).SetFontSize(fontSize)
                .Add(new Paragraph(new Text("Order LOT Size")).SetBold());
                table1.AddCell(OrderLOTSize);

                Cell STATUSCode = new Cell(0, 0)
                .SetTextAlignment(TextAlignment.CENTER).SetBorderTop(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER).SetFontSize(fontSize)
                .Add(new Paragraph(new Text("STATUS Code")).SetBold());
                table1.AddCell(STATUSCode);

                Cell N = new Cell(0, 0)
                .SetTextAlignment(TextAlignment.CENTER).SetBorderTop(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER).SetFontSize(fontSize)
                .Add(new Paragraph(new Text("N")).SetBold());
                table1.AddCell(N);

                Cell NPLUS1 = new Cell(0, 0)
                .SetTextAlignment(TextAlignment.CENTER).SetBorderTop(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER).SetFontSize(fontSize)
                .Add(new Paragraph(new Text("N+1")).SetBold());
                table1.AddCell(NPLUS1);

                Cell NPLUS2 = new Cell(0, 0)
                .SetTextAlignment(TextAlignment.CENTER).SetBorderTop(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER).SetFontSize(fontSize)
                .Add(new Paragraph(new Text("N+2")).SetBold());
                table1.AddCell(NPLUS2);

                Cell NPLUS3 = new Cell(0, 0)
                .SetTextAlignment(TextAlignment.CENTER).SetBorderTop(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER).SetFontSize(fontSize)
                .Add(new Paragraph(new Text("N+3")).SetBold());
                table1.AddCell(NPLUS3);

                Cell ErrorMessage = new Cell(0, 0)
                .SetTextAlignment(TextAlignment.CENTER).SetBorderTop(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER).SetFontSize(fontSize)
                .Add(new Paragraph(new Text("Error Message")).SetBold());
                table1.AddCell(ErrorMessage);
                document.Add(newline);
                document.Add(newline);

                document.Add(table1);

                Paragraph Result = new Paragraph("NO ERROR FOUND IN THIS PROCESS")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(16)
                .SetBold();
                document.Add(Result);

                document.Close();
            }
            catch (Exception ae) { a.Close(); document.Close(); }
        }

        /////////   FOR REPORT U01-002 END

    }
}
