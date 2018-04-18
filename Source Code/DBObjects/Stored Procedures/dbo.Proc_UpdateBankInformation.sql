IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateBankInformation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateBankInformation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 --SP_HELPTEXT Proc_UpdateBankInformation  
 /*----------------------------------------------------------              
Proc Name       : dbo.BankInformation              
Created by      : Ajit Singh Chahal              
Date            : 5/25/2005              
Purpose       :To Update records in bank information table.              
Revison History :              
Used In        : Wolverine              
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       -------------------------*/           
--drop proc dbo.Proc_UpdateBankInformation      
             
CREATE PROC [dbo].[Proc_UpdateBankInformation]              
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
@COMPANY_ID VARCHAR (10)   ,  
--ADD FOR BANK INFORMATION  
@BANK_ID INT= null,  
@NUMBER NVARCHAR(20)= null,  
@REGISTERED INT= null,  
@STARTING_OUR_NUMBER NVARCHAR(15)= null,  
@ENDING_OUR_NUMBER NVARCHAR(15) =null,
@ACCOUNT_TYPE INT,  
 @BRANCH_NUMBER NVARCHAR(40)= null,  
 @AGREEMENT_NUMBER NVARCHAR(20)=NULL ,
 @ADD_NUMBER NVARCHAR(20) = NULL,
 @BANK_TYPE INT 
                
)              
AS              
BEGIN    
   IF  EXISTS(SELECT BANK_NUMBER FROM ACT_BANK_INFORMATION WITH(NOLOCK) WHERE BANK_NUMBER =@BANK_NUMBER AND BANK_ID<>@BANK_ID)
	BEGIN   
	  RETURN -10
	END 
   ELSE
   BEGIN 	 
		IF (@BANK_ID IS NOT NULL AND @BANK_ID !=0)  
  
		BEGIN  
		 UPDATE ACT_BANK_INFORMATION              
		 SET              
		 BANK_NAME= @BANK_NAME,              
		 BANK_ADDRESS1= @BANK_ADDRESS1,              
		 BANK_ADDRESS2= @BANK_ADDRESS2,              
		 BANK_CITY= @BANK_CITY,              
		 BANK_COUNTRY= @BANK_COUNTRY,              
		 BANK_STATE= @BANK_STATE,              
		 BANK_ZIP= @BANK_ZIP,              
		 BANK_ACC_TITLE= @BANK_ACC_TITLE,              
		 BANK_NUMBER= @BANK_NUMBER,              
		 STARTING_DEPOSIT_NUMBER= @STARTING_DEPOSIT_NUMBER,              
		 IS_CHECK_ISSUED= @IS_CHECK_ISSUED,              
		 MODIFIED_BY= @MODIFIED_BY,              
		 LAST_UPDATED_DATETIME =@LAST_UPDATED_DATETIME,          
		 START_CHECK_NUMBER =@START_CHECK_NUMBER,          
		 END_CHECK_NUMBER =@END_CHECK_NUMBER,             
		 ROUTE_POSITION_CODE1 =@ROUTE_POSITION_CODE1,             
		 ROUTE_POSITION_CODE2 =@ROUTE_POSITION_CODE2,             
		 ROUTE_POSITION_CODE3 =@ROUTE_POSITION_CODE3,             
		 ROUTE_POSITION_CODE4 =@ROUTE_POSITION_CODE4,             
		 BANK_MICR_CODE =@BANK_MICR_CODE,        
		 SIGN_FILE_1 = @SIGN_FILE_1,        
		 SIGN_FILE_2 = @SIGN_FILE_2,    
		 TRANSIT_ROUTING_NUMBER = @TRANSIT_ROUTING_NUMBER,     
		 COMPANY_ID=@COMPANY_ID,  
		 ACCOUNT_TYPE=@ACCOUNT_TYPE,  
		 NUMBER = @NUMBER,  
		 REGISTERED = @REGISTERED,  
		 STARTING_OUR_NUMBER = @STARTING_OUR_NUMBER,  
		 ENDING_OUR_NUMBER = @ENDING_OUR_NUMBER , 
		  BRANCH_NUMBER=@BRANCH_NUMBER,
		  AGREEMENT_NUMBER=@AGREEMENT_NUMBER,
		  ADD_NUMBER = @ADD_NUMBER,
		  BANK_TYPE = @BANK_TYPE
		   
		 where BANK_ID=@BANK_ID  
		  
		end  
		else  
		 begin       
			update ACT_BANK_INFORMATION              
		 set              
		 BANK_NAME= @BANK_NAME,              
		 BANK_ADDRESS1= @BANK_ADDRESS1,              
		 BANK_ADDRESS2= @BANK_ADDRESS2,              
		 BANK_CITY= @BANK_CITY,              
		 BANK_COUNTRY= @BANK_COUNTRY,              
		 BANK_STATE= @BANK_STATE,              
		 BANK_ZIP= @BANK_ZIP,              
		 BANK_ACC_TITLE= @BANK_ACC_TITLE,              
		 BANK_NUMBER= @BANK_NUMBER,              
		 STARTING_DEPOSIT_NUMBER= @STARTING_DEPOSIT_NUMBER,              
		 IS_CHECK_ISSUED= @IS_CHECK_ISSUED,              
		 MODIFIED_BY= @MODIFIED_BY,              
		 LAST_UPDATED_DATETIME =@LAST_UPDATED_DATETIME,          
		 START_CHECK_NUMBER =@START_CHECK_NUMBER,          
		 END_CHECK_NUMBER =@END_CHECK_NUMBER,             
		 ROUTE_POSITION_CODE1 =@ROUTE_POSITION_CODE1,             
		 ROUTE_POSITION_CODE2 =@ROUTE_POSITION_CODE2,             
		 ROUTE_POSITION_CODE3 =@ROUTE_POSITION_CODE3,             
		 ROUTE_POSITION_CODE4 =@ROUTE_POSITION_CODE4,             
		 BANK_MICR_CODE =@BANK_MICR_CODE,        
		 SIGN_FILE_1 = @SIGN_FILE_1,        
		 SIGN_FILE_2 = @SIGN_FILE_2,    
		 TRANSIT_ROUTING_NUMBER = @TRANSIT_ROUTING_NUMBER,     
		 COMPANY_ID=@COMPANY_ID,
		 ACCOUNT_TYPE=@ACCOUNT_TYPE,  
		   
		 NUMBER = @NUMBER,  
		 REGISTERED = @REGISTERED,  
		 STARTING_OUR_NUMBER = @STARTING_OUR_NUMBER,  
		 ENDING_OUR_NUMBER = @ENDING_OUR_NUMBER,
		  BRANCH_NUMBER=@BRANCH_NUMBER,
		  AGREEMENT_NUMBER=@AGREEMENT_NUMBER,
		  BANK_TYPE = @BANK_TYPE 
		  
		 where GL_ID=@GL_ID and ACCOUNT_ID = @ACCOUNT_ID         
		 end  
		return 1
   END
END
 
       
         
              
              
              
            
          
        
      
    
GO

