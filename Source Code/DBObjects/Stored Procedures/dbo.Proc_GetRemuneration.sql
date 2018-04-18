IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetRemuneration]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetRemuneration]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




        
/*----------------------------------------------------------                              
Proc Name      : dbo.[Proc_GetRemuneration]                              
Created by       : praveer panghal                           
Date             : 14-02-2011                              
Purpose       : retrieving data from POL_REMUNERATION                          
Revison History :                     
Modify by       :                              
Date             :                              
Purpose       :             
                           
Used In        : Ebix Advantage                          
------------------------------------------------------------                              
Date     Review By          Comments                              
------   ------------       -------------------------*/                              
--drop proc dbo.Proc_GetRemuneration    
   -- SELECT * FROM POL_REMUNERATION
CREATE PROCEDURE [dbo].[Proc_GetRemuneration]   
@REMUNERATION_ID INT  ,
@CUSTOMER_ID INT,
@POLICY_ID INT,
@POLICY_VERSION_ID INT  
AS    
BEGIN     
SELECT    
  REMUNERATION_ID,    
   CUSTOMER_ID,
   POLICY_ID,
   POLICY_VERSION_ID,
   BROKER_ID,
   COMMISSION_PERCENT,
   COMMISSION_TYPE,
  IS_ACTIVE,    
  CREATED_BY,    
  CREATED_DATETIME,    
  MODIFIED_BY,    
  LAST_UPDATED_DATETIME  ,
  BRANCH,
    AMOUNT,
    LEADER,
    NAME,
    RISK_ID,
    CO_APPLICANT_ID
   

 FROM POL_REMUNERATION WITH(NOLOCK)     
 WHERE REMUNERATION_ID=@REMUNERATION_ID  AND  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID  
END 


GO

