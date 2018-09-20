using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Exchange.WebServices.Data;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;

public class EppLusExcel
{
    #region Defintions

    public string DateFormat { get; set; }

    public Color HeaderBackColor { get; set; }

    public Color HeaderFontColor { get; set; }

    public Color ResultBackColor { get; set; }

    public Color ResultForeColor { get; set; }

    public Color ErrorBackColor { get; set; }

    public Color ErrorForeColor { get; set; }

    public bool HasHeader { get; set; }

    public EpPlusType ReadTextOrValue { get; set; }

    #endregion

    #region Contructor

    public EppLusExcel()
    {
        this.HasHeader = true;
        this.ReadTextOrValue = EpPlusType.Value;
        this.HeaderBackColor = Color.FromArgb(112, 48, 160);
        this.HeaderFontColor = Color.White;

        this.ResultBackColor = Color.Yellow;
        this.ResultForeColor = Color.Black;

        this.ErrorBackColor = Color.Red;
        this.ErrorForeColor = Color.Black;
    }

    #endregion

    #region Create

    
    public void CreateExcelFromTemplate(string templatePath, string outputPath, DataSet ds, List<string> ignoreSheets)
    {
        // Copy template to output path
        File.Copy(templatePath, outputPath, true);

        // Open the output path
        var file = new FileInfo(outputPath);

        using (var package = new ExcelPackage(file))
        {
            var workBook = package.Workbook;
            if (workBook == null)
            {
                return;
            }

            if (workBook.Worksheets.Count <= 0)
            {
                return;
            }

            if (ignoreSheets != null)
            {
                // Delete not needed sheets
                foreach (var sheetName in ignoreSheets)
                {
                    if (workBook.Worksheets[sheetName] == null)
                    {
                        continue;
                    }

                    workBook.Worksheets.Delete(sheetName);
                }
            }

            foreach (DataTable table in ds.Tables)
            {
                var sheet = package.Workbook.Worksheets[table.TableName];

                try
                {
                    if (sheet != null)
                    {
                        // Add table to the output path
                        this.AddTableToExistingSheet(sheet, table);
                    }
                }
                catch (Exception ex)
                {
                }
            }

            package.Save();
        }
    }

    public void CreateExcelFromTemplate(string templatePath, string outputPath, DataSet ds, List<string> ignoreSheets, int startRowIndex)
    {
        // Copy template to output path
        File.Copy(templatePath, outputPath, true);

        // Open the output path
        var file = new FileInfo(outputPath);

        using (var package = new ExcelPackage(file))
        {
            var workBook = package.Workbook;
            if (workBook == null)
            {
                return;
            }

            if (workBook.Worksheets.Count <= 0)
            {
                return;
            }

            if (ignoreSheets != null)
            {
                // Delete not needed sheets
                foreach (var sheetName in ignoreSheets)
                {
                    if (workBook.Worksheets[sheetName] == null)
                    {
                        continue;
                    }

                    workBook.Worksheets.Delete(sheetName);
                }
            }

            foreach (DataTable table in ds.Tables)
            {
                var sheet = package.Workbook.Worksheets[table.TableName];

                try
                {
                    if (sheet != null)
                    {
                        // Add table to the output path
                        this.AddTableToExistingSheet(sheet, table, startRowIndex);
                    }
                }
                catch (Exception ex)
                {
                }
            }

            package.Save();
        }
    }


    public void CreateExcelFromTemplate(string templatePath, string outputPath, DataSet ds, List<string> ignoreSheets, bool ignoredColumn)
    {
        // Copy template to output path
        File.Copy(templatePath, outputPath, true);

        // Open the output path
        var file = new FileInfo(outputPath);

        using (var package = new ExcelPackage(file))
        {
            var workBook = package.Workbook;
            if (workBook == null)
            {
                return;
            }

            if (workBook.Worksheets.Count <= 0)
            {
                return;
            }

            if (ignoreSheets != null)
            {
                // Delete not needed sheets
                foreach (var sheetName in ignoreSheets)
                {
                    if (workBook.Worksheets[sheetName] == null)
                    {
                        continue;
                    }

                    workBook.Worksheets.Delete(sheetName);
                }
            }

            foreach (DataTable table in ds.Tables)
            {
                var sheet = package.Workbook.Worksheets[table.TableName];

                try
                {
                    if (sheet != null)
                    {
                        // Add table to the output path
                        if (ignoredColumn)
                            sheet.Column(10).Hidden = true;

                        this.AddTableToExistingSheet(sheet, table);
                    }
                }
                catch (Exception ex)
                {
                }
            }

            package.Save();
        }
    }

    public void CreateExcelFile(DataTable dt, string sheetName, string filepath)
    {
        if (File.Exists(filepath))
        {
            File.Delete(filepath);
        }

        var file = new FileInfo(filepath);

        using (var pck = new ExcelPackage(file))
        {
            var ws = pck.Workbook.Worksheets.Add(sheetName);

            this.AddTableToSheet(ws, dt);

            pck.Save();
        }
    }

