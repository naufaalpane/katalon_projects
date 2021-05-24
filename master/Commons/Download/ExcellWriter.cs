using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPOI.HPSF;
using NPOI.HSSF.Model;
using NPOI.HSSF.Util;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.IO;
using System.Text.RegularExpressions;

using NPOI.HSSF.Record;


namespace GPPSU.Commons.Download
{
    public class ExcelWriter
    {
        public const String COL_SEP = "|";
        public const String TAB_SEP = "\t";
        public const String MERGE_LEFT = "=<<";
        public const String MERGE_TOP = "=^^";
        public const char NAME_SEP = ',';

        const bool doDEBUG = false;
        const int gTOP = 0;
        const int gLEFT = 1;
        const int gBOTTOM = 2;
        const int gRIGHT = 3;
        const int gMIDDLE = 4;
        const int descRow = 4;
        const int descSpan = 4;

        private String[][] acols = null;
        private int[] awidth = null;
        private int columnCount;

        private HSSFWorkbook book = null;
        // private List<ISheet> pages = null;
        private ISheet page = null;
        private IRow row = null;

        const int xwHEAD = 0;
        const int xwCELL = 1;
        const int xwDESC = 2;
        const int xwTITLE = 3;
        const int xwTHIS = 4;
        const int xwAPP = 5;
        const int xwALL = 6;
        const int xwERR = 7;

        public const int xlRowLimit = 65536;

        private ICellStyle[] styles = new ICellStyle[xwERR + 1];
        private ICellStyle[] rowStyle = null;
        private IFont[] fonts = new IFont[xwERR + 1];

        private String fontName = "Calibri";

        // private int pageCount = 0;
        private int rowCount = 0;
        private int dataRow = 0;
        private int dataCount = 0;

        private List<ICell> aCell = null;
        private List<ICellStyle> aStyle = null;
        private Dictionary<String, ICellStyle> cs = new Dictionary<String, ICellStyle>();
        private Dictionary<String, ICell> cellByName = new Dictionary<String, ICell>();

        public ExcelWriter()
        {
            book = new HSSFWorkbook();
            InitStyles();
        }

        public ExcelWriter(String templateFile)
        {
            FileStream f = new FileStream(templateFile, FileMode.Open, FileAccess.Read);
            book = new HSSFWorkbook(f);
            aCell = new List<ICell>();
            aStyle = new List<ICellStyle>();
            page = book.GetSheetAt(0); // get first page 
        }

        public void CreatePage(String pageTitle)
        {
            page = book.CreateSheet(pageTitle);
        }

        public void MarkPage(String pageTitle)
        {
            page = book.GetSheet(pageTitle);            
        }

        public void SetPage(String pageTitle, int index)
        {
            book.SetSheetName(index, pageTitle);
        }

        public ICell Mark(String name)
        {
            ICell marked = null;
            int y = page.LastRowNum;
            for (int i = page.FirstRowNum; i <= y; i++)
            {
                IRow r = page.GetRow(i);
                if (r == null) continue;
                for (int j = r.FirstCellNum; j <= r.LastCellNum; j++)
                {
                    ICell c = r.GetCell(j);
                    if (c != null && c.CellType == CellType.String)
                    {
                        String v = c.StringCellValue;
                        v = v.Trim().ToLower();
                        if (v.Equals(name.Trim().ToLower()))
                        {
                            marked = c;
                            break;
                        }
                    }
                }
                if (marked != null) break;
            }
            return marked;
        }

        public ICell Mark(int index)
        {
            ICell marked = null;

            int y = page.LastRowNum;
            IRow r = page.GetRow(y);
            if (r == null) 
            {
                r = page.CreateRow(y);
            }
            ICell c = r.GetCell(index);
            if (c == null)
            {
                c = r.CreateCell(index);
            }

            marked = c;

            return marked;
        }

        public void ClearCells()
        {
            //Say("ClearCells()");
            dataCount = 0;
            aCell.Clear();
            cellByName.Clear();
        }

        public bool MarkLabels(String Names)
        {
            bool marked = true;

            if (Names == null || Names.Length < 1) return false;
            String[] names = Names.Split(NAME_SEP);

            foreach (String s in names)
            {
                ICell ic = Mark(s);
                if (ic == null)
                {
                    marked = false;
                    break;
                }
                else
                {
                    ICell nextCell = ic.Row.GetCell(ic.ColumnIndex + 1);
                    if (nextCell == null)
                    {
                        nextCell = ic.Row.CreateCell(ic.ColumnIndex + 1);
                    }
                    aCell.Add(nextCell);
                }
            }
            return marked;
        }

