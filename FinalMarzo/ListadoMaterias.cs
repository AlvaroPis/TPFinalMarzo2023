using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace FinalMarzo
{
    public partial class ListadoMaterias : Form
    {
        public ListadoMaterias()
        {
            InitializeComponent();
        }

        public void ListarDGV()
        {
            dataGridView1.Rows.Add(txtNombre.Text, txtDescripcion.Text);
        }
        private void AltaMateria()
        {
            FileStream archivo = new FileStream(@".\Materias.txt", FileMode.Append, FileAccess.Write);

            using (StreamWriter escritor = new StreamWriter(archivo))
            {
                string registro = txtNombre.Text + "," + txtDescripcion.Text;
                escritor.WriteLine(registro);
                txtNombre.Text = "";
                txtDescripcion.Text = "";
            }
            txtNombre.Focus();
            archivo.Close();
        }
        private void BajaMateria()
        {
            FileStream archivo = new FileStream(@".\Materias.txt", FileMode.Open, FileAccess.Read);
            FileStream auxiliar = new FileStream(@".\AuxiliarMateria.txt", FileMode.Append, FileAccess.Write);


            StreamReader lector = new StreamReader(archivo);
            StreamWriter escritor = new StreamWriter(auxiliar);

            string Nombre = txtNombre.Text;
            string Registro;

            while (!lector.EndOfStream)
            {
                Registro = lector.ReadLine();

                if (Registro != null)
                {
                    if (Registro.Split(',')[0] != Nombre)
                    {
                        escritor.WriteLine(Registro);
                    }
                }
            }
            lector.Close();
            escritor.Close();
            archivo.Close();
            auxiliar.Close();

            File.Delete(@".\Materias.txt");
            File.Move(@".\AuxiliarMateria.txt", @".\Materias.txt");

            txtNombre.Text = "";
            txtNombre.Focus();
            ListarDGV();

        }
        private void ModificarMateria()
        {
            FileStream archivo = new FileStream(@".\Materias.txt", FileMode.Open, FileAccess.Read);
            FileStream auxiliar = new FileStream(@".\AuxiliarMateria.txt", FileMode.Append, FileAccess.Write);


            StreamReader lector = new StreamReader(archivo);
            StreamWriter escritor = new StreamWriter(auxiliar);

            string Nombre = txtNombre.Text;
            string Registro;

            while (!lector.EndOfStream)
            {
                Registro = lector.ReadLine();

                if (Registro != null)
                {
                    if (Registro.Split(',')[0] == Nombre)
                    {
                        Registro = txtNombre.Text + "," + txtDescripcion.Text;
                    }

                    escritor.WriteLine(Registro);
                }
            }

            lector.Close();
            escritor.Close();
            archivo.Close();
            auxiliar.Close();


            File.Delete(@".\Materias.txt");

            File.Move(@".\AuxiliarMateria.txt", @".\Materias.txt");

            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtNombre.Focus();
            ListarDGV();
        }
        private void Lector()
        {
            using (FileStream archivo = new FileStream(@".\Materias.txt", FileMode.OpenOrCreate, FileAccess.Read))
            {
                using (StreamReader lector = new StreamReader(archivo))
                {
                    while (!lector.EndOfStream)
                    {
                        string registro = lector.ReadLine();

                        if (registro != null)
                        {
                            string Nombre = registro.Split(',')[0];
                            string Descripcion = registro.Split(',')[1];


                            dataGridView1.Rows.Add(Nombre, Descripcion);
                        }
                    }
                }
            }
        }

        public bool ValidaNombreyDesc()
        {
            if (txtNombre.Text.All(char.IsLetter) && txtNombre.Text != "")
            {
                return true;
            }
            else
            {
                MessageBox.Show("El nombre/descripcion ingresada debe poseer unicamente letras");
                txtNombre.Focus();
                return false;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidaNombreyDesc()) 
            {
                ListarDGV();
                AltaMateria();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            txtNombre.Text = "";
            txtDescripcion.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BajaMateria();
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            Lector();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ModificarMateria();
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            Lector();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 Form = new Form1();
            Form.Show();
        }

        private void ListadoMaterias_Load(object sender, EventArgs e)
        {
            Lector();
        }
    }
}
