IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAssindDriverIDsToMotorcyclePol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAssindDriverIDsToMotorcyclePol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO

/*************************************************************
Proc Name       : dbo.Proc_GetAssindDriverIDsToMotorcyclePol                                                                                      
Created by      : Manoj Rathore                                                    
Date            : 23/04/2009                                                                                      
Purpose         :                                               
Used In         : Wolverine                                                                                      
------------------------------------------------------------                                                                                      
Date     Review By          Comments                                              
------   ------------       ------------------------- *********/
--drop proc Proc_GetAssindDriverIDsToMotorcyclePol 1769,11,1,1
CREATE PROC [dbo].[Proc_GetAssindDriverIDsToMotorcyclePol]                                             
(                                                
@CUSTOMER_ID    int,                                                
@POL_ID    int,                                                
@POL_VERSION_ID   int,                                                
@VEHICLE_ID int                                               
)                                                
AS                                                
                                                
BEGIN                                                
    set quoted_identifier off                                                
	SELECT A.DRIVER_ID FROM POL_DRIVER_DETAILS D  WITH (NOLOCK)                       
	JOIN                   
	POL_DRIVER_ASSIGNED_VEHICLE A WITH (NOLOCK)                       
	ON                   
	A.CUSTOMER_ID = D.CUSTOMER_ID AND                  
	A.POLICY_ID = D.POLICY_ID AND                  
	A.POLICY_VERSION_ID = D.POLICY_VERSION_ID AND                  
	A.DRIVER_ID = D.DRIVER_ID                  
	LEFT OUTER JOIN                
	MNT_LOOKUP_VALUES V  WITH (NOLOCK)                      
	ON                
	A.APP_VEHICLE_PRIN_OCC_ID = V.LOOKUP_UNIQUE_ID       
	  
	WHERE                  
	D.CUSTOMER_ID = @CUSTOMER_ID AND                  
	D.POLICY_ID = @POL_ID AND                  
	D.POLICY_VERSION_ID = @POL_VERSION_ID AND                  
	ISNULL(D.IS_ACTIVE,'Y')='Y'      
	and ( A.APP_VEHICLE_PRIN_OCC_ID in (11398,11925)  )    
	and A.VEHICLE_ID = @VEHICLE_ID  AND D.DRIVER_DRIV_TYPE='11941'

  
END     








GO