    public void CreateExcelFile(DataSet ds, string filePath)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        var file = new FileInfo(filePath);

        using (var pck = new ExcelPackage(file))
        {
            foreach (DataTable dt in ds.Tables)
            {
                var sheet = pck.Workbook.Worksheets.Add(dt.TableName);

                this.AddTableToSheet(sheet, dt);
            }

            pck.Save();
        }
    }

    public void CreateExcelFile(DataTable dt, string filepath)
    {
        if (File.Exists(filepath))
        {
            File.Delete(filepath);
        }

        var file = new FileInfo(filepath);

        //using (var pck = new ExcelPackage(file))
        //{
        //    var sheet = pck.Workbook.Worksheets.Add("ForecastPlanned");
        //    this.AddTableToSheet(sheet, dt);
        //    pck.Save();
        //}
        using (ExcelPackage pck = new ExcelPackage(file))
        {
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Accounts");
            ws.Cells["A1"].LoadFromDataTable(dt, true);
            pck.Save();
        }
    }
    public void ExporttoExcel(DataTable table, string filename)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=SitesUploadResult.xlsx");


        using (ExcelPackage pack = new ExcelPackage())
        {
            ExcelWorksheet ws = pack.Workbook.Worksheets.Add(filename);
            
            ws.Cells["A1"].LoadFromDataTable(table, true);
            var ms = new System.IO.MemoryStream();
            pack.SaveAs(ms);
            ms.WriteTo(HttpContext.Current.Response.OutputStream);
        }

        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();

    }

    public void WriteToExistFile(DataTable dt, string filepath, string sheetName)
    {
        if (File.Exists(filepath) == false)
        {
            return;
        }

        var file = new FileInfo(filepath);

        using (var pck = new ExcelPackage(file))
        {
            var sheet = pck.Workbook.Worksheets[sheetName];
            this.AddTableToSheet(sheet, dt);

            pck.Save();
        }
    }
    public void WriteToExistFile(DataTable dt, string filepath, string sheetName, int startRowIndex)
    {
        if (File.Exists(filepath) == false)
        {
            return;
        }
         
        var file = new FileInfo(filepath);

        using (var pck = new ExcelPackage(file))
        {
            var sheet = pck.Workbook.Worksheets[sheetName];
            this.AddTableToSheet(sheet, dt, startRowIndex);

            pck.Save();
        }
    }

    #endregion

    #region Get

    public DataSet GetDataSetFromExcel(string filepath, List<EpPlusColumnType> listofColumnTypes)
    {
        var ds = new DataSet();
        var file = new FileInfo(filepath);

        using (var package = new ExcelPackage(file))
        {
            var workBook = package.Workbook;
            if (workBook == null)
            {
                return ds;
            }

            if (workBook.Worksheets.Count <= 0)
            {
                return ds;
            }

            foreach (var excelWorksheet in package.Workbook.Worksheets)
            {
                var dt = this.GetDataTableFromSheet(excelWorksheet, listofColumnTypes);
                dt.TableName = excelWorksheet.Name;
                ds.Tables.Add(dt);
            }
        }

        return ds;
    }

    public DataSet GetDataSetFromExcel(string filepath, List<EpPlusColumnType> listofColumnTypes, int startRowNumber)
    {
        var ds = new DataSet();
        var file = new FileInfo(filepath);

        using (var package = new ExcelPackage(file))
        {
            var workBook = package.Workbook;
            if (workBook == null)
            {
                return ds;
            }

            if (workBook.Worksheets.Count <= 0)
            {
                return ds;
            }

            foreach (var excelWorksheet in package.Workbook.Worksheets)
            {
                var dt = this.GetDataTableFromSheet(excelWorksheet, listofColumnTypes, startRowNumber);
                dt.TableName = excelWorksheet.Name;
                ds.Tables.Add(dt);
            }
        }

        return ds;
    }

    private DataTable GetDataTableFromSheet(ExcelWorksheet ws, List<EpPlusColumnType> listofColumnTypes)
    {
        using (new ExcelPackage())
        {
            var tbl = new DataTable();

            var index = 1;
            foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
            {
                var columnName = this.HasHeader
                                     ? firstRowCell.Text
                                     : string.Format("Column {0}", firstRowCell.Start.Column);

                var t = typeof(string);

                var epPlusColumnType = listofColumnTypes.FirstOrDefault(c => c.ColumnName == columnName);
                if (epPlusColumnType != null)
                {
                    t = epPlusColumnType.ColumnType;
                }

                var column = new DataColumn(columnName, t);
                tbl.Columns.Add(column);
                index++;
            }

            // Add new Column to save error list             
            tbl.Columns.Add("ListOfEpPlusErrorType", typeof(List<EpPlusErrorType>));

            var startRow = this.HasHeader ? 2 : 1;

            for (var rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
            {
                var lpErrors = new List<EpPlusErrorType>();
                var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                var row = tbl.NewRow();

                foreach (var cell in wsRow)
                {
                    var column = tbl.Columns[cell.Start.Column - 1];
                    if (cell.Style.Numberformat.Format.ToLower().Contains("d")
                        || cell.Style.Numberformat.Format.ToLower().Contains("m")
                        || cell.Style.Numberformat.Format.ToLower().Contains("yy")
                        || cell.Value is DateTime)
                    {
                        // Customization for Milestones sheet only.
                        // in case of Status column data type is date not general
                        if (column.ColumnName == "Status")
                        {
                            try
                            {
                                row[cell.Start.Column - 1] = this.ReadTextOrValue == EpPlusType.Text
                                                                 ? cell.Text
                                                                 : (cell.Value + string.Empty == string.Empty ? DBNull.Value : cell.Value);
                            }
                            catch
                            {
                                row[cell.Start.Column - 1] = DBNull.Value;
                            }

                            continue;
                        }

                        if (cell.Value == null)
                        {
                            row[cell.Start.Column - 1] = DBNull.Value;
                        }
                        else if (cell.Value.ToString().Length == 0)
                        {
                            row[cell.Start.Column - 1] = DBNull.Value;
                        }
                        else
                        {
                            if (cell.Value != null)
                            {
                                if (cell.Value is DateTime)
                                {
                                    row[cell.Start.Column - 1] = cell.Value;
                                }
                                else
                                {
                                    long serialDate;
                                    if (long.TryParse(cell.Value.ToString(), out serialDate))
                                    {
                                        row[cell.Start.Column - 1] = DateTime.FromOADate(serialDate);
                                    }
                                    else
                                    {
                                        row[cell.Start.Column - 1] = DBNull.Value;
                                        lpErrors.Add(
                                            new EpPlusErrorType
                                            {
                                                Text = cell.Value + string.Empty,
                                                Columnindex = cell.Start.Column - 1,
                                                Rowindex = tbl.Rows.Count,
                                                ColumnName = column.ColumnName
                                            });
                                    }
                                }
                            }
                            else
                            {
                                row[cell.Start.Column - 1] = DBNull.Value;
                            }
                        }
                    }
                    else if (tbl.Columns[cell.Start.Column - 1].DataType == typeof(Decimal))
                    {
                        if (cell.Value != null)
                        {
                            decimal serialDate;
                            if (decimal.TryParse(cell.Value.ToString(), out serialDate))
                            {
                                row[cell.Start.Column - 1] = cell.Value;
                            }
                            else
                            {
                                row[cell.Start.Column - 1] = DBNull.Value;
                                lpErrors.Add(new EpPlusErrorType() { Text = cell.Value + string.Empty, Columnindex = cell.Start.Column - 1, Rowindex = tbl.Rows.Count, ColumnName = column.ColumnName });
                            }
                        }
                        else
                        {
                            row[cell.Start.Column - 1] = DBNull.Value;
                        }
                    }
                    else
                    {
                        try
                        {
                            row[cell.Start.Column - 1] = this.ReadTextOrValue == EpPlusType.Text
                                                             ? cell.Text
                                                             : (cell.Value + string.Empty == string.Empty ? DBNull.Value : cell.Value);
                        }
                        catch
                        {
                            row[cell.Start.Column - 1] = DBNull.Value;
                        }
                    }
                }

                row["ListOfEpPlusErrorType"] = lpErrors;
                tbl.Rows.Add(row);
            }

            return tbl;
        }
    }

    private DataTable GetDataTableFromSheet(ExcelWorksheet ws, List<EpPlusColumnType> listofColumnTypes, int startRowNumber)
    {
        using (new ExcelPackage())
        {
            var tbl = new DataTable();

            var index = 1;
            foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
            {
                var columnName = this.HasHeader
                                     ? firstRowCell.Text
                                     : string.Format("Column {0}", firstRowCell.Start.Column);

                var t = typeof(string);

                var epPlusColumnType = listofColumnTypes.FirstOrDefault(c => c.ColumnName == columnName);
                if (epPlusColumnType != null)
                {
                    t = epPlusColumnType.ColumnType;
                }

                var column = new DataColumn(columnName, t);
                tbl.Columns.Add(column);
                index++;
            }

            // Add new Column to save error list             
            tbl.Columns.Add("ListOfEpPlusErrorType", typeof(List<EpPlusErrorType>));

            if (!ws.Row(1).Hidden)
            {
                startRowNumber = 1;
            }

            var startRow = this.HasHeader ? (startRowNumber + 1) : startRowNumber;

            for (var rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
            {
                var lpErrors = new List<EpPlusErrorType>();
                var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                var row = tbl.NewRow();

                foreach (var cell in wsRow)
                {
                    var column = tbl.Columns[cell.Start.Column - 1];
                    if (cell.Style.Numberformat.Format.ToLower().Contains("d")
                        || cell.Style.Numberformat.Format.ToLower().Contains("m")
                        || cell.Style.Numberformat.Format.ToLower().Contains("yy")
                        || cell.Value is DateTime)
                    {
                        // Customization for Milestones sheet only.
                        // in case of Status column data type is date not general
                        if (column.ColumnName == "Status")
                        {
                            try
                            {
                                row[cell.Start.Column - 1] = this.ReadTextOrValue == EpPlusType.Text
                                                                 ? cell.Text
                                                                 : (cell.Value + string.Empty == string.Empty ? DBNull.Value : cell.Value);
                            }
                            catch
                            {
                                row[cell.Start.Column - 1] = DBNull.Value;
                            }

                            continue;
                        }

                        if (cell.Value == null)
                        {
                            row[cell.Start.Column - 1] = DBNull.Value;
                        }
                        else if (cell.Value.ToString().Length == 0)
                        {
                            row[cell.Start.Column - 1] = DBNull.Value;
                        }
                        else
                        {
                            if (cell.Value != null)
                            {
                                if (cell.Value is DateTime)
                                {
                                    row[cell.Start.Column - 1] = cell.Value;
                                }
                                else
                                {
                                    long serialDate;
                                    if (long.TryParse(cell.Value.ToString(), out serialDate))
                                    {
                                        row[cell.Start.Column - 1] = DateTime.FromOADate(serialDate);
                                    }
                                    else
                                    {
                                        row[cell.Start.Column - 1] = DBNull.Value;
                                        lpErrors.Add(
                                            new EpPlusErrorType
                                            {
                                                Text = cell.Value + string.Empty,
                                                Columnindex = cell.Start.Column - 1,
                                                Rowindex = tbl.Rows.Count,
                                                ColumnName = column.ColumnName
                                            });
                                    }
                                }
                            }
                            else
                            {
                                row[cell.Start.Column - 1] = DBNull.Value;
                            }
                        }
                    }
                    else if (tbl.Columns[cell.Start.Column - 1].DataType == typeof(Decimal))
                    {
                        if (cell.Value != null)
                        {
                            decimal serialDate;
                            if (decimal.TryParse(cell.Value.ToString(), out serialDate))
                            {
                                row[cell.Start.Column - 1] = cell.Value;
                            }
                            else
                            {
                                row[cell.Start.Column - 1] = DBNull.Value;
                                lpErrors.Add(new EpPlusErrorType() { Text = cell.Value + string.Empty, Columnindex = cell.Start.Column - 1, Rowindex = tbl.Rows.Count, ColumnName = column.ColumnName });
                            }
                        }
                        else
                        {
                            row[cell.Start.Column - 1] = DBNull.Value;
                        }
                    }
                    else
                    {
                        try
                        {
                            row[cell.Start.Column - 1] = this.ReadTextOrValue == EpPlusType.Text
                                                             ? cell.Text
                                                             : (cell.Value + string.Empty == string.Empty ? DBNull.Value : cell.Value);
                        }
                        catch
                        {
                            row[cell.Start.Column - 1] = DBNull.Value;
                        }
                    }
                }

                row["ListOfEpPlusErrorType"] = lpErrors;
                tbl.Rows.Add(row);
            }

            return tbl;
        }
    }

    public DataTable GetDataTableFromExcel(string filepath, string sheetName)
    {
        var file = new FileInfo(filepath);
        using (var package = new ExcelPackage(file))
        {
            var workBook = package.Workbook;

            if (workBook != null)
            {
                if (workBook.Worksheets.Count > 0)
                {
                    var ws = package.Workbook.Worksheets[sheetName];
                    var tbl = this.GetDataTableFromSheet(ws);
                    return tbl;
                }
            }
        }

        return null;
    }

    public DataTable GetDataTableFromExcel(string filepath, string sheetName, int startRowIndex)
    {
        var file = new FileInfo(filepath);
        using (var package = new ExcelPackage(file))
        {
            var workBook = package.Workbook;

            if (workBook != null)
            {
                if (workBook.Worksheets.Count > 0)
                {
                    var ws = package.Workbook.Worksheets[sheetName];
                    var tbl = this.GetDataTableFromSheet(ws, startRowIndex);
                    return tbl;
                }
            }
        }

        return null;
    }

    public DataSet GetDataSetFromExcel(string filepath)
    {
        var resultDataSet = new DataSet();
        var file = new FileInfo(filepath);

        using (var package = new ExcelPackage(file))
        {
            var workBook = package.Workbook;
            if (workBook == null)
            {
                return resultDataSet;
            }

            if (workBook.Worksheets.Count <= 0)
            {
                return resultDataSet;
            }

            foreach (var excelWorksheet in package.Workbook.Worksheets)
            {
                // Get the Sheet as a Data Table
                var sheetTable = this.GetDataTableFromSheet(excelWorksheet);

                // Set Table Name
                sheetTable.TableName = excelWorksheet.Name;

                // Add Sheet Table to the Final Result Data Set
                resultDataSet.Tables.Add(sheetTable);
            }
        }

        return resultDataSet;
    }

    #endregion

    #region Helper Methods

    private void AddTableToSheet(ExcelWorksheet ws, DataTable dt)
    {
        if (dt.Columns.Contains("ListOfEpPlusErrorType"))
        {
            dt.Columns.Remove(dt.Columns["ListOfEpPlusErrorType"]);
        }

        // Headers
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            var column = dt.Columns[i];
            this.FormatColumn(ws, column, i);

            ws.Cells[1, i + 1].Value = column.ColumnName;
            ws.Cells[1, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;

            if (column.ColumnName == "Result")
            {
                ws.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(this.ResultBackColor);
                ws.Cells[1, i + 1].Style.Font.Color.SetColor(this.ResultForeColor);
            }
            else
            {
                ws.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(this.HeaderBackColor);
                ws.Cells[1, i + 1].Style.Font.Color.SetColor(this.HeaderFontColor);
            }

            ws.Cells[1, i + 1].Style.Font.Bold = true;
            ws.Column(i + 1).AutoFit();

            // Hide Column if Prefix is not empty
            if (column.Prefix.ToLower() == "hide" || column.ColumnName.ToLower().Contains("_id"))
            {
                // Hide
                ws.Column(i + 1).Hidden = true;
                ws.Column(i + 1).Style.Hidden = true;

                // Background
                ws.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(Color.Red);
                ws.Cells[1, i + 1].Style.Font.Color.SetColor(Color.Red);
            }
        }

        // Rows
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            var row = dt.Rows[i];
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                ws.Cells[i + 2, j + 1].Value = row[j];

                if (row[j].ToString().Contains("Error:"))
                {
                    ws.Cells[i + 2, j + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ws.Cells[i + 2, j + 1].Style.Fill.BackgroundColor.SetColor(this.ErrorBackColor);
                    ws.Cells[i + 2, j + 1].Style.Font.Color.SetColor(this.ErrorForeColor);
                }
            }
        }
    }

    private void AddTableToSheet(ExcelWorksheet ws, DataTable dt, int startRowIndex)
    {
        if (dt.Columns.Contains("ListOfEpPlusErrorType"))
        {
            dt.Columns.Remove(dt.Columns["ListOfEpPlusErrorType"]);
        }

        // Headers
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            var column = dt.Columns[i];
            this.FormatColumn(ws, column, i);

            ws.Cells[1, i + 1].Value = column.ColumnName;
            ws.Cells[1, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;

            if (column.ColumnName == "Result")
            {
                ws.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(this.ResultBackColor);
                ws.Cells[1, i + 1].Style.Font.Color.SetColor(this.ResultForeColor);
            }
            else
            {
                ws.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(this.HeaderBackColor);
                ws.Cells[1, i + 1].Style.Font.Color.SetColor(this.HeaderFontColor);
            }

            ws.Cells[1, i + 1].Style.Font.Bold = true;
            ws.Column(i + 1).AutoFit();

            // Hide Column if Prefix is not empty
            if (column.Prefix.ToLower() == "hide" || column.ColumnName.ToLower().Contains("_id"))
            {
                // Hide
                ws.Column(i + 1).Hidden = true;
                ws.Column(i + 1).Style.Hidden = true;

                // Background
                ws.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(Color.Red);
                ws.Cells[1, i + 1].Style.Font.Color.SetColor(Color.Red);
            }
        }

        // Rows
        for (int i = startRowIndex; i < dt.Rows.Count; i++)
        {
            var row = dt.Rows[i];
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                ws.Cells[i + 2, j + 1].Value = row[j];

                if (row[j].ToString().Contains("Error:"))
                {
                    ws.Cells[i + 2, j + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ws.Cells[i + 2, j + 1].Style.Fill.BackgroundColor.SetColor(this.ErrorBackColor);
                    ws.Cells[i + 2, j + 1].Style.Font.Color.SetColor(this.ErrorForeColor);
                }
            }
        }
    }

    private void AddTableToExistingSheet(ExcelWorksheet ws, DataTable dt)
    {
        if (dt.Columns.Contains("ListOfEpPlusErrorType"))
        {
            dt.Columns.Remove(dt.Columns["ListOfEpPlusErrorType"]);
        }

        // Get Column Names From Excel File
        var columnNames = new List<string>();
        foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
        {
            var columnName = firstRowCell.Value ?? string.Empty;
            columnNames.Add(columnName.ToString());
        }

        // Format Columns
        for (var i = 0; i < columnNames.Count; i++)
        {
            var columnName = columnNames[i];
            var column = dt.Columns[columnName];

            this.FormatColumn(ws, column, i);
        }

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            var row = dt.Rows[i];
            if (row == null)
            {
                continue;
            }

            for (var j = 0; j < columnNames.Count; j++)
            {
                var columnName = columnNames[j];
                var column = dt.Columns[columnName];

                if (column == null)
                {
                    continue;
                }

                if (column.DataType == typeof(DateTime))
                {
                    if (row.IsNull(columnName))
                    {
                        ws.Cells[i + 2, j + 1].Value = row[columnName];
                        continue;
                    }

                    ws.Cells[i + 2, j + 1].Value = ((DateTime)row[columnName]).ToOADate();
                    continue;
                }

                ws.Cells[i + 2, j + 1].Value = row[columnName];

                // Set Error Cell Format to RED
                if (row[columnName].ToString().Contains("Error:"))
                {
                    ws.Cells[i + 2, j + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ws.Cells[i + 2, j + 1].Style.Fill.BackgroundColor.SetColor(this.ErrorBackColor);
                    ws.Cells[i + 2, j + 1].Style.Font.Color.SetColor(this.ErrorForeColor);
                }
            }
        }
    }

    private void AddTableToExistingSheet(ExcelWorksheet ws, DataTable dt, int startRowIndex)
    {
        if (dt.Columns.Contains("ListOfEpPlusErrorType"))
        {
            dt.Columns.Remove(dt.Columns["ListOfEpPlusErrorType"]);
        }

        // Get Column Names From Excel File
        var columnNames = new List<string>();
        foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
        {
            var columnName = firstRowCell.Value ?? string.Empty;
            columnNames.Add(columnName.ToString());
        }

        // Format Columns
        for (var i = 0; i < columnNames.Count; i++)
        {
            var columnName = columnNames[i];
            var column = dt.Columns[columnName];

            this.FormatColumn(ws, column, i);
        }

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            var row = dt.Rows[i];
            if (row == null)
            {
                continue;
            }

            for (var j = 0; j < columnNames.Count; j++)
            {
                var columnName = columnNames[j];
                var column = dt.Columns[columnName];

                if (column == null)
                {
                    continue;
                }

                if (column.DataType == typeof(DateTime))
                {
                    if (row.IsNull(columnName))
                    {
                        ws.Cells[i + 2 + startRowIndex, j + 1].Value = row[columnName];
                        continue;
                    }

                    ws.Cells[i + 2 + startRowIndex, j + 1].Value = ((DateTime)row[columnName]).ToOADate();
                    continue;
                }

                ws.Cells[i + 2 + startRowIndex, j + 1].Value = row[columnName];

                // Set Error Cell Format to RED
                if (row[columnName].ToString().Contains("Error:"))
                {
                    ws.Cells[i + 2 + startRowIndex, j + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ws.Cells[i + 2 + startRowIndex, j + 1].Style.Fill.BackgroundColor.SetColor(this.ErrorBackColor);
                    ws.Cells[i + 2 + startRowIndex, j + 1].Style.Font.Color.SetColor(this.ErrorForeColor);
                }
            }
        }
    }

    private void FormatColumn(ExcelWorksheet ws, DataColumn column, int index)
    {
        if (column == null)
        {
            return;
        }

        var columnFormat = column.ColumnName == column.Caption ? string.Empty : column.Caption;

        if (column.DataType == typeof(DateTime))
        {
            this.DateFormat = "m/d/yyyy";
            ws.Column(index + 1).Style.Numberformat.Format = string.IsNullOrEmpty(columnFormat)
                                                                 ? this.DateFormat
                                                                 : columnFormat;
            return;
        }

        if (column.DataType == typeof(float)
            || column.DataType == typeof(double)
            || column.DataType == typeof(decimal))
        {

            ws.Column(index + 1).Style.Numberformat.Format = columnFormat == "int" ? "@" : "#,##0.00";
            return;
        }

        ws.Column(index + 1).Style.Numberformat.Format = "@";
    }

    private DataTable GetDataTableFromSheet(ExcelWorksheet excelWorksheet)
    {
        // this.DateFormat = "m/d/yyyy";
        this.DateFormat = "g"; // 2/27/2009 12:12 PM
        using (new ExcelPackage())
        {
            var resultDataTable = new DataTable();

            // Read Columns
            var index = 1;
            foreach (var firstRowCell in excelWorksheet.Cells[1, 1, 1, excelWorksheet.Dimension.End.Column])
            {
                var columnName = this.HasHeader
                    ? firstRowCell.Value + ""
                                     : string.Format("Column {0}", firstRowCell.Start.Column);

                var columnType = typeof(string);
                var cell = excelWorksheet.Cells[2, index];

                if (cell.Style.Numberformat.Format.ToLower().Contains("d")
                    || cell.Style.Numberformat.Format.ToLower().Contains("m")
                    || cell.Style.Numberformat.Format.ToLower().Contains("yy")
                    || cell.Value is DateTime)
                {
                    // Date Time Type
                    columnType = typeof(DateTime);
                }
                else if (cell.Value is Decimal
                    || cell.Value is double
                    || cell.Value is int)
                {
                    // Decimal Type
                    columnType = typeof(Decimal);
                }

                // Create the column instance
                var column = new DataColumn(columnName, columnType);

                // Add the column to the Final Result DataSet
                resultDataTable.Columns.Add(column);

                index++;
            }

            // Add new Column to save error list             
           // resultDataTable.Columns.Add("ListOfEpPlusErrorType", typeof(List<EpPlusErrorType>));

            // Read Rows
            var startRow = this.HasHeader ? 2 : 1;
            for (var rowNum = startRow; rowNum <= excelWorksheet.Dimension.End.Row; rowNum++)
            {
                var lpErrors = new List<EpPlusErrorType>();

                // Excel Work sheet Row
                var wsRow = excelWorksheet.Cells[rowNum, 1, rowNum, excelWorksheet.Dimension.End.Column];

                // Result Data Table Row
                var row = resultDataTable.NewRow();

                // Iterate through row cells
                foreach (var cell in wsRow)
                {
                    if (resultDataTable.Columns.Count <= cell.Start.Column - 1)
                    {
                        continue;
                    }

                    var column = resultDataTable.Columns[cell.Start.Column - 1];

                    // Date Time Cell
                    if (cell.Style.Numberformat.Format.ToLower().Contains("d")
                        || cell.Style.Numberformat.Format.ToLower().Contains("m")
                        || cell.Style.Numberformat.Format.ToLower().Contains("yy")
                        || cell.Value is DateTime)
                    {
                        if (cell.Value == null || cell.Value == DBNull.Value || cell.Value.ToString().Length == 0)
                        {
                            // Null cell value
                            row[cell.Start.Column - 1] = DBNull.Value;
                            continue;
                        }

                        double serialDate;
                        if (double.TryParse(cell.Value.ToString(), out serialDate))
                        {
                            // Set Cell Value
                            row[cell.Start.Column - 1] = DateTime.FromOADate(serialDate).ToString(this.DateFormat);
                            continue;
                        }

                        // Error
                        row[cell.Start.Column - 1] = DBNull.Value;
                        lpErrors.Add(
                            new EpPlusErrorType
                            {
                                Text = cell.Value + string.Empty,
                                Columnindex = cell.Start.Column - 1,
                                Rowindex = resultDataTable.Rows.Count,
                                ColumnName = column.ColumnName
                            });
                        continue;
                    }

                    // Decimal Cell
                    if (resultDataTable.Columns[cell.Start.Column - 1].DataType == typeof(Decimal))
                    {
                        if (cell.Value == null)
                        {
                            row[cell.Start.Column - 1] = DBNull.Value;
                            continue;
                        }

                        decimal serialDate;
                        if (decimal.TryParse(cell.Value.ToString(), out serialDate))
                        {
                            row[cell.Start.Column - 1] = cell.Value;
                            continue;
                        }

                        // Error
                        row[cell.Start.Column - 1] = DBNull.Value;
                        lpErrors.Add(
                            new EpPlusErrorType
                            {
                                Text = cell.Value + string.Empty,
                                Columnindex = cell.Start.Column - 1,
                                Rowindex = resultDataTable.Rows.Count,
                                ColumnName = column.ColumnName
                            });
                        continue;
                    }

                    // Text Cell
                    try
                    {
                        row[cell.Start.Column - 1] = this.ReadTextOrValue == EpPlusType.Text
                                                         ? cell.Text
                                                         : (cell.Value + string.Empty == string.Empty ? DBNull.Value : cell.Value);
                    }
                    catch
                    {
                        row[cell.Start.Column - 1] = DBNull.Value;
                    }
                }

                // Set Errors Cell Value
             //   row["ListOfEpPlusErrorType"] = lpErrors;

                // Add the Row to the final result data table
                resultDataTable.Rows.Add(row);
            }

            // Finally return the result data table
            return resultDataTable;
        }
    }

    private DataTable GetDataTableFromSheet(ExcelWorksheet excelWorksheet, int startRowIndex)
    {
        // this.DateFormat = "m/d/yyyy";
        this.DateFormat = "g"; // 2/27/2009 12:12 PM
        using (new ExcelPackage())
        {
            var resultDataTable = new DataTable();

            // Read Columns
            var index = 1;
            foreach (var firstRowCell in excelWorksheet.Cells[1, 1, 1, excelWorksheet.Dimension.End.Column])
            {
                var columnName = this.HasHeader
                    ? firstRowCell.Value + ""
                                     : string.Format("Column {0}", firstRowCell.Start.Column);

                var columnType = typeof(string);
                var cell = excelWorksheet.Cells[2, index];

                if (cell.Style.Numberformat.Format.ToLower().Contains("d")
                    || cell.Style.Numberformat.Format.ToLower().Contains("m")
                    || cell.Style.Numberformat.Format.ToLower().Contains("yy")
                    || cell.Value is DateTime)
                {
                    // Date Time Type
                    columnType = typeof(DateTime);
                }
                else if (cell.Value is Decimal
                    || cell.Value is double
                    || cell.Value is int)
                {
                    // Decimal Type
                    columnType = typeof(Decimal);
                }

                // Create the column instance
                var column = new DataColumn(columnName, columnType);

                // Add the column to the Final Result DataSet
                resultDataTable.Columns.Add(column);

                index++;
            }

            // Add new Column to save error list             
            resultDataTable.Columns.Add("ListOfEpPlusErrorType", typeof(List<EpPlusErrorType>));

            // Read Rows
            //var startRow = this.HasHeader ? 2 : 1;
            var startRow = this.HasHeader ? (startRowIndex + 1) : startRowIndex;
            for (var rowNum = startRow; rowNum <= excelWorksheet.Dimension.End.Row; rowNum++)
            {
                var lpErrors = new List<EpPlusErrorType>();

                // Excel Work sheet Row
                var wsRow = excelWorksheet.Cells[rowNum, 1, rowNum, excelWorksheet.Dimension.End.Column];

                // Result Data Table Row
                var row = resultDataTable.NewRow();

                // Iterate through row cells
                foreach (var cell in wsRow)
                {
                    if (resultDataTable.Columns.Count <= cell.Start.Column - 1)
                    {
                        continue;
                    }

                    var column = resultDataTable.Columns[cell.Start.Column - 1];

                    // Date Time Cell
                    if (cell.Style.Numberformat.Format.ToLower().Contains("d")
                        || cell.Style.Numberformat.Format.ToLower().Contains("m")
                        || cell.Style.Numberformat.Format.ToLower().Contains("yy")
                        || cell.Value is DateTime)
                    {
                        if (cell.Value == null || cell.Value == DBNull.Value || cell.Value.ToString().Length == 0)
                        {
                            // Null cell value
                            row[cell.Start.Column - 1] = DBNull.Value;
                            continue;
                        }

                        double serialDate;
                        if (double.TryParse(cell.Value.ToString(), out serialDate))
                        {
                            // Set Cell Value
                            row[cell.Start.Column - 1] = DateTime.FromOADate(serialDate).ToString(this.DateFormat);
                            continue;
                        }

                        // Error
                        row[cell.Start.Column - 1] = DBNull.Value;
                        lpErrors.Add(
                            new EpPlusErrorType
                            {
                                Text = cell.Value + string.Empty,
                                Columnindex = cell.Start.Column - 1,
                                Rowindex = resultDataTable.Rows.Count,
                                ColumnName = column.ColumnName
                            });
                        continue;
                    }

                    // Decimal Cell
                    if (resultDataTable.Columns[cell.Start.Column - 1].DataType == typeof(Decimal))
                    {
                        if (cell.Value == null)
                        {
                            row[cell.Start.Column - 1] = DBNull.Value;
                            continue;
                        }

                        decimal serialDate;
                        if (decimal.TryParse(cell.Value.ToString(), out serialDate))
                        {
                            row[cell.Start.Column - 1] = cell.Value;
                            continue;
                        }

                        // Error
                        row[cell.Start.Column - 1] = DBNull.Value;
                        lpErrors.Add(
                            new EpPlusErrorType
                            {
                                Text = cell.Value + string.Empty,
                                Columnindex = cell.Start.Column - 1,
                                Rowindex = resultDataTable.Rows.Count,
                                ColumnName = column.ColumnName
                            });
                        continue;
                    }

                    // Text Cell
                    try
                    {
                        row[cell.Start.Column - 1] = this.ReadTextOrValue == EpPlusType.Text
                                                         ? cell.Text
                                                         : (cell.Value + string.Empty == string.Empty ? DBNull.Value : cell.Value);
                    }
                    catch
                    {
                        row[cell.Start.Column - 1] = DBNull.Value;
                    }
                }

                // Set Errors Cell Value
                row["ListOfEpPlusErrorType"] = lpErrors;

                // Add the Row to the final result data table
                resultDataTable.Rows.Add(row);
            }

            // Finally return the result data table
            return resultDataTable;
        }
    }

    #endregion
}

public enum EpPlusType
{
    Text,

    Value
}

public class EpPlusErrorType
{
    public string Text { get; set; }

    public string ColumnName { get; set; }

    public int Columnindex { get; set; }

    public int Rowindex { get; set; }
}

public class EpPlusColumnType
{
    public string ColumnName { get; set; }

    public Type ColumnType { get; set; }

    public override bool Equals(object obj)
    {
        return this.ColumnName.Equals(obj.ToString());
    }
}

