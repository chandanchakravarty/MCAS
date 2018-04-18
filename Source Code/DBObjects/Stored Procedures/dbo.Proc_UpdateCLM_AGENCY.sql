IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateCLM_AGENCY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateCLM_AGENCY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                  
Proc Name       : dbo.Proc_UpdateCLM_AGENCY                                            
Created by      : Sumit Chhabra                                                
Date            : 26/05/2006                                                  
Purpose         : Update data at CLM_AGENCY table for agency records
Created by      : Sumit Chhabra                                                 
Revison History :                                                  
Used In        : Wolverine                                                  
------------------------------------------------------------                                                  
Date     Review By          Comments                                                  
------   ------------       -------------------------*/                                                  
CREATE PROC dbo.Proc_UpdateCLM_AGENCY           
(    
@CLAIM_ID int,
@AGENCY_ID int,                                       
@AGENCY_CODE varchar(10),
@AGENCY_SUB_CODE varchar(10),
@AGENCY_CUSTOMER_ID varchar(10),
@AGENCY_PHONE nvarchar(20),
@AGENCY_FAX nvarchar(20),
@MODIFIED_BY int,
@LAST_UPDATED_DATETIME datetime 
)                         
AS                                                  
BEGIN            

	UPDATE 
		CLM_AGENCY
	SET		                               
		AGENCY_CODE=@AGENCY_CODE,
		AGENCY_SUB_CODE=@AGENCY_SUB_CODE,
		AGENCY_CUSTOMER_ID=@AGENCY_CUSTOMER_ID,
		AGENCY_PHONE=@AGENCY_PHONE,
		AGENCY_FAX=@AGENCY_FAX,
		MODIFIED_BY=@MODIFIED_BY,
		LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME
	WHERE
		CLAIM_ID=@CLAIM_ID AND
		AGENCY_ID=@AGENCY_ID        

END        
      
  



GO

