IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_ACTIVATEDEACTIVATE_PROCUCT_LOCATION_INFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_ACTIVATEDEACTIVATE_PROCUCT_LOCATION_INFO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





 /*----------------------------------------------------------            
Proc Name       : dbo.POL_PRODUCT_LOCATION_INFO      
Created by      :  Lalit Kumar Chauhan
Date            : 04/27/2010            
Purpose         :To Activate and deactivate records in POL_PRODUCT_LOCATION_INFO table.            
Revison History :            
Used In        : Ebix Advantage            
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/            
--DROP PROC dbo.PROC_ACTIVATEDEACTIVATE_PROCUCT_LOCATION_INFO
  
  
CREATE  PROC [dbo].[PROC_ACTIVATEDEACTIVATE_PROCUCT_LOCATION_INFO]  
(          
 @CUSTOMER_ID INT,      
 @POLICY_ID INT,      
 @POLICY_VERSION_ID SMALLINT,      
 @PRODUCT_RISK_ID  INT,          
 @IS_ACTIVE   NCHAR(1)   ,
  @LOCATION_NUMBER INT=NULL,
 @ITEM_NUMBER INT=NULL         
)          
AS 
DECLARE @COUNT INT      
BEGIN  

SELECT  @COUNT= COUNT(*) FROM POL_PRODUCT_LOCATION_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID 
  AND LOCATION_NUMBER=@LOCATION_NUMBER AND ITEM_NUMBER=@ITEM_NUMBER and  PRODUCT_RISK_ID<>@PRODUCT_RISK_ID and IS_ACTIVE='Y'
  IF(@COUNT>=1)   
  BEGIN
  RETURN -5
  END
 ELSE         
BEGIN  
UPDATE POL_PRODUCT_LOCATION_INFO      
 SET           
    IS_ACTIVE    = @IS_ACTIVE,
    ORIGINAL_VERSION_ID = CASE  WHEN @IS_ACTIVE='Y' THEN 0 ELSE  @POLICY_VERSION_ID END             
 WHERE          
 PRODUCT_RISK_ID    = @PRODUCT_RISK_ID AND      
 CUSTOMER_ID =  @CUSTOMER_ID AND      
 POLICY_ID = @POLICY_ID AND      
 POLICY_VERSION_ID=@POLICY_VERSION_ID       
    RETURN 1
    END   
   
END   

GO

