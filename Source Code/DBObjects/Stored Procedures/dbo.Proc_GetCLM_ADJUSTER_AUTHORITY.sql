IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCLM_ADJUSTER_AUTHORITY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCLM_ADJUSTER_AUTHORITY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                    
Proc Name       : dbo.Proc_GetCLM_ADJUSTER_AUTHORITY              
Created by      : Sumit Chhabra                  
Date            : 25/04/2006                    
Purpose         : Fetch data from CLM_ADJUSTER_AUTHORITY  for an adjuster and the chosen LOB            
Created by      : Sumit Chhabra                   
Revison History :                    
Used In        : Wolverine                    
------------------------------------------------------------                    
Date     Review By          Comments                    
------   ------------       -------------------------*/                    
--DROP PROC dbo.Proc_GetCLM_ADJUSTER_AUTHORITY  
CREATE PROC dbo.Proc_GetCLM_ADJUSTER_AUTHORITY                
@ADJUSTER_AUTHORITY_ID int,            
@ADJUSTER_ID int                  
AS                    
BEGIN                    
 SELECT             
  ADJUSTER_AUTHORITY_ID,LOB_ID,CAA.LIMIT_ID AS LIMIT_ID,    
  convert(char,EFFECTIVE_DATE,101) EFFECTIVE_DATE,    
 CAA.IS_ACTIVE AS IS_ACTIVE,      
 floor(CAL.PAYMENT_LIMIT) AS PAYMENT_LIMIT,   
 floor(CAL.RESERVE_LIMIT) AS RESERVE_LIMIT,     
 ISNULL(CAA.NOTIFY_AMOUNT,0) AS NOTIFY_AMOUNT  
 FROM             
  CLM_ADJUSTER_AUTHORITY CAA INNER JOIN CLM_AUTHORITY_LIMIT CAL       
 ON      
 CAA.LIMIT_ID = CAL.LIMIT_ID      
 WHERE             
  ADJUSTER_AUTHORITY_ID = @ADJUSTER_AUTHORITY_ID AND            
  ADJUSTER_ID = @ADJUSTER_ID            
END              
  
  



GO

