IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetContractInformation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetContractInformation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
Proc Name       : dbo.Proc_GetContractInformation  
Created by      : Shafee  
Date            : 6/1/2005  
Purpose     :       
Revison History :    
Used In  : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
CREATE  PROC Dbo.Proc_GetContractInformation   
(  
@CONTRACT_ID SMALLINT  
)  
AS  
BEGIN  
 select convert(varchar,GL_ID)+'^'+convert(varchar,CONTRACT_ID)  as AccountID,  
 COMMISION_APPLICABLE,
 convert(varchar,GL_ID)+'^'+convert(varchar,REIN_PREMIUM_ACT) AS REIN_PREMIUM_ACT,
 convert(varchar,GL_ID)+'^'+convert(varchar,REIN_PAYMENT_ACT) AS REIN_PAYMENT_ACT,
 convert(varchar,GL_ID)+'^'+convert(varchar,REIN_COMMISION_ACT) AS REIN_COMMISION_ACT,
 convert(varchar,GL_ID)+'^'+convert(varchar,REIN_COMMISION_RECEVABLE) AS REIN_COMMISION_RECEVABLE
 FROM MNT_REINSURANCE_POSTING WHERE CONTRACT_ID=@CONTRACT_ID  AND GL_ID=1

END  
 
  UPDATE MNT_REINSURANCE_POSTING SET GL_ID=1



GO

