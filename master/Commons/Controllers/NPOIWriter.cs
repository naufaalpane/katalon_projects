using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Data;
using System.Reflection;
using System.IO;
using NPOI.SS.Util;

namespace GPPSU.Commons.Controllers
{
    public partial class NPOIWriter : IExcelWriter
    {
        private const int MaxNumberOfRowPerSheet = 65500;
        private const int MaximumSheetNameLength = 25;

        private HSSFWorkbook Workbook { get; set; }
        protected HSSFWorkbook XlsxWorkbook { get; set; }

        public NPOIWriter()
        {
            this.Workbook = new HSSFWorkbook();
            this.XlsxWorkbook = new HSSFWorkbook();
        }

        /// <summary>
        /// Create Table Header
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataSource"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        protected ISheet CreateColumnHeader<T>(IEnumerable<T> dataSource, String sheetName)
        {
            ISheet sheet = this.Workbook.CreateSheet(EscapeSheetName(sheetName));

            /* styling cell */
            ICellStyle headerStyle = this.Workbook.CreateCellStyle();
            headerStyle.BorderBottom = BorderStyle.Thin;
            headerStyle.BorderLeft = BorderStyle.Thin;
            headerStyle.BorderRight = BorderStyle.Thin;
            headerStyle.BorderTop = BorderStyle.Thin;

            headerStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LightOrange.Index;
            headerStyle.FillPattern = FillPattern.ThickHorizontalBands;
            headerStyle.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.LightOrange.Index;

            headerStyle.WrapText = false;
            headerStyle.VerticalAlignment = VerticalAlignment.Center;
            headerStyle.Alignment = HorizontalAlignment.Center;

            IFont headerLabelFont = this.Workbook.CreateFont();
            headerLabelFont.Boldweight = (short)FontBoldWeight.Bold;
            headerStyle.SetFont(headerLabelFont);

            IRow row = sheet.CreateRow(0);

            String cellValue = String.Empty;

            object obj = new object();
            if (dataSource.Any())
                obj = dataSource.ElementAt(0);

            Type t = obj.GetType();
            PropertyInfo[] pis = t.GetProperties();

            int i = 0;
            foreach (PropertyInfo pi in pis)
            {
                ICell cell = row.CreateCell(i);

                cellValue = pi.Name.ToString().Contains("_") ? pi.Name.ToString().Replace("_", " ") : pi.Name.ToString();
                cell.SetCellValue(cellValue);

                if (headerStyle != null)
                    cell.CellStyle = headerStyle;

                i++;
            }

            return sheet;
        }

        protected ISheet CreateColumnHeader(DataTable dataSource, String sheetName)
        {
            ISheet sheet = this.Workbook.CreateSheet(EscapeSheetName(sheetName));

            /* styling cell */
            ICellStyle headerStyle = this.Workbook.CreateCellStyle();
            headerStyle.BorderBottom = BorderStyle.Thin;
            headerStyle.BorderLeft = BorderStyle.Thin;
            headerStyle.BorderRight = BorderStyle.Thin;
            headerStyle.BorderTop = BorderStyle.Thin;

            IFont font = Workbook.CreateFont();
            font.FontHeightInPoints = 12;
            font.Boldweight = short.MaxValue;//Bold
            headerStyle.SetFont(font);

            headerStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LightOrange.Index;
            headerStyle.FillPattern = FillPattern.ThinHorizontalBands;
            headerStyle.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.LightOrange.Index;

            headerStyle.WrapText = true;
            headerStyle.VerticalAlignment = VerticalAlignment.Center;
            headerStyle.Alignment = HorizontalAlignment.Center;

            IFont headerLabelFont = this.Workbook.CreateFont();
            headerLabelFont.Boldweight = (short)FontBoldWeight.Bold;
            headerStyle.SetFont(headerLabelFont);

            IRow row = sheet.CreateRow(0);

            for (int colIndex = 0; colIndex < dataSource.Columns.Count; colIndex++)
            {
                ICell cell = row.CreateCell(colIndex);
                cell.SetCellValue(dataSource.Columns[colIndex].ColumnName);

                if (headerStyle != null)
                    cell.CellStyle = headerStyle;
            }

            return sheet;
        }

