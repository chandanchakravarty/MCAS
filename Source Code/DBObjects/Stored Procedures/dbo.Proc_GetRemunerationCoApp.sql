IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetRemunerationCoApp]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetRemunerationCoApp]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------      
Proc Name          : Dbo.Proc_GetRemunerationCoApp      
Created by         : Lalit Kr Chauhan	 
Date               : 21-feb-2011    
Purpose            :       
Revison History :      
Used In            :   Ebix Advantage Web
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
CREATE   PROCEDURE [dbo].[Proc_GetRemunerationCoApp]
(      
 @CUSTOMER_ID int,      
 @POLICY_ID int,      
 @POLICY_VERSION_ID int      
)      
AS      
BEGIN      	
		
	SELECT * FROM  POL_REMUNERATION wITH(NOLOCK)
	WHERE       
	   CUSTOMER_ID=@CUSTOMER_ID    AND       
	   POLICY_ID=@POLICY_ID              AND      
	   POLICY_VERSION_ID =@POLICY_VERSION_ID      
	      
END      
    
GO

