
/******************************************************************************************
<Author				: - Pradeep Kushwaha
<Start Date			: -	25-03-2010
<End Date			: -	
<Description		: - 
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date		: - 
<Modified By		: - 
<Purpose			: - 
*******************************************************************************************/

using System;
using System.Text;
using System.Data;
using System.Collections;
namespace Cms.Model.Support
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEbixModel
    {
        /// <summary>
        /// 
        /// </summary>
        String Proc_FetchData { get; set; }
        String Proc_Add_Name { get; set; }
        String Proc_Update_Name{ get; set; }
        String Proc_Delete_Name { get; set; }
        String Proc_ActivateDeactivate_Name { get; set; }
        Hashtable htPropertyCollection { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        DataSet GetData();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //int Save();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //int Update();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //int Delete();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //int ActivateDeactivate();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        int MaintainTrans();

    }
}
