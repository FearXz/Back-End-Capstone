﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_End_Capstone.Models
{
    public class IngredientiRistorante
    {
        // DB COLUMN
        [Key]
        public int IdIngrediente { get; set; }

        [Required]
        [ForeignKey("Ristorante")]
        public int IdRistorante { get; set; }

        [Required]
        public string NomeIngrediente { get; set; }

        [Required]
        public double PrezzoIngrediente { get; set; }

        // NOT MAPPED //NULLABLE

        // NAVIGATION PROPERTY
        public virtual Ristorante Ristorante { get; set; }
    }
}