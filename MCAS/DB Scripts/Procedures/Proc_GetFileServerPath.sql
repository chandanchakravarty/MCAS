CREATE Proc [Proc_GetFileServerPath] 
As
SET FMTONLY OFF;
BEGIN
select FileServerPath  FROM MNT_SYS_PARAMS 
END




