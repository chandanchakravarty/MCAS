IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetUMRule_Boat]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetUMRule_Boat]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*======================================================================================================    
Proc Name                : Dbo.Proc_GetUMRule_Boat     
Created by               : Ashwani                                                                                                
Date                     : 12 Oct,2006                                                
Purpose                  : To get the Boat Info for UM rules                                                
Revison History          :                                                                                                
Used In                  : Wolverine                                                                                                
======================================================================================================    
Date     Review By          Comments                                                                                                
=====  ==============   =============================================================================*/   

--DROP PROC Proc_GetUMRule_Boat
CREATE proc dbo.Proc_GetUMRule_Boat                                                                                                                                                                        
(                                                                                                                                                                            
 @CUSTOMERID    int,                                                                                                                                                                            
 @APPID    int,                                                                                                                                                                            
 @APPVERSIONID   int,                                                                                                                                                                  
 @BOATID int                                                                                                                                   
)                                                                                                                                                                    
AS                                                                                                                                                                                
BEGIN        
	 declare @BOAT_NO varchar(20)--int      
	 declare @MAX_SPEED int --dec      
	 declare @TYPE_OF_WATERCRAFT char(10)      
	 declare @YEAR varchar(20)--int      
	 declare @LENGTH nvarchar(10)      
	 declare @WATERS_NAVIGATED varchar(125)      
	 declare @INSURING_VALUE varchar(20) --dec      
	 declare @USED_PARTICIPATE char(8) -- int      
	 declare @WATERCRAFT_CONTEST varchar(125)       
	 declare @WATERCRAFT_HORSE_POWER varchar(20)  --int          
	      
	 IF EXISTS(SELECT CUSTOMER_ID FROM APP_UMBRELLA_WATERCRAFT_INFO WITH(NOLOCK)      
	 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID AND BOAT_ID=@BOATID)  BEGIN                                                              
	 SELECT  @BOAT_NO = ISNULL(CONVERT(VARCHAR(20),BOAT_NO),''),@MAX_SPEED = ISNULL(CONVERT(int,MAX_SPEED),''),      
	 @TYPE_OF_WATERCRAFT=ISNULL(TYPE_OF_WATERCRAFT,''),@YEAR=ISNULL(CONVERT(VARCHAR(20),YEAR),''),      
	 @LENGTH=ISNULL(LENGTH,''),@WATERS_NAVIGATED=ISNULL(WATERS_NAVIGATED,''),@INSURING_VALUE = ISNULL(CONVERT(VARCHAR(20),INSURING_VALUE),''),      
	 @USED_PARTICIPATE = ISNULL(CONVERT(VARCHAR(20),USED_PARTICIPATE),''),@WATERCRAFT_CONTEST=ISNULL(WATERCRAFT_CONTEST,''),      
	 @WATERCRAFT_HORSE_POWER = ISNULL(CONVERT(VARCHAR(20),WATERCRAFT_HORSE_POWER),'') 
	 FROM   APP_UMBRELLA_WATERCRAFT_INFO   WITH(NOLOCK)                                          
	 WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID   AND BOAT_ID=@BOATID                                                                                                               
END         
    
--If limit is under $300,000 then look at the Type of Watercraft on the Watercraft Details Tab 
--If Type of Watercraft is 
--Canoe 
--Paddleboat 
--Row Boat   Then refer to underwriters  

DECLARE @WATERCRAFT_TYPE VARCHAR
DECLARE @PERSONAL_LIABILITY VARCHAR(100)
DECLARE @WATERCRAFT_LIABILITY VARCHAR(100)
SELECT  @PERSONAL_LIABILITY  = COVERAGE_AMOUNT FROM APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES WITH(NOLOCK)
WHERE CUSTOMER_ID=@CUSTOMERID and APP_ID= @APPID and APP_VERSION_ID = @APPVERSIONID  AND COV_CODE='PL' 
SELECT  @WATERCRAFT_LIABILITY  = COVERAGE_AMOUNT FROM APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES WITH(NOLOCK)
WHERE CUSTOMER_ID=@CUSTOMERID and APP_ID= @APPID and APP_VERSION_ID = @APPVERSIONID  AND COV_CODE='LCCSL'  
IF((convert(int,convert(money,@PERSONAL_LIABILITY)) < convert(int,'300000')) OR (convert(int,convert(money,@WATERCRAFT_LIABILITY)) < convert(int,'300000')))
BEGIN
	IF(@TYPE_OF_WATERCRAFT=11376 OR @TYPE_OF_WATERCRAFT =11756 OR @TYPE_OF_WATERCRAFT=11377)
		BEGIN 
			SET @WATERCRAFT_TYPE='Y'
		END
	ELSE
		BEGIN
			SET @WATERCRAFT_TYPE='N'
		END
END
PRINT @WATERCRAFT_TYPE
-- Watercraft Tab Verify the list of  watercraft If the Length Field is less than 16 feet       
-- Then look at the Max Speed Field If  0 to 55 Then look at the horsepower field If over 160 then       
-- Refer to underwriters                                                                             
 declare @WC_LEN_MAXSPD_HORSEPWR char      
 set @WC_LEN_MAXSPD_HORSEPWR='N'      
      
if(@LENGTH< '16' and @MAX_SPEED between '0' and '55' and @WATERCRAFT_HORSE_POWER >'160')      
 set @WC_LEN_MAXSPD_HORSEPWR = 'Y'      
      
-- Watercraft Tab Verify the list of  watercraft If the Length Field is 16 to  26ft Then look at the Max Speed Field       
-- If  0 to 55 Then look at the horsepower field If over 260 then Refer to underwriters       
 declare @WC_LEN_MAXSPD_HPOVER char      
