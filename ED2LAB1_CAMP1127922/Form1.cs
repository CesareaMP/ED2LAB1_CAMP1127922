using ED2LAB1_CAMP1127922.DS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ED2LAB1_CAMP1127922
{
    public partial class Form1 : Form
    {
        ABB arbol = new ABB();
        int inserts = 0;
        int deletes = 0;
        int patchs = 0;
        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos CSV (*.csv)|*.csv";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string rutaArchivo = openFileDialog.FileName;
                Stopwatch stopwatch = new Stopwatch();

                stopwatch.Start();
                CargarDatosDesdeCSV(rutaArchivo);
                stopwatch.Stop();
                long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
                showmslbl.Text = $"{elapsedMilliseconds}ms";


                button1.Enabled = false;
                button1.Text = "Archivo cargado satisfactoriamente";
                edTabControl.Enabled = true;
                try
                {
                   
                }
                catch (Exception)
                {
                    MessageBox.Show("El archivo cargado es imposible de leer");
                }
                
            }
            else
            {
                MessageBox.Show("Seleccione el archivo a utilizar");
            }

        }
        private void CargarDatosDesdeCSV(string rutaArchivo)
        {
            using (var reader = new StreamReader(rutaArchivo))
            {
                while (reader.EndOfStream == false)
                {
                    var content = reader.ReadLine();
                    string action = content.Split(';')[0];
                    string info = content.Split(';')[1];
                    var persona = JsonConvert.DeserializeObject<Person>(info);                                       
                    if (action == "INSERT") { arbol.Add(persona); inserts++; }                   
                    else if (action == "PATCH") { arbol.Patch(persona); patchs++; }
                    else if (action == "DELETE") { arbol.Delete(persona); deletes++; }                  
                }
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void buscarbtn_Click(object sender, EventArgs e)
        {
            string dpi = dpitxt.Text;
            List<Person> listabusquedas = new List<Person>();
            Person aux;
            List<string> jsons = new List<string>();
            int cont = 0;
            if (nombretxt.Text == "") MessageBox.Show("Ingrese un nombre a buscar");
            else
            {
                listabusquedas.Add(arbol.SearchDpi(dpi));
                if (listabusquedas != null)
                {
                    cont = listabusquedas.Count();
                    for (int i = 0; i < cont; i++)
                    {
                        aux = listabusquedas[i];
                        var objpersona = new Person
                        {
                            name = aux.name,
                            dpi = aux.dpi,
                            datebirth = aux.datebirth,
                            address = aux.address,
                            companies = aux.companies
                        };
                        string jsonString = JsonConvert.SerializeObject(objpersona, Formatting.None);

                        jsons.Add(jsonString);
                    }
                    crearCSV(dpi, jsons);
                }
                else MessageBox.Show("no se encontraron datos asociados al dpi" + dpi);
            }
            dpitxt.Text = "";
        }
        public void crearCSV(string nombre, List<string> jsons)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Archivos CSV|*.csv";
            saveFileDialog.Title = "Guardar archivo CSV, nombre sugerido: " + nombre;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                    {
                        foreach (string json in jsons)
                        {
                            writer.WriteLine(json);
                        }
                    }

                    MessageBox.Show("Archivo CSV creado exitosamente.");
                    Process.Start(saveFileDialog.FileName);//abre el archivo creado
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al crear el archivo CSV: " + ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string nombre = nombretxt.Text;
            List<Person> listabusquedas = new List<Person>();
            Person aux;
            List<string> jsons = new List<string>();
            int cont = 0;
            if (nombretxt.Text == "") MessageBox.Show("Ingrese un nombre a buscar");
            else
            {
                listabusquedas = arbol.SearchName(nombre);
                if (listabusquedas != null)
                {
                    cont = listabusquedas.Count();
                    for (int i = 0; i < cont; i++)
                    {
                        aux = listabusquedas[i];
                        var objpersona = new Person
                        {
                            name = aux.name,
                            dpi = aux.dpi,
                            datebirth = aux.datebirth,
                            address = aux.address,
                            companies = aux.companies
                        };
                        string jsonString = JsonConvert.SerializeObject(objpersona, Formatting.None);

                        jsons.Add(jsonString);
                    }
                    crearCSV(nombre, jsons);
                }
                else MessageBox.Show("no se encontraron datos asociados al nombre de " + nombre);
            }
            nombretxt.Text = "";
        }

        private void dpibtn_Click(object sender, EventArgs e)
        {
            string dpi = dpitxt.Text;
            Person persona;
            string jsonString;
            if (dpitxt.Text == "") MessageBox.Show("Ingrese un nombre a buscar");
            else
            {
                persona = arbol.SearchDpi(dpi);
                if (persona != null)
                {
                    jsonString = JsonConvert.SerializeObject(persona, Formatting.Indented);
                    MessageBox.Show(jsonString);
                }
                else MessageBox.Show("No se encontraron datos asociados al DPI: " + dpi);
            }                   
        }

        private void nombrebtn_Click(object sender, EventArgs e)
        {
            string nombre = nombretxt.Text;
            List<Person> listabusquedas = new List<Person>();
            Person aux;
            List<string> jsons = new List<string>();
            int cont = 0;
            if (nombretxt.Text == "") MessageBox.Show("Ingrese un nombre a buscar");
            else
            {
                listabusquedas = arbol.SearchName(nombre);
                if (listabusquedas.Count()!=0)
                {
                    cont = listabusquedas.Count();
                    for (int i = 0; i < cont; i++)
                    {
                        aux = listabusquedas[i];
                        var objpersona = new Person
                        {
                            name = aux.name,
                            dpi = aux.dpi,
                            datebirth = aux.datebirth,
                            address = aux.address,
                            companies = aux.companies
                        };
                        string jsonString = JsonConvert.SerializeObject(objpersona, Formatting.None);

                        jsons.Add(jsonString);
                    }
                    crearCSV(nombre, jsons);
                }
                else MessageBox.Show("no se encontraron datos asociados al nombre de " + nombre);
            }
            nombretxt.Text = "";
        }
    }
}
