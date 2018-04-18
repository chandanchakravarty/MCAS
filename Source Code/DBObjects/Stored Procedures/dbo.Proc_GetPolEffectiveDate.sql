IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolEffectiveDate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolEffectiveDate]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                    
Proc Name    : dbo.Proc_GetPolEffectiveDate    
Created by    : Swastika          
Date          : 20 October,2005          
Purpose       : Get the Effective date of the Policy for Watercraft    
Revison History  :                          
Modified by : Pravesh K Chandel  
Modified Date : 12 Jan 09  
 ------------------------------------------------------------                                
Date     Review By          Comments                              
                     
------   ------------       -------------------------*/            
-- drop proc dbo.Proc_GetPolEffectiveDate 1692,118,4           
CREATE PROCEDURE dbo.Proc_GetPolEffectiveDate          
(          
 @CUSTOMER_ID int,          
 @POLICY_ID int,          
 @POLICY_VERSION_ID int          
)              
AS                   
BEGIN                    
          
-- SELECT DATEPART(YEAR,APP_EFFECTIVE_DATE) AS APP_EFFECTIVE_DATE          
 SELECT CONVERT(VARCHAR,APP_EFFECTIVE_DATE,101) AS APP_EFFECTIVE_DATE
 FROM POL_CUSTOMER_POLICY_LIST with(nolock)  
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID --and IS_ACTIVE='Y'           
     
End    
  
  
  
  
GO