        public bool MarkColumn(String name)
        {
            bool marked = true;
            ICell ic = Mark(name);
            if (ic == null)
            {
                marked = false;

            }
            else
            {
                ICell belowCell;
                IRow belowRow;
                int i = 1;
                do
                {
                    belowRow = page.GetRow(ic.RowIndex + i);
                    if (belowRow == null)
                    {
                        belowRow = page.CreateRow(ic.RowIndex + i);
                    }
                    belowCell = belowRow.GetCell(ic.ColumnIndex);
                    if (belowCell == null)
                        belowCell = belowRow.CreateCell(ic.ColumnIndex);
                    i++;
                } while (belowCell.IsMergedCell && i + ic.RowIndex <= page.LastRowNum);
                aCell.Add(belowCell);
                cellByName.Add(name, belowCell);
            }
            return marked;
        }

        public bool MarkColumn(String name, int index)
        {
            bool marked = true;
            
            ICell ic = Mark(index);
            if (ic == null)
            {
                marked = false;
            }
            else
            {
                ICell belowCell;
                IRow belowRow;
                int i = 1;
                do
                {
                    belowRow = page.GetRow(ic.RowIndex + i);
                    if (belowRow == null)
                    {
                        belowRow = page.CreateRow(ic.RowIndex + i);
                    }
                    belowCell = belowRow.GetCell(ic.ColumnIndex);
                    if (belowCell == null)
                        belowCell = belowRow.CreateCell(ic.ColumnIndex);
                    i++;
                } while (belowCell.IsMergedCell && i + ic.RowIndex <= page.LastRowNum);
                aCell.Add(belowCell);
                cellByName.Add(name, belowCell);
            }


            return marked;
        }

        public bool MarkCells(String Names)
        {
            bool marked = true;

            if (Names == null || Names.Length < 1) return false;
            String[] names = Names.Split(NAME_SEP);

            foreach (String s in names)
            {
                ICell ic = Mark(s);
                if (ic == null)
                {
                    marked = false;
                    break;
                }
                else
                {
                    aCell.Add(ic);
                }
            }

            return marked;
        }

        public bool MarkColumns(String Names)
        {
            // Say("MarkColumns({0})", Names);
            InitStyles();

            bool marked = true;

            if (Names == null || Names.Length < 1) return false;
            String[] names = Names.Split(NAME_SEP);

            dataCount = 0;

            int i = 0;
            foreach (String name in names)
            {
                marked = MarkColumn(name);
                if (!marked)
                    Say("fail MarkColumn('{0}')", name);
                i++;
            }
            dataRow = aCell[0].RowIndex;
            awidth = new int[aCell.Count];
            return marked;
        }

        public bool MarkColumns(String[] Names)
        {
            InitStyles();

            bool marked = true;
            dataCount = 0;

            for (int i = 0; i < Names.Length; i++)
            {
                marked = MarkColumn(Names[i], i);
                if (!marked)
                    Say("fail MarkColumn('{0}', {1})", Names[i], i);
            }

            dataRow = aCell[0].RowIndex;
            awidth = new int[aCell.Count];
            return marked;
        }

        public bool MarkRow()
        {
            return true;
        }

        /*
         *  Put Data to Mark(ed) cell 
         */

        public void PutCells(params object[] data)
        {
            int maxCol = data.Length;
            if (maxCol > aCell.Count) maxCol = aCell.Count;

            for (int i = 0; i < maxCol; i++)
            {
                ICell c = null;
                if (aCell[i] == null) continue;
                int col = (aCell[i] as ICell).ColumnIndex;

                String v = "";
                c = aCell[i];

                if (data[i] != null)
                {
                    Type t = data[i].GetType();
                    TypeCode tc = Type.GetTypeCode(t);

                    if (t.IsValueType || tc == TypeCode.String)
                    {
                        v = data[i].ToString();


                        //ICellStyle x = book.CreateCellStyle();
                        //x.CloneStyleFrom(c.CellStyle);

                        //IFont fx = x.GetFont(book);
                        //fx.Color = HSSFColor.BLACK.index;
                        //x.SetFont(fx);

                        //c.CellStyle = x;

                        switch (tc)
                        {
                            case TypeCode.Boolean:
                                c.SetCellValue((bool)data[i]);
                                break;
                            case TypeCode.String:
                                c.SetCellValue(v);
                                break;
                            case TypeCode.DateTime:
                                c.SetCellValue((DateTime)data[i]);
                                break;
                            case TypeCode.Int16:
                            case TypeCode.Int32:
                            case TypeCode.Int64:
                            case TypeCode.UInt16:
                            case TypeCode.UInt32:
                            case TypeCode.UInt64:
                            case TypeCode.Single:
                            case TypeCode.Double:
                            case TypeCode.Byte:
                            case TypeCode.SByte:
                                c.SetCellValue((double)Convert.ToDouble(data[i]));
                                break;
                            default:
                                break;
                        }
                    }
                    else if (t.IsClass)
                    {
                        v = (data[i]).ToString();
                        c.SetCellValue(v);
                    }
                    else if (t.IsArray)
                    {
                        v = "[]";
                        c.SetCellValue(v);
                    }
                    else
                    {
                        v = "";
                        c.SetCellValue(v);
                    }
                }
                else
                {
                    c.SetCellValue("");
                }
            }

        }

