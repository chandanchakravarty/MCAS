IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_ACTIVATEDEACTIVATENAMEDPERIL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_ACTIVATEDEACTIVATENAMEDPERIL]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------            
PROC NAME       : DBO.POL_PERILS            
CREATED BY      : PRADEEP KUSHWAHA   
DATE            : 05/04/2010            
PURPOSE       :TO ACTIVATE AND DEACTIVATE RECORDS IN POL_PERILS TABLE.            
REVISON HISTORY :            
USED IN        : EBIX ADVANTAGE            
------------------------------------------------------------            
DATE     REVIEW BY          COMMENTS            
------   ------------       -------------------------*/            
--DROP PROC DBO.PROC_ACTIVATEDEACTIVATENAMEDPERIL     
  
  
CREATE  PROC [dbo].[PROC_ACTIVATEDEACTIVATENAMEDPERIL]  
(          
 @CUSTOMER_ID INT,      
 @POLICY_ID INT,      
 @POLICY_VERSION_ID SMALLINT,      
 @PERIL_ID  SMALLINT,          
 @IS_ACTIVE   NCHAR(1) ,
  @LOCATION_NUMBER INT=NULL,
 @ITEM_NUMBER INT=NULL         
)          
AS          
DECLARE @COUNT INT      
BEGIN  

SELECT  @COUNT= COUNT(*) FROM POL_PERILS WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID 
  AND LOCATION_NUMBER=@LOCATION_NUMBER AND ITEM_NUMBER=@ITEM_NUMBER and  PERIL_ID<>@PERIL_ID and IS_ACTIVE='Y'
  IF(@COUNT>=1)   
  BEGIN
  RETURN -5
  END
 ELSE
 BEGIN  
UPDATE POL_PERILS      
 SET           
    IS_ACTIVE  = @IS_ACTIVE ,
     ORIGINAL_VERSION_ID = CASE  WHEN @IS_ACTIVE='Y' THEN 0 ELSE  @POLICY_VERSION_ID END            
 WHERE          
  CUSTOMER_ID =  @CUSTOMER_ID AND      
  POLICY_ID = @POLICY_ID AND      
  POLICY_VERSION_ID=@POLICY_VERSION_ID   AND     
  PERIL_ID=@PERIL_ID   
  RETURN 1 
  END    
    
END      
      

GO

