IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fun_EncriptText]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fun_EncriptText]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                      
Function Name :  dbo.fun_EncriptText                                   
CREATED BY      : Pravesh K Chandel         
DATE            : 28 may 2008    
PURPOSE         : TO Encript any text                                       
REVISON HISTORY :                                      
USED IN         : WOLVERINE 
*/

create function [dbo].[fun_EncriptText]
(
@textvalue varchar(800)
) returns  varbinary(8000)
as
begin

DECLARE @Passphrase varchar(128), @Mytext varchar(128) 
DECLARE @passphasekey as varbinary(max) 
SET @Passphrase = 'Ebix Wolverine Mutual' 
SET @Mytext = @textvalue
SET @passphasekey = EncryptByPassPhrase(@Passphrase,@Mytext) 

return  @passphasekey


end



GO

