using lab6.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace lab6.Repositories
{
    class CinemaRepository : MainRepository<Cinemas>
    {
        public override List<Cinemas> GetAll()
        {
            return GetContext.Cinemas.ToList();
        }

        public override Cinemas GetById(int id)
        {
            return GetContext.Cinemas.Find(id);
        }

        public override void Insert(Cinemas item)
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
                GetContext.Cinemas.Add(item);
                this.SaveChanges();
                Console.WriteLine("new country is valid!");

            }
        }

        public override void Remove(Cinemas item)
        {
            GetContext.Cinemas.Remove(item);
            this.SaveChanges();
        }

        public override void Update(Cinemas item)
        {
            GetContext.Entry(item).State = EntityState.Modified;
            this.SaveChanges();
        }
    }
}
