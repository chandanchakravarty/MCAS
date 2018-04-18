IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateReinsurer]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateReinsurer]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------            
Proc Name      : dbo.Proc_UpdateReinsurer            
Created by     : Priya Arora          
Date           : Jan 07,2006       
Purpose         : To update the data into Reinsurer table.            
Revison History :            
Used In          : Wolverine            
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/            
CREATE PROC Dbo.Proc_UpdateReinsurer            
(            
 @REIN_COMAPANY_CODE     nvarchar(6),          
  @REIN_COMAPANY_NAME     nvarchar(65),          
  @REIN_COMAPANY_ADD1     nvarchar(70),          
  @REIN_COMAPANY_ADD2    nvarchar(70),          
  @REIN_COMAPANY_CITY    nvarchar(40),          
  @REIN_COMAPANY_COUNTRY    nvarchar(5),          
  @REIN_COMAPANY_STATE    nvarchar(5),          
  @REIN_COMAPANY_ZIP   varchar(11),          
  @REIN_COMAPANY_PHONE    nvarchar(20),          
  @REIN_COMAPANY_EXT     nvarchar(10),          
  @REIN_COMAPANY_FAX    nvarchar(20),          
  @REIN_COMAPANY_MOBILE    nvarchar(20),                   
  @REIN_COMAPANY_ID     INT,          
  @REIN_COMAPANY_EMAIL  nvarchar(50),    
  @REIN_COMAPANY_NOTE nvarchar(250),    
  @REIN_COMAPANY_ACC_NUMBER nvarchar(20),    
  @M_REIN_COMPANY_ADD_1 nvarchar(70),    
  @M_RREIN_COMPANY_ADD_2 nvarchar(70),    
  @M_REIN_COMPANY_CITY  nVARCHAR (40),    
  @M_REIN_COMPANY_COUNTRY nVARCHAR(5),    
  @M_REIN_COMPANY_STATE nvarchar(5),        
  @M_REIN_COMPANY_ZIP VARCHAR(11),    
  @M_REIN_COMPANY_PHONE NVARCHAR(20), 
  @M_REIN_COMPANY_FAX nvarchar(20) ,    
  @M_REIN_COMPANY_EXT nvarchar(5),    
  @REIN_COMPANY_WEBSITE  nvarchar(100),    
  @REIN_COMPANY_IS_BROKER nchar(1),    
  @PRINCIPAL_CONTACT varchar(50),    
  @OTHER_CONTACT varchar(50),    
  @FEDERAL_ID varchar(10),    
  @ROUTING_NUMBER varchar(20),     
  @TERMINATION_DATE datetime,  
  @TERMINATION_REASON varchar(250),
  @MODIFIED_BY int,
  @LAST_UPDATED_DATETIME datetime
)            
AS            
BEGIN          
 /*Check for Unique Code of reinsurer  */          
if exists (SELECT @REIN_COMAPANY_CODE FROM MNT_REIN_COMAPANY_LIST WHERE REIN_COMAPANY_CODE = @REIN_COMAPANY_CODE AND REIN_COMAPANY_ID<>@REIN_COMAPANY_ID)           
RETURN       
ELSE         
  BEGIN         
  UPDATE MNT_REIN_COMAPANY_LIST            
  SET       
  REIN_COMAPANY_CODE=@REIN_COMAPANY_CODE,    
  REIN_COMAPANY_NAME=@REIN_COMAPANY_NAME,     
  REIN_COMAPANY_ADD1=@REIN_COMAPANY_ADD1,          
  REIN_COMAPANY_ADD2=@REIN_COMAPANY_ADD2,     
  REIN_COMAPANY_CITY=@REIN_COMAPANY_CITY,      
  REIN_COMAPANY_COUNTRY=@REIN_COMAPANY_COUNTRY,          
  REIN_COMAPANY_STATE=@REIN_COMAPANY_STATE,        
  REIN_COMAPANY_ZIP=@REIN_COMAPANY_ZIP,       
  REIN_COMAPANY_PHONE=@REIN_COMAPANY_PHONE,          
  REIN_COMAPANY_EXT=@REIN_COMAPANY_EXT,      
  REIN_COMAPANY_FAX=@REIN_COMAPANY_FAX,        
  REIN_COMAPANY_MOBILE=@REIN_COMAPANY_MOBILE,              
  REIN_COMAPANY_EMAIL=@REIN_COMAPANY_EMAIL,    
  REIN_COMAPANY_NOTE=@REIN_COMAPANY_NOTE,    
  REIN_COMAPANY_ACC_NUMBER=@REIN_COMAPANY_ACC_NUMBER,     
  M_REIN_COMPANY_ADD_1=@M_REIN_COMPANY_ADD_1,   
  M_RREIN_COMPANY_ADD_2=@M_RREIN_COMPANY_ADD_2,    
  M_REIN_COMPANY_CITY=@M_REIN_COMPANY_CITY  ,
  M_REIN_COMPANY_COUNTRY=@M_REIN_COMPANY_COUNTRY  ,
  M_REIN_COMPANY_STATE=@M_REIN_COMPANY_STATE,  
  M_REIN_COMPANY_ZIP=@M_REIN_COMPANY_ZIP,  
  M_REIN_COMPANY_PHONE=@M_REIN_COMPANY_PHONE , 
  M_REIN_COMPANY_FAX=@M_REIN_COMPANY_FAX,  
  M_REIN_COMPANY_EXT=@M_REIN_COMPANY_EXT ,
  REIN_COMPANY_WEBSITE=@REIN_COMPANY_WEBSITE,
  REIN_COMPANY_IS_BROKER=@REIN_COMPANY_IS_BROKER,  
  PRINCIPAL_CONTACT=@PRINCIPAL_CONTACT ,   
  OTHER_CONTACT=@OTHER_CONTACT,    
  FEDERAL_ID=@FEDERAL_ID,    
  ROUTING_NUMBER=@ROUTING_NUMBER,      
  TERMINATION_DATE=@TERMINATION_DATE,  
  TERMINATION_REASON =@TERMINATION_REASON 
      
      
  WHERE      
  REIN_COMAPANY_ID=@REIN_COMAPANY_ID         
 END      
END    
    
  





GO

