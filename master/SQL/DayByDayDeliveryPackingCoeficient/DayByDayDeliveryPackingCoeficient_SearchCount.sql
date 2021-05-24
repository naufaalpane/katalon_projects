select count(1) FROM TB_M_DELIVERY_COEF
where 1=1
	  and ((@YYMM is null OR @YYMM = '') OR (YYMM = @YYMM ))