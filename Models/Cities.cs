using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace lab6.Models
{
    public class Cities : SuperEntity, IValidatableObject
    {
/*        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }*/
        [Required]
        public string Name { get; set; }
        [Required]
        public int Area { get; set; }
        [Required]
        public int Student_Count { get; set; }
        [Required]
        public string Info { get; set; }
        [Required]
        public virtual Countries Country { get; set; }
        public List<Cinemas> Cinemas { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (this.Name == string.Empty)
                errors.Add(new ValidationResult("Name is required!"));
            if (this.Area == 0)
                errors.Add(new ValidationResult("Area is required!"));
            if (this.Student_Count == 0)
                errors.Add(new ValidationResult("Student_Count is required!"));
            if (this.Info == string.Empty)
                errors.Add(new ValidationResult("Info is required!"));
            if (this.Country == null)
                errors.Add(new ValidationResult("Country is required!"));
            return errors;
        }
    }
}