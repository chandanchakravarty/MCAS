IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchCheckInformationOnId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchCheckInformationOnId]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
Proc Name            : Dbo.Proc_FetchCheckInformationOnId      
Created by             : kranti singh      
Date                    : 18/06/2007      
Purpose                 :       
Revison History :      
Used In                 :   Wolverine        
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
---- DROP PROC Dbo.Proc_FetchCheckInformationOnId        
    
create procedure dbo.Proc_FetchCheckInformationOnId    
(    
 @CHECK_ID int    
)    
AS    
    
    
SELECT MLV.LOOKUP_VALUE_DESC ,
CONVERT(VARCHAR,CONVERT(MONEY,CHECK_AMOUNT),1) AS CHECK_AMOUNT_FORMATTED,
ISNULL(PAYMENT_MODE,0) AS PAYMENT_MODE,
ISNULL(CHECK_TYPE,0) AS CHECK_TYPE,
ISNULL(CUSTOMER_ID,0) AS CUSTOMER_ID,
ISNULL(POLICY_ID,0) AS POLICY_ID,
ISNULL(POLICY_VER_TRACKING_ID,0) AS POLICY_VER_TRACKING_ID,
* FROM ACT_CHECK_INFORMATION ACI 
LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV ON MLV.LOOKUP_UNIQUE_ID =ACI.CHECK_TYPE
WHERE CHECK_ID =@CHECK_ID   



    
   






GO

