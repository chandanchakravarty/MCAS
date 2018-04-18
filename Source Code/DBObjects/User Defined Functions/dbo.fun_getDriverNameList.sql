IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fun_getDriverNameList]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fun_getDriverNameList]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/* Function   
created By  : Pravesh K chandel  
Created Date : 6 May 09  
Purpose   : To get a string drivername List of Pol id Pol Version Id from prior loss Table  
*/  
CREATE FUNCTION dbo.fun_getDriverNameList(@List Text)   
RETURNS @Table table(  
CUSTOMER_ID int,  
APP_POL_ID INT,  
VERSION_ID INT,  
DRIVER_ID  INT  
)  
AS    
BEGIN   
  
Declare @Pos int  
Declare @SPos int  
Declare @COUNTER int  
declare @Delimiter char(1)  
set @Delimiter ='^'  
  
declare @driverName nvarchar(50)  
  
Set @Pos = CharIndex(@Delimiter,@List,0)  
Set @SPos = 1  
SET @COUNTER=1  
While @Pos > 0  
Begin  
   IF(@COUNTER=1)  
  Insert Into @Table(CUSTOMER_ID) Values(Substring(@List,@SPos,@Pos-@SPos))  
 ELSE IF(@COUNTER=2)  
 UPDATE @Table SET APP_POL_ID= Substring(@List,@SPos,@Pos-@SPos)  
 ELSE IF(@COUNTER=3)  
 UPDATE @Table SET VERSION_ID= Substring(@List,@SPos,@Pos-@SPos)  
 ELSE IF(@COUNTER=4)  
 UPDATE @Table SET DRIVER_ID = Substring(@List,@SPos,@Pos-@SPos)  
  
    Set @SPos = @Pos + 1  
 Set @COUNTER = @COUNTER + 1  
    Set @Pos = CharIndex(@Delimiter,@List,@Pos+1)  
End  

Return 
  
END  


GO

