IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertVendor]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertVendor]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                        
Proc Name       : dbo.Vendor                        
Created by      : Ajit Singh Chahal                        
Date            : 4/7/2005                        
Purpose       :To insert records in Vendor_list table.                        
Revison History :                        
Modified by      : Pravesh Chandel                        
Date            : 24/10/2006                        
Purpose       : add new Columns in tables and insert values in table.                        
                      
Used In        : Wolverine                        
------------------------------------------------------------                        
Date     Review By          Comments                        
------   ------------       -------------------------*/                        
--drop proc Dbo.Proc_InsertVendor                        
CREATE PROC [dbo].[Proc_InsertVendor]                        
(                        
@VENDOR_ID int output,                        
@VENDOR_CODE     nvarchar(12),                        
@VENDOR_FNAME     nvarchar(130),                        
@VENDOR_LNAME     nvarchar(30),                        
@VENDOR_ADD1     nvarchar(140),                        
@VENDOR_ADD2     nvarchar(140),                        
@VENDOR_CITY     nvarchar(80),                        
@VENDOR_COUNTRY     nvarchar(10),                        
@VENDOR_STATE     nvarchar(10),                        
@VENDOR_ZIP     nvarchar(20),                        
@VENDOR_PHONE     nvarchar(40),                        
@VENDOR_EXT     nvarchar(20),                        
@VENDOR_FAX     nvarchar(40),                        
@VENDOR_MOBILE     nvarchar(40),                        
@VENDOR_EMAIL     nvarchar(100),                        
@VENDOR_SALUTATION     nvarchar(50),                        
@VENDOR_FEDERAL_NUM     nvarchar(400),                        
@VENDOR_NOTE     nvarchar(500),                        
@VENDOR_ACC_NUMBER     nvarchar(500),                        
@IS_ACTIVE     nchar(2),                        
@CREATED_BY     int,                        
@CREATED_DATETIME     datetime,                        
@MODIFIED_BY     int,                        
@LAST_UPDATED_DATETIME     datetime,                      
@BUSI_OWNERNAME  nvarchar(70),                      
@COMPANY_NAME   nvarchar(70),                      
@CHK_MAIL_ADD1  nvarchar(280),                      
@CHK_MAIL_ADD2  nvarchar(280),                      
@CHK_MAIL_CITY  nvarchar(160),                      
@CHK_MAIL_STATE nvarchar(20),                      
@CHKCOUNTRY     nvarchar(20),                      
@CHK_MAIL_ZIP   varchar(11),                      
@MAIL_1099_ADD1  nvarchar(280),                      
@MAIL_1099_ADD2  nvarchar(280),                      
@MAIL_1099_CITY  nvarchar(160),                      
@MAIL_1099_STATE nvarchar(20),                      
@MAIL_1099_COUNTRY nvarchar(20),                      
@MAIL_1099_ZIP     varchar(11) ,                     
@PROCESS_1099_OPT nvarchar(20),                    
@W9_FORM           nvarchar(20),              
@ALLOWS_EFT int,              
@BANK_NAME  varchar(20),              
@BANK_BRANCH  varchar(20),              
@DFI_ACCOUNT_NUMBER  varchar(100),              
@ROUTING_NUMBER  varchar(100),              
@ACCOUNT_VERIFIED_DATE datetime,              
@ACCOUNT_ISVERIFIED char(1),              
@REASON varchar(100),            
@ACCOUNT_TYPE varchar(4),        
@MAIL_1099_NAME varchar(75) =null,        
@REVERIFIED_AC int = null ,  
@REQ_SPECIAL_HANDLING int = null ,
@SUSEP_NUM varchar(20)=null,
@REGIONAL_IDENTIFICATION NVARCHAR(40),
 @DATE_OF_BIRTH DATETIME,
 @CPF NVARCHAR(40),
 @REG_ID_ISSUE_DATE DATETIME,
 @ACTIVITY INT,
 @REG_ID_ISSUE NVARCHAR(40)         
)                        
AS                        
BEGIN                        
Declare @Count int                        
Set @Count= (SELECT count(*) FROM MNT_VENDOR_LIST WHERE VENDOR_CODE =@VENDOR_CODE)                        
                        
