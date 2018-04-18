IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateMasterLOB]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateMasterLOB]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name       : dbo.Proc_UpdateMasterLOB
Created by      : Priya
Date            : 6/24/2005
Purpose         : To update record in Master LOB table
Revison History :
Used In         :   Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC dbo.Proc_UpdateMasterLOB
(
	@LOB_ID  		INT,
	@LOB_PREFIX  		varchar(10),
	@LOB_SUFFIX		varchar(10),
	@LOB_SEED		int
)
AS

BEGIN
	IF NOT EXISTS (SELECT POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST WHERE POLICY_LOB = @LOB_ID ) 
	BEGIN
		UPDATE MNT_LOB_MASTER
		SET 
	    
		  LOB_PREFIX = @LOB_PREFIX  ,                      
		  LOB_SUFFIX = @LOB_SUFFIX ,                   
		  LOB_SEED   = @LOB_SEED                   
		
		  
		WHERE 
		 LOB_ID = @LOB_ID 
		return 1
	END
	ELSE
BEGIN
		return -2
END
END


GO

