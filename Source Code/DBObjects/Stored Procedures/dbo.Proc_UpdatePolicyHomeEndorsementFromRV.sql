IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdatePolicyHomeEndorsementFromRV]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdatePolicyHomeEndorsementFromRV]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
Proc Name   : dbo.Proc_UpdatePolicyHomeEndorsementFromRV
Created by  : Ravindra
Date        : 07-04-2006
Purpose     :           
Revison History  :                
 ------------------------------------------------------------                      
Date     Review By          Comments                    
           
------   ------------       -------------------------*/    
-- drop proc Proc_UpdatePolicyHomeEndorsementFromRV
CREATE     PROCEDURE Proc_UpdatePolicyHomeEndorsementFromRV
(  
   
 @CUSTOMER_ID Int,  
 @POLICY_ID Int,  
 @POLICY_VERSION_ID SmallInt  
)  
  
As
BEGIN

DECLARE @NO_OF_VEHICLES INT
DECLARE @STATEID SMALLINT                                          
DECLARE @HO864 SMALLINT
DECLARE @IDENT_COL INT   
DECLARE @DWELLING_ID INT          
--Hold Dewellin Attached to Application 
DECLARE @TEMP_DWELLING_LIST TABLE              
(              
	IDENT_COL INT IDENTITY (1,1),              
	DWELLING_ID INT
)      
	

SELECT  @NO_OF_VEHICLES = COUNT(*) FROM POL_HOME_OWNER_RECREATIONAL_VEHICLES
WHERE   CUSTOMER_ID = @CUSTOMER_ID
	AND POLICY_ID = @POLICY_ID
	AND POLICY_VERSION_ID = @POLICY_VERSION_ID
        AND ACTIVE   = 'Y'

SELECT @STATEID = STATE_ID                                          
FROM 	POL_CUSTOMER_POLICY_LIST
WHERE   CUSTOMER_ID = @CUSTOMER_ID AND                                          
	POLICY_ID = @POLICY_ID AND                                          
	POLICY_VERSION_ID = @POLICY_VERSION_ID   

-- 289 --14
-- 198 --22
IF ( @STATEID = 14 )  
BEGIN 
	SET @HO864 = 289
END 
IF ( @STATEID = 22 )  
BEGIN 
	SET @HO864 = 198
END 
	
IF (@NO_OF_VEHICLES =1)
BEGIN
	SET  @IDENT_COL = 1              
	 
	-- Insert Dwelling_ID in temporary Table
	 INSERT INTO @TEMP_DWELLING_LIST              
	 (              
		DWELLING_ID
	 )            
	 SELECT DWELLING_ID              
	 FROM POL_DWELLINGS_INFO
		  WHERE CUSTOMER_ID	   = @CUSTOMER_ID 
			AND POLICY_ID 	   = @POLICY_ID
			AND POLICY_VERSION_ID = @POLICY_VERSION_ID             
	              
	 WHILE 1 = 1              
	 BEGIN              
		IF NOT EXISTS              
	  	(              
		   	SELECT IDENT_COL FROM @TEMP_DWELLING_LIST             
		   	WHERE IDENT_COL = @IDENT_COL              
	        )              
	  	BEGIN              
	   		BREAK              
	  	END              
	                
	 	SELECT @DWELLING_ID = DWELLING_ID
	  	FROM @TEMP_DWELLING_LIST              
		WHERE IDENT_COL = @IDENT_COL              
		EXEC Proc_Insert_HOME_ENDORSEMENT_BY_ID_FOR_POLICY  
			@CUSTOMER_ID,--@CUSTOMER_ID int,              
			@POLICY_ID,--@POLICY_ID int,              
			@POLICY_VERSION_ID,--@POLICY_VERSION_ID smallint,               
			@HO864,--@ENDORSEMENT_ID smallint,          
			@DWELLING_ID--@DWELLING_ID smallint              
	  
	  IF @@ERROR <> 0   
	  BEGIN  
	   RAISERROR('Unable to insert HO-864 endorsement.',16,1)  
	     
	  END  
	                
	                 
		             
		SET @IDENT_COL = @IDENT_COL + 1              
	END  --- End While Loop       
END
IF (@NO_OF_VEHICLES =0)
BEGIN
	
	SET  @IDENT_COL = 1              
	-- Insert Dwelling_ID in temporary Tabl
	 INSERT INTO @TEMP_DWELLING_LIST              
	 (              
		DWELLING_ID
	 )            
	 SELECT DWELLING_ID              
	  FROM POL_DWELLINGS_INFO
		  WHERE CUSTOMER_ID	   = @CUSTOMER_ID 
			AND POLICY_ID 	   = @POLICY_ID
			AND POLICY_VERSION_ID = @POLICY_VERSION_ID             
	              
	 WHILE 1 = 1              
	 BEGIN              
		IF NOT EXISTS              
	  	(              
		   	SELECT IDENT_COL FROM @TEMP_DWELLING_LIST             
		   	WHERE IDENT_COL = @IDENT_COL              
	        )              
	  	BEGIN              
	   		BREAK              
	  	END              
	                
	 	SELECT @DWELLING_ID = DWELLING_ID
	  	FROM @TEMP_DWELLING_LIST              
		WHERE IDENT_COL = @IDENT_COL              
		EXEC Proc_Delete_HOME_ENDORSEMENT_BY_ID_FOR_POLICY  
		     @CUSTOMER_ID,--@CUSTOMER_ID int,              
		     @POLICY_ID,--@POLICY_ID int,              
		     @POLICY_VERSION_ID,--@POLICY_VERSION_ID smallint,               
		     @HO864,--@ENDORSEMENT_ID smallint,          
		     @DWELLING_ID--@DWELLING_ID smallint                 
	  
	  IF @@ERROR <> 0   
	  BEGIN  
	   RAISERROR('Unable to delete HO-864 endorsement.',16,1)  
	     
	  END  
	                
	                 
		             
		SET @IDENT_COL = @IDENT_COL + 1              
	END  --- End While Loop       
END 
END
  




GO

