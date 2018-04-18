 IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Insert_MOTOR_QUOTE_DETAILS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Insert_MOTOR_QUOTE_DETAILS]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO


 
 /*----------------------------------------------------------    
Proc Name       : [Proc_Insert_MOTOR_QUOTE_DETAILS]    
Created by      : Agniswar Das    
Date            : 7/17/2011    
Purpose			: Demo    
Revison History :    
Used In        : Singapore    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/   




CREATE PROC dbo.Proc_Insert_MOTOR_QUOTE_DETAILS(    
     @CUSTOMER_ID int    
           ,@POLICY_ID int    
           ,@POLICY_VERSION_ID smallint    
           ,@QUOTE_ID int    
           ,@YEAR_OF_REG varchar(4) = null    
           ,@MAKE nvarchar(75) = null    
           ,@MODEL nvarchar(75) = null    
           ,@MODEL_TYPE nvarchar(75) = null    
           ,@ENG_CAPACITY int = null    
           ,@NO_OF_DRIVERS smallint = null    
           ,@ANY_CLAIM nchar(1) = null    
           ,@NO_OF_CLAIM int = null    
           ,@TOTAL_CLAIM_AMT int    
           ,@COVERAGE_TYPE int = null    
           ,@NO_CLAIM_DISCOUNT nchar(1) = null    
           ,@IS_ACTIVE nchar(1) = null    
           ,@CREATED_BY int = null    
           ,@CREATED_DATETIME datetime = null    
           ,@MODIFIED_BY int  = null    
           ,@LAST_UPDATED_DATETIME datetime = null  
           ,@NAMED_DRIVER_AMT nvarchar(30) = null  
           ,@UNNAMED_DRIVER_AMT nvarchar(30) = null)    
    
AS    
    
    
IF NOT EXISTS(select * from QQ_MOTOR_QUOTE_DETAILS where CUSTOMER_ID = @CUSTOMER_ID    
and POLICY_ID = @POLICY_ID and POLICY_VERSION_ID = @POLICY_VERSION_ID and QUOTE_ID = @QUOTE_ID)    
    
BEGIN    
    
    
    
INSERT INTO [dbo].[QQ_MOTOR_QUOTE_DETAILS]    
           ([CUSTOMER_ID]    
           ,[POLICY_ID]    
           ,[POLICY_VERSION_ID]    
           ,[QUOTE_ID]    
           ,[YEAR_OF_REG]    
           ,[MAKE]    
           ,[MODEL]    
           ,[MODEL_TYPE]    
           ,[ENG_CAPACITY]    
           ,[NO_OF_DRIVERS]    
           ,[ANY_CLAIM]    
           ,[NO_OF_CLAIM]    
           ,[TOTAL_CLAIM_AMT]    
           ,[COVERAGE_TYPE]    
           ,[NO_CLAIM_DISCOUNT]    
           ,[IS_ACTIVE]    
           ,[CREATED_BY]    
           ,[CREATED_DATETIME]    
           ,[MODIFIED_BY]    
           ,[LAST_UPDATED_DATETIME]  
           ,[NAMED_DRIVER_AMT]  
           ,[UNNAMED_DRIVER_AMT])    
     VALUES    
           (@CUSTOMER_ID    
           ,@POLICY_ID    
           ,@POLICY_VERSION_ID    
           ,@QUOTE_ID    
           ,@YEAR_OF_REG    
           ,@MAKE    
           ,@MODEL    
           ,@MODEL_TYPE    
           ,@ENG_CAPACITY    
           ,@NO_OF_DRIVERS    
           ,@ANY_CLAIM    
           ,@NO_OF_CLAIM    
           ,@TOTAL_CLAIM_AMT    
           ,@COVERAGE_TYPE    
           ,@NO_CLAIM_DISCOUNT    
           ,@IS_ACTIVE     
           ,@CREATED_BY    
           ,@CREATED_DATETIME    
           ,@MODIFIED_BY    
           ,@LAST_UPDATED_DATETIME  
           ,@NAMED_DRIVER_AMT  
           ,@UNNAMED_DRIVER_AMT)    
               
RETURN 1               
    
    
END    
    
ELSE    
--- UPDATE     
BEGIN    
    
UPDATE [dbo].[QQ_MOTOR_QUOTE_DETAILS]    
   SET [YEAR_OF_REG] = @YEAR_OF_REG    
      ,[MAKE] = @MAKE    
      ,[MODEL] = @MODEL    
      ,[MODEL_TYPE] = @MODEL_TYPE    
      ,[ENG_CAPACITY] = @ENG_CAPACITY    
      ,[NO_OF_DRIVERS] = @NO_OF_DRIVERS    
      ,[ANY_CLAIM] = @ANY_CLAIM    
      ,[NO_OF_CLAIM] = @NO_OF_CLAIM    
      ,[TOTAL_CLAIM_AMT] = @TOTAL_CLAIM_AMT    
      ,[COVERAGE_TYPE] = @COVERAGE_TYPE    
      ,[NO_CLAIM_DISCOUNT] = @NO_CLAIM_DISCOUNT   
      ,[NAMED_DRIVER_AMT] = @NAMED_DRIVER_AMT  
      ,[UNNAMED_DRIVER_AMT] = @UNNAMED_DRIVER_AMT  
        
 WHERE [CUSTOMER_ID] = @CUSTOMER_ID and    
       [POLICY_ID] = @POLICY_ID and    
       [POLICY_VERSION_ID] = @POLICY_VERSION_ID and    
       [QUOTE_ID] = @QUOTE_ID    
           
           
 RETURN 2    
END