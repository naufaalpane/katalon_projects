delete from REPORT_ERROR
------------------------------------------3.1 
declare @@NOMOR               varchar(max)
declare @@DATAID 			 varchar(max)
declare @@Param_VERSION 			 varchar(max)
declare @@REVISIONNO 		 varchar(max)
declare @@IMPORTERDCD 		 varchar(max)
declare @@EXPORTERCD 		 varchar(max)
declare @@ODRTYPE 			 varchar(max)
declare @@PCMN 				 varchar(max)
declare @@CFC 				 varchar(max)
declare @@REEXPCD 			 varchar(max)
declare @@AICOMirCEPT 		 varchar(max)
declare @@PXPLAG 			 varchar(max)
declare @@IMPORTERNAME 		 varchar(max)
declare @@EXPORTERNAME 		 varchar(max)
declare @@SSNO 				 varchar(max)
declare @@LINECD 			 varchar(max)
declare @@PARTNO 			 varchar(max)
declare @@Lot_Code 			 varchar(max)
declare @@Exterior_Color 	 varchar(max)
declare @@Interior_Color 	 varchar(max)
declare @@Control_Mode_Code 	 varchar(max)
declare @@Display_Mode_Code 	 varchar(max)
declare @@ODRLOT 			 varchar(max)
declare @@Nmonth 			 varchar(max)
declare @@NmonthPLUS1 		 varchar(max)
declare @@NmonthPLUS2 		 varchar(max)
declare @@NmonthPLUS3 		 varchar(max)
declare @@1N 				 varchar(max)
declare @@2N 				 varchar(max)
declare @@3N 				 varchar(max)
declare @@4N 				 varchar(max)
declare @@5N 				 varchar(max)
declare @@6N 				 varchar(max)
declare @@7N 				 varchar(max)
declare @@8N 				 varchar(max)
declare @@9N 				 varchar(max)
declare @@10N 				 varchar(max)
declare @@11N 				 varchar(max)
declare @@12N 				 varchar(max)
declare @@13N 				 varchar(max)
declare @@14N 				 varchar(max)
declare @@15N 				 varchar(max)
declare @@16N 				 varchar(max)
declare @@17N 				 varchar(max)
declare @@18N 				 varchar(max)
declare @@19N 				 varchar(max)
declare @@20N 				 varchar(max)
declare @@21N 				 varchar(max)
declare @@22N 				 varchar(max)
declare @@23N 				 varchar(max)
declare @@24N 				 varchar(max)
declare @@25N 				 varchar(max)
declare @@26N 				 varchar(max)
declare @@27N 				 varchar(max)
declare @@28N 				 varchar(max)
declare @@29N 				 varchar(max)
declare @@30N 				 varchar(max)
declare @@31N 				 varchar(max)
declare @@1NPLUS1 			 varchar(max)
declare @@2NPLUS1 			 varchar(max)
declare @@3NPLUS1 			 varchar(max)
declare @@4NPLUS1 			 varchar(max)
declare @@5NPLUS1 			 varchar(max)
declare @@6NPLUS1 			 varchar(max)
declare @@7NPLUS1 			 varchar(max)
declare @@8NPLUS1 			 varchar(max)
declare @@9NPLUS1 			 varchar(max)
declare @@10NPLUS1 			 varchar(max)
declare @@11NPLUS1 			 varchar(max)
declare @@12NPLUS1 			 varchar(max)
declare @@13NPLUS1 			 varchar(max)
declare @@14NPLUS1 			 varchar(max)
declare @@15NPLUS1 			 varchar(max)
declare @@16NPLUS1 			 varchar(max)
declare @@17NPLUS1 			 varchar(max)
declare @@18NPLUS1 			 varchar(max)
declare @@19NPLUS1 			 varchar(max)
declare @@20NPLUS1 			 varchar(max)
declare @@21NPLUS1 			 varchar(max)
declare @@22NPLUS1 			 varchar(max)
declare @@23NPLUS1 			 varchar(max)
declare @@24NPLUS1 			 varchar(max)
declare @@25NPLUS1 			 varchar(max)
declare @@26NPLUS1 			 varchar(max)
declare @@27NPLUS1 			 varchar(max)
declare @@28NPLUS1 			 varchar(max)
declare @@29NPLUS1 			 varchar(max)
declare @@30NPLUS1 			 varchar(max)
declare @@31NPLUS1 			 varchar(max)
declare @@Transportation_Code varchar(max)
declare @@Order_Date 		 varchar(max)
declare @@Series 			 varchar(max)
declare @@Dummy 				 varchar(max)
declare @@Termination_Code 	 varchar(max)
declare @@RCVNO 				 varchar(max)
declare @@DTLNO 				 varchar(max)
declare @@ERRORFLAG 			 varchar(max)

