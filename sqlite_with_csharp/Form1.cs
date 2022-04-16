using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//Importar carpetas donde estan las clases
using sqlite_with_csharp.Model;
using sqlite_with_csharp.Logic;

namespace sqlite_with_csharp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Movie item = new Movie() {
                //id = txt_Id.Text, //El id solo se usa cuando se va a editar o eliminar
                movie_tittle = txtTittle.Text,
                movie_genre = txtGenre.Text
            };

            //se pasan los parametros a la funcion de guardar
            bool response = MovieLogic.Instance.Save(item);

            //si es verdadero se llama al metodo
            if (response)
            {
                show_movies();
            }
        }

        //metodo para limpiar datagrid y llamar a funcion que lista los registros
        public void show_movies()
        {
            //dgvMovies.DataSource = null;
            dgvMovies.DataSource = MovieLogic.Instance.getList();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            show_movies();
        }
    }
}
