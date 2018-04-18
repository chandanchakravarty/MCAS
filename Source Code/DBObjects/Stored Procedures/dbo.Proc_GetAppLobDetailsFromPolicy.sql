IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAppLobDetailsFromPolicy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAppLobDetailsFromPolicy]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  
      
/*----------------------------------------------------------                
Proc Name       : dbo.Proc_GetAppLobDetailsFromPolicy        
Created by      : Vijay Arora            
Date            : 05-01-2006        
Purpose        : Get the Application and Lob details from Policy Master Table.        
Revison History :                
Modified by  : Pravesh      
date   :3 Aug 09      
Used In         : Wolverine                  
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                
--drop proc dbo.Proc_GetAppLobDetailsFromPolicy   2156,220,1        
CREATE PROC [dbo].[Proc_GetAppLobDetailsFromPolicy]            
@CUSTOMER_ID INT,            
@POLICY_ID INT,            
@POLICY_VERSION_ID SMALLINT          
AS            
BEGIN            
  SELECT D.APP_ID, D.APP_VERSION_ID, D.POLICY_LOB  ,M.LOB_CODE as QUOTE_TYPE,    
  D.POLICY_CURRENCY, D.TRANSACTION_TYPE PRODUCT_TYPE,    
  D.CO_INSURANCE      ,
  ISNULL(INS.PLAN_TYPE,'') AS PLAN_TYPE
  FROM POL_CUSTOMER_POLICY_LIST D  WITH (NOLOCK)       
  INNER JOIN MNT_LOB_MASTER M WITH (NOLOCK)       
  ON D.POLICY_LOB = M.LOB_ID      
  LEFT OUTER JOIN ACT_INSTALL_PLAN_DETAIL INS ON
  INS.IDEN_PLAN_ID =  D.INSTALL_PLAN_ID
  WHERE CUSTOMER_ID = @CUSTOMER_ID            
  AND POLICY_ID = @POLICY_ID          
  AND POLICY_VERSION_ID = @POLICY_VERSION_ID      
          
END            
          
        
      
      
      
      
      
      
GO

