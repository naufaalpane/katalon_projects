select count(1) FROM TB_M_PROD_COEF
where 1=1
	  and ((@company_cd is null OR @company_cd = '') OR (COMPANY_CD = @company_cd ))
	  and ((@process_cd is null OR @process_cd = '') OR (PROCESS_CD = @process_cd ))
	  and ((@yearmonth is null OR @yearmonth = '') OR (YYMM = @yearmonth ))