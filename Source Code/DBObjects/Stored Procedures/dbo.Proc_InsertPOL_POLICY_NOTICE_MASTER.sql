IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertPOL_POLICY_NOTICE_MASTER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertPOL_POLICY_NOTICE_MASTER]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
Proc Name        : Dbo.Proc_InsertPOL_POLICY_NOTICE_MASTER      
Created by       : Vijay Arora  
Date             : 22-12-2005  
Purpose          : Insert record in to POL_POLICY_NOTICE_MASTER  
Revison History  :      
Used In        : Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
CREATE PROC Dbo.Proc_InsertPOL_POLICY_NOTICE_MASTER      
(      
 @CUSTOMER_ID       INT,      
 @POLICY_ID        INT,      
 @POLICY_VERSION_ID      SMALLINT,      
 @ROW_ID        INT,  
 @NOTICE_DESCRIPTION  VARCHAR(250),
 @NOTICE_TYPE VARCHAR(20) 
)      
AS      
BEGIN      
      
       
 INSERT INTO POL_POLICY_NOTICE_MASTER       
 (      
  CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, ROW_ID, NOTICE_DESCRIPTION ,NOTICE_TYPE    
 )      
 VALUES      
 (      
  @CUSTOMER_ID, @POLICY_ID, @POLICY_VERSION_ID, @ROW_ID,@NOTICE_DESCRIPTION,@NOTICE_TYPE      
 )      
END      
    
  



GO

