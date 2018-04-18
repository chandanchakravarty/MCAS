IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPendingProcessforEOD]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPendingProcessforEOD]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop proc dbo.Proc_GetPendingProcessforEOD



/*----------------------------------------------------------
Proc Name       : dbo.Proc_GetPendingProcessforEOD
Created by      : 	Anurag Verma
Date                : 	3/21/2007
Purpose         : 	To fetch pending process for diary entry from EOD process
Revison History :
Used In         :  	 Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE   PROC dbo.Proc_GetPendingProcessforEOD
(
@DAY		int

)
AS

BEGIN
--insert into 
SELECT DISTINCT DATEDIFF(DD,PPP.CREATED_DATETIME,GETDATE()) COUNT_DAYS,LISTTYPEID,PPP.CUSTOMER_ID,PPP.POLICY_ID, 
PPP.POLICY_VERSION_ID,TDL.PROCESS_ROW_ID,PPP.PROCESS_STATUS,TDL.LISTTYPEID,TDL.MODULE_ID,PCPL.POLICY_LOB
FROM POL_POLICY_PROCESS PPP with (NOLOCK)
INNER JOIN TODOLIST  TDL with (NOLOCK) ON PPP.CUSTOMER_ID=TDL.CUSTOMER_ID AND PPP.POLICY_ID=TDL.POLICY_ID AND 
PPP.POLICY_VERSION_ID=TDL.POLICY_VERSION_ID AND PPP.ROW_ID=TDL.PROCESS_ROW_ID
LEFT OUTER JOIN POL_CUSTOMER_POLICY_LIST PCPL with (NOLOCK)  ON PPP.CUSTOMER_ID=PCPL.CUSTOMER_ID AND PPP.POLICY_ID=PCPL.POLICY_ID
AND PPP.POLICY_VERSION_ID=PCPL.POLICY_VERSION_ID
WHERE PPP.PROCESS_STATUS='PENDING' AND DATEDIFF(DD,PPP.CREATED_DATETIME,GETDATE())>@DAY
AND PCPL.POLICY_LOB = 4
--and convert(varchar(15),recdate,101)=convert(varchar(15),getdate(),101)
END








GO