declare cursor_1 CURSOR FAST_FORWARD
for 
select * from [dbo].[TB_T_EXPORT_ORDER_ATTACHMENT]
open cursor_1 
fetch next from cursor_1 INTO 
@@NOMOR              
,@@DATAID 			
,@@Param_VERSION 			
,@@REVISIONNO 		
,@@IMPORTERDCD 		
,@@EXPORTERCD 		
,@@ODRTYPE 			
,@@PCMN 				
,@@CFC 				
,@@REEXPCD 			
,@@AICOMirCEPT 		
,@@PXPLAG 			
,@@IMPORTERNAME 		
,@@EXPORTERNAME 		
,@@SSNO 				
,@@LINECD 			
,@@PARTNO 			
,@@Lot_Code 			
,@@Exterior_Color 	
,@@Interior_Color 	
,@@Control_Mode_Code 	
,@@Display_Mode_Code 	
,@@ODRLOT 			
,@@Nmonth 			
,@@NmonthPLUS1 		
,@@NmonthPLUS2 		
,@@NmonthPLUS3 		
,@@1N 				
,@@2N 				
,@@3N 				
,@@4N 				
,@@5N 				
,@@6N 				
,@@7N 				
,@@8N 				
,@@9N 				
,@@10N 				
,@@11N 				
,@@12N 				
,@@13N 				
,@@14N 				
,@@15N 				
,@@16N 				
,@@17N 				
,@@18N 				
,@@19N 				
,@@20N 				
,@@21N 				
,@@22N 				
,@@23N 				
,@@24N 				
,@@25N 				
,@@26N 				
,@@27N 				
,@@28N 				
,@@29N 				
,@@30N 				
,@@31N 				
,@@1NPLUS1 			
,@@2NPLUS1 			
,@@3NPLUS1 			
,@@4NPLUS1 			
,@@5NPLUS1 			
,@@6NPLUS1 			
,@@7NPLUS1 			
,@@8NPLUS1 			
,@@9NPLUS1 			
,@@10NPLUS1 			
,@@11NPLUS1 			
,@@12NPLUS1 			
,@@13NPLUS1 			
,@@14NPLUS1 			
,@@15NPLUS1 			
,@@16NPLUS1 			
,@@17NPLUS1 			
,@@18NPLUS1 			
,@@19NPLUS1 			
,@@20NPLUS1 			
,@@21NPLUS1 			
,@@22NPLUS1 			
,@@23NPLUS1 			
,@@24NPLUS1 			
,@@25NPLUS1 			
,@@26NPLUS1 			
,@@27NPLUS1 			
,@@28NPLUS1 			
,@@29NPLUS1 			
,@@30NPLUS1 			
,@@31NPLUS1 			
,@@Transportation_Code
,@@Order_Date 		
,@@Series 			
,@@Dummy 				
,@@Termination_Code 	
,@@RCVNO 				
,@@DTLNO 				
,@@ERRORFLAG 			
BEGIN
while @@@FETCH_STATUS = 0
BEGIN

------------------------------------------3.1 
if  (@@DATAID != 'MSO1')
begin
update TB_T_EXPORT_ORDER_ATTACHMENT set ERRORFLAG = 'ERROR' where NOMOR = @@NOMOR
insert into REPORT_ERROR
select @@IMPORTERDCD + '/' + @@EXPORTERCD + '/' + @@PCMN + '/' + @@ODRTYPE , @@CFC , @@PARTNO , @@ODRLOT, 'ERR' , @@Nmonth, @@NmonthPLUS1, @@NmonthPLUS2, @@NmonthPLUS3 , 'DataID Check Error(Except MSO1).'  
end

------------------------------------------3.2
if  (
      @@IMPORTERDCD is null
   or @@EXPORTERCD is null
   or @@ODRTYPE is null
   or @@PCMN is null
   or @@CFC is null
)
begin
update TB_T_EXPORT_ORDER_ATTACHMENT set ERRORFLAG = 'ERROR' where NOMOR = @@NOMOR
insert into REPORT_ERROR
select @@IMPORTERDCD + '/' + @@EXPORTERCD + '/' + @@PCMN + '/' + @@ODRTYPE , @@CFC , @@PARTNO , @@ODRLOT, 'ERR' , @@Nmonth, @@NmonthPLUS1, @@NmonthPLUS2, @@NmonthPLUS3 , 'OCN Check items error where required to input.'
end


