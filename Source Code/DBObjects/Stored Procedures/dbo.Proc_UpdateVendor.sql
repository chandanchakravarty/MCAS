IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateVendor]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateVendor]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                      
Proc Name       : dbo.Proc_UpdateVendor                      
Created by      : Anshuman                      
Date            : July 20, 2005                      
Purpose  : To update records in Vendor_list table.                      
Revison History :                      
Modified by      : Pravesh Chandel                      
Date            : 24/10/2006                      
Purpose       : add new Columns in tables and update values in table.                      
                    
Used In  : Wolverine                      
------------------------------------------------------------                      
Date     Review By          Comments                      
------   ------------       -------------------------*/                      
--drop proc Proc_UpdateVendor                       
CREATE procedure [dbo].[Proc_UpdateVendor]                       
(                      
 @VENDOR_ID   int,                      
 @VENDOR_CODE  nvarchar(12),                      
 @VENDOR_FNAME  nvarchar(130),                      
 @VENDOR_LNAME  nvarchar(30),                      
 @VENDOR_ADD1  nvarchar(140),                      
 @VENDOR_ADD2  nvarchar(140),                      
 @VENDOR_CITY  nvarchar(80),                      
 @VENDOR_COUNTRY  nvarchar(10),                      
 @VENDOR_STATE  nvarchar(10),                      
 @VENDOR_ZIP  nvarchar(20),                      
 @VENDOR_PHONE  nvarchar(40),                      
 @VENDOR_EXT  nvarchar(20),                      
 @VENDOR_FAX  nvarchar(40),                      
 @VENDOR_MOBILE  nvarchar(40),                      
 @VENDOR_EMAIL  nvarchar(100),                      
 @VENDOR_SALUTATION nvarchar(50),                      
 @VENDOR_FEDERAL_NUM nvarchar(400),                      
 @VENDOR_NOTE  nvarchar(500),                      
 @VENDOR_ACC_NUMBER nvarchar(40),                      
 @MODIFIED_BY  int,                      
 @LAST_UPDATED_DATETIME datetime,                    
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
@MAIL_1099_ZIP     varchar(11),                    
@PROCESS_1099_OPT nvarchar(20),                  
@W9_FORM           nvarchar(20),              
@ALLOWS_EFT   int,              
@BANK_NAME   varchar(20),              
@BANK_BRANCH    varchar(20),              
@DFI_ACCOUNT_NUMBER   varchar(100),              
@ROUTING_NUMBER   varchar(100),              
@ACCOUNT_VERIFIED_DATE  datetime,              
@ACCOUNT_ISVERIFIED  nchar(1),              
@REASON    varchar(100),              
@ACCOUNT_TYPE varchar(4),        
@MAIL_1099_NAME varchar(75) =null ,        
@REVERIFIED_AC int = null ,  
@REQ_SPECIAL_HANDLING int = null,
--added by Chetna
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
 if not exists                      
 (                      
  select  VENDOR_CODE                      
  from   MNT_VENDOR_LIST                      
  where  VENDOR_CODE = @VENDOR_CODE AND                      
    VENDOR_ID <> @VENDOR_ID                      
 )                      
 begin                  
        
--Update Reverify flag on basis of chage        
DECLARE @NEW_ACCOUNT_ISVERIFIED INT        
DECLARE @OLD_DFI_ACCOUNT_NUMBER varchar(20)        
DECLARE @OLD_ROUTING_NUMBER  varchar(20)        
        
SELECT          
@OLD_DFI_ACCOUNT_NUMBER = DFI_ACCOUNT_NUMBER ,        
@OLD_ROUTING_NUMBER = ROUTING_NUMBER         
FROM MNT_VENDOR_LIST with(nolock)        
where  VENDOR_ID = @VENDOR_ID             
        
--compare DFI_ACCOUNT_NUMBER and ROUTING_NUMBER        
--..Any chnage in DFI and Routing Number set @NEW_ACCOUNT_ISVERIFIED to NO        
IF( (@OLD_DFI_ACCOUNT_NUMBER <> @DFI_ACCOUNT_NUMBER)        
OR (@OLD_ROUTING_NUMBER <> @ROUTING_NUMBER) )        
BEGIN        
 SET @NEW_ACCOUNT_ISVERIFIED = 10964 --NO        
 UPDATE MNT_VENDOR_LIST SET ACCOUNT_ISVERIFIED = @NEW_ACCOUNT_ISVERIFIED        
 WHERE VENDOR_ID = @VENDOR_ID          
END        
        
        
        
        
--        
            
  update  MNT_VENDOR_LIST                      
  set                  
  VENDOR_CODE  = @VENDOR_CODE,                      
  VENDOR_FNAME  = @VENDOR_FNAME,                      
  VENDOR_LNAME  = @VENDOR_LNAME,                      
  VENDOR_ADD1  = @VENDOR_ADD1,                      
  VENDOR_ADD2  = @VENDOR_ADD2,                      
  VENDOR_CITY  = @VENDOR_CITY,                      
  VENDOR_COUNTRY  = @VENDOR_COUNTRY,                      
  VENDOR_STATE  = @VENDOR_STATE,                      
  VENDOR_ZIP  = @VENDOR_ZIP,                      
  VENDOR_PHONE  = @VENDOR_PHONE,                      
  VENDOR_EXT  = @VENDOR_EXT,                      
  VENDOR_FAX  = @VENDOR_FAX,                      
  VENDOR_MOBILE  = @VENDOR_MOBILE,                      
  VENDOR_EMAIL  = @VENDOR_EMAIL,                      
  VENDOR_SALUTATION = @VENDOR_SALUTATION,                      
  VENDOR_FEDERAL_NUM = @VENDOR_FEDERAL_NUM,                      
  VENDOR_NOTE  = @VENDOR_NOTE,                      
  VENDOR_ACC_NUMBER = @VENDOR_ACC_NUMBER,                      
  MODIFIED_BY  = @MODIFIED_BY,                      
  LAST_UPDATED_DATETIME = @LAST_UPDATED_DATETIME,                                  
BUSI_OWNERNAME =@BUSI_OWNERNAME,                    
COMPANY_NAME =@COMPANY_NAME  ,                    
CHK_MAIL_ADD1 =@CHK_MAIL_ADD1 ,                    
CHK_MAIL_ADD2= @CHK_MAIL_ADD2 ,                    
CHK_MAIL_CITY = @CHK_MAIL_CITY,                    
CHK_MAIL_STATE= @CHK_MAIL_STATE ,                    
CHKCOUNTRY  =@CHKCOUNTRY    ,                    
CHK_MAIL_ZIP =  @CHK_MAIL_ZIP ,                    
MAIL_1099_ADD1= @MAIL_1099_ADD1 ,                    
MAIL_1099_ADD2 =@MAIL_1099_ADD2 ,                    
MAIL_1099_CITY  =@MAIL_1099_CITY,                    
MAIL_1099_STATE =@MAIL_1099_STATE,                    
MAIL_1099_COUNTRY= @MAIL_1099_COUNTRY ,                    
MAIL_1099_ZIP  =@MAIL_1099_ZIP,                       
PROCESS_1099_OPT =@PROCESS_1099_OPT,                  
W9_FORM          =@W9_FORM,                
ALLOWS_EFT=@ALLOWS_EFT,              
BANK_NAME=@BANK_NAME,              
BANK_BRANCH=@BANK_BRANCH,              
DFI_ACCOUNT_NUMBER=@DFI_ACCOUNT_NUMBER,              
ROUTING_NUMBER=@ROUTING_NUMBER,              
--ACCOUNT_VERIFIED_DATE=@ACCOUNT_VERIFIED_DATE,              
--ACCOUNT_ISVERIFIED=@NEW_ACCOUNT_ISVERIFIED,              
REASON=@REASON,              
ACCOUNT_TYPE=@ACCOUNT_TYPE,        
MAIL_1099_NAME=@MAIL_1099_NAME,        
REVERIFIED_AC = @REVERIFIED_AC ,  
REQ_SPECIAL_HANDLING = @REQ_SPECIAL_HANDLING, 
--added by Chetna        
SUSEP_NUM=@SUSEP_NUM ,
DATE_OF_BIRTH=@DATE_OF_BIRTH, 
  CPF=@CPF,
  REG_ID_ISSUE_DATE=@REG_ID_ISSUE_DATE,
  ACTIVITY=@ACTIVITY, 
  REG_ID_ISSUE=@REG_ID_ISSUE, 
  REGIONAL_IDENTIFICATION =@REGIONAL_IDENTIFICATION                   
                        
  where VENDOR_ID = @VENDOR_ID                      
 end                      
END                      
                      
                      
                    
                  
                
              
          
          
        
        
        
        
        
        
      
    
GO

