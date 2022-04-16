using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sqlite_with_csharp.Model
{
    public class Movie
    {
        public int id { get; set; }
        public string movie_tittle { get; set; }
        public string movie_genre { get; set; }
    }
}
