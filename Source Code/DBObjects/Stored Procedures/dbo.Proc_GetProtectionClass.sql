IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetProtectionClass]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetProtectionClass]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                      
Proc Name        : dbo.Proc_GetProtectionClass                  
Created by       : PRAVEEN KASANA                      
Date             : 7 JUNE 2006                      
Purpose          : RETRIEVING THE PROTECTION CLASS    
Revison History  :                     
 Used In         : Wolverine  

Modified : Praveen
Date             : 14 feb 2008 
purpose : If Protection class is any and Distance to the 
Fire Hydrant is over 1000 and # of Miles from Fire Station > 5,
 rated class comes as 10 else it rated class is same as protection class.


Added Note Date : 29 Sep 2008 #Itrack 3596
If Protection class is any and Distance to the Fire Hydrant is over 1000 
and # of Miles from Fire Station > 5,
 rated class comes as 10 else it rated class is same as protection class.

*/   


 


--   drop proc dbo.Proc_GetProtectionClass  
CREATE PROC dbo.Proc_GetProtectionClass  
(                  
@FIREPROTECTIONCLASS VARCHAR(10),      
@MILESTODWELLING INT,    
@FEETTOHYDRANT VARCHAR(30),    
@LOB VARCHAR(10),    
@CALLEDFROM varchar(10)='QQ'   
)    
AS                      
BEGIN                   
    
DECLARE @MAXMILESTODWELL AS INT    
DECLARE @RATEDCLASS nVARCHAR(20)    
SET @MAXMILESTODWELL = 5    
DECLARE @ISOCLASS VARCHAR(10)    
SET @ISOCLASS = '8B' 
    
/*CASE1 RENTAL DWELLING:    
  * When Miles to dwelling is greater than 5,    
  * then protection class chages to 10, no matter what class you have selected    
  * Position() = last() = 10 Protection class */    
IF(@LOB='REDW')    
BEGIN    
	 IF(@MILESTODWELLING > @MAXMILESTODWELL)    
	 BEGIN    
	     SET  @RATEDCLASS = '10'    
	 END    
	 ELSE    
	 BEGIN    
		    SET  @RATEDCLASS = @FIREPROTECTIONCLASS    
		 --------    
		-- IF(@FEETTOHYDRANT='Over 1000' or @FEETTOHYDRANT='11556')    
		 -- IF(@FIREPROTECTIONCLASS!='10')      
		  --  SET  @RATEDCLASS = '09'    
	 END    
	 
	IF(@FIREPROTECTIONCLASS = @ISOCLASS)    
	 BEGIN    
	    SET  @RATEDCLASS = '09'    
	 END    

	 IF(@CALLEDFROM = 'QQ')    
	      SELECT @RATEDCLASS AS RATEDCLASS    
	  ELSE    
	 	RETURN @RATEDCLASS    
	    
END    
ELSE-----------------------------------HOME---------------------------------    
BEGIN    
  /*CASE1 HOME:*/    
-- DECLARE @ISOCLASS VARCHAR(10)    
-- SET @ISOCLASS = '8B'    
IF(@MILESTODWELLING > @MAXMILESTODWELL)    
 BEGIN    
 IF(@FIREPROTECTIONCLASS = @ISOCLASS)    
 BEGIN    
    SET  @RATEDCLASS = '09'    
 END    
 ELSE    
 BEGIN    
    SET  @RATEDCLASS = '10'    
 END    
 END    
 ELSE    
   BEGIN    
 IF(@FIREPROTECTIONCLASS = @ISOCLASS)    
 BEGIN    
    SET  @RATEDCLASS = '09'    
 END    
          ELSE    
 BEGIN    
   SET  @RATEDCLASS = @FIREPROTECTIONCLASS    
 END    
 END    
    
  IF(@CALLEDFROM = 'QQ')    
          SELECT @RATEDCLASS AS RATEDCLASS    
  ELSE    
   RETURN @RATEDCLASS    
 END    
   
END   

              
    
    
 






GO

