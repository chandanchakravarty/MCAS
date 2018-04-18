IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_DELETECOMMODITYINFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_DELETECOMMODITYINFO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/* PROC NAME :[DBO].[PROC_DELETECOMMODITYINFO] 
CREATED BY :PRADEEP KUSHWAHA    
DATE  : 15 APRIL 2010    
PURPOSE  : TO DELETE POL_COMMODITY_INFO DATA  
*/    
--DROP PROC [DBO].[PROC_DELETECOMMODITYINFO]
  
CREATE PROC [dbo].[PROC_DELETECOMMODITYINFO]    
(     
 @COMMODITY_ID  INT ,  
 @CUSTOMER_ID INT,      
 @POLICY_ID INT,      
 @POLICY_VERSION_ID INT  
      
)                
AS                
BEGIN                
    
  DELETE FROM POL_COMMODITY_INFO     
  WHERE     
  CUSTOMER_ID =  @CUSTOMER_ID AND      
  POLICY_ID = @POLICY_ID AND      
  POLICY_VERSION_ID=@POLICY_VERSION_ID  AND
  COMMODITY_ID=@COMMODITY_ID  
  DELETE FROM POL_DISCOUNT_SURCHARGE  WHERE  CUSTOMER_ID =@CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND  POLICY_VERSION_ID=@POLICY_VERSION_ID AND RISK_ID=@COMMODITY_ID       
  DELETE FROM POL_PRODUCT_COVERAGES  WHERE  CUSTOMER_ID =@CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND  POLICY_VERSION_ID=@POLICY_VERSION_ID  AND RISK_ID=@COMMODITY_ID 
END         
    

GO

