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
    public partial class ListadoAlumnos : Form
    {
        public ListadoAlumnos()
        {
            InitializeComponent();
        }

        List<Alumnos> ListaDeAlumnos = new List<Alumnos>();
        public void ListarDGV()
        {

            dataGridView1.Rows.Add(txtLegajo.Text, txtNombre.Text, txtApellido.Text, txtDNI.Text);



        }
        private void Lector()
        {

            using (FileStream archivo = new FileStream(@".\Alumnos.txt", FileMode.OpenOrCreate, FileAccess.Read))
            {
                using (StreamReader lector = new StreamReader(archivo))
                {
                    while (!lector.EndOfStream)
                    {
                        string registro = lector.ReadLine();

                        if (registro != null)
                        {
                            string Legajo = registro.Split(',')[0];
                            string Nombre = registro.Split(',')[1];
                            string Apellido = registro.Split(',')[2];
                            string DNI = registro.Split(',')[3];


                            dataGridView1.Rows.Add(Legajo, Nombre, Apellido, DNI);
                        }
                    }
                }
            }
        }

        private void AltaAlumno()
        {
            FileStream archivo = new FileStream(@".\Alumnos.txt", FileMode.Append, FileAccess.Write);

            using (StreamWriter escritor = new StreamWriter(archivo))
            {
                string registro = txtLegajo.Text + "," + txtNombre.Text + "," + txtApellido.Text + "," + txtDNI.Text;
                escritor.WriteLine(registro);
                txtNombre.Text = "";
                txtApellido.Text = "";
                txtDNI.Text = "";
                txtLegajo.Text = "";
                escritor.Close();
            }
            txtLegajo.Focus();
            archivo.Close();
        }

        private void BajaAlumno()
        {
            FileStream archivo = new FileStream(@".\Alumnos.txt", FileMode.Open, FileAccess.Read);
            FileStream auxiliar = new FileStream(@".\AuxiliarAlumno.txt", FileMode.Append, FileAccess.Write);


            StreamReader lector = new StreamReader(archivo);
            StreamWriter escritor = new StreamWriter(auxiliar);

            string Legajo = txtLegajo.Text;
            string Registro;

            while (!lector.EndOfStream)
            {
                Registro = lector.ReadLine();

                if (Registro != null)
                {
                    if (Registro.Split(',')[0] != Legajo)
                    {
                        escritor.WriteLine(Registro);
                    }
                }
            }
            lector.Close();
            escritor.Close();
            archivo.Close();
            auxiliar.Close();

            File.Delete(@".\Alumnos.txt");
            File.Move(@".\AuxiliarAlumno.txt", @".\Alumnos.txt");

            txtLegajo.Text = "";
            txtLegajo.Focus();
            ListarDGV();

        }
        private void ModificarAlumno()
        {
            FileStream archivo = new FileStream(@".\Alumnos.txt", FileMode.Open, FileAccess.Read);
            FileStream auxiliar = new FileStream(@".\AuxiliarAlumno.txt", FileMode.Append, FileAccess.Write);


            StreamReader lector = new StreamReader(archivo);
            StreamWriter escritor = new StreamWriter(auxiliar);

            string Legajo = txtLegajo.Text;
            string Registro;

            while (!lector.EndOfStream)
            {
                Registro = lector.ReadLine();

                if (Registro != null)
                {
                    if (Registro.Split(',')[0] == Legajo)
                    {
                        Registro = txtLegajo.Text + "," + txtNombre.Text + "," + txtApellido.Text + "," + txtDNI.Text;
                    }

                    escritor.WriteLine(Registro);
                }
            }

            lector.Close();
            escritor.Close();
            archivo.Close();
            auxiliar.Close();


            File.Delete(@".\Alumnos.txt"); 

            File.Move(@".\AuxiliarAlumno.txt", @".\Alumnos.txt");

            txtLegajo.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtDNI.Text = "";

            txtLegajo.Focus();
            ListarDGV();
        }

        public bool ValidaNombreApellido()
        {
            if (txtNombre.Text.All(char.IsLetter) && txtNombre.Text != "" && txtApellido.Text.All(char.IsLetter) && txtApellido.Text != "")
            {
                return true;
            }
            else
            {
                MessageBox.Show("El nombre ingresado debe poseer unicamente letras");
                txtNombre.Focus();
                return false;
            }
        }
        public bool ValidaDni()
        {
            bool validador = false;
            if (txtDNI.Text.Length == 8)
            {
                validador = true;
            }
            else
            {
                MessageBox.Show("El dni debe tener 8 caracteres");
                txtDNI.Focus();
                validador = false;
                return false;
            }

            if (txtDNI.Text.All(char.IsDigit) && validador == true)
            {
                return true;
            }
            else
            {
                MessageBox.Show("El DNI ingresado debe poseer 8 numeros");
                return false;
            }

        }

        public bool ValidaLegajo()
        {
            if (txtLegajo.Text.All(char.IsDigit) && (txtLegajo.Text != ""))
            {
                return true;
            }
            else
            {
                MessageBox.Show("El Legajo ingresado debe unicamente numeros");
                return false;
            }

        }


        private void button1_Click(object sender, EventArgs e)
        {
            
            if (ValidaLegajo() && ValidaDni() && ValidaNombreApellido())
            {
                ListarDGV();
                AltaAlumno();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ModificarAlumno();
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            Lector();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BajaAlumno();
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            Lector();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtLegajo.Text = "";
            txtDNI.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 Form = new Form1();
            Form.Show();
        }

        private void ListadoAlumnos_Load(object sender, EventArgs e)
        {
            Lector();
        }
    }
}
