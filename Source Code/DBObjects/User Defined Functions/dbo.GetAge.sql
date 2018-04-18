IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAge]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetAge]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*  
Create By : Pravesh K. Chandel  
dated  : 11 Jan 2007  
purpose  : to deference of two date (to get age)  
*/  
  
Create FUNCTION dbo.GetAge (@DOB datetime, @Today Datetime) RETURNS Int  
AS   
Begin  
Declare @Age As Int  
Set @Age = Year(@Today) - Year(@DOB)  
If Month(@Today) < Month(@DOB)   
Set @Age = @Age -1  
If Month(@Today) = Month(@DOB) and Day(@Today) < Day(@DOB)   
Set @Age = @Age - 1  
Return @AGE  
End  


GO