if (
 @@ODRTYPE not in ('R','H','U','M','C')
or 
len(@@PCMN) <> 6  
or	left(@@PCMN,4) not between 1000 and 9999 
or right(@@PCMN,2) not between 1 and 31 
)
begin
update TB_T_EXPORT_ORDER_ATTACHMENT set ERRORFLAG = 'ERROR' where NOMOR = @@NOMOR
insert into REPORT_ERROR
select @@IMPORTERDCD + '/' + @@EXPORTERCD + '/' + @@PCMN + '/' + @@ODRTYPE , @@CFC , @@PARTNO , @@ODRLOT, 'ERR' , @@Nmonth, @@NmonthPLUS1, @@NmonthPLUS2, @@NmonthPLUS3 , 'OCN Value Error.'
end

---------------------------------------- 3.3

IF EXISTS(																						
     select * from  TB_R_ORDER_SPEC Spec where																					
		Spec.IMPORTER_CD = @@IMPORTERDCD And																				
		Spec.EXPORTER_CD = @@EXPORTERCD And																				
		Spec.ORD_TYPE    = @@ODRTYPE And																				
		Spec.PACK_MONTH  = @@PCMN And																				
		Spec.CFC         = @@CFC	   and																																			
		@@REVISIONNO = Spec.REVISION
) 																						
Begin		
update TB_T_EXPORT_ORDER_ATTACHMENT set ERRORFLAG = 'ERROR' where NOMOR = @@NOMOR
insert into REPORT_ERROR
select @@IMPORTERDCD + '/' + @@EXPORTERCD + '/' + @@PCMN + '/' + @@ODRTYPE , @@CFC , @@PARTNO , @@ODRLOT, 'ERR' , @@Nmonth, @@NmonthPLUS1, @@NmonthPLUS2, @@NmonthPLUS3 , 'Error in receiving orders because Revision was duplicated'
end		
																																																		
IF EXISTS(		
     select * from  TB_R_ORDER_SPEC Spec where																					
		Spec.IMPORTER_CD = @@IMPORTERDCD And																				
		Spec.EXPORTER_CD = @@EXPORTERCD And																				
		Spec.ORD_TYPE    = @@ODRTYPE And																				
		Spec.PACK_MONTH  = @@PCMN And																				
		Spec.CFC         = @@CFC	   and																																			
		@@REVISIONNO <= Spec.REVISION																	
) 																						
Begin			
update TB_T_EXPORT_ORDER_ATTACHMENT set ERRORFLAG = 'ERROR' where NOMOR = @@NOMOR
insert into REPORT_ERROR
select @@IMPORTERDCD + '/' + @@EXPORTERCD + '/' + @@PCMN + '/' + @@ODRTYPE , @@CFC , @@PARTNO , @@ODRLOT, 'ERR' , @@Nmonth, @@NmonthPLUS1, @@NmonthPLUS2, @@NmonthPLUS3 , 'Old Revision Recieved Error in the same OCN.'
end																						

-- 3.4
if (
@@Param_VERSION not in ('T','F','K','M','C')
)
begin
update TB_T_EXPORT_ORDER_ATTACHMENT set ERRORFLAG = 'ERROR' where NOMOR = @@NOMOR
insert into REPORT_ERROR
select @@IMPORTERDCD + '/' + @@EXPORTERCD + '/' + @@PCMN + '/' + @@ODRTYPE , @@CFC , @@PARTNO , @@ODRLOT, 'ERR' , @@Nmonth, @@NmonthPLUS1, @@NmonthPLUS2, @@NmonthPLUS3 , 'Version Check Error'
end

-- 3.5
if ( select count(1) from TB_T_EXPORT_ORDER_ATTACHMENT  where PCMN = @@PCMN  group by PCMN ) > 1
begin
update TB_T_EXPORT_ORDER_ATTACHMENT set ERRORFLAG = 'ERROR' where NOMOR = @@NOMOR
insert into REPORT_ERROR
select @@IMPORTERDCD + '/' + @@EXPORTERCD + '/' + @@PCMN + '/' + @@ODRTYPE , @@CFC , @@PARTNO , @@ODRLOT, 'ERR' , @@Nmonth, @@NmonthPLUS1, @@NmonthPLUS2, @@NmonthPLUS3 , 'Version Check Error'
end

if( @@PCMN <= left(convert(varchar(max),getdate(),112),6) )
begin
update TB_T_EXPORT_ORDER_ATTACHMENT set ERRORFLAG = 'ERROR' where NOMOR = @@NOMOR
insert into REPORT_ERROR
select @@IMPORTERDCD + '/' + @@EXPORTERCD + '/' + @@PCMN + '/' + @@ODRTYPE , @@CFC , @@PARTNO , @@ODRLOT, 'ERR' , @@Nmonth, @@NmonthPLUS1, @@NmonthPLUS2, @@NmonthPLUS3 , 'The Packing Month should be current month + 1' 
end

