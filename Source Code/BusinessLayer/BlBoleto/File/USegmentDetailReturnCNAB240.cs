using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.BusinessLayer.BlBoleto
{
    public class USegmentDetailReturnCNAB240
    {

        #region Variables

        double _InterestPenaltyCharges;
        double _ValueDiscountGranted;
        double _ValueAbatementGranted;
        double _IOFCollapsedvalue;
        double _Amountpaidbythedrawee ;
        double _Netvaluetobecredited ;
        double _ValueOtherExpenses ;
        double _OtherCreditsvalue;
        DateTime _Occurrencedate ;
        DateTime _Creditdate ;
        string _DraweeOccurrencecode;
        DateTime _OccurrencedateDrawee;
        double _ValueOccurrenceDrawee;
        string _record;

        private List<USegmentDetailReturnCNAB240> _ListDetail = new List<USegmentDetailReturnCNAB240>();

        #endregion

        #region Construtores

        public USegmentDetailReturnCNAB240(string record)
		{
            _record = record;
        }

        public USegmentDetailReturnCNAB240()
        {
        }

        #endregion

        #region Propriedades

        public double InterestPenaltyCharges
        {
            get { return _InterestPenaltyCharges; }
            set { _InterestPenaltyCharges = value; }
        }

        public double ValueDiscountGranted
        {
            get { return _ValueDiscountGranted; }
            set { _ValueDiscountGranted = value; }
        }

        public double ValueAbatementGranted
        {
            get { return _ValueAbatementGranted; }
            set { _ValueAbatementGranted = value; }
        }

        public double IOFCollapsedvalue
        {
            get { return _IOFCollapsedvalue; }
            set { _IOFCollapsedvalue = value; }
        }

        public double Amountpaidbythedrawee
        {
            get { return _Amountpaidbythedrawee ; }
            set { _Amountpaidbythedrawee  = value; }
        }

        public double Netvaluetobecredited 
        {
            get { return _Netvaluetobecredited ; }
            set { _Netvaluetobecredited  = value; }
        }

        public double ValueOtherExpenses 
        {
            get { return _ValueOtherExpenses ; }
            set { _ValueOtherExpenses  = value; }
        }

        public double OtherCreditsvalue
        {
            get { return _OtherCreditsvalue; }
            set { _OtherCreditsvalue = value; }
        }

        public DateTime Occurrencedate
        {
            get { return _Occurrencedate ; }
            set { _Occurrencedate  = value; }
        }

        public DateTime Creditdate 
        {
            get { return _Creditdate ; }
            set { _Creditdate  = value; }
        }

        public string DraweeOccurrencecode
        {
            get { return _DraweeOccurrencecode; }
            set { _DraweeOccurrencecode = value; }
        }

        public DateTime OccurrencedateDrawee
        {
            get { return _OccurrencedateDrawee; }
            set { _OccurrencedateDrawee = value; }
        }

        public double ValueOccurrenceDrawee

        {
            get { return _ValueOccurrenceDrawee; }
            set { _ValueOccurrenceDrawee = value; }
        }

        public List<USegmentDetailReturnCNAB240> ListDetail
        {
            get { return _ListDetail; }
            set { _ListDetail = value; }
        }

        public string Record
        {
            get { return _record; }
        }

        #endregion
    }
}
