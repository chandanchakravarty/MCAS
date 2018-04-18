-- drop proc dbo.Proc_InsertACT_REG_COMM_SETUP          
create PROC dbo.Proc_InsertACT_REG_COMM_SETUP          
(          
@COMM_ID     int out,          
@AGENCY_ID    int,          
@COUNTRY_ID     int,          
@STATE_ID     smallint,          
@LOB_ID     smallint,          
@SUB_LOB_ID     smallint,          
@CLASS_RISK     int,          
@TERM     char(1),          
@EFFECTIVE_FROM_DATE     datetime,          
@EFFECTIVE_TO_DATE     datetime,          
@COMMISSION_PERCENT     decimal(10,3),          
@COMMISSION_TYPE char(1),          
@REMARKS varchar(250),          
@IS_ACTIVE     nchar(2),          
@CREATED_BY     int,          
@CREATED_DATETIME     datetime,          
@MODIFIED_BY     int,          
@LAST_UPDATED_DATETIME     datetime,        
@AMOUNT_TYPE CHAR(1)          
)          
AS          
BEGIN          
set @STATE_ID=92      
  -- checking if to date overlaps with any record of same group          
  -- checking if from date overlaps with any record of same group    
       
 if @COMMISSION_TYPE='R'          
 begin           
  if exists (          
           select * from ACT_REG_COMM_SETUP where           
                  (STATE_ID  = @STATE_ID           
        and LOB_ID  =  @LOB_ID           
        and SUB_LOB_ID  = @SUB_LOB_ID           
        and CLASS_RISK  = @CLASS_RISK           
        and TERM    =  @TERM      
  and COMMISSION_TYPE = @COMMISSION_TYPE )           
      AND          
        (           
    (@EFFECTIVE_FROM_DATE BETWEEN EFFECTIVE_FROM_DATE  AND EFFECTIVE_TO_DATE)          
         OR           
    (@EFFECTIVE_TO_DATE BETWEEN EFFECTIVE_FROM_DATE  AND EFFECTIVE_TO_DATE)          
                OR          
    (@EFFECTIVE_FROM_DATE  < EFFECTIVE_FROM_DATE AND @EFFECTIVE_TO_DATE > EFFECTIVE_TO_DATE)          
               )          
     )          
  begin            
   SELECT @COMM_ID = -2 --dates overlap           
   return -- Exiting procedure            
  end          
 end          
 if @COMMISSION_TYPE='A'          
 begin           
  if exists (          
           select * from ACT_REG_COMM_SETUP where           
                  (          
        AGENCY_ID  =  @AGENCY_ID and          
        STATE_ID  = @STATE_ID           
        and LOB_ID  =  @LOB_ID           
        and SUB_LOB_ID  = @SUB_LOB_ID           
        and CLASS_RISK  = @CLASS_RISK           
        and TERM    =  @TERM      
  and COMMISSION_TYPE = @COMMISSION_TYPE )           
      AND          
        (           
    (@EFFECTIVE_FROM_DATE BETWEEN EFFECTIVE_FROM_DATE  AND EFFECTIVE_TO_DATE)          
         OR           
    (@EFFECTIVE_TO_DATE BETWEEN EFFECTIVE_FROM_DATE  AND EFFECTIVE_TO_DATE)          
                OR          
    (@EFFECTIVE_FROM_DATE  < EFFECTIVE_FROM_DATE AND @EFFECTIVE_TO_DATE > EFFECTIVE_TO_DATE)          
               )          
     )          
  begin            
   SELECT @COMM_ID = -2 --dates overlap           
   return -- Exiting procedure            
  end          
 end          
 if @COMMISSION_TYPE='P'          
 begin           
  if exists (          
           select * from ACT_REG_COMM_SETUP where           
                  (STATE_ID  = @STATE_ID           
        and LOB_ID  =  @LOB_ID           
        and SUB_LOB_ID  = @SUB_LOB_ID           
        and CLASS_RISK  = @CLASS_RISK           
        and TERM    =  @TERM       
  and COMMISSION_TYPE = @COMMISSION_TYPE)           
      AND          
        (           
    (@EFFECTIVE_FROM_DATE BETWEEN EFFECTIVE_FROM_DATE  AND EFFECTIVE_TO_DATE)          
         OR           
    (@EFFECTIVE_TO_DATE BETWEEN EFFECTIVE_FROM_DATE  AND EFFECTIVE_TO_DATE)          
                OR          
    (@EFFECTIVE_FROM_DATE  < EFFECTIVE_FROM_DATE AND @EFFECTIVE_TO_DATE > EFFECTIVE_TO_DATE)          
               )          
     )          
  begin            
   SELECT @COMM_ID = -2 --dates overlap           
   return -- Exiting procedure            
  end          
 end          
--*****************************        
 if @COMMISSION_TYPE='C'  -- Complete App Bonus        
 begin          
  if exists (          
           select * from ACT_REG_COMM_SETUP where           
                  (STATE_ID  = @STATE_ID           
        and LOB_ID  =  @LOB_ID           
        and AMOUNT_TYPE = @AMOUNT_TYPE           
        and TERM    =  @TERM       
  and COMMISSION_TYPE = @COMMISSION_TYPE)           
      AND          
        (           
    (@EFFECTIVE_FROM_DATE BETWEEN EFFECTIVE_FROM_DATE  AND EFFECTIVE_TO_DATE)          
         OR           
    (@EFFECTIVE_TO_DATE BETWEEN EFFECTIVE_FROM_DATE  AND EFFECTIVE_TO_DATE)          
                OR          
    (@EFFECTIVE_FROM_DATE  < EFFECTIVE_FROM_DATE AND @EFFECTIVE_TO_DATE > EFFECTIVE_TO_DATE)          
               )          
     )          
  begin            
   SELECT @COMM_ID = -2 --dates overlap           
   return -- Exiting procedure            
  end          
 end          
--*****************************        
          
  select  @COMM_ID=isnull(Max(COMM_ID),0)+1 from ACT_REG_COMM_SETUP          
  INSERT INTO ACT_REG_COMM_SETUP          
  (          
  COMM_ID,          
  AGENCY_ID,          
  COUNTRY_ID,          
  STATE_ID,          
  LOB_ID,          
  SUB_LOB_ID,          
  CLASS_RISK,          
  TERM,          
  EFFECTIVE_FROM_DATE,          
  EFFECTIVE_TO_DATE,          
  COMMISSION_PERCENT,          
  COMMISSION_TYPE,          
  REMARKS,          
  IS_ACTIVE,          
  CREATED_BY,          
  CREATED_DATETIME,          
  MODIFIED_BY,          
  LAST_UPDATED_DATETIME,        
  AMOUNT_TYPE          
  )          
  VALUES          
  (          
  @COMM_ID,          
  @AGENCY_ID,          
  @COUNTRY_ID,          
  @STATE_ID,          
  @LOB_ID,          
  @SUB_LOB_ID,          
  @CLASS_RISK,          
  @TERM,          
  @EFFECTIVE_FROM_DATE,          
  @EFFECTIVE_TO_DATE,          
  @COMMISSION_PERCENT,          
  @COMMISSION_TYPE,          
  @REMARKS,          
  @IS_ACTIVE,          
  @CREATED_BY,          
  @CREATED_DATETIME,          
  @MODIFIED_BY,          
  @LAST_UPDATED_DATETIME,        
  @AMOUNT_TYPE          
  )          
end          
      
        
      
      
    
  