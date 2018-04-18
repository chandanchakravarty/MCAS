IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertCLM_LIABILITY_TYPE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertCLM_LIABILITY_TYPE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                            
                            
Proc Name       : Proc_InsertCLM_LIABILITY_TYPE    
Created by      : Sumit Chhabra            
Date            : 09/05/2006                            
Purpose         : Insert of Liability Type data in CLM_LIABILITY_TYPE            
Revison History :                            
Used In                   : Wolverine                            
------------------------------------------------------------                            
Date     Review By          Comments                            
------   ------------       -------------------------*/                            
CREATE PROC dbo.Proc_InsertCLM_LIABILITY_TYPE                   
(                            
 @LIABILITY_TYPE_ID int output,  
 @CLAIM_ID int,  
 @PREMISES_INSURED int,  
 @OTHER_DESCRIPTION varchar(25),  
 @TYPE_OF_PREMISES varchar(256),  
 @CREATED_BY int,      
 @CREATED_DATETIME datetime                      
)                            
AS                            
BEGIN      

	SELECT 
		@LIABILITY_TYPE_ID=ISNULL(MAX(LIABILITY_TYPE_ID),0)+1 
	FROM
		CLM_LIABILITY_TYPE
	WHERE
		CLAIM_ID = @CLAIM_ID
	                   
 INSERT INTO CLM_LIABILITY_TYPE
	(
		LIABILITY_TYPE_ID,
		CLAIM_ID,
		PREMISES_INSURED,
		OTHER_DESCRIPTION,
		TYPE_OF_PREMISES,
		CREATED_BY,
		CREATED_DATETIME,
		IS_ACTIVE
	)
	VALUES
	(
		@LIABILITY_TYPE_ID,
		@CLAIM_ID,
		@PREMISES_INSURED,
		@OTHER_DESCRIPTION,
		@TYPE_OF_PREMISES,
		@CREATED_BY,
		@CREATED_DATETIME,
		'Y'
	)   
END                  
      
      
    
  
  
  
  



GO

