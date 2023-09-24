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
        string rutaArchivo;
        string rutaCarpeta;
        int inserts = 0;
        int deletes = 0;
        int patchs = 0;
        Aritmetica code = new Aritmetica();
        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos CSV (*.csv)|*.csv";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                rutaArchivo = openFileDialog.FileName;
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                CargarDatosDesdeCSV(rutaArchivo);
                //crearCSV(rutaArchivo, "ARBOL_CODIFICADO", arbol.PrintTree());
                stopwatch.Stop();
                long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
                showmslbl.Text = $"{elapsedMilliseconds}ms";
                button1.Enabled = false;
                button1.Text = "Archivo cargado satisfactoriamente";
                edTabControl.Enabled = true;    
            }
            else
            {
                MessageBox.Show("Seleccione el archivo a utilizar");
            }
            }
            catch (Exception)
            {
                MessageBox.Show("El archivo cargado es imposible de leer");
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
                    persona.dpi=code.Encode(persona.dpi);
                    for (int i = 0; i < persona.companies.Count; i++)
                    {
                        persona.companies[i] = code.Encode(persona.companies[i]);
                    }
                    if (action == "INSERT") { arbol.Add(persona); inserts++; }                   
                    else if (action == "PATCH") { arbol.Patch(persona); patchs++; }
                    else if (action == "DELETE") { arbol.Delete(persona); deletes++; }
                }
            }
        }        

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }        
        public void crearCSV(string ruta, string nombreArchivo, List<string> jsons)
        {
            int indiceUltimaDiagonal = ruta.LastIndexOf('\\');
            ruta = ruta.Substring(0, indiceUltimaDiagonal) + "\\Exports";
            CrearCarpeta(ruta);
            try
            {
                string filePath = Path.Combine(ruta, nombreArchivo + ".csv");

                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (string json in jsons)
                    {
                        writer.WriteLine(json);
                    }
                }

                MessageBox.Show("Archivo CSV creado exitosamente en " + filePath);
                Process.Start(filePath); // Abrir el archivo creado
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear el archivo CSV: " + ex.Message);
            }
        }

        public void CrearCarpeta(string ruta)
        {
            try
            {
                // Verifica si la carpeta no existe en la ruta proporcionada.
                if (!Directory.Exists(ruta))
                {
                    // Crea la carpeta si no existe.
                    Directory.CreateDirectory(ruta);
                    Console.WriteLine("Carpeta creada en: " + ruta);
                }
                else
                {
                    Console.WriteLine("La carpeta ya existe en: " + ruta);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al crear la carpeta: " + ex.Message);
            }
        }

        private void dpibtn_Click(object sender, EventArgs e)
        {
            string dpi = code.Encode(dpitxt.Text);
            Person persona;
            string jsonString;
            if (dpitxt.Text == "") MessageBox.Show("Ingrese un nombre a buscar");
            else
            {
                persona = arbol.SearchDpi(dpi);                
                if (persona != null)
                {
                    persona.dpi = code.Decode(dpi);
                    for (int i = 0; i < persona.companies.Count; i++)
                    {
                        persona.companies[i] = code.Decode(persona.companies[i]);
                    }
                    jsonString = JsonConvert.SerializeObject(persona, Formatting.Indented);
                    MessageBox.Show(jsonString);
                    persona.dpi = code.Encode(persona.dpi);
                    for (int i = 0; i < persona.companies.Count; i++)
                    {
                        persona.companies[i] = code.Encode(persona.companies[i]);
                    }
                }

                else MessageBox.Show("No se encontraron datos asociados al DPI: " + dpitxt.Text);
            }
            dpitxt.Text = "";
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
                    crearCSV(rutaArchivo,nombre, jsons);
                }
                else MessageBox.Show("no se encontraron datos asociados al nombre de " + nombre);
            }
            nombretxt.Text = "";
        }
    }
}
