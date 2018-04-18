using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cms.BusinessLayer.BlBoleto
{
    public class Documentkind : AbstractDocumentkind,IDocumentkind
    {
         #region Variables

        private IDocumentkind _IDocumentkind;

        #endregion

        #region Constructors

        internal Documentkind()
        {
        }

        public Documentkind(int BankCode)
        {
            try
            {
                InstanciatDocumentKind(BankCode, 0);
            }
            catch (Exception ex)
            {
                throw new Exception("Error instantiating the object.", ex);
            }
        }

        public Documentkind(int BankCode, int Documentcode)
        {
            try
            {
                InstanciatDocumentKind(BankCode, Documentcode);
            }
            catch (Exception ex)
            {
                throw new Exception("Error instantiating the object.", ex);
            }
        }

        #endregion

        #region Interface Properties

        public override IBank Bank
        {
            get { return _IDocumentkind.Bank; }
            set { _IDocumentkind.Bank= value; }
        }

        public override int Code
        {
            get { return _IDocumentkind.Code; }
            set { _IDocumentkind.Code = value; }
        }

        public override string Sigla
        {
            get { return _IDocumentkind.Sigla; }
            set { _IDocumentkind.Sigla = value; }
        }

        public override string Especie
        {
            get { return _IDocumentkind.Especie; }
            set { _IDocumentkind.Especie = value; }
        }

        #endregion

        # region Private Methods


        private void InstanciatDocumentKind(int Bankcode, int Documentcode)
        {
            try
            {
                switch (Bankcode)
                {
                    //341 - Itaú
                    case 341:
                        _IDocumentkind = new DocumentKind_Itau(Documentcode);
                        break;
                    //356 - BankBoston
                    case 479:
                       // _IEspecieDocumento = new EspecieDocumento_BankBoston(codigoEspecie);
                        break;
                    //422 - Safra
                    case 1:
                        //_IEspecieDocumento = new EspecieDocumento_BancoBrasil(codigoEspecie);
                        break;
                    //237 - Bradesco
                    case 237:
                       // _IEspecieDocumento = new EspecieDocumento_Bradesco(codigoEspecie);
                        break;
                    case 356:
                      //  _IEspecieDocumento = new EspecieDocumento_Real(codigoEspecie);
                        break;
                    case 33:
                       // _IEspecieDocumento = new EspecieDocumento_Santander(codigoEspecie);
                        break;
                    case 347:
                       // _IEspecieDocumento = new EspecieDocumento_Sudameris(codigoEspecie);
                        break;
                    case 104:
                       // _IEspecieDocumento = new EspecieDocumento_Caixa(codigoEspecie);
                        break;
                    default:
                        throw new Exception("Error during execution of the transaction. " + Bankcode);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error during execution of the transaction.", ex);
            }
        }

                //public static EspeciesDocumento CarregaTodas(int codigoBanco)
                        //{
                        //    try
                        //    {

                        //        switch (codigoBanco)
                        //        {
                        //            case 1:
                        //                return EspecieDocumento_BancoBrasil.CarregaTodas();
                        //            case 237:
                        //                return EspecieDocumento_Bradesco.CarregaTodas();
                        //            case 341:
                        //                return EspecieDocumento_Itau.CarregaTodas();
                        //            case 356:
                        //                return EspecieDocumento_Itau.CarregaTodas();
                        //            case 104:
                        //                return EspecieDocumento_Caixa.CarregaTodas();
                        //            default:
                        //                throw new Exception("Espécies do Documento não implementado para o banco : " + codigoBanco);
                        //        }
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        throw new Exception("Erro ao listar objetos", ex);
                        //    }
                        //}

        #endregion



        public static string ValidateSigla(IDocumentkind document)
        {
            try
            {
                return document.Sigla;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static int ValidateCode(IDocumentkind document)
        {
            try
            {
                return document.Code;
            }
            catch
            {
                return 0;
            }
        }
    }

}
