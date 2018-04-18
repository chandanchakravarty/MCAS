 -- drop proc dbo.Proc_UpdateACT_REG_COMM_SETUP    
CREATE PROC dbo.Proc_UpdateACT_REG_COMM_SETUP    
(    
@COMM_ID     int out,    
@AGENCY_ID   int,    
@COUNTRY_ID     int,    
@STATE_ID     smallint,    
@LOB_ID     smallint,    
@SUB_LOB_ID     smallint,    
@CLASS_RISK     int,    
@TERM     char(1),    
@EFFECTIVE_FROM_DATE     datetime,    
@EFFECTIVE_TO_DATE     datetime,    
@COMMISSION_PERCENT     decimal(10,3),    
@REMARKS varchar(250),    
@MODIFIED_BY     int,    
@LAST_UPDATED_DATETIME     datetime,    
@COMMISSION_TYPE varchar(2)=null,  
@AMOUNT_TYPE CHAR(1)    
)    
AS    
BEGIN    
--Checking if any other record exists for same combination of Agency Commission Group.    
 Declare @Count int  
 set @STATE_ID=92  
-- Set @Count= (SELECT count(*) FROM ACT_REG_COMM_SETUP WHERE    
-- STATE_ID  = @STATE_ID     
-- and LOB_ID  =  @LOB_ID     
-- and SUB_LOB_ID  = @SUB_LOB_ID     
-- and CLASS_RISK  = @CLASS_RISK     
-- and TERM    =  @TERM     
-- and EFFECTIVE_FROM_DATE = @EFFECTIVE_FROM_DATE         
-- and EFFECTIVE_TO_DATE   = @EFFECTIVE_TO_DATE    
-- and COMM_ID <> @COMM_ID)    
--     
--Added By Shafi    
    
if @COMMISSION_TYPE='R'    
 Set @Count= (SELECT count(*) FROM ACT_REG_COMM_SETUP WHERE    
 STATE_ID  = @STATE_ID     
 and LOB_ID  =  @LOB_ID     
 and SUB_LOB_ID  = @SUB_LOB_ID     
 and CLASS_RISK  = @CLASS_RISK     
 and TERM    =  @TERM     
 and EFFECTIVE_FROM_DATE = @EFFECTIVE_FROM_DATE         
 and EFFECTIVE_TO_DATE   = @EFFECTIVE_TO_DATE  
 and COMMISSION_TYPE = @COMMISSION_TYPE    
  and COMM_ID <> @COMM_ID)    
  
if @COMMISSION_TYPE='A'    
 Set @Count= (SELECT count(*) FROM ACT_REG_COMM_SETUP WHERE    
 AGENCY_ID  =  @AGENCY_ID and    
 STATE_ID  = @STATE_ID     
 and LOB_ID  =  @LOB_ID     
 and SUB_LOB_ID  = @SUB_LOB_ID     
 and CLASS_RISK  = @CLASS_RISK     
 and TERM    =  @TERM     
 and EFFECTIVE_FROM_DATE = @EFFECTIVE_FROM_DATE         
 and EFFECTIVE_TO_DATE   = @EFFECTIVE_TO_DATE   
 and COMMISSION_TYPE = @COMMISSION_TYPE   
  and COMM_ID <> @COMM_ID)    
    
if @COMMISSION_TYPE='P'    
 Set @Count= (SELECT count(*) FROM ACT_REG_COMM_SETUP WHERE    
 STATE_ID  = @STATE_ID     
 and LOB_ID  =  @LOB_ID     
 and EFFECTIVE_FROM_DATE = @EFFECTIVE_FROM_DATE         
 and EFFECTIVE_TO_DATE   = @EFFECTIVE_TO_DATE  
 and COMMISSION_TYPE = @COMMISSION_TYPE    
  and COMM_ID <> @COMM_ID)    
    
if @COMMISSION_TYPE='C'    
 Set @Count= (SELECT count(*) FROM ACT_REG_COMM_SETUP WHERE    
 STATE_ID  = @STATE_ID     
 and LOB_ID  =  @LOB_ID     
 and CLASS_RISK  = @CLASS_RISK     
 and TERM    =  @TERM   
 and AMOUNT_TYPE = @AMOUNT_TYPE       
 and EFFECTIVE_FROM_DATE = @EFFECTIVE_FROM_DATE         
 and EFFECTIVE_TO_DATE   = @EFFECTIVE_TO_DATE   
 and COMMISSION_TYPE = @COMMISSION_TYPE  
 and COMM_ID <> @COMM_ID)    
     
