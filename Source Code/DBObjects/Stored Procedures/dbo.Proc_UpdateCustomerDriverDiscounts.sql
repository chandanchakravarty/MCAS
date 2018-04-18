IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateCustomerDriverDiscounts]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateCustomerDriverDiscounts]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name       : dbo.Proc_UpdateDriverDiscounts
Created by      :Priya
Date            : 4/28/2005
Purpose         : To update the record in driver discount table
Revison History :
Used In         :   wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE  PROC Dbo.Proc_UpdateCustomerDriverDiscounts
(
@CustomerId      int,
@DRIVER_ID  	smallint,
@DISC_ID        smallint ,
@DISC_TYPE      nvarchar(5),
@DISC_PER	float,
@DISC_DATE	datetime
)
AS
BEGIN
Declare @Count int
select @Count =isnull(count(*),0) 
  from CLT_CUSTOMER_DRIVER_DISCOUNTS  
	where CUSTOMER_ID=@CustomerId   AND
	 DRIVER_ID=@DRIVER_ID and disc_type=@DISC_TYPE and DISC_ID<>@DISC_ID ;


if(@Count=0)
	begin
	UPDATE CLT_CUSTOMER_DRIVER_DISCOUNTS
	SET 
		DISC_TYPE=@DISC_TYPE,
                DISC_PER=@DISC_PER,
                DISC_DATE=@DISC_DATE
		
	WHERE
                CUSTOMER_ID=@CustomerId and
                DRIVER_ID=@DRIVER_ID and
       		DISC_ID=@DISC_ID
	end
else
	return -1;
end


GO

