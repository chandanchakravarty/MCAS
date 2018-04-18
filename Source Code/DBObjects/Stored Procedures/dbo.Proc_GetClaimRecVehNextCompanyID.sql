IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetClaimRecVehNextCompanyID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetClaimRecVehNextCompanyID]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                    
Proc Name       : dbo.Proc_GetClaimRecVehNextCompanyID                   
Created by      : Sumit Chhabra        
Date            : 11/08/2008                    
Purpose      	: Get next company ID for the current claim
Revison History :                    
Used In   : Wolverine                    
                  
        
------------------------------------------------------------                    
Date     Review By          Comments                    
------   ------------       -------------------------*/                    
--drop proc Proc_GetClaimRecVehNextCompanyID                   
                  
CREATE PROC dbo.Proc_GetClaimRecVehNextCompanyID                 
(                    
 @CLAIM_ID int                                  
)                    
AS                  
BEGIN                    
declare @NEXT_COMPANY_ID_NUMBER smallint
	SELECT @NEXT_COMPANY_ID_NUMBER = ISNULL(MAX(COMPANY_ID_NUMBER),0) + 1 FROM
		CLM_RECREATIONAL_VEHICLES WHERE CLAIM_ID=@CLAIM_ID

	return @NEXT_COMPANY_ID_NUMBER

END                    
                

                  
                  
                  
                  
                  
                  
                
              
            
          
          
          
          
          
          
          
          
          
        
      
    
  



GO

