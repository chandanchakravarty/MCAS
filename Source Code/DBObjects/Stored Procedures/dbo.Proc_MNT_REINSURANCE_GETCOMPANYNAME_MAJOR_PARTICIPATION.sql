IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_MNT_REINSURANCE_GETCOMPANYNAME_MAJOR_PARTICIPATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_MNT_REINSURANCE_GETCOMPANYNAME_MAJOR_PARTICIPATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------  
Created by  : Harmanjeet Singh  
Purpose     : Evaluation for the Contract Type screen  
Date  : May 7, 2007  
Revison History :  
Used In  : Wolverine  


Updated by  : Shikha Chourasiya
Purpose     : itrack issue 1000("CARRIER TYPE" = 'REINSURER')
Date  : Mar 31, 2011  
 
------------------------------------------------------------  
Date     Review By          Comments  
drop proc [dbo].Proc_MNT_REINSURANCE_GETCOMPANYNAME_MAJOR_PARTICIPATION  
------   ------------       -------------------------*/  
  
CREATE PROC [dbo].[Proc_MNT_REINSURANCE_GETCOMPANYNAME_MAJOR_PARTICIPATION]  
  
AS  
BEGIN  
  
       SELECT REIN_COMAPANY_ID AS REIN_COMPANY_ID,REIN_COMAPANY_NAME AS REIN_COMPANY_NAME 
		FROM MNT_REIN_COMAPANY_LIST 
		WHERE REIN_COMPANY_TYPE=13054
		ORDER BY REIN_COMAPANY_NAME    
  
END  
  
  
  
  
GO

