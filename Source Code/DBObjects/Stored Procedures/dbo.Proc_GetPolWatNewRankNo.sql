IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolWatNewRankNo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolWatNewRankNo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
Proc Name           : dbo.Proc_GetPolWatNewRankNo      
Created by          : Swastika Gaur      
Date                : 28/04/2005      
Purpose             : To get the new additional rank no.     
Revison History     :      
Used In             :   Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
--DROP PROC dbo.Proc_GetPolWatNewRankNo 
CREATE PROC dbo.Proc_GetPolWatNewRankNo      
@CUSTOMER_ID INT,      
@POLICY_ID INT,      
@POLICY_VERSION_ID INT,      
@CALLEDFROM varchar(4)       
as      
BEGIN      
      
 if (@CALLEDFROM ='WAT' or @CALLEDFROM='HOME')      
 begin      
  SELECT    (isnull(MAX(RANK),0)) +1 as RANK      
  FROM         POL_WATERCRAFT_COV_ADD_INT WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID      
 end      
      
END      
   


GO

