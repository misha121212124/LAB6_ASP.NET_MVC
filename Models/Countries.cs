using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace lab6.Models
{
    public class Countries : SuperEntity, IValidatableObject
    {
/*        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }*/
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public int Population { get; set; }
        [Required]
        public int Border_Length { get; set; }
        [Required]
        public int Area { get; set; }
        public List<Cities> Cities { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (this.Name == string.Empty)
                errors.Add(new ValidationResult("Name is required!"));
            if (this.Code == string.Empty)
                errors.Add(new ValidationResult("Code is required!"));
            if (this.Population == 0)
                errors.Add(new ValidationResult("Population is required!"));
            if (this.Border_Length == 0)
                errors.Add(new ValidationResult("Border_Length is required!"));
            if (this.Area == 0)
                errors.Add(new ValidationResult("Area is required!"));
            return errors;
        }
    }
}