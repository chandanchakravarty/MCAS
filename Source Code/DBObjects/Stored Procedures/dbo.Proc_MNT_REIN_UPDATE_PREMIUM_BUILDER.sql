IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_MNT_REIN_UPDATE_PREMIUM_BUILDER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_MNT_REIN_UPDATE_PREMIUM_BUILDER]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------      
Proc Name       : dbo.[Proc_MNT_REIN_UPDATE_PREMIUM_BUILDER]      
Created by      : Swarup     
Date            : Aug 20,2007.      
Purpose       :      
Revison History :      
    
Used In        : Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------    
drop proc [dbo].[Proc_MNT_REIN_UPDATE_PREMIUM_BUILDER]    
*/      
CREATE PROC [dbo].[Proc_MNT_REIN_UPDATE_PREMIUM_BUILDER]      
(      
 @PREMIUM_BUILDER_ID int,       
 @CONTRACT_ID  int,        
 @CONTRACT nvarchar(25),  
 @EFFECTIVE_DATE datetime,  
 @EXPIRY_DATE datetime,  
 @LAYER int,  
 @COVERAGE_CATEGORY varchar (200),  
 @CALCULATION_BASE int,  
 @INSURANCE_VALUE varchar(15),  
 @TOTAL_INSURANCE_FROM varchar(15),  
 @TOTAL_INSURANCE_TO varchar(15),  
 @OTHER_INST int,  
 @RATE_APPLIED numeric(10,4),  
 @CONSTRUCTION int,  
 @PROTECTION varchar(200),  
 @ALARM_CREDIT int,  
 @ALARM_PERCENTAGE numeric(10,4),  
 @HOME_CREDIT int,  
 @HOME_AGE int, 
 @HOME_PERCENTAGE numeric(10,4),      
 @COMMENTS varchar(300),  
 @MODIFIED_BY      smallint,      
 @LAST_UPDATED_DATETIME    datetime       
       
)      
AS      
BEGIN      
    
  
        
--begin Transaction     
UPDATE MNT_REIN_PREMIUM_BUILDER      
SET    
    
    
 PREMIUM_BUILDER_ID =@PREMIUM_BUILDER_ID    ,      
 CONTRACT_ID =@CONTRACT_ID  ,    
 CONTRACT =@CONTRACT   ,    
 EFFECTIVE_DATE=@EFFECTIVE_DATE ,    
 EXPIRY_DATE=@EXPIRY_DATE ,    
 LAYER=@LAYER ,    
 COVERAGE_CATEGORY =@COVERAGE_CATEGORY    ,      
 CALCULATION_BASE=@CALCULATION_BASE     ,      
 INSURANCE_VALUE=@INSURANCE_VALUE ,    
 TOTAL_INSURANCE_FROM=@TOTAL_INSURANCE_FROM   ,    
 TOTAL_INSURANCE_TO=@TOTAL_INSURANCE_TO ,    
 OTHER_INST=@OTHER_INST ,    
 RATE_APPLIED=@RATE_APPLIED ,    
 CONSTRUCTION=@CONSTRUCTION ,    
 PROTECTION =@PROTECTION,    
 ALARM_CREDIT =@ALARM_CREDIT,    
 ALARM_PERCENTAGE =@ALARM_PERCENTAGE,    
 HOME_CREDIT=@HOME_CREDIT ,    
 HOME_AGE=@HOME_AGE ,    
 HOME_PERCENTAGE =@HOME_PERCENTAGE,
 COMMENTS=@COMMENTS ,    
 MODIFIED_BY=@MODIFIED_BY    ,      
 LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME    
          
WHERE    
 PREMIUM_BUILDER_ID=@PREMIUM_BUILDER_ID  
    
  
    
    
END    
    
    
    
  






GO

