IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Chk_Watercraft_Exists_Pol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Chk_Watercraft_Exists_Pol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--drop proc Dbo.Proc_Chk_Watercraft_Exists_Pol            

/*---------------------------------------------------------------------------------            
Proc Name           : Dbo.Proc_Chk_Watercraft_Exists_Pol            
Created by          : Ashwani     
Date                : 15 June 2006     
Purpose             :         
Revison History     : To chk the watercraft in Homeowner at policy level               
Used In             : Wolverine        
------------------------------------------------------------ ----------------------          
Date     Review By          Comments            
------   ------------       -------------------------------------------------------*/     
  
CREATE proc Dbo.Proc_Chk_Watercraft_Exists_Pol                                    
(                                    
 @CUSTOMER_ID     int,                                    
 @POLICY_ID      int,                                    
 @POL_VERSION_ID  int    
)                                    
as                        
begin    
    
DECLARE @IS_EXISTS CHAR ,@BOAT_WITH_HOMEOWNER NCHAR(2)   
SET @IS_EXISTS='N'  
     
SELECT @BOAT_WITH_HOMEOWNER=ISNULL(BOAT_WITH_HOMEOWNER,'')  
FROM POL_HOME_OWNER_GEN_INFO    WITH(NOLOCK)   
WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POL_VERSION_ID   
  
--Manoj Rathore 02-07-2009   
--Watercraft rule applied if Boat attached with Homeowners is 'Y' Itrack No. 6027   
  
IF(@BOAT_WITH_HOMEOWNER='1')  
BEGIN  
 --  BOAT     
 IF EXISTS(SELECT BOAT_ID FROM POL_WATERCRAFT_INFO  WITH (NOLOCK)    
 WHERE    CUSTOMER_ID = @CUSTOMER_ID   AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POL_VERSION_ID AND IS_ACTIVE='Y')    
 BEGIN     
 SET @IS_EXISTS='Y'    
 END   
     
 -- DRIVER      
 IF EXISTS (SELECT DRIVER_ID FROM POL_WATERCRAFT_DRIVER_DETAILS  WITH (NOLOCK)           
 WHERE    CUSTOMER_ID = @CUSTOMER_ID   AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POL_VERSION_ID AND IS_ACTIVE='Y')    
 BEGIN     
 SET @IS_EXISTS='Y'    
 END     
   
 -- UQ     
 IF EXISTS (SELECT CUSTOMER_ID FROM POL_WATERCRAFT_GEN_INFO  WITH (NOLOCK)    
 WHERE    CUSTOMER_ID = @CUSTOMER_ID   AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POL_VERSION_ID AND IS_ACTIVE='Y')    
 BEGIN     
 SET @IS_EXISTS='Y'    
 END    
END  
ELSE--------Modified on 05 Oct 2009 : Itrack 6513
BEGIN
		---------INACTVE BOATS
		IF EXISTS(SELECT 1 FROM POL_WATERCRAFT_INFO 
			WHERE CUSTOMER_ID =@CUSTOMER_ID AND   POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POL_VERSION_ID
			AND IS_ACTIVE='N')
		BEGIN
			IF EXISTS(SELECT QUOTE_ID from QOT_CUSTOMER_QUOTE_LIST_POL           
				WHERE   CUSTOMER_ID =@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POL_VERSION_ID        
				AND QUOTE_TYPE='HOME-BOAT')	
			BEGIN
				DELETE FROM QOT_CUSTOMER_QUOTE_LIST_POL             
				WHERE CUSTOMER_ID = @CUSTOMER_ID  and POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POL_VERSION_ID and QUOTE_TYPE='HOME-BOAT'          			
			END
		END
		---DELETED BOATS --if Watercraft Info has been deleted 
		IF NOT EXISTS(SELECT 1 FROM POL_WATERCRAFT_INFO 
			WHERE CUSTOMER_ID =@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POL_VERSION_ID)
		BEGIN
			IF EXISTS(SELECT QUOTE_ID from QOT_CUSTOMER_QUOTE_LIST_POL           
				WHERE CUSTOMER_ID =@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POL_VERSION_ID       
				AND QUOTE_TYPE='HOME-BOAT')	
			BEGIN
				DELETE FROM QOT_CUSTOMER_QUOTE_LIST_POL             
				WHERE CUSTOMER_ID = @CUSTOMER_ID  and POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POL_VERSION_ID and QUOTE_TYPE='HOME-BOAT'          			
			END
		END
		
		
END 

  
 select  @IS_EXISTS as IS_EXISTS    
    
end    
    
    
    
    
    
    
    
    
    
    
  
  
  



GO

