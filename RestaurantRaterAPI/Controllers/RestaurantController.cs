using RestaurantRaterAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestaurantRaterAPI.Controllers
{
    public class RestaurantController : ApiController
    {
        private readonly RestaurantDbContext _context = new RestaurantDbContext();
        //POST (create method)
        //api/Restaurant
        [HttpPost]
        public IHttpActionResult PostRestaurant([FromBody] Restaurant model)
        {
            if (ModelState.IsValid)
            {
                _context.Restaurants.Add(model);
                _context.SaveChanges();
                //store the model in the db
                return Ok("your restaurant was created!");
            }
            //if not valid, reject the request
            return BadRequest(ModelState);
        }

        //GET ALL
        //api/Restaurant
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Restaurant> restaurants = _context.Restaurants.ToList();
            return Ok(restaurants);
        }

        //GET BY ID
        //api/Restaurant/{id}
        [HttpGet]
        public IHttpActionResult GetRestaurantById([FromUri] int id)
        {
            Restaurant restaurant = _context.Restaurants.Find(id);

            if(restaurant == null)
            {
                return NotFound();
            }
            return Ok(restaurant);
        }

        //PUT (update)
        //api/Restaurant/{id}
        [HttpPut]
        public IHttpActionResult UpdateRestaurant([FromUri] int id, [FromBody] Restaurant updateRestaurant)
        {
            //Check the Ids if they match
            if(id != updateRestaurant?.Id)
            {
                return BadRequest("Ids do not match");
            }
            //Check ModelState
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //Find the Restaurant in db
            Restaurant restaurant = _context.Restaurants.Find(id);
            //If no match/doesn't exist, do something
            if (restaurant is null)
                return NotFound();
            //Update properties
            restaurant.Name = updateRestaurant.Name;
            restaurant.Address = updateRestaurant.Address;
            restaurant.Rating = updateRestaurant.Rating;
            //save changes to db
            _context.SaveChanges();
            return Ok("The restaurant has been successfully updated");
        }

        //DELETE (delete)
        //api/Restaurant/{id}
        [HttpDelete]
        public IHttpActionResult DeleteRestaurant([FromUri] int id)
        {
            Restaurant res = _context.Restaurants.Find(id);
            if(res is null)
            {
                return NotFound();
            }

            _context.Restaurants.Remove(res);
            if(_context.SaveChanges() == 1)
            {
                return Ok("The Restaurant was deleted");
            }

            return InternalServerError();
        }
    }
}
