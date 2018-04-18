IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCOVERAGE_ID_ACORD]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCOVERAGE_ID_ACORD]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------          
Proc Name       : Dbo.Proc_GetCOVERAGE_ID          
Created by      : Pradeep          
Date            : 9/5/2005          
Purpose       :Gets the CoverageID for the passed code         
Revison History :          
Used In        : Wolverine    
        
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/          
--drop proc Proc_GetCOVERAGE_ID  
CREATE PROCEDURE Proc_GetCOVERAGE_ID_ACORD     
(        
 @CUSTOMER_ID Int,        
 @APP_ID Int,        
 @APP_VERSION_ID smallint,        
 @COV_CODE VarChar(10)        
)        
AS        
        
       
DECLARE @COV_ID Int  
        
SELECT @COV_ID = MNT.COV_ID        
FROM MNT_COVERAGE MNT INNER JOIN APP_LIST APP ON        
  MNT.LOB_ID=APP.APP_LOB AND  
  MNT.STATE_ID=APP.STATE_ID   
WHERE APP.CUSTOMER_ID = @CUSTOMER_ID AND        
 APP.APP_ID = @APP_ID AND        
 APP.APP_VERSION_ID = @APP_VERSION_ID and       
 MNT.COV_CODE = @COV_CODE 
--AND   MNT.IS_ACTIVE = 'Y'        
        
RETURN ISNULL(@COV_ID,0)        
GO

