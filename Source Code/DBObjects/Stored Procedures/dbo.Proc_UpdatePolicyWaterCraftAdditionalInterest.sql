IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdatePolicyWaterCraftAdditionalInterest]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdatePolicyWaterCraftAdditionalInterest]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------          
Proc Name      	: dbo.Proc_UpdatePolicyWaterCraftAdditionalInterest      
Created by     	: Vijay Arora        
Date           	: 23-11-2005    
Purpose       	: Update the Policy Water Craft Addtional Interest   
Revison History :     
updated by		:	pravesh 
modified date	: 25 april 2008
Used In    	: Wolverine             
 ------------------------------------------------------------                      
Date     Review By          Comments                    
------   ------------       -------------------------*/         
-- drop proc dbo.Proc_UpdatePolicyWaterCraftAdditionalInterest      
CREATE    PROC dbo.Proc_UpdatePolicyWaterCraftAdditionalInterest      
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
 @MODIFIED_BY      INT,            
 @LAST_UPDATED_DATETIME DATETIME ,      
 @ADD_INT_ID   INT,      
 @HOLDER_NAME   NVARCHAR(70),        
 @HOLDER_ADD1   NVARCHAR(140),        
 @HOLDER_ADD2   NVARCHAR(140),        
 @HOLDER_CITY   NVARCHAR(80),        
 @HOLDER_COUNTRY  NVARCHAR(10),        
 @HOLDER_STATE   NVARCHAR(10),        
 @HOLDER_ZIP   VARCHAR(11)      
)      
AS


DECLARE @COUNT INT
DECLARE @HOLDER_ID_EXIST int
 SELECT @COUNT = count(RANK)  from POL_WATERCRAFT_COV_ADD_INT WHERE  CUSTOMER_ID = @CUSTOMER_ID AND                            
		  POLICY_ID = @POLICY_ID AND                            
		  POLICY_VERSION_ID = @POLICY_VERSION_ID                            
		   and RANK=@RANK AND ADD_INT_ID <> @ADD_INT_ID                     
     IF @COUNT>0                             
	  BEGIN 
	   RETURN -1                       
	  END                      
 ELSE
BEGIN      

	SET @HOLDER_ID_EXIST=0
	if ( @HOLDER_ID = 0 ) 
		begin
			SELECT  @HOLDER_ID_EXIST = HOLDER_ID FROM POL_WATERCRAFT_COV_ADD_INT
				WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID                     
					and  BOAT_ID  = @BOAT_ID  AND ADD_INT_ID = @ADD_INT_ID  
			IF (ISNULL(@HOLDER_ID_EXIST,0)!=0)
				SET @HOLDER_ID=@HOLDER_ID_EXIST
		end

	IF ( @HOLDER_ID = 0 )      
	 BEGIN      
	  UPDATE POL_WATERCRAFT_COV_ADD_INT      
	  SET       
	   MEMO=@MEMO,      
	   NATURE_OF_INTEREST=@NATURE_OF_INTEREST,      
	   RANK=@RANK,      
	   LOAN_REF_NUMBER=@LOAN_REF_NUMBER,      
	   MODIFIED_BY=@MODIFIED_BY,       
	   LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME,      
	   HOLDER_NAME = @HOLDER_NAME,      
	   HOLDER_ADD1 = @HOLDER_ADD1,      
	   HOLDER_ADD2 = @HOLDER_ADD2,      
	   HOLDER_CITY = @HOLDER_CITY,      
	   HOLDER_COUNTRY = @HOLDER_COUNTRY,      
	   HOLDER_STATE = @HOLDER_STATE,      
	   HOLDER_ZIP = @HOLDER_ZIP      
	      
	  WHERE       
	   CUSTOMER_ID  = @CUSTOMER_ID and      
	   POLICY_ID   = @POLICY_ID and      
	   POLICY_VERSION_ID = @POLICY_VERSION_ID and      
	   ADD_INT_ID  = @ADD_INT_ID and      
	   BOAT_ID  = @BOAT_ID      
	 END      
	 ELSE      
	 BEGIN      
	  UPDATE POL_WATERCRAFT_COV_ADD_INT      
	  SET       
	   MEMO=@MEMO,      
	   NATURE_OF_INTEREST=@NATURE_OF_INTEREST,      
	   RANK=@RANK,      
	   LOAN_REF_NUMBER=@LOAN_REF_NUMBER,      
	   MODIFIED_BY=@MODIFIED_BY,       
	   LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME      
	  WHERE       
	   CUSTOMER_ID  = @CUSTOMER_ID and      
	   POLICY_ID   = @POLICY_ID and      
	   POLICY_VERSION_ID = @POLICY_VERSION_ID and      
	   ADD_INT_ID  = @ADD_INT_ID and      
	   BOAT_ID  = @BOAT_ID        
	        
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

