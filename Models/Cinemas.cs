using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace lab6.Models
{
    public class Cinemas : SuperEntity, IValidatableObject
    {
/*        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }*/

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public int Q_Income { get; set; }

        [Required]
        public virtual Cities City { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (this.Name == string.Empty)
                errors.Add(new ValidationResult("Name is required!"));

            if (this.Address == string.Empty)
                errors.Add(new ValidationResult("Area is required!"));

            if (this.Q_Income == 0)
                errors.Add(new ValidationResult("Student_Count is required!"));


            if (this.City == null)
                errors.Add(new ValidationResult("Country is required!"));

            return errors;
        }
    }
}
