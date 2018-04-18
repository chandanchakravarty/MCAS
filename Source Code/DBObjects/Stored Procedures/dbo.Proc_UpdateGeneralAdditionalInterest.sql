IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateGeneralAdditionalInterest]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateGeneralAdditionalInterest]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
Proc Name    : Proc_UpdateGeneralAdditionalInterest      
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
-- drop proc dbo.Proc_UpdateGeneralAdditionalInterest       
CREATE   PROC dbo.Proc_UpdateGeneralAdditionalInterest      
(      
 @CUSTOMER_ID      int,      
 @APP_ID   int,      
 @APP_VERSION_ID  smallint,      
 @HOLDER_ID  int,      
 @MEMO   NVARCHAR(1000),      
 @NATURE_OF_INTEREST NVARCHAR(60),      
 @RANK   smallint,  
 --@CERTIFICATE_REQUIRED NCHAR(1),      
 @LOAN_REF_NUMBER NVARCHAR(300),      
 @MODIFIED_BY      int,            
 @LAST_UPDATED_DATETIME  datetime,            
 @ADD_INT_ID Int,      
 @HOLDER_NAME nvarchar(280),      
 @HOLDER_ADD1 nvarchar(560),      
 @HOLDER_ADD2 nvarchar(280),      
 @HOLDER_CITY nvarchar(320),      
 @HOLDER_COUNTRY varchar(20),      
 @HOLDER_STATE nvarchar(40),      
 @HOLDER_ZIP varchar(11)                
)      
AS   
DECLARE @COUNT INT
 SELECT @COUNT = count(RANK)  from APP_GENERAL_HOLDER_INTEREST WHERE  CUSTOMER_ID = @CUSTOMER_ID AND                            
		  APP_ID = @APP_ID AND                            
		  APP_VERSION_ID = @APP_VERSION_ID                            
		  and RANK=@RANK AND ADD_INT_ID <> @ADD_INT_ID                    
     IF @COUNT>0                             
	  BEGIN 
	   RETURN -1                       
	  END                      
 ELSE
BEGIN      
	 IF ( @HOLDER_ID = 0 )      
	 BEGIN      
	  UPDATE APP_GENERAL_HOLDER_INTEREST      
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
	   HOLDER_ZIP = @HOLDER_ZIP      
	  WHERE CUSTOMER_ID = @CUSTOMER_ID and      
	   APP_ID = @APP_ID and      
	   APP_VERSION_ID = @APP_VERSION_ID and      
	   ADD_INT_ID = @ADD_INT_ID     
	 END      
	 ELSE      
	 BEGIN      
	        
	  UPDATE APP_GENERAL_HOLDER_INTEREST      
	  SET MEMO = @MEMO,      
	   NATURE_OF_INTEREST = @NATURE_OF_INTEREST,      
	   RANK = @RANK,      
	   --CERTIFICATE_REQUIRED=@CERTIFICATE_REQUIRED,      
	   LOAN_REF_NUMBER = @LOAN_REF_NUMBER,      
	   MODIFIED_BY = @MODIFIED_BY,       
	   LAST_UPDATED_DATETIME = @LAST_UPDATED_DATETIME      
	  WHERE CUSTOMER_ID = @CUSTOMER_ID and      
	   APP_ID = @APP_ID and      
	   APP_VERSION_ID = @APP_VERSION_ID and      
	   ADD_INT_ID = @ADD_INT_ID     
	        
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

