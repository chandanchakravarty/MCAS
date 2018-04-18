IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DELETE_HOME_ENDORSEMENTS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DELETE_HOME_ENDORSEMENTS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       : dbo.Proc_DELETE_HOME_ENDORSEMENTS  
Created by      : Pradeep  
Date            : 10/13/2005  
Purpose     	: Deletes records in Dwelling_Endorsements   
Revison History :  
Used In  : Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE            PROC Dbo.Proc_DELETE_HOME_ENDORSEMENTS  
(  
	 @CUSTOMER_ID     int,  
	 @APP_ID     int,  
	 @APP_VERSION_ID     smallint,  
	 @DWELLING_ID smallint,  
	 @ENDORSEMENT_ID int,  
	 @DWELLING_ENDORSEMENT_ID Int
)  
AS  
  


BEGIN  
   
	IF EXISTS
	(
		SELECT * FROM APP_DWELLING_ENDORSEMENTS
		WHERE CUSTOMER_ID = @CUSTOMER_ID and   
			   APP_ID=@APP_ID and   
			   APP_VERSION_ID = @APP_VERSION_ID   
			   and DWELLING_ID = @DWELLING_ID  AND
				ENDORSEMENT_ID = @ENDORSEMENT_ID AND
			   DWELLING_ENDORSEMENT_ID = @DWELLING_ENDORSEMENT_ID
	)
	BEGIN
		DELETE FROM APP_DWELLING_ENDORSEMENTS
		WHERE CUSTOMER_ID = @CUSTOMER_ID and   
			   APP_ID=@APP_ID and   
			   APP_VERSION_ID = @APP_VERSION_ID   
			   and DWELLING_ID = @DWELLING_ID  AND
				ENDORSEMENT_ID = @ENDORSEMENT_ID AND
			   DWELLING_ENDORSEMENT_ID = @DWELLING_ENDORSEMENT_ID
	END	
	
END	
 
  
  








GO

