using System;
using System.Collections.Generic;
using Toyota.Common.Database;
using Toyota.Common.Web.Platform;
using GPPSU.Commons.Controllers;
using GPPSU.Models.OrderVolumeDifferentListCreation;
using GPPSU.Commons.Constants;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Kernel.Geom;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout.Borders;
using iText.Kernel.Font;
using iText.IO.Font.Constants;

namespace GPPSU.Controllers
{
    public class OrderVolumeDifferentListCreationController : BaseController
    {
        // GET: OrderVolumeDifferentListCreation
        public OrderVolumeDifferentListCreationRepo ordDiffRepo = OrderVolumeDifferentListCreationRepo.Instance;
        protected override void Startup()
        {
            Settings.Title = "WA0AUE16 - Order Volume Different List";
        }

        public string executeBatch(OrderVolumeDifferentListCreation data)
        {
            string resultBatch = null;
            IDBContext db = DatabaseManager.Instance.GetContext();
            try
            {
                string fileName = "Order Volume Different List.pdf";
                string folder = Server.MapPath("~" + CommonConstant.TEMPLATE_EXCEL_DIR + "PDF/");
                string templateFileDirectory = Server.MapPath("~" + CommonConstant.TEMPLATE_EXCEL_DIR + "PDF/" + fileName);

                bool isExist = obtaindDifferentList(data, folder, templateFileDirectory);

                if (isExist)
                {
                    db.Execute("OrderVolumeDifferentListCreationController/UpdateOrderControl", new { company_code = data.COMPANY_CD });
                }

                resultBatch = ordDiffRepo.recordToBatchStatus(db, data.COMPANY_CD, "OK");

                byte[] result = System.IO.File.ReadAllBytes(templateFileDirectory);
                this.SendDataAsAttachment(fileName, result);
                System.IO.File.Delete(templateFileDirectory);
                return "success";
            }
            catch (Exception e)
            {
                resultBatch = ordDiffRepo.recordToBatchStatus(db, data.COMPANY_CD, "ERR");
                return e.Message;
            }
        }

