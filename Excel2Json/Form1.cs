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
        /// 输出文件的目录名(默认
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
        private static readonly string ExcelPathKey = "ExcelPath";
        private static readonly string ProjectPathKey = "OutFilePath";

        private static readonly string ExportConfigFilesName = "FileConfig.json";

        private List<IWorkbook> workbooks;


        //private string OutJsonPath = "";
        //private string OutTSPath = "";
        private string DefaultOutFilePath = "";

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

                string fileConfigPath = Path.Combine(configDirPath, ExportConfigFilesName);//配置文件的绝对路径
                string content = File.ReadAllText(fileConfigPath);

                string defaultOutFilePath = Path.Combine(PARENT_PATH, OUT_FILE_DIR);//默认输出文件的目录地址;
                this.DefaultOutFilePath = defaultOutFilePath;

                FileConfigHistory historyData = JsonConvert.DeserializeObject<FileConfigHistory>(content);

                ConfigFileData configFileData = historyData.History[historyData.CurIndex];

                string filePath = configFileData.ExcelPath;
                this.onOpenFile(filePath);
                this.DragFileTextBox.Text = filePath;

                filePath = configFileData.OutFilePath;
                this.OutFileTextBox.Text = filePath;

                filePath = configFileData.OutJsonPath;
                this.OutJsonTextBox.Text = filePath;

                filePath = configFileData.OutTSPath;
                this.OutTsTextBox.Text = filePath;

                InitConfigHistoryListComboBox(historyData.History);
                //JObject jObject = JsonConvert.DeserializeObject<JObject>(content);
                //string filePath = jObject[ExcelPathKey].ToString();
                //this.onOpenFile(filePath);
                //this.DragFileTextBox.Text = filePath;

                //string defaultOutFilePath = Path.Combine(PARENT_PATH, OUT_FILE_DIR);//默认输出文件的目录地址;
                //this.DefaultOutFilePath = defaultOutFilePath;

                //filePath = jObject[ProjectPathKey] == null ? "" : jObject[ProjectPathKey].ToString();
                //this.OutFileTextBox.Text = filePath;

                //if (jObject[OutJsonPathKey] != null)
                //{
                //    this.OutJsonTextBox.Text = jObject[OutJsonPathKey].ToString();
                //}

                //if (jObject[OutTSPathKey] != null)
                //{
                //    this.OutTsTextBox.Text = jObject[OutTSPathKey].ToString();
                //}

                this.AllowDrop = true;
                this.CheckFilePanel.AllowDrop = true;
                this.CheckFilePanel.DragEnter += new System.Windows.Forms.DragEventHandler(this.onFileDragEnter);
                this.CheckFilePanel.DragDrop += new System.Windows.Forms.DragEventHandler(this.onFileDragDrop);

                this.OutFilePanel.AllowDrop = true;
                this.OutFilePanel.DragEnter += new System.Windows.Forms.DragEventHandler(this.onFileDragEnter);
                this.OutFilePanel.DragDrop += new System.Windows.Forms.DragEventHandler(this.onFileDragDrop);

                this.OutJsonFilePanel.AllowDrop = true;
                this.OutJsonFilePanel.DragEnter += new System.Windows.Forms.DragEventHandler(this.onFileDragEnter);
                this.OutJsonFilePanel.DragDrop += new System.Windows.Forms.DragEventHandler(this.onFileDragDrop);

                this.OutStructFilePanel.AllowDrop = true;
                this.OutStructFilePanel.DragEnter += new System.Windows.Forms.DragEventHandler(this.onFileDragEnter);
                this.OutStructFilePanel.DragDrop += new System.Windows.Forms.DragEventHandler(this.onFileDragDrop);

                //if (jObject[ProjectPathKey] == null || jObject[ProjectPathKey].ToString() == "")
                //{
                //    OutTSPath = OutJsonPath = Path.Combine(PARENT_PATH, OUT_FILE_DIR);//配置文件的目录地址;
                //}
                //else
                //{
                //    string projectPath = jObject[ProjectPathKey].ToString();
                //    OutJsonPath = Path.Combine(projectPath, @"assets\config");
                //    OutTSPath = Path.Combine(projectPath, @"scripts\tableData");
                //}



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
            string filePath = DragFileTextBox.Text;
            onOpenFile(filePath, curOutJsonComboBoxIndex);
            buildJsonFile(workbooks);
        }

        /// <summary>
        /// 保存数据类型按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveTsBtn_Click(object sender, EventArgs e)
        {
            string filePath = DragFileTextBox.Text;
            onOpenFile(filePath, curOutJsonComboBoxIndex);

            buildTSFile(workbooks);
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
                        ICell cell = currentRow.GetCell(j, MissingCellPolicy.RETURN_BLANK_AS_NULL);

                        if (j == Form1.dictJsonKeyIndex)//取第一列为key
                        {
                            if (cell == null || cell.CellType == CellType.Blank)//如果第一列是空的格子，则没必要继续初始化下去了
                            {
                                isEmptyRow = true;
                                break;
                            }
                            continue;
                        }

                        if (cell == null || cell.CellType == CellType.Blank)
                        {
                            //rowObj[allNameCells[j].StringCellValue] = null;
                            continue;
                        }
                        else
                        {
                            try
                            {
                                switch (cell.CellType)
                                {
                                    case CellType.Unknown:
                                        break;
                                    case CellType.Numeric:
                                        rowObj[allNameCells[j].StringCellValue] = cell.NumericCellValue;
                                        break;
                                    case CellType.String:
                                        string str = cell.StringCellValue;

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
                                        rowObj[allNameCells[j].StringCellValue] = str;
                                        break;
                                    case CellType.Formula:
                                        break;
                                    case CellType.Blank:
                                        break;
                                    case CellType.Boolean:
                                        rowObj[allNameCells[j].StringCellValue] = cell.BooleanCellValue;
                                        break;
                                    case CellType.Error:
                                        break;
                                    default:
                                        break;
                                }
                            }
                            catch (Exception e)
                            {
                                ShowMsgInBox("=======error=======");
                                ShowMsgInBox(string.Format("表单名{0} 行{1}:列{2} 数据出错", sheet.SheetName, currentRow.RowNum, j));
                                MessageBox.Show(e.ToString());
                            }

                        }

                    }

                    if (!isEmptyRow)
                    {
                        JToken jToken = JToken.FromObject(rowObj);
                        ICell headCell = currentRow.GetCell(Form1.dictJsonKeyIndex);
                        if (headCell.CellType == CellType.Numeric)
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


                //字段行
                List<ICell> allNameCells = nameRow.Cells;
                //类型行
                List<ICell> allTypeCells = typeRow.Cells;

                int cols = allNameCells.Count;

                int startReadRowIndex = Form1.tableHeadIndex;
                System.Collections.IEnumerator enumerator = sheet.GetRowEnumerator();

                JArray jArray = new JArray();
                //string[] outObjArr =new string [cols];
                List<string> outObjArr = new List<string>();

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
                        ICell cell = currentRow.GetCell(j, MissingCellPolicy.RETURN_BLANK_AS_NULL);

                        if (j == Form1.dictJsonKeyIndex)//取第一列为key
                        {
                            if (cell == null || cell.CellType == CellType.Blank)//如果第一列是空的格子，则没必要继续初始化下去了
                            {
                                isEmptyRow = true;
                                break;
                            }
                        }

                        if (cell == null || cell.CellType == CellType.Blank)
                        {
                            //rowObj[allNameCells[j].StringCellValue] = null;
                            continue;
                        }
                        else
                        {
                            try
                            {
                                switch (cell.CellType)
                                {
                                    case CellType.Unknown:
                                        break;
                                    case CellType.Numeric:
                                        rowObj[allNameCells[j].StringCellValue] = cell.NumericCellValue;
                                        break;
                                    case CellType.String:
                                        string str = cell.StringCellValue;

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
                                        rowObj[allNameCells[j].StringCellValue] = str;
                                        break;
                                    case CellType.Formula:
                                        break;
                                    case CellType.Blank:
                                        break;
                                    case CellType.Boolean:
                                        rowObj[allNameCells[j].StringCellValue] = cell.BooleanCellValue;
                                        break;
                                    case CellType.Error:
                                        break;
                                    default:
                                        break;
                                }
                            }
                            catch (Exception e)
                            {
                                ShowMsgInBox("=======error=======");
                                ShowMsgInBox(string.Format("表单名{0} 行{1}:列{2} 数据出错", sheet.SheetName, currentRow.RowNum, j));
                                MessageBox.Show(e.ToString());
                            }

                        }

                    }

                    if (!isEmptyRow)
                    {
                        JToken jToken = JToken.FromObject(rowObj);
                        jArray.Add(rowObj);
                    }

                }

                string json = JsonConvert.SerializeObject(jArray, Formatting.Indented);
                Console.WriteLine("输出的json:" + json);
                strArr[i] = json;
            }

            return strArr;
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
                        if (allNameCells[j] == null || allNameCells[j].CellType == CellType.Blank)
                        {
                            continue;
                        }

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
            string filePath = this.DragFileTextBox.Text;
            onOpenFile(filePath);

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
                OutJsonTextBox.Text = Path.Combine(filePath, OutJsonPrefixTextBox.Text);
                OutTsTextBox.Text = Path.Combine(filePath, OutTsPrefixTextBox.Text);
            }
            else if (this.OutJsonFilePanel == sender)
            {
                OutJsonTextBox.Text = filePath;
            }
            else if (this.OutStructFilePanel == sender)
            {
                OutTsTextBox.Text = filePath;
            }

        }

        private List<IWorkbook> loadExcel(string excelPaths)
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
            return workbooks;
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

        private void initOutFileCombobox(List<IWorkbook> workbooks, int outJsonComboBoxIndex = 0)
        {
            curOutJsonComboBoxIndex = outJsonComboBoxIndex;

            Dictionary<int, string> kvDictonary = new Dictionary<int, string>();
            int sheetIdx = 0;
            kvDictonary.Add(sheetIdx, "全选");
            sheetIdx++;

            List<string> allSheetNameList = new List<string>();

            foreach (var book in workbooks)
            {
                int sheetNum = book.NumberOfSheets;
                for (int i = 0; i < sheetNum; i++)//遍历每个表单
                {
                    allSheetNameList.Add(book.GetSheetAt(i).SheetName);
                }
            }
            allSheetNameList.Sort();

            foreach (var name in allSheetNameList)
            {
                kvDictonary.Add(sheetIdx, name);
                sheetIdx++;
            }

            BindingSource bs = new BindingSource();
            bs.DataSource = kvDictonary;

            outJsonComboBox.DataSource = bs;
            outJsonComboBox.ValueMember = "Key";
            outJsonComboBox.DisplayMember = "Value";

            outJsonComboBox.SelectedIndex = outJsonComboBoxIndex;
        }

        private void InitConfigHistoryListComboBox(List<ConfigFileData> configFileDatas, int selectedIndex = 0)
        {
            string tag = configFileDatas[selectedIndex].Tag;
            Dictionary<int, string> kvDictonary = new Dictionary<int, string>();
            for (int i = 0; i < configFileDatas.Count; i++)
            {
                kvDictonary.Add(i, configFileDatas[i].Tag);
            }

            BindingSource bs = new BindingSource();
            bs.DataSource = kvDictonary;

            ConfigHistoryListComboBox.DataSource = bs;
            ConfigHistoryListComboBox.ValueMember = "Key";
            ConfigHistoryListComboBox.DisplayMember = "Value";

            ConfigHistoryListComboBox.SelectedIndex = selectedIndex;

            ConfigTagTextBox.Text = tag;
        }

        private void RefreshAllOutTextBox()
        {
            string outFilePath = this.OutFileTextBox.Text;
            string outJsonPath = Path.Combine(outFilePath, this.OutJsonPrefixTextBox.Text);
            this.OutJsonTextBox.Text = outJsonPath;

            string outTSPath = Path.Combine(outFilePath, this.OutTsPrefixTextBox.Text);
            this.OutTsTextBox.Text = outTSPath;
        }

        private void RefreshAllOutTextBox(ConfigFileData data)
        {
            this.OutFileTextBox.Text = data.OutFilePath;

            this.OutJsonTextBox.Text = data.OutJsonPath;

            this.OutTsTextBox.Text = data.OutTSPath;

            this.DragFileTextBox.Text = data.ExcelPath;

            ConfigTagTextBox.Text = data.Tag;

        }

        /// <summary>
        /// 打开指定路径的文件
        /// </summary>
        /// <param name="filePath"></param>
        private void onOpenFile(string filePath, int outSheetIdx = 0)
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

            initOutFileCombobox(workbooks, outSheetIdx);
        }

        private void RecordFilePath_Click(object sender, EventArgs e)
        {
            string configDirPath = Path.Combine(PARENT_PATH, CONFIG_FILE_DIR);//配置文件的目录地址

            string fileConfigPath = Path.Combine(configDirPath, ExportConfigFilesName);//配置文件的绝对路径
            string content = File.ReadAllText(fileConfigPath);

            FileConfigHistory historyData = JsonConvert.DeserializeObject<FileConfigHistory>(content);
            List<ConfigFileData> configFileDatas = historyData.History;

            int curIndex = 0;

            curIndex = configFileDatas.FindIndex((d) =>
          {
              return d.Tag == this.ConfigTagTextBox.Text;
          });

            ConfigFileData nowData = new ConfigFileData(this.ConfigTagTextBox.Text, this.DragFileTextBox.Text,
        this.OutFileTextBox.Text, this.OutJsonTextBox.Text, this.OutTsTextBox.Text);

            if (curIndex == -1)
            {
                configFileDatas.Add(nowData);
                curIndex = configFileDatas.Count - 1;
            }
            else
            {
                configFileDatas[curIndex] = nowData;
            }

            string outString = JsonConvert.SerializeObject(historyData, Formatting.Indented);
            StreamWriter streamWriter = new StreamWriter(fileConfigPath, false, System.Text.Encoding.UTF8);
            streamWriter.AutoFlush = true;//每次调用write 方法则将数据写入基础流（文件）  如果为false，则每次调用完write()后，调用flush()或close()，才将数据写入基础流。
            streamWriter.WriteLine(outString);
            streamWriter.Close();

            MessageBox.Show("保存地址成功", this.DragFileTextBox.Text);

            this.InitConfigHistoryListComboBox(configFileDatas, curIndex);
        }

        private void btn_SaveJsonAndTS_Click(object sender, EventArgs e)
        {
            string filePath = DragFileTextBox.Text;
            onOpenFile(filePath, curOutJsonComboBoxIndex);
            buildJsonFile(workbooks);
            buildTSFile(workbooks);
        }

        /// <summary>
        /// 构建json文件
        /// </summary>
        /// <param name="workbooks">目标文件</param>
        private void buildJsonFile(List<IWorkbook> workbooks)
        {
            if (workbooks.Count == 0)
            {
                MessageBox.Show("excel地址为空");
                return;
            }

            string targetSheetName = null;
            if (outJsonComboBox.SelectedIndex > 0)//导出特定表单
            {
                targetSheetName = outJsonComboBox.Text;
            }

            Boolean outDictJson = !checkOutArrJsonBox.Checked;//导出的json 格式是字典型还是数组型

            ShowMsgInBox("=======开始导出配置=======");
            string outDirPath = OutJsonTextBox.Text;
            AutoCreateDirectoryIfNotExist(outDirPath);
            if (clearDirectoryCheckBox.Checked)
            {
                ClearDirectory(outDirPath);
            }

            float startMillionSeconds = DateTime.Now.Millisecond;
            foreach (IWorkbook currentWorkBook in workbooks)
            {

                int sheetNum = currentWorkBook.NumberOfSheets;
                for (int i = 0; i < sheetNum; i++)//遍历每个表单
                {
                    ISheet sheet = currentWorkBook.GetSheetAt(i);
                    if (targetSheetName != null && sheet.SheetName != targetSheetName)
                    {
                        continue;
                    }

                    string[] outString = outDictJson ? this.createDictObjJsonBySheet(currentWorkBook) : this.createArrayJsonBySheet(currentWorkBook);

                    string outPath = Path.Combine(outDirPath, "Table_" + sheet.SheetName + ".json");//配置文件的绝对路径

                    StreamWriter streamWriter = new StreamWriter(outPath, false, System.Text.Encoding.UTF8);
                    streamWriter.AutoFlush = true;//每次调用write 方法则将数据写入基础流（文件）  如果为false，则每次调用完write()后，调用flush()或close()，才将数据写入基础流。
                    streamWriter.WriteLine(outString[i]);
                    streamWriter.Close();

                    ShowMsgInBox(outString[i]);
                }

            }
            float endMillionSeconds = DateTime.Now.Millisecond;
            ShowMsgInBox("=======导出配置完毕=======");
            ShowMsgInBox(string.Format("用时：{0}毫秒", endMillionSeconds - startMillionSeconds));
            MessageBox.Show("配置导出成功");
        }

        /// <summary>
        /// 构建ts文件
        /// </summary>
        /// <param name="workbooks">目标文件</param>
        private void buildTSFile(List<IWorkbook> workbooks)
        {
            if (workbooks.Count == 0)
            {
                MessageBox.Show("excel地址为空");
                return;
            }

            string targetSheetName = null;
            if (outJsonComboBox.SelectedIndex > 0)//导出特定表单
            {
                targetSheetName = outJsonComboBox.Text;
            }

            ShowMsgInBox("=======开始导出数据类型文件=======");
            string outDirPath = OutTsTextBox.Text;//配置文件的目录地址
            AutoCreateDirectoryIfNotExist(outDirPath);
            if (clearDirectoryCheckBox.Checked)
            {
                ClearDirectory(outDirPath);
            }

            float startMillionSeconds = DateTime.Now.Millisecond;
            foreach (IWorkbook currentWorkBook in workbooks)
            {
                string[] outString = this.createTsClass(templateFile, currentWorkBook);

                int sheetNum = currentWorkBook.NumberOfSheets;
                for (int i = 0; i < sheetNum; i++)//遍历每个表单
                {
                    ISheet sheet = currentWorkBook.GetSheetAt(i);
                    if (targetSheetName != null && sheet.SheetName != targetSheetName)
                    {
                        continue;
                    }

                    string outPath = Path.Combine(outDirPath, "TBDATA_" + sheet.SheetName + ".ts");//配置文件的绝对路径
                    StreamWriter streamWriter = new StreamWriter(outPath, false, System.Text.Encoding.UTF8);
                    streamWriter.AutoFlush = true;//每次调用write 方法则将数据写入基础流（文件）  如果为false，则每次调用完write()后，调用flush()或close()，才将数据写入基础流。
                    streamWriter.WriteLine(outString[i]);
                    streamWriter.Close();
                    ShowMsgInBox(outString[i]);
                }
            }
            float endMillionSeconds = DateTime.Now.Millisecond;
            ShowMsgInBox("=======导出配置完毕=======");
            ShowMsgInBox(string.Format("用时：{0}毫秒", endMillionSeconds - startMillionSeconds));
            MessageBox.Show("配置导出成功");
            MessageBox.Show("类型定义脚本导出成功");
        }

        /// <summary>
        /// 自动生成对应文件夹（如果改文件夹不存在）
        /// </summary>
        /// <param name="Path"></param>
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

        // 清空指定目录下的所有文件和文件夹
        private void ClearDirectory(string directoryPath)
        {
            DirectoryInfo directory = new DirectoryInfo(directoryPath);
            foreach (FileInfo file in directory.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo subDirectory in directory.GetDirectories())
            {
                subDirectory.Delete(true);
            }
        }

        private void ShowMsgInBox(string content)
        {
            showResultBox.AppendText(content + "\r\n");
        }

        /// <summary>
        /// 打开当前配置文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_goExelPath_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Explorer.exe", DragFileTextBox.Text);
        }

        /// <summary>
        /// 打开当前构建目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_goBuildPath_Click(object sender, EventArgs e)
        {
            if (OutFileTextBox.Text != null && !OutFileTextBox.Text.Equals(""))
            {
                System.Diagnostics.Process.Start("Explorer.exe", OutFileTextBox.Text);
            }
        }

        private void btn_goBuildJsonPath_Click(object sender, EventArgs e)
        {
            if (OutJsonTextBox.Text != null && !OutJsonTextBox.Text.Equals(""))
            {
                System.Diagnostics.Process.Start("Explorer.exe", OutJsonTextBox.Text);
            }
        }

        private void btn_goBuildTsPath_Click(object sender, EventArgs e)
        {
            if (OutTsTextBox.Text != null && !OutTsTextBox.Text.Equals(""))
            {
                System.Diagnostics.Process.Start("Explorer.exe", OutTsTextBox.Text);
            }
        }

        private void OutFileTextBox_TextChanged(object sender, EventArgs e)
        {
            this.RefreshAllOutTextBox();
        }

        private void ConfigHistoryListComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int curIndex = this.ConfigHistoryListComboBox.SelectedIndex;

            string configDirPath = Path.Combine(PARENT_PATH, CONFIG_FILE_DIR);//配置文件的目录地址
            AutoCreateDirectoryIfNotExist(configDirPath);

            string fileConfigPath = Path.Combine(configDirPath, ExportConfigFilesName);//配置文件的绝对路径
            string content = File.ReadAllText(fileConfigPath);

            FileConfigHistory historyData = JsonConvert.DeserializeObject<FileConfigHistory>(content);
            ConfigFileData configFileData = historyData.History[curIndex];
            string tag = configFileData.Tag;
            this.ConfigTagTextBox.Text = tag;

            this.RefreshAllOutTextBox(configFileData);

            onOpenFile(configFileData.ExcelPath);
        }

        private void CheckFilePanel_Paint(object sender, PaintEventArgs e)
        {

        }

        /// <summary>
        /// 当前输出表单索引 默认为0 ，则视为全部表单都导出
        /// </summary>
        protected int curOutJsonComboBoxIndex = 0;
        /// <summary>
        /// 输出表单改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void outJsonComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            curOutJsonComboBoxIndex = outJsonComboBox.SelectedIndex;
        }

        private void DeleteFilePath_Click(object sender, EventArgs e)
        {
            string curFileTag = ConfigTagTextBox.Text;

            string configDirPath = Path.Combine(PARENT_PATH, CONFIG_FILE_DIR);//配置文件的目录地址

            string fileConfigPath = Path.Combine(configDirPath, ExportConfigFilesName);//配置文件的绝对路径
            string content = File.ReadAllText(fileConfigPath);

            FileConfigHistory historyData = JsonConvert.DeserializeObject<FileConfigHistory>(content);
            List<ConfigFileData> configFileDatas = historyData.History;

            int curIndex = configFileDatas.FindIndex((d) =>
             {
                 return d.Tag == ConfigTagTextBox.Text;
             });

            if (curIndex >= 0)
            {
                configFileDatas.RemoveAt(curIndex);
            }

            string outString = JsonConvert.SerializeObject(historyData, Formatting.Indented);

            Console.Write(outString);
            StreamWriter streamWriter = new StreamWriter(fileConfigPath, false, System.Text.Encoding.UTF8);
            streamWriter.AutoFlush = true;//每次调用write 方法则将数据写入基础流（文件）  如果为false，则每次调用完write()后，调用flush()或close()，才将数据写入基础流。
            streamWriter.WriteLine(outString);
            streamWriter.Close();

            MessageBox.Show("删除地址成功", this.DragFileTextBox.Text);

            if (configFileDatas.Count > 0)
            {
                //删除成功后默认读第一个
                curIndex = 0;
                InitConfigHistoryListComboBox(configFileDatas, curIndex);

                ConfigFileData configFileData = historyData.History[curIndex];

                RefreshAllOutTextBox(configFileData);

                onOpenFile(configFileData.ExcelPath);

            }
        }
    }

    struct ConfigFileData
    {
        public string Tag;
        public string ExcelPath;
        public string OutFilePath;
        public string OutJsonPath;
        public string OutTSPath;

        public ConfigFileData(string tag, string excelPath, string outFilePath, string outJsonPath, string outTSPath)
        {
            this.Tag = tag;
            this.ExcelPath = excelPath;
            this.OutFilePath = outFilePath;
            this.OutJsonPath = outJsonPath;
            this.OutTSPath = outTSPath;
        }
    }

    struct FileConfigHistory
    {
        public int CurIndex;
        public List<ConfigFileData> History;
    }

}
