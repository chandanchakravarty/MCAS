IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateRisk_Renumeration]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateRisk_Renumeration]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  
  /*----------------------------------------------------------            
Proc Name       : dbo.Proc_UpdateRisk_Renumeration          
Created by      : Sonal 
Date            : 12/08/2010            
Purpose       :To update renumeration accoprding risk if product is risk level           
Revison History :            
Used In        : Ebix Advantage            
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/            
--DROP PROC dbo.Proc_UpdateRisk_Renumeration  

        
CREATE PROC [dbo].[Proc_UpdateRisk_Renumeration]  
(        
 @CUSTOMER_ID int,  
 @POLICY_ID int ,  
 @POLICY_VERSION_ID smallint,  
 @RISK_ID smallint,
 @CREATED_BY int

)            
AS            
BEGIN  
 
   DECLARE @AGENCY_ID INT,
           @REMUNERATION_ID INT
   
   SELECT @AGENCY_ID= AGENCY_ID FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID
   SELECT @REMUNERATION_ID=ISNULL(MAX(REMUNERATION_ID),0)+1 FROM POL_REMUNERATION WITH(NOLOCK)          


  UPDATE POL_REMUNERATION SET RISK_ID=@RISK_ID WHERE RISK_ID=0 AND CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID 
    
  IF NOT EXISTS (SELECT RISK_ID FROM POL_REMUNERATION WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND RISK_ID=@RISK_ID AND BROKER_ID=@AGENCY_ID)
  BEGIN
    
     INSERT INTO POL_REMUNERATION(REMUNERATION_ID,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,BROKER_ID,COMMISSION_PERCENT,IS_ACTIVE,
     COMMISSION_TYPE,CREATED_BY,CREATED_DATETIME,BRANCH,AMOUNT,LEADER,NAME,RISK_ID)
     SELECT TOP 1 @REMUNERATION_ID,@CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID,@AGENCY_ID,COMMISSION_PERCENT,'Y',COMMISSION_TYPE,
     @CREATED_BY,GETDATE(),BRANCH,AMOUNT,LEADER,NAME,@RISK_ID FROM POL_REMUNERATION 
     WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID 
     AND BROKER_ID=@AGENCY_ID

   
  END
  
 
   
        
  
END            

        
      
    
    
    
    
GO

