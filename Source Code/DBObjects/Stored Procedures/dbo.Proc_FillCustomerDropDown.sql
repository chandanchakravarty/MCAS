IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FillCustomerDropDown]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FillCustomerDropDown]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO






/*----------------------------------------------------------
Proc Name       : dbo.Proc_FillCustomerDropDown
Created by      : 	Ajit Singh Chahal
Date                : 	04/20/2005
Purpose         : 	To fill drop down of Agency Names
Revison History :
Used In         :  	 Wolverine
modified by: Nidhi
Modified Date : 05/09/2005

------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE     PROCEDURE [dbo].[Proc_FillCustomerDropDown] AS
begin
select CUSTOMER_ID, isnull(CUSTOMER_FIRST_NAME,'')+' ' +isnull(CUSTOMER_MIDDLE_NAME,'')+' '+ isnull(CUSTOMER_LAST_NAME ,'') as CUSTOMER_FIRST_NAME,CUSTOMER_TYPE from CLT_CUSTOMER_LIST 
where upper(isnull(IS_ACTIVE,'Y')) = 'Y' 
order by  CUSTOMER_FIRST_NAME
End



GO

