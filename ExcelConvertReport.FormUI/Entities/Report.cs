using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelConvertReport.FormUI.Entities
{
    public class Report
    {
        public string CustomerCode { get; set; }//Müşteri kodu
        public string CustomerName { get; set; }//Müşteri adı
        public string OrderNumber { get; set; }//Sipariş numarası
        public DateTime? OrderDate { get; set; }//Sipariş tarihi
        public DateTime? DeadLine { get; set; }//Termin tarihi
        public int? DeadLineWeek { get; set; }//Termn haftası
        public int? ProjectCode { get; set; }//Proje kodu
        public int? OrderDesk { get; set; }//Sipariş sırası
        public string StockCode { get; set; }//Stok kodu
        public string StockName { get; set; }//Stok adı
        public string Unit { get; set; }//Birim
        public int? OrderQuantity { get; set; }//sipariş adeti
        public int? DeliveryAmount { get; set; }//Teslim miktarı
        public int? BalanceAmount { get; set; }//Bakiye tutarı
    }
}