        protected ISheet CreateColumnFooter(ISheet sheet, String[] footerText)
        {
            /* styling cell */
            ICellStyle footerStyle = this.Workbook.CreateCellStyle();
            //footerStyle.BorderBottom = BorderStyle.Thin;
            //footerStyle.BorderLeft = BorderStyle.Thin;
            //footerStyle.BorderRight = BorderStyle.Thin;
            //footerStyle.BorderTop = BorderStyle.Thin;

            footerStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Red.Index;
            footerStyle.FillPattern = FillPattern.SolidForeground;
            //footerStyle.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.White.Index;

            footerStyle.Indention = 1;
            footerStyle.WrapText = false;
            footerStyle.VerticalAlignment = VerticalAlignment.Center;
            footerStyle.Alignment = HorizontalAlignment.Left;

            IFont footerLabelFont = this.Workbook.CreateFont();
            footerLabelFont.Boldweight = (short)FontBoldWeight.Bold;
            footerLabelFont.Color = NPOI.HSSF.Util.HSSFColor.Black.Index;
            footerStyle.SetFont(footerLabelFont);

            int lastRowIndex = sheet.LastRowNum;
            int footerRowIndex = 0;
            int counter = 2;
            String cellValue = String.Empty;

            foreach (String footer in footerText)
            {
                footerRowIndex = lastRowIndex + counter;
                IRow row = sheet.CreateRow(footerRowIndex);

                ICell cell = row.CreateCell(0);

                cellValue = footer;
                cell.SetCellValue(cellValue);
                cell.CellStyle = footerStyle;

                CellRangeAddress cellRange = new CellRangeAddress(footerRowIndex, footerRowIndex, 0, 10);
                sheet.AddMergedRegion(cellRange);

                counter++;
            }

            return sheet;
        }

        public static String EscapeSheetName(String sheetName)
        {
            String escapedSheetName = sheetName
                                        .Replace("/", "-")
                                        .Replace("\\", " ")
                                        .Replace("?", String.Empty)
                                        .Replace("*", String.Empty)
                                        .Replace("[", String.Empty)
                                        .Replace("]", String.Empty)
                                        .Replace(":", String.Empty);

            if (escapedSheetName.Length > MaximumSheetNameLength)
                escapedSheetName = escapedSheetName.Substring(0, MaximumSheetNameLength);

            return escapedSheetName;
        }


        /// <summary>
        /// Create Content Table
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataSource"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public byte[] Write<T>(IEnumerable<T> dataSource, String sheetName)
        {
            if (this.Workbook == null)
                this.Workbook = new HSSFWorkbook();

            byte[] result;
            if (dataSource == null)
                throw new ArgumentNullException("Data source cannot be null!!!");
            if (String.IsNullOrEmpty(sheetName))
                throw new ArgumentNullException("Sheet Name, cann0t be null!!");

            ISheet sheet = CreateColumnHeader(dataSource, sheetName);

            int rowIndex = 1;
            int i = 0;
            object value;
            if (dataSource.Any())
            {
                Type t = dataSource.ElementAt(0).GetType();
                PropertyInfo[] pis = t.GetProperties();

                ICellStyle headerStyle = this.Workbook.CreateCellStyle();
                headerStyle.BorderBottom = BorderStyle.Thin;
                headerStyle.BorderLeft = BorderStyle.Thin;
                headerStyle.BorderRight = BorderStyle.Thin;
                headerStyle.BorderTop = BorderStyle.Thin;

                ICell cell;
                foreach (object obj in dataSource)
                {
                    IRow row = sheet.CreateRow(rowIndex++);
                    i = 0;
                    foreach (PropertyInfo pi in pis)
                    {
                        value = pi.GetValue(obj, null);
                        cell = row.CreateCell(i);

                        if (value != null)
                            cell.SetCellValue(value.ToString());

                        cell.CellStyle = headerStyle;
                        i++;
                    }
                }
            }

            using (MemoryStream buffer = new MemoryStream())
            {
                this.Workbook.Write(buffer);
                result = buffer.GetBuffer();
            }

            this.Workbook = null;
            return result;
        }

