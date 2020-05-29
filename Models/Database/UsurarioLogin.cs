using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Paschoalotto.Models.Database
{
    [Table("usuariologin")]
    public class UsuarioLogin : Login
    {
        [Column("id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("nome")]

        public string Nome { get; set; }

        [Required(ErrorMessage = "CPF ou CNPJ é necessario!")]
        [Column("documento")]

        public string Documento { get; set; }


        [Required(ErrorMessage = "Email é necessario!")]
        [Column("email")]

        public string Email { get; set; }


    }
}