 IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetLookupvaluesFromUniqueID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetLookupvaluesFromUniqueID]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO


 
 /*----------------------------------------------------------    
Proc Name       : [Proc_GetLookupvaluesFromUniqueID]    
Created by      : Agniswar Das    
Date            : 7/17/2011    
Purpose			: Demo    
Revison History :    
Used In        : Singapore    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/   


-- Proc_GetLookupvaluesFromUniqueID 901
CREATE PROC dbo.Proc_GetLookupvaluesFromUniqueID
(@LOOKUP_UNIQUE_ID int)

AS

SELECT * from MNT_LOOKUP_VALUES 
where LOOKUP_UNIQUE_ID = @LOOKUP_UNIQUE_ID