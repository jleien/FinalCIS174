using System;
using System.ComponentModel.DataAnnotations;

namespace FinalCIS174.Models
{
    public class Player
    {
        public string PlayerID { get; set; }
        [Required(ErrorMessage = "Please enter a name.")]
        [RegularExpression("^[a-zA-Z0-9]+$",
    ErrorMessage = "The name cannot have special characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a number.")]
        [Range(1, 20, ErrorMessage = "please enter a number between 1 and 20")]
        public int Level { get; set; }

        public string ClassID { get; set; }

        [Required(ErrorMessage = "Please select a Class.")]
        public Class? Class { get; set; }

        public string RaceID { get; set; }

        [Required(ErrorMessage = "Please select a race.")]

        public Race? Race { get; set; }
    }
}
