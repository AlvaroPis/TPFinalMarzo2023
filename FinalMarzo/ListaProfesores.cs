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
    public partial class ListaProfesores : Form
    {
        public ListaProfesores()
        {
            InitializeComponent();
        }

        public void ListarDGV()
        {

            dataGridView1.Rows.Add(txtLegajo.Text, txtNombre.Text, txtApellido.Text, txtDNI.Text);



        }
        private void AltaProfesor()
        {
            FileStream archivo = new FileStream(@".\Profesores.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            string Legajo = txtLegajo.Text;
            string Registro;
            bool valida = false;

            using (StreamReader lector = new StreamReader(archivo))
            {
                while (!lector.EndOfStream)
                {
                    Registro = lector.ReadLine();

                    if (Registro != null && valida == false)
                    {
                        if (Registro.Split(',')[0] == Legajo)
                        {
                            MessageBox.Show("El Legajo del Profesor Ingresado ya existe");
                            valida = true;
                            lector.ReadLine();
                        }
                        else if (Registro.Split(',')[0] != Legajo && valida == false)
                        {
                            lector.ReadLine();
                        }
                    } 
                }
                if (valida == false)
                {
                    using (StreamWriter escritor = new StreamWriter(archivo))
                    {
                        ListarDGV();
                        string registro = txtLegajo.Text + "," + txtNombre.Text + "," + txtApellido.Text + "," + txtDNI.Text;
                        escritor.WriteLine(registro);
                        txtNombre.Text = "";
                        txtApellido.Text = "";
                        txtDNI.Text = "";
                        txtLegajo.Text = "";
                    }
                    txtLegajo.Focus();
                }
                lector.Close();
            } archivo.Close();
        }

        private void BajaProfesor()
        {
            FileStream archivo = new FileStream(@".\Profesores.txt", FileMode.Open, FileAccess.Read);
            FileStream auxiliar = new FileStream(@".\AuxiliarProfesores.txt", FileMode.Append, FileAccess.Write);


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

            File.Delete(@".\Profesores.txt");
            File.Move(@".\AuxiliarProfesores.txt", @".\Profesores.txt");

            txtLegajo.Text = "";
            txtLegajo.Focus();
            ListarDGV();

        }

        private void ModificarProfesor()
        {
            FileStream archivo = new FileStream(@".\Profesores.txt", FileMode.Open, FileAccess.Read);
            FileStream auxiliar = new FileStream(@".\AuxiliarProfesor.txt", FileMode.Append, FileAccess.Write);


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


            File.Delete(@".\Profesores.txt");

            File.Move(@".\AuxiliarProfesor.txt", @".\Profesores.txt");

            txtLegajo.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtDNI.Text = "";

            txtLegajo.Focus();
            ListarDGV();
        }
        private void Lector()
        {

            using (FileStream archivo = new FileStream(@".\Profesores.txt", FileMode.OpenOrCreate, FileAccess.Read))
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
                MessageBox.Show("El Legajo ingresado debe poseer unicamente numeros");
                return false;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidaLegajo() && ValidaDni() && ValidaNombreApellido()) 
            {
                AltaProfesor();
                txtLegajo.Focus();
            }
                
        }

        private void button4_Click(object sender, EventArgs e)
        {
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtLegajo.Text = "";
            txtDNI.Text = "";
            txtLegajo.Focus();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtLegajo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDNI_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtApellido_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            BajaProfesor();
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            txtLegajo.Focus();
            Lector();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ModificarProfesor();
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ListaProfesores_Load(object sender, EventArgs e)
        {
            Lector();
        }
    }
}
