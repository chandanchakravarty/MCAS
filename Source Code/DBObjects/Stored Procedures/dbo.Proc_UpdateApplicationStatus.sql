IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateApplicationStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateApplicationStatus]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

    
 /*----------------------------------------------------------              
Proc Name       : dbo.Proc_UpdateApplicationStatus          
Created by      : Charles Gomes      
Date            : 19/Mar/2010              
Purpose        : Update the Application Status when converted to Policy.          
Revison History :              
Used In         : Wolverine                
------------------------------------------------------------              
Date     Review By          Comments              
    
--drop PROC Proc_UpdateApplicationStatus      
------   ------------       -------------------------*/              
CREATE PROC [dbo].[Proc_UpdateApplicationStatus]          
@CUSTOMER_ID INT,          
@POLICY_ID INT,          
@POLICY_VERSION_ID SMALLINT,      
@POLICY_STATUS NVARCHAR(20) = 'SUSPENDED',  
@POLICY_NUMBER NVARCHAR(75) = NULL ,
@APP_SUBMITTED_DATE DATETIME = NULL
AS          
BEGIN        
      
IF EXISTS      
(SELECT CUSTOMER_ID FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)     
WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID)      
BEGIN      
 IF NOT EXISTS    
 (SELECT CUSTOMER_ID FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)     
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND ISNULL(POLICY_STATUS,'') <> '' AND ISNULL(IS_ACTIVE,'N') = 'Y')    
 BEGIN    
  UPDATE POL_CUSTOMER_POLICY_LIST SET POLICY_STATUS = @POLICY_STATUS, APP_STATUS = 'COMPLETE', POLICY_NUMBER = ISNULL(@POLICY_NUMBER,POLICY_NUMBER)      
  ,APP_SUBMITTED_DATE = @APP_SUBMITTED_DATE --Added by  Lalit Dec 23,2010
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID    
    
  IF EXISTS    
  (SELECT CUSTOMER_ID FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)     
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID != @POLICY_VERSION_ID AND UPPER(APP_STATUS) = 'APPLICATION')    
  BEGIN    
   UPDATE POL_CUSTOMER_POLICY_LIST SET IS_ACTIVE = 'N', APP_STATUS = 'INACTIVE', POLICY_STATUS = NULL      
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID != @POLICY_VERSION_ID AND UPPER(APP_STATUS) = 'APPLICATION'         
  END    
 END          
END      
    
END 
GO

