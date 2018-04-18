IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertPolicyRecVehAdditionalInterest]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertPolicyRecVehAdditionalInterest]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------              
Proc Name    : Proc_InsertPolicyRecVehAdditionalInterest          
Created by   : Swastika Gaur          
Date         : 15th June'06             
Purpose      : Insert the record into  POL_HOMEOWNER_REC_VEH_ADD_INT  Table          
  
Modified By  :   
Modified On  :   
Purpose      :   
 ------------------------------------------------------------                          
Date     Review By          Comments                        
               
------   ------------       -------------------------*/             
-- drop proc dbo.Proc_InsertPolicyRecVehAdditionalInterest          
CREATE    PROC dbo.Proc_InsertPolicyRecVehAdditionalInterest          
(          
 @CUSTOMER_ID      int,          
 @POLICY_ID   smallint,          
 @POLICY_VERSION_ID  smallint,          
 @HOLDER_ID  int,          
 @REC_VEH_ID  smallint,          
 @MEMO   NVARCHAR(500),          
 @NATURE_OF_INTEREST NVARCHAR(60),          
 @RANK   smallint,          
 @LOAN_REF_NUMBER NVARCHAR(150),          
 @CREATED_BY      int,                
 @CREATED_DATETIME      datetime,          
 @HOLDER_NAME nvarchar(140),          
 @HOLDER_ADD1 nvarchar(280),          
 @HOLDER_ADD2 nvarchar(280),          
 @HOLDER_CITY nvarchar(160),          
 @HOLDER_COUNTRY nvarchar(20),          
 @HOLDER_STATE nvarchar(20),          
 @HOLDER_ZIP varchar(22),        
 @IS_ACTIVE VARCHAR(2)='Y'        
)          
AS          
          
DECLARE @MAX_ADD_INT int,@COUNT NUMERIC          
       
BEGIN            
    
    SELECT @COUNT = COUNT(RANK) FROM POL_HOMEOWNER_REC_VEH_ADD_INT (NOLOCK)    
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID     
  AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND RANK = @RANK            
    IF (@COUNT >0 )    
   BEGIN    
   RETURN -1    
   END    
    
    ELSE       
 BEGIN          
  SELECT @MAX_ADD_INT = ISNULL(MAX(ADD_INT_ID),0) + 1          
  FROM POL_HOMEOWNER_REC_VEH_ADD_INT (NOLOCK)         
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND          
        POLICY_ID = @POLICY_ID AND          
    POLICY_VERSION_ID = @POLICY_VERSION_ID AND          
   REC_VEH_ID = @REC_VEH_ID          
            
  IF ( @HOLDER_ID = 0 )          
   BEGIN          
    INSERT INTO POL_HOMEOWNER_REC_VEH_ADD_INT          
   (          
    CUSTOMER_ID,           
    POLICY_ID,           
    POLICY_VERSION_ID,           
    REC_VEH_ID,           
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
    @REC_VEH_ID,          
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
    INSERT INTO POL_HOMEOWNER_REC_VEH_ADD_INT          
   (          
    CUSTOMER_ID,           
    POLICY_ID,           
    POLICY_VERSION_ID,           
    HOLDER_ID,          
    REC_VEH_ID,           
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
    @REC_VEH_ID,          
     @MEMO,           
    @NATURE_OF_INTEREST,           
    @RANK,          
    @LOAN_REF_NUMBER,           
    @CREATED_BY,           
    @CREATED_DATETIME,          
    @MAX_ADD_INT,        
    @IS_ACTIVE        
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

