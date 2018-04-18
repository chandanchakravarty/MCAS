IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAppEffectiveDate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAppEffectiveDate]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  create PROCEDURE dbo.Proc_GetAppEffectiveDate            
(            
 @CUSTOMER_ID int,            
 @APP_ID int,            
 @APP_VERSION_ID int            
)                
AS    
  
 /*----------------------------------------------------------                      
Proc Name    : dbo.Proc_GetAppEffectiveDate      
Created by    : Swastika            
Date          : 20 October,2005            
Purpose       : Get the Effective date of the Application for Watercraft      
Revison History  :                            
 ------------------------------------------------------------                                  
Date     Review By          Comments                                
                       
------   ------------       -------------------------*/              
-- drop proc dbo.Proc_GetAppEffectiveDate     
                   
BEGIN                      
            
-- SELECT DATEPART(YEAR,APP_EFFECTIVE_DATE) AS APP_EFFECTIVE_DATE            
 SELECT CONVERT(VARCHAR,APP_EFFECTIVE_DATE,101) AS APP_EFFECTIVE_DATE            
 FROM APP_LIST           
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID  
       
End           
  
  
GO

