SELECT COUNT(*) 
FROM TB_M_UNIT_PROD
WHERE 1=1
	AND ((@cfc is null OR @cfc = '') OR (CFC LIKE '%' + @cfc + '%' ))
	AND ((@partsno is null OR @partsno = '') OR (PART_NO like '%' +@partsno+ '%' ))
	AND ((@partsno2 is null OR @partsno2 = '') OR (SUBSTRING(PART_NO, 6, 5) like '%' +@partsno2+ '%' ))
	AND ((@partsno3 is null OR @partsno3 = '') OR (SUBSTRING(PART_NO, 11, 2) like '%' +@partsno3+ '%' ))
	AND ((@linecd is null OR @linecd = '') OR (LINE_CD LIKE '%' + @linecd + '%' ))
