IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_DEACTIVATE_RISK]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_DEACTIVATE_RISK]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

    
/*----------------------------------------------------------              
PROC NAME       : DBO.POL_CIVIL_TRANSPORT_VEHICLES              
CREATED BY      : praveer panghal    
DATE            : 13/06/2011             
PURPOSE   :TO ACTIVATE AND DEACTIVATE RECORDS IN POL_CIVIL_TRANSPORT_VEHICLES TABLE.              
REVISON HISTORY :              
USED IN   : EBIX ADVANTAGE              
------------------------------------------------------------              
DATE     REVIEW BY          COMMENTS              
------   ------------       -------------------------*/              
--DROP PROC DBO.PROC_DEACTIVATE_RISK     28033,286,2
    
--DROP PROC DBO.PROC_DEACTIVATE_RISK       
--GO
CREATE  PROC [DBO].[PROC_DEACTIVATE_RISK]    
(           
    
  @CUSTOMER_ID INT,        
  @POLICY_ID INT,        
  @POLICY_VERSION_ID INT,
  @CANCEL_VERSION_ID INT  ,  
  @LOB_ID INT
)            
AS       
   
  BEGIN   
 IF(@LOB_ID IN (17,18) )
  
  BEGIN
  UPDATE POL_CIVIL_TRANSPORT_VEHICLES        
   SET             
    IS_ACTIVE  = 'N'  ,
     ORIGINAL_VERSION_ID =@POLICY_VERSION_ID         
  WHERE         
       
  CUSTOMER_ID =  @CUSTOMER_ID AND        
  POLICY_ID = @POLICY_ID AND        
  POLICY_VERSION_ID=@POLICY_VERSION_ID 
  AND VEHICLE_ID IN 
	(SELECT VEHICLE_ID FROM POL_CIVIL_TRANSPORT_VEHICLES WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND 
	 POLICY_VERSION_ID = @CANCEL_VERSION_ID AND VEHICLE_ID NOT IN
	 ( SELECT VEHICLE_ID FROM POL_CIVIL_TRANSPORT_VEHICLES  WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND 
		POLICY_VERSION_ID < @CANCEL_VERSION_ID
	 )
	)
  
  
  END  
END        



        
GO

