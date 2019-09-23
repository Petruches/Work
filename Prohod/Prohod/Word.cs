using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using WorD = Microsoft.Office.Interop.Word;
using Microsoft.Office;
using Microsoft.Office.Interop.Word;
using System.Data.SqlClient;


namespace Prohod
{
    class Word
    {
        

        public void MS_Export_Table(DataGridView DGV, string filename)
        {// код указанный ниже в комментариях даёт возможность распечать всю таблицу
            /*if (DGV.Rows.Count != 0)*/
            if (DGV.SelectedRows.Count != 0)
            {
                /*int RowCount = DGV.Rows.Count;*/
                int RowCount = DGV.SelectedRows.Count;
                int ColumnCount = DGV.Columns.Count;
                Object[,] DataArray = new object[RowCount + 1, ColumnCount + 1];

                //Добавляем строчки
                int r = 0;
                for (int c = 0; c <= ColumnCount - 1; c++)
                {
                    for (r = 0; r <= RowCount - 1; r++)
                    {
                        DataArray[r, c] = DGV.Rows[r].Cells[c].Value;
                    } //конец строки
                } //конец столбца

                WorD.Document oDoc = new WorD.Document();
                oDoc.Application.Visible = true;

                //Ориентация страницы
                oDoc.PageSetup.Orientation = WorD.WdOrientation.wdOrientLandscape;


                dynamic oRange = oDoc.Content.Application.Selection.Range;
                string oTemp = "";
                for (r = 0; r <= RowCount - 1; r++)
                {
                    for (int c = 0; c <= ColumnCount - 1; c++)
                    {
                        oTemp = oTemp + DataArray[r, c] + "\t";

                    }
                }

                //Формат таблицы
                oRange.Text = oTemp;
                object oMissing = Missing.Value;
                object Separator = WorD.WdTableFieldSeparator.wdSeparateByTabs;
                object ApplyBorders = true;
                object AutoFit = true;
                object AutoFitBehavior = WorD.WdAutoFitBehavior.wdAutoFitContent;

                oRange.ConvertToTable(ref Separator, ref RowCount, ref ColumnCount,
                                      Type.Missing, Type.Missing, ref ApplyBorders,
                                      Type.Missing, Type.Missing, Type.Missing,
                                      Type.Missing, Type.Missing, Type.Missing,
                                      Type.Missing, ref AutoFit, ref AutoFitBehavior, Type.Missing);

                oRange.Select();

                oDoc.Application.Selection.Tables[1].Select();
                oDoc.Application.Selection.Tables[1].Rows.AllowBreakAcrossPages = 0;
                oDoc.Application.Selection.Tables[1].Rows.Alignment = 0;
                oDoc.Application.Selection.Tables[1].Rows[1].Select();
                oDoc.Application.Selection.InsertRowsAbove(1);
                oDoc.Application.Selection.Tables[1].Rows[1].Select();

                //стиль текста в строчке
                oDoc.Application.Selection.Tables[1].Rows[1].Range.Bold = 1;
                oDoc.Application.Selection.Tables[1].Rows[1].Range.Font.Name = "Times New Roman";
                oDoc.Application.Selection.Tables[1].Rows[1].Range.Font.Size = 13;

                //Добавление загаловка
                for (int c = 0; c <= ColumnCount - 1; c++)
                {
                    oDoc.Application.Selection.Tables[1].Cell(1, c + 1).Range.Text = DGV.Columns[c].HeaderText;
                }

                //стиль таблиц
                oDoc.Application.Selection.Tables[1].Rows[1].Select();
                oDoc.Application.Selection.Cells.VerticalAlignment = WorD.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                //заголовок
                foreach (WorD.Section section in oDoc.Application.ActiveDocument.Sections)
                {
                    WorD.Range headerRange = section.Headers[WorD.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                    headerRange.Fields.Add(headerRange, WorD.WdFieldType.wdFieldPage);
                    headerRange.Text = "Введите тект";
                    headerRange.Font.Size = 16;
                    headerRange.ParagraphFormat.Alignment = WorD.WdParagraphAlignment.wdAlignParagraphCenter;
                }

                //Сохранение файла
                oDoc.SaveAs(filename, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing);
            }
        }
    }
}