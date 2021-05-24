using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.POIFS.FileSystem;
using System.IO;
using NPOI.XSSF.UserModel;


namespace GPPSU.Commons.Download
{
    public class CommonDownloadNPOI
    {
        private ExcelWriter excel;

        #region Singleton 
        private CommonDownloadNPOI() { }
        private static CommonDownloadNPOI instance = null;
        public static CommonDownloadNPOI Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = new CommonDownloadNPOI();
                }
                return instance;
            }
        }
        #endregion Singleton

        public CommonDownloadNPOI(string file)
        {
            excel = new ExcelWriter(file);
        }

        public void openFile(string file){
            excel = new ExcelWriter(file);
        }

        public byte[] closeFile()
        {
            return excel.GetBytes();
        }

        public void createSheet(string name)
        {
            excel.CreatePage(name);
        }

        public void setSheet(string name, int index)
        {
            excel.SetPage(name, index);
        }

        public void markAndFillRow(List<string[]> Lstobj)
        {
            excel.ClearCells();
            bool oMarked = excel.MarkColumns(Lstobj[0]);
            for (int r = 0; r < Lstobj.Count(); r++)
            {
                string[] column = Lstobj[r];
                excel.PutRow(column);
            }
        }

        public void markAndFillCell(List<string[]> Lstobj)
        {
            string header = String.Join(",", Lstobj[0]);
            bool oMarked = excel.MarkCells(header);
            excel.PutCells(Lstobj[1]);
            excel.ClearCells();
        }

        public void markAndFillTable(List<string[]> Lstobj)
        {
            excel.ClearCells();
            string header = String.Join(",", Lstobj[0]);
            //bool oMarked = excel.MarkColumns(header);
            for (int r = 1; r < Lstobj.Count(); r++)
            {
                string[] column = Lstobj[r];
                excel.PutRow(column);
            }
        }

        public void markAndFillCellValue(string param, string value)
        {
            ICell markedCell = excel.Mark(param);
            markedCell.SetCellValue(value);
        }

        #region functioncommon
        public byte[] createXlxFromTemplate(List<string[]> Lstobj, object classname, string file)
        {
            ExcelWriter o = new ExcelWriter(file);
            string header = String.Join(",",Lstobj[0]);
            bool oMarked = o.MarkColumns(header);
            for (int r = 1; r < Lstobj.Count(); r++)
            {                
                string[] column = Lstobj[r];
                o.PutRow(column);
            }
            return o.GetBytes();
        }
        
        public IWorkbook CreateObjectToSheet(List<string[]> Lstobj, object classname, string file)
        {

            string fileExtension = Path.GetExtension(file);
            IWorkbook workbook = null; //reference NPOI HSSF UserModel

            if (fileExtension == ".xlsx")
            {
                
                workbook = new XSSFWorkbook(new FileStream(file, FileMode.Open, FileAccess.Read));
            }
            else
            {
                POIFSFileSystem fs = new POIFSFileSystem(
               new FileStream(file, FileMode.Open, FileAccess.Read));
                workbook = new HSSFWorkbook();
            }
           
            ISheet sheet = workbook.GetSheetAt(0);
            //var font = workbook.CreateFont();
            //var style = workbook.CreateCellStyle();
            //style.FillBackgroundColor = NPOI.XSSF.Util.XSS.GREY_25_PERCENT.index;
            //style.FillPattern = FillPatternType.SOLID_FOREGROUND;
            //style.Alignment = HorizontalAlignment.CENTER;
            //font.FontHeightInPoints = 10;
            //font.Color = NPOI.XSSF.Util.XSSFColor.WHITE.index;
            //font.FontName = "Arial";
            //font.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.BOLD;
           
            for (int r = 1; r < Lstobj.Count(); r++)// fill data to sheet=========================================
            {
                var row = sheet.CreateRow(r);
                string[] column = Lstobj[r];
                for (int i = 0; i < column.Count(); i++)
                {
                   // row.CreateCell(i).SetCellValue(column[i]);
                    //InsertRows(ref sheet, 0, 1);
                    row.CreateCell(i).SetCellValue(column[i]);
                }
            }
            //for (int i = 0; i < columnheader.Count(); i++)// set autozise column==================================
            //{
            //    try
            //    {
            //        sheet.AutoSizeColumn(i);
            //    }
            //    catch { }
            //}
            return workbook;
        }
                
        private void InsertRows(ref ISheet sheet1, int fromRowIndex, int rowCount)
        {
            sheet1.ShiftRows(fromRowIndex, sheet1.LastRowNum, rowCount, true, true);

            for (int rowIndex = fromRowIndex; rowIndex < fromRowIndex + rowCount; rowIndex++)
            {
                IRow rowSource = sheet1.GetRow(rowIndex + rowCount);
                IRow rowInsert = sheet1.CreateRow(rowIndex);
                rowInsert.Height = rowSource.Height;
                for (int colIndex = 0; colIndex < rowSource.LastCellNum; colIndex++)
                {
                    ICell cellSource = rowSource.GetCell(colIndex);
                    ICell cellInsert = rowInsert.CreateCell(colIndex);
                    if (cellSource != null)
                    {
                        cellInsert.CellStyle = cellSource.CellStyle;
                    }
                }
            }
        }

        public void InserRows(int destinationRowNum)
        {
            excel.insertRow(destinationRowNum);        
        }

        public void SetRows(List<string[]> data)
        {
            foreach (object[] item in data) {
                excel.SetRow(item);
            }
        }
        #endregion functioncommon 
    }
}
