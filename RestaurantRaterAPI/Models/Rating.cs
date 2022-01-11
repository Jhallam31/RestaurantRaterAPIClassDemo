using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RestaurantRaterAPI.Models
{
    public class Rating
    {
        //Primary Key
        [Key]
        public int Id { get; set; }

        //Foreign Key --> Reference to a PK from another table to link two objects together
        [ForeignKey(nameof(Restaurant))]
        public int RestaurantId { get; set; }
        //virtual keyword allows EF to recognize a property that can be overwritten.
        //Navigation Property
        public virtual Restaurant Restaurant { get; set;}
        [Required]
        [Range(0,10)]
        public double FoodScore { get; set; }
        [Required, Range(0, 10)]
        public double CleanlinessScore { get; set; }
        [Required, Range(0, 10)]
        public double EnvironmentScore { get; set; }

        //no setter means this is readonly
        public double AverageRating
        {
            get
            {
                var totalScore = FoodScore + EnvironmentScore + CleanlinessScore;
                return totalScore / 3;
            }
        }
    }
}