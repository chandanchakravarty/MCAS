IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertPolicyWCTrailerAdditionalInterest]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertPolicyWCTrailerAdditionalInterest]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------              
Proc Name       : dbo.Proc_InsertPolicyWCTrailerAdditionalInterest          
Created by     : Vijay Arora       
Date            : 29-11-2005      
Purpose         : Insert the record into POL_WATERCRAFT_TRAILER_ADD_INT  Table          
Revison History :          
Used In     :   Wolverine                 
 ------------------------------------------------------------                          
Date     Review By          Comments                        
               
------   ------------       -------------------------*/             
          
CREATE PROC dbo.Proc_InsertPolicyWCTrailerAdditionalInterest          
(          
 @CUSTOMER_ID   INT,          
 @POLICY_ID     INT,          
 @POLICY_VERSION_ID   SMALLINT,          
 @HOLDER_ID     INT,          
 @TRAILER_ID    SMALLINT,          
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
 @IS_ACTIVE    varchar(2)=null   
)          
AS          
          
DECLARE @MAX_ADD_INT int          
          
BEGIN          
           
 SELECT @MAX_ADD_INT = ISNULL(MAX(ADD_INT_ID),0) + 1          
 FROM POL_WATERCRAFT_TRAILER_ADD_INT          
 WHERE     
 CUSTOMER_ID = @CUSTOMER_ID AND          
        POLICY_ID = @POLICY_ID AND          
   POLICY_VERSION_ID = @POLICY_VERSION_ID AND    
 TRAILER_ID = @TRAILER_ID    
       
          
 IF ( @HOLDER_ID = 0 )          
 BEGIN          
  INSERT INTO POL_WATERCRAFT_TRAILER_ADD_INT          
  (          
   CUSTOMER_ID,          
   POLICY_ID,          
   POLICY_VERSION_ID,          
   TRAILER_ID,          
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
   @TRAILER_ID,          
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
  INSERT INTO POL_WATERCRAFT_TRAILER_ADD_INT          
  (          
   CUSTOMER_ID,          
   POLICY_ID,          
   POLICY_VERSION_ID,          
   HOLDER_ID,          
   TRAILER_ID,          
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
   @TRAILER_ID,          
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
        
      
    
  



GO

