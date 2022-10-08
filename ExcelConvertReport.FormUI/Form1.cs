using ExcelConvertReport.FormUI.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExcelConvertReport.FormUI
{
    public partial class Form1 : Form
    {
        Report report = new Report();
        public Form1()
        {
            InitializeComponent();
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
                string query = "SELECT * FROM [Sayfa1$A:B] ";
                OleDbDataAdapter data_adaptor = new OleDbDataAdapter(query, connect);
                connect.Close();

                DataTable dt = new DataTable();
                data_adaptor.Fill(dt);
                dgwScreen.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu! \n Hata: {ex}");
            }

        }

        public static List<Report> list = new List<Report>();
        private void btnConvert_Click(object sender, EventArgs e)
        {
            int columCount = dgwScreen.Columns.Count;
            try
            {
                for (int j = 0; j < dgwScreen.Rows.Count; j++)
                {
                    for (int i = 0; i < columCount; i++)
                    {
                        report.CustomerCode = dgwScreen.Rows[j].Cells[i].Value.ToString();
                        report.CustomerName = dgwScreen.Rows[j].Cells[i + 1].Value.ToString();
                        report.OrderNumber = dgwScreen.Rows[j].Cells[i + 2].Value.ToString();
                        report.OrderDate = DateTime.Parse(dgwScreen.Rows[j].Cells[i + 3].Value.ToString());
                        report.DeadLine = DateTime.Parse(dgwScreen.Rows[j].Cells[i + 4].Value.ToString());
                        report.DeadLineWeek = Convert.ToInt32(dgwScreen.Rows[j].Cells[i + 5].Value);
                        report.ProjectCode = Convert.ToInt32(dgwScreen.Rows[j].Cells[i + 6].Value.ToString());
                        report.OrderDesk = Convert.ToInt32(dgwScreen.Rows[j + 7].Cells[i].Value);
                        report.StockCode = dgwScreen.Rows[j].Cells[i + 8].Value.ToString();
                        report.StockName = dgwScreen.Rows[j].Cells[i + 9].Value.ToString();
                        report.Unit = dgwScreen.Rows[j].Cells[i + 10].Value.ToString();
                        report.OrderQuantity = Convert.ToInt32(dgwScreen.Rows[j].Cells[i + 11].Value.ToString());
                        report.DeliveryAmount = Convert.ToInt32(dgwScreen.Rows[j].Cells[i + 12].Value.ToString());
                        report.BalanceAmount = Convert.ToInt32(dgwScreen.Rows[j].Cells[i + 13].Value.ToString());
                    }
                    if (report.CustomerName != null)
                    {
                        list.Add(report);
                    }
                }
                dgwScreen.DataSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu! \n Hata : {ex}");
            }
        }
    }
}