if (@Count=0)                        
BEGIN                        
 select  @VENDOR_ID=isnull(Max(VENDOR_ID),0)+1 from MNT_VENDOR_LIST                         INSERT INTO MNT_VENDOR_LIST                        
 (                        
 VENDOR_ID,                        
 VENDOR_CODE,                        
 VENDOR_FNAME,                        
 VENDOR_LNAME,                        
 VENDOR_ADD1,                        
 VENDOR_ADD2,                        
 VENDOR_CITY,                        
 VENDOR_COUNTRY,                        
 VENDOR_STATE,                        
 VENDOR_ZIP,                        
 VENDOR_PHONE,                        
 VENDOR_EXT,                        
 VENDOR_FAX,                        
 VENDOR_MOBILE,             
 VENDOR_EMAIL,                  
 VENDOR_SALUTATION,                        
 VENDOR_FEDERAL_NUM,                        
 VENDOR_NOTE,                        
 VENDOR_ACC_NUMBER,                        
 IS_ACTIVE,                        
 CREATED_BY,                        
 CREATED_DATETIME,                        
 MODIFIED_BY,                        
 LAST_UPDATED_DATETIME,                      
                       
 BUSI_OWNERNAME  ,                      
 COMPANY_NAME   ,                      
 CHK_MAIL_ADD1  ,                      
 CHK_MAIL_ADD2  ,                      
 CHK_MAIL_CITY  ,                      
 CHK_MAIL_STATE ,                      
 CHKCOUNTRY     ,                      
 CHK_MAIL_ZIP   ,                      
 MAIL_1099_ADD1 ,                      
 MAIL_1099_ADD2 ,                      
 MAIL_1099_CITY ,                      
 MAIL_1099_STATE ,                
 MAIL_1099_COUNTRY,                       
 MAIL_1099_ZIP,                    
 PROCESS_1099_OPT ,                    
 W9_FORM ,              
ALLOWS_EFT,              
BANK_NAME,              
BANK_BRANCH,              
DFI_ACCOUNT_NUMBER,              
ROUTING_NUMBER,              
--ACCOUNT_VERIFIED_DATE,              
--ACCOUNT_ISVERIFIED,              
REASON,            
ACCOUNT_TYPE,              
MAIL_1099_NAME ,        
REVERIFIED_AC ,  
REQ_SPECIAL_HANDLING ,
SUSEP_NUM,
REGIONAL_IDENTIFICATION,
DATE_OF_BIRTH,
CPF ,
REG_ID_ISSUE_DATE,
ACTIVITY,
REG_ID_ISSUE                                     
 )                        
 VALUES                        
 (                        
 @VENDOR_ID,                        
 @VENDOR_CODE,                        
 @VENDOR_FNAME,                        
 @VENDOR_LNAME,                        
 @VENDOR_ADD1,                        
 @VENDOR_ADD2,                        
 @VENDOR_CITY,                        
 @VENDOR_COUNTRY,                        
 @VENDOR_STATE,                        
 @VENDOR_ZIP,                        
 @VENDOR_PHONE,                        
 @VENDOR_EXT,                        
 @VENDOR_FAX,                        
 @VENDOR_MOBILE,                        
 @VENDOR_EMAIL,                        
 @VENDOR_SALUTATION,                        
 @VENDOR_FEDERAL_NUM,                        
 @VENDOR_NOTE,                        
 @VENDOR_ACC_NUMBER,                        
 @IS_ACTIVE,                        
 @CREATED_BY,                        
 @CREATED_DATETIME,                        
 @MODIFIED_BY,                        
 @LAST_UPDATED_DATETIME ,                       
                       
 @BUSI_OWNERNAME  ,                      
 @COMPANY_NAME  ,                      
 @CHK_MAIL_ADD1 ,                      
 @CHK_MAIL_ADD2 ,                      
 @CHK_MAIL_CITY ,                      
 @CHK_MAIL_STATE,                      
 @CHKCOUNTRY    ,                      
 @CHK_MAIL_ZIP  ,                      
 @MAIL_1099_ADD1,                      
 @MAIL_1099_ADD2,                      
 @MAIL_1099_CITY ,                      
 @MAIL_1099_STATE ,                      
 @MAIL_1099_COUNTRY ,                      
 @MAIL_1099_ZIP,                           
 @PROCESS_1099_OPT ,                    
 @W9_FORM,              
@ALLOWS_EFT,              
@BANK_NAME,              
@BANK_BRANCH,              
@DFI_ACCOUNT_NUMBER,              
@ROUTING_NUMBER,              
--@ACCOUNT_VERIFIED_DATE,              
--@ACCOUNT_ISVERIFIED,              
@REASON,            
@ACCOUNT_TYPE,              
@MAIL_1099_NAME,        
@REVERIFIED_AC ,   
@REQ_SPECIAL_HANDLING ,
@SUSEP_NUM,
@REGIONAL_IDENTIFICATION ,
 @DATE_OF_BIRTH,
 @CPF ,
 @REG_ID_ISSUE_DATE,
 @ACTIVITY,
 @REG_ID_ISSUE         
 )   
END                        
ELSE                 
BEGIN                        
/*Record already exist*/                        
  SELECT @VENDOR_ID = -1                        
                        
END                        
END                        
                        
                
                
            
          
          
        
        
        
        
        
        
        
      
GO

