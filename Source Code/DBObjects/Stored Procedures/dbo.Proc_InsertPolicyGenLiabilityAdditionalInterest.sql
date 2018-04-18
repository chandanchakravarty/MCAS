IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertPolicyGenLiabilityAdditionalInterest]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertPolicyGenLiabilityAdditionalInterest]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                          
Proc Name       : dbo.Proc_InsertPolicyGenLiabilityAdditionalInterest                          
Created by      : Sumit Chhabra                          
Date            : 04/05/2006                          
Purpose      	  : To insert data in POL_GENERAL_HOLDER_INTEREST
Revison History :                          

Used In        : Wolverine                 
                 
------------------------------------------------------------                          
Date     Review By          Comments                          
------   ------------       -------------------------*/         
CREATE   PROC dbo.Proc_InsertPolicyGenLiabilityAdditionalInterest                
(                
 @CUSTOMER_ID      int,                
 @POLICY_ID   int,                
 @POLICY_VERSION_ID  smallint,                
 @HOLDER_ID  int,                
 @MEMO   NVARCHAR(1000),                
 @NATURE_OF_INTEREST NVARCHAR(120),                
 @RANK   smallint,  
 --@CERTIFICATE_REQUIRED NCHAR(1),                
 @LOAN_REF_NUMBER NVARCHAR(300),  
 @CREATED_BY      int,                      
 @CREATED_DATETIME      datetime,                
 @HOLDER_NAME nvarchar(280),                
 @HOLDER_ADD1 nvarchar(560),                
 @HOLDER_ADD2 nvarchar(280),                
 @HOLDER_CITY nvarchar(320),                
 @HOLDER_COUNTRY nvarchar(20),                
 @HOLDER_STATE nvarchar(40),                
 @HOLDER_ZIP varchar(11),            
 @IS_ACTIVE nchar(4)='Y'               
                     
)                
AS                
                
DECLARE @MAX_ADD_INT int                
                
BEGIN                
 SELECT @MAX_ADD_INT = ISNULL(MAX(ADD_INT_ID),0) + 1                
 FROM POL_GENERAL_HOLDER_INTEREST                
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND                
       POLICY_ID = @POLICY_ID AND                
  POLICY_VERSION_ID = @POLICY_VERSION_ID         
    
                 
 IF ( @HOLDER_ID = 0 )                
 BEGIN                
  INSERT INTO POL_GENERAL_HOLDER_INTEREST        
  (                
   CUSTOMER_ID,                 
   POLICY_ID,                 
   POLICY_VERSION_ID,                
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
     IS_ACTIVE              
  )                
  VALUES                
  (                
   @CUSTOMER_ID,                
    @POLICY_ID,                
    @POLICY_VERSION_ID,                
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
   @IS_ACTIVE          
  )                
 END                
 ELSE                
 BEGIN                
  INSERT INTO POL_GENERAL_HOLDER_INTEREST                
  (                
  CUSTOMER_ID,                 
  POLICY_ID,                 
  POLICY_VERSION_ID,                
   HOLDER_ID,             
  MEMO,                
   NATURE_OF_INTEREST,                 
  RANK,                
  --CERTIFICATE_REQUIRED,                
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
   @MEMO,                 
   @NATURE_OF_INTEREST,                
    @RANK,                
   --@CERTIFICATE_REQUIRED,                
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
                
            
          
        
      
    
  



GO