        public void SetCalc(bool b = true)
        {
            book.ForceFormulaRecalculation = b;
        }

        public void SetStyleRed()
        {
            IRow r;

            int newRow = dataRow + dataCount - 1;
            //Say("SetStyleRed newRow = {0}; dataRow = {1}; dataCount = {2}", newRow, dataRow, dataCount);

            if (dataCount >= 1)
            {
                r = page.GetRow(newRow);
            }
            else
            {
                r = page.GetRow((aCell[0] as ICell).RowIndex);
            }

            int maxCol = aCell.Count;

            for (int i = 0; i < maxCol; i++)
            {
                ICell c = null;
                if (aCell[i] == null) continue;
                int col = (aCell[i] as ICell).ColumnIndex;
                c = r.GetCell(col);

                if (c != null)
                {
                    if (c.CellStyle == null)
                    {
                        // Say("Set CellStyle to ERR for ({0}, {1})", newRow, col);
                        c.CellStyle = styles[xwERR];
                    }
                    else
                    {
                        // Say("Set Color FillBackground to RED for ({0}, {1})", r.RowNum, col);
                        ICellStyle x = book.CreateCellStyle();
                        x.CloneStyleFrom(c.CellStyle);

                        IFont fx = x.GetFont(book);
                        fonts[xwERR].FontName = fx.FontName;
                        fonts[xwERR].FontHeightInPoints = fx.FontHeightInPoints;

                        x.SetFont(fonts[xwERR]);
                        x.FillForegroundColor = HSSFColor.Red.Index;
                        x.DataFormat = HSSFDataFormat.GetBuiltinFormat("Text");
                        x.FillPattern = FillPattern.SolidForeground;

                        c.CellStyle = x;
                    }
                }
                else
                    Say("Cannot find cell on row ({0},{1})", newRow, col);

            }

        }

        public void GetStyles()
        {
            //Say("GetStyles");
            IRow r = page.GetRow((aCell[0] as ICell).RowIndex);

            rowStyle = new ICellStyle[aCell.Count];
            for (int i = 0; i < aCell.Count; i++)
            {
                rowStyle[i] = book.CreateCellStyle();
                if (aCell[i] != null)
                    rowStyle[i].CloneStyleFrom(aCell[i].CellStyle);
            }
        }

        public void SetRow(object[] data)
        {
            IRow r;
            if (data == null) return;
            int newRow = dataRow + dataCount;

            //Say("SetRow newRow = {0}; dataRow = {1}; dataCount = {2}", newRow, dataRow, dataCount);

            bool isFirst = dataCount < 1;
            if (!isFirst)
            {
                if (page.LastRowNum < (newRow))
                    r = page.CreateRow(newRow);
                else
                    r = page.GetRow(newRow);
            }
            else
            {
                GetStyles();
                r = page.GetRow((aCell[0] as ICell).RowIndex);
            }
            dataCount++;

            int maxCol = data.Length;
            if (maxCol > aCell.Count) maxCol = aCell.Count;

            // Say("data.length = {0}; aCell.Count = {0};", data.Length, aCell.Count);

            for (int i = 0; i < maxCol; i++)
            {
                ICell c = null;
                if (aCell[i] == null) continue;
                int col = (aCell[i] as ICell).ColumnIndex;

                String v = "";
                if (isFirst)
                    c = aCell[i];

                if (data[i] != null)
                {
                    Type t = data[i].GetType();
                    TypeCode tc = Type.GetTypeCode(t);

                    if (t.IsValueType || tc == TypeCode.String)
                    {
                        v = data[i].ToString();

                        switch (tc)
                        {
                            case TypeCode.DateTime:
                                if (!isFirst)
                                    c = r.CreateCell(col, CellType.Blank);
                                if (c.CellType == CellType.Blank)
                                    c = r.CreateCell(col, CellType.Blank);
                                c.SetCellValue((DateTime)data[i]);
                                break;
                            case TypeCode.Boolean:
                                if (!isFirst)
                                    c = r.CreateCell(col, CellType.Boolean);
                                if (c.CellType == CellType.Blank)
                                    c = r.CreateCell(col, CellType.Boolean);
                                c.SetCellValue((bool)data[i]);
                                break;
                            case TypeCode.String:
                                if (!isFirst)
                                    c = r.CreateCell(col, CellType.String);
                                if (c.CellType == CellType.Blank)
                                    c = r.CreateCell(col, CellType.String);
                                c.SetCellValue(v);
                                break;
                            case TypeCode.Int16:
                            case TypeCode.Int32:
                            case TypeCode.Int64:
                            case TypeCode.UInt16:
                            case TypeCode.UInt32:
                            case TypeCode.UInt64:
                            case TypeCode.Single:
                            case TypeCode.Double:
                            case TypeCode.Byte:
                            case TypeCode.SByte:
                            case TypeCode.Decimal:
                                if (!isFirst)
                                    c = r.CreateCell(col, CellType.Numeric);
                                if (c.CellType == CellType.Blank)
                                    c = r.CreateCell(col, CellType.Numeric);
                                c.SetCellValue(Convert.ToDouble(data[i]));
                                break;
                            default:
                                if (!isFirst)
                                    c = r.CreateCell(col, CellType.String);
                                if (c.CellType == CellType.Blank)
                                    c = r.CreateCell(col, CellType.String);
                                c.SetCellValue(Convert.ToString(data[i]));
                                break;
                        }
                    }
                    else if (t.IsClass)
                    {
                        v = (data[i]).ToString();
                        if (!isFirst)
                            c = r.CreateCell(col);
                        c.SetCellValue(v);
                    }
                    else if (t.IsArray)
                    {
                        v = "[]";
                        if (!isFirst)
                            c = r.CreateCell(col);
                        c.SetCellValue(v);
                    }
                    else
                    {
                        v = "";
                        if (!isFirst)
                            c = r.CreateCell(col);
                        c.SetCellValue(v);
                    }
                }
                else
                {
                    if (!isFirst)
                    {
                        c = r.CreateCell(col, CellType.Blank);
                    }
                    else
                    {
                        c.SetCellValue("");
                    }
                }
                if (!isFirst && c != null)
                {
                    c.CellStyle = rowStyle[i];
                }
                else if (c == null) {}
            }
        }

