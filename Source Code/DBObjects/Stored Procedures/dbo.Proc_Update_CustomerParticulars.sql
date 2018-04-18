 IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Update_CustomerParticulars]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Update_CustomerParticulars]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO


 
 /*----------------------------------------------------------    
Proc Name       : [Proc_Update_CustomerParticulars]    
Created by      : Agniswar Das    
Date            : 7/17/2011    
Purpose			: Demo    
Revison History :    
Used In        : Singapore    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/   





--Proc_Update_CustomerParticulars  
  
CREATE PROC dbo.Proc_Update_CustomerParticulars (    
   @ID int    
     ,@CUSTOMER_ID int    
     ,@QUOTE_ID int  
           ,@CUSTOMER_CODE nvarchar(10)= null  
           ,@CUSTOMER_TYPE int = null    
           ,@CUSTOMER_FIRST_NAME nvarchar(100) = null    
           ,@CUSTOMER_MIDDLE_NAME nvarchar(100)  = null  
           ,@CUSTOMER_LAST_NAME nvarchar(100)  = null  
           ,@DATE_OF_BIRTH datetime  = null  
           ,@GENDER nchar(1)  = null  
           ,@NATIONALITY nvarchar(100)  = null  
           ,@IS_HOME_EMPLOYEE nchar(2)  = null  
           ,@CUSTOMER_OCCU int  = null  
           ,@DRIVER_EXP_YEAR int  = null  
           ,@ANY_CLAIM nchar(2)  = null  
           ,@EXIST_NCD_LESS_10 nchar(2)  = null  
           ,@EXISTING_NCD nvarchar(25)  = null  
           ,@DEMERIT_DISCOUNT nchar(2)  = null  
           ,@CUSTOMER_AGENCY_ID smallint  = null  
           ,@IS_ACTIVE nchar(1)  = null  
           ,@CREATED_BY int  = null  
           ,@CREATED_DATETIME datetime  = null  
           ,@MODIFIED_BY int  = null  
           ,@LAST_UPDATED_DATETIME datetime = null)    
AS    
    
BEGIN    
IF EXISTS(select * from QQ_CUSTOMER_PARTICULAR where CUSTOMER_ID = @CUSTOMER_ID and QUOTE_ID = @QUOTE_ID)  
BEGIN  
  
  
UPDATE [dbo].[QQ_CUSTOMER_PARTICULAR]  
   SET [CUSTOMER_CODE] = @CUSTOMER_CODE  
      ,[CUSTOMER_TYPE] = @CUSTOMER_TYPE  
      ,[CUSTOMER_FIRST_NAME] = @CUSTOMER_FIRST_NAME  
      ,[CUSTOMER_MIDDLE_NAME] = @CUSTOMER_MIDDLE_NAME  
      ,[CUSTOMER_LAST_NAME] = @CUSTOMER_LAST_NAME  
      ,[DATE_OF_BIRTH] = @DATE_OF_BIRTH  
      ,[GENDER] = @GENDER  
      ,[NATIONALITY] = @NATIONALITY  
      ,[IS_HOME_EMPLOYEE] = @IS_HOME_EMPLOYEE  
      ,[CUSTOMER_OCCU] = @CUSTOMER_OCCU  
      ,[DRIVER_EXP_YEAR] = @DRIVER_EXP_YEAR  
      ,[ANY_CLAIM] = @ANY_CLAIM  
      ,[EXIST_NCD_LESS_10] = @EXIST_NCD_LESS_10  
      ,[EXISTING_NCD] = @EXISTING_NCD  
      ,[DEMERIT_DISCOUNT] = @DEMERIT_DISCOUNT  
      ,[CUSTOMER_AGENCY_ID] = @CUSTOMER_AGENCY_ID  
      ,[IS_ACTIVE] = @IS_ACTIVE  
      ,[CREATED_BY] = @CREATED_BY  
      ,[CREATED_DATETIME] = @CREATED_DATETIME  
      ,[MODIFIED_BY] = @MODIFIED_BY  
      ,[LAST_UPDATED_DATETIME] = @LAST_UPDATED_DATETIME  
 WHERE [CUSTOMER_ID] = @CUSTOMER_ID and  
      [QUOTE_ID] = @QUOTE_ID and  
      [ID] = @ID  
  
RETURN 1  
  
END  
  
END  