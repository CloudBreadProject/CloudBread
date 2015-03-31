
-- 관리자 페이지 최초 로그인 id 생성 - SHA512
INSERT INTO CloudBread.AdminMembers
(AdminMemberID, AdminMemberPWD, AdminMemberEmail, AdminGroup, TimeZoneID, Name1)
values(
	'CBAdmin'		--관리자의 ID고유값 
	, 'a/zEAmtfFieZptyDBcCducFnSsYWvVx0IqRfu20IFqwWMEfEeh9Cb09Ma1tQQsZx6rxP3HMQ/VsYPu9Z3CdGBA=='  --SHA512로 해쉬된 P@ssw0rd
	, 'youremail1@abc.com'		-- 메일주소
	, 'Admin'		-- 최고 관리 권한
	, 'Korea Standard Time'	--관리자 TimeZone
	, '임시관리자'	--이름
	)

--암호는 P@ssw0rd 
