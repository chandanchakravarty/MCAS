IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_GET_MAX_CLIENT_ORDER_AND_VEHICLENUMBER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_GET_MAX_CLIENT_ORDER_AND_VEHICLENUMBER]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
/*----------------------------------------------------------                          
PROC NAME      : DBO.[PROC_GETCIVILTRANSPORTVEHICLEINFODATA]                          
CREATED BY     : PRADEEP KUSHWAHA                        
DATE           : 14-05-2010                          
PURPOSE        : Get the Max id of Client order and vehiclenumber 
REVISON HISTORY:                 
MODIFY BY      :                          
DATE           :                          
PURPOSE        :         
USED IN        : EBIX ADVANTAGE                      
------------------------------------------------------------                          
DATE     REVIEW BY          COMMENTS                          
------   ------------       -------------------------*/                          
--DROP PROC DBO.PROC_GET_MAX_CLIENT_ORDER_AND_VEHICLENUMBER   
         
CREATE PROC [dbo].[PROC_GET_MAX_CLIENT_ORDER_AND_VEHICLENUMBER]
	@CUSTOMER_ID  INT ,  
	@POLICY_ID INT,  
	@POLICY_VERSION_ID SMALLINT  ,
	@CLIENT_ORDER_OUT NUMERIC OUT ,
	@VEHICLE_NUMBER_OUT NUMERIC OUT ,
	@CLIENT_ORDER NUMERIC ,
	@VEHICLE_NUMBER NUMERIC ,
	@FLAG INT 
AS                          
                          
BEGIN 
	 
      IF(@FLAG=1)--Get the max id CLIENT_ORDER and VEHICLE_NUMBER 
		BEGIN 
		   SELECT  @CLIENT_ORDER_OUT=ISNULL(MAX(CLIENT_ORDER),0)+1 FROM POL_CIVIL_TRANSPORT_VEHICLES WITH (NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID =@POLICY_ID      
		   SELECT  @VEHICLE_NUMBER_OUT=ISNULL(MAX(VEHICLE_NUMBER),0)+1 FROM POL_CIVIL_TRANSPORT_VEHICLES WITH (NOLOCK)  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID =@POLICY_ID  AND POLICY_VERSION_ID =@POLICY_VERSION_ID    
		   RETURN 1 --Return 1 if both CLIENT_ORDER and @VEHICLE_NUMBER_OUT exist
		END
      ELSE IF( @FLAG=2 ) ---this flag is use to check the check CLIENT ORDER   
			BEGIN
				IF EXISTS (SELECT CLIENT_ORDER FROM POL_CIVIL_TRANSPORT_VEHICLES WITH (NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID =@POLICY_ID AND CLIENT_ORDER=@CLIENT_ORDER)
					RETURN 4 --Return 4 if the CLIENT_ORDER is exist
				ELSE
					RETURN 5 --Return 5 if the CLIENT_ORDER NUMBER does not exist
					 
			END
	 ELSE IF (@FLAG=3)---this flag is use to check the check VEHICLE NUMBER
		BEGIN 
			IF EXISTS ( SELECT VEHICLE_NUMBER FROM POL_CIVIL_TRANSPORT_VEHICLES WITH (NOLOCK)  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID =@POLICY_ID  AND POLICY_VERSION_ID =@POLICY_VERSION_ID AND VEHICLE_NUMBER=@VEHICLE_NUMBER)
				 RETURN 6  --Return 6 if the VEHICLE NUMBER is exist
			ELSE
				 RETURN 7 --Return 7 if the VEHICLE NUMBER does not exist
				 
					 
		END
		
END            
GO

