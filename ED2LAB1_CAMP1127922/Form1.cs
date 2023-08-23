using ED2LAB1_CAMP1127922.DS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
                CargarDatosDesdeCSV(rutaArchivo);
                button1.Enabled = false;
                button1.Text = "Archivo cargado satisfactoriamente";
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
        }
}