if(@Count=0)    
BEGIN    
  -- checking if to date overlaps with any record of same group    
  -- checking if from date overlaps with any record of same group    
   if @COMMISSION_TYPE='R'    
   BEGIN     
  if exists (    
     select * from ACT_REG_COMM_SETUP where     
     (STATE_ID  = @STATE_ID     
     and LOB_ID  =  @LOB_ID     
     and SUB_LOB_ID  = @SUB_LOB_ID     
     and CLASS_RISK  = @CLASS_RISK  
  and COMMISSION_TYPE = @COMMISSION_TYPE     
     and TERM    =  @TERM  
  )     
     AND    
     (     
     (@EFFECTIVE_FROM_DATE BETWEEN EFFECTIVE_FROM_DATE  AND EFFECTIVE_TO_DATE)    
     OR     
     (@EFFECTIVE_TO_DATE BETWEEN EFFECTIVE_FROM_DATE  AND EFFECTIVE_TO_DATE)    
     OR    
     (@EFFECTIVE_FROM_DATE  < EFFECTIVE_FROM_DATE AND @EFFECTIVE_TO_DATE > EFFECTIVE_TO_DATE)    
     )    
     and COMM_ID <> @COMM_ID    
         
     )    
     BEGIN      
   SELECT @COMM_ID = -2 --DATES OVERLAP     
      RETURN -- EXITING PROCEDURE      
     END    
   END    
   IF @COMMISSION_TYPE='A'    
   BEGIN     
    IF EXISTS (    
     SELECT * FROM ACT_REG_COMM_SETUP WHERE     
     (    
     AGENCY_ID  =  @AGENCY_ID AND    
     STATE_ID  = @STATE_ID     
     AND LOB_ID  =  @LOB_ID     
     AND SUB_LOB_ID  = @SUB_LOB_ID     
     AND CLASS_RISK  = @CLASS_RISK   
  and COMMISSION_TYPE = @COMMISSION_TYPE    
     AND TERM    =  @TERM )     
     AND    
     (     
     (@EFFECTIVE_FROM_DATE BETWEEN EFFECTIVE_FROM_DATE  AND EFFECTIVE_TO_DATE)    
     OR     
     (@EFFECTIVE_TO_DATE BETWEEN EFFECTIVE_FROM_DATE  AND EFFECTIVE_TO_DATE)    
     OR    
 (@EFFECTIVE_FROM_DATE  < EFFECTIVE_FROM_DATE AND @EFFECTIVE_TO_DATE > EFFECTIVE_TO_DATE)    
   )    
     AND COMM_ID <> @COMM_ID    
     )    
     BEGIN      
      SELECT @COMM_ID = -2 --DATES OVERLAP     
      RETURN -- EXITING PROCEDURE      
     END    
   END    
   IF @COMMISSION_TYPE='P'    
   BEGIN     
    IF EXISTS (    
    SELECT * FROM ACT_REG_COMM_SETUP WHERE     
    (STATE_ID  = @STATE_ID     
    AND LOB_ID  =  @LOB_ID     
    AND SUB_LOB_ID  = @SUB_LOB_ID     
    AND CLASS_RISK  = @CLASS_RISK  
 and COMMISSION_TYPE = @COMMISSION_TYPE     
    AND TERM    =  @TERM )     
    AND    
    (     
    (@EFFECTIVE_FROM_DATE BETWEEN EFFECTIVE_FROM_DATE  AND EFFECTIVE_TO_DATE)    
    OR     
    (@EFFECTIVE_TO_DATE BETWEEN EFFECTIVE_FROM_DATE  AND EFFECTIVE_TO_DATE)    
    OR    
    (@EFFECTIVE_FROM_DATE  < EFFECTIVE_FROM_DATE AND @EFFECTIVE_TO_DATE > EFFECTIVE_TO_DATE)    
    )    
    AND COMM_ID <> @COMM_ID    
    )    
    BEGIN      
     SELECT @COMM_ID = -2 --DATES OVERLAP     
     RETURN -- EXITING PROCEDURE      
    END    
   END    
  
   IF @COMMISSION_TYPE='C'    
   BEGIN     
    IF EXISTS (    
    SELECT * FROM ACT_REG_COMM_SETUP WHERE     
    (STATE_ID  = @STATE_ID     
    AND LOB_ID  =  @LOB_ID     
    AND AMOUNT_TYPE = @AMOUNT_TYPE  
 and COMMISSION_TYPE = @COMMISSION_TYPE  
    AND TERM =  @TERM )     
    AND    
    (     
    (@EFFECTIVE_FROM_DATE BETWEEN EFFECTIVE_FROM_DATE  AND EFFECTIVE_TO_DATE)    
    OR     
    (@EFFECTIVE_TO_DATE BETWEEN EFFECTIVE_FROM_DATE  AND EFFECTIVE_TO_DATE)    
    OR    
    (@EFFECTIVE_FROM_DATE  < EFFECTIVE_FROM_DATE AND @EFFECTIVE_TO_DATE > EFFECTIVE_TO_DATE)    
    )    
    AND COMM_ID <> @COMM_ID    
    )    
    BEGIN      
  SELECT @COMM_ID = -2 --DATES OVERLAP     
     RETURN -- EXITING PROCEDURE      
    END    
 END    
   Update ACT_REG_COMM_SETUP    
   set       
   COUNTRY_ID= @COUNTRY_ID,    
   AGENCY_ID = @AGENCY_ID,    
   STATE_ID= @STATE_ID,    
   LOB_ID=@LOB_ID ,    
   SUB_LOB_ID= @SUB_LOB_ID,    
   CLASS_RISK=@CLASS_RISK ,    
   TERM= @TERM,    
   EFFECTIVE_FROM_DATE=@EFFECTIVE_FROM_DATE ,    
   EFFECTIVE_TO_DATE= @EFFECTIVE_TO_DATE,    
   COMMISSION_PERCENT= @COMMISSION_PERCENT,    
   REMARKS = @REMARKS,    
   MODIFIED_BY= @MODIFIED_BY,    
   LAST_UPDATED_DATETIME = @LAST_UPDATED_DATETIME,  
   AMOUNT_TYPE = @AMOUNT_TYPE    
   where COMM_ID = @COMM_ID    
 END    
ELSE    
 BEGIN    
/*Record already exist*/    
   SELECT @COMM_ID = -1    
   return    
 END     
    
end    
    
  
  
  
  
  
  
  