        /// <summary>
        /// Create Content Table With Footer Section.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataSource"></param>
        /// <param name="sheetName"></param>
        /// <param name="footer"></param>
        /// <returns></returns>
        public byte[] Write<T>(IEnumerable<T> dataSource, String sheetName, String[] footer)
        {
            if (this.Workbook == null)
                this.Workbook = new HSSFWorkbook();

            byte[] result;
            if (dataSource == null)
                throw new ArgumentNullException("Data source cannot be null!!!");
            if (String.IsNullOrEmpty(sheetName))
                throw new ArgumentNullException("Sheet Name, cann0t be null!!");

            ISheet sheet = CreateColumnHeader(dataSource, sheetName);

            int rowIndex = 1;
            int i = 0;
            object value;
            if (dataSource.Any())
            {
                Type t = dataSource.ElementAt(0).GetType();
                PropertyInfo[] pis = t.GetProperties();

                ICellStyle headerStyle = this.Workbook.CreateCellStyle();
                headerStyle.BorderBottom = BorderStyle.Thin;
                headerStyle.BorderLeft = BorderStyle.Thin;
                headerStyle.BorderRight = BorderStyle.Thin;
                headerStyle.BorderTop = BorderStyle.Thin;
                                
                ICell cell;
                foreach (object obj in dataSource)
                {
                    IRow row = sheet.CreateRow(rowIndex++);
                    i = 0;
                    foreach (PropertyInfo pi in pis)
                    {
                        value = pi.GetValue(obj, null);
                        cell = row.CreateCell(i);

                        if (value != null)
                            cell.SetCellValue(value.ToString());

                        cell.CellStyle = headerStyle;

                        i++;
                    }
                }

                CreateColumnFooter(sheet, footer);
            }

            using (MemoryStream buffer = new MemoryStream())
            {
                this.Workbook.Write(buffer);
                result = buffer.GetBuffer();
            }

            this.Workbook = null;
            return result;
        }

        public byte[] Write(DataTable dataSource, String sheetName)
        {
            if (this.Workbook == null)
                this.Workbook = new HSSFWorkbook();

            byte[] result;
            if (dataSource == null)
                throw new ArgumentNullException("Data source cannot be null!!!");
            if (String.IsNullOrEmpty(sheetName))
                throw new ArgumentNullException("Sheet Name, cannot be null!!");

            ISheet sheet = CreateColumnHeader(dataSource, sheetName);

            if (dataSource != null)
            {
                ICellStyle headerStyle = this.Workbook.CreateCellStyle();
                headerStyle.BorderBottom = BorderStyle.Thin;
                headerStyle.BorderLeft = BorderStyle.Thin;
                headerStyle.BorderRight = BorderStyle.Thin;
                headerStyle.BorderTop = BorderStyle.Thin;

                int currentNPOIRowIndex = 1;
                int sheetCount = 1;

                for (int rowIndex = 0; rowIndex < dataSource.Rows.Count; rowIndex++)
                {
                    if (currentNPOIRowIndex >= MaxNumberOfRowPerSheet)
                    {
                        sheetCount++;
                        currentNPOIRowIndex = 1;

                        sheet = CreateColumnHeader(dataSource, sheetName + " - " + sheetCount);
                    }

                    IRow row = sheet.CreateRow(currentNPOIRowIndex++);

                    for (int colIndex = 0; colIndex < dataSource.Columns.Count; colIndex++)
                    {
                        ICell cell = row.CreateCell(colIndex);
                        cell.SetCellValue(dataSource.Rows[rowIndex][colIndex].ToString());
                    }
                }
            }

            using (MemoryStream buffer = new MemoryStream())
            {
                this.Workbook.Write(buffer);
                result = buffer.GetBuffer();
            }

            this.Workbook = null;
            return result;
        }

