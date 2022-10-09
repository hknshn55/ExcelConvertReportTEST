using ExcelConvertReport.FormUI.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Microsoft.Office.Interop.Excel;
//using excel = Microsoft.Office.Interop.Excel;

namespace ExcelConvertReport.FormUI
{
    public partial class Form1 : Form
    {
        Report report = new Report();
        public Form1()
        {
            InitializeComponent();
            btnSearch.Enabled = false;
            dtpStart.Enabled = false;
            dtpFinish.Enabled = false;
            btnConvert.Enabled = false;
            btnTransfer.Enabled = false;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                ofd.ShowDialog();
                string path = ofd.FileName;

                OleDbConnection connect = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source = "
                    + path + "; Extended Properties = Excel 12.0");
                connect.Open();
                string query = "SELECT * FROM [Sayfa1$] ";
                OleDbDataAdapter data_adaptor = new OleDbDataAdapter(query, connect);
                connect.Close();

                DataTable dt = new DataTable();
                data_adaptor.Fill(dt);
                dgwScreen.DataSource = dt;
                btnConvert.Enabled = true;
            }
            catch (Exception)
            {
                MessageBox.Show($"Seçilen dosya uygun formatta değil!");
            }
        }
        public static List<Report> list = new List<Report>();
        int columCount = 0;

        private void btnConvert_Click(object sender, EventArgs e)
        {
            try
            {
                columCount = dgwScreen.Columns.Count;
            
                for (int j = 0; j < dgwScreen.Rows.Count; j++)
                {
                    for (int i = 0; i < columCount; i++)
                    {
                        if (dgwScreen.Rows[j].Cells[i + 1].Value != null)
                        {
                            report.CustomerCode = dgwScreen.Rows[j].Cells[i].Value.ToString();
                            report.CustomerName = dgwScreen.Rows[j].Cells[i + 1].Value.ToString();
                            report.OrderNumber = dgwScreen.Rows[j].Cells[i + 2].Value.ToString();
                            report.OrderDate = DateTime.ParseExact(dgwScreen.Rows[j].Cells[i + 3].Value.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            report.DeadLine = DateTime.ParseExact(dgwScreen.Rows[j].Cells[i + 4].Value.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            report.DeadLineWeek = Convert.ToInt32(dgwScreen.Rows[j].Cells[i + 5].Value);
                            report.ProjectCode = Convert.ToInt32(dgwScreen.Rows[j].Cells[i + 6].Value.ToString());
                            report.OrderDesk = Convert.ToInt32(dgwScreen.Rows[j + 7].Cells[i].Value);
                            report.StockCode = dgwScreen.Rows[j].Cells[i + 8].Value.ToString();
                            report.StockName = dgwScreen.Rows[j].Cells[i + 9].Value.ToString();
                            report.Unit = dgwScreen.Rows[j].Cells[i + 10].Value.ToString();
                            report.OrderQuantity = Convert.ToInt32(dgwScreen.Rows[j].Cells[i + 11].Value.ToString());
                            report.DeliveryAmount = Convert.ToInt32(dgwScreen.Rows[j].Cells[i + 12].Value.ToString());
                            report.BalanceAmount = Convert.ToInt32(dgwScreen.Rows[j].Cells[i + 13].Value.ToString());
                            if (report.OrderDate == list[j-1].OrderDate && report.CustomerName == list[j-1].CustomerName/* && report.StockName == list[j - 1].StockName*/)
                            {
                                list[j - 1].OrderQuantity += report.OrderQuantity.Value;
                            }
                            else
                            list.Add(report);
                        }
                    }
                }
                dgwScreen.DataSource = (list.Select(x => new
                {
                    x.CustomerCode,
                    x.CustomerName,
                    x.OrderNumber,
                    x.OrderDate,
                    x.DeadLine,
                    x.DeadLineWeek,
                    x.StockCode,
                    x.StockName,
                    x.Unit,
                    x.OrderQuantity,
                    x.DeliveryAmount,
                    x.BalanceAmount
                }))
                .OrderBy(x => x.CustomerName);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu! \n Hata : {ex}");
            }
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    excel.Application app = new excel.Application();
            //    app.Visible = true;
            //    Workbook book = app.Workbooks.Add(System.Reflection.Missing.Value);
            //    Worksheet sheet = (Worksheet)book.Sheets[1];
            //    for (int i = 0; i < dgwScreen.Columns.Count; i++)
            //    {
            //        Range range = (Range)sheet.Cells[1, 1];
            //        range.Cells[1, i + 1] = dgwScreen.Columns[i].HeaderText;
            //    }
            //    for (int i = 0; i < columCount; i++)
            //    {
            //        for (int j = 0; j < dgwScreen.Rows.Count; j++)
            //        {
            //            Range range = (Range)sheet.Cells[j + 1, i + 1];
            //            range.Cells[2, 1] = dgwScreen[i, j].Value;
            //        }
            //    }
            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("Lütfen seçili dosyanın doğru formatta olduğunu kontrol ediniz!");
            //}
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            DateTime start = dtpStart.Value;
            DateTime finish = dtpFinish.Value;

            var complexType = (list.Select(x => new
            {
                x.CustomerCode,
                x.CustomerName,
                x.OrderNumber,
                x.OrderDate,
                x.DeadLine,
                x.DeadLineWeek,
                x.StockCode,
                x.StockName,
                x.Unit,
                x.OrderQuantity,
                x.DeliveryAmount,
                x.BalanceAmount
            }))
            .Where(x => x.OrderDate >= start && x.OrderDate <= finish)
            .OrderBy(x => x.CustomerName);
            dgwScreen.DataSource = complexType;
        }
     
    }
}