-------------------------------------------------------------------------------------------------------------------------------4 Format Check


--4.1
if (
   @@IMPORTERDCD   is null 
or @@EXPORTERCD	is null
or @@PCMN		is null
or @@CFC			is null
or @@REEXPCD		is null
or @@AICOMirCEPT	is null
)
begin
update TB_T_EXPORT_ORDER_ATTACHMENT set ERRORFLAG = 'ERROR' where NOMOR = @@NOMOR
insert into REPORT_ERROR
select @@IMPORTERDCD + '/' + @@EXPORTERCD + '/' + @@PCMN + '/' + @@ODRTYPE , @@CFC , @@PARTNO , @@ODRLOT, 'ERR' , @@Nmonth, @@NmonthPLUS1, @@NmonthPLUS2, @@NmonthPLUS3 , 'OCN : Check items error where required to input.' 
end

if (@@ODRTYPE		is null)  
begin
update TB_T_EXPORT_ORDER_ATTACHMENT set ERRORFLAG = 'ERROR' where NOMOR = @@NOMOR
insert into REPORT_ERROR
select @@IMPORTERDCD + '/' + @@EXPORTERCD + '/' + @@PCMN + '/' + @@ODRTYPE , @@CFC , @@PARTNO , @@ODRLOT, 'ERR' , @@Nmonth, @@NmonthPLUS1, @@NmonthPLUS2, @@NmonthPLUS3 , 'MSP Order Type : Check items error where required to input.' 
end
		
if (@@PXPLAG         is null)
begin
update TB_T_EXPORT_ORDER_ATTACHMENT set ERRORFLAG = 'ERROR' where NOMOR = @@NOMOR
insert into REPORT_ERROR
select @@IMPORTERDCD + '/' + @@EXPORTERCD + '/' + @@PCMN + '/' + @@ODRTYPE , @@CFC , @@PARTNO , @@ODRLOT, 'ERR' , @@Nmonth, @@NmonthPLUS1, @@NmonthPLUS2, @@NmonthPLUS3 , 'Set/PxPFlag : Check items error where required to input.' 
end


if (@@PARTNO       	is null) 
begin
update TB_T_EXPORT_ORDER_ATTACHMENT set ERRORFLAG = 'ERROR' where NOMOR = @@NOMOR
insert into REPORT_ERROR
select @@IMPORTERDCD + '/' + @@EXPORTERCD + '/' + @@PCMN + '/' + @@ODRTYPE , @@CFC , @@PARTNO , @@ODRLOT, 'ERR' , @@Nmonth, @@NmonthPLUS1, @@NmonthPLUS2, @@NmonthPLUS3 , 'Part No. : Check items error where required to input.' 
end

if (@@ODRLOT       	is null) 
begin
update TB_T_EXPORT_ORDER_ATTACHMENT set ERRORFLAG = 'ERROR' where NOMOR = @@NOMOR
insert into REPORT_ERROR
select @@IMPORTERDCD + '/' + @@EXPORTERCD + '/' + @@PCMN + '/' + @@ODRTYPE , @@CFC , @@PARTNO , @@ODRLOT, 'ERR' , @@Nmonth, @@NmonthPLUS1, @@NmonthPLUS2, @@NmonthPLUS3 , 'Order LOT : Check items error where required to input.' 
end


if (@@NmonthPLUS2   	is null) 
begin
update TB_T_EXPORT_ORDER_ATTACHMENT set ERRORFLAG = 'ERROR' where NOMOR = @@NOMOR
insert into REPORT_ERROR
select @@IMPORTERDCD + '/' + @@EXPORTERCD + '/' + @@PCMN + '/' + @@ODRTYPE , @@CFC , @@PARTNO , @@ODRLOT, 'ERR' , @@Nmonth, @@NmonthPLUS1, @@NmonthPLUS2, @@NmonthPLUS3 , 'N+2 Month Order Volume : Check items error where required to input.' 
end

if (@@NmonthPLUS3   	is null) 
begin
update TB_T_EXPORT_ORDER_ATTACHMENT set ERRORFLAG = 'ERROR' where NOMOR = @@NOMOR
insert into REPORT_ERROR
select @@IMPORTERDCD + '/' + @@EXPORTERCD + '/' + @@PCMN + '/' + @@ODRTYPE , @@CFC , @@PARTNO , @@ODRLOT, 'ERR' , @@Nmonth, @@NmonthPLUS1, @@NmonthPLUS2, @@NmonthPLUS3 , 'N+3 Month Order Volume : Check items error where required to input.' 
end