        public byte[] WriteXLSx(DataTable dataSource, String sheetName)
        {

            if (this.XlsxWorkbook == null)
                this.XlsxWorkbook = new HSSFWorkbook();

            byte[] result;
            if (dataSource == null)
                throw new ArgumentNullException("Data source cannot be null!!!");
            if (String.IsNullOrEmpty(sheetName))
                throw new ArgumentNullException("Sheet Name, cannot be null!!");

            ISheet sheet = CreateColumnHeaderXlsx(dataSource, sheetName);

            if (dataSource != null)
            {
                ICellStyle headerStyle = this.XlsxWorkbook.CreateCellStyle();
                headerStyle.BorderBottom = BorderStyle.Thin;
                headerStyle.BorderLeft = BorderStyle.Thin;
                headerStyle.BorderRight = BorderStyle.Thin;
                headerStyle.BorderTop = BorderStyle.Thin;

                int currentNPOIRowIndex = 1;
                int sheetCount = 1;

                for (int rowIndex = 0; rowIndex < dataSource.Rows.Count; rowIndex++)
                {
                    if (currentNPOIRowIndex >= MaxNumberOfRowPerSheet)
                    {
                        sheetCount++;
                        currentNPOIRowIndex = 1;

                        sheet = CreateColumnHeader(dataSource, sheetName + " - " + sheetCount);
                    }

                    IRow row = sheet.CreateRow(currentNPOIRowIndex++);

                    for (int colIndex = 0; colIndex < dataSource.Columns.Count; colIndex++)
                    {
                        ICell cell = row.CreateCell(colIndex);
                        cell.SetCellValue(dataSource.Rows[rowIndex][colIndex].ToString());
                    }
                }
            }

            using (MemoryStream buffer = new MemoryStream())
            {
                this.XlsxWorkbook.Write(buffer);
                result = buffer.GetBuffer();
            }

            this.XlsxWorkbook = null;
            return result;
        }

        protected ISheet CreateColumnHeaderXlsx(DataTable dataSource, String sheetName)
        {
            ISheet sheet = this.XlsxWorkbook.CreateSheet(EscapeSheetName(sheetName));

            /* styling cell */
            ICellStyle headerStyle = this.XlsxWorkbook.CreateCellStyle();
            headerStyle.BorderBottom = BorderStyle.Thin;
            headerStyle.BorderLeft = BorderStyle.Thin;
            headerStyle.BorderRight = BorderStyle.Thin;
            headerStyle.BorderTop = BorderStyle.Thin;

            headerStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LightOrange.Index;
            headerStyle.FillPattern = FillPattern.ThinHorizontalBands;
            headerStyle.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.LightOrange.Index;

            headerStyle.WrapText = true;
            headerStyle.VerticalAlignment = VerticalAlignment.Center;
            headerStyle.Alignment = HorizontalAlignment.Center;

            IFont headerLabelFont = this.XlsxWorkbook.CreateFont();
            headerLabelFont.Boldweight = (short)FontBoldWeight.Bold;
            headerStyle.SetFont(headerLabelFont);

            IRow row = sheet.CreateRow(0);

            for (int colIndex = 0; colIndex < dataSource.Columns.Count; colIndex++)
            {
                ICell cell = row.CreateCell(colIndex);
                cell.SetCellValue(dataSource.Columns[colIndex].ColumnName);

                if (headerStyle != null)
                    cell.CellStyle = headerStyle;
            }

            return sheet;
        }

        public static ICellStyle createCellStyleData(HSSFWorkbook wb, bool statement)
        {
            ICellStyle headerStyle = wb.CreateCellStyle();
            if (statement)
            {
                headerStyle.Alignment = HorizontalAlignment.Center;
            }
            headerStyle.VerticalAlignment = VerticalAlignment.Center;
            headerStyle.BorderBottom = BorderStyle.Thin;
            headerStyle.BorderLeft = BorderStyle.Thin;
            headerStyle.BorderRight = BorderStyle.Thin;
            headerStyle.BorderTop = BorderStyle.Thin;
            return headerStyle;
        }

        public static ICellStyle createMergedStyleTitle(HSSFWorkbook wb, bool statement)
        {
            ICellStyle headerStyle = wb.CreateCellStyle();
            headerStyle.WrapText = statement == true ? true : false;
            headerStyle.VerticalAlignment = VerticalAlignment.Center;
            headerStyle.Alignment = statement == true ? HorizontalAlignment.Center : HorizontalAlignment.Left;

            IFont headerLabelFont = wb.CreateFont();
            headerLabelFont.Boldweight = (short)FontBoldWeight.Bold;
            headerStyle.SetFont(headerLabelFont);
            return headerStyle;
        }

