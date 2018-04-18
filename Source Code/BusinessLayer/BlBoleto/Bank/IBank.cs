using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.BusinessLayer.BlBoleto
{
    public interface IBank
    {
        /// <summary>
        /// Formata o código de barras
        /// </summary>
        void BarCodeFormats(Boleto boleto);
       
        /// <summary>
        /// Formata a linha digital
        /// </summary>
        void FormatLineDigitavel(Boleto boleto);

        void OurNumberFormats(Boleto boleto);
        /// <summary>
        /// Formata o número do documento, alguns bancos exige uma formatação. Tipo: 123-4
        /// </summary>
        void DocumentNumberFormats(Boleto boleto);
        /// <summary>
        /// Responsável pela validação de todos os dados referente ao banco, que serão usados no boleto
        /// </summary>

        /// <summary>
        /// Gera o header do arquivo de remessa
        /// </summary>
       // string GerarHeaderRemessa(string numeroConvenio, Assginor cendente, Filetype filetype);

        void ValidateBoleto(Boleto boleto);
        /// <summary>
        /// Gera o header do arquivo de remessa
        /// </summary>
        /// 
        //string GenerateShipmentHeader(string numeroConvenio, Assginor assginor, Filetype filetype);
        /// <summary>
        /// Gera os registros de detalhe do arquivo de remessa
        /// </summary>
        //$%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        /// <summary>
     
        
        ///// Formata o código de barras
        ///// </summary>
        //void FormataCodigoBarra(Boleto boleto);
        ///// <summary>
        ///// Formata a linha digital
        ///// </summary>
        //void FormataLinhaDigitavel(Boleto boleto);
        ///// <summary>
        ///// Formata o nosso número
        ///// </summary>
        //void FormataNossoNumero(Boleto boleto);
        ///// <summary>
        ///// Formata o número do documento, alguns bancos exige uma formatação. Tipo: 123-4
        ///// </summary>
        //void FormataNumeroDocumento(Boleto boleto);
        ///// <summary>
        ///// Responsável pela validação de todos os dados referente ao banco, que serão usados no boleto
        ///// </summary>
        ///// 

        
        //void ValidaBoleto(Boleto boleto);

        ///// <summary>
        ///// Gera o header do arquivo de remessa
        ///// </summary>


        //string GerarHeaderRemessa(string numeroConvenio, Assginor cendente, Filetype filetype);
       
        /// <summary>
        /// Gera os registros de detalhe do arquivo de remessa
        /// </summary>
        
        
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //string GerarDetalheRemessa(Boleto boleto, int numeroRegistro, Filetype filetype);
        ///// <summary>
        ///// Gera o header de arquivo do arquivo de remessa
        ///// </summary>
        //string GerarHeaderRemessa(Assginor cendente, Filetype filetype);
        ///// <summary>
        ///// Gera o Trailer do arquivo de remessa
        ///// </summary>
        //string GerarTrailerRemessa(int numeroRegistro, Filetype filetype);
        ///// <summary>
        ///// Gera o header de lote do arquivo de remessa
        ///// </summary>
        //string GerarHeaderLoteRemessa(string numeroConvenio, Assginor cendente, int numeroArquivoRemessa);
        ///// <summary>
        ///// Gera o header de lote do arquivo de remessa
        ///// </summary>
        //string GerarHeaderLoteRemessa(string numeroConvenio, Assginor cendente, int numeroArquivoRemessa, Filetype filetype);
        ///// <summary>
        ///// Gera os registros de detalhe do arquivo de remessa - SEGMENTO P
        ///// </summary>
        //string GerarDetalheSegmentoPRemessa(Boleto boleto, int numeroRegistro, string numeroConvenio);
        ///// <summary>
        ///// Gera os registros de detalhe do arquivo de remessa - SEGMENTO P
        ///// </summary>
        //string GerarDetalheSegmentoPRemessa(Boleto boleto, int numeroRegistro, string numeroConvenio, Assginor cedente);
        ///// <summary>
        ///// Gera os registros de detalhe do arquivo de remessa - SEGMENTO Q
        ///// </summary>
        //string GerarDetalheSegmentoQRemessa(Boleto boleto, int numeroRegistro, Filetype filetype);
        ///// <summary>
        ///// Gera os registros de detalhe do arquivo de remessa - SEGMENTO R
        ///// </summary>
        //string GerarDetalheSegmentoRRemessa(Boleto boleto, int numeroRegistro, Filetype filetype);
        ///// <summary>
        ///// Gera o Trailer de arquivo do arquivo de remessa
        ///// </summary>
        //string GerarTrailerArquivoRemessa(int numeroRegistro);
        ///// <summary>
        ///// Gera o Trailer de lote do arquivo de remessa
        ///// </summary>
        ///// 

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


        //string GerarTrailerLoteRemessa(int numeroRegistro);

        //TSegmentDetailReturnCNAB240 LerDetalheSegmentoTRetornoCNAB240(string record);

        //USegmentDetailReturnCNAB240 LerDetalheSegmentoURetornoCNAB240(string record);

        //ReturnDetails LerDetalheRetornoCNAB400(string registro);
        //$%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%




        Assginor Assginor { get; }
        int Code { get; set; }
        string Name { get; }
        int Digit { get; }
    }
}
