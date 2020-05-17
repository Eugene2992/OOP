using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Laba_1_Form
{
    public partial class Form1 : Form
    {
        struct constr
        {
            public string Name;
            public ConstructorInfo cinf;
        };
        List<constr> coll = new List<constr>();
        List<Figure> MyList = new List<Figure>();
        float LineWidth;
        Color LineColor;
        private void button5_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "DLL-files(*.dll)|*.dll|All files(*.*)|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string libpath = openFileDialog1.FileName;
                try
                {
                    Assembly asm = Assembly.LoadFrom(libpath);

                    Type[] types = asm.GetTypes().Where(t => t.BaseType == typeof(Figure)).ToArray();

                    foreach (Type t in types)
                    {
                        ConstructorInfo ci = t.GetConstructor(new Type[] { typeof(List<Int32>), typeof(float), typeof(Color) });
                        constr elem;
                        elem.Name = t.Name;
                        elem.cinf = ci;
                        coll.Add(elem);
                        this.comboBox1.Items.Add(t.Name);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Ошибка подключения dll");
                }
            }

            openFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
        }
       
        public void SaveFile()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "MySave.dat"), FileMode.Create))
            {
                foreach (Figure elem in MyList)
                {
                    formatter.Serialize(fs, elem);
                }
                MessageBox.Show("Успешно сохранено!");
            }
            MyList.Clear();
        }
        public void LoadFile()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "MySave.dat"), FileMode.OpenOrCreate))
            {
                bool suc = true;
                while (fs.Position < fs.Length)
                {
                    try
                    {
                        Figure elem = (Figure)formatter.Deserialize(fs, null);
                        MyList.Add(elem);
                    }
                    catch (SerializationException)
                    {
                        MessageBox.Show("Похоже, одной из фигур больше нет в списке плагинов.");
                        suc = false;
                        break;
                    }
                }
                if (suc)
                    MessageBox.Show("Успешно загружено!");
            }
        }
       
        public Form1()
        {
            InitializeComponent();
            Type[] types = Assembly.GetExecutingAssembly().GetTypes().Where(t =>typeof(Figure).IsAssignableFrom(t)).ToArray();
            foreach (Type t in types)
            {
                this.comboBox1.Items.Add(t);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.comboBox1.Items.Clear();
            coll.Clear();
            Type[] types = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.BaseType == typeof(Figure)).ToArray();
            foreach (Type t in types)
            {
                this.comboBox1.Items.Add(t.ToString().Remove(0, 12));
                Type mytype = Type.GetType(t.ToString(), false, true);
                ConstructorInfo ci = mytype.GetConstructor(new Type[] { typeof(List<Int32>), typeof(float), typeof(Color) });
                constr elem;
                elem.Name = t.ToString().Remove(0, 12);
                elem.cinf = ci;
                coll.Add(elem);
            }
            openFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
        }
 


        private void button6_Click(object sender, EventArgs e)
        {
            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);
            //вызов отрисовки листа
            foreach (Figure Obj_Draw in MyList)
            {
                Figure tmp = (Figure)Obj_Draw;
                tmp.Draw(g);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //стереть всё
            pictureBox1.Image = null;
        }
        private void button8_Click(object sender, EventArgs e)
        {
            MyList.Clear();
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                LineColor = colorDialog1.Color;
                textBox1.BackColor = LineColor;
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {

                LineColor = colorDialog1.Color;
                textBox2.BackColor = LineColor;
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            List<Int32> PointsList = new List<Int32>();
            Rectangle LocalRectangle = new Rectangle(0, 0, 0, 0);
            try
            {

                LineWidth = float.Parse(textBox1.Text);
                string Coordinate = "";
                for (int i = 0; i < textBox3.TextLength; i++)
                {
                    if (textBox3.Text[i] != ',')
                    {
                        Coordinate += textBox3.Text[i].ToString();
                    }
                    else
                    {
                        PointsList.Add(Convert.ToInt32(Coordinate));
                        Coordinate = "";
                    }
                }
                PointsList.Add(Convert.ToInt32(Coordinate));
                Coordinate = "";
                foreach (constr co in coll)
                {
                    if (String.Compare(co.Name, comboBox1.SelectedItem.ToString()) == 0)
                    {
                        object obj = co.cinf.Invoke(new object[] { PointsList, LineWidth, LineColor });
                        MyList.Add((Figure)obj);
                    }
                }
                PointsList.Clear();
            }
            catch(Exception)
            {
                MessageBox.Show("Введите все поля данных фигуры, и если можно, пожалуйста правильно, а то мне будет грустно(");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LoadFile();
        }

 
    }

}


