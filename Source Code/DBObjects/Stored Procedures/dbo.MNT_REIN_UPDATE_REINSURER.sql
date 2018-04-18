IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_REIN_UPDATE_REINSURER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MNT_REIN_UPDATE_REINSURER]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------                          
Proc Name       : dbo.[MNT_REIN_UPDATE_REINSURER]                          
Created by      : HARMANJEET SINGH                         
Date            :APRIL 21, 2007                         
Purpose         : To insert the data into Reinsurer table.                          
Revison History :                          
Used In         : Wolverine                   
                         
------------------------------------------------------------                          
Date     Review By          Comments                          
------   ------------       -------------------------*/           
--drop PROC [dbo].[MNT_REIN_UPDATE_REINSURER]                         
CREATE PROC [dbo].[MNT_REIN_UPDATE_REINSURER]                          
(                          
  @REIN_COMPANY_ID     INT ,                       
  @REIN_COMPANY_CODE     nvarchar(6) = NULL,                          
  @REIN_COMPANY_NAME     nvarchar(65) = NULL,            
  @REIN_COMPANY_TYPE     nvarchar(50),            
                        
  @REIN_COMPANY_ADD1     nvarchar(70),                          
  @REIN_COMPANY_ADD2    nvarchar(70),                          
  @REIN_COMPANY_CITY    nvarchar(40),                          
  @REIN_COMPANY_COUNTRY    nvarchar(50),                          
  @REIN_COMPANY_STATE    nvarchar(50),                          
  @REIN_COMPANY_ZIP   varchar(11),                          
  @REIN_COMPANY_PHONE    nvarchar(20),                          
  @REIN_COMPANY_EXT     nvarchar(20),                          
  @REIN_COMPANY_FAX    nvarchar(20),            
  @REIN_COMPANY_SPEED_DIAL    nvarchar(4),                          
                       
  @REIN_COMPANY_MOBILE    nvarchar(20),                          
  @REIN_COMPANY_EMAIL  nvarchar(50),                    
  @REIN_COMPANY_NOTE nvarchar(250),                    
  @REIN_COMPANY_ACC_NUMBER nvarchar(20),             
                   
            
  @M_REIN_COMPANY_ADD_1 nvarchar(70),                    
  @M_REIN_COMPANY_ADD_2 nvarchar(70),                    
  @M_REIN_COMPANY_CITY  nVARCHAR (40),                    
  @M_REIN_COMPANY_COUNTRY nVARCHAR(50),                    
  @M_REIN_COMPANY_STATE nvarchar(50),                        
  @M_REIN_COMPANY_ZIP VARCHAR(11),                    
  @M_REIN_COMPANY_PHONE NVARCHAR(20),                 
  @M_REIN_COMPANY_FAX nvarchar(20) ,                    
  @M_REIN_COMPANY_EXT nvarchar(20),          
                    
  @REIN_COMPANY_WEBSITE  nvarchar(100),                    
  @REIN_COMPANY_IS_BROKER nchar(1),                    
  @PRINCIPAL_CONTACT varchar(50),                    
  @OTHER_CONTACT varchar(50),                    
  @FEDERAL_ID varchar(100),                    
  @ROUTING_NUMBER varchar(20),                     
  @TERMINATION_DATE datetime,                  
  @TERMINATION_REASON varchar(250) ,          
  @DOMICILED_STATE NVARCHAR(25),          
  @NAIC_CODE NVARCHAR(50),          
  @AM_BEST_RATING NVARCHAR(5),          
  @EFFECTIVE_DATE DATETIME,          
  @COMMENTS NVARCHAR(255),          
  @MODIFIED_BY INT,          
  @LAST_UPDATED_DATETIME DATETIME,              
  --added By Chetna  
  @SUSEP_NUM NVARCHAR(20)=null,  
  @COM_TYPE varchar(20)=null,
  @DISTRICT nvarchar(50)=null,  
  @BANK_NUMBER nvarchar(20)=null,  
  @BANK_BRANCH_NUMBER nvarchar(40)=null,  
  @CARRIER_CNPJ nvarchar(40)=null,  
  @BANK_ACCOUNT_TYPE int=null  ,
  @PAYMENT_METHOD int =null,
   @AGENCY_CLASSIFICATION nvarchar(25) ,
  @RISK_CLASSIFICATION nvarchar(25)          
                    
)                          
AS                          
BEGIN                
                            
                 
                        
  UPDATE MNT_REIN_COMAPANY_LIST                          
  SET          
                         
              
                     
  REIN_COMAPANY_CODE= @REIN_COMPANY_CODE     ,                          
  REIN_COMAPANY_NAME= @REIN_COMPANY_NAME  ,            
  REIN_COMPANY_TYPE=  @REIN_COMPANY_TYPE  ,            
  REIN_COMAPANY_ADD1= @REIN_COMPANY_ADD1  ,                          
  REIN_COMAPANY_ADD2= @REIN_COMPANY_ADD2  ,                          
  REIN_COMAPANY_CITY= @REIN_COMPANY_CITY  ,                          
  REIN_COMAPANY_COUNTRY=@REIN_COMPANY_COUNTRY,                          
  REIN_COMAPANY_STATE= @REIN_COMPANY_STATE  ,                          
  REIN_COMAPANY_ZIP=               @REIN_COMPANY_ZIP   ,                          
  REIN_COMAPANY_PHONE=              @REIN_COMPANY_PHONE ,                          
  REIN_COMAPANY_EXT=              @REIN_COMPANY_EXT   ,                          
  REIN_COMAPANY_FAX=     @REIN_COMPANY_FAX   ,            
  REIN_COMPANY_SPEED_DIAL=                 @REIN_COMPANY_SPEED_DIAL ,                          
  REIN_COMAPANY_MOBILE=                 @REIN_COMPANY_MOBILE    ,                          
  REIN_COMAPANY_EMAIL=             @REIN_COMPANY_EMAIL  ,                    
  REIN_COMAPANY_NOTE=             @REIN_COMPANY_NOTE ,                    
  REIN_COMAPANY_ACC_NUMBER=              @REIN_COMPANY_ACC_NUMBER ,            
  M_REIN_COMPANY_ADD_1=         @M_REIN_COMPANY_ADD_1 ,                    
  M_RREIN_COMPANY_ADD_2=              @M_REIN_COMPANY_ADD_2 ,                    
  M_REIN_COMPANY_CITY=            @M_REIN_COMPANY_CITY  ,                    
  M_REIN_COMPANY_COUNTRY=              @M_REIN_COMPANY_COUNTRY ,                    
  M_REIN_COMPANY_STATE=            @M_REIN_COMPANY_STATE ,                        
  M_REIN_COMPANY_ZIP=            @M_REIN_COMPANY_ZIP ,                    
  M_REIN_COMPANY_PHONE=              @M_REIN_COMPANY_PHONE ,                 
  M_REIN_COMPANY_FAX=            @M_REIN_COMPANY_FAX  ,                    
  M_REIN_COMPANY_EXT =           @M_REIN_COMPANY_EXT ,          
  REIN_COMPANY_WEBSITE=            @REIN_COMPANY_WEBSITE ,                    
  REIN_COMPANY_IS_BROKER=              @REIN_COMPANY_IS_BROKER ,                    
  PRINCIPAL_CONTACT=         @PRINCIPAL_CONTACT ,                    
  OTHER_CONTACT=          @OTHER_CONTACT ,                    
  FEDERAL_ID=           @FEDERAL_ID,                    
  ROUTING_NUMBER=                @ROUTING_NUMBER ,                     
  TERMINATION_DATE=            @TERMINATION_DATE ,                  
  TERMINATION_REASON=       @TERMINATION_REASON  ,          
  DOMICILED_STATE =    @DOMICILED_STATE ,          
  NAIC_CODE=   @NAIC_CODE ,          
  AM_BEST_RATING=    @AM_BEST_RATING ,          
  EFFECTIVE_DATE =    @EFFECTIVE_DATE ,          
  COMMENTS      =    @COMMENTS,          
  MODIFIED_BY =@MODIFIED_BY,          
  LAST_UPDATED_DATETIME =@LAST_UPDATED_DATETIME,           
  --added By Chetna  
  SUSEP_NUM = @SUSEP_NUM ,  
  COM_TYPE=@COM_TYPE,
  DISTRICT=@DISTRICT,
  BANK_NUMBER=@BANK_NUMBER,
  BANK_BRANCH_NUMBER=@BANK_BRANCH_NUMBER,
  CARRIER_CNPJ=@CARRIER_CNPJ,
  BANK_ACCOUNT_TYPE=@BANK_ACCOUNT_TYPE,
  PAYMENT_METHOD=@PAYMENT_METHOD,
  AGENCY_CLASSIFICATION=@AGENCY_CLASSIFICATION,
  RISK_CLASSIFICATION=@RISK_CLASSIFICATION 
        
  WHERE REIN_COMAPANY_ID  = @REIN_COMPANY_ID           
          
END         
        
GO

