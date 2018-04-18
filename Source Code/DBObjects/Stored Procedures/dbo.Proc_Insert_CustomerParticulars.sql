 IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Insert_CustomerParticulars]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Insert_CustomerParticulars]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO


 
 /*----------------------------------------------------------    
Proc Name       : [Proc_Insert_CustomerParticulars]    
Created by      : Agniswar Das    
Date            : 7/17/2011    
Purpose			: Demo    
Revison History :    
Used In        : Singapore    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/   




CREATE PROC dbo.Proc_Insert_CustomerParticulars (      
   @ID int output      
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
           ,@LAST_UPDATED_DATETIME datetime = null  
           ,@DATE_OF_QUOTATION varchar(20) = null)      
AS      
      
BEGIN      
IF NOT EXISTS(select * from QQ_CUSTOMER_PARTICULAR where CUSTOMER_ID = @CUSTOMER_ID and QUOTE_ID = @QUOTE_ID)    
BEGIN    
INSERT INTO [dbo].[QQ_CUSTOMER_PARTICULAR]      
           ([CUSTOMER_ID]      
           ,[QUOTE_ID]    
           ,[CUSTOMER_CODE]      
           ,[CUSTOMER_TYPE]      
           ,[CUSTOMER_FIRST_NAME]      
           ,[CUSTOMER_MIDDLE_NAME]      
           ,[CUSTOMER_LAST_NAME]      
           ,[DATE_OF_BIRTH]      
           ,[GENDER]      
           ,[NATIONALITY]      
           ,[IS_HOME_EMPLOYEE]      
           ,[CUSTOMER_OCCU]      
           ,[DRIVER_EXP_YEAR]      
           ,[ANY_CLAIM]      
           ,[EXIST_NCD_LESS_10]      
           ,[EXISTING_NCD]      
           ,[DEMERIT_DISCOUNT]      
           ,[CUSTOMER_AGENCY_ID]      
           ,[IS_ACTIVE]      
           ,[CREATED_BY]      
           ,[CREATED_DATETIME]      
           ,[MODIFIED_BY]      
           ,[LAST_UPDATED_DATETIME]  
           ,[DATE_OF_QUOTATION])      
     VALUES      
           (@CUSTOMER_ID     
           ,@QUOTE_ID     
           ,@CUSTOMER_CODE      
           ,@CUSTOMER_TYPE      
           ,@CUSTOMER_FIRST_NAME      
           ,@CUSTOMER_MIDDLE_NAME      
           ,@CUSTOMER_LAST_NAME      
           ,@DATE_OF_BIRTH      
           ,@GENDER      
           ,@NATIONALITY      
           ,@IS_HOME_EMPLOYEE      
           ,@CUSTOMER_OCCU      
           ,@DRIVER_EXP_YEAR      
           ,@ANY_CLAIM      
           ,@EXIST_NCD_LESS_10      
           ,@EXISTING_NCD      
           ,@DEMERIT_DISCOUNT      
           ,@CUSTOMER_AGENCY_ID      
           ,@IS_ACTIVE      
           ,@CREATED_BY      
           ,@CREATED_DATETIME      
           ,@MODIFIED_BY      
           ,@LAST_UPDATED_DATETIME  
           ,@DATE_OF_QUOTATION)      
                 
 SELECT @ID=@@IDENTITY      
     
 RETURN 1    
      
END    
    
    
    
    
END      