        public static ICellStyle createCellStyleColumnHeader(HSSFWorkbook wb)
        {
            ICellStyle headerStyle = wb.CreateCellStyle();
            headerStyle.BorderBottom = BorderStyle.Thin;
            headerStyle.BorderLeft = BorderStyle.Thin;
            headerStyle.BorderRight = BorderStyle.Thin;
            headerStyle.BorderTop = BorderStyle.Thin;

            headerStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey40Percent.Index;
            headerStyle.FillPattern = FillPattern.ThinHorizontalBands;
            headerStyle.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.Grey40Percent.Index;

            headerStyle.WrapText = true;
            headerStyle.VerticalAlignment = VerticalAlignment.Center;
            headerStyle.Alignment = HorizontalAlignment.Center;

            IFont headerLabelFont = wb.CreateFont();
            headerLabelFont.Color = NPOI.HSSF.Util.HSSFColor.White.Index;
            headerLabelFont.Boldweight = (short)FontBoldWeight.Bold;
            headerStyle.SetFont(headerLabelFont);
            return headerStyle;
        }

        public static ICellStyle createCellStyleColumnHeaderMandatory(HSSFWorkbook wb)
        {
            ICellStyle headerStyle = wb.CreateCellStyle();
            headerStyle.BorderBottom = BorderStyle.Thin;
            headerStyle.BorderLeft = BorderStyle.Thin;
            headerStyle.BorderRight = BorderStyle.Thin;
            headerStyle.BorderTop = BorderStyle.Thin;

            headerStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Red.Index;
            headerStyle.FillPattern = FillPattern.ThinHorizontalBands;
            headerStyle.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.Red.Index;

            headerStyle.WrapText = true;
            headerStyle.VerticalAlignment = VerticalAlignment.Center;
            headerStyle.Alignment = HorizontalAlignment.Center;

            IFont headerLabelFont = wb.CreateFont();
            headerLabelFont.Color = NPOI.HSSF.Util.HSSFColor.White.Index;
            headerLabelFont.Boldweight = (short)FontBoldWeight.Bold;
            headerStyle.SetFont(headerLabelFont);
            return headerStyle;
        }

        public static ICellStyle createCellStyleColumnHeaderNonMandatory(HSSFWorkbook wb)
        {
            ICellStyle headerStyle = wb.CreateCellStyle();
            headerStyle.BorderBottom = BorderStyle.Thin;
            headerStyle.BorderLeft = BorderStyle.Thin;
            headerStyle.BorderRight = BorderStyle.Thin;
            headerStyle.BorderTop = BorderStyle.Thin;

            //headerStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LightBlue.Index;
            //headerStyle.FillPattern = FillPattern.ThinHorizontalBands;
            //headerStyle.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.LightBlue.Index;

            headerStyle.WrapText = true;
            headerStyle.VerticalAlignment = VerticalAlignment.Center;
            headerStyle.Alignment = HorizontalAlignment.Center;

            IFont headerLabelFont = wb.CreateFont();
            headerLabelFont.Color = NPOI.HSSF.Util.HSSFColor.Black.Index;
            headerLabelFont.Boldweight = (short)FontBoldWeight.Bold;
            headerStyle.SetFont(headerLabelFont);
            return headerStyle;
        }

        public static ICellStyle createCellStyleDataDate(HSSFWorkbook wb, short dataFormat)
        {
            ICellStyle headerStyle = wb.CreateCellStyle();
            headerStyle.BorderBottom = BorderStyle.Thin;
            headerStyle.BorderLeft = BorderStyle.Thin;
            headerStyle.BorderRight = BorderStyle.Thin;
            headerStyle.BorderTop = BorderStyle.Thin;
            headerStyle.DataFormat = dataFormat;

            return headerStyle;
        }

        public static ICellStyle createCellStyleColumnHeader(HSSFWorkbook wb, short format)
        {
            ICellStyle headerStyle = createCellStyleColumnHeader(wb);
            headerStyle.DataFormat = format;

            return headerStyle;
        }

        public static void createCellText(
            IRow row,
            ICellStyle cellStyle,
            int colIdx, String txt)
        {
            ICell cell = row.CreateCell(colIdx);

            if (txt != null)
            {
                cell.SetCellValue(txt);
                cell.SetCellType(CellType.String);
            }
            else
            {
                cell.SetCellType(CellType.Blank);
            }

            if (cellStyle != null)
            {
                cell.CellStyle = cellStyle;
            }
        }

