using RestaurantRaterAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestaurantRaterAPI.Controllers
{
    public class RatingController : ApiController
    {
        private readonly RestaurantDbContext _context = new RestaurantDbContext();

        //Create new ratings
        // POST api/Rating
        [HttpPost]
        public IHttpActionResult CreateRating([FromBody] Rating model)
        {
            if(model is null)
            {
                return BadRequest("Your request body can not be null");
            }
            if (ModelState.IsValid)
            {
                //find the restaurant by the restaurant Id in the model
                var restaurant = _context.Restaurants.Find(model.RestaurantId);
                //handle null exception
                if(restaurant == null)
                {
                    return BadRequest($"The target restaurant with the Id of {model.RestaurantId} does not exist");
                }
                //create the rating
                //Add to the rating table
                _context.Ratings.Add(model);

                //OR Add to the restaurant entity
                // restaurant.Ratings.Add(model);
                if (_context.SaveChanges() == 1)
                    return Ok($"You rated restaurant {model.Restaurant.Name} successfully");
                //store the model in the db
                return Ok("your rating was created!");
            }
            //if not valid, reject the request
            return BadRequest(ModelState);
        }

        //Get a rating by ID
        //api/Rating/{id}
        [HttpGet]
        public IHttpActionResult GetRatingById([FromUri] int id)
        {
            Rating rating = _context.Ratings.Find(id);

            if (rating == null)
            {
                return NotFound();
            }
            return Ok(rating);
        }
        //Get ALL Ratings
        //api/Rating
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Rating> ratings = _context.Ratings.ToList();
            return Ok(ratings);
        }
        //GET ALL Ratings for a specific restaurant by the restaurant ID

        //PUT (update)
        //api/Rating/{id}
        [HttpPut]
        public IHttpActionResult UpdateRating([FromUri] int id, [FromBody] Rating updatedRating)
        {
            //Check the Ids if they match
            if (id != updatedRating?.Id)
            {
                return BadRequest("Ids do not match");
            }
            //Check ModelState
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //Find the Rating in db
            Rating rating = _context.Ratings.Find(id);
            //If no match/doesn't exist, do something
            if (rating is null)
                return NotFound();
            //Update properties
            rating.FoodScore = rating.FoodScore;
            rating.CleanlinessScore = rating.CleanlinessScore;
            rating.EnvironmentScore = rating.EnvironmentScore;
            //save changes to db
            if(_context.SaveChanges() == 1)
            {
                return Ok("The restaurant has been successfully updated");

            }
            return BadRequest(ModelState);
        }


        //DELETE (delete)
        //api/Rating/{id}
        [HttpDelete]
        public IHttpActionResult DeleteRating([FromUri] int id)
        {
            Rating rating = _context.Ratings.Find(id);
            if (rating is null)
            {
                return NotFound();
            }

            _context.Ratings.Remove(rating);
            if (_context.SaveChanges() == 1)
            {
                return Ok("The Rating was deleted");
            }

            return InternalServerError();
        }
    }
}
