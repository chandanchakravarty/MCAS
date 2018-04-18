IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateCLM_WATERCRAFT_PROPERTY_DAMAGED]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateCLM_WATERCRAFT_PROPERTY_DAMAGED]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------              
Proc Name       : dbo.Proc_UpdateCLM_WATERCRAFT_PROPERTY_DAMAGED     
Created by      : Sumit Chhabra            
Date            : 24 May,2006              
Purpose         : Update records at CLM_WATERCRAFT_PROPERTY_DAMAGED         
Revison History :              
Used In         : Wolverine              
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       -------------------------   */          
          
CREATE PROCEDURE dbo.Proc_UpdateCLM_WATERCRAFT_PROPERTY_DAMAGED      
(          
@PROPERTY_DAMAGED_ID int,
@CLAIM_ID int,        
@DESCRIPTION varchar(300),
@OTHER_VEHICLE char(1),
@OTHER_INSURANCE_NAME varchar(150),
@OTHER_OWNER_NAME varchar(150),
@ADDRESS1 varchar(50),
@ADDRESS2 varchar(50),
@CITY varchar(10),
@STATE int,
@ZIP varchar(10),
@HOME_PHONE varchar(15),
@WORK_PHONE varchar(15),
@MODIFIED_BY int,
@LAST_UPDATED_DATETIME datetime
)          
          
As          
BEGIN  

 UPDATE
  CLM_WATERCRAFT_PROPERTY_DAMAGED              
 SET		
		DESCRIPTION=@DESCRIPTION,
		OTHER_VEHICLE=@OTHER_VEHICLE,
		OTHER_INSURANCE_NAME=@OTHER_INSURANCE_NAME,
		OTHER_OWNER_NAME=@OTHER_OWNER_NAME,
		ADDRESS1=@ADDRESS1,
		ADDRESS2=@ADDRESS2,
		CITY=@CITY,
		STATE=@STATE,
		ZIP=@ZIP,
		HOME_PHONE=@HOME_PHONE,
		WORK_PHONE=@WORK_PHONE,
		MODIFIED_BY=@MODIFIED_BY,
		LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME
 WHERE
		CLAIM_ID=@CLAIM_ID AND
		PROPERTY_DAMAGED_ID=@PROPERTY_DAMAGED_ID
END          
        
      
    
  



GO

