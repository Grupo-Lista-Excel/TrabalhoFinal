using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;

namespace Funcionarios_Farmacia.Telas
{
    public partial class TelaLista : Form
    {
        public TelaLista()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void CarregarDadosDoExcel(string caminhoArquivo)
        {
            try
            {
                Excel.Application app = new Excel.Application();
                Excel.Workbook pasta = app.Workbooks.Open(caminhoArquivo);
                Excel.Worksheet plan = pasta.Worksheets["Funcionários"];

                dataGridView1.Rows.Clear();//limpa linhas
                dataGridView1.Columns.Clear();//limpa colunas

                //percorre linhas da planilha
                for (int row = 1; row <= plan.UsedRange.Rows.Count; row++)
                {
                    if (row == 1)
                    {
                        //se for a 1°linha adiciona as colunas na lista
                        for (int col = 1; col <= plan.UsedRange.Columns.Count; col++)
                        {
                            //adiciona coluna a lista com nome da 1° linha
                            dataGridView1.Columns.Add(plan.Cells[row, col].Value.ToString(), plan.Cells[row, col].Value.ToString());
                        }
                    }
                    else
                    {
                        //adiciona uma nova linha a lista
                        int rowIndex = dataGridView1.Rows.Add();
                        for (int col = 1; col <= plan.UsedRange.Columns.Count; col++)
                        {
                            //preenche as células da linha com os dados da planilha
                            dataGridView1.Rows[rowIndex].Cells[col - 1].Value = plan.Cells[row, col].Value;
                        }
                    }
                }

                pasta.Close();
                app.Quit();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro ao carregar dados do Excel: {ex.Message}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string caminhoArquivo = @"C:\Users\2022102020014\Desktop\Lista Funcionários\Lista.xlsx"; 
            CarregarDadosDoExcel(caminhoArquivo);
        }
    }
}
