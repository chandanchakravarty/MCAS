IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateTRAILERAdditionalInterest]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateTRAILERAdditionalInterest]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
Proc Name    : Proc_UpdateTrailerAdditionalInterest  
Created by   : Anurag Verma  
Date         : 23 May, 2005  
Purpose     : Update the record into  APP_WATERCRAFT_TRAILER_ADD_INT  
Revison History :  
Used In  :   Wolverine         
 ------------------------------------------------------------                  
Date     Review By          Comments                
       
------   ------------       -------------------------*/     
-- drop proc dbo.Proc_UpdateTRAILERAdditionalInterest  
CREATE PROC Proc_UpdateTRAILERAdditionalInterest  
(  
 @CUSTOMER_ID      int,  
 @APP_ID   smallint,  
 @APP_VERSION_ID  smallint,  
 @HOLDER_ID  int,  
 @TRAILER_ID  smallint,  
 @MEMO   NVARCHAR(500),  
 @NATURE_OF_INTEREST NVARCHAR(60),  
 @RANK   SMALLINT,
 @LOAN_REF_NUMBER NVARCHAR(75),  
 @MODIFIED_BY      int,        
 @LAST_UPDATED_DATETIME  datetime ,  
 @ADD_INT_ID Int,  
 @HOLDER_NAME nvarchar(140),  
 @HOLDER_ADD1 nvarchar(280),  
 @HOLDER_ADD2 nvarchar(280),  
 @HOLDER_CITY nvarchar(160),  
 @HOLDER_COUNTRY nvarchar(20),  
 @HOLDER_STATE nvarchar(20),  
 @HOLDER_ZIP nvarchar(22)           
)  
AS  
DECLARE @COUNT INT
 SELECT @COUNT = count(RANK)  from APP_WATERCRAFT_TRAILER_ADD_INT WHERE  CUSTOMER_ID = @CUSTOMER_ID AND                            
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
	  
	 UPDATE APP_WATERCRAFT_TRAILER_ADD_INT  
	 SET MEMO = @MEMO,  
	  NATURE_OF_INTEREST = @NATURE_OF_INTEREST,  
	  RANK = @RANK,  
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
	  ADD_INT_ID = @ADD_INT_ID and  
	  TRAILER_ID = @TRAILER_ID  
	 END  
	 ELSE  
	 BEGIN  
	  UPDATE APP_WATERCRAFT_TRAILER_ADD_INT  
	  SET MEMO = @MEMO,  
	   NATURE_OF_INTEREST = @NATURE_OF_INTEREST,  
	   RANK = @RANK,  
	   LOAN_REF_NUMBER = @LOAN_REF_NUMBER,  
	   MODIFIED_BY = @MODIFIED_BY,   
	   LAST_UPDATED_DATETIME = @LAST_UPDATED_DATETIME  
	  WHERE CUSTOMER_ID = @CUSTOMER_ID and  
	   APP_ID = @APP_ID and  
	   APP_VERSION_ID = @APP_VERSION_ID and  
	   ADD_INT_ID = @ADD_INT_ID and  
	   TRAILER_ID = @TRAILER_ID  
	   
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

