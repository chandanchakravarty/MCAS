IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getWaterCraftHomeAttachedForQQ]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[getWaterCraftHomeAttachedForQQ]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


create proc getWaterCraftHomeAttachedForQQ          
(          
 @CUSTOMER_ID  as integer,          
 @APP_ID  as integer,          
 @APP_VERSION_ID as integer          
)          
as          
          
Begin          
   Select APP_NUMBER, APP_LOB  from APP_LIST with (NoLock)       
   Where    
 CUSTOMER_ID=@CUSTOMER_ID AND    
 APP_ID=@APP_ID AND    
 APP_VERSION_ID=@APP_VERSION_ID     

--IF ATTACHED TO HOME UPDATE BOAT_WITH_HOMEOWNER FIELD HOME UQ

	IF(SELECT SUBSTRING(APP_NUMBER,1,1) APP_NUMBER FROM APP_LIST WITH (NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID AND 
	 APP_ID=@APP_ID AND    
	 APP_VERSION_ID=@APP_VERSION_ID) ='H'
	BEGIN
	UPDATE APP_HOME_OWNER_GEN_INFO SET BOAT_WITH_HOMEOWNER='1'
	   WHERE    
	 CUSTOMER_ID=@CUSTOMER_ID AND    
	 APP_ID=@APP_ID AND    
	 APP_VERSION_ID=@APP_VERSION_ID  
	END
              
end        



          
        
      
    
  



GO

