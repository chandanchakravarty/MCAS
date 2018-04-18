IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_ACTIVATEDEACTIVATECOMMODITYINFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_ACTIVATEDEACTIVATECOMMODITYINFO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------            
PROC NAME       : DBO.POL_COMMODITY_INFO            
CREATED BY      : PRADEEP KUSHWAHA   
DATE            : 15/04/2010            
PURPOSE       :TO ACTIVATE AND DEACTIVATE RECORDS IN POL_COMMODITY_INFO TABLE.            
REVISON HISTORY :            
USED IN        : EBIX ADVANTAGE            
------------------------------------------------------------            
DATE     REVIEW BY          COMMENTS            
------   ------------       -------------------------*/            
--DROP PROC DBO.PROC_ACTIVATEDEACTIVATECOMMODITYINFO    
CREATE  PROC [DBO].[PROC_ACTIVATEDEACTIVATECOMMODITYINFO]  
(          
 @CUSTOMER_ID INT,      
 @POLICY_ID INT,      
 @POLICY_VERSION_ID INT,      
 @COMMODITY_ID  INT,          
 @IS_ACTIVE   NCHAR(1)          
)          
AS          
BEGIN  
UPDATE POL_COMMODITY_INFO      
 SET           
    IS_ACTIVE  = @IS_ACTIVE ,
      ORIGINAL_VERSION_ID = CASE  WHEN @IS_ACTIVE='Y' THEN 0 ELSE  @POLICY_VERSION_ID END            
 WHERE          
	 CUSTOMER_ID =  @CUSTOMER_ID AND      
	 POLICY_ID = @POLICY_ID AND      
	 POLICY_VERSION_ID=@POLICY_VERSION_ID AND   
	 COMMODITY_ID    = @COMMODITY_ID          
	   
END      
      
GO

