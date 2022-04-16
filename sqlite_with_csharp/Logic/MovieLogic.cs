using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//agregar la referencia
using System.Configuration;
//agrefar carpeta de model
using sqlite_with_csharp.Model;
//agregar sqlite
using System.Data.SQLite;


namespace sqlite_with_csharp.Logic
{
    public class MovieLogic
    {
        //traer la cadena de conexion
        private static string connString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

        //realizar patron singleton, el cual permite usar una sola instancia de la clase
        private static MovieLogic _instance = null;

        //constructor
        public MovieLogic()
        {

        }

        public static MovieLogic Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MovieLogic();
                return _instance;
            }
        }

        //crear metodo para guardar|insertar datos
        public bool Save(Movie obj)
        {
            bool response = true;

            using (SQLiteConnection connection = new SQLiteConnection(connString))
            {
                connection.Open();
                string query = "insert into movies(movie_tittle,  movie_genre) values (@movie_tittle, @movie_genre)";

                SQLiteCommand cmd = new SQLiteCommand(query, connection);
                cmd.Parameters.Add(new SQLiteParameter("@movie_tittle", obj.movie_tittle));
                cmd.Parameters.Add(new SQLiteParameter("@movie_genre", obj.movie_genre));
                cmd.CommandType = System.Data.CommandType.Text;

                //validar si la ejecucion ha sido completada
                if (cmd.ExecuteNonQuery() < 1)
                {
                    response = false;
                }
            }
            return response;
        }


        //Leer la data de la db
        public List<Movie> getList()
        {
            List<Movie> oList = new List<Movie>();

            using (SQLiteConnection connection = new SQLiteConnection(connString))
            {
                connection.Open();
                string query = "select id, movie_tittle,  movie_genre from movies";
                SQLiteCommand cmd = new SQLiteCommand(query, connection);
                cmd.CommandType = System.Data.CommandType.Text;

                //ejecutar dentro del using el bloque de codigo que va a leer y luego finalizar la conexion
                using (SQLiteDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oList.Add(new Movie()
                        {
                            id = int.Parse(dr["id"].ToString()),
                            movie_tittle = dr["movie_tittle"].ToString(),
                            movie_genre = dr["movie_genre"].ToString()
                        });
                    }
                }
                return oList;
            }

        }

        //crear metodo para editar|actualizar datos
        public bool Update(Movie obj)
        {
            bool response = true;

            using (SQLiteConnection connection = new SQLiteConnection(connString))
            {
                connection.Open();
                string query = "update movies set movie_tittle = @movie_tittle,  movie_genre = @movie_genre WHERE id = @id";

                SQLiteCommand cmd = new SQLiteCommand(query, connection);
                cmd.Parameters.Add(new SQLiteParameter("@id", obj.id));
                cmd.Parameters.Add(new SQLiteParameter("@movie_tittle", obj.movie_tittle));
                cmd.Parameters.Add(new SQLiteParameter("@movie_genre", obj.movie_genre));
                cmd.CommandType = System.Data.CommandType.Text;

                //validar si la ejecucion ha sido completada
                if (cmd.ExecuteNonQuery() < 1)
                {
                    response = false;
                }
            }
            return response;
        }
    }
}
