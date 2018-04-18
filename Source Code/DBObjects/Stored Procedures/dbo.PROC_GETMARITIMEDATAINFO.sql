IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_GETMARITIMEDATAINFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_GETMARITIMEDATAINFO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
    
      
/*----------------------------------------------------------                          
PROC NAME      : DBO.[PROC_GETMARITIMEDATAINFO]                          
CREATED BY       : PRADEEP KUSHWAHA                        
DATE             : 09-04-2010                          
PURPOSE       : RETRIEVING DATA FROM POL_MARITIME                          
REVISON HISTORY :                 
MODIFY BY       :                          
DATE             :                          
PURPOSE       :         
                       
USED IN        : EBIX ADVANTAGE                      
------------------------------------------------------------                          
DATE     REVIEW BY          COMMENTS                          
------   ------------       -------------------------*/                          
--DROP PROC PROC_GETMARITIMEDATAINFO                 
CREATE PROC [dbo].[PROC_GETMARITIMEDATAINFO]    
	@MARITIME_ID INT,
	@CUSTOMER_ID  INT ,  
	@POLICY_ID INT,  
	@POLICY_VERSION_ID SMALLINT                           
AS                          
                          
BEGIN     
	SELECT     
		CUSTOMER_ID,  
		POLICY_ID,  
		POLICY_VERSION_ID,  
		MARITIME_ID,  
		VESSEL_NUMBER,  
		NAME_OF_VESSEL,  
		TYPE_OF_VESSEL,  
		MANUFACTURE_YEAR,  
		MANUFACTURER,  
		BUILDER,  
		CONSTRUCTION,  
		PROPULSION,  
		CLASSIFICATION,  
		LOCAL_OPERATION,  
		LIMIT_NAVIGATION,  
		PORT_REGISTRATION,  
		REGISTRATION_NUMBER,  
		TIE_NUMBER,  
		VESSEL_ACTION_NAUTICO_CLUB,  
		NAME_OF_CLUB,  
		LOCAL_CLUB,  
		NUMBER_OF_CREW,  
		NUMBER_OF_PASSENGER,  
		REMARKS,  
		IS_ACTIVE  ,
		ORIGINAL_VERSION_ID,
		 EXCEEDED_PREMIUM
		 
		  
	    
	FROM POL_MARITIME  WITH(NOLOCK)  
	  
	WHERE  
	CUSTOMER_ID=@CUSTOMER_ID AND    
    POLICY_ID=@POLICY_ID AND    
    POLICY_VERSION_ID=@POLICY_VERSION_ID  AND
    MARITIME_ID=@MARITIME_ID                
END            
GO

