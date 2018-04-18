IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateHomeOwnerAdditionalInterest]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateHomeOwnerAdditionalInterest]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                        
Proc Name    : Proc_UpdateHomeOwnerAdditionalInterest                    
Created by   : Vijay Joshi                    
Date         : 16 May, 2005                    
Purpose     : Insert the record into  HoemOwner Additiona Interest                    
Revison History :                    
Used In  :   Wolverine                           
                    
Modified By  : Anurag Verma                    
Modified On : 25/07/2005                    
Purpose  : Assigning HOLDER_ADD2 with @HOLDER_ADD2 parameter instead of @HOLDER_ADD1 parameter                    
    and putting @ character in front of field names to make them as paramater                    
Modified By     : Mohit Gupta                    
Modified On     : 05/10/2005                    
Purpose         : Commenting field Certificate Required                     
                    
 ------------------------------------------------------------                                    
Date     Review By          Comments                                  
                         
------   ------------       -------------------------*/                       
-- drop proc dbo.Proc_UpdateHomeOwnerAdditionalInterest                    
CREATE   PROC dbo.Proc_UpdateHomeOwnerAdditionalInterest                    
(                    
 @CUSTOMER_ID      int,                    
 @APP_ID   smallint,                    
 @APP_VERSION_ID  smallint,                    
 @HOLDER_ID  int,                    
 @DWELLING_ID  smallint,                    
 @MEMO   NVARCHAR(500),                    
 @NATURE_OF_INTEREST NVARCHAR(60),                    
 @RANK   smallint,                
 --@CERTIFICATE_REQUIRED NCHAR(1),                    
 @LOAN_REF_NUMBER NVARCHAR(150),                    
 @MODIFIED_BY      int,                          
 @LAST_UPDATED_DATETIME  datetime,                          
 @ADD_INT_ID Int,                    
 @HOLDER_NAME nvarchar(140),                    
 @HOLDER_ADD1 nvarchar(280),                    
 @HOLDER_ADD2 nvarchar(280),                    
 @HOLDER_CITY nvarchar(160),                    
 @HOLDER_COUNTRY nvarchar(20),                    
 @HOLDER_STATE nvarchar(20),                    
 @HOLDER_ZIP varchar(11),        
 @BILL_MORTAGAGEE smallint = null                              
)                    
AS                    
              
              
DECLARE @COUNT INT           
declare @YES_LOOKUP_ID smallint
DECLARE @TOTAL_YES_COUNT INT           
 DECLARE @HOLDER_ID_EXIST INT         
set @YES_LOOKUP_ID = 10963        
                                          
 SELECT @COUNT = count(RANK)  from APP_HOME_OWNER_ADD_INT WHERE  CUSTOMER_ID = @CUSTOMER_ID AND                                          
    APP_ID = @APP_ID AND                                          
    APP_VERSION_ID = @APP_VERSION_ID                                          
    and RANK=@RANK AND ADD_INT_ID <> @ADD_INT_ID           
            
                     
 IF @COUNT>0                                           
  BEGIN               
                                       
   RETURN -1                                     
  END                                    
 ELSE                  
  BEGIN  
-- Added by Swarup 
	IF @BILL_MORTAGAGEE = @YES_LOOKUP_ID
	UPDATE APP_HOME_OWNER_ADD_INT SET  BILL_MORTAGAGEE = 10964
	WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND                
   	DWELLING_ID = @DWELLING_ID      

