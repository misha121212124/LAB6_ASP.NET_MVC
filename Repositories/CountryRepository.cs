using lab6.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace lab6.Repositories
{
    class CountryRepository : MainRepository<Countries>
    {


        public override void Insert(Countries item)
        {
            var results = new List<ValidationResult>();
            var context_isValid = new ValidationContext(item);

            if (!Validator.TryValidateObject(item, context_isValid, results, true)){
                foreach (var error in results){
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            else{
                GetContext.Countries.Add(item);
                this.SaveChanges();
                Console.WriteLine("new country is valid!");

            }

        }

        public override List<Countries> GetAll()
        {
            return GetContext.Countries.ToList();
        }

        public override Countries GetById(int id)
        {
            return GetContext.Countries.Find(id);
        }

        public List<Countries> GetByName(String Name)
        {
            return GetContext.Countries.Where(m => m.Name.Equals(Name)).ToList();
        }

        public List<Countries> GetByCity(Cities city)
        {
            return GetContext.Countries.Where(m => m.Cities.Contains(city)).ToList();
        }


        public override void Remove(Countries item)
        {
            GetContext.Countries.Remove(item);
            this.SaveChanges();

        }

        public override void Update(Countries item)
        {
            GetContext.Entry(item).State = EntityState.Modified;
            this.SaveChanges();
        }
    }
}
