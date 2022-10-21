using System;
using System.Drawing;
using System.Windows.Forms;

namespace Test_task
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }
        private void DrowPic()//Drows a shape
        {
            Pen myPen = new Pen(Color.Black, 2f);
            Graphics graphics = pictureBox1.CreateGraphics();
            graphics.Clear(Color.White);
            PointF[] points = new PointF[dataGridView1.RowCount+1];
            float[] picBoxCenter = new float[2] { pictureBox1.Width / 2, pictureBox1.Height / 2 };//find center picture box

            float scale = 10;

            for (int i = 0; i < dataGridView1.RowCount; i++)//forms points in array
            {
                float xPoint = picBoxCenter[0] + (float)dataGridView1[0, i].Value * scale;
                float yPoint = picBoxCenter[1] - (float)dataGridView1[1, i].Value * scale;
                points[i] = new PointF(xPoint, yPoint);
            }

            if (dataGridView1.RowCount > 1)//drows
            {
                points[points.Length-1] = points[0];
                graphics.DrawLines(myPen, points);
            }
        }
        private void addBtn_Click(object sender, EventArgs e)//add one point from text boxes
        {
            xTextBox.Text.Replace(".", ",");
            yTextBox.Text.Replace(".", ",");
            try
            {
                float x = float.Parse(xTextBox.Text);
                float y = float.Parse(yTextBox.Text);
                dataGridView1.Rows.Add();
                dataGridView1[0, dataGridView1.RowCount-1].Value = x;
                dataGridView1[1, dataGridView1.RowCount-1].Value = y;
                xTextBox.Text = "";
                yTextBox.Text = "";
                xTextBox.Focus();
                DrowPic();
            }
            catch(FormatException)
            {
                MessageBox.Show("Неправильный ввод x или y");
            }
        }

        private void deleteBtn_Click(object sender, EventArgs e)//delete selected row
        {
            dataGridView1.Rows.Remove(dataGridView1.Rows[dataGridView1.CurrentRow.Index]);
            if (dataGridView1.RowCount > 0)
            {
                DrowPic();
            }
        }

        private void calcBtn_Click(object sender, EventArgs e)//solve task
        {
            try
            {
                if (dataGridView1.RowCount == 0)
                {
                    throw new Exception("Кол-во точек не может быть равно 0");
                }
                float[][] myPoints = new float[dataGridView1.RowCount][];

                float summ1 = 0, summ2 = 0;
                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    summ1 += (float)dataGridView1[0, i].Value * (float)dataGridView1[1, i + 1].Value;
                    summ2 += (float)dataGridView1[1, i].Value * (float)dataGridView1[0, i + 1].Value;
                }
                summ1 += (float)dataGridView1[0, dataGridView1.RowCount - 1].Value * (float)dataGridView1[1, 0].Value;
                summ2 += (float)dataGridView1[1, dataGridView1.RowCount - 1].Value * (float)dataGridView1[0, 0].Value;
                float result = (summ1 - summ2) / 2;
                resultLabel.Text = Convert.ToString(result);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