        public object[] ReadRow()
        {
            object[] r = null;
            if (aCell.Count < 1)
            {
                Say("no Cells");
                return null;
            }
            dataRow = aCell[0].RowIndex + dataCount;

            Say("ReadRow dataRow={0} dataCount={1} lastRowNum = {2}", dataRow, dataCount, page.LastRowNum);
            if (dataRow > page.LastRowNum)
            {
                Say("dataRow exceeded");
                return r;
            }

            IRow row = page.GetRow(dataRow);
            if (row == null)
            {
                Say("no row past {0}", dataRow);
                return r;
            }
            r = new object[aCell.Count];
            int nulls = 0;
            for (int i = 0; i < aCell.Count; i++)
            {
                row = page.GetRow(aCell[i].RowIndex + dataCount);
                if (row == null)
                {
                    Say("no row for Cell[{0}]", i);
                    nulls++;
                    continue;
                }
                // Say("aCell[{0}].ColumnIndex={1} RowIndex={2}", i, aCell[i].ColumnIndex, aCell[i].RowIndex);
                ICell c = row.GetCell(aCell[i].ColumnIndex);
                if (c == null)
                {
                    r[i] = null;
                    // Say("r[{0|] = null", i);
                    nulls++;
                    continue;
                }

                CellType t = c.CellType;
                switch (t)
                {
                    case CellType.Blank:
                        r[i] = null;
                        nulls++;
                        break;
                    case CellType.Boolean:
                        r[i] = c.BooleanCellValue;
                        break;
                    case CellType.Error:
                        r[i] = null;
                        break;
                    case CellType.Formula:
                        r[i] = c.CellFormula.ToString();
                        break;
                    case CellType.Numeric:
                        r[i] = c.NumericCellValue;
                        break;
                    case CellType.String:
                        r[i] = c.StringCellValue;
                        break;
                    default:
                        r[i] = c.StringCellValue;
                        break;
                }
                TypeCode tc = t.GetTypeCode();

                // Say("r[{0}] GetType={1} Value={2}", i, t, r[i]);
            }

            ++dataCount;
            if (nulls >= aCell.Count)
                return null;
            return r;
        }

        public void WriteRow(object[] data) {}

        /*
         * Put data to Mark(ed) row, add new row if necessary 
         */

        public void PutRow(params object[] data)
        {
            SetRow(data);
        }

        public void PutPage(String pageTitle)
        {
            page = book.CreateSheet(pageTitle);
            page.DisplayGridlines = false;
            Say("PutPage(\"{0}\");", pageTitle);
        }