        private bool obtaindDifferentList(OrderVolumeDifferentListCreation data, string folder, string path)
        {
            IDBContext db = DatabaseManager.Instance.GetContext();
            if (!System.IO.Directory.Exists(folder))
            {
                System.IO.Directory.CreateDirectory(folder);
            };

            bool isExist = true;

            FileStream fs = new FileStream(path, FileMode.Create);
            fs.Close();

            PdfWriter writer = new PdfWriter(path);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf, PageSize.A4.Rotate());
            try
            {
                int numberOfPage = 1;
                IList<OrderVolumeDifferentListCreation> listVolumeInfo = ordDiffRepo.GetOrderInformation(db, data.COMPANY_CD);
                int totalPage = listVolumeInfo.Count;

                Style fontStyle = new Style();
                PdfFont font = PdfFontFactory.CreateFont(StandardFonts.COURIER);
                fontStyle.SetFont(font);

                if (listVolumeInfo.Count > 0)
                {
                    foreach (OrderVolumeDifferentListCreation volumeInfo in listVolumeInfo)
                    {
                        volumeInfo.listDifferent = ordDiffRepo.GetOrderDifferentList(db, volumeInfo);
                    }

                    for (int i = 0; i < listVolumeInfo.Count; i++)
                    {
                        #region Title
                        Paragraph title = new Paragraph(string.Format("{0}", "** Warning of Order Difference Between Previous and Now **"))
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetBold()
                            .SetFontSize(12)
                            .AddStyle(fontStyle);

                        LineSeparator ls = new LineSeparator(new SolidLine());

                        document.Add(title);
                        document.Add(ls);
                        #endregion

                        #region Header
                        Table header = new Table(5)
                            .SetWidth(UnitValue.CreatePercentValue(90))
                            .SetMarginTop(15)
                            .SetHorizontalAlignment(HorizontalAlignment.CENTER);

                        Cell hr1c1 = new Cell(1, 1)
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetBorder(Border.NO_BORDER)
                            .SetFontSize(10)
                            .SetPaddingBottom(0)
                            .AddStyle(fontStyle)
                            .Add(new Paragraph("Destination"));
                        header.AddCell(hr1c1);

                        Cell hr1c2 = new Cell(1, 1)
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetBorder(Border.NO_BORDER)
                            .SetFontSize(10)
                            .SetPaddingBottom(0)
                            .AddStyle(fontStyle)
                            .Add(new Paragraph("Now"));
                        header.AddCell(hr1c2);

                        Cell hr1c3 = new Cell(1, 1)
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetBorder(Border.NO_BORDER)
                            .SetFontSize(10)
                            .SetPaddingBottom(0)
                            .AddStyle(fontStyle)
                            .Add(new Paragraph(listVolumeInfo[i].VERSION_NAME));
                        header.AddCell(hr1c3);

                        Cell hr1c4 = new Cell(1, 1)
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetBorder(Border.NO_BORDER)
                            .SetFontSize(10)
                            .SetPaddingBottom(0)
                            .AddStyle(fontStyle)
                            .Add(new Paragraph("MSP Order Type"));
                        header.AddCell(hr1c4);

                        Cell hr1c5 = new Cell(1, 1)
                            .SetTextAlignment(TextAlignment.RIGHT)
                            .SetBorder(Border.NO_BORDER)
                            .SetFontSize(10)
                            .SetPaddingBottom(0)
                            .AddStyle(fontStyle)
                            .SetWidth(UnitValue.CreatePercentValue(50))
                            .Add(new Paragraph(DateTime.Now.ToString("yyyy/MM/dd HH:mm")));
                        header.AddCell(hr1c5);

                        Cell hr2c1 = new Cell(1, 1)
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetBorder(Border.NO_BORDER)
                            .SetFontSize(10)
                            .AddStyle(fontStyle)
                            .Add(new Paragraph(listVolumeInfo[i].DESTINATION_CD));
                        header.AddCell(hr2c1);

                        Cell hr2c2 = new Cell(1, 1)
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetBorder(Border.NO_BORDER)
                            .SetFontSize(10)
                            .AddStyle(fontStyle)
                            .Add(new Paragraph(""));
                        header.AddCell(hr2c2);

                        Cell hr2c3 = new Cell(1, 1)
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetBorder(Border.NO_BORDER)
                            .SetFontSize(10)
                            .AddStyle(fontStyle)
                            .Add(new Paragraph(listVolumeInfo[i].REVISION_NO));
                        header.AddCell(hr2c3);

                        Cell hr2c4 = new Cell(1, 1)
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetBorder(Border.NO_BORDER)
                            .SetFontSize(10)
                            .AddStyle(fontStyle)
                            .Add(new Paragraph(listVolumeInfo[i].ORDER_TYPE));
                        header.AddCell(hr2c4);

                        Cell hr2c5 = new Cell(1, 1)
                            .SetTextAlignment(TextAlignment.RIGHT)
                            .SetBorder(Border.NO_BORDER)
                            .SetFontSize(10)
                            .AddStyle(fontStyle)
                            .SetWidth(UnitValue.CreatePercentValue(50))
                            .Add(new Paragraph(numberOfPage.ToString() + " / " + totalPage.ToString()));
                        header.AddCell(hr2c5);

                        document.Add(header);
                        #endregion

                        #region ContentHeader
                        Table content = new Table(14).UseAllAvailableWidth().SetMarginTop(10);

                        Cell cr1c1 = new Cell(2, 1)
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetFontSize(8)
                            .SetPaddingBottom(0)
                            .AddStyle(fontStyle)
                            .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                            .SetBorder(new RoundDotsBorder(1))
                            .Add(new Paragraph("CAR FAMILY CODE"));
                        content.AddCell(cr1c1);

                        Cell cr1c2 = new Cell(2, 1)
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetFontSize(8)
                            .SetPaddingBottom(0)
                            .AddStyle(fontStyle)
                            .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                            .SetBorder(new RoundDotsBorder(1))
                            .Add(new Paragraph("PART NO"));
                        content.AddCell(cr1c2);

                        for (int x = 0; x < 4; x++)
                        {
                            Paragraph p = new Paragraph();

                            switch (x)
                            {
                                case 0:
                                    p.Add(listVolumeInfo[i].listDifferent[0].NMONTH);
                                    break;
                                case 1:
                                    p.Add(listVolumeInfo[i].listDifferent[0].NMONTH_1);
                                    break;
                                case 2:
                                    p.Add(listVolumeInfo[i].listDifferent[0].NMONTH_2);
                                    break;
                                case 3:
                                    p.Add(listVolumeInfo[i].listDifferent[0].NMONTH_3);
                                    break;
                            }

                            Cell crcColspan = new Cell(1, 3)
                                .SetTextAlignment(TextAlignment.CENTER)
                                .SetFontSize(8)
                                .SetPaddingBottom(0)
                                .SetBorder(new RoundDotsBorder(1))
                                .AddStyle(fontStyle)
                                .Add(p);
                            content.AddCell(crcColspan);
                        }

                        for (int y = 0; y < 4; y++)
                        {
                            Cell cr1c3 = new Cell(1, 1)
                                    .SetTextAlignment(TextAlignment.CENTER)
                                    .SetFontSize(8)
                                    .SetPaddingBottom(0)
                                    .SetBorder(new RoundDotsBorder(1))
                                    .AddStyle(fontStyle)
                                    .Add(new Paragraph("PREVIOUS"));
                            content.AddCell(cr1c3);

                            Cell cr1c4 = new Cell(1, 1)
                                .SetTextAlignment(TextAlignment.CENTER)
                                .SetFontSize(8)
                                .SetPaddingBottom(0)
                                .SetBorder(new RoundDotsBorder(1))
                                .AddStyle(fontStyle)
                                .Add(new Paragraph("NOW"));
                            content.AddCell(cr1c4);

                            Cell cr1c5 = new Cell(1, 1)
                                .SetTextAlignment(TextAlignment.CENTER)
                                .SetFontSize(8)
                                .SetPaddingBottom(0)
                                .SetBorder(new RoundDotsBorder(1))
                                .AddStyle(fontStyle)
                                .Add(new Paragraph("DIFFERENT"));
                            content.AddCell(cr1c5);
                        }
                        #endregion

                        #region ContentDetail
                        foreach (VolumeDifferentList diff in listVolumeInfo[i].listDifferent)
                        {
                            Cell cr2c1 = new Cell(1, 1)
                                .SetTextAlignment(TextAlignment.CENTER)
                                .SetFontSize(8)
                                .SetPaddingBottom(0)
                                .AddStyle(fontStyle)
                                .SetBorder(new RoundDotsBorder(1))
                                .Add(new Paragraph(diff.CAR_FAMILY_CODE));
                            content.AddCell(cr2c1);

                            Cell cr2c2 = new Cell(1, 1)
                                .SetTextAlignment(TextAlignment.CENTER)
                                .SetFontSize(8)
                                .SetPaddingBottom(0)
                                .AddStyle(fontStyle)
                                .SetBorder(new RoundDotsBorder(1))
                                .Add(new Paragraph(diff.PART_NO));
                            content.AddCell(cr2c2);

                            Cell cr2c3 = new Cell(1, 1)
                                .SetTextAlignment(TextAlignment.RIGHT)
                                .SetFontSize(8)
                                .SetPaddingBottom(0)
                                .AddStyle(fontStyle)
                                .SetBorder(new RoundDotsBorder(1))
                                .Add(new Paragraph(diff.PREV_N.ToString()));
                            content.AddCell(cr2c3);

                            Cell cr2c4 = new Cell(1, 1)
                                .SetTextAlignment(TextAlignment.RIGHT)
                                .SetFontSize(8)
                                .SetPaddingBottom(0)
                                .AddStyle(fontStyle)
                                .SetBorder(new RoundDotsBorder(1))
                                .Add(new Paragraph(diff.NOW_N.ToString()));
                            content.AddCell(cr2c4);

                            Cell cr2c5 = new Cell(1, 1)
                                .SetTextAlignment(TextAlignment.RIGHT)
                                .SetFontSize(8)
                                .SetPaddingBottom(0)
                                .AddStyle(fontStyle)
                                .SetBorder(new RoundDotsBorder(1))
                                .Add(new Paragraph(diff.DIFF_N.ToString()));
                            content.AddCell(cr2c5);

                            Cell cr2c6 = new Cell(1, 1)
                                .SetTextAlignment(TextAlignment.RIGHT)
                                .SetFontSize(8)
                                .SetPaddingBottom(0)
                                .AddStyle(fontStyle)
                                .SetBorder(new RoundDotsBorder(1))
                                .Add(new Paragraph(diff.PREV_N_1.ToString()));
                            content.AddCell(cr2c6);

                            Cell cr2c7 = new Cell(1, 1)
                                .SetTextAlignment(TextAlignment.RIGHT)
                                .SetFontSize(8)
                                .SetPaddingBottom(0)
                                .AddStyle(fontStyle)
                                .SetBorder(new RoundDotsBorder(1))
                                .Add(new Paragraph(diff.NOW_N_1.ToString()));
                            content.AddCell(cr2c7);

                            Cell cr2c8 = new Cell(1, 1)
                                .SetTextAlignment(TextAlignment.RIGHT)
                                .SetFontSize(8)
                                .SetPaddingBottom(0)
                                .AddStyle(fontStyle)
                                .SetBorder(new RoundDotsBorder(1))
                                .Add(new Paragraph(diff.DIFF_N_1.ToString()));
                            content.AddCell(cr2c8);

                            Cell cr2c9 = new Cell(1, 1)
                                .SetTextAlignment(TextAlignment.RIGHT)
                                .SetFontSize(8)
                                .SetPaddingBottom(0)
                                .AddStyle(fontStyle)
                                .SetBorder(new RoundDotsBorder(1))
                                .Add(new Paragraph(diff.PREV_N_2.ToString()));
                            content.AddCell(cr2c9);

                            Cell cr2c10 = new Cell(1, 1)
                                .SetTextAlignment(TextAlignment.RIGHT)
                                .SetFontSize(8)
                                .SetPaddingBottom(0)
                                .AddStyle(fontStyle)
                                .SetBorder(new RoundDotsBorder(1))
                                .Add(new Paragraph(diff.NOW_N_2.ToString()));
                            content.AddCell(cr2c10);

                            Cell cr2c11 = new Cell(1, 1)
                                .SetTextAlignment(TextAlignment.RIGHT)
                                .SetFontSize(8)
                                .SetPaddingBottom(0)
                                .AddStyle(fontStyle)
                                .SetBorder(new RoundDotsBorder(1))
                                .Add(new Paragraph(diff.DIFF_N_2.ToString()));
                            content.AddCell(cr2c11);

                            Cell cr2c12 = new Cell(1, 1)
                                .SetTextAlignment(TextAlignment.RIGHT)
                                .SetFontSize(8)
                                .SetPaddingBottom(0)
                                .AddStyle(fontStyle)
                                .SetBorder(new RoundDotsBorder(1))
                                .Add(new Paragraph(diff.PREV_N_3.ToString()));
                            content.AddCell(cr2c12);

                            Cell cr2c13 = new Cell(1, 1)
                                .SetTextAlignment(TextAlignment.RIGHT)
                                .SetFontSize(8)
                                .SetPaddingBottom(0)
                                .AddStyle(fontStyle)
                                .SetBorder(new RoundDotsBorder(1))
                                .Add(new Paragraph(diff.NOW_N_3.ToString()));
                            content.AddCell(cr2c13);

                            Cell cr2c14 = new Cell(1, 1)
                                .SetTextAlignment(TextAlignment.RIGHT)
                                .SetFontSize(8)
                                .SetPaddingBottom(0)
                                .AddStyle(fontStyle)
                                .SetBorder(new RoundDotsBorder(1))
                                .Add(new Paragraph(diff.DIFF_N_3.ToString()));
                            content.AddCell(cr2c14);
                        }

                        document.Add(content);
                        #endregion

                        if (numberOfPage != totalPage)
                        {
                            document.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));
                        }

                        numberOfPage++;
                    }
                }
                else
                {
                    #region Title
                    Paragraph title = new Paragraph(string.Format("{0}", "** Warning of Order Difference Between Previous and Now **"))
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetBold()
                        .SetFontSize(12)
                        .AddStyle(fontStyle);

                    LineSeparator ls = new LineSeparator(new SolidLine());

                    document.Add(title);
                    document.Add(ls);
                    #endregion

                    #region Header
                    Table header = new Table(5)
                        .SetWidth(UnitValue.CreatePercentValue(90))
                        .SetMarginTop(15)
                        .SetHorizontalAlignment(HorizontalAlignment.CENTER);

                    Cell hr1c1 = new Cell(1, 1)
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetBorder(Border.NO_BORDER)
                        .SetFontSize(10)
                        .SetPaddingBottom(0)
                        .AddStyle(fontStyle)
                        .Add(new Paragraph("Destination"));
                    header.AddCell(hr1c1);

                    Cell hr1c2 = new Cell(1, 1)
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetBorder(Border.NO_BORDER)
                        .SetFontSize(10)
                        .SetPaddingBottom(0)
                        .AddStyle(fontStyle)
                        .Add(new Paragraph("Now"));
                    header.AddCell(hr1c2);

                    Cell hr1c3 = new Cell(1, 1)
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetBorder(Border.NO_BORDER)
                        .SetFontSize(10)
                        .SetPaddingBottom(0)
                        .AddStyle(fontStyle)
                        .Add(new Paragraph(""));
                    header.AddCell(hr1c3);

                    Cell hr1c4 = new Cell(1, 1)
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetBorder(Border.NO_BORDER)
                        .SetFontSize(10)
                        .SetPaddingBottom(0)
                        .AddStyle(fontStyle)
                        .Add(new Paragraph("MSP Order Type"));
                    header.AddCell(hr1c4);

                    Cell hr1c5 = new Cell(1, 1)
                        .SetTextAlignment(TextAlignment.RIGHT)
                        .SetBorder(Border.NO_BORDER)
                        .SetFontSize(10)
                        .SetPaddingBottom(0)
                        .AddStyle(fontStyle)
                        .SetWidth(UnitValue.CreatePercentValue(50))
                        .Add(new Paragraph(DateTime.Now.ToString("yyyy/MM/dd HH:mm")));
                    header.AddCell(hr1c5);

                    document.Add(header);
                    #endregion

                    #region ContentHeader
                    Table content = new Table(14).UseAllAvailableWidth().SetMarginTop(10);

                    Cell cr1c1 = new Cell(2, 1)
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFontSize(8)
                        .SetPaddingBottom(0)
                        .AddStyle(fontStyle)
                        .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                        .SetBorder(new RoundDotsBorder(1))
                        .Add(new Paragraph("CAR FAMILY CODE"));
                    content.AddCell(cr1c1);

                    Cell cr1c2 = new Cell(2, 1)
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFontSize(8)
                        .SetPaddingBottom(0)
                        .AddStyle(fontStyle)
                        .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                        .SetBorder(new RoundDotsBorder(1))
                        .Add(new Paragraph("PART NO"));
                    content.AddCell(cr1c2);

                    for (int x = 0; x < 4; x++)
                    {
                        Paragraph p = new Paragraph();

                        switch (x)
                        {
                            case 0:
                                p.Add("NMONTH");
                                break;
                            case 1:
                                p.Add("NMONTH+1");
                                break;
                            case 2:
                                p.Add("NMONTH+2");
                                break;
                            case 3:
                                p.Add("NMONTH+3");
                                break;
                        }

                        Cell crcColspan = new Cell(1, 3)
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetFontSize(8)
                            .SetPaddingBottom(0)
                            .SetBorder(new RoundDotsBorder(1))
                            .AddStyle(fontStyle)
                            .Add(p);
                        content.AddCell(crcColspan);
                    }

                    for (int y = 0; y < 4; y++)
                    {
                        Cell cr1c3 = new Cell(1, 1)
                                .SetTextAlignment(TextAlignment.CENTER)
                                .SetFontSize(8)
                                .SetPaddingBottom(0)
                                .SetBorder(new RoundDotsBorder(1))
                                .AddStyle(fontStyle)
                                .Add(new Paragraph("PREVIOUS"));
                        content.AddCell(cr1c3);

                        Cell cr1c4 = new Cell(1, 1)
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetFontSize(8)
                            .SetPaddingBottom(0)
                            .SetBorder(new RoundDotsBorder(1))
                            .AddStyle(fontStyle)
                            .Add(new Paragraph("NOW"));
                        content.AddCell(cr1c4);

                        Cell cr1c5 = new Cell(1, 1)
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetFontSize(8)
                            .SetPaddingBottom(0)
                            .SetBorder(new RoundDotsBorder(1))
                            .AddStyle(fontStyle)
                            .Add(new Paragraph("DIFFERENT"));
                        content.AddCell(cr1c5);
                    }
                    #endregion

                    #region ContentHeader
                    Cell cr2c1 = new Cell(1, 14)
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFontSize(8)
                        .SetPaddingBottom(5)
                        .SetPaddingTop(5)
                        .AddStyle(fontStyle)
                        .SetBorder(new RoundDotsBorder(1))
                        .Add(new Paragraph("No data found for comparing the order volume difference."));
                    content.AddCell(cr2c1);
                    #endregion

                    document.Add(content);
                    isExist = false;
                }

                document.Close();
            }
            catch (Exception e)
            {
                document.Close();
                isExist = false;
            }
            return isExist;
        }
    }
}