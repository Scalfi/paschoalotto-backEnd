
using System;
using Paschoalotto.Models;
using Paschoalotto.Models.Database;
using Paschoalotto.Repositories;

namespace Paschoalotto.Services
{
    public static class CalculaJurosService
    {
        
        public static double CalculaJurosSimples(ConfiguracaoTaxas config, Contrato contrato, DateTime DiaPagamento)
        {

            var J = contrato.Valor * (config.JurosPorcetagem / 100) * (DiaPagamento.Date - contrato.Data_vencimento.Date).Days;

            return J + contrato.Valor;
        }

        public static double CalculaJurosComposto(ConfiguracaoTaxas config, Contrato contrato, DateTime DiaPagamento)
        {
            return contrato.Valor * (Math.Pow((1 + (config.JurosPorcetagem / 100)), ( DiaPagamento.Date - contrato.Data_vencimento.Date).Days));

        }

    }
}