        private void InitStyles()
        {
            styles[xwTITLE] = book.CreateCellStyle();
            fonts[xwTITLE] = book.CreateFont();
            fonts[xwTITLE].FontName = fontName;
            fonts[xwTITLE].FontHeightInPoints = 17;
            fonts[xwTITLE].Boldweight = ((short)FontBoldWeight.Bold);
            styles[xwTITLE].SetFont(fonts[xwTITLE]);

            styles[xwAPP] = book.CreateCellStyle();
            fonts[xwAPP] = book.CreateFont();
            fonts[xwAPP].FontName = fontName;
            fonts[xwAPP].FontHeightInPoints = 14;
            fonts[xwAPP].Boldweight = ((short)FontBoldWeight.Bold);
            styles[xwAPP].SetFont(fonts[xwAPP]);

            styles[xwTHIS] = book.CreateCellStyle();
            fonts[xwTHIS] = book.CreateFont();
            fonts[xwTHIS].FontName = fontName;
            fonts[xwTHIS].FontHeightInPoints = 18;
            fonts[xwTHIS].Boldweight = ((short)FontBoldWeight.Bold);
            styles[xwTHIS].SetFont(fonts[xwTHIS]);
            styles[xwTHIS].Alignment = HorizontalAlignment.Center;

            styles[xwHEAD] = book.CreateCellStyle();
            fonts[xwHEAD] = book.CreateFont();
            fonts[xwHEAD].FontName = fontName;
            fonts[xwHEAD].FontHeightInPoints = 11;
            fonts[xwHEAD].Boldweight = ((short)FontBoldWeight.Bold);

            styles[xwHEAD].BorderTop = BorderStyle.Thin;
            styles[xwHEAD].BorderBottom = BorderStyle.Thin;
            styles[xwHEAD].BorderLeft = BorderStyle.Thin;
            styles[xwHEAD].BorderRight = BorderStyle.Thin;

            styles[xwHEAD].Alignment = HorizontalAlignment.Center;
            styles[xwHEAD].VerticalAlignment = VerticalAlignment.Center;
            styles[xwHEAD].FillPattern = FillPattern.SolidForeground;
            styles[xwHEAD].FillForegroundColor = HSSFColor.Grey25Percent.Index;

            styles[xwHEAD].SetFont(fonts[xwHEAD]);

            styles[xwDESC] = book.CreateCellStyle();
            fonts[xwDESC] = book.CreateFont();
            fonts[xwDESC].FontName = fontName;
            fonts[xwDESC].FontHeightInPoints = 11;

            fonts[xwDESC].Boldweight = ((short)FontBoldWeight.None);
            styles[xwDESC].SetFont(fonts[xwDESC]);
            styles[xwDESC].Alignment = HorizontalAlignment.Left;

            styles[xwCELL] = book.CreateCellStyle();

            fonts[xwCELL] = book.CreateFont();
            fonts[xwCELL].FontName = fontName;
            fonts[xwCELL].FontHeightInPoints = 11;
            styles[xwCELL].SetFont(fonts[xwCELL]);

            styles[xwCELL].BorderTop = BorderStyle.Thin;
            styles[xwCELL].BorderBottom = BorderStyle.Thin;
            styles[xwCELL].BorderLeft = BorderStyle.Thin;
            styles[xwCELL].BorderRight = BorderStyle.Thin;

            styles[xwCELL].Alignment = HorizontalAlignment.Center;

            styles[xwERR] = book.CreateCellStyle();
            styles[xwERR].FillForegroundColor = HSSFColor.Red.Index;
            IFont fx = book.GetFontAt(0);
            fonts[xwERR] = book.CreateFont();
            fonts[xwERR].FontName = fx.FontName;
            fonts[xwERR].FontHeightInPoints = fx.FontHeightInPoints;
            fonts[xwERR].Color = HSSFColor.Yellow.Index;
            styles[xwERR].SetFont(fonts[xwERR]);
            styles[xwERR].DataFormat = HSSFDataFormat.GetBuiltinFormat("Text");
            styles[xwERR].FillPattern = FillPattern.SolidForeground;

        }

        private void InsertLogo(String logoFile)
        {
            List<IRow> rlogo = new List<IRow>();

            for (int i = 0; i < 3; i++)
            {
                IRow rowLogo = page.CreateRow(i);
                for (int j = 0; j < 3; j++)
                {
                    rowLogo.CreateCell(j);
                }
                rlogo.Add(rowLogo);

            }
            rlogo[0].Cells[2].SetCellValue("Toyota Motor Manufacturing Indonesia");
            rlogo[0].Cells[2].CellStyle = styles[xwTITLE];

            rlogo[1].Cells[2].SetCellValue("Electronic Voucher Integrated System");
            rlogo[1].Cells[2].CellStyle = styles[xwAPP];

            CellRangeAddress cr = new CellRangeAddress(0, 2, 0, 0);
            page.AddMergedRegion(cr);
            cr.FirstColumn = 1;
            cr.LastColumn = 1;
            page.AddMergedRegion(cr);
            page.SetColumnWidth(1, 16 * 256);
            HSSFPatriarch px = (HSSFPatriarch)page.CreateDrawingPatriarch();
            IClientAnchor anchor = px.CreateAnchor(20, 20, 825, 200, 0, 0, 1, 2);
            anchor.AnchorType = (int)AnchorType.MoveAndResize;
            HSSFPicture pic = (HSSFPicture)px.CreatePicture(anchor, LoadImage(logoFile, book));

            pic.LineStyle = LineStyle.None;
            //pic.Resize();
            rowCount += 3;
            CellRangeAddress crTitle = new CellRangeAddress(0, 0, 2, (columnCount - 1));
            page.AddMergedRegion(crTitle);
            crTitle.FirstRow = 1;
            crTitle.LastRow = 1;
            page.AddMergedRegion(crTitle);

        }

