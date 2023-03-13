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
    public partial class ListadoCursos : Form
    {
        public ListadoCursos()
        {
            InitializeComponent();
        }

        private void Lector()
        {

            using (FileStream archivo = new FileStream(@".\Cursos.txt", FileMode.OpenOrCreate, FileAccess.Read))
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
                            string Curso = registro.Split(',')[4];
                            string Profesor = registro.Split(',')[5];

                            dataGridView1.Rows.Add(Legajo, Nombre, Apellido, DNI, Curso, Profesor);
                        }
                    }
                }
            }
        }

        private void BusquedaMateria()
        {
            FileStream archivo = new FileStream(@".\Materias.txt", FileMode.Open, FileAccess.Read);

            string Nombre = txtBusqueda1.Text;
            string Registro;

            using (StreamReader lector = new StreamReader(archivo))
            {
                while (!lector.EndOfStream)
                {
                    Registro = lector.ReadLine();

                    if (Registro != null)
                    {
                        if (Registro.Split(',')[0] == Nombre)
                        {
                            txtCurso.Text = Registro.Split(',')[0].ToString();
                            MessageBox.Show("La materia Existe");
                        }
                    }
                }
                archivo.Close();
                lector.Close();
            }
        }

        private void BusquedaProfesor()
        {
            FileStream archivo = new FileStream(@".\Profesores.txt", FileMode.OpenOrCreate, FileAccess.Read);

            StreamReader lector = new StreamReader(archivo);

            string Legajo = txtBusqueda2.Text;
            string Registro;

            while (!lector.EndOfStream)
            {
                Registro = lector.ReadLine();

                if (Registro != null)
                {
                    if (Registro.Split(',')[0] == Legajo)
                    {
                        txtProfesor.Text = Registro.Split(',')[1].ToString();
                        MessageBox.Show("El Profesor Existe");
                    }

                }
            }
            archivo.Close();
            lector.Close();

        }
        /*FileStream archivo = new FileStream(@".\Profesores.txt", FileMode.Open, FileAccess.ReadWrite);
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
         */
        private void BusquedaAlumno()
        {
            if (txtCurso.Text == "" || txtProfesor.Text == "")
            {
                MessageBox.Show("Ingrese el Curso y el Profesor previamente.");
            }
            else {
                FileStream archivo2 = new FileStream(@".\Cursos.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                FileStream archivo = new FileStream(@".\Alumnos.txt", FileMode.Open, FileAccess.Read);

                string Legajo = txtBusqueda3.Text;
                string Materia = txtBusqueda1.Text;
                string Registro;
                string Registro2;
                bool yaexiste = false;

                using (StreamReader lector2 = new StreamReader(archivo2))
                {
                    while (!lector2.EndOfStream)
                    {
                        Registro = lector2.ReadLine();

                        if (Registro != null && yaexiste == false)
                        {
                            if (Registro.Split(',')[0] == Legajo && Registro.Split(',')[4] == Materia)
                            {
                                MessageBox.Show("El alumno ya esta en el curso");
                                yaexiste = true;
                            }
                        }
                    }
                    if (yaexiste == false)
                    {
                        using (StreamReader lector = new StreamReader(archivo))
                        {
                            while (!lector.EndOfStream)
                            {
                                Registro2 = lector.ReadLine();

                                if (Registro2 != null)
                                {
                                    if (Registro2.Split(',')[0].ToString() == (Legajo))
                                    {
                                        using (StreamWriter escritor = new StreamWriter(archivo2))
                                        {
                                            string Legajo1 = Registro2.Split(',')[0].ToString();
                                            string Nombre = Registro2.Split(',')[1].ToString();
                                            string Apellido = Registro2.Split(',')[2].ToString();
                                            string DNI = Registro2.Split(',')[3].ToString();
                                            string alumnoecontrado = (Legajo1 + "," + Nombre + "," + Apellido + "," + DNI + "," + txtCurso.Text + "," + txtProfesor.Text);
                                            dataGridView1.Rows.Add(Legajo, Nombre, Apellido, DNI, txtCurso.Text, txtProfesor.Text);
                                            escritor.WriteLine(alumnoecontrado);

                                        }
                                    }
                                }

                            }lector.Close();
                        }
                    }lector2.Close();
                    
                }archivo.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BusquedaMateria();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BusquedaProfesor();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            BusquedaAlumno();
        }

        private void ListadoCursos_Load(object sender, EventArgs e)
        {
            Lector();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            txtCurso.Text = "";
            txtProfesor.Text = "";
        }
    }
}
