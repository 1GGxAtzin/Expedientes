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

namespace Expedientes
{
    public partial class Form1 : Form
    {
        string Path = @"C:\\Users\\Atz1NNN\\Desktop\\Expedientes";
        ABB arbol = new ABB();
        public Form1()
        {
            
            InitializeComponent();

            
        }
        public class Pacientes
        {
            public string name; 
            public int edad;
            public string coronavirus;
            public string expediente;
            public Pacientes()
            {
                name = "";
                edad = 0;
                coronavirus = "";
                expediente = "";
            }
        }
        
            
        
        public class Nodo
        {
            public Nodo hi;
            public Nodo hd;
            public Pacientes valor;

            public Nodo()
            {
                hi = null;
                hd = null;
                valor = new Pacientes();

            }
        }
        public class ABB
        {
            public Nodo raiz;

            public ABB()
            {
                raiz = null;
            }
            public void insertar(Pacientes valor)
            {
                if (raiz == null)
                {
                    raiz = new Nodo();
                    raiz.valor = valor;
                }
                else
                {
                    Nodo nuevo = new Nodo();
                    nuevo.valor = valor;
                    nuevo.hi = null;
                    nuevo.hd = null;
                    Nodo anterior = null, recorrer;
                    recorrer = raiz;
                    while (recorrer != null)
                    {
                        anterior = recorrer;
                        if (String.Compare(recorrer.valor.name, valor.name)< 0)
                        {
                            recorrer = recorrer.hd;

                        }
                        else
                        {
                            recorrer = recorrer.hi;
                        }
                    }
                    if (String.Compare(valor.name, anterior.valor.name) > 0)
                    {
                        anterior.hd = nuevo;
                    }
                    else
                    {
                        anterior.hi = nuevo;
                    }
                }
            }
            public Pacientes Buscar(string valor)
            {
                if (raiz == null)
                {
                    MessageBox.Show("No hay nada");
                    return new Pacientes();
                }
                else
                {
                   
                    Nodo anterior = null, recorrer;
                    recorrer = raiz;
                    while (recorrer != null)
                    {
                        anterior = recorrer;
                        if (String.Compare(recorrer.valor.name, valor) < 0)
                        {
                            recorrer = recorrer.hd;

                        }
                        else if  (String.Compare(recorrer.valor.name, valor) > 0)
                        {
                            recorrer = recorrer.hi;
                        }
                        else
                        {
                            return recorrer.valor;
                        }

                    }                 
                }
                MessageBox.Show("No hay nada");
                return new Pacientes();
            }
            
        }

       
        private void buscarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            Pacientes chido = arbol.Buscar(textoabuscar.Text);         
            a.Text = chido.name;
            b.Text = chido.edad.ToString();
            c.Text = chido.coronavirus;
            d.Text = chido.expediente;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetFiles();
        }
        public void GetFiles()
        {
            
            
            string aux = "", nombre;
            string[] archivos = Directory.GetFiles(Path); 
            foreach(var archivo in archivos)
            {
                nombre = archivo.Substring(0, archivo.Length - 4);//para eliminar el ".txt"

                aux = "";
                for (int i = nombre.Length - 1; nombre[i] != '\\' && i >= 0; --i)
                {
                    aux += nombre[i];
                    
                }
                nombre = "";
                for (int i = aux.Length - 1; i >= 0; --i)
                    nombre += aux[i];

                Pacientes nom = new Pacientes();
                nom.name = nombre;
                arbol.insertar(nom);


                string[] contenido = File.ReadAllLines(archivo);
                int contador = 0;
                foreach (string linea in contenido)
                {
                    if (contador == 0)
                    {
                        nom.edad = int.Parse(linea);
                        contador++;
                    }
                    else if (contador == 1)
                    {
                        nom.coronavirus = linea;
                        contador++;

                    }
                    else if (contador >= 2)
                    {
                        nom.expediente += linea;
                        
                    }

                }
               

            }
            
            


        }

        private void button1_Click(object sender, EventArgs e)
        {
            Pacientes chido = arbol.Buscar(a.Text);
            chido.edad =  int.Parse(b.Text);
            chido.coronavirus = c.Text;
            chido.expediente = d.Text;      
            File.WriteAllText("C:\\Users\\Atz1NNN\\Desktop\\Expedientes\\" + chido.name + ".txt", chido.edad + "\n" + chido.coronavirus + "\n" + chido.expediente + "\n");
            a.Text = "";
            b.Text = "";
            c.Text = "";
            d.Text = "";



        }
    }
}
