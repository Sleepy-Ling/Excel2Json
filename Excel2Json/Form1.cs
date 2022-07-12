using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NPOI;
using NPOI.SS.UserModel;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Excel2Json
{
    public partial class Form1 : Form
    {

        /// <summary>
        /// 表头序号
        /// </summary>
        private static int tableHeadIndex = 3;

        /// <summary>
        /// * 生成字典型json专用  以某一列为key值
        /// </summary>
        private static int dictJsonKeyIndex = 0;


        /// <summary>
        /// 软件本地路径
        /// </summary>
        private static readonly string LOCAL_PATH = Directory.GetCurrentDirectory();
        /// <summary>
        /// 软件父目录路径
        /// </summary>
        private static readonly string PARENT_PATH = Directory.GetParent(LOCAL_PATH).FullName;

        /// <summary>
        /// 输出文件的目录名
        /// </summary>
        private static readonly string OUT_FILE_DIR = "OutFiles";

        /// <summary>
        /// 配置文件的目录名
        /// </summary>
        private static readonly string CONFIG_FILE_DIR = "Config";

        /// <summary>
        /// 导入的excel路径
        /// </summary>
        private List<string> tempExcelPath = new List<string>();

        /// <summary>
        /// 模板ts文件路径
        /// </summary>
        private string templateFile = Path.Combine(PARENT_PATH, CONFIG_FILE_DIR + @"\TemplateClass.ts");

        private static readonly string OutJsonPathKey = "OutJsonPath";
        private static readonly string OutTSPathKey = "OutTSPath";
        private static readonly string ExcelPathKey = "excelPath";
        private static readonly string ProjectPathKey = "outFilePath";

        private List<IWorkbook> workbooks;


        private string OutJsonPath = "";
        private string OutTSPath = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.workbooks = new List<IWorkbook>();
            try
            {
                string configDirPath = Path.Combine(PARENT_PATH, CONFIG_FILE_DIR);//配置文件的目录地址
                AutoCreateDirectoryIfNotExist(configDirPath);

                string fileConfigPath = Path.Combine(configDirPath, "FileConfig.json");//配置文件的绝对路径
                string content = File.ReadAllText(fileConfigPath);
                JObject jObject = JsonConvert.DeserializeObject<JObject>(content);
                string filePath = jObject[ExcelPathKey].ToString();
                this.onOpenFile(filePath);
                this.DragFileTextBox.Text = filePath;

                filePath = jObject[ProjectPathKey] == null ? "" : jObject[ProjectPathKey].ToString();
                this.OutFileTextBox.Text = filePath;

                this.AllowDrop = true;
                this.CheckFilePanel.AllowDrop = true;
                this.CheckFilePanel.DragEnter += new System.Windows.Forms.DragEventHandler(this.onFileDragEnter);
                this.CheckFilePanel.DragDrop += new System.Windows.Forms.DragEventHandler(this.onFileDragDrop);

                this.OutFilePanel.AllowDrop = true;
                this.OutFilePanel.DragEnter += new System.Windows.Forms.DragEventHandler(this.onFileDragEnter);
                this.OutFilePanel.DragDrop += new System.Windows.Forms.DragEventHandler(this.onFileDragDrop);

                if (jObject[ProjectPathKey] == null|| jObject[ProjectPathKey].ToString() == "")
                {
                    OutTSPath = OutJsonPath = Path.Combine(PARENT_PATH, OUT_FILE_DIR);//配置文件的目录地址;
                }
                else
                {
                    string projectPath = jObject[ProjectPathKey].ToString();
                    OutJsonPath = Path.Combine(projectPath, @"assets\config");
                    OutTSPath = Path.Combine(projectPath, @"scripts\tableData");
                }

            }
            catch (Exception err)
            {
                Console.Write(err);
            }

        }

        /// <summary>
        /// 保存json文件按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveJsonBtn_Click(object sender, EventArgs e)
        {
            if (workbooks.Count == 0)
            {
                MessageBox.Show("excel地址为空");
                return;
            }

            foreach (IWorkbook currentWorkBook in workbooks)
            {
                string[] outString = this.createDictObjJsonBySheet(currentWorkBook);
                string outDirPath = OutJsonPath;
                AutoCreateDirectoryIfNotExist(outDirPath);

                int sheetNum = currentWorkBook.NumberOfSheets;
                for (int i = 0; i < sheetNum; i++)//遍历每个表单
                {
                    ISheet sheet = currentWorkBook.GetSheetAt(i);
                    string outPath = Path.Combine(outDirPath, "Table_" + sheet.SheetName + ".json");//配置文件的绝对路径

                    StreamWriter streamWriter = new StreamWriter(outPath, false, System.Text.Encoding.UTF8);
                    streamWriter.AutoFlush = true;//每次调用write 方法则将数据写入基础流（文件）  如果为false，则每次调用完write()后，调用flush()或close()，才将数据写入基础流。
                    streamWriter.WriteLine(outString[i]);
                    streamWriter.Close();
                }

            }

            MessageBox.Show("配置导出成功");
        }

        /// <summary>
        /// 保存数据类型按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveTsBtn_Click(object sender, EventArgs e)
        {
            if (workbooks.Count == 0)
            {
                MessageBox.Show("excel地址为空");
                return;
            }

            foreach (IWorkbook currentWorkBook in workbooks)
            {
                string[] outString = this.createTsClass(templateFile, currentWorkBook);
                string outDirPath = OutTSPath;//配置文件的目录地址
                AutoCreateDirectoryIfNotExist(outDirPath);

                int sheetNum = currentWorkBook.NumberOfSheets;
                for (int i = 0; i < sheetNum; i++)//遍历每个表单
                {
                    ISheet sheet = currentWorkBook.GetSheetAt(i);
                    string outPath = Path.Combine(outDirPath, "TBDATA_" + sheet.SheetName + ".ts");//配置文件的绝对路径

                    StreamWriter streamWriter = new StreamWriter(outPath, false, System.Text.Encoding.UTF8);
                    streamWriter.AutoFlush = true;//每次调用write 方法则将数据写入基础流（文件）  如果为false，则每次调用完write()后，调用flush()或close()，才将数据写入基础流。
                    streamWriter.WriteLine(outString[i]);
                    streamWriter.Close();
                }
            }
            MessageBox.Show("类型定义脚本导出成功");
        }

        /// <summary>
        /// 根据表单生成字典型json
        /// </summary>
        /// <param name="workbook"></param>
        /// <returns></returns>
        private string[] createDictObjJsonBySheet(IWorkbook workbook)
        {
            int sheetNum = workbook.NumberOfSheets;

            string[] strArr = new string[sheetNum];

            for (int i = 0; i < sheetNum; i++)//遍历每个表单
            {
                ISheet sheet = workbook.GetSheetAt(i);


                int rows = sheet.PhysicalNumberOfRows;//行数
                if (rows.Equals(0))
                {
                    continue;
                }

                IRow nameRow = sheet.GetRow(0);//取第一行为字段名
                IRow typeRow = sheet.GetRow(1);//取第二行为类型值


                //字段行
                List<ICell> allNameCells = nameRow.Cells;
                //类型行
                List<ICell> allTypeCells = typeRow.Cells;

                int cols = allNameCells.Count;

                int startReadRowIndex = Form1.tableHeadIndex;
                System.Collections.IEnumerator enumerator = sheet.GetRowEnumerator();

                JObject outObj = new JObject();

                while (enumerator.MoveNext())
                {
                    if (startReadRowIndex > 0)
                    {
                        --startReadRowIndex;
                        continue;
                    }

                    IRow currentRow = enumerator.Current as IRow;//当前需要读取的行数据
                    bool isEmptyRow = false;//当前行是否作废

                    JObject rowObj = new JObject();
                    for (int j = 0; j < cols; j++)
                    {
                        ICell cell = currentRow.GetCell(j);

                        if (j == Form1.dictJsonKeyIndex)//取第一列为key
                        {
                            if (cell == null)//如果第一列是空的格子，则没必要继续初始化下去了
                            {
                                isEmptyRow = true;
                                break;
                            }
                            continue;
                        }

                        if (cell == null)
                        {
                            rowObj[allNameCells[j].StringCellValue] = null;
                            continue;
                        }
                        else
                        {
                            if (allTypeCells[j].StringCellValue == "string")
                            {
                                string str = "";
                                try
                                {
                                    str = cell.StringCellValue;
                                }
                                catch (Exception e)
                                {

                                }

                                JArray array = this.tryDeserializeObject<JArray>(str);//尝试转成需要类型

                                if (array != null)
                                {
                                    rowObj[allNameCells[j].StringCellValue] = array;
                                    continue;
                                }

                                JObject tempObj = this.tryDeserializeObject<JObject>(str);//尝试转成需要类型
                                if (tempObj != null)
                                {
                                    rowObj[allNameCells[j].StringCellValue] = tempObj;
                                    continue;
                                }

                                str = str == null ? "" : str;

                                rowObj[allNameCells[j].StringCellValue] = str;
                            }
                            else if (allTypeCells[j].StringCellValue == "int" || allTypeCells[j].StringCellValue == "number")
                            {
                                rowObj[allNameCells[j].StringCellValue] = cell.NumericCellValue;
                            }
                            else if (allTypeCells[j].StringCellValue == "boolean")
                            {
                                rowObj[allNameCells[j].StringCellValue] = cell.BooleanCellValue;
                            }
                            else
                            {
                                rowObj[allNameCells[j].StringCellValue] = cell.StringCellValue;
                            }

                        }

                    }

                    if (!isEmptyRow)
                    {
                        JToken jToken = JToken.FromObject(rowObj);
                        ICell headCell = currentRow.GetCell(Form1.dictJsonKeyIndex);
                        if (allTypeCells[Form1.dictJsonKeyIndex].StringCellValue == "int" || allTypeCells[Form1.dictJsonKeyIndex].StringCellValue == "number")
                        {
                            outObj.Add(headCell.NumericCellValue.ToString(), jToken);
                        }
                        else if (headCell.StringCellValue != "")
                        {
                            outObj.Add(headCell.StringCellValue, jToken);
                        }

                    }

                }

                string json = JsonConvert.SerializeObject(outObj, Formatting.Indented);
                Console.WriteLine("输出的json:" + json);
                strArr[i] = json;
            }

            return strArr;
        }

        /// <summary>
        /// 通过每个表单生成 数组型json  建议使用这方法
        /// </summary>
        /// <param name="workbook"></param>
        /// <returns></returns>
        private string[] createArrayJsonBySheetNew(IWorkbook workbook)
        {
            int sheetNum = workbook.NumberOfSheets;

            string[] strArr = new string[sheetNum];

            for (int i = 0; i < sheetNum; i++)//遍历每个表单
            {
                ISheet sheet = workbook.GetSheetAt(i);

                int rows = sheet.PhysicalNumberOfRows;//行数
                if (rows.Equals(0))
                {
                    continue;
                }

                IRow nameRow = sheet.GetRow(0);//取第一行为字段名
                IRow typeRow = sheet.GetRow(1);//取第二行为类型值

                List<ICell> allNameCells = nameRow.Cells;
                List<ICell> allTypeCells = typeRow.Cells;

                int startReadRowIndex = Form1.tableHeadIndex;
                System.Collections.IEnumerator enumerator = sheet.GetRowEnumerator();

                JArray jArray = new JArray();

                while (enumerator.MoveNext())
                {
                    if (startReadRowIndex > 0)
                    {
                        --startReadRowIndex;
                        continue;
                    }

                    IRow currentRow = enumerator.Current as IRow;//当前需要读取的行数据
                    List<ICell> currentCellsList = currentRow.Cells;//当前行的每一列集合

                    JObject rowObj = new JObject();
                    for (int j = 0; j < currentCellsList.Count; j++)
                    {
                        ICell cell = currentCellsList[j];

                        if (allTypeCells[j].StringCellValue == "string")
                        {
                            JArray array = this.tryDeserializeObject<JArray>(cell.StringCellValue);//尝试转成需要类型

                            if (array != null)
                            {
                                rowObj[allNameCells[j].StringCellValue] = array;
                                continue;
                            }

                            JObject tempObj = this.tryDeserializeObject<JObject>(cell.StringCellValue);//尝试转成需要类型
                            if (tempObj != null)
                            {
                                rowObj[allNameCells[j].StringCellValue] = tempObj;
                                continue;
                            }

                            rowObj[allNameCells[j].StringCellValue] = cell.StringCellValue;

                        }
                        else if (allTypeCells[j].StringCellValue == "int" || allTypeCells[j].StringCellValue == "number")
                        {
                            rowObj[allNameCells[j].StringCellValue] = cell.NumericCellValue;
                        }
                        else if (allTypeCells[j].StringCellValue == "boolean")
                        {
                            rowObj[allNameCells[j].StringCellValue] = cell.BooleanCellValue;
                        }



                    }
                    jArray.Add(rowObj);

                }

                string json = JsonConvert.SerializeObject(jArray, Formatting.Indented);
                Console.WriteLine("输出的json:" + json);
                strArr[i] = json;
            }

            return strArr;
        }

        /// <summary>
        /// 通过每个表单生成 数组型json
        /// </summary>
        /// <param name="workbook"></param>
        /// <returns></returns>
        private string[] createArrayJsonBySheet(IWorkbook workbook)
        {
            int sheetNum = workbook.NumberOfSheets;

            string[] strArr = new string[sheetNum];

            for (int i = 0; i < sheetNum; i++)//遍历每个表单
            {
                ISheet sheet = workbook.GetSheetAt(i);
                int rows = sheet.PhysicalNumberOfRows;//行数
                if (rows.Equals(0))
                {
                    continue;
                }

                IRow nameRow = sheet.GetRow(0);//取第一行为字段名
                IRow typeRow = sheet.GetRow(1);//取第二行为类型值
                DataTable dataTable = this.createDataTableByTypeRow(typeRow, nameRow);//创建对应字段的dataTable

                List<ICell> typeList = typeRow.Cells;

                int startReadRowIndex = 3;
                System.Collections.IEnumerator enumerator = sheet.GetRowEnumerator();//获取行的迭代器
                while (enumerator.MoveNext())
                {
                    if (startReadRowIndex > 0)
                    {
                        --startReadRowIndex;
                        continue;
                    }

                    IRow currentRow = enumerator.Current as IRow;//当前需要读取的行数据
                    List<ICell> currentCellsList = currentRow.Cells;//当前行的每一列集合
                    DataRow row = this.createDataRow(dataTable, currentCellsList);
                    dataTable.Rows.Add(row);
                }

                string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
                Console.WriteLine("输出的json:" + json);
                strArr[i] = json;
            }

            return strArr;
        }

        private DataTable createDataTableByTypeRow(IRow TypeRow, IRow NameRow)
        {
            //序列化DataTable
            DataTable dt = new DataTable();

            List<ICell> allTypeCells = TypeRow.Cells;
            List<ICell> allNameCells = NameRow.Cells;
            for (int i = 0; i < allTypeCells.Count; i++)
            {
                ICell cell = allTypeCells[i];
                string name = allNameCells[i].StringCellValue;
                if (cell.StringCellValue == "string")
                {
                    dt.Columns.Add(name, Type.GetType("System.String"));
                }

                else if (cell.StringCellValue == "boolean")
                {
                    dt.Columns.Add(name, Type.GetType("System.Boolean"));
                }

                else if (cell.StringCellValue == "number" || cell.StringCellValue == "int")
                {
                    dt.Columns.Add(name, Type.GetType("System.Int32"));
                }
            }

            return dt;
        }

        private DataRow createDataRow(DataTable dt, List<ICell> cells)
        {
            DataRow dataRow = dt.NewRow();

            DataColumnCollection dataColumnCollection = dt.Columns;

            for (int i = 0; i < dataColumnCollection.Count; i++)
            {
                DataColumn dataColumn = dataColumnCollection[i];

                string name = dataColumn.ColumnName;
                Type type = dataColumn.DataType;

                if (type == Type.GetType("System.Int32"))
                {
                    dataRow[name] = cells[i].NumericCellValue;
                }
                else if (type == Type.GetType("System.Boolean"))
                {
                    dataRow[name] = cells[i].BooleanCellValue;
                }
                else if (type == Type.GetType("System.String"))
                {
                    dataRow[name] = cells[i].StringCellValue;
                }
            }

            return dataRow;

        }

        /// <summary>
        /// 尝试json转换成指定对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        private T tryDeserializeObject<T>(string json)
        {
            T obj;
            try
            {
                obj = JsonConvert.DeserializeObject<T>(json);
            }

            catch (Exception err)
            {
                obj = default(T);
            }
            return obj;
        }

        /// <summary>
        /// 创建对应配置的ts 类文件
        /// </summary>
        /// <param name="templateFilePath"></param>
        /// <param name="workbook"></param>
        /// <returns></returns>
        private string[] createTsClass(string templateFilePath, IWorkbook workbook)
        {
            int sheetNum = workbook.NumberOfSheets;
            string[] strArr = new string[sheetNum];

            for (int i = 0; i < sheetNum; i++)
            {
                ISheet sheet = workbook.GetSheetAt(i);
                string propertyStart = "\r\n    /*-----property end-----*/";
                string summaryStr = "\r\t/**{0} */";
                string propertyStr = "\r\tpublic {0}: {1};\n";
                try
                {
                    string text = File.ReadAllText(templateFilePath);
                    text = text.Replace("$TemplateClass", "TBDATA_" + sheet.SheetName);
                    /* --------------------写入类属性 --------------------*/
                    int startInsertIdx = text.IndexOf(propertyStart);
                    IRow nameRow = sheet.GetRow(0);//取第一行为字段名
                    IRow typeRow = sheet.GetRow(1);//取第二行为类型值
                    IRow summaryRow = sheet.GetRow(2);//取第三行为注释字符串

                    List<ICell> allNameCells = nameRow.Cells;
                    List<ICell> allTypeCells = typeRow.Cells;
                    List<ICell> allSummaryCells = summaryRow.Cells;

                    for (int j = 0; j < allNameCells.Count; j++)
                    {
                        string summary;
                        string property = allNameCells[j].StringCellValue;
                        string type = allTypeCells[j].StringCellValue;
                        if (j >= allSummaryCells.Count)
                        {
                            summary = "  ^ . ^";
                        }
                        else
                        {
                            summary = allSummaryCells[j].StringCellValue;

                        }

                        summary = String.Format(summaryStr, summary);
                        property = String.Format(propertyStr, property, type);

                        text = text.Insert(startInsertIdx, summary);
                        startInsertIdx += summary.Length;
                        text = text.Insert(startInsertIdx, property);
                        startInsertIdx += property.Length;
                    }


                    /* --------------------改写克隆方法 --------------------*/
                    //string cloneStart = "\r\n        return clone";
                    //string cloneStr = "\r\t\t clone.{0} = this.{1};";
                    //int cloneInsertIdx = text.IndexOf(cloneStart);
                    //for (int k = 0; k < allNameCells.Count; k++)
                    //{
                    //    string property = allNameCells[k].StringCellValue;
                    //    string tempClone = String.Format(cloneStr, property, property);
                    //    text = text.Insert(cloneInsertIdx, tempClone);
                    //    cloneInsertIdx += tempClone.Length;
                    //}

                    strArr[i] = text;
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.ToString());
                }
            }

            return strArr;
        }

        private void RefreshFile_Click(object sender, EventArgs e)
        {
            this.loadExcel(tempExcelPath);

        }

        /// <summary>
        /// 文件拖入方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onFileDragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))//判断是否为文件拖拽类型
            {
                e.Effect = DragDropEffects.Link;//是，则将获取目标的链接
            }
        }

        /// <summary>
        /// 文件拖入后释放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onFileDragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            System.Array fileObj = (System.Array)e.Data.GetData(DataFormats.FileDrop);
            //获取单个文件地址
            string filePath = fileObj.GetValue(0).ToString();

            if (this.CheckFilePanel == (sender as Panel))
            {
                this.DragFileTextBox.Text = filePath;
                onOpenFile(filePath);

            }
            else if (this.OutFilePanel == sender)
            {
                this.OutFileTextBox.Text = filePath;
                OutJsonPath = Path.Combine(filePath, @"assets\config");
                OutTSPath = Path.Combine(filePath, @"scripts\tableData");
            }

        }

        private void loadExcel(string excelPaths)
        {
            try
            {
                if (workbooks.Count >= 0)
                {
                    foreach (IWorkbook book in workbooks)
                    {
                        book.Close();
                    }
                    workbooks.Clear();
                }

                FileStream fileStream = new FileStream(excelPaths, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                IWorkbook workbook = WorkbookFactory.Create(fileStream);
                workbooks.Add(workbook);
                fileStream.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }

        }

        private void loadExcel(string[] excelPaths)
        {
            try
            {
                if (workbooks.Count >= 0)
                {
                    foreach (IWorkbook book in workbooks)
                    {
                        book.Close();
                    }
                    workbooks.Clear();
                }

                foreach (string path in excelPaths)
                {
                    FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                    IWorkbook workbook = WorkbookFactory.Create(fileStream);

                    workbooks.Add(workbook);
                    fileStream.Close();

                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }

        }

        private void loadExcel(List<string> excelPaths)
        {
            try
            {
                if (workbooks.Count >= 0)
                {
                    foreach (IWorkbook book in workbooks)
                    {
                        book.Close();
                    }
                    workbooks.Clear();
                }

                foreach (string path in excelPaths)
                {
                    FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                    IWorkbook workbook = WorkbookFactory.Create(fileStream);
                    workbooks.Add(workbook);
                    fileStream.Close();

                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }

        }

        private void onOpenFile(string filePath)
        {
            if (filePath == null || filePath == "")
            {
                return;
            }
            tempExcelPath.Clear();
            if (Directory.Exists(filePath))
            {
                String[] filesName = Directory.GetFiles(filePath);
                List<string> excelPath = new List<string>();
                for (int i = 0; i < filesName.Length; i++)
                {
                    string extension = Path.GetExtension(filesName[i]);
                    if (extension.Equals(".xlsx") || extension.Equals(".xls"))
                    {
                        excelPath.Add(filesName[i]);
                        tempExcelPath.Add(filesName[i]);
                    }
                }

                this.loadExcel(excelPath);
            }
            else
            {
                tempExcelPath.Add(filePath);
                string extension = Path.GetExtension(filePath);
                if (extension.Equals(".xlsx") || extension.Equals(".xls"))
                {
                    this.loadExcel(filePath);

                }
            }
        }

        private void RecordFilePath_Click(object sender, EventArgs e)
        {
            string configDirPath = Path.Combine(PARENT_PATH, CONFIG_FILE_DIR);//配置文件的目录地址

            string fileConfigPath = Path.Combine(configDirPath, "FileConfig.json");//配置文件的绝对路径
            string content = File.ReadAllText(fileConfigPath);
            JObject jObject = JsonConvert.DeserializeObject<JObject>(content);
            jObject[ExcelPathKey] = this.DragFileTextBox.Text;
            jObject[ProjectPathKey] = this.OutFileTextBox.Text;

            string outString = JsonConvert.SerializeObject(jObject, Formatting.Indented);
            StreamWriter streamWriter = new StreamWriter(fileConfigPath, false, System.Text.Encoding.UTF8);
            streamWriter.AutoFlush = true;//每次调用write 方法则将数据写入基础流（文件）  如果为false，则每次调用完write()后，调用flush()或close()，才将数据写入基础流。
            streamWriter.WriteLine(outString);
            streamWriter.Close();

            MessageBox.Show("保存地址成功", this.DragFileTextBox.Text);
        }

        private void btn_SaveJsonAndTS_Click(object sender, EventArgs e)
        {
            this.SaveJsonBtn_Click(sender, e);
            this.SaveTsBtn_Click(sender, e);
        }

        private void AutoCreateDirectoryIfNotExist(string Path)
        {
            if (!Directory.Exists(Path))
            {
                try
                {
                    Directory.CreateDirectory(Path);
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }

            }
        }
    }
}