-- 4.2
if (
ISNUMERIC(@@ODRLOT) = 0
)
begin
update TB_T_EXPORT_ORDER_ATTACHMENT set ERRORFLAG = 'ERROR' where NOMOR = @@NOMOR
insert into REPORT_ERROR
select @@IMPORTERDCD + '/' + @@EXPORTERCD + '/' + @@PCMN + '/' + @@ODRTYPE , @@CFC , @@PARTNO , @@ODRLOT, 'ERR' , @@Nmonth, @@NmonthPLUS1, @@NmonthPLUS2, @@NmonthPLUS3 , 'Order LOT: Attribute Error.' 
end

if (
ISNUMERIC(@@Nmonth) = 0
)
begin
update TB_T_EXPORT_ORDER_ATTACHMENT set ERRORFLAG = 'ERROR' where NOMOR = @@NOMOR
insert into REPORT_ERROR
select @@IMPORTERDCD + '/' + @@EXPORTERCD + '/' + @@PCMN + '/' + @@ODRTYPE , @@CFC , @@PARTNO , @@ODRLOT, 'ERR' , @@Nmonth, @@NmonthPLUS1, @@NmonthPLUS2, @@NmonthPLUS3 , 'N Month Order Volume : Attribute Error.'
end

if (
 ISNUMERIC(@@NmonthPLUS1) = 0
)
begin
update TB_T_EXPORT_ORDER_ATTACHMENT set ERRORFLAG = 'ERROR' where NOMOR = @@NOMOR
insert into REPORT_ERROR
select @@IMPORTERDCD + '/' + @@EXPORTERCD + '/' + @@PCMN + '/' + @@ODRTYPE , @@CFC , @@PARTNO , @@ODRLOT, 'ERR' , @@Nmonth, @@NmonthPLUS1, @@NmonthPLUS2, @@NmonthPLUS3 , 'N+1 Month Order Volume : Attribute Error.'
end

if (
ISNUMERIC(@@NmonthPLUS2) = 0
)
begin
update TB_T_EXPORT_ORDER_ATTACHMENT set ERRORFLAG = 'ERROR' where NOMOR = @@NOMOR
insert into REPORT_ERROR
select @@IMPORTERDCD + '/' + @@EXPORTERCD + '/' + @@PCMN + '/' + @@ODRTYPE , @@CFC , @@PARTNO , @@ODRLOT, 'ERR' , @@Nmonth, @@NmonthPLUS1, @@NmonthPLUS2, @@NmonthPLUS3 , 'N+2 Month Order Volume : Attribute Error.'
end

if (
 ISNUMERIC(@@NmonthPLUS3) = 0
)
begin
update TB_T_EXPORT_ORDER_ATTACHMENT set ERRORFLAG = 'ERROR' where NOMOR = @@NOMOR
insert into REPORT_ERROR
select @@IMPORTERDCD + '/' + @@EXPORTERCD + '/' + @@PCMN + '/' + @@ODRTYPE , @@CFC , @@PARTNO , @@ODRLOT, 'ERR' , @@Nmonth, @@NmonthPLUS1, @@NmonthPLUS2, @@NmonthPLUS3 , 'N+3 Month Order Volume : Attribute Error.'
end

if  (
   ISNUMERIC(@@1N)    = 0
or ISNUMERIC(@@2N)	  = 0
or ISNUMERIC(@@3N)	  = 0
or ISNUMERIC(@@4N)	  = 0
or ISNUMERIC(@@5N)	  = 0
or ISNUMERIC(@@6N)	  = 0
or ISNUMERIC(@@7N)	  = 0
or ISNUMERIC(@@8N)	  = 0
or ISNUMERIC(@@9N)	  = 0
or ISNUMERIC(@@10N)	  = 0
or ISNUMERIC(@@11N)	  = 0
or ISNUMERIC(@@12N)	  = 0
or ISNUMERIC(@@13N)	  = 0
or ISNUMERIC(@@14N)	  = 0
or ISNUMERIC(@@15N)	  = 0
or ISNUMERIC(@@16N)	  = 0
or ISNUMERIC(@@17N)	  = 0
or ISNUMERIC(@@18N)	  = 0
or ISNUMERIC(@@19N)	  = 0
or ISNUMERIC(@@20N)	  = 0
or ISNUMERIC(@@21N)	  = 0
or ISNUMERIC(@@22N)	  = 0
or ISNUMERIC(@@23N)	  = 0
or ISNUMERIC(@@24N)	  = 0
or ISNUMERIC(@@25N)	  = 0
or ISNUMERIC(@@26N)	  = 0
or ISNUMERIC(@@27N)	  = 0
or ISNUMERIC(@@28N)	  = 0
or ISNUMERIC(@@29N)	  = 0
or ISNUMERIC(@@30N)	  = 0
or ISNUMERIC(@@31N)	  = 0
)

