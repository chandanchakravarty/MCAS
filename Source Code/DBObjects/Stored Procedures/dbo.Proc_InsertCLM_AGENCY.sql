IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertCLM_AGENCY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertCLM_AGENCY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                      
Proc Name       : dbo.Proc_InsertCLM_AGENCY                                                
Created by      : Sumit Chhabra                                                    
Date            : 26/05/2006                                                      
Purpose         : Insert data at CLM_AGENCY table for AGENCY RECORDS  
Created by      : Sumit Chhabra                                                     
Revison History :                                                      
Used In        : Wolverine                                                      
------------------------------------------------------------                                                      
Date     Review By          Comments                                                      
------   ------------       -------------------------*/                                                      
CREATE PROC dbo.Proc_InsertCLM_AGENCY               
(        
@CLAIM_ID int,    
@AGENCY_ID int output,                                           
@AGENCY_CODE varchar(10),    
@AGENCY_SUB_CODE varchar(10),    
@AGENCY_CUSTOMER_ID varchar(10),    
@AGENCY_PHONE nvarchar(20),    
@AGENCY_FAX nvarchar(20),    
@CREATED_BY int,    
@CREATED_DATETIME datetime     
)                             
AS                                                      
BEGIN                
    
 SELECT     
  @AGENCY_ID=ISNULL(MAX(AGENCY_ID),0)+1     
 FROM     
  CLM_AGENCY     
 WHERE     
  CLAIM_ID=@CLAIM_ID    
INSERT INTO CLM_AGENCY    
(    
CLAIM_ID,    
AGENCY_ID,                                           
AGENCY_CODE,    
AGENCY_SUB_CODE,    
AGENCY_CUSTOMER_ID,    
AGENCY_PHONE,    
AGENCY_FAX,    
CREATED_BY,    
CREATED_DATETIME,    
IS_ACTIVE      
)    
VALUES    
(    
@CLAIM_ID,    
@AGENCY_ID,                                           
@AGENCY_CODE,    
@AGENCY_SUB_CODE,    
@AGENCY_CUSTOMER_ID,    
@AGENCY_PHONE,    
@AGENCY_FAX,    
@CREATED_BY,    
@CREATED_DATETIME,    
'Y'      
)    
END            
          
      
    
  








GO

