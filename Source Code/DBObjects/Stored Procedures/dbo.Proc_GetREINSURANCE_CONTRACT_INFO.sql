IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetREINSURANCE_CONTRACT_INFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetREINSURANCE_CONTRACT_INFO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------      
Proc Name       : dbo.Proc_GetREINSURANCE_CONTRACT_INFO      
Created by      : Swarup     
Date            : 3/9/2007      
Purpose       :      
Revison History :      
Used In        : Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
--drop PROC dbo.Proc_GetREINSURANCE_CONTRACT_INFO 62      
CREATE PROC [dbo].[Proc_GetREINSURANCE_CONTRACT_INFO]      
(      
 @CONTRACT_ID     int      
)      
AS      
BEGIN    
  
SELECT   
 A.CONTRACT_ID CONTRACT_ID,C.CONTRACT_TYPE_DESC AS CONTRACT_TYPE,  
 A.CONTRACT_NUMBER CONTRACT_NUMBER,CASH_CALL_LIMIT,  
 A.CALCULATION_BASE PREMIUM_BASIS,ISNULL(CONVERT(VARCHAR,A.EFFECTIVE_DATE,101),'') AS EFFECTIVE_DATE,  
 ISNULL(CONVERT(VARCHAR,A.EXPIRATION_DATE,101),'') AS EXPIRATION_DATE, A.COMMISSION COMMISSION,  
 ISNULL(CONVERT(VARCHAR,A.TERMINATION_DATE,101),'') TERMINATION_DATE,A.IS_ACTIVE IS_ACTIVE,  
 L1.LOOKUP_VALUE_DESC CALCULATION_BASE  
FROM   
 MNT_REINSURANCE_CONTRACT A   
 INNER JOIN MNT_REINSURANCE_CONTRACT_TYPE C ON C.CONTRACTTYPEID =A.CONTRACT_TYPE  
 INNER JOIN MNT_LOOKUP_VALUES L1 ON L1.LOOKUP_UNIQUE_ID=A.CALCULATION_BASE  
       
      
      
WHERE    
 CONTRACT_ID = @CONTRACT_ID      
      
END      
    
  
  
GO

