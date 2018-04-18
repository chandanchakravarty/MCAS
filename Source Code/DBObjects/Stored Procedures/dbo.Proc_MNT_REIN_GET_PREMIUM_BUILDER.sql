IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_MNT_REIN_GET_PREMIUM_BUILDER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_MNT_REIN_GET_PREMIUM_BUILDER]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------          
Proc Name       : Proc_MNT_REIN_GET_PREMIUM_BUILDER        
Created by      : Swarup          
Date            : 20-Aug-2007         
Purpose     : Get values from  MNT_REIN_PREMIUM_BUILDER table          
Revison History :          
Used In  : Wolverine          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/          
      
-- drop proc dbo.Proc_MNT_REIN_GET_PREMIUM_BUILDER         
CREATE PROC dbo.Proc_MNT_REIN_GET_PREMIUM_BUILDER              
(          
 @PREMIUM_BUILDER_ID      int          
)          
AS          
BEGIN          
 SELECT          
  PREMIUM_BUILDER_ID,  
  CONTRACT_ID,  
  CONTRACT,  
  --convert(varchar,EFFECTIVE_DATE,101) as EFFECTIVE_DATE ,    
 -- convert(varchar,EXPIRY_DATE,101) as EXPIRY_DATE ,    
  LAYER,  
  COVERAGE_CATEGORY,  
  CALCULATION_BASE,  
  INSURANCE_VALUE,  
  TOTAL_INSURANCE_FROM,  
  TOTAL_INSURANCE_TO,  
  OTHER_INST,  
  RATE_APPLIED,  
  CONSTRUCTION,  
  PROTECTION,  
  ALARM_CREDIT,  
  ALARM_PERCENTAGE,  
  HOME_CREDIT,  
  HOME_AGE,
  HOME_PERCENTAGE,  
  COMMENTS,  
  IS_ACTIVE        
 FROM     
  MNT_REIN_PREMIUM_BUILDER      
 WHERE           
  PREMIUM_BUILDER_ID = @PREMIUM_BUILDER_ID          
  
END          
  
  
  






GO

