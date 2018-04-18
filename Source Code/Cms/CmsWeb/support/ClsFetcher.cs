/******************************************************************************************
	<Author					: - > Pradeep Iyer
	<Start Date				: -	> 24/03/2004
	<End Date				: - > 
	<Description			: - > This class acts as a wrapper for the clsSingleton class.
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

	<Modified Date			: Jan 5 ,2006
	<Modified By			: Nidhi
	<Purpose				: Added - Reinsurance Contract Type

*******************************************************************************************/

using System;
using System.Data;

namespace Cms.CmsWeb
{
	/// <summary>
	/// Summary description for ClsFetcher.
	/// </summary>
	public class ClsFetcher
	{
		public ClsFetcher()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
		//Returns a list of countries
		public static DataTable Country
		{
			get
			{
				return ClsSingleton.Country;
			}
		}
       
        //Returns all Fund Types
        public static DataTable Fund_Types
        {
            get
            {
                return ClsSingleton.Fund_Types;
            }
        }

        public static DataTable Currency
        {
            get
            {
                return ClsSingleton.Currency;
            }
        }
		//Returns a list of All countries (newly added countries also)
		public static DataTable AllCountry
		{
			get
			{
				return ClsSingleton.AllCountry;
			}
		}
		
		//Returns a list of States
		public static DataTable State
		{
			get
			{
				return ClsSingleton.State;
			}
		}

		//Returns only active states
		public static DataTable ActiveState
		{
			get
			{
				return ClsSingleton.ActiveState;
			}
		}

		//Returns the ASLOB code 
		public static DataTable ASLOB
		{
			get
			{
				return ClsSingleton.ASLOB;
			}
		}
		
		
		//Returns only process names
		public static DataTable PolicyProcess
		{
			get
			{
				return ClsSingleton.PolicyProcess;
			}
		}
		//Returns a list of Todolist type
		public static DataTable TodolistType
		{
			get
			{
				return ClsSingleton.TodolistType;
			}
		}
		public static DataTable TransactionListType
		{
			get
			{
				return ClsSingleton.TransactionListType;
			}
		}

		public static DataTable PolicyTermMonths
		{
			get
			{
				return ClsSingleton.PolicyTermMonths;
			}
		}
		public static DataTable LOBs
		{
			get
			{
				return ClsSingleton.LOBs;
			}
		}

		// Reinsurance Contract Type
		public static DataTable ReinsuranceContractType
		{
			get
			{
				return ClsSingleton.ReinsuranceContractType;
			}
		}

		//Subline Code
		public static DataTable SublineCode
		{
			get
			{
				return ClsSingleton.SublineCode;
			}
		}
        //Added by Praveen Kumar on 29/04/2010
        //SUSEP LOB
        public static DataTable Susep_lob
        {
            get
            {
                return ClsSingleton.SUSEP_LOB;
            }
        }	
	}
}
