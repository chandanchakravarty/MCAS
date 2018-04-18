IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SaveAPP_MVR_INFORMATION_ACORD]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SaveAPP_MVR_INFORMATION_ACORD]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                          
                          
Proc Name       : Proc_SaveAPP_MVR_INFORMATION_ACORD                          
Created by      : Pradeep                
Date            : 10/27/2005                          
Purpose         :Insert of Driver MVR Information                          
Revison History :                          
Used In                   : Wolverine                          
------------------------------------------------------------                          
Date     Review By          Comments                          
------   ------------       -------------------------*/          
--drop proc Dbo.Proc_SaveAPP_MVR_INFORMATION_ACORD                          
CREATE PROC [dbo].[Proc_SaveAPP_MVR_INFORMATION_ACORD]  
(                          
 @APP_MVR_ID  int OUTPUT,                          
 @CUSTOMER_ID  int,                          
 @APP_ID  int,                          
 @APP_VERSION_ID int,                          
 @VIOLATION_ID  int,                          
 @VIOLATION_CODE  varchar(30),                          
 @DRIVER_ID  int,                          
 @MVR_AMOUNT  decimal(20,2),                
 --@MVR_DEATH  nvarchar(2),        
 @MVR_DEATH  nvarchar(1),                          
 @MVR_DATE  datetime,                      
 @IS_ACTIVE  nvarchar(2),    
 @POINTS_ASSIGNED int = null    
)                          
AS                          
BEGIN              
           
-- violation code manipulation for capital rating
DECLARE @FLGAVADJPNT NVARCHAR(20)
DECLARE @ADJUST_VIOLATION_POINTS NVARCHAR(20)
IF (CHARINDEX('^',@VIOLATION_CODE)>=0)
	BEGIN
		SET @FLGAVADJPNT=dbo.piece(@VIOLATION_CODE,'^',2)
		SET @VIOLATION_CODE=dbo.piece(@VIOLATION_CODE,'^',1)
	END	



---LOB CHECK --AUTO and MOTORCYCLE

Declare @LOBID int
SELECT        
 @LOBID = APP_LOB
FROM APP_LIST              
WHERE CUSTOMER_ID = @CUSTOMER_ID AND              
 APP_ID = @APP_ID AND              
 APP_VERSION_ID = @APP_VERSION_ID 


              
DECLARE @VIOLATION_IDS varchar(100)      
DECLARE @VIOLATION_TYPE_ID varchar(20)     
DECLARE @VIOLATION_TYPE varchar(20)
DECLARE @VIOLATION_POINT VARCHAR(10)     
    
    
         
/* Fetch the violation id of the selected violation  and the vioaltionid of the violation type(category)*/    
    
if(@VIOLATION_ID = 0)    
begin    
 EXEC Proc_GetVIOLATIONID_FROM_CODE_ACCORD  @VIOLATION_CODE,@CUSTOMER_ID, @APP_ID ,@APP_VERSION_ID ,@VIOLATION_IDS OUTPUT, @LOBID    
 SET @VIOLATION_TYPE_ID = dbo.piece(@VIOLATION_IDS,':',1)    
 SET @VIOLATION_TYPE = dbo.piece(@VIOLATION_IDS,':',2)
 SET @VIOLATION_POINT = DBO.PIECE(@VIOLATION_IDS,':',3)    
end    
else    
begin    
 SET @VIOLATION_TYPE_ID = '0'    
  SET @VIOLATION_TYPE = @VIOLATION_ID 
end    
   
-- FOR CAPITAL RATING 
IF(@FLGAVADJPNT='1')
BEGIN
	SET @ADJUST_VIOLATION_POINTS ='-' + @VIOLATION_POINT
END
ELSE
BEGIN
	SET @ADJUST_VIOLATION_POINTS =''
END

 IF(ISNULL(@POINTS_ASSIGNED,0) =0)
	BEGIN
		SET @POINTS_ASSIGNED = CONVERT(INT,@VIOLATION_POINT)
	END      
    
 /*Generating the new mvr id*/                          
 SELECT @APP_MVR_ID=ISNULL(MAX(APP_MVR_ID),0)+1 FROM APP_MVR_INFORMATION                          
               
  IF ( @VIOLATION_TYPE_ID IS NULL or @VIOLATION_TYPE_ID ='')              
  BEGIN              
   RETURN              
  END               
       
    
    
    
 IF( @VIOLATION_ID <> 15046 and @VIOLATION_ID <> 15047 and --Accident AUTO     
    @VIOLATION_ID <> 15048 and @VIOLATION_ID <> 15049  --Accident MOTOR    
    
    
 )    
    
 BEGIN    
    
       
   INSERT INTO APP_MVR_INFORMATION                
   (                          
     APP_MVR_ID,               
 CUSTOMER_ID,              
 APP_ID,               
 APP_VERSION_ID,               
    VIOLATION_ID,              
    DRIVER_ID,                          
    MVR_AMOUNT,               
    MVR_DEATH,              
    MVR_DATE,               
 IS_ACTIVE,      
 VIOLATION_TYPE  ,    
 OCCURENCE_DATE,    
 POINTS_ASSIGNED,
 ADJUST_VIOLATION_POINTS    
               
    )                          
    VALUES                          
    (              
    @APP_MVR_ID,              
    @CUSTOMER_ID,              
    @APP_ID,              
    @APP_VERSION_ID,        
    @VIOLATION_TYPE_ID,              
    @DRIVER_ID,                  
    @MVR_AMOUNT,              
    @MVR_DEATH,              
    @MVR_DATE,              
    @IS_ACTIVE,      
    @VIOLATION_TYPE ,    
    @MVR_DATE,    
    @POINTS_ASSIGNED,
	@ADJUST_VIOLATION_POINTS     
               
  )          
 End    
 END    

GO

