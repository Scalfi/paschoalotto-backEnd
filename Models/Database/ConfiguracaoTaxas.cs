using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Paschoalotto.Models.Database
{
    [Table("configuracaotaxas")]
    public class ConfiguracaoTaxas
    {
        [Column("id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("porcentagem_comissao")]
        public double  PorcentagemComissao { get; set; }

        [Column("qtd_maxima_parcelas")]
        public int  QtdParcelas { get; set; }

        [Column("juros_porcetagem")]
        public double  JurosPorcetagem { get; set; }

        [Column("juros_simples")]
        public bool  JurosSimple { get; set; }
        
        [Column("juros_composto")]
        public bool  JurosComposto { get; set; }
    }
}