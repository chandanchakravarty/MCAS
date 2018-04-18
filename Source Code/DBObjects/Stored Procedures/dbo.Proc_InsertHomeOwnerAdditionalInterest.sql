IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertHomeOwnerAdditionalInterest]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertHomeOwnerAdditionalInterest]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- drop proc dbo.Proc_InsertHomeOwnerAdditionalInterest                
CREATE  PROC dbo.Proc_InsertHomeOwnerAdditionalInterest                
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
	 @CREATED_BY      int,                      
	 @CREATED_DATETIME      datetime,                
	 @HOLDER_NAME nvarchar(140),                
	 @HOLDER_ADD1 nvarchar(280),                
	 @HOLDER_ADD2 nvarchar(280),                
	 @HOLDER_CITY nvarchar(160),                
	 @HOLDER_COUNTRY nvarchar(20),                
	 @HOLDER_STATE nvarchar(20),                
	 @HOLDER_ZIP varchar(11),            
	 @IS_ACTIVE nchar(2)='Y',      
	 @BILL_MORTAGAGEE smallint = null               
                     
)                
AS           
                
DECLARE @MAX_ADD_INT int, @COUNT INT      
DECLARE @YES_LOOKUP_ID smallint      
               
                
BEGIN          
	SET @YES_LOOKUP_ID = 10963            
        
	SELECT @COUNT = COUNT(RANK) FROM APP_HOME_OWNER_ADD_INT         
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID         
	AND APP_VERSION_ID = @APP_VERSION_ID AND RANK = @RANK                
 IF (@COUNT >0 )        
 BEGIN        
 RETURN -1        
 END        
        
 ELSE        
        
 BEGIN         
	SELECT @MAX_ADD_INT = ISNULL(MAX(ADD_INT_ID),0) + 1                
	FROM APP_HOME_OWNER_ADD_INT  with (NOLOCK)              
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND                
	APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID AND  DWELLING_ID = @DWELLING_ID                
 -- Added by Swarup 
	IF @BILL_MORTAGAGEE = @YES_LOOKUP_ID
	UPDATE APP_HOME_OWNER_ADD_INT SET  BILL_MORTAGAGEE = 10964
	WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND                
   	DWELLING_ID = @DWELLING_ID                         
         
  IF ( @HOLDER_ID = 0 )                
  BEGIN                
	   INSERT INTO APP_HOME_OWNER_ADD_INT                
	   (                
		CUSTOMER_ID,                 
		APP_ID,                 
		APP_VERSION_ID,                
		DWELLING_ID,                 
		MEMO,                
		NATURE_OF_INTEREST,                 
		RANK,                
		--CERTIFICATE_REQUIRED,                
		LOAN_REF_NUMBER,                
		CREATED_BY,                
		CREATED_DATETIME,                
		HOLDER_NAME,                
		HOLDER_ADD1,                
		HOLDER_ADD2,                
		HOLDER_CITY,                
		HOLDER_COUNTRY,                
		HOLDER_STATE,                
		HOLDER_ZIP,                
		ADD_INT_ID  ,              
		IS_ACTIVE ,  
		BILL_MORTAGAGEE             
	   )                
	   VALUES                
	   (                
		@CUSTOMER_ID,                
		@APP_ID,                
		@APP_VERSION_ID,                
		@DWELLING_ID,                 
		@MEMO,                 
		@NATURE_OF_INTEREST,                
		@RANK,                
		--@CERTIFICATE_REQUIRED,                
		@LOAN_REF_NUMBER,                
		@CREATED_BY,                 
		@CREATED_DATETIME,                
		@HOLDER_NAME,                
		@HOLDER_ADD1,                
		@HOLDER_ADD2,                
		@HOLDER_CITY,                
		@HOLDER_COUNTRY,                
		@HOLDER_STATE,                
		@HOLDER_ZIP,                
		@MAX_ADD_INT  ,              
		@IS_ACTIVE,  
		@BILL_MORTAGAGEE      
	   )     
	IF (@BILL_MORTAGAGEE = @YES_LOOKUP_ID)      
	UPDATE APP_LIST SET DWELLING_ID=@DWELLING_ID,ADD_INT_ID=@MAX_ADD_INT       
	WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                 
  END                
  ELSE                
	  BEGIN                
	   INSERT INTO APP_HOME_OWNER_ADD_INT                
	   (                
		CUSTOMER_ID,                 
		APP_ID,                 
		APP_VERSION_ID,     
		HOLDER_ID,                
		DWELLING_ID,                 
		MEMO,                
		NATURE_OF_INTEREST,                 
		RANK,
		----
		HOLDER_NAME,                
		HOLDER_ADD1,                
		HOLDER_ADD2,                
		HOLDER_CITY,                
		HOLDER_COUNTRY,                
		HOLDER_STATE,                
		HOLDER_ZIP,                 
		--CERTIFICATE_REQUIRED,                
		LOAN_REF_NUMBER,                
		CREATED_BY,                
		CREATED_DATETIME,                
		ADD_INT_ID,          
		IS_ACTIVE,  
		BILL_MORTAGAGEE                 
	   )                
	   VALUES                
	   (                
		@CUSTOMER_ID,                
		@APP_ID,                
		@APP_VERSION_ID,                
		@HOLDER_ID,                
		@DWELLING_ID,                 
		@MEMO,                 
		@NATURE_OF_INTEREST,                
		@RANK,
		--===========
		@HOLDER_NAME,                
		@HOLDER_ADD1,                
		@HOLDER_ADD2,                
		@HOLDER_CITY,                
		@HOLDER_COUNTRY,                
		@HOLDER_STATE,                
		@HOLDER_ZIP,                
		--@CERTIFICATE_REQUIRED,                
		@LOAN_REF_NUMBER,                
		@CREATED_BY,                 
		@CREATED_DATETIME,                
		@MAX_ADD_INT,          
		@IS_ACTIVE,  
		@BILL_MORTAGAGEE                 
	   )       
    
		IF (@BILL_MORTAGAGEE = @YES_LOOKUP_ID)      
		UPDATE APP_LIST SET DWELLING_ID=@DWELLING_ID,ADD_INT_ID=@MAX_ADD_INT       
		WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                 
		     
		           
		IF @@ERROR <> 0                 
		BEGIN                
		RETURN -2                
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
                  
  RETURN  @MAX_ADD_INT                 
                 
 END                
        
 END      
    
  



GO

