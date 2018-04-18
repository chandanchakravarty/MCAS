using System;
using System.Collections;
using System.Text;

namespace Cms.BusinessLayer.BlBoleto
{
    public class Bank : AbstractBank, IBank
    {

        #region Variaveis

        private IBank _IBank;

        #endregion Variaveis

        #region Construtores

        internal Bank() 
        { 
        }

        public Bank(int bankcode)
        {
            try
            {
                InstanciateBank(bankcode);
            }
            catch (Exception ex)
            {
                throw new Exception("Error instantiating object.", ex);
            }
        }

        #endregion

        #region Interface Properties

        public override int Code
        {
            get { return _IBank.Code; }
            set { _IBank.Code = value; }
        }

        public override int Digit
        {
            get { return _IBank.Digit; }
        }

        public override string Name
        {
            get { return _IBank.Name; }
        }

        #endregion

        # region Interface Methods

        private void InstanciateBank(int bankcode)
        {
            try
            {
                switch (bankcode)
                {
                    //104 - Caixa
                    case 104:
                        //_IBank = new Banco_Caixa();
                        break;
                    //341 - Itaú
                    case 341:
                        _IBank = new Bank_Itau();
                        break;
                    //356 - Real
                    case 275:
                    case 356:
                        //_IBank = new Banco_Real();
                        break;
                    //422 - Safra
                    case 422:
                        //_IBank = new Banco_Safra();
                        break;
                    //237 - Bradesco
                    case 237:
                      //  _IBank = new Banco_Bradesco();
                        break;
                    //347 - Sudameris
                    case 347:
                     _IBank = new Bank_Sudameris();
                        break;
                    //353 - Santander
                    case 353:
                        _IBank = new Bank_Santander();
                        break;
                    //070 - BRB
                    case 70:
                       // _IBank = new Banco_BRB();
                        break;
                    //479 - BankBoston
                    case 479:
                        //_IBank = new Banco_BankBoston();
                        break;
                    //001 - Banco do Brasil
                    case 1:
                        _IBank = new Bank_Brazil();
                        break;
                    //399 - HSBC
                    case 399:
                        _IBank = new Bank_HSBC();
                        break;
                    //003 - HSBC
                    case 3:
                        //_IBank = new Banco_Basa();
                        break;
                    //409 - Unibanco
                    case 409:
                       // _IBank = new Banco_Unibanco();
                        break;
                    //33 - Unibanco
                    case 33:
                        _IBank = new Bank_Santander();
                        break;
                    //41 - Banrisul
                    case 41:
                       // _IBank = new Banco_Banrisul();
                        break;
                    default:
                        throw new Exception("Bank Code not implementing: " + bankcode);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while running transaction.", ex);
            }
        }

        # endregion

        # region Interface Methods

        public override void BarCodeFormats(Boleto boleto)
        {
            try
            {
                _IBank.BarCodeFormats(boleto);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while formatting bar code.", ex);
            }
        }

        public override void FormatLineDigitavel(Boleto boleto)
        {
            try
            {
                _IBank.FormatLineDigitavel(boleto);
            }
            catch (Exception ex)
            {
                throw new Exception("Error when formatting the line digitável.", ex);
            }
        }

        public override void OurNumberFormats(Boleto boleto)
        {
            try
            {
                _IBank.OurNumberFormats(boleto);
            }
            catch (Exception ex)
            {
                throw new Exception("Error during the formatting of our number.", ex);
            }
        }

        public override void DocumentNumberFormats(Boleto boleto)
        {
            try
            {
                _IBank.DocumentNumberFormats(boleto);
            }
            catch (Exception ex)
            {
                throw new Exception("Error during the formatting of Document Number Formats.", ex);
            }
        }

        public override void ValidateBoleto(Boleto boleto)
        {
            try
            {
                _IBank.ValidateBoleto(boleto);
            }
            catch (Exception ex)
            {
                throw new Exception("Error during the validation of bank.", ex);
            }
        }

        # endregion

        # region Methods for generating file

        //public override string GenerateShipmentHeader(string numeroConvenio, Assginor assginor, Filetype filetype)
        //{
        //    try
        //    {
        //        return _IBank.GenerateShipmentHeader(numeroConvenio, assginor, filetype);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error during generation of the HEADER record file SHIPMENT.", ex);
        //    }
        //}

        //public override string GerarDetalheRemessa(Boleto boleto, int numeroRegistro, Filetype tipoArquivo)
        //{
        //    try
        //    {
        //        return _IBank.GerarDetalheRemessa(boleto, numeroRegistro, tipoArquivo);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error during generation of records SHIPMENT DETAIL file.", ex);
        //    }
        //}

        //public override string GerarTrailerRemessa(int numeroRegistro, Filetype tipoArquivo)
        //{
        //    try
        //    {
        //        return _IBank.GerarTrailerRemessa(numeroRegistro, tipoArquivo);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error during generation of the register file TRAILER SHIPMENT.", ex);
        //    }
        //}

        //public override string GerarHeaderRemessa(Assginor assginor, Filetype tipoArquivo)
        //{
        //    try
        //    {
        //        return _IBank.GerarHeaderRemessa(assginor, tipoArquivo);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error during generation of the HEADER record file SHIPMENT.", ex);
        //    }
        //}

        //public override string GerarHeaderLoteRemessa(string numeroConvenio, Assginor assginor, int numeroArquivoRemessa)
        //{
        //    try
        //    {
        //        return _IBank.GerarHeaderLoteRemessa(numeroConvenio, assginor, numeroArquivoRemessa);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error during generation of the HEADER record file SHIPMENT.", ex);
        //    }
        //}

        //public override string GerarHeaderLoteRemessa(string numeroConvenio, Assginor assginor, int numeroArquivoRemessa, Filetype tipoArquivo)
        //{
        //    try
        //    {
        //        return _IBank.GerarHeaderLoteRemessa(numeroConvenio, assginor, numeroArquivoRemessa, tipoArquivo);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error during generation of the HEADER record file SHIPMENT.", ex);
        //    }
        //}

        //public override string GerarDetalheSegmentoPRemessa(Boleto boleto, int numeroRegistro, string numeroConvenio)
        //{
        //    try
        //    {
        //        return _IBank.GerarDetalheSegmentoPRemessa(boleto, numeroRegistro, numeroConvenio);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error during generation of records SHIPMENT DETAIL file", ex);
        //    }
        //}

        //public override string GerarDetalheSegmentoPRemessa(Boleto boleto, int numeroRegistro, string numeroConvenio, Assginor assginor)
        //{
        //    try
        //    {
        //        return _IBank.GerarDetalheSegmentoPRemessa(boleto, numeroRegistro, numeroConvenio, assginor);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error during generation of records SHIPMENT DETAIL file", ex);
        //    }
        //}

        //public override string GerarDetalheSegmentoQRemessa(Boleto boleto, int numeroRegistro, Filetype tipoArquivo)
        //{
        //    try
        //    {
        //        return _IBank.GerarDetalheSegmentoQRemessa(boleto, numeroRegistro, tipoArquivo);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error during generation of records SHIPMENT DETAIL file", ex);
        //    }
        //}

        //public override string GerarDetalheSegmentoRRemessa(Boleto boleto, int numeroRegistro, Filetype tipoArquivo)
        //{
        //    try
        //    {
        //        return _IBank.GerarDetalheSegmentoRRemessa(boleto, numeroRegistro, tipoArquivo);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error during generation of records SHIPMENT DETAIL file.", ex);
        //    }
        //}

        //public override string GerarTrailerArquivoRemessa(int numeroRegistro)
        //{
        //    try
        //    {
        //        return _IBank.GerarTrailerArquivoRemessa(numeroRegistro);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error during generation of the register file TRAILER SHIPMENT.", ex);
        //    }
        //}

      //public override string GerarTrailerLoteRemessa(int numeroRegistro)
      //  {
      //      try
      //      {
      //          return _IBank.GerarTrailerLoteRemessa(numeroRegistro);
      //      }
      //      catch (Exception ex)
      //      {
      //          throw new Exception("Error during generation of the register file TRAILER SHIPMENT.", ex);
      //      }
      //  }

        # endregion


        //#Methods region Reading file Return

        //public override TSegmentDetailReturnCNAB240 LerDetalheSegmentoTRetornoCNAB240(string record)
        //{
        //    return _IBank.LerDetalheSegmentoTRetornoCNAB240(record);
        //}

        //public override USegmentDetailReturnCNAB240 LerDetalheSegmentoURetornoCNAB240(string record)
        //{
        //    return _IBank.LerDetalheSegmentoURetornoCNAB240(record);
        //}

        //public override ReturnDetails LerDetalheRetornoCNAB400(string record)
        //{
        //    return _IBank.LerDetalheRetornoCNAB400(record);
        //}

        //#endregion Métodos de Leitura do arquivo de Retorno
    }
}
