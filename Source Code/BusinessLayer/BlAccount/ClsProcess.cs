using System;
using System.Data;
using Cms.BusinessLayer.BlCommon;

namespace Cms.BusinessLayer.BlAccount
{
	/// <summary>
	/// Summary description for ClsProcess.
	/// </summary>
	public class ClsProcess
	{
		public ClsProcess()
		{
			//
			// TODO: Add constructor logic here
			//
		}


		public DataTable GetProcess()
		{
			try
			{
				DataSet ds = BlCommon.ClsCommon.ExecuteDataSet("SELECT PROCESS_ID, PROCESS_DESC, PROCESS_SHORTNAME FROM POL_PROCESS_MASTER WITH(NOLOCK) WHERE ISNULL(IS_ACTIVE,'') = 'Y'");
				return ds.Tables[0];
			}
			catch 
			{
				throw(new Exception("Unable to fetch details of process."));
			}
		}


		/// <summary>
		/// Starts the process, which should be executed under EOD Processes.
		/// </summary>
		public void StartProcess(string Activity, DateTime StartDate, DateTime EndDate)
		{
			try
			{
				switch(Activity.ToUpper())
				{
					case "SUSPENSE_TO_NORMAL":		//In premium posting, moving suspense payment to normal payment
						break;
					case "PREMIUM_NOTICE":		//Making premium notices.
						break;
					case "CANCEL_NOTICE":		//Performing cancellation notices
						break;
					case "CANCEL_PROCESS":		//Cancellation process
						break;
				}
			}
			catch
			{
			}
		}
	}
}
