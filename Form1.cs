using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HW_Notes
{
    public partial class Form1 : Form
    {
        SQLITE qLITE = new SQLITE();
        public Form1()
        {
            InitializeComponent();
        }

        

        private void b_save_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string filename = saveFileDialog1.FileName;
            // сохраняем текст в файл
            System.IO.File.WriteAllText(filename, tb_note.Text);
            MessageBox.Show("Файл сохранен");
        }

        private void b_load_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string filename = openFileDialog1.FileName;
            // читаем файл в строку
            string fileText = System.IO.File.ReadAllText(filename);
            tb_note.Text = fileText;
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {            
            var dialog = MessageBox.Show("ок-записать данные, cancel-вернуть", "DATABASE", MessageBoxButtons.OKCancel);
            if (dialog == DialogResult.OK)
            {
                monthCalendar1.AddAnnuallyBoldedDate(monthCalendar1.SelectionStart);
                qLITE.AddNote(tb_note.Text, monthCalendar1.SelectionStart.ToString("yyyy.MM.dd"));
                monthCalendar1.UpdateBoldedDates();
            }
            else
            {
                tb_note.Text = qLITE.SelectNote(monthCalendar1.SelectionStart.ToString("yyyy.MM.dd"));
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            tb_note.Text += dateTimePicker1.Value.ToString("yyyy.MM.dd");
        }
    }
}
