using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestaurantRaterAPI.Models
{
    public class Restaurant
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]

        public string Address { get; set; }

        public virtual List<Rating> Ratings { get; set; } = new List<Rating>();

        public double Rating
        {
            get
            {
                double totalAverageRating = 0;
                //add all ratings
                foreach (var rating in Ratings)
                {
                    totalAverageRating += rating.AverageRating;
                }
                //get average from the total, use ternary to eliminate issues from dividing by zero
                return Ratings.Count > 0
                    ? Math.Round(totalAverageRating / Ratings.Count, 2) //comma followed by two specifies how many decimal places are returned.
                    : 0;
            }
        }

        public bool IsRecommended
        {
            get
            {
                return Rating > 8;
            }
        }

        //Average Food Rating
        //Average Cleanliness Rating
        //Average Environment Rating
    }
}