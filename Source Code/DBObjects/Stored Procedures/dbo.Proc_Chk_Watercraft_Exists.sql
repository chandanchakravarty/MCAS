IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Chk_Watercraft_Exists]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Chk_Watercraft_Exists]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--DROP PROC Proc_Chk_Watercraft_Exists
/*----------------------------------------------------------        
Proc Name           : Dbo.Proc_Chk_Watercraft_Exists        
Created by          : Ashwani 
Date                : 28 Mar 2006 
Purpose             :     
Revison History     : To chk the watercraft in Homeowner     
Used In             : Wolverine          
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/ 
CREATE proc Dbo.Proc_Chk_Watercraft_Exists                             
(                                
	@CUSTOMER_ID     int,                                
	@APP_ID    	int,                                
	@APP_VERSION_ID   int
)                                
as                    
begin

 Declare @IS_EXISTS char,@BOAT_WITH_HOMEOWNER NCHAR(2) 
 set @IS_EXISTS='N'
 
SELECT @BOAT_WITH_HOMEOWNER=ISNULL(BOAT_WITH_HOMEOWNER,'')
FROM APP_HOME_OWNER_GEN_INFO    WITH(NOLOCK) 
WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID  
--Manoj Rathore 02-07-2009 
--Watercraft rule applied if Boat attached with Homeowners is 'Y' Itrack No. 6027 

IF(@BOAT_WITH_HOMEOWNER='1')
BEGIN
	--  Boat 
	 IF EXISTS(SELECT BOAT_ID FROM APP_WATERCRAFT_INFO
	 WHERE    CUSTOMER_ID = @CUSTOMER_ID   AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND IS_ACTIVE='Y')
	 BEGIN 
		SET @IS_EXISTS='Y'
	 END  
	
	-- Driver  
	 IF EXISTS (SELECT DRIVER_ID FROM APP_WATERCRAFT_DRIVER_DETAILS        
	 WHERE    CUSTOMER_ID = @CUSTOMER_ID   AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND IS_ACTIVE='Y')
	 BEGIN 
		SET @IS_EXISTS='Y'
	 END 

	--UQ
	 IF EXISTS (SELECT CUSTOMER_ID FROM APP_WATERCRAFT_GEN_INFO
	 WHERE    CUSTOMER_ID = @CUSTOMER_ID   AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND IS_ACTIVE='Y')
	 BEGIN 
		SET @IS_EXISTS='Y'
	 END
END
ELSE--------Modified on 05 Oct 2009 : Itrack 6513
BEGIN
		---------INACTVE BOATS
		IF EXISTS(SELECT 1 FROM APP_WATERCRAFT_INFO 
			WHERE CUSTOMER_ID =@CUSTOMER_ID AND   APP_ID=@APP_ID AND   APP_VERSION_ID=@APP_VERSION_ID
			AND IS_ACTIVE='N')
		BEGIN
			IF EXISTS(SELECT QUOTE_ID from QOT_CUSTOMER_QUOTE_LIST           
				WHERE   CUSTOMER_ID =@CUSTOMER_ID AND   APP_ID=@APP_ID AND   APP_VERSION_ID=@APP_VERSION_ID        
				AND QUOTE_TYPE='HOME-BOAT')	
			BEGIN
				DELETE FROM QOT_CUSTOMER_QUOTE_LIST             
				WHERE CUSTOMER_ID = @CUSTOMER_ID  and APP_ID =@APP_ID and APP_VERSION_ID=@APP_VERSION_ID and QUOTE_TYPE='HOME-BOAT'          			
			END
		END
		---DELETED BOATS --if Watercraft Info has been deleted 
		IF NOT EXISTS(SELECT 1 FROM APP_WATERCRAFT_INFO 
			WHERE CUSTOMER_ID =@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID)
		BEGIN
			IF EXISTS(SELECT QUOTE_ID from QOT_CUSTOMER_QUOTE_LIST           
				WHERE CUSTOMER_ID =@CUSTOMER_ID AND   APP_ID=@APP_ID AND   APP_VERSION_ID=@APP_VERSION_ID        
				AND QUOTE_TYPE='HOME-BOAT')	
			BEGIN
				DELETE FROM QOT_CUSTOMER_QUOTE_LIST             
				WHERE CUSTOMER_ID = @CUSTOMER_ID  and APP_ID =@APP_ID and APP_VERSION_ID=@APP_VERSION_ID and QUOTE_TYPE='HOME-BOAT'          			
			END
		END
		
		
END 

 select  @IS_EXISTS as IS_EXISTS

end







GO

