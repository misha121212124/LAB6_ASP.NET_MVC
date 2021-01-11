using lab6.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace lab6.Repositories
{
    class CityRepository : MainRepository<Cities>
    {
        public override List<Cities> GetAll()
        {
            return GetContext.Cities.ToList();
        }

        public override Cities GetById(int id)
        {
            return GetContext.Cities.Find(id);
        }

        public override void Insert(Cities item)
        {

            var results = new List<ValidationResult>();
            var context_isValid = new ValidationContext(item);

            if (!Validator.TryValidateObject(item, context_isValid, results, true))
            {
                foreach (var error in results)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            else
            {
                GetContext.Cities.Add(item);
                this.SaveChanges();
                Console.WriteLine("new city is valid!");

            }
        }

        public override void Remove(Cities item)
        {
            GetContext.Cities.Remove(item);
            this.SaveChanges();
        }

        public override void Update(Cities item)
        {
            GetContext.Entry(item).State = EntityState.Modified;
            this.SaveChanges();
        }
    }
}
