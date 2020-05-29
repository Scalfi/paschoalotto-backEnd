using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;
using Paschoalotto.Models.Database;

namespace Paschoalotto.Models
{
    public abstract class Login 
    {
        [Column("usuario")]

        [Required(ErrorMessage = "Usuario é necessario!")]
        public string Usuario { get; set; }
        
        [Column("senha")]

        [Required(ErrorMessage = "Senha é necessaria!")]
        public string Senha { get; set; }
        
        
        [Column("role")]
        public string Role { get; set; }
    }
}
