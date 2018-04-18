IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCustomerLookup]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCustomerLookup]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

    /*----------------------------------------------------------  
Proc Name          : Dbo.Proc_GetCustomerLookup  
Created by         : Pradeep  
Date               : Sep 16, 2005  
Purpose            : Gets the Lookup values used in Customer page  
Revison History    :  
Used In            :   Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
--drop proc Proc_GetCustomerLookup 2
------   ------------       -------------------------*/  
CREATE  PROC [dbo].[Proc_GetCustomerLookup]  
@LANG_ID INT = 1 --Added by Charles on 20-Apr-2010 for Multilingual Implementation
AS  
  
--Get Reason codes Lookup  
exec Proc_GetLookupValues @LookupCode = N'RCFC' , @LANG_ID = @LANG_ID --@LANG_ID Added by Charles on 20-Apr-2010 for Multilingual Implementation
  
--Get Salutation lookup  
exec Proc_GetLookupValues @LookupCode = N'%SAL' , @LANG_ID = @LANG_ID --@LANG_ID Added by Charles on 20-Apr-2010 for Multilingual Implementation
  
--Get Customer types  
exec Proc_GetLookupValues @LookupCode = N'CUSTYPE' , @LANG_ID= @LANG_ID  --@LANG_ID Added by Charles on 20-Apr-2010 for Multilingual Implementation
  
--Get Business types  
--exec Proc_GetLookupValues @LookupCode = N'%SIC'  

GO

