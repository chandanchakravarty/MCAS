IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateContactList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateContactList]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------                
Proc Name       : dbo.Proc_UpdateContactList                
Created by      : Mohit Agarwal/Manoj Rathore                
Date            : 5/7/2007                
Purpose         : To Upadte the data into Contact List table.                
Revison History :                
Used In         : Wolverine                
------------------------------------------------------------                
Date     Review By          Comments                
Drop proc Proc_UpdateContactList
------   ------------       -------------------------*/                
CREATE PROC [dbo].[Proc_UpdateContactList]                
(                
               
@CONTACT_CODE     nchar(20),    
@CONTACT_TYPE_ID     smallint,    
@CONTACT_SALUTATION     nvarchar(20),    
@CONTACT_POS     nvarchar(500),    
@INDIVIDUAL_CONTACT_ID     int,    
@CONTACT_FNAME     nvarchar(200),-- Changed by amit for the  Itrack  1561 (tfs bug#554)   
@CONTACT_MNAME     nvarchar(15),    
@CONTACT_LNAME     nvarchar(75),    
@CONTACT_ADD1     nvarchar(70),    
@CONTACT_ADD2     nvarchar(70),    
@CONTACT_CITY     nvarchar(20),    
@CONTACT_STATE     nvarchar(5),    
@CONTACT_ZIP     nvarchar(20),    
@CONTACT_COUNTRY     nvarchar(25),    
@CONTACT_BUSINESS_PHONE    nvarchar(40),    
@CONTACT_EXT     nvarchar(5),    
@CONTACT_FAX     nvarchar(40),    
@CONTACT_MOBILE     nvarchar(40),    
@CONTACT_EMAIL     nvarchar(50),    
@CONTACT_PAGER     nvarchar(20),    
@CONTACT_HOME_PHONE     nvarchar(20),    
@CONTACT_TOLL_FREE     nvarchar(20),    
@CONTACT_NOTE     nvarchar(256),    
@CONTACT_AGENCY_ID     int,    
@IS_ACTIVE     nchar(2),    
@MODIFIED_BY     int,    
@LAST_UPDATED_DATETIME     datetime,
@DATE_OF_BIRTH datetime,
@CPF_CNPJ NVARCHAR(40),
@REGIONAL_IDENTIFICATION NVARCHAR(40),
@REG_ID_ISSUE_DATE datetime,
@ACTIVITY INT,
@REG_ID_ISSUE NVARCHAR(40),
@NUMBER NVARCHAR(40),  
@DISTRICT NVARCHAR(40),  
@REGIONAL_ID_TYPE  NVARCHAR(100),
@NATIONALITY NVARCHAR(100),        
@CONTACT_ID int             
)                
AS                
BEGIN              
 /*Check for Unique Code of Department  */              
 BEGIN             
   UPDATE MNT_CONTACT_LIST                
     SET                  
CONTACT_CODE = @CONTACT_CODE,    
CONTACT_TYPE_ID = @CONTACT_TYPE_ID,    
CONTACT_SALUTATION = @CONTACT_SALUTATION,    
CONTACT_POS = @CONTACT_POS,    
INDIVIDUAL_CONTACT_ID = @INDIVIDUAL_CONTACT_ID,    
CONTACT_FNAME = @CONTACT_FNAME,    
CONTACT_MNAME = @CONTACT_MNAME,    
CONTACT_LNAME = @CONTACT_LNAME,    
CONTACT_ADD1 = @CONTACT_ADD1,    
CONTACT_ADD2 = @CONTACT_ADD2,    
CONTACT_CITY = @CONTACT_CITY,    
CONTACT_STATE = @CONTACT_STATE,    
CONTACT_ZIP = @CONTACT_ZIP,    
CONTACT_COUNTRY = @CONTACT_COUNTRY,    
CONTACT_BUSINESS_PHONE = @CONTACT_BUSINESS_PHONE,    
CONTACT_EXT = @CONTACT_EXT,    
CONTACT_FAX = @CONTACT_FAX,    
CONTACT_MOBILE = @CONTACT_MOBILE,    
CONTACT_EMAIL = @CONTACT_EMAIL,    
CONTACT_PAGER = @CONTACT_PAGER,    
CONTACT_HOME_PHONE = @CONTACT_HOME_PHONE,    
CONTACT_TOLL_FREE = @CONTACT_TOLL_FREE,    
CONTACT_NOTE = @CONTACT_NOTE,    
CONTACT_AGENCY_ID = @CONTACT_AGENCY_ID,    
IS_ACTIVE = @IS_ACTIVE,    
MODIFIED_BY = @MODIFIED_BY,    
LAST_UPDATED_DATETIME = @LAST_UPDATED_DATETIME,
DATE_OF_BIRTH=@DATE_OF_BIRTH,
CPF_CNPJ=@CPF_CNPJ,
REGIONAL_IDENTIFICATION=@REGIONAL_IDENTIFICATION,
REG_ID_ISSUE_DATE = @REG_ID_ISSUE_DATE ,
ACTIVITY=@ACTIVITY,
REG_ID_ISSUE=@REG_ID_ISSUE ,
NUMBER = @NUMBER,
DISTRICT = @DISTRICT,
REGIONAL_ID_TYPE = @REGIONAL_ID_TYPE,
NATIONALITY = @NATIONALITY      
  WHERE CONTACT_ID = @CONTACT_ID      
 END    
          
END       
  
  
  
  
  
GO


