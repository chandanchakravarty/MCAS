IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fun_DecriptText]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fun_DecriptText]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                      
Function Name :  dbo.fun_DecriptText                                   
CREATED BY      : Pravesh K Chandel         
DATE            : 28 may 2008    
PURPOSE         : TO Encript any text                                       
REVISON HISTORY :                                      
USED IN         : WOLVERINE 
*/
create function [dbo].[fun_DecriptText]
(
@textvalue nvarchar(4000)
) returns  nvarchar(800)
as
begin

DECLARE @Passphrase varchar(128)
DECLARE @passphasekey as varbinary(max) 
SET @Passphrase = 'Ebix Wolverine Mutual' 

SET @passphasekey = cast(@textvalue as varbinary(max))
return convert(varchar(max),DecryptByPassPhrase(@Passphrase,@passphasekey) )

end




GO

