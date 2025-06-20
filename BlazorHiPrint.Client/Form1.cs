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
            string sDefault = print.PrinterSettings.PrinterName;//Ĭ�ϴ�ӡ����
            lblDefault.Text = sDefault;

            foreach (string sPrint in PrinterSettings.InstalledPrinters)//��ȡ���д�ӡ������
            {
                lstPrinter.Items.Add(sPrint);
                if (sPrint == sDefault)
                    lstPrinter.SelectedIndex = lstPrinter.Items.IndexOf(sPrint);
            }
        }
    }
}
