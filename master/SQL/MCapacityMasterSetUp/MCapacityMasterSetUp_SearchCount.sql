SELECT COUNT(*) 
FROM TB_M_CAPACITY A 
	INNER JOIN TB_M_LINE B ON A.LINE_CD = B.LINE_CD AND A.COMPANY_CD = B.COMPANY_CD
	INNER JOIN TB_M_HEIJUNKA C ON A.HEIJUNKA_CD = C.HEIJUNKA_CD AND A.COMPANY_CD = C.COMPANY_CD
WHERE 1=1
	AND ((@proccd is null OR @proccd = '') OR (B.PROCESS_CD like '%' +@proccd+'%' ))
	AND ((@linecd is null OR @linecd = '') OR (B.LINE_CD like '%' +@linecd+'%' ))
	AND ((@hjkcd is null OR @hjkcd = '') OR (C.HEIJUNKA_CD like '%' +@hjkcd+'%' ))
