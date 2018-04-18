IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertPrimaryNamedInsured]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertPrimaryNamedInsured]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------      
Proc Name          : Dbo.Proc_InsertPrimaryNamedInsured      
Created by           : Vijay Arora    
Date                    : 19/05/2005      
Purpose               :       
Revison History :      
Modified by   :Pravesh K chandel  
Modified date  : 28 July 09  
purpose    : To update Home Coverages Itrack 6179  
Used In                :   Wolverine        
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
--DROP PROCEDURE Proc_InsertPrimaryNamedInsured    
CREATE   PROCEDURE [dbo].[Proc_InsertPrimaryNamedInsured]      
(      
 @POLICY_ID int,    
 @POLICY_VERSION_ID smallint,    
 @APPLICANT_ID int,      
 @CUSTOMER_ID int,      
 @CREATED_BY int,       
 @IS_PRIMARY_APPLICANT int,
 @COMMISSION_PERCENT DECIMAL(8,4)   ,   
 @FEES_PERCENT  DECIMAL(8,4),
 @PRO_LABORE_PERCENT  DECIMAL(8,4) = null
)      
AS      
BEGIN      
      
DECLARE @LOB_ID INT  
SELECT @LOB_ID=POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)  
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID   
 AND POLICY_VERSION_ID=@POLICY_VERSION_ID  
  
INSERT INTO POL_APPLICANT_LIST      
(      
POLICY_ID,    
POLICY_VERSION_ID,    
APPLICANT_ID,      
CUSTOMER_ID,      
CREATED_BY,      
CREATED_DATETIME,      
IS_PRIMARY_APPLICANT,
  COMMISSION_PERCENT,
  FEES_PERCENT,
  PRO_LABORE_PERCENT
)      
VALUES      
(     
@POLICY_ID,    
@POLICY_VERSION_ID,     
@APPLICANT_ID,      
@CUSTOMER_ID,      
@CREATED_BY,      
GETDATE(),      
@IS_PRIMARY_APPLICANT,
 @COMMISSION_PERCENT,
@FEES_PERCENT,
@PRO_LABORE_PERCENT
)      
  
    
--------Added by pravesh on 28 July 09 Itrack 6179  
IF (@LOB_ID=1)  
BEGIN  
 EXEC PROC_UPDATE_POLICY_HOME_COVERAGE_BY_APPLICANT  @CUSTOMER_ID,@APPLICANT_ID,@POLICY_ID,@POLICY_VERSION_ID  
END  
---- END HERE   
  
END      
  
  
GO

