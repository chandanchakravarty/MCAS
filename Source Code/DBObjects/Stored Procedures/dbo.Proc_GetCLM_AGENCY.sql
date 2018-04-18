IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCLM_AGENCY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCLM_AGENCY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                    
Proc Name       : dbo.Proc_GetCLM_AGENCY                                              
Created by      : Sumit Chhabra                                                  
Date            : 26/05/2006                                                    
Purpose         : Fetch data from CLM_AGENCY table for agency records
Created by      : Sumit Chhabra                                                   
Revison History :                                                    
Used In        : Wolverine                                                    
------------------------------------------------------------                                                    
Date     Review By          Comments                                                    
------   ------------       -------------------------*/                                                    
CREATE PROC dbo.Proc_GetCLM_AGENCY             
(                                             
@CLAIM_ID int,          
@AGENCY_ID int          
)                           
AS                                                    
BEGIN              
  
SELECT   
 AGENCY_CODE,AGENCY_SUB_CODE,AGENCY_CUSTOMER_ID,AGENCY_PHONE,AGENCY_FAX,IS_ACTIVE  
FROM   
 CLM_AGENCY  
WHERE  
 CLAIM_ID=@CLAIM_ID AND  
 AGENCY_ID=@AGENCY_ID  
END     



GO

