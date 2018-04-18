  
  /*----------------------------------------------------------                
Proc Name       : dbo.BankInformation                
Created by      : Ajit Singh Chahal                
Date            : 5/25/2005                
Purpose       :To insert records in bank information table.                
Revison History :                
Used In        : Wolverine 

Modified On    : 27 January, 2012
Modified By     : Ruchika Chauhan
Reason         : Bank Code is not in use for Singapore implementation 
               
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                
-- DROP PROC dbo.Proc_InsertBankInformation         
      
             
CREATE PROC [dbo].[Proc_InsertBankInformation]                
(                
 @GL_ID     int=NULL,                
 @ACCOUNT_ID     int=NULL,                
 @BANK_NAME     varchar(100),                
 @BANK_ADDRESS1     nvarchar(140),                
 @BANK_ADDRESS2     nvarchar(140),                
 @BANK_CITY     nvarchar(80),                
 @BANK_COUNTRY     nvarchar(20),                
 @BANK_STATE     nvarchar(20),                
 @BANK_ZIP     nvarchar(20),                
 @BANK_ACC_TITLE     nvarchar(100),                
 @BANK_NUMBER     nvarchar(50),                
 @STARTING_DEPOSIT_NUMBER     int,                
 @IS_CHECK_ISSUED     nchar(2),                
 @IS_ACTIVE     nchar(2),                
 @CREATED_BY     int,                
 @CREATED_DATETIME     datetime,                
 @MODIFIED_BY     int,                
 @LAST_UPDATED_DATETIME     datetime,            
 @START_CHECK_NUMBER int,            
 @END_CHECK_NUMBER int,                
 @ROUTE_POSITION_CODE1 nvarchar(5),                
 @ROUTE_POSITION_CODE2 nvarchar(5),                
 @ROUTE_POSITION_CODE3 nvarchar(5),                
 @ROUTE_POSITION_CODE4 nvarchar(10),                
 @BANK_MICR_CODE nvarchar(19),          
 @SIGN_FILE_1 nvarchar(200),          
 @SIGN_FILE_2 nvarchar(200),        
 @TRANSIT_ROUTING_NUMBER VARCHAR(9),        
 @COMPANY_ID VARCHAR (10) ,      
 --ADD FOR BANK INFORMATION      
 @BANK_ID INT OUT,      
 @NUMBER NVARCHAR(20)= null,      
 @REGISTERED INT= null,      
 @STARTING_OUR_NUMBER NVARCHAR(15)= null,      
 @ENDING_OUR_NUMBER NVARCHAR(15)=NULL,    
 @ACCOUNT_TYPE INT ,    
    @BRANCH_NUMBER NVARCHAR(40)= null,      
 @AGREEMENT_NUMBER NVARCHAR(20)=NULL,  
 @ADD_NUMBER NVARCHAR(20) = NULL ,  
 @BANK_TYPE INT =NULL  
)               
AS                
      
BEGIN      
      
     SELECT BANK_NUMBER FROM ACT_BANK_INFORMATION WITH(NOLOCK) WHERE BANK_NUMBER ='011'    
 IF NOT EXISTS(SELECT BANK_NUMBER FROM ACT_BANK_INFORMATION WITH(NOLOCK) WHERE BANK_NUMBER =@BANK_NUMBER)    
 BEGIN           
select @BANK_ID=isnull(Max(BANK_ID),0)+1 from ACT_BANK_INFORMATION  WITH(NOLOCK) --WHERE BANK_ID = @BANK_ID       
                           
INSERT INTO ACT_BANK_INFORMATION                
(                
 GL_ID,                
 ACCOUNT_ID,                
 BANK_NAME,                
 BANK_ADDRESS1,                
 BANK_ADDRESS2,                
 BANK_CITY,                
 BANK_COUNTRY,                
 BANK_STATE,                
 BANK_ZIP,                
 BANK_ACC_TITLE,                
 BANK_NUMBER,                
 STARTING_DEPOSIT_NUMBER,                
 IS_CHECK_ISSUED,                
 IS_ACTIVE,                
 CREATED_BY,                
 CREATED_DATETIME,                
 MODIFIED_BY,                
 LAST_UPDATED_DATETIME,              
 START_CHECK_NUMBER,             
 END_CHECK_NUMBER,             
 ROUTE_POSITION_CODE1,               
 ROUTE_POSITION_CODE2,               
 ROUTE_POSITION_CODE3,               
 ROUTE_POSITION_CODE4,               
 BANK_MICR_CODE,          
 SIGN_FILE_1,          
 SIGN_FILE_2,        
 TRANSIT_ROUTING_NUMBER,        
 COMPANY_ID ,      
 BANK_ID ,      
 NUMBER ,      
 REGISTERED ,      
 STARTING_OUR_NUMBER ,      
 ENDING_OUR_NUMBER,    
 ACCOUNT_TYPE ,       
 BRANCH_NUMBER,    
 AGREEMENT_NUMBER,  
 ADD_NUMBER,  
 BANK_TYPE    
)                
VALUES                
(                
@GL_ID,         
@ACCOUNT_ID,                
@BANK_NAME,                
@BANK_ADDRESS1,                
@BANK_ADDRESS2,                
@BANK_CITY,                
@BANK_COUNTRY,                
@BANK_STATE,                
@BANK_ZIP,                
@BANK_ACC_TITLE,                
@BANK_NUMBER,                
@STARTING_DEPOSIT_NUMBER,                
@IS_CHECK_ISSUED,                
@IS_ACTIVE,                
@CREATED_BY,                
@CREATED_DATETIME,                
@MODIFIED_BY,                
@LAST_UPDATED_DATETIME,            
@START_CHECK_NUMBER,              
@END_CHECK_NUMBER,              
@ROUTE_POSITION_CODE1,               
@ROUTE_POSITION_CODE2,               
@ROUTE_POSITION_CODE3,               
@ROUTE_POSITION_CODE4,               
@BANK_MICR_CODE,          
@SIGN_FILE_1,          
@SIGN_FILE_2,        
@TRANSIT_ROUTING_NUMBER,        
@COMPANY_ID  ,      
@BANK_ID,      
--@CURR_MAX_BANK_ID,      
@NUMBER ,      
@REGISTERED ,      
@STARTING_OUR_NUMBER,      
@ENDING_OUR_NUMBER,    
@ACCOUNT_TYPE ,    
 @BRANCH_NUMBER,    
 @AGREEMENT_NUMBER,  
 @ADD_NUMBER,  
 @BANK_TYPE                
)               
      
return 1    
----return @CURR_MAX_BANK_ID      
 END    
   ELSE    
 RETURN -10    
      
END                
                
                
              
            
          
        
  --select * from ACT_BANK_INFORMATION order by bank_id desc    
    