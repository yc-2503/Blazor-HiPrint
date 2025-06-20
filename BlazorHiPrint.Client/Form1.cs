using System.Drawing.Printing;

namespace BlazorHiPrint.Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PrintDocument print = new PrintDocument();
            string sDefault = print.PrinterSettings.PrinterName;//默认打印机名
            lblDefault.Text = sDefault;

            foreach (string sPrint in PrinterSettings.InstalledPrinters)//获取所有打印机名称
            {
                lstPrinter.Items.Add(sPrint);
                if (sPrint == sDefault)
                    lstPrinter.SelectedIndex = lstPrinter.Items.IndexOf(sPrint);
            }
        }
    }
}
