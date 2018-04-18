IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetGLAccounts_Reins]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetGLAccounts_Reins]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create   PROCEDURE dbo.Proc_GetGLAccounts_Reins
AS
BEGIN
	SELECT t1.account_id,t1.ACC_DISP_NUMBER as ACC_NUMBER,
	case when t1.acc_parent_id is null 
		then t1.ACC_DESCRIPTION + ' : ' +  isnull(t1.ACC_DISP_NUMBER,'')  
		else  isnull(t2.acc_description,'') + ' - ' + t1.ACC_DESCRIPTION  + ' : ' + isnull(t1.ACC_DISP_NUMBER,'')
		end as ACC_DESCRIPTION
	FROM ACT_GL_ACCOUNTS t1 
	LEFT OUTER JOIN  ACT_GL_ACCOUNTS t2 ON t2.account_id = t1.acc_parent_id 
	WHERE t1.ACC_LEVEL_TYPE='AS'   and   t1.ACC_RELATES_TO_TYPE IN (11199,11200)    
	ORDER BY ACC_DESCRIPTION   
END

 /*----------------------------------------------------------                                                    
Proc Name       : dbo.Proc_GetFiscalYearActiveCancPolicies                                                    
Created by      :  Mohit Agarwal                                                    
Date            :  16-May-2007                                                    
Purpose         :  To get Fiscal Year policies which are Active and Cancelled                                                    
Revison History :                                                    
Used In         :    Wolverine                                                    
                                                    
Modified By		:  Praveen kasana                                          
Modified Date	:  10 March 2008
purpose			: Append A   -If it is ab policy, cancelled or non cancelled all
Blank a space if normal policy, if DB normal. Including suspended, treat suspended as normal
Append N       --- If cancelled 
For DB
If Brics policy status is Cancelled NSF Replace,
Cancelled NSF No Replace,
Marked for Cancellation NSF Replace, 
Marked for Cancellation NSF No Replace,
or Cancellation in Progress, 
code the policy on the hotfile with a 'N'.
------------------------------------------------------------                                                    
Date     Review By          Comments                                                    
------   ------------       -------------------------*/                                                    

GO

