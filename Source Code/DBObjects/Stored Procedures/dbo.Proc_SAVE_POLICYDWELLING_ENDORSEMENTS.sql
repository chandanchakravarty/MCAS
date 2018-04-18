IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SAVE_POLICYDWELLING_ENDORSEMENTS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SAVE_POLICYDWELLING_ENDORSEMENTS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       : dbo.Proc_SAVE_POLICYDWELLING_ENDORSEMENTS  
Created by      : Anurag Verma
Date            : 11/16/2005  
Purpose     	: Inserts/Updates records in POL_Dwelling_Endorsements   
Revison History :  
Used In  : Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE            PROC Dbo.Proc_SAVE_POLICYDWELLING_ENDORSEMENTS  
(  
	 @CUSTOMER_ID     int,  
	 @POL_ID     int,  
	 @POL_VERSION_ID     smallint,  
	 @DWELLING_ID smallint,  
	 @ENDORSEMENT_ID int,  
	 @REMARKS NVarChar(500),
	 @DWELLING_ENDORSEMENT_ID Int
)  
AS  
  
DECLARE @DWELLING_END_ID_MAX int  

BEGIN  
   
	IF EXISTS
	(
		SELECT * FROM POL_DWELLING_ENDORSEMENTS
		WHERE CUSTOMER_ID = @CUSTOMER_ID and   
			   POLICY_ID=@POL_ID and   
			   POLICY_VERSION_ID = @POL_VERSION_ID   
			   and DWELLING_ID = @DWELLING_ID  AND
				ENDORSEMENT_ID = @ENDORSEMENT_ID
	)
	BEGIN
		UPDATE POL_DWELLING_ENDORSEMENTS
		SET REMARKS = @REMARKS
		WHERE CUSTOMER_ID = @CUSTOMER_ID and   
			   POLICY_ID=@POL_ID and   
			   POLICY_VERSION_ID = @POL_VERSION_ID   
			   and DWELLING_ID = @DWELLING_ID  AND
				ENDORSEMENT_ID = @ENDORSEMENT_ID
	END	
	ELSE
	BEGIN
		
		select  @DWELLING_END_ID_MAX = isnull(Max(DWELLING_ENDORSEMENT_ID),0)+1 
			from POL_DWELLING_ENDORSEMENTS  
		  where CUSTOMER_ID = @CUSTOMER_ID and   
		   POLICY_ID=@POL_ID and   
		   POLICY_VERSION_ID = @POL_VERSION_ID   
		   and DWELLING_ID = @DWELLING_ID 
	
		INSERT INTO POL_DWELLING_ENDORSEMENTS
		(
			CUSTOMER_ID,
			POLICY_ID,
			POLICY_VERSION_ID,
			DWELLING_ID,
			ENDORSEMENT_ID,
			REMARKS,
			DWELLING_ENDORSEMENT_ID
		)
		VALUES
		(
			@CUSTOMER_ID,
			@POL_ID,
			@POL_VERSION_ID,
			@DWELLING_ID,
			@ENDORSEMENT_ID,
			@REMARKS,
			@DWELLING_ENDORSEMENT_ID
		)
	
	
	END
END	
 
  
  










GO

