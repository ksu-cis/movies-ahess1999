using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Movies.Pages
{
    public class IndexModel : PageModel
    {

        MovieDatabase movieDatabase = new MovieDatabase();

        public List<Movie> Movies;

        [BindProperty]
        public string search { get; set; }

        [BindProperty]
        public List<string> rating { get; set; } = new List<string>();

        [BindProperty]
        public float? minIMDB { get; set; }

        [BindProperty]
        public float? maxIMDB { get; set; }


        public void OnGet()
        {
            Movies = MovieDatabase.All;
        }

        public void OnPost(string search, List<string> rating, float? minIMDB, float? maxIMDB)
        {
            Movies = MovieDatabase.All;

            if (search != null && rating.Count != 0)
            {
                Movies = MovieDatabase.SearchAndFilter(search, rating);
            }
            else if (rating.Count != 0)
            {
                Movies = MovieDatabase.Filter(rating);
            }
            else if (search != null)
            {
                Movies = MovieDatabase.Search(Movies, search);
            }
            else if (minIMDB != null)
            {
                Movies = MovieDatabase.FilterByMinIMDB(Movies, (float)minIMDB);
            }
            else
            {
                Movies = movieDatabase.All;
            }
        }
    }
}
