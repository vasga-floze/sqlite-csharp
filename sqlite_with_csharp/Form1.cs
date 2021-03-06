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
    public partial class frmInitial : Form
    {
        public frmInitial()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Movie item = new Movie()
            {
                //id = txt_Id.Text, //El id solo se usa cuando se va a editar o eliminar
                movie_tittle = txtTittle.Text,
                movie_genre = txtGenre.Text
            };

            if (txt_Id.Text != string.Empty)
            {
                const string message = "Para crear un nuevo registro esl campo de Id no se debe ingresar ya que es un campo autoincremental";
                const string caption = "Información";
                MessageBox.Show(message, caption, MessageBoxButtons.OK ,MessageBoxIcon.Warning);
            }
            else
            {
                //se pasan los parametros a la funcion de guardar
                bool response = MovieLogic.Instance.Save(item);
                //si es verdadero se llama al metodo
                if (response)
                {
                    clean(); //limpia los campos
                    show_movies();
                }
            }
        }

        //metodo para limpiar datagrid y llamar a funcion que lista los registros
        public void show_movies()
        {
            dgvMovies.DataSource = null;
            dgvMovies.DataSource = MovieLogic.Instance.getList();
        }

        //metodo para limpiar los textbox
        public void clean()
        {
            txt_Id.Text = "";
            txtTittle.Text = "";
            txtGenre.Text = "";
            txtTittle.Focus();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            show_movies();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            if (txt_Id.Text == string.Empty)
            {
                const string message = "Debe ingresar un Id para actualizar un registro";
                const string caption = "Información";
                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //se almacenan los valores
                Movie item = new Movie()
                {
                    id = int.Parse(txt_Id.Text),
                    movie_tittle = txtTittle.Text,
                    movie_genre = txtGenre.Text
                };

                //se pasan los parametros a la funcion de editar
                bool response = MovieLogic.Instance.Update(item);

                //si es verdadero se llama al metodo de mostrar
                if (response)
                {
                    clean(); //limpia los campos
                    show_movies();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            
            if (txt_Id.Text == string.Empty)
            {
                const string message = "Debe ingresar un Id para eliminarlo";
                const string caption = "Información";
                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //se almacenan los valores
                Movie item = new Movie()
                {
                    id = int.Parse(txt_Id.Text)
                };

                //se pasan los parametros a la funcion de eliminar
                bool response = MovieLogic.Instance.Delete(item);

                //si es verdadero se llama al metodo de mostrar
                if (response)
                {
                    clean(); //limpia los campos
                    show_movies();
                }
            }
        }
    }
}
