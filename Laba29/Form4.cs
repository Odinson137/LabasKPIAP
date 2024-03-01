using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Layout;
using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace Laba29
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            {
                var dataTable = GetDataFromDatabase();

                // Создание PDF-документа
                using (var stream = new MemoryStream())
                {
                    var pdfWriter = new PdfWriter(stream);
                    pdfWriter.SetSmartMode(true);

                    var pdfDocument = new PdfDocument(pdfWriter);
                    var document = new Document(pdfDocument);

                    // Заголовок
                    document.Add(new Paragraph("Отчет о заказах"));

                    // Таблица с данными
                    var table = new Table(dataTable.Columns.Count);
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        table.AddHeaderCell(new Cell().Add(new Paragraph(column.ColumnName))); // формируем заголовки столбцов таблицы на основании название столбцов для таблицы Orders
                    }

                    foreach (DataRow row in dataTable.Rows)
                    {
                        foreach (object item in row.ItemArray)
                        {
                            table.AddCell(new Cell().Add(new Paragraph(item.ToString()))); // заполняем строки таблицы в документе данными из таблицы Orders
                        }
                    }

                    document.Add(table); // добавляем получившуюся таблицу в pdf-документ

                    // Закрываем документ
                    document.Close();

                    // Сохраняем PDF-файл 
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllBytes(saveFileDialog.FileName, stream.ToArray());
                        MessageBox.Show("PDF-файл успешно создан!");
                    }
                }
                DataTable GetDataFromDatabase() // получаем данные из таблицы Orders для pdf-документа
                {

                    DataTable dataTable1 = new DataTable();

                    using (SqlConnection connection = new SqlConnection("Data Source=localhost;Persist Security Info=True;User ID=sa;Password=S3cur3P@ssW0rd!;Encrypt=True;TrustServerCertificate=True"))
                    {
                        connection.Open();

                        string sqlQuery = " SELECT id, id_drink, quantity, total FROM Orders";

                        using (SqlDataAdapter adapter = new SqlDataAdapter(sqlQuery, connection))
                        {
                            adapter.Fill(dataTable1);
                        }
                    }

                    return dataTable1;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PDF files (*.pdf)|*.pdf";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string pdfFilePath = openFileDialog.FileName;
                DataTable dataTable = ExtractPdfData(pdfFilePath);

                dataGridView1.DataSource = dataTable;
            }

        }

        private DataTable ExtractPdfData(string pdfFilePath)
        {
            DataTable dataTable = new DataTable();

            // Add columns based on the table headers in the **second** row of the PDF
            dataTable.Columns.Add("Номер");
            dataTable.Columns.Add("Номер напитка");
            dataTable.Columns.Add("Количество");
            dataTable.Columns.Add("Стоимость всего");

            using (PdfReader reader = new PdfReader(pdfFilePath))
            {
                PdfDocument pdfDoc = new PdfDocument(reader);

                for (int i = 1; i <= pdfDoc.GetNumberOfPages(); i++)
                {
                    var strategy = new SimpleTextExtractionStrategy();
                    string pageText = PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(i), strategy);

                    string[] lines = pageText.Split('\n');

                    // Skip the first line (header)
                    for (int j = 1; j < lines.Length; j++)
                    {
                        string[] cells = lines[j].Split(' ');

                        DataRow row = dataTable.NewRow();
                        for (int k = 0; k < cells.Length; k++)
                        {
                            if (k < dataTable.Columns.Count)
                            {
                                row[k] = cells[k].Trim();
                            }
                            else
                            {
                                break;
                            }
                        }

                        dataTable.Rows.Add(row);
                    }
                }
            }

            return dataTable;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Open PDF file
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string pdfFilePath = openFileDialog.FileName;

                // Show Print dialog
                PrintDialog printDialog = new PrintDialog();
                printDialog.Document = new PrintDocument();
                printDialog.UseEXDialog = true;

                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    // Print PDF file
                    PrintDocument printDocument = printDialog.Document;
                    printDocument.PrintController = new StandardPrintController();
                    printDocument.DefaultPageSettings.PaperSize = new PaperSize("A4", 826, 595);
                    printDocument.Print();
                }
            }

        }
    }
}
