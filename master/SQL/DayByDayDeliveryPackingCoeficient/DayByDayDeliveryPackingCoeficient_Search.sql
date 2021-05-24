select * from (
select ROW_NUMBER() over(order by YYMM asc ) r,
	   COMPANY_CD,
	   DEST_CD,
	   PROCESS_CD,
	   YYMM

from TB_M_DELIVERY_COEF
where 1=1
	  and ((@YYMM is null OR @YYMM = '') OR (YYMM = @YYMM ))
)
tb
where 1 = 1 and tb.r between @RowStart and @RowEnd