begin
update TB_T_EXPORT_ORDER_ATTACHMENT set ERRORFLAG = 'ERROR' where NOMOR = @@NOMOR
insert into REPORT_ERROR
select @@IMPORTERDCD + '/' + @@EXPORTERCD + '/' + @@PCMN + '/' + @@ODRTYPE , @@CFC , @@PARTNO , @@ODRLOT, 'ERR' , @@Nmonth, @@NmonthPLUS1, @@NmonthPLUS2, @@NmonthPLUS3 , 'Daily Order Volume (N Mon) : Attribute Error.'
end

if  (
   ISNUMERIC(@@1NPLUS1)    = 0
or ISNUMERIC(@@2NPLUS1)	  = 0
or ISNUMERIC(@@3NPLUS1)	  = 0
or ISNUMERIC(@@4NPLUS1)	  = 0
or ISNUMERIC(@@5NPLUS1)	  = 0
or ISNUMERIC(@@6NPLUS1)	  = 0
or ISNUMERIC(@@7NPLUS1)	  = 0
or ISNUMERIC(@@8NPLUS1)	  = 0
or ISNUMERIC(@@9NPLUS1)	  = 0
or ISNUMERIC(@@10NPLUS1)	  = 0
or ISNUMERIC(@@11NPLUS1)	  = 0
or ISNUMERIC(@@12NPLUS1)	  = 0
or ISNUMERIC(@@13NPLUS1)	  = 0
or ISNUMERIC(@@14NPLUS1)	  = 0
or ISNUMERIC(@@15NPLUS1)	  = 0
or ISNUMERIC(@@16NPLUS1)	  = 0
or ISNUMERIC(@@17NPLUS1)	  = 0
or ISNUMERIC(@@18NPLUS1)	  = 0
or ISNUMERIC(@@19NPLUS1)	  = 0
or ISNUMERIC(@@20NPLUS1)	  = 0
or ISNUMERIC(@@21NPLUS1)	  = 0
or ISNUMERIC(@@22NPLUS1)	  = 0
or ISNUMERIC(@@23NPLUS1)	  = 0
or ISNUMERIC(@@24NPLUS1)	  = 0
or ISNUMERIC(@@25NPLUS1)	  = 0
or ISNUMERIC(@@26NPLUS1)	  = 0
or ISNUMERIC(@@27NPLUS1)	  = 0
or ISNUMERIC(@@28NPLUS1)	  = 0
or ISNUMERIC(@@29NPLUS1)	  = 0
or ISNUMERIC(@@30NPLUS1)	  = 0
or ISNUMERIC(@@31NPLUS1)	  = 0
)

begin
update TB_T_EXPORT_ORDER_ATTACHMENT set ERRORFLAG = 'ERROR' where NOMOR = @@NOMOR
insert into REPORT_ERROR
select @@IMPORTERDCD + '/' + @@EXPORTERCD + '/' + @@PCMN + '/' + @@ODRTYPE , @@CFC , @@PARTNO , @@ODRLOT, 'ERR' , @@Nmonth, @@NmonthPLUS1, @@NmonthPLUS2, @@NmonthPLUS3 , 'Daily Order Volume (N+1 Mon) : Attribute Error.'
end

---- 4.3

if (
@@ODRTYPE = 'C'
)
begin
update TB_T_EXPORT_ORDER_ATTACHMENT set ERRORFLAG = 'ERROR' where NOMOR = @@NOMOR
insert into REPORT_ERROR
select @@IMPORTERDCD + '/' + @@EXPORTERCD + '/' + @@PCMN + '/' + @@ODRTYPE , @@CFC , @@PARTNO , @@ODRLOT, 'ERR' , @@Nmonth, @@NmonthPLUS1, @@NmonthPLUS2, @@NmonthPLUS3 , 'Order LOT : Volume unmatch (Monthly / Sum of Daily).'
end

if (
@@PXPLAG != 'P'
)
begin
update TB_T_EXPORT_ORDER_ATTACHMENT set ERRORFLAG = 'ERROR' where NOMOR = @@NOMOR
insert into REPORT_ERROR
select @@IMPORTERDCD + '/' + @@EXPORTERCD + '/' + @@PCMN + '/' + @@ODRTYPE , @@CFC , @@PARTNO , @@ODRLOT, 'ERR' , @@Nmonth, @@NmonthPLUS1, @@NmonthPLUS2, @@NmonthPLUS3 , 'Order LOT : Value Error.'
end