        private void InsertDesc(String _heads, int headRows = 7)
        {
            String[] aHead = _heads.Split(new String[] { COL_SEP }, StringSplitOptions.None);
            int iheads = 0;
            int maxHeadRow = headRows;
            List<IRow> hrows = new List<IRow>();

            if (aHead.Length < headRows) maxHeadRow = aHead.Length;

            for (int i = 0; i < maxHeadRow; i++)
            {
                hrows.Add(page.CreateRow(descRow + i));
            }
            while (iheads < aHead.Length)
            {
                int hcol = iheads / headRows;
                int hrow = iheads % headRows;

                ICell c0 = hrows[hrow].CreateCell(hcol * descSpan);
                ICell c1 = hrows[hrow].CreateCell(hcol * descSpan + 1);
                ICell c2 = hrows[hrow].CreateCell(hcol * descSpan + 2);

                c0.CellStyle = styles[xwDESC];
                c1.CellStyle = styles[xwDESC];
                c2.CellStyle = styles[xwDESC];
                CellRangeAddress crH = new CellRangeAddress(descRow + hrow, descRow + hrow, hcol * descSpan, hcol * descSpan + 1);
                page.AddMergedRegion(crH);

                String v = aHead[iheads];
                int sepPos = v.IndexOf(":");

                if (sepPos >= 0)
                {
                    c0.SetCellValue(v.Substring(0, sepPos));
                    c2.SetCellValue(v.Substring(sepPos, v.Length - sepPos));
                }
                else
                {
                    c0.SetCellValue(v);
                }
                iheads++;
            }
            rowCount = descRow + maxHeadRow;
        }

        public void PutHeader(String Title, String Header, String Columns, String ImagePath, int descRows = 7)
        {
            String a = ColumnExcelWriterUtil.translate(Columns);
            PutHead(Title, Header, a, ImagePath, 7);
        }

        public void PutHead(String _title, String _heads, String _columns, String _image, int descRows)
        {
            Say("PutHead(\"{0}\");", _columns);

            String[] colHeadLine = _columns.Split(new String[] { "\n" }, StringSplitOptions.None);
            System.Array.Resize<String[]>(ref acols, colHeadLine.Length);

            columnCount = 0; /// reset per page
            for (int i = 0; i < acols.Length; i++)
            {
                acols[i] = colHeadLine[i].Split(new String[] { COL_SEP, TAB_SEP }, StringSplitOptions.None);
                if (columnCount < acols[i].Length) columnCount = acols[i].Length;
            }

            System.Array.Resize<int>(ref awidth, columnCount);
            for (int i = 0; i < awidth.Length; i++)
            {
                awidth[i] = 10;
            }
            for (int i = 0; i < acols.Length; i++)
            {
                for (int j = 0; j < acols[i].Length; j++)
                {
                    if (awidth[j] < acols[i][j].Length) awidth[j] = acols[i][j].Length + 5;
                }
                if (acols[i].Length < columnCount)
                {
                    // fill uneven column

                    int rowColumnCount = acols[i].Length;
                    // resize everyThing to max
                    System.Array.Resize<String>(ref acols[i], columnCount);
                    for (int k = rowColumnCount; k < columnCount; k++)
                    {
                        acols[i][k] = "?";
                    }
                }
            }

            InsertLogo(_image);
            InsertDesc(_heads, descRows);

            IRow titleRow = page.CreateRow(rowCount);
            ICell titleCell = titleRow.CreateCell(0);
            titleCell.SetCellValue(_title);
            titleCell.CellStyle = styles[xwTHIS];
            for (int i = 1; i < columnCount; i++)
            {
                ICell c = titleRow.CreateCell(i);
            }

            CellRangeAddress crTitle = new CellRangeAddress(rowCount, rowCount, 0, columnCount - 1);
            page.AddMergedRegion(crTitle);

            int r0 = rowCount + 1;

            for (int i = 0; i < acols.Length; i++)
            {
                IRow r = page.CreateRow(rowCount + 1);
                ++rowCount;
                for (int j = 0; j < columnCount; j++)
                {
                    if (!String.IsNullOrEmpty(acols[i][j])
                        || acols[i][j].Equals(MERGE_LEFT)
                        || acols[i][j].Equals(MERGE_TOP))
                    {
                        int k = j + 1;
                        int l = 0;
                        ICell c = r.CreateCell(j, CellType.String);

                        c.SetCellValue(acols[i][j]);
                        c.CellStyle = styles[xwHEAD];
                        ICell cx = null;
                        while (k < acols[i].Length && acols[i][k].Equals(MERGE_LEFT))
                        {
                            cx = r.CreateCell(k, CellType.Blank);
                            k++;
                            l++;
                        }
                        if (l > 0)
                        {
                            CellRangeAddress cr = new CellRangeAddress(r.RowNum, r.RowNum, c.ColumnIndex, cx.ColumnIndex);
                            page.AddMergedRegion(cr);
                        }
                    }
                }
            }

            // column merge 
            for (int j = 0; j < columnCount; j++)
            {
                int k = acols.Length - 1;
                int l = 0;
                while (k > 0 && acols[k][j].Equals(MERGE_TOP))
                {
                    // Say("merge column {0} up to line {1}", j, k);
                    k--;
                    l++;
                }
                if (l > 0)
                {
                    CellRangeAddress cr = new CellRangeAddress(r0 + k, r0 + l, j, j);
                    page.AddMergedRegion(cr);
                }

            }
        }