        public static void createCellDate(
            IRow row,
            ICellStyle cellStyle,
            int colIdx, DateTime? val)
        {
            ICell cell = row.CreateCell(colIdx);

            if (val != null)
            {
                cell.SetCellValue((DateTime)val);
                cell.SetCellType(CellType.Numeric);
            }
            else
            {
                cell.SetCellType(CellType.Blank);
            }

            if (cellStyle != null)
            {
                cell.CellStyle = cellStyle;
            }
        }

        public static void writeColHeader(
            HSSFWorkbook wb, ISheet sheet, int startRow,
            ICellStyle colStyleHeader,
            String[] colHeaders)
        {
            IRow row =
                sheet.CreateRow(startRow);

            for (int i = 0; i < colHeaders.Length; i++)
            {
                ICell cell = row.CreateCell(i);

                cell.SetCellValue(colHeaders[i]);

                if (colStyleHeader != null)
                {
                    cell.CellStyle = colStyleHeader;
                }
            }
        }

        public static void createCellDouble(
            IRow row,
            ICellStyle cellStyle,
            int colIdx, double? val)
        {
            ICell cell = row.CreateCell(colIdx);

            if (val != null)
            {
                cell.SetCellValue((double)val);
                cell.SetCellType(CellType.Numeric);
            }
            else
            {
                cell.SetCellType(CellType.Blank);
            }

            if (cellStyle != null)
            {
                cell.CellStyle = cellStyle;
            }
        }

        public static void createCellDecimal(
            IRow row,
            ICellStyle cellStyle,
            int colIdx, decimal? val)
        {
            ICell cell = row.CreateCell(colIdx);
            if (val != null)
            {
                cell.SetCellValue((double)val);
                cell.SetCellType(CellType.Numeric);
            }
            else
            {
                cell.SetCellType(CellType.Blank);
            }

            if (cellStyle != null)
            {
                cell.CellStyle = cellStyle;
            }
        }

        public static void CreateMergedColHeader(HSSFWorkbook wb, ISheet sheet, int startRow, int endRow, int startCol, 
            int endCol, ICellStyle colStyleHeader, String cellValue)
        {
            IRow row = null;

            for (var i = startRow; i <= endRow; i++)
            {
                row = sheet.GetRow(i) ?? sheet.CreateRow(i);
                for (var j = startCol; j <= endCol; j++)
                {
                    ICell cell = row.CreateCell(j);
                    cell.CellStyle = colStyleHeader;
                    cell.SetCellValue(cellValue);
                }
            }

            var cellRange = new CellRangeAddress(startRow, endRow, startCol, endCol);
            sheet.AddMergedRegion(cellRange);
        }

        public static void CreateSingleColHeader(HSSFWorkbook wb, ISheet sheet, int rows, int col, ICellStyle colStyleHeader, String cellValue)
        {
            IRow row = sheet.GetRow(rows) ?? sheet.CreateRow(rows);

            ICell cell = row.CreateCell(col);
            cell.CellStyle = colStyleHeader;
            cell.SetCellValue(cellValue);
        }

        public static void CreateSingleColHeader(HSSFWorkbook wb, ISheet sheet, int rows, int col, ICellStyle colStyleHeader, String cellValue, CellType cellType)
        {
            IRow row = sheet.GetRow(rows) ?? sheet.CreateRow(rows);

            ICell cell = row.CreateCell(col);
            cell.CellStyle = colStyleHeader;

            try
            {
                int number1 = 0;
                bool canConvert = int.TryParse(cellValue, out number1);
                if (canConvert == true)
                {
                    cell.SetCellValue(number1);
                    cell.SetCellType(cellType);
                }

                double number3 = 0;
                canConvert = double.TryParse(cellValue, out number3);
                if (canConvert == true)
                {
                    cell.SetCellValue(number3);
                    cell.SetCellType(cellType);
                }
                
            }
            catch (Exception ex)
            {
                cell.SetCellValue(0);
                cell.SetCellType(cellType);
            }

        }
    }
}