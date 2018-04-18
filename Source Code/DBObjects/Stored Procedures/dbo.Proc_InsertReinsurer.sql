IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertReinsurer]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertReinsurer]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                
Proc Name       : dbo.Proc_InsertReinsurer                
Created by      : Priya Arora               
Date            : Jan 07,2006               
Purpose         : To insert the data into Reinsurer table.                
Revison History :                
Used In         : Wolverine         
               
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                
CREATE PROC Dbo.Proc_InsertReinsurer                
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
  @CREATED_BY     int,                
  @CREATED_DATETIME     datetime,                
  @REIN_COMAPANY_ID     INT output,                
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
  @TERMINATION_REASON varchar(250)        
      
          
)                
AS                
BEGIN      
if not exists (SELECT @REIN_COMAPANY_CODE FROM MNT_REIN_COMAPANY_LIST WHERE REIN_COMAPANY_CODE = @REIN_COMAPANY_CODE )  
begin                     
SELECT @REIN_COMAPANY_ID = isnull(Max(REIN_COMAPANY_ID),0)+1 FROM MNT_REIN_COMAPANY_LIST        
              
   INSERT INTO MNT_REIN_COMAPANY_LIST                
   (               
  REIN_COMAPANY_ID,    
  REIN_COMAPANY_CODE,          
  REIN_COMAPANY_NAME,           
  REIN_COMAPANY_ADD1,                
  REIN_COMAPANY_ADD2,           
  REIN_COMAPANY_CITY,            
  REIN_COMAPANY_COUNTRY,                
  REIN_COMAPANY_STATE,              
  REIN_COMAPANY_ZIP,             
  REIN_COMAPANY_PHONE,                
  REIN_COMAPANY_EXT,            
  REIN_COMAPANY_FAX,              
  REIN_COMAPANY_MOBILE,                
  CREATED_BY,           
  CREATED_DATETIME,          
  REIN_COMAPANY_EMAIL,          
  REIN_COMAPANY_NOTE,          
  REIN_COMAPANY_ACC_NUMBER,          
            
  IS_ACTIVE,       
  M_REIN_COMPANY_ADD_1,         
  M_RREIN_COMPANY_ADD_2,          
  M_REIN_COMPANY_CITY,          
  M_REIN_COMPANY_COUNTRY,          
  M_REIN_COMPANY_STATE,          
  M_REIN_COMPANY_ZIP,          
  M_REIN_COMPANY_PHONE,          
  M_REIN_COMPANY_FAX,          
  M_REIN_COMPANY_EXT ,       
  REIN_COMPANY_WEBSITE,        
  REIN_COMPANY_IS_BROKER,          
  PRINCIPAL_CONTACT,          
  OTHER_CONTACT,          
  FEDERAL_ID,          
  ROUTING_NUMBER,            
  TERMINATION_DATE,        
  TERMINATION_REASON        
                
   )                
   VALUES                
   (    
  @REIN_COMAPANY_ID,                
  @REIN_COMAPANY_CODE,            
  @REIN_COMAPANY_NAME,            
  @REIN_COMAPANY_ADD1 ,                
  @REIN_COMAPANY_ADD2,           
  @REIN_COMAPANY_CITY,            
  @REIN_COMAPANY_COUNTRY,                
  @REIN_COMAPANY_STATE,             
  @REIN_COMAPANY_ZIP,           
  @REIN_COMAPANY_PHONE,                
  @REIN_COMAPANY_EXT,            
  @REIN_COMAPANY_FAX,             
  @REIN_COMAPANY_MOBILE,                
  @CREATED_BY,           
  @CREATED_DATETIME,          
  @REIN_COMAPANY_EMAIL,          
  @REIN_COMAPANY_NOTE,          
    @REIN_COMAPANY_ACC_NUMBER,          
  'Y',          
  @M_REIN_COMPANY_ADD_1,          
  @M_RREIN_COMPANY_ADD_2,          
  @M_REIN_COMPANY_CITY,          
  @M_REIN_COMPANY_COUNTRY ,          
  @M_REIN_COMPANY_STATE,          
  @M_REIN_COMPANY_ZIP,          
  @M_REIN_COMPANY_PHONE  ,           
  @M_REIN_COMPANY_FAX,          
  @M_REIN_COMPANY_EXT,          
  @REIN_COMPANY_WEBSITE,          
  @REIN_COMPANY_IS_BROKER,          
  @PRINCIPAL_CONTACT,          
  @OTHER_CONTACT,          
  @FEDERAL_ID ,          
  @ROUTING_NUMBER,           
  @TERMINATION_DATE,        
  @TERMINATION_REASON           
   )    
  return 1            
        
     END
else
 return -1              
    
END  



GO

