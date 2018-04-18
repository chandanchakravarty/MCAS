IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertPOL_POLICY_ENDORSEMENTS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertPOL_POLICY_ENDORSEMENTS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /**************************************************        
CREATED BY    : Vijay Joshi        
CREATED DATETIME : 23 Dec, 2005        
PURPOSE    : INsert record into endorsement log        
Review        
Review By  Date    Purpose        
Vijay   9 feb,2005  Generate the endorsement no policy wise      
DROP PROC Proc_InsertPOL_POLICY_ENDORSEMENTS        
***************************************************/        
CREATE PROCEDURE [dbo].[Proc_InsertPOL_POLICY_ENDORSEMENTS]        
(        
 @POLICY_ID   INT,        
 @POLICY_VERSION_ID INT,        
 @CUSTOMER_ID  INT,        
 @ENDORSEMENT_NO  INT  OUTPUT,        
 @ENDORSEMENT_DATE DATETIME,        
 @CREATED_BY   INT,        
 @CREATED_DATETIME DATETIME,      
 @PROCESS_TYPE NVARCHAR(25) = NULL,  --Modified By Lalit Chauhan,Nov 02,2010      
 @ENDORSEMENT_STATUS NVARCHAR(50) = NULL --Modified By Lalit Chauhan,dec 07,2010      
)        
AS        
BEGIN        
        
 SET @ENDORSEMENT_NO = 1        
 IF(@ENDORSEMENT_STATUS IS NULL OR @ENDORSEMENT_STATUS = NULL) --Modified By Lalit Chauhan,dec 07,2010      
   SELECT @ENDORSEMENT_STATUS = 'OPEN'    
-- By Pravesh on 11 Nov 2010      
 DECLARE @POLICY_NUMBER NVARCHAR(25)         
 SELECT @POLICY_NUMBER =POLICY_NUMBER       
 FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)       
 WHERE CUSTOMER_ID = @CUSTOMER_ID       
 AND POLICY_ID = @POLICY_ID      
 AND POLICY_VERSION_ID = @POLICY_VERSION_ID      
       
        
 SELECT @ENDORSEMENT_NO = ISNULL(MAX(ENDORSEMENT_NO),0) + 1         
 FROM POL_POLICY_ENDORSEMENTS with(nolock)        
 WHERE CUSTOMER_ID = @CUSTOMER_ID        
  AND POLICY_ID = @POLICY_ID    
  AND POLICY_VERSION_ID IN      
 (      
 SELECT POLICY_VERSION_ID      
 FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)       
 WHERE CUSTOMER_ID = @CUSTOMER_ID       
 AND POLICY_ID = @POLICY_ID      
 AND POLICY_NUMBER = @POLICY_NUMBER      
 )      
       
 INSERT INTO POL_POLICY_ENDORSEMENTS        
 (        
  POLICY_ID, POLICY_VERSION_ID, CUSTOMER_ID, ENDORSEMENT_NO,        
  ENDORSEMENT_DATE, ENDORSEMENT_STATUS,        
  IS_ACTIVE, CREATED_BY, CREATED_DATETIME, MODIFIED_BY, LAST_UPDATED_DATETIME  ,PROCESS_TYPE      
 )        
 VALUES        
 (        
  @POLICY_ID, @POLICY_VERSION_ID, @CUSTOMER_ID, @ENDORSEMENT_NO,        
  @ENDORSEMENT_DATE, @ENDORSEMENT_STATUS,         
  'Y', @CREATED_BY, @CREATED_DATETIME, @CREATED_BY, @CREATED_DATETIME  ,@PROCESS_TYPE      
 )     
 
 IF(@PROCESS_TYPE  in ('37','12') ) --if Revertback process,cancellation
	 UPDATE POL_POLICY_PROCESS SET ENDORSEMENT_NO = @ENDORSEMENT_NO 
	 WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID
	 AND NEW_POLICY_VERSION_ID = @POLICY_VERSION_ID
	 AND PROCESS_ID =  37 and UPPER(PROCESS_STATUS) = 'COMPLETE'
  
 
    
END        
        
        
        
GO

