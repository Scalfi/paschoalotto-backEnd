using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using MariGlobals.Utils;
using Microsoft.EntityFrameworkCore;
using Paschoalotto.Models.Database;
using Paschoalotto.Services;

namespace Paschoalotto.Repositories
{
    public class BoletoRepository
    {
        private readonly DatabaseContext db;
        private readonly ContratoRepository _contratoRepository;

        private readonly ConfiguracaoTaxasRepository _configuracaoTaxasRepository;

        public BoletoRepository(DatabaseContext context, ContratoRepository contratoRepository, ConfiguracaoTaxasRepository configuracaoTaxasRepository)
        {
            db = context;
            _contratoRepository = contratoRepository;
            _configuracaoTaxasRepository = configuracaoTaxasRepository;
        }

        public async Task<List<Boleto>> GeracaoBoletoAsync(List<Boleto> boletos)
        {
            Guid idContrato = Guid.Empty;

            foreach (var boleto in boletos)
            {
                await db.AddAsync(boleto);
                idContrato = boleto.IdContrato;
            }

            await CancelaBoletosAntigosAsync(idContrato);


            await db.SaveChangesAsync();
            return boletos;
        }

        public async Task<List<Boleto>> SimulaGeracaoBoletoAsync(GeraBoleto geraBoleto)
        {
            return await BuildBoleotoAsync(geraBoleto);

        }

        public async Task CancelaBoletosAntigosAsync(Guid IdContrato)
        {
            var boletosAntigos = await db.Boletos
                    .Where(b => b.IdContrato.Equals(IdContrato) && !b.Pago)
                    .ToListAsync();
            
            if (boletosAntigos.HasContent())
            {

                foreach (var boleto in boletosAntigos)
                {
                    boleto.Cancelado = true;
                    await db.AddAsync(boleto);
                }

                await db.SaveChangesAsync();

            }


        }

        public async Task<Dictionary<dynamic, dynamic>> PegaBoletosAsync(string documento)
        {
            var result = from boleto in db.Boletos
                         join contrato in db.Contrato on boleto.IdContrato equals contrato.IdContrato into Details
                         from m in Details.DefaultIfEmpty()
                         orderby boleto.Id
                         where !boleto.Pago && !boleto.Cancelado && boleto.Contrato.Documento.Equals(documento)
                         select new
                         {
                             IdContrato = boleto.IdContrato,
                             IdBoleto = boleto.IdBoleto,
                             ValorParcela = boleto.ValorParcela,
                             ValorJuros = boleto.ValorJuros,
                             ValorFinal = boleto.ValorFinal,
                             ValorOriginal = boleto.ValorOriginal,
                             ParcelaAtual = boleto.ParcelaAtual,
                             DataVencimento = boleto.DataVencimento,
                             QtdParcela = boleto.QtdParcela,
                             DataVencimentoContrato = boleto.Contrato.Data_vencimento,
                             DiasAtraso = boleto.DiasAtraso

                         };


            var objs = MontaResponse(await result.ToListAsync());

            return objs;
        }

        public async Task<List<Boleto>> BuildBoleotoAsync(GeraBoleto geraBoleto)
        {
            ConfiguracaoTaxas config = null;
            // Valor Total Com Juros
            double valorTotal;

            var boletos = new List<Boleto>();

            var contrato = await _contratoRepository.PegaContratoAsync(geraBoleto.IdContrato);


            if (contrato.juros_simples)
            {
                config = await _configuracaoTaxasRepository.GetConfigSimpleAsync();
                valorTotal = CalculaJurosService.CalculaJurosSimples(config, contrato, geraBoleto.DataPrimeiroPagamento);
            }
            else
            {
                config = await _configuracaoTaxasRepository.GetConfigCompostoAsync();
                valorTotal = CalculaJurosService.CalculaJurosComposto(config, contrato, geraBoleto.DataPrimeiroPagamento);

            }


            for (int i = 1; i <= geraBoleto.QtdParcelas; i++)
            {
                var boleto = new Boleto();
                boleto.DiasAtraso = (geraBoleto.DataPrimeiroPagamento.Date - contrato.Data_vencimento.Date).Days;
                boleto.IdContrato = contrato.IdContrato;
                boleto.QtdParcela = geraBoleto.QtdParcelas;
                boleto.ValorOriginal = contrato.Valor;
                boleto.ValorJuros = valorTotal - contrato.Valor;
                boleto.ValorFinal = valorTotal;
                boleto.LucroPaschoalotto = valorTotal * (config.PorcentagemComissao / 100);
                boleto.ValorParcela = valorTotal / geraBoleto.QtdParcelas;
                boleto.ParcelaAtual = $"{i}/{geraBoleto.QtdParcelas}";
                boleto.DataVencimento = geraBoleto.DataPrimeiroPagamento.AddMonths(i - 1);

                boletos.Add(boleto);
            }

            return boletos;

        }


        public async Task<Boleto> PagaBoletosAsync(Boleto boleto)
        {

            var boletoExiste = await db.Boletos
                    .Where(b => b.IdContrato.Equals(boleto.IdContrato) && !b.Pago && !b.Cancelado)
                    .FirstOrDefaultAsync();

            if (boletoExiste != null)
            {
                boletoExiste.Pago = true;

                await db.SaveChangesAsync();

                var boletosPagos = await db.Boletos
                    .Where(b => b.IdContrato.Equals(boletoExiste.IdContrato) && b.Pago)
                    .ToListAsync();

                if (boletosPagos.Count == boletoExiste.QtdParcela)
                    await _contratoRepository.FinalizaContratoAsync(boletoExiste.IdContrato);

                return boletoExiste;
            }

            return null;


        }

        private Dictionary<dynamic, dynamic> MontaResponse(dynamic objs)
        {

            var newList = new Dictionary<dynamic, dynamic>();

            foreach (var obj in objs)
            {

                if (!newList.ContainsKey(obj.IdContrato))
                {
                    newList[obj.IdContrato] = new
                    {
                        IdContrato = obj.IdContrato.ToString(),
                        ValorJuros = obj.ValorJuros,
                        ValorFinal = obj.ValorFinal,
                        ValorOriginal = obj.ValorOriginal,
                        DataVencimentoContrato = obj.DataVencimentoContrato.Date.ToString("dd/M/yyyy", CultureInfo.InvariantCulture),
                        DiasAtraso = obj.DiasAtraso,
                        QtdParcela = obj.QtdParcela,
                        Boletos = new List<dynamic>(),

                    };
                }
                newList[obj.IdContrato].Boletos.Add(new
                {
                    IdBoleto = obj.IdBoleto,
                    DataVencimento = obj.DataVencimento.Date.ToString("dd/M/yyyy", CultureInfo.InvariantCulture),
                    ValorParcela = obj.ValorParcela,
                    ParcelaAtual = obj.ParcelaAtual,

                });

            }

            return newList;

        }
    }
}