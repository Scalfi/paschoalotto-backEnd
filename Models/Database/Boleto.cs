using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Paschoalotto.Models.Database
{
    [Table("boletos")]
    public class Boleto
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("id_boleto")]
        public Guid IdBoleto { get; set; } = Guid.NewGuid();

        [Column("id_contrato")]
        [ForeignKey(nameof(Contrato))]
        public Guid IdContrato { get; set; }

        [JsonIgnore]
        public Contrato Contrato {get; set;}

        [Required(ErrorMessage = "Valor Original é necessario!")]
        [Column("valor_original")]
        public double ValorOriginal { get; set; }

        [Required(ErrorMessage = "Lucro Paschoalotto é necessario!")]
        [Column("lucro_paschoalotto")]
        public double LucroPaschoalotto { get; set; }

        [Required(ErrorMessage = "Valor Juros é necessario!")]

        [Column("valor_juros")]
        public double ValorJuros { get; set; }

        [Required(ErrorMessage = "Valor Final é necessario!")]

        [Column("valor_final")]
        public double ValorFinal { get; set; }

        [Required(ErrorMessage = "Valor Parcela é necessaria!")]

        [Column("valor_parcela")]
        public double ValorParcela { get; set; }

        [Required(ErrorMessage = "Qauantindade  de Parcelas é necessaria!")]
        [Column("qtd_parcela")]
        public int QtdParcela { get; set; }

        [Required(ErrorMessage = "Parcela Atual é necessaria!")]

        [Column("parcela_atual")]
        public string ParcelaAtual { get; set; }

        [Required(ErrorMessage = "Data Vencimento é necessaria!")]

        [Column("data_vencimento")]
        public DateTime DataVencimento { get; set; }

        [Column("pago")]
        public bool Pago { get; set; } = false;


        [Column("cancelado")]
        public bool Cancelado { get; set; } = false;
        
        [Column("dias_atraso")]
        public int DiasAtraso { get; set; }

    }
}