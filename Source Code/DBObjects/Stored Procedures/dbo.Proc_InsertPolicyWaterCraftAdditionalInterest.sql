IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertPolicyWaterCraftAdditionalInterest]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertPolicyWaterCraftAdditionalInterest]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------            
Proc Name      	: dbo.Proc_InsertPolicyWaterCraftAdditionalInterest        
Created by     	: Vijay Arora     
Date           	: 23-11-2005    
Purpose        	: Insert the record into  POL_WATERCRAFT_COV_ADD_INT  Table        
Revison History :        
Used In    	:   Wolverine               
 ------------------------------------------------------------                        
Date     Review By          Comments                      
             
------   ------------       -------------------------*/           
-- drop proc dbo.Proc_InsertPolicyWaterCraftAdditionalInterest        
CREATE PROC dbo.Proc_InsertPolicyWaterCraftAdditionalInterest        
(        
 @CUSTOMER_ID   INT,        
 @POLICY_ID     INT,        
 @POLICY_VERSION_ID   SMALLINT,        
 @HOLDER_ID     INT,        
 @BOAT_ID    SMALLINT,        
 @MEMO     NVARCHAR(250),        
 @NATURE_OF_INTEREST  NVARCHAR(30),        
 @RANK     SMALLINT,        
 @LOAN_REF_NUMBER  NVARCHAR(75),        
 @CREATED_BY      INT,              
 @CREATED_DATETIME      DATETIME,        
 @HOLDER_NAME   NVARCHAR(70),        
 @HOLDER_ADD1   NVARCHAR(140),        
 @HOLDER_ADD2   NVARCHAR(140),        
 @HOLDER_CITY   NVARCHAR(80),        
 @HOLDER_COUNTRY  NVARCHAR(10),        
 @HOLDER_STATE   NVARCHAR(10),        
 @HOLDER_ZIP   VARCHAR(11),
@IS_ACTIVE nchar(2)='Y'      
)        
AS        
 DECLARE @MAX_ADD_INT int
 DECLARE @COUNT NUMERIC       

BEGIN        

    SELECT @COUNT = COUNT(RANK) FROM POL_WATERCRAFT_COV_ADD_INT (NOLOCK)
		WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID 
		AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND RANK = @RANK        
    IF (@COUNT >0 )
		 BEGIN
			RETURN -1
		 END

    ELSE    
	BEGIN        
	         
	 SELECT @MAX_ADD_INT = ISNULL(MAX(ADD_INT_ID),0) + 1        
	 FROM POL_WATERCRAFT_COV_ADD_INT        
	 WHERE   
	 CUSTOMER_ID = @CUSTOMER_ID AND        
	        POLICY_ID = @POLICY_ID AND        
	   POLICY_VERSION_ID = @POLICY_VERSION_ID AND  
	  BOAT_ID = @BOAT_ID  
	     
	        
	 IF ( @HOLDER_ID = 0 )        
	 BEGIN        
	  INSERT INTO POL_WATERCRAFT_COV_ADD_INT        
	  (        
	   CUSTOMER_ID,        
	   POLICY_ID,        
	   POLICY_VERSION_ID,        
	   BOAT_ID,        
	   MEMO,        
	   NATURE_OF_INTEREST,        
	   RANK,        
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
	   ADD_INT_ID,      
	   IS_ACTIVE        
	        
	  )        
	  VALUES        
	  (        
	   @CUSTOMER_ID,        
	   @POLICY_ID,        
	   @POLICY_VERSION_ID,        
	   @BOAT_ID,        
	   @MEMO,        
	   @NATURE_OF_INTEREST,        
	   @RANK,        
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
	   @MAX_ADD_INT,      
	   @IS_ACTIVE  
	  )         
	 END        
	 ELSE        
	 BEGIN        
	  INSERT INTO POL_WATERCRAFT_COV_ADD_INT        
	  (        
	   CUSTOMER_ID,        
	   POLICY_ID,        
	   POLICY_VERSION_ID,        
	   HOLDER_ID,        
	   BOAT_ID,        
	   MEMO,        
	   NATURE_OF_INTEREST,        
	   RANK,        
	   LOAN_REF_NUMBER,        
	   CREATED_BY,         
	   CREATED_DATETIME,        
	   ADD_INT_ID,      
	   IS_ACTIVE        
	  )        
	  VALUES        
	  (        
	   @CUSTOMER_ID,        
	   @POLICY_ID,        
	   @POLICY_VERSION_ID,        
	   @HOLDER_ID,        
	   @BOAT_ID,        
	   @MEMO,        
	   @NATURE_OF_INTEREST,        
	   @RANK,        
	   @LOAN_REF_NUMBER,        
	   @CREATED_BY,         
	   GetDate(),        
	   @MAX_ADD_INT,      
	   'Y'  
	  )        
	          
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

