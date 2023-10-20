using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ED2LAB1_CAMP1127922
{
    public partial class MultiWindow : Form
    {
        List<string> listaDeTextos = null;
        public MultiWindow(List<string> l, string nombreform)
        {
            listaDeTextos = l;            
            InitializeComponent();
            this.Text = nombreform;
        }

        private void MultiWindow_Load(object sender, EventArgs e)
        {
            if (listaDeTextos != null && listaDeTextos.Count > 0)
            {
                for (int i = 0; i < listaDeTextos.Count; i++)
                {
                    // Crea una nueva TabPage
                    TabPage tabPage = new TabPage();
                    tabPage.Text = "No." + i.ToString(); // Usar el índice como nombre

                    // Crea un Panel en la TabPage
                    Panel panel = new Panel();
                    panel.AutoScroll = true; // Configura para mostrar barras de desplazamiento

                    // Crea un Label en el Panel
                    Label label = new Label();
                    label.Text = listaDeTextos[i];
                    label.AutoSize = true; // Permite que el Label ajuste su tamaño al contenido

                    // Agrega el Label al Panel
                    panel.Controls.Add(label);//776, 657
                    panel.MinimumSize = new Size(776, 640);

                    // Agrega el Panel a la TabPage
                    tabPage.Controls.Add(panel);

                    // Agrega la TabPage al TabControl
                    tabControl1.TabPages.Add(tabPage);
                }
            }
            // Establece el tamaño mínimo del TabControl para que sea lo suficientemente grande
            // para acomodar las TabPages sin cambiar su tamaño
            tabControl1.MinimumSize = new Size(776, 657); // Ajusta el tamaño mínimo según tus necesidades        
        }

        }
    }

