using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GPPSU.Commons.Download
{
    class ColumnExcelWriterUtil
    {
        readonly static char MERGE_COL_OPEN = '{';
        readonly static char MERGE_COL_CLOSE = '}';
        readonly static char COL_SEPARATOR = '|';
        readonly static String MERGE_COL_TO = "=<<";
        readonly static String MERGE_COL_UP = "=^^";
        readonly static String NEW_LINE = "\n";

        readonly static string ERR_IMBALANCE = "Parse Error: Open bracket is not in the same number with the closing bracket";

        readonly static string ERR_EMPTY = "Parse Error: Empty char or closing bracket after open bracket";

        public static string translate(string newColumns)
        {
            ColumnExcelWriterUtil c = new ColumnExcelWriterUtil();
            return c.finalExcelOutputStr(newColumns);
        }

        public String finalExcelOutputStr(String param)
        {
            String lastExcelStatement = "";
            if (param != null && !String.IsNullOrWhiteSpace(param))
            {
                if (param.Count(f => f == MERGE_COL_OPEN) == param.Count(f => f == MERGE_COL_CLOSE))
                {
                    lastExcelStatement = param;
                    while (lastExcelStatement.Contains(MERGE_COL_OPEN))
                        lastExcelStatement = generateColumnXlsStr(lastExcelStatement);

                    lastExcelStatement = rightPaddingAdjustment(lastExcelStatement);
                }
                else
                {
                    return ERR_IMBALANCE;
                }
            }
            return lastExcelStatement;
        }

        String rightPaddingAdjustment(String param)
        {
            String lastGeneratedColumn = "";
            if (!String.IsNullOrEmpty(param) && param.Contains(NEW_LINE))
            {
                String[] column_lines = Regex.Split(param, /* "\\" + */ NEW_LINE);
                bool first = true;
                int firstColCount = 0;
                int nextColCount = 0;
                for (int i = 0; i < column_lines.Length; i++)
                {
                    String line = column_lines[i];
                    if (first)
                    {
                        firstColCount = line.Count(f => f == COL_SEPARATOR);
                    }
                    else
                    {
                        nextColCount = line.Count(f => f == COL_SEPARATOR);
                        if (nextColCount < firstColCount)
                        {
                            for (int j = firstColCount - nextColCount; j > 0; j--)
                                line += (COL_SEPARATOR + MERGE_COL_UP);

                            column_lines[i] = line;
                        }
                    }
                    first = false;
                }

                for (int i = 0; i < column_lines.Length; i++)
                {
                    String line = column_lines[i];
                    if ((i + 1) >= column_lines.Length)
                        lastGeneratedColumn += line;
                    else
                        lastGeneratedColumn += (line + NEW_LINE);
                }
            }
            else
            {
                lastGeneratedColumn = param;
            }
            return lastGeneratedColumn;
        }

        String generateColumnXlsStr(String param)
        {
            String generatedColumn = "";

            if (param != null && !String.IsNullOrWhiteSpace(param))
            {
                if (param.Count(f => f == MERGE_COL_OPEN) == param.Count(f => f == MERGE_COL_CLOSE))
                {
                    List<String> columns = getColumns(param);
                    List<String> childs = new List<String>();
                    bool first = true;
                    int parentCount = 0;
                    foreach (String column in columns)
                    {
                        String child = "";
                        if (column.Contains(NEW_LINE))
                        {
                            parentCount = 0;
                        }
                        if (first)
                            generatedColumn += convertExcelColumnParent(column, parentCount, out child);
                        else
                            generatedColumn += (COL_SEPARATOR + convertExcelColumnParent(column, parentCount, out child));

                        parentCount++;
                        if (!String.IsNullOrWhiteSpace(child))
                        {
                            childs.Add(child);
                            parentCount = 0;
                        }
                        first = false;
                    }
                    if (childs.Count > 0)
                    {
                        generatedColumn += NEW_LINE;
                        first = true;
                    }
                    foreach (String childColumn in childs)
                    {
                        if (first)
                            generatedColumn += childColumn;
                        else
                            generatedColumn += (COL_SEPARATOR + childColumn);
                        first = false;
                    }
                }
                else
                {
                    return ERR_IMBALANCE;
                }
            }

            return generatedColumn;
        }

        List<String> getColumns(string header)
        {
            List<String> columns = new List<String>();
            if (header.Contains(COL_SEPARATOR))
            {
                String tmp = header;
                int curSepIdx = header.IndexOf(COL_SEPARATOR);
                //int curIdx = 0;
                while (tmp.Contains(COL_SEPARATOR))
                {
                    String chkBr = tmp.Split(COL_SEPARATOR)[0];
                    if (chkBr.Contains(MERGE_COL_OPEN))
                    {
                        if (chkBr.Count(f => f == MERGE_COL_OPEN) != chkBr.Count(f => f == MERGE_COL_CLOSE))
                        {
                            int op_ctr = chkBr.Count(f => f == MERGE_COL_OPEN);
                            String tmp2 = tmp.Substring(curSepIdx);
                            char[] tmp2_chr = tmp2.ToCharArray();

                            for (int tmp2_idx = 0; tmp2_idx < tmp2_chr.Length; tmp2_idx++)
                            {
                                if (tmp2_chr[tmp2_idx] == MERGE_COL_OPEN)
                                    op_ctr++;
                                else if (tmp2_chr[tmp2_idx] == MERGE_COL_CLOSE)
                                    op_ctr--;
                                chkBr += tmp2_chr[tmp2_idx];
                                curSepIdx++;

                                if (op_ctr <= 0)
                                    break;
                            }
                        }
                    }
                    columns.Add(chkBr);
                    if (curSepIdx < tmp.Length)
                    {
                        tmp = tmp.Substring(curSepIdx + 1);
                        curSepIdx = tmp.IndexOf(COL_SEPARATOR);
                        if (curSepIdx <= 0)
                        {
                            columns.Add(tmp);
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {
                columns.Add(header);
            }
            return columns;
        }

        String convertExcelColumnParent(string param, int parentCount, out String child)
        {
            String generatedColumn = "";
            String tmpChld = "";

            if (param != null)
            {
                generatedColumn = Convert.ToString(param);
                if (!String.IsNullOrWhiteSpace(generatedColumn) && generatedColumn.Contains(MERGE_COL_OPEN))
                {
                    if (generatedColumn.Count(f => f == MERGE_COL_OPEN) == generatedColumn.Count(f => f == MERGE_COL_CLOSE))
                    {
                        int startIndex = generatedColumn.IndexOf(MERGE_COL_OPEN);
                        string checkNextChar = generatedColumn.Substring(startIndex + 1, 1);
                        if (!checkNextChar.Trim().Equals("") && !checkNextChar.Trim().Equals(MERGE_COL_CLOSE))
                        {
                            int endIndex = generatedColumn.IndexOf(MERGE_COL_CLOSE, startIndex);
                            String inBetween = generatedColumn.Substring(startIndex + 1,
                                               (endIndex - startIndex) - 1);
                            while (inBetween.Contains(MERGE_COL_OPEN))
                            {
                                if (endIndex < (generatedColumn.Length - 1))
                                {
                                    endIndex = generatedColumn.IndexOf(MERGE_COL_CLOSE, endIndex + 1);
                                    inBetween = generatedColumn.Substring(startIndex + 1,
                                                   (endIndex - startIndex) - 1);
                                }
                                else
                                {
                                    break;
                                }
                            }
                            int childCount = inBetween.Count(f => f == COL_SEPARATOR);
                            String prevStr = "";
                            String endStr = "";
                            if (startIndex > 0)
                            {
                                prevStr = generatedColumn.Substring(0, startIndex);
                                for (int i = 0; i < childCount; i++)
                                    prevStr += (COL_SEPARATOR + MERGE_COL_TO);
                            }
                            if (endIndex != (generatedColumn.Length - 1))
                            {
                                endStr = generatedColumn.Substring(endIndex);
                            }
                            prevStr += endStr;
                            for (int i = 0; i < parentCount; i++)
                                tmpChld += (MERGE_COL_UP + COL_SEPARATOR);
                            tmpChld += inBetween;
                            generatedColumn = prevStr;
                        }
                        else
                        {
                            child = "";
                            return ERR_EMPTY;
                        }
                    }
                }
            }
            child = tmpChld;
            return generatedColumn;
        }
    }
}