        public void Put(params object[] data)
        {
            IRow r = page.CreateRow(rowCount + 1);
            rowCount++;
            for (int i = 0; i < data.Length; i++)
            {
                ICell c = null;
                String v = "";

                if (data[i] != null)
                {
                    Type t = data[i].GetType();
                    TypeCode tc = Type.GetTypeCode(t);

                    // Say("   data[{0}] GetType = {1} {2}", i, t, tc);


                    if (t.IsValueType)
                    {
                        v = data[i].ToString();

                        switch (tc)
                        {
                            case TypeCode.Boolean:
                                c = r.CreateCell(i, CellType.Boolean);
                                c.SetCellValue((bool)data[i]);
                                break;
                            case TypeCode.String:
                                c = r.CreateCell(i, CellType.String);
                                c.SetCellValue(v);
                                break;
                            case TypeCode.Int16:
                            case TypeCode.Int32:
                            case TypeCode.Int64:
                            case TypeCode.UInt16:
                            case TypeCode.UInt32:
                            case TypeCode.UInt64:
                            case TypeCode.Single:
                            case TypeCode.Double:
                            case TypeCode.Byte:
                            case TypeCode.SByte:
                            case TypeCode.Decimal:
                                c = r.CreateCell(i, CellType.Numeric);
                                c.SetCellValue(Convert.ToDouble(data[i]));
                                break;
                            default:
                                break;
                        }
                    }
                    else if (t.IsClass)
                    {
                        v = (data[i]).ToString();
                    }
                    else if (t.IsArray)
                    {
                        v = "[]";
                    }
                    else
                    {
                        v = "";
                    }

                    if (c == null)
                    {
                        c = r.CreateCell(i);
                        c.SetCellValue(v);
                    }

                    if (v.Length > awidth[i])
                        awidth[i] = v.Length + 5;

                }
                else
                {
                    c = r.CreateCell(i, CellType.Blank);
                }

                c.CellStyle = styles[xwCELL];
            }
        }

        public void PutTail()
        {
            for (int i = 0; i < awidth.Length; i++)
            {
                page.SetColumnWidth(i, awidth[i] * 256);
                // page.AutoSizeColumn(i);
            }
            Say("PutTail();");
        }

        public MemoryStream Get()
        {
            MemoryStream x = new MemoryStream();
            book.Write(x);
            return x;
        }
        
        public byte[] GetBytes()
        {
            MemoryStream x = new MemoryStream();
            book.Write(x);
            return x.GetBuffer();
        }

        public void Write(String filename)
        {
            FileStream f = new FileStream(filename, FileMode.Create);
            book.Write(f);
            f.Close();
        }

        public static int LoadImage(String path, HSSFWorkbook wb)
        {
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[file.Length];
            file.Read(buffer, 0, (int)file.Length);
            return wb.AddPicture(buffer, PictureType.PNG);
        }

        public void ColumnarGroup(String columnName)
        {
            if (!cellByName.ContainsKey(columnName))
            {
                Say("Column Not Exist, cannot Group '{0}'", columnName);
                return;
            }
            ICell c = cellByName[columnName];
            if (c != null)
            {
                ColumnarGroup(c.ColumnIndex);
            }
        }

        public void ColumnarGroup(int col)
        {
            ColumnarGroup(col, dataRow, dataRow + dataCount);
        }

        private const int _TOP_BORDER = 1;
        private const int _BOTTOM_BORDER = 2;
        private const int _LEFT_BORDER = 4;
        private const int _RIGHT_BORDER = 8;

        private void RemoveBorder(ICell c, int BorderToRemove = 0)
        {
            if (c == null || BorderToRemove <= 0) return;

            ICellStyle x = book.CreateCellStyle();
            x.CloneStyleFrom(c.CellStyle);
            if ((BorderToRemove & _TOP_BORDER) > 0)
            {
                x.BorderTop = BorderStyle.None;
            }

            if ((BorderToRemove & _BOTTOM_BORDER) > 0)
            {
                x.BorderBottom = BorderStyle.None;
            }

            if ((BorderToRemove & _LEFT_BORDER) > 0)
            {
                x.BorderLeft = BorderStyle.None;
            }

            if ((BorderToRemove & _RIGHT_BORDER) > 0)
            {
                x.BorderRight = BorderStyle.None;
            }

            c.CellStyle = x;
        }

