IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Piece]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[Piece]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


Create FUNCTION Piece ( @CharacterExpression VARCHAR(8000), @Delimiter CHAR(1), @Position INTEGER)  

RETURNS VARCHAR(8000)  
  
AS  
  
BEGIN  
  
If @Position<1 return null  
  
if len(@Delimiter)<>1 return null  
  
declare @Start integer  
  
set @Start=1  
  
while @Position>1  
  
BEGIN  
  
Set @Start=ISNULL(CHARINDEX(@Delimiter, @CharacterExpression, @Start),0)  
  
IF @Start=0 return null  
  
set @position= @position-1  
  
set @Start=@Start+1  
  
END  
  
Declare @End INTEGER  
  
Set @End= ISNULL(CHARINDEX(@Delimiter, @CharacterExpression, @Start),0)  
  
If @End=0 Set @End=LEN(@CharacterExpression)+1  
  

RETURN SUBSTRING(@CharacterExpression, @Start, @End-@Start)  
  
END  
  
  
  



GO

