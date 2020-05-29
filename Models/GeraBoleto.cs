using System;
using System.ComponentModel.DataAnnotations;

namespace Paschoalotto.Models.Database
{
    public class GeraBoleto
    {
        [Required(ErrorMessage ="Id do contrato é necessario!")]
        public Guid IdContrato { get; set; }

        [Required(ErrorMessage ="DataPrimeiroPagamento é necessario!")]
        public DateTime DataPrimeiroPagamento {get; set;}
        
        [Required(ErrorMessage ="QtdParcelas é necessario!")]
        public int QtdParcelas {get; set;}

    }
}