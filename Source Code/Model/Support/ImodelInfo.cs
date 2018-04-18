using System;
using System.Data;

/******************************************************************************************
<Author				: - Anshuman
<Start Date			: -	April 5, 2005
<End Date			: -	April 5, 2005
<Description		: - An interface which will be implemented in all Model class.
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date		: - 
<Modified By		: - 
<Purpose			: - 
*******************************************************************************************/

namespace Cms.Model
{
	/// <summary>
	/// This interface will be implemeted in all model class.
	/// </summary>
	public interface IModelInfo
	{
		/// <summary>
		/// datatable containing all the information of model object //Comment
		/// </summary>
		DataTable TableInfo
		{
			get;
			set;
		}

		/// <summary>
		/// An XML string having mapping information of label and field name of Model object
		/// </summary>
		string TransactLabel
		{
			get;
			set;
		}
	}
}
