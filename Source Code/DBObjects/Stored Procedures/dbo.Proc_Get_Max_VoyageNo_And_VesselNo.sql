IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Get_Max_VoyageNo_And_VesselNo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Get_Max_VoyageNo_And_VesselNo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
/*----------------------------------------------------------                            
PROC NAME      : DBO.[Proc_Get_Max_VoyageNo_And_VesselNo]                            
CREATED BY     : PRADEEP KUSHWAHA                          
DATE           : 26-05-2010                            
PURPOSE        : Get the Max id of Voyage Number and Vessel number   
REVISON HISTORY:                   
MODIFY BY      :                            
DATE           :                            
PURPOSE        :           
USED IN        : EBIX ADVANTAGE                        
------------------------------------------------------------                            
DATE     REVIEW BY          COMMENTS                            
------   ------------       -------------------------*/                            
--DROP PROC dbo.Proc_Get_Max_VoyageNo_And_VesselNo
           
CREATE PROC [dbo].[Proc_Get_Max_VoyageNo_And_VesselNo]
 @CUSTOMER_ID  INT ,    
 @POLICY_ID INT,    
 @POLICY_VERSION_ID SMALLINT  ,  
 @COMMODITY_NUMBER NUMERIC ,  
 @VESSEL_NUMBER NUMERIC ,  
 @FLAG INT,
 @CALLEDFOR NVARCHAR (20),   
 @NUMBER NUMERIC OUT   
AS                            
                            
BEGIN   
    IF(@CALLEDFOR='VoyageNo')--Called for the Voyage number (COMMODITY_NUMBER) of National and International Cargo transpotation product 
		BEGIN
			 IF(@FLAG=1)--Get the max COMMODITY_NUMBER if the pageload call (flag==1)
				BEGIN
					SELECT  @NUMBER=ISNULL(MAX(COMMODITY_NUMBER),0)+1 FROM POL_COMMODITY_INFO  WITH (NOLOCK)  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID =@POLICY_ID  AND POLICY_VERSION_ID=@POLICY_VERSION_ID  
					RETURN 1
				END
			ELSE IF (@FLAG=2)--To check whether the COMMODITY_NUMBER exists or not
				BEGIN
					IF EXISTS (SELECT COMMODITY_NUMBER FROM POL_COMMODITY_INFO  WITH (NOLOCK)  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID =@POLICY_ID  AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND COMMODITY_NUMBER=@COMMODITY_NUMBER )
						RETURN -2 --if COMMODITY_NUMBER exists then return -2
					ELSE
						RETURN -3 --if COMMODITY_NUMBER does not exists then return -3
				END
		END
    ELSE IF(@CALLEDFOR='VesselNo')--Called for the Vessel number (VESSEL_NUMBER) of Maritime product 
		BEGIN
			IF(@FLAG=3)--Get the max COMMODITY_NUMBER if the pageload call (flag==3)
				BEGIN
					SELECT  @NUMBER=ISNULL(MAX(VESSEL_NUMBER),0)+1 FROM POL_MARITIME WITH(NOLOCK)   WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID =@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID      
					RETURN 1
				END
			ELSE IF (@FLAG=4)--To check whether the VESSEL_NUMBER exists or not
				BEGIN
					IF EXISTS(SELECT VESSEL_NUMBER  FROM POL_MARITIME WITH(NOLOCK)   WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID =@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND VESSEL_NUMBER=@VESSEL_NUMBER )     
						RETURN -4 --if VESSEL_NUMBER exists then return -4
					ELSE	
						RETURN -5 --if VESSEL_NUMBER does not exists then return -5
				END
				
		END
END 
GO

