IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdatePOL_POLICY_PROCESS_INFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdatePOL_POLICY_PROCESS_INFO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                      
Proc Name       : dbo.Proc_UpdatePOL_POLICY_PROCESS_INFO                    
Created by      : PRAVESH   K Chandel                      
create date		: 7 may 2008
Purpose  : update process table before commit
                 
Date     Review By          Comments                      
------   ------------       -------------------------
drop proc dbo.Proc_UpdatePOL_POLICY_PROCESS_INFO
*/                      
CREATE PROC [dbo].[Proc_UpdatePOL_POLICY_PROCESS_INFO]                      
(                      
  @CUSTOMER_ID       INT,                      
  @POLICY_ID        INT,                      
  @POLICY_VERSION_ID      SMALLINT,                      
  @ROW_ID        INT,                      
  @NEW_POLICY_VERSION_ID  SMALLINT,                      
  @EFFECTIVE_DATETIME     DATETIME,                      
  @CANCELLATION_OPTION    INT,                      
  @CANCELLATION_TYPE      INT,                    
  @RETURN_PREMIUM      DECIMAL(13)                      
  
)                      
AS                  
BEGIN   

       
UPDATE  POL_POLICY_PROCESS                      
SET                    
 EFFECTIVE_DATETIME		=  @EFFECTIVE_DATETIME,                      
 CANCELLATION_OPTION    =  @CANCELLATION_OPTION,                      
 CANCELLATION_TYPE		=  @CANCELLATION_TYPE,                     
 RETURN_PREMIUM			=  @RETURN_PREMIUM                     
WHERE                       
 CUSTOMER_ID				=  @CUSTOMER_ID                      
 AND POLICY_ID				=  @POLICY_ID                      
 AND POLICY_VERSION_ID		=  @POLICY_VERSION_ID                      
 AND ROW_ID					=  @ROW_ID 
 AND NEW_POLICY_VERSION_ID  =  @NEW_POLICY_VERSION_ID 	                    
                
                     
                      
END        





































GO

