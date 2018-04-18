using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCAS.Web.Objects.CommonHelper
{
   public class EntityModel
   {
       #region constructor

       #endregion

       #region Properties
       /// <summary>
        /// Created By user Id
        /// </summary>
        public Guid CreatedBy { get; set; }


        /// <summary>
        /// DateTime on which Record created
        /// </summary>
        public DateTime CreatedOn { get; set; }

        public Guid? ModifiedBy { get; set; }

        /// <summary>
        /// DateTime when record modified
        /// </summary>
        public DateTime? ModifiedOn { get; set; }
       #endregion
   }
}
