using System;
using System.Globalization;
using System.IO;
using NPOI.SS.UserModel;
using UnityEngine;

namespace Util
{
    public static partial class Utility
    {
        public static IWorkbook CreateWorkbook(string path)
        {
            try
            {
                using var stream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                return WorkbookFactory.Create(stream);
            }
            catch (Exception e)
            {
                Debug.LogError($"Excel加载失败：{path}\n{e.Message}\n{e.StackTrace}\n InnerException=>{e.InnerException}");
                throw;
            }
        }
        
        public static void WriteExcel(IWorkbook workbook, string path)
        {
            using var ms = new MemoryStream();
            using var fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            workbook.Write(ms);
            var b = ms.ToArray();
            fs.Write(b, 0, b.Length);
            fs.Flush();
            fs.Close();
        }

        public static ISheet GetSheetByName(IWorkbook workbook, string name)
        {
            for (int i = 0; i < workbook.NumberOfSheets; i++)
            {
                var sheet = workbook.GetSheetAt(i);
                if (sheet.SheetName == name)
                {
                    return sheet;
                }
            }

            return null;
        }
        
        /// <summary>
        /// 获得单元格的值
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static string GetCellValue(ICell cell)
        {
            if (cell == null) return string.Empty;
            var str = cell.CellType switch
            {
                CellType.Unknown => string.Empty,
                CellType.Numeric => cell.NumericCellValue.ToString(CultureInfo.InvariantCulture),
                CellType.String => cell.StringCellValue,
                CellType.Formula => cell.StringCellValue,
                CellType.Blank => string.Empty,
                CellType.Boolean => cell.BooleanCellValue.ToString(),
                CellType.Error => cell.ErrorCellValue.ToString(),
                _ => throw new ArgumentOutOfRangeException()
            };

            return str.Trim();
        }
        
        /// <summary>
        /// 获得单元格的列index
        /// </summary>
        /// <param name="row"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static int GetColumnIndex(IRow row, string name)
        {
            var index = -1;
            if (row == null || string.IsNullOrEmpty(name)) return index;
            foreach (var cell in row.Cells)
            {
                if (GetCellValue(cell).ToLower().Trim() != name.ToLower().Trim()) continue;
                index = cell.ColumnIndex;
                break;
            }

            return index;
        }
        
        public static ICell GetCellByName(IRow row, string name)
        {
            foreach (var cell in row.Cells)
            {
                if (cell.StringCellValue == name)
                {
                    return cell;
                }
            }

            return null;
        }
    }
}
