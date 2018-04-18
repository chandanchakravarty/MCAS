using System;
using System.ComponentModel;
using System.Web.UI.Design.WebControls;
namespace Cms.BusinessLayer.BlBoleto
{
    internal class BilletBankingDesigner : System.Web.UI.Design.ControlDesigner
    {
        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
        }

        // Make this control resizeable on the design surface
        public override bool AllowResize
        {
            get
            {
                return true;
            }
        }

        public override string GetDesignTimeHtml()
        {
            return CreatePlaceHolderDesignTimeHtml("BlBoleto.BilletBanking");
        }


        protected override string GetErrorDesignTimeHtml(Exception e)
        {
            string pattern = "Boleto.NET while creating design time HTML:<br/>{0}";
            return String.Format(pattern, e.Message);
        }

    }
}
