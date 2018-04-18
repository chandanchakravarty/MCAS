IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyDefaultTerritory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyDefaultTerritory]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name          	: Dbo.Proc_GetPolicyDefaultTerritory
Created by         	: Vijay Arora
Date               	: 16-11-2005
Purpose            	: To Get the default territory.   
Revison History 	:  
Used In                	:   Wolverine    
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE   PROCEDURE Proc_GetPolicyDefaultTerritory  
(  
 @CUSTOMER_ID int,  
 @POLICY_ID int,  
 @POLICY_VERSION_ID int,  
 @DWELLING_ID int  
)  
AS  
BEGIN  
SELECT A.terr TERRITORY FROM   
MNT_TERRITORY_CODES A ,  
POL_DWELLINGS_INFO B ,  
POL_LOCATIONS C  
WHERE   
B.LOCATION_ID=C.LOCATION_ID 
AND  
A.ZIP = C.LOC_ZIP 
AND   
B.CUSTOMER_ID=@CUSTOMER_ID  
AND   
B.POLICY_ID=@POLICY_ID 
AND  
B.POLICY_VERSION_ID=@POLICY_VERSION_ID 
AND  
B.DWELLING_ID=@DWELLING_ID 
AND A.LOBID=1  -- FOR HOME OWNERS
END





GO

