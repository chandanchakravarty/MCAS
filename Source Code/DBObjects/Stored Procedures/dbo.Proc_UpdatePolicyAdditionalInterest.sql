IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdatePolicyAdditionalInterest]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdatePolicyAdditionalInterest]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------        
Proc Name     : dbo.Proc_UpdatePolicyAdditionalInterest    
Created by    : Vijay Arora      
Date          : 10-11-2005  
Purpose      : Update the Policy Addtional Interest in table named POL_ADD_OTHER_INT   
Revison History :    
Used In   :   Wolverine           
 ------------------------------------------------------------                    
Date     Review By          Comments                  
------   ------------       -------------------------*/       
-- drop proc dbo.Proc_UpdatePolicyAdditionalInterest    
CREATE    PROC dbo.Proc_UpdatePolicyAdditionalInterest    
(    
 @CUSTOMER_ID    int,--   nchar(4),    
 @POLICY_ID int,--  NVARCHAR(2),    
 @POLICY_VERSION_ID smallint,-- NVARCHAR(2),    
 @HOLDER_ID   NVARCHAR(4),    
 @VEHICLE_ID   NVARCHAR(4),    
 @MEMO   NVARCHAR(250),    
 @NATURE_OF_INTEREST NVARCHAR(30),    
 @RANK   NVARCHAR(2),    
 @LOAN_REF_NUMBER NVARCHAR(75),    
 @MODIFIED_BY     int,          
 @LAST_UPDATED_DATETIME     datetime ,    
 @ADD_INT_ID Int,    
 @HOLDER_NAME nvarchar(70),    
 @HOLDER_ADD1 nvarchar(70),    
 @HOLDER_ADD2 nvarchar(70),    
 @HOLDER_CITY nvarchar(40),    
 @HOLDER_COUNTRY nvarchar(5),    
 @HOLDER_STATE nvarchar(5),    
 @HOLDER_ZIP varchar(11)         
)    
AS    
  
DECLARE @COUNT INT  
 SELECT @COUNT = count(RANK)  from POL_ADD_OTHER_INT WHERE  CUSTOMER_ID = @CUSTOMER_ID AND                              
    POLICY_ID = @POLICY_ID AND                              
    POLICY_VERSION_ID = @POLICY_VERSION_ID                              
    and RANK=@RANK AND ADD_INT_ID <> @ADD_INT_ID 
--Added by Charles on 1-Jun-2009 for Itrack 5634
	AND VEHICLE_ID= @VEHICLE_ID  
                         
     IF @COUNT>0                               
   BEGIN   
    RETURN -1                         
   END                        
 ELSE  
   BEGIN    
  IF ( @HOLDER_ID = 0 )    
  BEGIN    
   UPDATE POL_ADD_OTHER_INT    
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
    VEHICLE_ID  = @VEHICLE_ID    
  END    
  ELSE    
  BEGIN    
   UPDATE POL_ADD_OTHER_INT    
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
    VEHICLE_ID  = @VEHICLE_ID    
       
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

