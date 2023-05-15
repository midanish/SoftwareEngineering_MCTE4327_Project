using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginPage
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            var path = @"C:\Users\naufa\Documents\Final Year\List.txt";
            var lines = System.IO.File.ReadAllLines(path);

            foreach (var line in lines)
            {
                var data = line.Split(',');
                dataGridView1.Rows.Add(data);
            }
        }
    }
}