if (
 ISNUMERIC(@@ODRLOT) = 0 
)
begin
update TB_T_EXPORT_ORDER_ATTACHMENT set ERRORFLAG = 'ERROR' where NOMOR = @@NOMOR
insert into REPORT_ERROR
select @@IMPORTERDCD + '/' + @@EXPORTERCD + '/' + @@PCMN + '/' + @@ODRTYPE , @@CFC , @@PARTNO , @@ODRLOT, 'ERR' , @@Nmonth, @@NmonthPLUS1, @@NmonthPLUS2, @@NmonthPLUS3 , 'Order LOT : Value Error.'
end

if (
len(@@Nmonth) > 7
)
begin
update TB_T_EXPORT_ORDER_ATTACHMENT set ERRORFLAG = 'ERROR' where NOMOR = @@NOMOR
insert into REPORT_ERROR
select @@IMPORTERDCD + '/' + @@EXPORTERCD + '/' + @@PCMN + '/' + @@ODRTYPE , @@CFC , @@PARTNO , @@ODRLOT, 'ERR' , @@Nmonth, @@NmonthPLUS1, @@NmonthPLUS2, @@NmonthPLUS3 , 'N Month Order Volume : Value Error.'
end

if  (
 ISNUMERIC(@@Nmonth) = 0 or CHARINDEX(',' ,@@Nmonth ) = 1 or CHARINDEX('.' ,@@Nmonth ) = 1
)
begin
update TB_T_EXPORT_ORDER_ATTACHMENT set ERRORFLAG = 'ERROR' where NOMOR = @@NOMOR
insert into REPORT_ERROR
select @@IMPORTERDCD + '/' + @@EXPORTERCD + '/' + @@PCMN + '/' + @@ODRTYPE , @@CFC , @@PARTNO , @@ODRLOT, 'ERR' , @@Nmonth, @@NmonthPLUS1, @@NmonthPLUS2, @@NmonthPLUS3 , 'N Month Order Volume : Volume unmatch (Monthly / Sum of Daily)'
end


if (
 len(@@NmonthPLUS1) > 7
)
begin
update TB_T_EXPORT_ORDER_ATTACHMENT set ERRORFLAG = 'ERROR' where NOMOR = @@NOMOR
insert into REPORT_ERROR
select @@IMPORTERDCD + '/' + @@EXPORTERCD + '/' + @@PCMN + '/' + @@ODRTYPE , @@CFC , @@PARTNO , @@ODRLOT, 'ERR' , @@Nmonth, @@NmonthPLUS1, @@NmonthPLUS2, @@NmonthPLUS3 , 'N+1 Month Order Volume : Value Error.'
end

if (
ISNUMERIC(@@NmonthPLUS1) = 0 or CHARINDEX(',' ,@@NmonthPLUS1 ) = 1 or CHARINDEX('.' ,@@NmonthPLUS1 ) = 1
)
begin
update TB_T_EXPORT_ORDER_ATTACHMENT set ERRORFLAG = 'ERROR' where NOMOR = @@NOMOR
insert into REPORT_ERROR
select @@IMPORTERDCD + '/' + @@EXPORTERCD + '/' + @@PCMN + '/' + @@ODRTYPE , @@CFC , @@PARTNO , @@ODRLOT, 'ERR' , @@Nmonth, @@NmonthPLUS1, @@NmonthPLUS2, @@NmonthPLUS3 , 'N+1 Month Order Volume : Volume unmatch (Monthly / Sum of Daily)' 
end

if  (
len(@@NmonthPLUS2) > 7
)
begin
update TB_T_EXPORT_ORDER_ATTACHMENT set ERRORFLAG = 'ERROR' where NOMOR = @@NOMOR
insert into REPORT_ERROR
select @@IMPORTERDCD + '/' + @@EXPORTERCD + '/' + @@PCMN + '/' + @@ODRTYPE , @@CFC , @@PARTNO , @@ODRLOT, 'ERR' , @@Nmonth, @@NmonthPLUS1, @@NmonthPLUS2, @@NmonthPLUS3 , 'N+2 Month Order Volume : Value Error.'
end

if (
 ISNUMERIC(@@NmonthPLUS2) = 0 or CHARINDEX(',' ,@@NmonthPLUS2 ) = 1 or CHARINDEX('.' ,@@NmonthPLUS2 ) = 1
)
begin
update TB_T_EXPORT_ORDER_ATTACHMENT set ERRORFLAG = 'ERROR' where NOMOR = @@NOMOR
insert into REPORT_ERROR
select @@IMPORTERDCD + '/' + @@EXPORTERCD + '/' + @@PCMN + '/' + @@ODRTYPE , @@CFC , @@PARTNO , @@ODRLOT, 'ERR' , @@Nmonth, @@NmonthPLUS1, @@NmonthPLUS2, @@NmonthPLUS3 , 'N+2 Month Order Volume : Volume unmatch (Monthly / Sum of Daily)'
end