SET @HOLDER_ID_EXIST=0
if ( @HOLDER_ID = 0 ) 
	begin
		SELECT  @HOLDER_ID_EXIST = HOLDER_ID FROM APP_HOME_OWNER_ADD_INT
			WHERE CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND                  
			DWELLING_ID = @DWELLING_ID AND ADD_INT_ID = @ADD_INT_ID  
		IF (ISNULL(@HOLDER_ID_EXIST,0)!=0)
			SET @HOLDER_ID=@HOLDER_ID_EXIST
	end
                  
  IF ( @HOLDER_ID = 0 )                    
  BEGIN                    
   UPDATE APP_HOME_OWNER_ADD_INT                    
   SET MEMO = @MEMO,                    
    NATURE_OF_INTEREST = @NATURE_OF_INTEREST,                    
    RANK = @RANK,                    
    --CERTIFICATE_REQUIRED=@CERTIFICATE_REQUIRED,                    
    LOAN_REF_NUMBER = @LOAN_REF_NUMBER,                    
    MODIFIED_BY = @MODIFIED_BY,                     
    LAST_UPDATED_DATETIME = @LAST_UPDATED_DATETIME,                    
    HOLDER_NAME = @HOLDER_NAME,                    
    HOLDER_ADD1 = @HOLDER_ADD1,                    
    HOLDER_ADD2 = @HOLDER_ADD2,                    
    HOLDER_CITY = @HOLDER_CITY,                    
    HOLDER_COUNTRY = @HOLDER_COUNTRY,                    
    HOLDER_STATE = @HOLDER_STATE,                    
    HOLDER_ZIP = @HOLDER_ZIP,  
     BILL_MORTAGAGEE=@BILL_MORTAGAGEE               
   WHERE CUSTOMER_ID = @CUSTOMER_ID and                    
    APP_ID = @APP_ID and                    
    APP_VERSION_ID = @APP_VERSION_ID and                    
    DWELLING_ID = @DWELLING_ID and            
    ADD_INT_ID  = @ADD_INT_ID        
	      
	 IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_ADD_INT  WHERE CUSTOMER_ID = @CUSTOMER_ID AND                    
	   APP_ID = @APP_ID AND    APP_VERSION_ID = @APP_VERSION_ID AND                    
	   DWELLING_ID = @DWELLING_ID AND   ADD_INT_ID  = @ADD_INT_ID  AND ISNULL(IS_ACTIVE,'N')='Y')    
	 BEGIN 
		
		set @TOTAL_YES_COUNT=0 --check if any yes add int to bill this to mortegagee if no Yes then update app list's dwellin id and add_int_id
		SELECT @TOTAL_YES_COUNT=COUNT(ADD_INT_ID) FROM APP_HOME_OWNER_ADD_INT  WHERE CUSTOMER_ID = @CUSTOMER_ID AND                    
						APP_ID = @APP_ID AND    APP_VERSION_ID = @APP_VERSION_ID 
						AND DWELLING_ID = @DWELLING_ID AND  BILL_MORTAGAGEE=@YES_LOOKUP_ID
		  IF (@BILL_MORTAGAGEE = @YES_LOOKUP_ID)        
				UPDATE APP_LIST SET DWELLING_ID=@DWELLING_ID,ADD_INT_ID=@ADD_INT_ID         
				WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID  
		  ELSE IF (@TOTAL_YES_COUNT=0)
				UPDATE APP_LIST SET DWELLING_ID=0,ADD_INT_ID=0         
				WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                      
	 END    
  END                    
  ELSE                    
  BEGIN                    
                       
   UPDATE APP_HOME_OWNER_ADD_INT                    
   SET MEMO = @MEMO,                    
    NATURE_OF_INTEREST = @NATURE_OF_INTEREST,                    
    RANK = @RANK,                    
    --CERTIFICATE_REQUIRED=@CERTIFICATE_REQUIRED,                    
    LOAN_REF_NUMBER = @LOAN_REF_NUMBER,                    
    MODIFIED_BY = @MODIFIED_BY,                     
    LAST_UPDATED_DATETIME = @LAST_UPDATED_DATETIME,  
	 BILL_MORTAGAGEE=@BILL_MORTAGAGEE         
      WHERE CUSTOMER_ID = @CUSTOMER_ID and                    
    APP_ID = @APP_ID and                    
    APP_VERSION_ID = @APP_VERSION_ID and                    
    HOLDER_ID = @HOLDER_ID and                
    DWELLING_ID = @DWELLING_ID AND            
    ADD_INT_ID  = @ADD_INT_ID   
           
	      
	 IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_ADD_INT  WHERE CUSTOMER_ID = @CUSTOMER_ID AND                    
	   APP_ID = @APP_ID AND    APP_VERSION_ID = @APP_VERSION_ID AND                    
	   DWELLING_ID = @DWELLING_ID AND   ADD_INT_ID  = @ADD_INT_ID  AND ISNULL(IS_ACTIVE,'N')='Y')    
	 BEGIN
		
		set @TOTAL_YES_COUNT=0 --check if any yes add int to bill this to mortegagee if no Yes then update app list's dwellin id and add_int_id
		SELECT @TOTAL_YES_COUNT=COUNT(ADD_INT_ID) FROM APP_HOME_OWNER_ADD_INT  WHERE CUSTOMER_ID = @CUSTOMER_ID AND                    
						APP_ID = @APP_ID AND    APP_VERSION_ID = @APP_VERSION_ID 
						AND DWELLING_ID = @DWELLING_ID AND  BILL_MORTAGAGEE=@YES_LOOKUP_ID
		IF (@BILL_MORTAGAGEE = @YES_LOOKUP_ID)        
				UPDATE APP_LIST SET DWELLING_ID=@DWELLING_ID,ADD_INT_ID=@ADD_INT_ID         
				WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID 
		 ELSE IF (@TOTAL_YES_COUNT=0)
				UPDATE APP_LIST SET DWELLING_ID=0,ADD_INT_ID=0         
				WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                            
	 END              
                       
   IF @@ERROR <> 0                    
   BEGIN                     
    RETURN -4                     
   END                    
                       
   UPDATE MNT_HOLDER_INTEREST_LIST                    
    SET HOLDER_ADD1 = @HOLDER_ADD1,                    
     HOLDER_ADD2 = @HOLDER_ADD2,                    
     HOLDER_CITY = @HOLDER_CITY,                    
     HOLDER_COUNTRY = @HOLDER_COUNTRY,                    
     HOLDER_STATE = @HOLDER_STATE,                    
     HOLDER_ZIP = @HOLDER_ZIP   
                   
    WHERE HOLDER_ID = @HOLDER_ID            
        
          
                
                   
  END                    
  END                    
               
            
          
        
    
  
  





GO