        private void ColumnarGroup(int col, int rowStart, int rowEnd)
        {

            ICell prevCell = null;

            for (int i = rowStart; i < rowEnd; i++)
            {
                IRow r = page.GetRow(i);
                if (r != null)
                {
                    ICell c = r.GetCell(col);

                    if (c != null
                        && (c.CellType == CellType.Blank ||
                            (c.CellType == CellType.String && String.IsNullOrEmpty(c.StringCellValue))
                            )
                        )
                    {
                        /// remove top when cell is empty 
                        RemoveBorder(c, _TOP_BORDER);
                        RemoveBorder(prevCell, _BOTTOM_BORDER);
                    }

                    if (c != null)
                        prevCell = c;

                }
            }
        }

        public void RowGroup()
        {
            RowGroup(dataRow + dataCount - 1);
        }

        private void RowGroup(int row)
        {
            IRow r = page.GetRow(row);
            if (r == null)
                return;
            ICell prevCell = null;
            for (int i = 0; i < aCell.Count; i++)
            {
                ICell c = r.GetCell(aCell[i].ColumnIndex);
                if (c != null
                        && (c.CellType == CellType.Blank ||
                            (c.CellType == CellType.String && String.IsNullOrEmpty(c.StringCellValue))
                            )
                        )
                {
                    RemoveBorder(c, _LEFT_BORDER);
                    RemoveBorder(prevCell, _RIGHT_BORDER);
                }
                if (c != null) prevCell = c;
            }
        }

        public static void Say(String word, params object[] x)
        {
            //LoggingLogic.say("ExcelWriter", word, x);
        }

        public static int Coli(String strCol)
        {
            strCol = strCol.ToUpper();
            int intColNumber = 0;
            int mul = 1;
            for (int j = strCol.Length - 1; j >= 0; --j)
            {
                intColNumber += Convert.ToInt16(Convert.ToByte(strCol[j]) - 64) * (mul);
                mul *= 26;
            }
            return intColNumber;
        }

        public static String Cole(int coli)
        {
            String x = "";

            while (coli > 0)
            {
                int a = (coli - 1) % 26;

                x = Convert.ToChar((byte)'A' + a) + x;

                coli = (int)((coli - a) / 26);
            }
            return x;
        }

        public static void XlsCellRange(String range, ref XlsCellPos topLeft, ref XlsCellPos bottomRight)
        {
            String[] rx = range.Split(':');

            if (rx.Length > 0) topLeft.Cell = rx[0];
            if (rx.Length > 1) bottomRight.Cell = rx[1];

            if (topLeft.Row < bottomRight.Row && bottomRight.Row > 0 && topLeft.Row > 0)
            {
                int t = topLeft.Row;
                topLeft.Row = bottomRight.Row;
                bottomRight.Row = t;
            }

            if (topLeft.Col < bottomRight.Col && bottomRight.Col > 0 && topLeft.Col > 0)
            {
                int t = topLeft.Col;
                topLeft.Col = bottomRight.Col;
                bottomRight.Col = t;
            }
        }

        #region custom
        public void insertRow(int destinationRowNum)
        {
            ISheet worksheet = book.GetSheetAt(0);
            book.GetSheetAt(0);

            IRow newRow = worksheet.GetRow(destinationRowNum);
          

            // If the row exist in destination, push down all rows by 1 else create a new row
            if (newRow != null)
            {
                worksheet.ShiftRows(destinationRowNum, worksheet.LastRowNum, 1);
            }
            else
            {
                newRow = worksheet.CreateRow(destinationRowNum);
            }
        }
        #endregion
    }

    public class XlsCellPos
    {
        public XlsCellPos(String CellAddr = null)
        {
            if (CellAddr != null)
            {
                cell = CellAddr;
                Parse(cell);
            }
            else
            {
                row = -1;
                col = -1;
            }
        }

        private void Parse(String r)
        {
            Regex cellPattern = new Regex("([A-Za-z]+)([0-9]*)", RegexOptions.IgnoreCase);
            MatchCollection c = cellPattern.Matches(r);
            if (c != null && c.Count > 0 && c[0].Groups.Count > 0)
            {
                String cellcol = c[0].Groups[1].Value;
                String cellrow = "";
                if (c[0].Groups.Count > 2) cellrow = c[0].Groups[2].Value;
                /*if (cellrow.isEmpty())
                    row = -1;
                else
                    row = cellrow.Int();
                row = cellrow.Int();*/
                col = ExcelWriter.Coli(cellcol) - 1;
            }
            else
            {
                cell = "";
                row = -1;
                row = -1;
            }
        }

        private String cell;
        public String Cell
        {
            get
            {
                return cell;
            }
            set
            {
                cell = value;
                Parse(cell);
            }
        }

        private int row;
        public int Row
        {
            get
            {
                return row;
            }
            set
            {
                row = value;
                setCell();
            }
        }

        private int col;
        public int Col
        {
            get
            {
                return col;
            }

            set
            {
                col = value;
                setCell();
            }
        }

        private void setCell()
        {
            cell = ColName + ((row > 0) ? row.ToString() : "");
        }

        public String ColName
        {
            get
            {
                return ExcelWriter.Cole(col);
            }
        }
    }
}