if (
 len(@@NmonthPLUS3) > 7
)
begin
update TB_T_EXPORT_ORDER_ATTACHMENT set ERRORFLAG = 'ERROR' where NOMOR = @@NOMOR
insert into REPORT_ERROR
select @@IMPORTERDCD + '/' + @@EXPORTERCD + '/' + @@PCMN + '/' + @@ODRTYPE , @@CFC , @@PARTNO , @@ODRLOT, 'ERR' , @@Nmonth, @@NmonthPLUS1, @@NmonthPLUS2, @@NmonthPLUS3 , 'N+3 Month Order Volume : Value Error.'
end

if (
 ISNUMERIC(@@NmonthPLUS3) = 0 or CHARINDEX(',' ,@@NmonthPLUS3 ) = 1 or CHARINDEX('.' ,@@NmonthPLUS3 ) = 1
)
begin
update TB_T_EXPORT_ORDER_ATTACHMENT set ERRORFLAG = 'ERROR' where NOMOR = @@NOMOR
insert into REPORT_ERROR
select @@IMPORTERDCD + '/' + @@EXPORTERCD + '/' + @@PCMN + '/' + @@ODRTYPE , @@CFC , @@PARTNO , @@ODRLOT, 'ERR' , @@Nmonth, @@NmonthPLUS1, @@NmonthPLUS2, @@NmonthPLUS3 , 'N+3 Month Order Volume : Volume unmatch (Monthly / Sum of Daily'
end

fetch next from cursor_1 INTO 
@@NOMOR              
,@@DATAID 			
,@@Param_VERSION 			
,@@REVISIONNO 		
,@@IMPORTERDCD 		
,@@EXPORTERCD 		
,@@ODRTYPE 			
,@@PCMN 				
,@@CFC 				
,@@REEXPCD 			
,@@AICOMirCEPT 		
,@@PXPLAG 			
,@@IMPORTERNAME 		
,@@EXPORTERNAME 		
,@@SSNO 				
,@@LINECD 			
,@@PARTNO 			
,@@Lot_Code 			
,@@Exterior_Color 	
,@@Interior_Color 	
,@@Control_Mode_Code 	
,@@Display_Mode_Code 	
,@@ODRLOT 			
,@@Nmonth 			
,@@NmonthPLUS1 		
,@@NmonthPLUS2 		
,@@NmonthPLUS3 		
,@@1N 				
,@@2N 				
,@@3N 				
,@@4N 				
,@@5N 				
,@@6N 				
,@@7N 				
,@@8N 				
,@@9N 				
,@@10N 				
,@@11N 				
,@@12N 				
,@@13N 				
,@@14N 				
,@@15N 				
,@@16N 				
,@@17N 				
,@@18N 				
,@@19N 				
,@@20N 				
,@@21N 				
,@@22N 				
,@@23N 				
,@@24N 				
,@@25N 				
,@@26N 				
,@@27N 				
,@@28N 				
,@@29N 				
,@@30N 				
,@@31N 				
,@@1NPLUS1 			
,@@2NPLUS1 			
,@@3NPLUS1 			
,@@4NPLUS1 			
,@@5NPLUS1 			
,@@6NPLUS1 			
,@@7NPLUS1 			
,@@8NPLUS1 			
,@@9NPLUS1 			
,@@10NPLUS1 			
,@@11NPLUS1 			
,@@12NPLUS1 			
,@@13NPLUS1 			
,@@14NPLUS1 			
,@@15NPLUS1 			
,@@16NPLUS1 			
,@@17NPLUS1 			
,@@18NPLUS1 			
,@@19NPLUS1 			
,@@20NPLUS1 			
,@@21NPLUS1 			
,@@22NPLUS1 			
,@@23NPLUS1 			
,@@24NPLUS1 			
,@@25NPLUS1 			
,@@26NPLUS1 			
,@@27NPLUS1 			
,@@28NPLUS1 			
,@@29NPLUS1 			
,@@30NPLUS1 			
,@@31NPLUS1 			
,@@Transportation_Code
,@@Order_Date 		
,@@Series 			
,@@Dummy 				
,@@Termination_Code 	
,@@RCVNO 				
,@@DTLNO 				
,@@ERRORFLAG 			
END
END
close cursor_1
deallocate cursor_1

if exists (select * from [TB_T_EXPORT_ORDER_ATTACHMENT] where ERRORFLAG is not null)
begin
select 'ERROR' [RESULT]
return
end
else
begin
select 'SUCCESS' [RESULT] end

--select * from REPORT_ERROR
--select * from TB_T_EXPORT_ORDER_ATTACHMENT
--delete REPORT_ERROR

