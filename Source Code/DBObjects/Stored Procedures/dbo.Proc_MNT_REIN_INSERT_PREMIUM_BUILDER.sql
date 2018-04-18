IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_MNT_REIN_INSERT_PREMIUM_BUILDER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_MNT_REIN_INSERT_PREMIUM_BUILDER]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------          
Proc Name       : dbo.[Proc_MNT_REIN_INSERT_PREMIUM_BUILDER]          
Created by      : Swarup        
Date            : Aug 20,2007.          
Purpose       :          
Used In        : Wolverine          
------------------------------------------------------------          
Date     Review By          Comments      
drop PROC [dbo].[Proc_MNT_REIN_INSERT_PREMIUM_BUILDER]          
------   ------------       -------------------------*/          
CREATE PROC [dbo].[Proc_MNT_REIN_INSERT_PREMIUM_BUILDER]          
(    
 @PREMIUM_BUILDER_ID int output,         
 @CONTRACT_ID  int,          
 @CONTRACT nvarchar(50),    
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
 @CREATED_BY     smallint,          
 @CREATED_DATETIME     datetime    
           
)          
AS          
BEGIN          
       
--begin Transaction         
select @PREMIUM_BUILDER_ID=isnull(Max(PREMIUM_BUILDER_ID),0)+1 from MNT_REIN_PREMIUM_BUILDER          
INSERT INTO MNT_REIN_PREMIUM_BUILDER          
(     
 PREMIUM_BUILDER_ID,    
 CONTRACT_ID,    
 CONTRACT,    
 EFFECTIVE_DATE,    
 EXPIRY_DATE,    
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
 CREATED_BY  ,          
 CREATED_DATETIME,        
 IS_ACTIVE             
)          
VALUES          
(    
 @PREMIUM_BUILDER_ID,    
 @CONTRACT_ID,    
 @CONTRACT,    
 @EFFECTIVE_DATE,    
 @EXPIRY_DATE,    
 @LAYER,    
 @COVERAGE_CATEGORY,    
 @CALCULATION_BASE,    
 @INSURANCE_VALUE,    
 @TOTAL_INSURANCE_FROM,    
 @TOTAL_INSURANCE_TO,    
 @OTHER_INST,    
 @RATE_APPLIED,    
 @CONSTRUCTION,    
 @PROTECTION,    
 @ALARM_CREDIT,    
 @ALARM_PERCENTAGE,    
 @HOME_CREDIT,    
 @HOME_AGE,    
 @HOME_PERCENTAGE,
 @COMMENTS,    
 @CREATED_BY,          
  @CREATED_DATETIME,        
  'Y'            
)         
        
    
        
END    
  








GO

