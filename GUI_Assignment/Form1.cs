using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUIMay
{       
    public partial class GUITest : Form
    {
        public GUITest()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void GUITest_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
       
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        public void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Calculate();

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Bitmap image = new Bitmap(@"C:\CatTessel.jpg");
            pictureBox1.Image = image;
            //pictureBox2.Image = Process(image);
        }

        private void label4_Click_1(object sender, EventArgs e)
        {
            //label4.Text = output.ToString();
            //Calculate();
            
        }

        private void Calculate()
        {
            double number1 = double.Parse(textBox4.Text);
            double number2 = double.Parse(textBox5.Text);
            double output = 0;
            try
            {
                switch (comboBox2.Text)
                {
                    case "Addition":
                        output = number1 + number2;
                        break;
                    case "Subtraction":
                        output = number1 - number2;
                        break;
                    case "Multiplication":
                        output = number1 * number2;
                        break;
                    case "Division":
                        output = number1 / number2;
                        break;
                }

                label4.Text = output.ToString();

            }

            catch
            {
                label4.Text = "Answer:N/A";

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Calculate();
        }
    }


}
