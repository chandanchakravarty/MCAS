IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPOLICY_BOAT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPOLICY_BOAT]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                
Proc Name       : dbo.Proc_GetPOLICY_BOAT                
Created by      : Sumit Chhabra            
Date            : 5/24/2006                
Purpose         :To retrieve policy level boats        
Revison History :                
Used In        : Wolverine                
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                
CREATE   PROC dbo.Proc_GetPOLICY_BOAT                
(                
@CLAIM_ID int,  
@BOAT_ID int = null        
)                
AS                
begin              
DECLARE @CUSTOMER_ID INT        
DECLARE @POLICY_ID INT        
DECLARE @POLICY_VERSION_ID INT        
        
 SELECT         
  @CUSTOMER_ID=CUSTOMER_ID,@POLICY_ID=POLICY_ID,@POLICY_VERSION_ID=POLICY_VERSION_ID        
 FROM        
  CLM_CLAIM_INFO        
 WHERE        
  CLAIM_ID=@CLAIM_ID        
         
 IF @CUSTOMER_ID=0         
  RETURN      
  
if @BOAT_ID IS NULL OR @BOAT_ID=0  
begin         
 SELECT BOAT_ID,YEAR,MAKE,MODEL,LENGTH,HULL_ID_NO AS SERIAL_NUMBER
 FROM         
  POL_WATERCRAFT_INFO         
 WHERE        
  CUSTOMER_ID=@CUSTOMER_ID AND        
  POLICY_ID = @POLICY_ID AND        
  POLICY_VERSION_ID = @POLICY_VERSION_ID AND        
  IS_ACTIVE='Y'   
	ORDER BY BOAT_ID    
end  
else  
begin   
 SELECT BOAT_ID,YEAR,MAKE,MODEL,LENGTH,WATERCRAFT_HORSE_POWER,HULL_ID_NO AS SERIAL_NUMBER,
				STATE_REG AS STATE,HULL_MATERIAL AS BODY_TYPE 
 FROM         
  POL_WATERCRAFT_INFO         
 WHERE        
  CUSTOMER_ID=@CUSTOMER_ID AND        
  POLICY_ID = @POLICY_ID AND        
  POLICY_VERSION_ID = @POLICY_VERSION_ID AND  
 BOAT_ID=@BOAT_ID AND  
  IS_ACTIVE='Y'        
end  
           
END           
    
  



GO

