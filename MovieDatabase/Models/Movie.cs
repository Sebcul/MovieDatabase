using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieDatabase.Models
{
    public class Movie
    {
        private double _averageScore;

        public Movie()
        {
            Genres = new List<Genre>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int ProductionYear { get; set; }

        public double AverageScore
        {
            get
            {
                _averageScore = 0;
                foreach (var rating in Ratings)
                {
                    _averageScore += rating.Score;
                }
                _averageScore = (_averageScore / Ratings.Count);
                return _averageScore;
            }
        }

        public Director Director { get; set; }
        public int DirectorId { get; set; }

        public ICollection<Rating> Ratings { get; set; }
        public ICollection<Genre> Genres { get; set; }
        public ICollection<MovieActors> MovieActors { get; set; }
    }
}