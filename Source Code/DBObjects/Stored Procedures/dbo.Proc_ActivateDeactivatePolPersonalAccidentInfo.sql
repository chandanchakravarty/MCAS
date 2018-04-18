IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivatePolPersonalAccidentInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivatePolPersonalAccidentInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                
Proc Name       : dbo.[Proc_ActivateDeactivatePolPersonalAccidentInfo]                
Created by      : Chetna Agarwal       
Date            : 16/04/2010   
Modify by		: PRADEEP KUSHWAHA
Date			: 25-05-2010              
Purpose			:To Activate and deactivate records in POL_PERSONAL_ACCIDENT_INFO table.                
Revison History :                
Used In			: Ebix Advantage                
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                
--DROP PROC dbo.Proc_ActivateDeactivatePolPersonalAccidentInfo         
      
      
CREATE  PROC dbo.Proc_ActivateDeactivatePolPersonalAccidentInfo      
(              
 @CUSTOMER_ID INT,
 @POLICY_ID INT,
 @POLICY_VERSION_ID SMALLINT,
 @PERSONAL_INFO_ID INT,                    
 @IS_ACTIVE   NCHAR(1)              
)              
AS              
BEGIN     
UPDATE POL_PERSONAL_ACCIDENT_INFO          
 SET               
    IS_ACTIVE  = @IS_ACTIVE ,
    ORIGINAL_VERSION_ID = CASE  WHEN @IS_ACTIVE='Y' THEN 0 ELSE  @POLICY_VERSION_ID END    
              
 WHERE              
    CUSTOMER_ID= @CUSTOMER_ID AND
	POLICY_ID=@POLICY_ID AND
	POLICY_VERSION_ID=@POLICY_VERSION_ID AND
	PERSONAL_INFO_ID=@PERSONAL_INFO_ID           
END 
GO

