using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Paschoalotto.Models.Database;

namespace Paschoalotto.Models
{
    [Table("contrato")]
    public class Contrato
    {
        [Key]
        [Column("id_contrato")]
        public Guid IdContrato { get; set; } = Guid.NewGuid();

        [Column("valor")]
        public double Valor { get; set; }

        [JsonIgnore]
        [Column("data_vencimento")]
        public DateTime Data_vencimento { get; set; }

         [Column("juros_composto")]
        public bool JurosComposto { get; set; }

        [Column("juros_simples")]
        public bool juros_simples { get; set; }

        [Column("divida_finalizada")]

        public bool DividaFinalizada { get; set; } = false;

        public HashSet<Boleto> Boletos {get; set;} 


        [Column("nome")]
        public string Nome { get; set; }

        [Column("documento")]

        public string Documento { get; set; }

        [Column("telefone")]
        public string Telefone { get; set; }

        [Column("data_nascimento")]
        public string DataNascimento { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("cep")]
        public string CEP { get; set; }

        [Column("numero")]
        public string Numero { get; set; }

        [Column("logradouro")]
        public string Logradouro { get; set; }

        [Column("complemento")]
        public string Complemento { get; set; }

        [Column("bairro")]
        public string Bairro { get; set; }

        [Column("cidade")]
        public string Cidade { get; set; }

        [Column("uf")]
        public string UF { get; set; }

    }
}