IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPDFCancellationCode]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPDFCancellationCode]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                  
Proc Name       : dbo.Proc_GetPDFCancellationCode            
Created by      : Mohit Agarwal            
Date            : 7-Feb-2007          
Purpose       : Return Cancellation Notice Code for a lob,state and agent/insured            
Revison History :                  
Used In    : Wolverine                  
------------------------------------------------------------                  
Date     Review By          Comments                  
------   ------------       -------------------------*/             
--drop proc dbo.Proc_GetPDFCancellationCode                 
CREATE PROC dbo.Proc_GetPDFCancellationCode           
(                  
  @LOB_CODE  varchar(20),              
  @STATE_CODE  varchar(20),              
  @INS_AGN varchar(20),     
  @CANC_CODE varchar(20)              
)                  
AS                  
BEGIN               
   SELECT DOCUMENT_CODE FROM MNT_PRINT_DOCUMENT_TYPE WHERE DOCUMENT_TYPE LIKE '%' + @LOB_CODE + '%' AND       
          DOCUMENT_TYPE LIKE '%' + @STATE_CODE + ' %' AND      
          DOCUMENT_TYPE LIKE '%' + UPPER(@INS_AGN) + '%' AND        
          DOCUMENT_TYPE LIKE '%' + UPPER(@CANC_CODE)-- + '%'        
END              
            
            
          
          
          
          
          
        
      
    
  



GO

