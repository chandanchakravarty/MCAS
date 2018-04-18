IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCountListType]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCountListType]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO





/*----------------------------------------------------------      
Proc Name	: dbo.Proc_GetCountListType
Created by    	: Anurag Verma      
Date            : 17/03/2005      
Purpose         : To retrieve TOCOUNT OF APPOINTED ACCORDING TO TODOLIST TYPES
Used In         : Wolverine      

Modification History 
------------------------------------------------------------      
Date		: June 30, 2005
Modified By	: Anshuman
Comments      	: selecting records on the basis of customer_id if passed
------   ------------       -------------------------*/      
CREATE   PROC Dbo.Proc_GetCountListType
(      
	@USER_ID      nvarchar(4),
	@CUSTOMER_ID	nvarchar(4) = null
)      
AS      
SET NOCOUNT ON
BEGIN
	if(@USER_ID = '-1')
	begin
		if(@CUSTOMER_ID is null or @CUSTOMER_ID = '')
		begin
			select count(listtypeid) 'Counting',listtypeid from todolist tdl where listopen='Y' group by listtypeid
		end
		else
		begin
			select count(listtypeid) 'Counting',listtypeid from todolist tdl where listopen='Y' and CUSTOMER_ID = @CUSTOMER_ID group by listtypeid
		end
	end
	else
	begin
		if(@CUSTOMER_ID is null or @CUSTOMER_ID = '')
		begin
			select count(listtypeid) 'Counting',listtypeid from todolist tdl where touserid=@USER_ID and listopen='Y' group by listtypeid
		end
		else
		begin
			select count(listtypeid) 'Counting',listtypeid from todolist tdl where touserid=@USER_ID and listopen='Y' and CUSTOMER_ID=@CUSTOMER_ID group by listtypeid
		end
	end
END
SET NOCOUNT OFF




GO