set @WC_LEN_MAXSPD_HPOVER ='N'      
      
if(@LENGTH between '16' and '26' and @MAX_SPEED between '0' and '55' and @WATERCRAFT_HORSE_POWER >'260')      
 set @WC_LEN_MAXSPD_HPOVER = 'Y'      
      
-- Watercraft Tab Verify the list of  watercraft If the Length Field is 27 to 40 ft Then look at the Max Speed Field       
-- If 0 to 55 Then look at the horsepower field If over 600 then Refer to underwriters        
      
 declare @WC_LEN_MAXSPD_HPOVER600 char      
 set @WC_LEN_MAXSPD_HPOVER600 ='N'      
      
if(@LENGTH between '27' and '40' and @MAX_SPEED between '0' and '55' and @WATERCRAFT_HORSE_POWER >'600')      
 set @WC_LEN_MAXSPD_HPOVER600 = 'Y'      
      
-- Watercraft Tab Verify the list of  watercraft If the Length Field is over 40 ft Then look at the Max Speed Field       
--If  0 to 55 Then look at the horsepower field If over 600 then Refer to underwriters       
      
 declare @WC_LENOVER40_MAXSPD_HPOVER600 char      
 set @WC_LENOVER40_MAXSPD_HPOVER600 ='N'      
      
if(@LENGTH  > '40' and @MAX_SPEED between '0' and '55' and @WATERCRAFT_HORSE_POWER >'600')      
 set @WC_LENOVER40_MAXSPD_HPOVER600 = 'Y'      
      
      
-- Watercraft Tab Verify the list of  watercraft If the Length Field is 16 to 26 feet Then look at the Max Speed Field       
-- If 56 to 65 Then look at the horsepower field If over 600 then Refer to underwriters       
    
 declare @WC_LEN_MAXSPD56_HPOVER600 char      
 set @WC_LEN_MAXSPD56_HPOVER600 ='N'      
      
if( @LENGTH between '16' and '26' and @MAX_SPEED between '56' and '65' and @WATERCRAFT_HORSE_POWER >'600')        
 set @WC_LEN_MAXSPD56_HPOVER600 = 'Y'     
     
-- Watercraft Tab Verify the list of  watercraft If the Length Field is lies between 27 to  40 feet Then look at the Max Speed Field       
-- If 56 to 65 Then look at the horsepower field If over 600 then Refer to underwriters     
      
declare @WC_LEN27_MAXSPD56_HPOVER600 char      
 set @WC_LEN27_MAXSPD56_HPOVER600 ='N'      
if( @LENGTH between '27' and '40' and @MAX_SPEED between '56' and '65' and @WATERCRAFT_HORSE_POWER >'600')       
set @WC_LEN27_MAXSPD56_HPOVER600 = 'Y'      
      
-- Watercraft Tab Verify the list of  watercraft If the Length Field is lies over 40 feet Then look at the Max Speed Field       
-- If 56 to 65 Then look at the horsepower field If over 600 then Refer to underwriters       
    
declare @WC_LEN40_MAXSPD56_HPOVER600 char      
 set @WC_LEN40_MAXSPD56_HPOVER600 ='N'      
if( @LENGTH > '40' and @MAX_SPEED between '56' and '65' and @WATERCRAFT_HORSE_POWER >'600')       
set @WC_LEN40_MAXSPD56_HPOVER600 = 'Y'      
      
-- Watercraft Details If any of the watercraft list have yes to this field Watercraft Used to participate in      
--  any race speed  or fishing contest ? * Then refer to underwriters       
 declare @RF_USED_PARTICIPATE char      
 set @RF_USED_PARTICIPATE='N'      
       
 if(@USED_PARTICIPATE='10963')      
 set @RF_USED_PARTICIPATE='Y'      
select      
-- Mnadatory         
@BOAT_NO as BOAT_NO ,      
@MAX_SPEED as MAX_SPEED ,      
@TYPE_OF_WATERCRAFT as TYPE_OF_WATERCRAFT,      
@YEAR as YEAR,      
@LENGTH as LENGTH,      
--@WATERS_NAVIGATED as WATERS_NAVIGATED,      
@INSURING_VALUE as INSURING_VALUE,      
      
-- Rules                                
@USED_PARTICIPATE as USED_PARTICIPATE,      
@RF_USED_PARTICIPATE AS RF_USED_PARTICIPATE,      
case  @USED_PARTICIPATE when 'Y'      
then @WATERCRAFT_CONTEST      
end as WATERCRAFT_CONTEST ,                                           
@WC_LEN_MAXSPD_HORSEPWR as WC_LEN_MAXSPD_HORSEPWR,      
@WC_LEN_MAXSPD_HPOVER as WC_LEN_MAXSPD_HPOVER,      
@WC_LEN_MAXSPD_HPOVER600 as WC_LEN_MAXSPD_HPOVER600,      
@WC_LENOVER40_MAXSPD_HPOVER600 as WC_LENOVER40_MAXSPD_HPOVER600,      
@WC_LEN_MAXSPD56_HPOVER600 as WC_LEN_MAXSPD56_HPOVER600,      
@WC_LEN27_MAXSPD56_HPOVER600 as WC_LEN27_MAXSPD56_HPOVER600,      
@WC_LEN40_MAXSPD56_HPOVER600 as WC_LEN40_MAXSPD56_HPOVER600 ,
@WATERCRAFT_TYPE AS WATERCRAFT_TYPE 
end



GO

