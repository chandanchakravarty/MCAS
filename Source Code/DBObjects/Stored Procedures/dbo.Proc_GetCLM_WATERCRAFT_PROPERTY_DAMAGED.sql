IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCLM_WATERCRAFT_PROPERTY_DAMAGED]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCLM_WATERCRAFT_PROPERTY_DAMAGED]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------              
Proc Name       : dbo.Proc_GetCLM_WATERCRAFT_PROPERTY_DAMAGED     
Created by      : Sumit Chhabra            
Date            : 24 May,2006              
Purpose         : Fetch records from CLM_WATERCRAFT_PROPERTY_DAMAGED         
Revison History :              
Used In         : Wolverine              
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       -------------------------   */          
          
CREATE PROCEDURE dbo.Proc_GetCLM_WATERCRAFT_PROPERTY_DAMAGED      
(          
@PROPERTY_DAMAGED_ID int,
@CLAIM_ID int
)          
          
As          
BEGIN  

 SELECT 
		DESCRIPTION,
		OTHER_VEHICLE,
		OTHER_INSURANCE_NAME,
		OTHER_OWNER_NAME,
		ADDRESS1,
		ADDRESS2,
		CITY,
		STATE,
		ZIP,
		HOME_PHONE,
		WORK_PHONE,
		IS_ACTIVE
 FROM
  CLM_WATERCRAFT_PROPERTY_DAMAGED              
 WHERE
		CLAIM_ID=@CLAIM_ID AND
		PROPERTY_DAMAGED_ID=@PROPERTY_DAMAGED_ID
END          
      


GO

