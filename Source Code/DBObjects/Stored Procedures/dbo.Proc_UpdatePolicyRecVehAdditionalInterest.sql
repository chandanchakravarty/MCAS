IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdatePolicyRecVehAdditionalInterest]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdatePolicyRecVehAdditionalInterest]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------        
Proc Name    : Proc_UpdatePolicyRecVehAdditionalInterest    
Created by   : Swastika Gaur    
Date         : 15th Jun'06    
Purpose      : Update the record into  POL_HOMEOWNER_REC_VEH_ADD_INT    

-----   ------------       -------------------------*/      
 
-- drop proc dbo.Proc_UpdatePolicyRecVehAdditionalInterest    
CREATE PROC dbo.Proc_UpdatePolicyRecVehAdditionalInterest    
(    
 @CUSTOMER_ID      int,    
 @POLICY_ID   smallint,    
 @POLICY_VERSION_ID  smallint,    
 @HOLDER_ID  int,    
 @REC_VEH_ID  smallint,    
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
 SELECT @COUNT = count(RANK)  from POL_HOMEOWNER_REC_VEH_ADD_INT WHERE  CUSTOMER_ID = @CUSTOMER_ID AND                            
		  POLICY_ID = @POLICY_ID AND                            
		  POLICY_VERSION_ID = @POLICY_VERSION_ID                            
		  and RANK=@RANK AND ADD_INT_ID <> @ADD_INT_ID                      
     IF @COUNT>0                             
	  BEGIN 
	   RETURN -1                       
	  END                      
 ELSE
 BEGIN    
  IF ( @HOLDER_ID = 0 )    
      BEGIN    
     
	  UPDATE POL_HOMEOWNER_REC_VEH_ADD_INT    
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
	   POLICY_ID = @POLICY_ID and    
	   POLICY_VERSION_ID = @POLICY_VERSION_ID and    
	   ADD_INT_ID = @ADD_INT_ID and    
	   REC_VEH_ID = @REC_VEH_ID    
      END    
  ELSE    
      BEGIN    
	   UPDATE POL_HOMEOWNER_REC_VEH_ADD_INT    
	   SET MEMO = @MEMO,    
	    NATURE_OF_INTEREST = @NATURE_OF_INTEREST,    
	    RANK = @RANK,    
	    LOAN_REF_NUMBER = @LOAN_REF_NUMBER,    
	    MODIFIED_BY = @MODIFIED_BY,     
	    LAST_UPDATED_DATETIME = @LAST_UPDATED_DATETIME    
	   WHERE CUSTOMER_ID = @CUSTOMER_ID and    
	    POLICY_ID = @POLICY_ID and    
	    POLICY_VERSION_ID = @POLICY_VERSION_ID and    
	    ADD_INT_ID = @ADD_INT_ID and    
	    REC_VEH_ID = @REC_VEH_ID    
	      
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

