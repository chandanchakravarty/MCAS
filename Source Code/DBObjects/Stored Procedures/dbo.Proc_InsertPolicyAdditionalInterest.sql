IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertPolicyAdditionalInterest]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertPolicyAdditionalInterest]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------              
Proc Name     : dbo.Proc_InsertPolicyAdditionalInterest          
Created by    : Vijay Arora       
Date          : 10-11-2005      
Purpose       : Insert the record into  POL_ADD_OTHER_INT  Table          
Revison History :          
Used In   :   Wolverine                 
 ------------------------------------------------------------                          
Date     Review By          Comments                        
               
------   ------------       -------------------------*/             
-- drop proc dbo.Proc_InsertPolicyAdditionalInterest          
CREATE PROC dbo.Proc_InsertPolicyAdditionalInterest          
(          
 @CUSTOMER_ID   int,--    nchar(4),          
 @POLICY_ID int,--  NVARCHAR(2),          
 @POLICY_VERSION_ID smallint,-- NVARCHAR(2),          
 @HOLDER_ID   Int,          
 @VEHICLE_ID   NVARCHAR(4),          
 @MEMO   NVARCHAR(250),          
 @NATURE_OF_INTEREST NVARCHAR(30),          
 @RANK   NVARCHAR(2),          
 @LOAN_REF_NUMBER NVARCHAR(75),      
 @CREATED_BY     int,                
 @CREATED_DATETIME     datetime,          
 @HOLDER_NAME nvarchar(70),          
 @HOLDER_ADD1 nvarchar(70),          
 @HOLDER_ADD2 nvarchar(70),          
 @HOLDER_CITY nvarchar(40),          
 @HOLDER_COUNTRY nvarchar(5),          
 @HOLDER_STATE nvarchar(5),          
 @HOLDER_ZIP varchar(11),        
 @IS_ACTIVE nchar(2)      
      
)          
AS          
          
DECLARE @MAX_ADD_INT int, @COUNT NUMERIC, @LOBID INT         
    SET @COUNT=0      
BEGIN         
 SELECT @LOBID =POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST WHERE      
  CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID       
  AND POLICY_VERSION_ID = @POLICY_VERSION_ID      
     
--Removed by Charles, IF(@LOBID<>2) block for Itrack Issue 5634 on 27-April-2009.   
   SELECT @COUNT = COUNT(RANK) FROM POL_ADD_OTHER_INT (NOLOCK)      
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID       
   AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND RANK = @RANK           
	--Added by Charles on 12-May-2009 for Itrack 5634
	AND VEHICLE_ID= @VEHICLE_ID     

    IF (@COUNT >0 )      
   BEGIN      
   RETURN -1      
   END      
      
    ELSE           
 BEGIN        
           
  SELECT @MAX_ADD_INT = ISNULL(MAX(ADD_INT_ID),0) + 1          
  FROM POL_ADD_OTHER_INT          
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND          
        POLICY_ID = @POLICY_ID AND          
   POLICY_VERSION_ID = @POLICY_VERSION_ID --AND          
             
           
  IF ( @HOLDER_ID = 0 )          
  BEGIN          
   INSERT INTO POL_ADD_OTHER_INT          
   (          
    CUSTOMER_ID,          
    POLICY_ID,          
    POLICY_VERSION_ID,          
    VEHICLE_ID,          
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
    @VEHICLE_ID,          
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
   INSERT INTO POL_ADD_OTHER_INT          
   (          
    CUSTOMER_ID,          
    POLICY_ID,          
    POLICY_VERSION_ID,          
    HOLDER_ID,          
    VEHICLE_ID,          
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
    @VEHICLE_ID,          
    @MEMO,          
    @NATURE_OF_INTEREST,          
    @RANK,          
    @LOAN_REF_NUMBER,          
    @CREATED_BY,           
    GetDate(),          
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

