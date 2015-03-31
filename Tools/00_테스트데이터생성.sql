--USE master
--go
--sp_who
--kill 51
--drop database CloudBreadDB;
--create database CloudBreadDB;
use CloudBreadDB
go

/*
-- 암호화 처리 CLR		-- SQL Server에서만 되고 Azure SQL에서는 사용 불가 - 테스트 용도로 제작
EXEC dbo.sp_configure 'clr enabled',1 
RECONFIGURE WITH OVERRIDE
GO

create assembly SQLServerEncryption from 'c:\SQLDLL\SQLCLRCrypt.dll' WITH PERMISSION_SET = SAFE
CREATE FUNCTION [dbo].[ENCRYPT](@plaintext [nvarchar](1000))
RETURNS [nvarchar](2000) WITH EXECUTE AS CALLER
AS
EXTERNAL NAME SQLServerEncryption.[SQLCLRCrypt.SQLCrypt].Enc
GO

select dbo.ENCRYPT('1')
m4R0rUAW4REnW0XPhHfDCw==

*/


--컬럼 데이터 생성
SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'AdminMembers'
SELECT COLUMN_NAME + ', ' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'AdminMembers'
SELECT ',	@' + COLUMN_NAME + ' ' + data_type +'(' + convert(varchar(10), isnull(CHARACTER_MAXIMUM_LENGTH, ''))  + ')'  FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'AdminMembers'
SELECT ',	@' + COLUMN_NAME + '_' + TABLE_NAME + ' ' + data_type +'(MAX)'  FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'AdminMembers'
SELECT  ', ' + COLUMN_NAME + ' = CASE WHEN @' + COLUMN_NAME + ' is not null THEN @' + COLUMN_NAME + ' ELSE  ' + COLUMN_NAME + ' END' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'AdminMembers'
SELECT  ', ' + COLUMN_NAME + ' = CASE WHEN @' + COLUMN_NAME + '_' + TABLE_NAME + ' is not null THEN @' + COLUMN_NAME+ '_' + TABLE_NAME  + ' ELSE  ' + COLUMN_NAME + ' END' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'AdminMembers'

--컬럼 데이터 생성
SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'Coupon'
SELECT COLUMN_NAME + ', ' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'Coupon'
SELECT ',	@' + COLUMN_NAME + ' ' + data_type +'(' + convert(varchar(10), isnull(CHARACTER_MAXIMUM_LENGTH, ''))  + ')'  FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'Coupon'
SELECT ',	@' + COLUMN_NAME + '_' + TABLE_NAME + ' ' + data_type +'(MAX)'  FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'Coupon'
SELECT  ', ' + COLUMN_NAME + ' = CASE WHEN @' + COLUMN_NAME + ' is not null THEN @' + COLUMN_NAME + ' ELSE  ' + COLUMN_NAME + ' END' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'Coupon'
SELECT  ', ' + COLUMN_NAME + ' = CASE WHEN @' + COLUMN_NAME + '_' + TABLE_NAME + ' is not null THEN @' + COLUMN_NAME+ '_' + TABLE_NAME  + ' ELSE  ' + COLUMN_NAME + ' END' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'Coupon'

--컬럼 데이터 생성
SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'CouponMember'
SELECT COLUMN_NAME + ', ' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'CouponMember'
SELECT ',	@' + COLUMN_NAME + ' ' + data_type +'(' + convert(varchar(10), isnull(CHARACTER_MAXIMUM_LENGTH, ''))  + ')'  FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'CouponMember'
SELECT ',	@' + COLUMN_NAME + '_' + TABLE_NAME + ' ' + data_type +'(MAX)'  FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'CouponMember'
SELECT  ', ' + COLUMN_NAME + ' = CASE WHEN @' + COLUMN_NAME + ' is not null THEN @' + COLUMN_NAME + ' ELSE  ' + COLUMN_NAME + ' END' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'CouponMember'
SELECT  ', ' + COLUMN_NAME + ' = CASE WHEN @' + COLUMN_NAME + '_' + TABLE_NAME + ' is not null THEN @' + COLUMN_NAME+ '_' + TABLE_NAME  + ' ELSE  ' + COLUMN_NAME + ' END' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'CouponMember'


--컬럼 데이터 생성
SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'GameEventMember'
SELECT COLUMN_NAME + ', ' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'GameEventMember'
SELECT ',	@' + COLUMN_NAME + ' ' + data_type +'(' + convert(varchar(10), isnull(CHARACTER_MAXIMUM_LENGTH, ''))  + ')'  FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'GameEventMember'
SELECT ',	@' + COLUMN_NAME + '_' + TABLE_NAME + ' ' + data_type +'(MAX)'  FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'GameEventMember'
SELECT  ', ' + COLUMN_NAME + ' = CASE WHEN @' + COLUMN_NAME + ' is not null THEN @' + COLUMN_NAME + ' ELSE  ' + COLUMN_NAME + ' END' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'GameEventMember'
SELECT  ', ' + COLUMN_NAME + ' = CASE WHEN @' + COLUMN_NAME + '_' + TABLE_NAME + ' is not null THEN @' + COLUMN_NAME+ '_' + TABLE_NAME  + ' ELSE  ' + COLUMN_NAME + ' END' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'GameEventMember'


--컬럼 데이터 생성
SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'GameEvents'
SELECT COLUMN_NAME + ', ' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'GameEvents'
SELECT ',	@' + COLUMN_NAME + ' ' + data_type +'(' + convert(varchar(10), isnull(CHARACTER_MAXIMUM_LENGTH, ''))  + ')'  FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'GameEvents'
SELECT ',	@' + COLUMN_NAME + '_' + TABLE_NAME + ' ' + data_type +'(MAX)'  FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'GameEvents'
SELECT  ', ' + COLUMN_NAME + ' = CASE WHEN @' + COLUMN_NAME + ' is not null THEN @' + COLUMN_NAME + ' ELSE  ' + COLUMN_NAME + ' END' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'GameEvents'
SELECT  ', ' + COLUMN_NAME + ' = CASE WHEN @' + COLUMN_NAME + '_' + TABLE_NAME + ' is not null THEN @' + COLUMN_NAME+ '_' + TABLE_NAME  + ' ELSE  ' + COLUMN_NAME + ' END' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'GameEvents'


--컬럼 데이터 생성
SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'GiftDepositories'
SELECT COLUMN_NAME + ', ' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'GiftDepositories'
SELECT ',	@' + COLUMN_NAME + ' ' + data_type +'(' + convert(varchar(10), isnull(CHARACTER_MAXIMUM_LENGTH, ''))  + ')'  FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'GiftDepositories'
SELECT ',	@' + COLUMN_NAME + '_' + TABLE_NAME + ' ' + data_type +'(MAX)'  FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'GiftDepositories'
SELECT  ', ' + COLUMN_NAME + ' = CASE WHEN @' + COLUMN_NAME + ' is not null THEN @' + COLUMN_NAME + ' ELSE  ' + COLUMN_NAME + ' END' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'GiftDepositories'
SELECT  ', ' + COLUMN_NAME + ' = CASE WHEN @' + COLUMN_NAME + '_' + TABLE_NAME + ' is not null THEN @' + COLUMN_NAME+ '_' + TABLE_NAME  + ' ELSE  ' + COLUMN_NAME + ' END' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'GiftDepositories'

--컬럼 데이터 생성
SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'ItemLists'
SELECT COLUMN_NAME + ', ' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'ItemLists'
SELECT ',	@' + COLUMN_NAME + ' ' + data_type +'(' + convert(varchar(10), isnull(CHARACTER_MAXIMUM_LENGTH, ''))  + ')'  FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'ItemLists'
SELECT ',	@' + COLUMN_NAME + '_' + TABLE_NAME + ' ' + data_type +'(MAX)'  FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'ItemLists'
SELECT  ', ' + COLUMN_NAME + ' = CASE WHEN @' + COLUMN_NAME + ' is not null THEN @' + COLUMN_NAME + ' ELSE  ' + COLUMN_NAME + ' END' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'ItemLists'
SELECT  ', ' + COLUMN_NAME + ' = CASE WHEN @' + COLUMN_NAME + '_' + TABLE_NAME + ' is not null THEN @' + COLUMN_NAME+ '_' + TABLE_NAME  + ' ELSE  ' + COLUMN_NAME + ' END' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'ItemLists'

--컬럼 데이터 생성
SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'MemberAccountBlockLog'
SELECT COLUMN_NAME + ', ' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'MemberAccountBlockLog'
SELECT ',	@' + COLUMN_NAME + ' ' + data_type +'(' + convert(varchar(10), isnull(CHARACTER_MAXIMUM_LENGTH, ''))  + ')'  FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'MemberAccountBlockLog'
SELECT ',	@' + COLUMN_NAME + '_' + TABLE_NAME + ' ' + data_type +'(MAX)'  FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'MemberAccountBlockLog'
SELECT  ', ' + COLUMN_NAME + ' = CASE WHEN @' + COLUMN_NAME + ' is not null THEN @' + COLUMN_NAME + ' ELSE  ' + COLUMN_NAME + ' END' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'MemberAccountBlockLog'
SELECT  ', ' + COLUMN_NAME + ' = CASE WHEN @' + COLUMN_NAME + '_' + TABLE_NAME + ' is not null THEN @' + COLUMN_NAME+ '_' + TABLE_NAME  + ' ELSE  ' + COLUMN_NAME + ' END' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'MemberAccountBlockLog'

--컬럼 데이터 생성
SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'MemberGameInfoes'
SELECT COLUMN_NAME + ', ' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'MemberGameInfoes'
SELECT ',	@' + COLUMN_NAME + ' ' + data_type +'(' + convert(varchar(10), isnull(CHARACTER_MAXIMUM_LENGTH, ''))  + ')'  FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'MemberGameInfoes'
SELECT ',	@' + COLUMN_NAME + '_' + TABLE_NAME + ' ' + data_type +'(MAX)'  FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'MemberGameInfoes'
SELECT  ', ' + COLUMN_NAME + ' = CASE WHEN @' + COLUMN_NAME + ' is not null THEN @' + COLUMN_NAME + ' ELSE  ' + COLUMN_NAME + ' END' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'MemberGameInfoes'
SELECT  ', ' + COLUMN_NAME + ' = CASE WHEN @' + COLUMN_NAME + '_' + TABLE_NAME + ' is not null THEN @' + COLUMN_NAME+ '_' + TABLE_NAME  + ' ELSE  ' + COLUMN_NAME + ' END' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'MemberGameInfoes'

--컬럼 데이터 생성
SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'MemberGameInfoStages'
SELECT COLUMN_NAME + ', ' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'MemberGameInfoStages'
SELECT ',	@' + COLUMN_NAME + ' ' + data_type +'(' + convert(varchar(10), isnull(CHARACTER_MAXIMUM_LENGTH, ''))  + ')'  FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'MemberGameInfoStages'
SELECT ',	@' + COLUMN_NAME + '_' + TABLE_NAME + ' ' + data_type +'(MAX)'  FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'MemberGameInfoStages'
SELECT  ', ' + COLUMN_NAME + ' = CASE WHEN @' + COLUMN_NAME + ' is not null THEN @' + COLUMN_NAME + ' ELSE  ' + COLUMN_NAME + ' END' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'MemberGameInfoStages'
SELECT  ', ' + COLUMN_NAME + ' = CASE WHEN @' + COLUMN_NAME + '_' + TABLE_NAME + ' is not null THEN @' + COLUMN_NAME+ '_' + TABLE_NAME  + ' ELSE  ' + COLUMN_NAME + ' END' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'MemberGameInfoStages'

--컬럼 데이터 생성
SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'MemberItemPurchases'
SELECT COLUMN_NAME + ', ' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'MemberItemPurchases'
SELECT ',	@' + COLUMN_NAME + ' ' + data_type +'(' + convert(varchar(10), isnull(CHARACTER_MAXIMUM_LENGTH, ''))  + ')'  FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'MemberItemPurchases'
SELECT ',	@' + COLUMN_NAME + '_' + TABLE_NAME + ' ' + data_type +'(MAX)'  FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'MemberItemPurchases'
SELECT  ', ' + COLUMN_NAME + ' = CASE WHEN @' + COLUMN_NAME + ' is not null THEN @' + COLUMN_NAME + ' ELSE  ' + COLUMN_NAME + ' END' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'MemberItemPurchases'
SELECT  ', ' + COLUMN_NAME + ' = CASE WHEN @' + COLUMN_NAME + '_' + TABLE_NAME + ' is not null THEN @' + COLUMN_NAME+ '_' + TABLE_NAME  + ' ELSE  ' + COLUMN_NAME + ' END' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'MemberItemPurchases'

--컬럼 데이터 생성
SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'MemberItems'
SELECT COLUMN_NAME + ', ' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'MemberItems'
SELECT ',	@' + COLUMN_NAME + ' ' + data_type +'(' + convert(varchar(10), isnull(CHARACTER_MAXIMUM_LENGTH, ''))  + ')'  FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'MemberItems'
SELECT ',	@' + COLUMN_NAME + ' ' + data_type +'(MAX)'  FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'MemberItems'
SELECT ',	@' + COLUMN_NAME + '_' + TABLE_NAME + ' ' + data_type +'(MAX)'  FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'MemberItems'
SELECT  ', ' + COLUMN_NAME + ' = CASE WHEN @' + COLUMN_NAME + ' is not null THEN @' + COLUMN_NAME + ' ELSE  ' + COLUMN_NAME + ' END' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'MemberItems'
SELECT  ', ' + COLUMN_NAME + ' = CASE WHEN @' + COLUMN_NAME + '_' + TABLE_NAME + ' is not null THEN @' + COLUMN_NAME+ '_' + TABLE_NAME  + ' ELSE  ' + COLUMN_NAME + ' END' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'MemberItems'


--컬럼 데이터 생성
SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'Members'
SELECT COLUMN_NAME + ', ' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'Members'
SELECT ',	@' + COLUMN_NAME + ' ' + data_type +'(' + convert(varchar(10), isnull(CHARACTER_MAXIMUM_LENGTH, ''))  + ')'  FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'Members'
SELECT ',	@' + COLUMN_NAME + '_' + TABLE_NAME + ' ' + data_type +'(MAX)'  FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'Members'
SELECT  ', ' + COLUMN_NAME + ' = CASE WHEN @' + COLUMN_NAME + ' is not null THEN @' + COLUMN_NAME + ' ELSE  ' + COLUMN_NAME + ' END' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'Members'
SELECT  ', ' + COLUMN_NAME + ' = CASE WHEN @' + COLUMN_NAME + '_' + TABLE_NAME + ' is not null THEN @' + COLUMN_NAME+ '_' + TABLE_NAME  + ' ELSE  ' + COLUMN_NAME + ' END' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'Members'


--컬럼 데이터 생성
SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'Notices'
SELECT COLUMN_NAME + ', ' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'Notices'
SELECT ',	@' + COLUMN_NAME + ' ' + data_type +'(' + convert(varchar(10), isnull(CHARACTER_MAXIMUM_LENGTH, ''))  + ')'  FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'Notices'
SELECT ',	@' + COLUMN_NAME + '_' + TABLE_NAME + ' ' + data_type +'(MAX)'  FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'Notices'
SELECT  ', ' + COLUMN_NAME + ' = CASE WHEN @' + COLUMN_NAME + ' is not null THEN @' + COLUMN_NAME + ' ELSE  ' + COLUMN_NAME + ' END' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'Notices'
SELECT  ', ' + COLUMN_NAME + ' = CASE WHEN @' + COLUMN_NAME + '_' + TABLE_NAME + ' is not null THEN @' + COLUMN_NAME+ '_' + TABLE_NAME  + ' ELSE  ' + COLUMN_NAME + ' END' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'Notices'



----------------------------------------------------------------------------------------------------------------
--Members 테이블 테스트 데이터 삽입
--truncate table CloudBread.Members 
--SELECT COLUMN_NAME + ', ' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'Members'
--SELECT '''' + COLUMN_NAME + ''', ' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'Members'
INSERT INTO CloudBread.Members (MemberID, MemberPWD, EmailAddress, EmailConfirmedYN, PhoneNumber1, PhoneNumber2, PINumber, Name1, Name2, Name3, DOB, RecommenderID, MemberGroup, LastDeviceID, LastIPaddress, LastLoginDT, LastLogoutDT, LastMACAddress, AccountBlockYN, AccountBlockEndDT, AnonymousYN, [3rdAuthProvider], [3rdAuthID], [3rdAuthParam], PushNotificationID, PushNotificationProvider, PushNotificationGroup, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, TimeZoneID, HideYN, DeleteYN, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
values('aaa', 'MemberPWD', 'aaa@aa.com', 'Y', 'PhoneNumber1', 'PhoneNumber2', 'PINumber', 'Name1', 'Name2', 'Name3', 'DOB', 'RecommenderID', 'MemberGroup', 'LastDeviceID', 'LastIPaddress', SYSUTCDATETIME(), SYSUTCDATETIME(), 'LastMACAddress', 'N', '1900-01-01', 'N', '3rdAuthProvider', '3rdAuthIDaaa', '3rdAuthParam', 'PushNotificationID', 'PushNotificationProvider', 'PushNotificationGroup', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', 'Korea Standard Time', 'N', 'N', '', '', '', '')
INSERT INTO CloudBread.Members (MemberID, MemberPWD, EmailAddress, EmailConfirmedYN, PhoneNumber1, PhoneNumber2, PINumber, Name1, Name2, Name3, DOB, RecommenderID, MemberGroup, LastDeviceID, LastIPaddress, LastLoginDT, LastLogoutDT, LastMACAddress, AccountBlockYN, AccountBlockEndDT, AnonymousYN, [3rdAuthProvider], [3rdAuthID], [3rdAuthParam], PushNotificationID, PushNotificationProvider, PushNotificationGroup, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, TimeZoneID, HideYN, DeleteYN, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
values('bbb', 'MemberPWD', 'bbb@aa.com', 'Y', 'PhoneNumber1', 'PhoneNumber2', 'PINumber', 'Name1', 'Name2', 'Name3', 'DOB', 'RecommenderID', 'MemberGroup', 'LastDeviceID', 'LastIPaddress', SYSUTCDATETIME(), SYSUTCDATETIME(), 'LastMACAddress', 'N', '1900-01-01', 'N', '3rdAuthProvider', '3rdAuthIDbbb', '3rdAuthParam', 'PushNotificationID', 'PushNotificationProvider', 'PushNotificationGroup', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', 'Korea Standard Time', 'N', 'N', '', '', '', '')
INSERT INTO CloudBread.Members (MemberID, MemberPWD, EmailAddress, EmailConfirmedYN, PhoneNumber1, PhoneNumber2, PINumber, Name1, Name2, Name3, DOB, RecommenderID, MemberGroup, LastDeviceID, LastIPaddress, LastLoginDT, LastLogoutDT, LastMACAddress, AccountBlockYN, AccountBlockEndDT, AnonymousYN, [3rdAuthProvider], [3rdAuthID], [3rdAuthParam], PushNotificationID, PushNotificationProvider, PushNotificationGroup, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, TimeZoneID, HideYN, DeleteYN, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
values('ccc', 'MemberPWD', 'ccc@aa.com', 'Y', 'PhoneNumber1', 'PhoneNumber2', 'PINumber', 'Name1', 'Name2', 'Name3', 'DOB', 'RecommenderID', 'MemberGroup', 'LastDeviceID', 'LastIPaddress', SYSUTCDATETIME(), SYSUTCDATETIME(), 'LastMACAddress', 'N', '1900-01-01', 'N', '3rdAuthProvider', '3rdAuthIDccc', '3rdAuthParam', 'PushNotificationID', 'PushNotificationProvider', 'PushNotificationGroup', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', 'Korea Standard Time', 'N', 'N', '', '', '', '')
INSERT INTO CloudBread.Members (MemberID, MemberPWD, EmailAddress, EmailConfirmedYN, PhoneNumber1, PhoneNumber2, PINumber, Name1, Name2, Name3, DOB, RecommenderID, MemberGroup, LastDeviceID, LastIPaddress, LastLoginDT, LastLogoutDT, LastMACAddress, AccountBlockYN, AccountBlockEndDT, AnonymousYN, [3rdAuthProvider], [3rdAuthID], [3rdAuthParam], PushNotificationID, PushNotificationProvider, PushNotificationGroup, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, TimeZoneID, HideYN, DeleteYN, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
values('ddd', 'MemberPWD', 'ddd@aa.com', 'Y', 'PhoneNumber1', 'PhoneNumber2', 'PINumber', 'Name1', 'Name2', 'Name3', 'DOB', 'RecommenderID', 'MemberGroup', 'LastDeviceID', 'LastIPaddress', SYSUTCDATETIME(), SYSUTCDATETIME(), 'LastMACAddress', 'N', '1900-01-01', 'N', '3rdAuthProvider', '3rdAuthIDddd', '3rdAuthParam', 'PushNotificationID', 'PushNotificationProvider', 'PushNotificationGroup', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', 'Korea Standard Time', 'N', 'N', '', '', '', '')
INSERT INTO CloudBread.Members (MemberID, MemberPWD, EmailAddress, EmailConfirmedYN, PhoneNumber1, PhoneNumber2, PINumber, Name1, Name2, Name3, DOB, RecommenderID, MemberGroup, LastDeviceID, LastIPaddress, LastLoginDT, LastLogoutDT, LastMACAddress, AccountBlockYN, AccountBlockEndDT, AnonymousYN, [3rdAuthProvider], [3rdAuthID], [3rdAuthParam], PushNotificationID, PushNotificationProvider, PushNotificationGroup, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, TimeZoneID, HideYN, DeleteYN, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
values('eee', 'MemberPWD', 'eee@aa.com', 'Y', 'PhoneNumber1', 'PhoneNumber2', 'PINumber', 'Name1', 'Name2', 'Name3', 'DOB', 'RecommenderID', 'MemberGroup', 'LastDeviceID', 'LastIPaddress', SYSUTCDATETIME(), SYSUTCDATETIME(), 'LastMACAddress', 'N', '1900-01-01', 'N', '3rdAuthProvider', '3rdAuthIDeee', '3rdAuthParam', 'PushNotificationID', 'PushNotificationProvider', 'PushNotificationGroup', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', 'Korea Standard Time', 'N', 'N', '', '', '', '')
--select * from CloudBread.Members


--ItemLists 테이블 테스트 데이터 삽입
--truncate table CloudBread.ItemLists 
--SELECT COLUMN_NAME + ', ' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'ItemLists'
--SELECT '''' + COLUMN_NAME + ''', ' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'ItemLists'
INSERT INTO CloudBread.ItemLists (ItemListID, ItemName, ItemDescription, ItemPrice, ItemSellPrice, ItemCategory1, ItemCategory2, ItemCategory3, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, IteamCreateAdminID, IteamUpdateAdminID, HideYN, DeleteYN, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
VALUES(newid(), 'ItemName1', 'ItemDescription', '10', '10', 'ItemCategory1', 'ItemCategory2', 'ItemCategory3', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', 'IteamCreateAdminID', 'IteamUpdateAdminID', 'N', 'N', '', '', '', '')
INSERT INTO CloudBread.ItemLists (ItemListID, ItemName, ItemDescription, ItemPrice, ItemSellPrice, ItemCategory1, ItemCategory2, ItemCategory3, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, IteamCreateAdminID, IteamUpdateAdminID, HideYN, DeleteYN, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
VALUES(newid(), 'ItemName2', 'ItemDescription', '20', '10', 'ItemCategory1', 'ItemCategory2', 'ItemCategory3', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', 'IteamCreateAdminID', 'IteamUpdateAdminID', 'N', 'N', '', '', '', '')
INSERT INTO CloudBread.ItemLists (ItemListID, ItemName, ItemDescription, ItemPrice, ItemSellPrice, ItemCategory1, ItemCategory2, ItemCategory3, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, IteamCreateAdminID, IteamUpdateAdminID, HideYN, DeleteYN, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
VALUES(newid(), 'ItemName3', 'ItemDescription', '30', '10', 'ItemCategory1', 'ItemCategory2', 'ItemCategory3', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', 'IteamCreateAdminID', 'IteamUpdateAdminID', 'N', 'N', '', '', '', '')
INSERT INTO CloudBread.ItemLists (ItemListID, ItemName, ItemDescription, ItemPrice, ItemSellPrice, ItemCategory1, ItemCategory2, ItemCategory3, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, IteamCreateAdminID, IteamUpdateAdminID, HideYN, DeleteYN, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
VALUES(newid(), 'ItemName4', 'ItemDescription', '40', '10', 'ItemCategory1', 'ItemCategory2', 'ItemCategory3', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', 'IteamCreateAdminID', 'IteamUpdateAdminID', 'N', 'N', '', '', '', '')
--select * from CloudBread.ItemLists

--GiftDepositories 테이블 테스트 데이터 삽입
--truncate table CloudBread.GiftDepositories
--SELECT COLUMN_NAME + ', ' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'GiftDepositories'
--SELECT '''' + COLUMN_NAME + ''', ' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'GiftDepositories'
INSERT INTO CloudBread.GiftDepositories(GiftDepositoryID, ItemListID, ItemCount, FromMemberID, ToMemberID, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, SentMemberYN, HideYN, DeleteYN, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
VALUES (newid(), 'A2C03A61-41C8-496C-823B-F7B2255DE55D', '1', 'bbb', 'aaa', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', 'N', 'N', 'N', '', '', '', '')
INSERT INTO CloudBread.GiftDepositories(GiftDepositoryID, ItemListID, ItemCount, FromMemberID, ToMemberID, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, SentMemberYN, HideYN, DeleteYN, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
VALUES (newid(), 'A2C03A61-41C8-496C-823B-F7B2255DE55D', '2', 'ccc', 'aaa', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', 'N', 'N', 'N', '', '', '', '')
INSERT INTO CloudBread.GiftDepositories(GiftDepositoryID, ItemListID, ItemCount, FromMemberID, ToMemberID, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, SentMemberYN, HideYN, DeleteYN, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
VALUES (newid(), 'C21BA95D-B736-4123-8531-75B7C5E8C906', '3', 'bbb', 'aaa', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', 'N', 'N', 'N', '', '', '', '')
INSERT INTO CloudBread.GiftDepositories(GiftDepositoryID, ItemListID, ItemCount, FromMemberID, ToMemberID, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, SentMemberYN, HideYN, DeleteYN, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
VALUES (newid(), '5395E0E0-9C9F-4A5E-995E-668C7E5B1415', '4', 'ccc', 'bbb', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', 'N', 'N', 'N', '', '', '', '')
--select * from CloudBread.GiftDepositories

--MemberItems 테이블 테스트 데이터 삽입
--truncate table CloudBread.MemberItems
--SELECT COLUMN_NAME + ', ' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'MemberItems'
--SELECT '''' + COLUMN_NAME + ''', ' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'MemberItems'
INSERT INTO CloudBread.MemberItems(MemberItemID, MemberID, ItemListID, ItemCount, ItemStatus, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, HideYN, DeleteYN, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
values(newid(), 'aaa', 'A2C03A61-41C8-496C-823B-F7B2255DE55D', '1', 'ItemStatus', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', 'N', 'N', '', '', '', '')
INSERT INTO CloudBread.MemberItems(MemberItemID, MemberID, ItemListID, ItemCount, ItemStatus, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, HideYN, DeleteYN, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
values(newid(), 'aaa', 'C21BA95D-B736-4123-8531-75B7C5E8C906', '1', 'ItemStatus', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', 'N', 'N', '', '', '', '')
INSERT INTO CloudBread.MemberItems(MemberItemID, MemberID, ItemListID, ItemCount, ItemStatus, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, HideYN, DeleteYN, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
values(newid(), 'aaa', 'C21BA95D-B736-4123-8531-75B7C5E8C906', '1', 'ItemStatus', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', 'N', 'N', '', '', '', '')
INSERT INTO CloudBread.MemberItems(MemberItemID, MemberID, ItemListID, ItemCount, ItemStatus, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, HideYN, DeleteYN, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
values(newid(), 'bbb', 'A2C03A61-41C8-496C-823B-F7B2255DE55D', '1', 'ItemStatus', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', 'N', 'N', '', '', '', '')
--select * from CloudBread.MemberItems


--MemberGameInfoes 테이블 데이터 삽입
--truncate table CloudBread.MemberGameInfoes
--SELECT COLUMN_NAME + ', ' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'MemberGameInfoes'
--SELECT '''' + COLUMN_NAME + ''', ' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'MemberGameInfoes'
INSERT INTO CloudBread.MemberGameInfoes(MemberID, Level, Exps, Points, UserSTAT1, UserSTAT2, UserSTAT3, UserSTAT4, UserSTAT5, UserSTAT6, UserSTAT7, UserSTAT8, UserSTAT9, UserSTAT10, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, HideYN, DeleteYN, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
VALUES ('aaa', '10', '10', '10', 'UserSTAT1', 'UserSTAT2', 'UserSTAT3', 'UserSTAT4', 'UserSTAT5', 'UserSTAT6', 'UserSTAT7', 'UserSTAT8', 'UserSTAT9', 'UserSTAT10', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', 'N', 'N', '', '', '', '')
INSERT INTO CloudBread.MemberGameInfoes(MemberID, Level, Exps, Points, UserSTAT1, UserSTAT2, UserSTAT3, UserSTAT4, UserSTAT5, UserSTAT6, UserSTAT7, UserSTAT8, UserSTAT9, UserSTAT10, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, HideYN, DeleteYN, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
VALUES ('bbb', '20', '20', '20', 'UserSTAT1', 'UserSTAT2', 'UserSTAT3', 'UserSTAT4', 'UserSTAT5', 'UserSTAT6', 'UserSTAT7', 'UserSTAT8', 'UserSTAT9', 'UserSTAT10', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', 'N', 'N', '', '', '', '')
INSERT INTO CloudBread.MemberGameInfoes(MemberID, Level, Exps, Points, UserSTAT1, UserSTAT2, UserSTAT3, UserSTAT4, UserSTAT5, UserSTAT6, UserSTAT7, UserSTAT8, UserSTAT9, UserSTAT10, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, HideYN, DeleteYN, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
VALUES ('ccc', '30', '30', '30', 'UserSTAT1', 'UserSTAT2', 'UserSTAT3', 'UserSTAT4', 'UserSTAT5', 'UserSTAT6', 'UserSTAT7', 'UserSTAT8', 'UserSTAT9', 'UserSTAT10', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', 'N', 'N', '', '', '', '')
INSERT INTO CloudBread.MemberGameInfoes(MemberID, Level, Exps, Points, UserSTAT1, UserSTAT2, UserSTAT3, UserSTAT4, UserSTAT5, UserSTAT6, UserSTAT7, UserSTAT8, UserSTAT9, UserSTAT10, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, HideYN, DeleteYN, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
VALUES ('ddd', '40', '40', '40', 'UserSTAT1', 'UserSTAT2', 'UserSTAT3', 'UserSTAT4', 'UserSTAT5', 'UserSTAT6', 'UserSTAT7', 'UserSTAT8', 'UserSTAT9', 'UserSTAT10', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', 'N', 'N', '', '', '', '')
--select * from CloudBread.MemberGameInfoes


-- MemberItemPurchases 테이블 테스트 데이터 삽입
--truncate table CloudBread.MemberItemPurchases
--SELECT COLUMN_NAME + ', ' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'MemberItemPurchases'
--SELECT '''' + COLUMN_NAME + ''', ' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'MemberItemPurchases'
INSERT INTO CloudBread.MemberItemPurchases(MemberItemPurchaseID, MemberID, ItemListID, PurchaseQuantity, PurchasePrice, PGinfo1, PGinfo2, PGinfo3, PGinfo4, PGinfo5, PurchaseDeviceID, PurchaseDeviceIPAddress, PurchaseDeviceMACAddress, PurchaseDT, PurchaseCancelYN, PurchaseCancelDT, PurchaseCancelingStatus, PurchaseCancelReturnedAmount, PurchaseCancelDeviceID, PurchaseCancelDeviceIPAddress, PurchaseCancelDeviceMACAddress, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, HideYN, DeleteYN, PurchaseCancelConfirmAdminMemberID, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
VALUES (newid(), 'aaa', 'A2C03A61-41C8-496C-823B-F7B2255DE55D', '10', '10', 'PGinfo1', 'PGinfo2', 'PGinfo3', 'PGinfo4', 'PGinfo5', 'PurchaseDeviceID', 'PurchaseDeviceIPAddress', 'PurchaseDeviceMACAddress', SYSUTCDATETIME(), 'N', '1900-01-01', '0', '100', 'PurchaseCancelDeviceID', 'PurchaseCancelDeviceIPAddress', 'PurchaseCancelDeviceMACAddress', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', 'N', 'N', 'admin1', SYSUTCDATETIME(), SYSUTCDATETIME(), '', '1900-01-01')
INSERT INTO CloudBread.MemberItemPurchases(MemberItemPurchaseID, MemberID, ItemListID, PurchaseQuantity, PurchasePrice, PGinfo1, PGinfo2, PGinfo3, PGinfo4, PGinfo5, PurchaseDeviceID, PurchaseDeviceIPAddress, PurchaseDeviceMACAddress, PurchaseDT, PurchaseCancelYN, PurchaseCancelDT, PurchaseCancelingStatus, PurchaseCancelReturnedAmount, PurchaseCancelDeviceID, PurchaseCancelDeviceIPAddress, PurchaseCancelDeviceMACAddress, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, HideYN, DeleteYN, PurchaseCancelConfirmAdminMemberID, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
VALUES (newid(), 'aaa', 'C21BA95D-B736-4123-8531-75B7C5E8C906', '10', '10', 'PGinfo1', 'PGinfo2', 'PGinfo3', 'PGinfo4', 'PGinfo5', 'PurchaseDeviceID', 'PurchaseDeviceIPAddress', 'PurchaseDeviceMACAddress', SYSUTCDATETIME(), 'N', '1900-01-01', '1', '100', 'PurchaseCancelDeviceID', 'PurchaseCancelDeviceIPAddress', 'PurchaseCancelDeviceMACAddress', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', 'N', 'N', 'admin1', SYSUTCDATETIME(), SYSUTCDATETIME(), '', '1900-01-01')
INSERT INTO CloudBread.MemberItemPurchases(MemberItemPurchaseID, MemberID, ItemListID, PurchaseQuantity, PurchasePrice, PGinfo1, PGinfo2, PGinfo3, PGinfo4, PGinfo5, PurchaseDeviceID, PurchaseDeviceIPAddress, PurchaseDeviceMACAddress, PurchaseDT, PurchaseCancelYN, PurchaseCancelDT, PurchaseCancelingStatus, PurchaseCancelReturnedAmount, PurchaseCancelDeviceID, PurchaseCancelDeviceIPAddress, PurchaseCancelDeviceMACAddress, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, HideYN, DeleteYN, PurchaseCancelConfirmAdminMemberID, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
VALUES (newid(), 'aaa', '5395E0E0-9C9F-4A5E-995E-668C7E5B1415', '10', '10', 'PGinfo1', 'PGinfo2', 'PGinfo3', 'PGinfo4', 'PGinfo5', 'PurchaseDeviceID', 'PurchaseDeviceIPAddress', 'PurchaseDeviceMACAddress', SYSUTCDATETIME(), 'N', '1900-01-01', '2', '100', 'PurchaseCancelDeviceID', 'PurchaseCancelDeviceIPAddress', 'PurchaseCancelDeviceMACAddress', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', 'N', 'N', 'admin1', SYSUTCDATETIME(), SYSUTCDATETIME(), '', '1900-01-01')
INSERT INTO CloudBread.MemberItemPurchases(MemberItemPurchaseID, MemberID, ItemListID, PurchaseQuantity, PurchasePrice, PGinfo1, PGinfo2, PGinfo3, PGinfo4, PGinfo5, PurchaseDeviceID, PurchaseDeviceIPAddress, PurchaseDeviceMACAddress, PurchaseDT, PurchaseCancelYN, PurchaseCancelDT, PurchaseCancelingStatus, PurchaseCancelReturnedAmount, PurchaseCancelDeviceID, PurchaseCancelDeviceIPAddress, PurchaseCancelDeviceMACAddress, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, HideYN, DeleteYN, PurchaseCancelConfirmAdminMemberID, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
VALUES (newid(), 'ccc', 'A2C03A61-41C8-496C-823B-F7B2255DE55D', '10', '10', 'PGinfo1', 'PGinfo2', 'PGinfo3', 'PGinfo4', 'PGinfo5', 'PurchaseDeviceID', 'PurchaseDeviceIPAddress', 'PurchaseDeviceMACAddress', SYSUTCDATETIME(), 'N', '1900-01-01', '4', '100', 'PurchaseCancelDeviceID', 'PurchaseCancelDeviceIPAddress', 'PurchaseCancelDeviceMACAddress', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', 'N', 'N', 'admin1', SYSUTCDATETIME(), SYSUTCDATETIME(), '', '1900-01-01')
--select * from CloudBread.MemberItems

--select * from CloudBread.MemberItemPurchases


--AdminMembers 테이블 데이터 삽입
--truncate table CloudBread.AdminMembers
--SELECT COLUMN_NAME + ', ' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'AdminMembers'
--SELECT '''' + COLUMN_NAME + ''', ' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'AdminMembers'

INSERT INTO CloudBread.AdminMembers
(AdminMemberID, AdminMemberPWD, AdminMemberEmail, IDCreateAdminMember, AdminGroup, TimeZoneID, PINumber, Name1, Name2, Name3, DOB, LastIPaddress, LastLoginDT, LastLogoutDT, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, HideYN, DeleteYN, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
values('admin1', 'AdminMemberPWD', 'admin1@abc.com', 'admin1', 'AdminGroup', 'Korea Standard Time', 'PINumber', 'Name1', 'Name2', 'Name3', '19991212', 'LastIPaddress', '', '', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', 'N', 'N', '', '', '', '')
INSERT INTO CloudBread.AdminMembers
(AdminMemberID, AdminMemberPWD, AdminMemberEmail, IDCreateAdminMember, AdminGroup, TimeZoneID, PINumber, Name1, Name2, Name3, DOB, LastIPaddress, LastLoginDT, LastLogoutDT, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, HideYN, DeleteYN, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
values('admin2', 'AdminMemberPWD', 'admin2@abc.com', 'admin1', 'AdminGroup', 'Korea Standard Time', 'PINumber', 'Name1', 'Name2', 'Name3', '19991212', 'LastIPaddress', '', '', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', 'N', 'N', '', '', '', '')
INSERT INTO CloudBread.AdminMembers
(AdminMemberID, AdminMemberPWD, AdminMemberEmail, IDCreateAdminMember, AdminGroup, TimeZoneID, PINumber, Name1, Name2, Name3, DOB, LastIPaddress, LastLoginDT, LastLogoutDT, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, HideYN, DeleteYN, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
values('admin3', 'AdminMemberPWD', 'admin3@abc.com', 'admin2', 'AdminGroup', 'Korea Standard Time', 'PINumber', 'Name1', 'Name2', 'Name3', '19991212', 'LastIPaddress', '', '', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', 'N', 'N', '', '', '', '')
INSERT INTO CloudBread.AdminMembers
(AdminMemberID, AdminMemberPWD, AdminMemberEmail, IDCreateAdminMember, AdminGroup, TimeZoneID, PINumber, Name1, Name2, Name3, DOB, LastIPaddress, LastLoginDT, LastLogoutDT, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, HideYN, DeleteYN, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
values('admin4', 'AdminMemberPWD', 'admin4@abc.com', 'admin2', 'AdminGroup', 'Korea Standard Time', 'PINumber', 'Name1', 'Name2', 'Name3', '19991212', 'LastIPaddress', '', '', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', 'N', 'N', '', '', '', '')
--select * from CloudBread.AdminMembers


--MemberGameInfoStages 테이블 데이터 삽입
--truncate table CloudBread.MemberGameInfoStages
--SELECT COLUMN_NAME + ', ' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'MemberGameInfoStages'
--SELECT '''' + COLUMN_NAME + ''', ' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'MemberGameInfoStages'
INSERT INTO CloudBread.MemberGameInfoStages(MemberGameInfoStageID, MemberID, StageName, StageStatus, Category1, Category2, Category3, Mission1, Mission2, Mission3, Mission4, Mission5, Points, StageStat1, StageStat2, StageStat3, StageStat4, StageStat5, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, HideYN, DeleteYN, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
values(newid(), 'aaa', 'StageName', 'StageStatus', 'Category1', 'Category2', 'Category3', 'Mission1', 'Mission2', 'Mission3', 'Mission4', 'Mission5', 'Points', 'StageStat1', 'StageStat2', 'StageStat3', 'StageStat4', 'StageStat5', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', 'N', 'N', '', '', '', '')
INSERT INTO CloudBread.MemberGameInfoStages(MemberGameInfoStageID, MemberID, StageName, StageStatus, Category1, Category2, Category3, Mission1, Mission2, Mission3, Mission4, Mission5, Points, StageStat1, StageStat2, StageStat3, StageStat4, StageStat5, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, HideYN, DeleteYN, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
values(newid(), 'aaa', 'StageName', 'StageStatus', 'Category1', 'Category2', 'Category3', 'Mission1', 'Mission2', 'Mission3', 'Mission4', 'Mission5', 'Points', 'StageStat1', 'StageStat2', 'StageStat3', 'StageStat4', 'StageStat5', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', 'N', 'N', '', '', '', '')
INSERT INTO CloudBread.MemberGameInfoStages(MemberGameInfoStageID, MemberID, StageName, StageStatus, Category1, Category2, Category3, Mission1, Mission2, Mission3, Mission4, Mission5, Points, StageStat1, StageStat2, StageStat3, StageStat4, StageStat5, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, HideYN, DeleteYN, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
values(newid(), 'aaa', 'StageName', 'StageStatus', 'Category1', 'Category2', 'Category3', 'Mission1', 'Mission2', 'Mission3', 'Mission4', 'Mission5', 'Points', 'StageStat1', 'StageStat2', 'StageStat3', 'StageStat4', 'StageStat5', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', 'N', 'N', '', '', '', '')
INSERT INTO CloudBread.MemberGameInfoStages(MemberGameInfoStageID, MemberID, StageName, StageStatus, Category1, Category2, Category3, Mission1, Mission2, Mission3, Mission4, Mission5, Points, StageStat1, StageStat2, StageStat3, StageStat4, StageStat5, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, HideYN, DeleteYN, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
values(newid(), 'bbb', 'StageName', 'StageStatus', 'Category1', 'Category2', 'Category3', 'Mission1', 'Mission2', 'Mission3', 'Mission4', 'Mission5', 'Points', 'StageStat1', 'StageStat2', 'StageStat3', 'StageStat4', 'StageStat5', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', 'N', 'N', '', '', '', '')
--select * from CloudBread.MemberGameInfoStages


--Notices  테이블 데이터 삽입
--truncate table CloudBread.Notices
--SELECT COLUMN_NAME + ', ' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'Notices'
--SELECT '''' + COLUMN_NAME + ''', ' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'Notices'
insert into CloudBread.Notices(NoticeID, NoticeCategory1, NoticeCategory2, NoticeCategory3, TargetGroup, TargetOS, TargetDevice, NoticeImageLink, title, content, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, NoticeDurationFrom, NoticeDurationTo, OrderNumber, CreateAdminID, HideYN, DeleteYN, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
values(newid(), 'NoticeCategory1', 'NoticeCategory2', 'NoticeCategory3', 'TargetGroup', 'TargetOS', 'TargetDevice', 'NoticeImageLink', 'title1', 'content', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', '2015-03-01', '2015-04-01', 0, 'admin1', 'N', 'N', '', '', '', '')
insert into CloudBread.Notices(NoticeID, NoticeCategory1, NoticeCategory2, NoticeCategory3, TargetGroup, TargetOS, TargetDevice, NoticeImageLink, title, content, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, NoticeDurationFrom, NoticeDurationTo, OrderNumber, CreateAdminID, HideYN, DeleteYN, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
values(newid(), 'NoticeCategory1', 'NoticeCategory2', 'NoticeCategory3', 'TargetGroup', 'TargetOS', 'TargetDevice', 'NoticeImageLink', 'title2', 'content', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', '2015-03-01', '2015-04-01', 0, 'admin1', 'N', 'N', '', '', '', '')
insert into CloudBread.Notices(NoticeID, NoticeCategory1, NoticeCategory2, NoticeCategory3, TargetGroup, TargetOS, TargetDevice, NoticeImageLink, title, content, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, NoticeDurationFrom, NoticeDurationTo, OrderNumber, CreateAdminID, HideYN, DeleteYN, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
values(newid(), 'NoticeCategory1', 'NoticeCategory2', 'NoticeCategory3', 'TargetGroup', 'TargetOS', 'TargetDevice', 'NoticeImageLink', 'title3', 'content', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', '2015-03-01', '2015-04-01', 0, 'admin1', 'N', 'N', '', '', '', '')
insert into CloudBread.Notices(NoticeID, NoticeCategory1, NoticeCategory2, NoticeCategory3, TargetGroup, TargetOS, TargetDevice, NoticeImageLink, title, content, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, NoticeDurationFrom, NoticeDurationTo, OrderNumber, CreateAdminID, HideYN, DeleteYN, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
values(newid(), 'NoticeCategory1', 'NoticeCategory2', 'NoticeCategory3', 'TargetGroup', 'TargetOS', 'TargetDevice', 'NoticeImageLink', 'title4', 'content', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', '2015-03-01', '2015-04-01', 0, 'admin1', 'N', 'N', '', '', '', '')
--select * from CloudBread.Notices


--GameEvents 테이블 데이터 삽입
--truncate table CloudBread.GameEvents
--SELECT COLUMN_NAME + ', ' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'GameEvents'
--SELECT '''' + COLUMN_NAME + ''', ' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'GameEvents'
insert into CloudBread.GameEvents(GameEventID, EventCategory1, EventCategory2, EventCategory3, ItemListID, ItemCount, Itemstatus, TargetGroup, TargetOS, TargetDevice, EventImageLink, Title, Content, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, EventDurationFrom, EventDurationTo, OrderNumber, CreateAdminID, HideYN, DeleteYN, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
values(newid(), 'EventCategory1', 'EventCategory2', 'EventCategory3', 'A2C03A61-41C8-496C-823B-F7B2255DE55D', '1', 'Itemstatus', 'TargetGroup', 'TargetOS', 'TargetDevice', 'EventImageLink', 'Title1', 'Content', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', '2015-03-01', '2015-04-01', 0, 'admin1', 'N', 'N', '', '', '', '')
insert into CloudBread.GameEvents(GameEventID, EventCategory1, EventCategory2, EventCategory3, ItemListID, ItemCount, Itemstatus, TargetGroup, TargetOS, TargetDevice, EventImageLink, Title, Content, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, EventDurationFrom, EventDurationTo, OrderNumber, CreateAdminID, HideYN, DeleteYN, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
values(newid(), 'EventCategory1', 'EventCategory2', 'EventCategory3', 'C21BA95D-B736-4123-8531-75B7C5E8C906', '2', 'Itemstatus', 'TargetGroup', 'TargetOS', 'TargetDevice', 'EventImageLink', 'Title2', 'Content', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', '2015-03-01', '2015-04-01', 0, 'admin1', 'N', 'N', '', '', '', '')
insert into CloudBread.GameEvents(GameEventID, EventCategory1, EventCategory2, EventCategory3, ItemListID, ItemCount, Itemstatus, TargetGroup, TargetOS, TargetDevice, EventImageLink, Title, Content, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, EventDurationFrom, EventDurationTo, OrderNumber, CreateAdminID, HideYN, DeleteYN, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
values(newid(), 'EventCategory1', 'EventCategory2', 'EventCategory3', '5395E0E0-9C9F-4A5E-995E-668C7E5B1415', '3', 'Itemstatus', 'TargetGroup', 'TargetOS', 'TargetDevice', 'EventImageLink', 'Title3', 'Content', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', '2015-03-01', '2015-04-01', 0, 'admin1', 'N', 'N', '', '', '', '')
insert into CloudBread.GameEvents(GameEventID, EventCategory1, EventCategory2, EventCategory3, ItemListID, ItemCount, Itemstatus, TargetGroup, TargetOS, TargetDevice, EventImageLink, Title, Content, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, EventDurationFrom, EventDurationTo, OrderNumber, CreateAdminID, HideYN, DeleteYN, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
values(newid(), 'EventCategory1', 'EventCategory2', 'EventCategory3', 'A2D24A56-9314-40C4-AD5E-12F6A08C8DD9', '4', 'Itemstatus', 'TargetGroup', 'TargetOS', 'TargetDevice', 'EventImageLink', 'Title4', 'Content', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', '2015-03-01', '2015-04-01', 0, 'admin1', 'N', 'N', '', '', '', '')
select * from CloudBread.GameEvents


--GameEventMember  테이블 데이터 삽입
--truncate table CloudBread.GameEventMember
--SELECT COLUMN_NAME + ', ' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'GameEventMember'
--SELECT '''' + COLUMN_NAME + ''', ' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'GameEventMember'
insert into CloudBread.GameEventMember(GameEventMemberID, eventID, MemberID, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, HideYN, DeleteYN, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
values(newid(), '7A411FA7-18BC-4383-99B3-AD57FEF3AF7E', 'aaa', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', 'N', 'N', '', '', '', '' )
insert into CloudBread.GameEventMember(GameEventMemberID, eventID, MemberID, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, HideYN, DeleteYN, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
values(newid(), '708AEC02-39F0-4EA3-A656-D2186C526335', 'aaa', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', 'N', 'N', '', '', '', '' )
insert into CloudBread.GameEventMember(GameEventMemberID, eventID, MemberID, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, HideYN, DeleteYN, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
values(newid(), '892409D2-13DD-4FE7-97F0-2FBCB93135F0', 'aaa', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', 'N', 'N', '', '', '', '' )
insert into CloudBread.GameEventMember(GameEventMemberID, eventID, MemberID, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, HideYN, DeleteYN, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
values(newid(), '7A411FA7-18BC-4383-99B3-AD57FEF3AF7E', 'bbb', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', 'N', 'N', '', '', '', '' )
SELECT * FROM CloudBread.GameEventMember


--Coupon  테이블 데이터 삽입
--truncate table CloudBread.Coupon
--SELECT COLUMN_NAME + ', ' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'Coupon'
--SELECT '''' + COLUMN_NAME + ''', ' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'Coupon'
insert into CloudBread.Coupon(CouponID, CouponCategory1, CouponCategory2, CouponCategory3, ItemListID, ItemCount, ItemStatus, TargetGroup, TargetOS, TargetDevice, Title, Content, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, DupeYN, OrderNumber, CouponDurationFrom, CouponDurationTo, CreateAdminID, HideYN, DeleteYN, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
values(newid(), 'CouponCategory1', 'CouponCategory2', 'CouponCategory3', 'A2C03A61-41C8-496C-823B-F7B2255DE55D', '1', 'ItemStatus', 'TargetGroup', 'TargetOS', 'TargetDevice', 'Title1', 'Content', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', 'N', 0, '2015-03-01', '2015-04-01', 'admin1', 'N', 'N', '', '', '', '')
insert into CloudBread.Coupon(CouponID, CouponCategory1, CouponCategory2, CouponCategory3, ItemListID, ItemCount, ItemStatus, TargetGroup, TargetOS, TargetDevice, Title, Content, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, DupeYN, OrderNumber, CouponDurationFrom, CouponDurationTo, CreateAdminID, HideYN, DeleteYN, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
values(newid(), 'CouponCategory1', 'CouponCategory2', 'CouponCategory3', 'C21BA95D-B736-4123-8531-75B7C5E8C906', '2', 'ItemStatus', 'TargetGroup', 'TargetOS', 'TargetDevice', 'Title2', 'Content', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', 'Y', 0, '2015-03-01', '2015-04-01', 'admin1', 'N', 'N', '', '', '', '')
insert into CloudBread.Coupon(CouponID, CouponCategory1, CouponCategory2, CouponCategory3, ItemListID, ItemCount, ItemStatus, TargetGroup, TargetOS, TargetDevice, Title, Content, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, DupeYN, OrderNumber, CouponDurationFrom, CouponDurationTo, CreateAdminID, HideYN, DeleteYN, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
values(newid(), 'CouponCategory1', 'CouponCategory2', 'CouponCategory3', '5395E0E0-9C9F-4A5E-995E-668C7E5B1415', '3', 'ItemStatus', 'TargetGroup', 'TargetOS', 'TargetDevice', 'Title3', 'Content', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', 'N', 0, '2015-03-01', '2015-04-01', 'admin1', 'N', 'N', '', '', '', '')
insert into CloudBread.Coupon(CouponID, CouponCategory1, CouponCategory2, CouponCategory3, ItemListID, ItemCount, ItemStatus, TargetGroup, TargetOS, TargetDevice, Title, Content, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, DupeYN, OrderNumber, CouponDurationFrom, CouponDurationTo, CreateAdminID, HideYN, DeleteYN, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
values(newid(), 'CouponCategory1', 'CouponCategory2', 'CouponCategory3', 'A2D24A56-9314-40C4-AD5E-12F6A08C8DD9', '4', 'ItemStatus', 'TargetGroup', 'TargetOS', 'TargetDevice', 'Title4', 'Content', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', 'Y', 0, '2015-03-01', '2015-04-01', 'admin1', 'N', 'N', '', '', '', '')
select * from CloudBread.Coupon

--CouponMember  테이블 데이터 삽입
--truncate table CloudBread.Coupon
--SELECT COLUMN_NAME + ', ' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'CouponMember'
--SELECT '''' + COLUMN_NAME + ''', ' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'CouponMember'
insert into CloudBread.CouponMember(CouponMemberID, CouponID, MemberID, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, HideYN, DeleteYN, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
values(newid(), 'ABC00D7A-4047-4A87-A61A-C4E96F62F8E9', 'aaa', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', 'N', 'N', '', '', '', '')
insert into CloudBread.CouponMember(CouponMemberID, CouponID, MemberID, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, HideYN, DeleteYN, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
values(newid(), 'AE2BB4AE-0296-43A0-9A02-FADFD402E6A5', 'aaa', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', 'N', 'N', '', '', '', '')
insert into CloudBread.CouponMember(CouponMemberID, CouponID, MemberID, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, HideYN, DeleteYN, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
values(newid(), 'FB336164-DC1E-4343-A740-CDEB6AD4CFD2', 'aaa', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', 'N', 'N', '', '', '', '')
insert into CloudBread.CouponMember(CouponMemberID, CouponID, MemberID, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, HideYN, DeleteYN, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
values(newid(), 'ABC00D7A-4047-4A87-A61A-C4E96F62F8E9', 'bbb', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', 'N', 'N', '', '', '', '')
select * from CloudBread.CouponMember
select * from CloudBread.Coupon
select * from CloudBread.Members


--SELECT * FROM CloudBread.MemberAccountBlockLog
--truncate table CloudBread.MemberAccountBlockLog
--SELECT COLUMN_NAME + ', ' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'MemberAccountBlockLog'
--SELECT '''' + COLUMN_NAME + ''', ' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'MemberAccountBlockLog'
insert into CloudBread.MemberAccountBlockLog(MemberAccountBlockID, MemberID, MemberAccountBlockReasonCategory1, MemberAccountBlockReasonCategory2, MemberAccountBlockReasonCategory3, MemberAccountBlockReason, MemberAccountBlockProcess, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, CreateAdminID, HideYN, DeleteYN, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
values(newid(), 'ddd', '치팅사용', 'MemberAccountBlockReasonCategory2', 'MemberAccountBlockReasonCategory3', 'MemberAccountBlockReason', '제한시작', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', 'admin1', 'N', 'N', '', '', '', '')
insert into CloudBread.MemberAccountBlockLog(MemberAccountBlockID, MemberID, MemberAccountBlockReasonCategory1, MemberAccountBlockReasonCategory2, MemberAccountBlockReasonCategory3, MemberAccountBlockReason, MemberAccountBlockProcess, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, CreateAdminID, HideYN, DeleteYN, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
values(newid(), 'ccc', '치팅사용', 'MemberAccountBlockReasonCategory2', 'MemberAccountBlockReasonCategory3', 'MemberAccountBlockReason', '제한종료', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', 'admin1', 'N', 'N', '', '', '', '')
insert into CloudBread.MemberAccountBlockLog(MemberAccountBlockID, MemberID, MemberAccountBlockReasonCategory1, MemberAccountBlockReasonCategory2, MemberAccountBlockReasonCategory3, MemberAccountBlockReason, MemberAccountBlockProcess, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, CreateAdminID, HideYN, DeleteYN, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
values(newid(), 'ddd', '치팅사용', 'MemberAccountBlockReasonCategory2', 'MemberAccountBlockReasonCategory3', 'MemberAccountBlockReason', '제한종료', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', 'admin1', 'N', 'N', '', '', '', '')
insert into CloudBread.MemberAccountBlockLog(MemberAccountBlockID, MemberID, MemberAccountBlockReasonCategory1, MemberAccountBlockReasonCategory2, MemberAccountBlockReasonCategory3, MemberAccountBlockReason, MemberAccountBlockProcess, sCol1, sCol2, sCol3, sCol4, sCol5, sCol6, sCol7, sCol8, sCol9, sCol10, CreateAdminID, HideYN, DeleteYN, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
values(newid(), 'ddd', '치팅사용', 'MemberAccountBlockReasonCategory2', 'MemberAccountBlockReasonCategory3', 'MemberAccountBlockReason', '계정삭제', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', 'sCol6', 'sCol7', 'sCol8', 'sCol9', 'sCol10', 'admin1', 'N', 'N', '', '', '', '')
select * from CloudBread.MemberAccountBlockLog


--SELECT * FROM CloudBread.ServerInfo
--truncate table CloudBread.ServerInfo
--SELECT COLUMN_NAME + ', ' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'ServerInfo'
--SELECT '''' + COLUMN_NAME + ''', ' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME LIKE 'ServerInfo'
insert into CloudBread.ServerInfo(InfoID, FEServerLists, SocketServerLists, [Version], ResourceLink, EULAText, sCol1, sCol2, sCol3, sCol4, sCol5, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
values(newid(), 'FEServerLists', 'SocketServerLists', '1.0', 'ResourceLink', 'EULAText', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', '1900-01-01', '1900-01-01', '', '1900-01-01')
insert into CloudBread.ServerInfo(InfoID, FEServerLists, SocketServerLists, [Version], ResourceLink, EULAText, sCol1, sCol2, sCol3, sCol4, sCol5, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
values(newid(), '프론트엔드1,프론트엔드2', '소켓서버1,소켓서버2', '1.2', 'blob주소링크', '법적인문구', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', '1900-01-01', '1900-01-01', '', '1900-01-01')
insert into CloudBread.ServerInfo(InfoID, FEServerLists, SocketServerLists, [Version], ResourceLink, EULAText, sCol1, sCol2, sCol3, sCol4, sCol5, CreatedAt, UpdatedAt, DataFromRegion, DataFromRegionDT)
values(newid(), '웹서버1, 웹서버2', 'tcp서버1, tcp서버2', '1.0', 'ResourceLink', 'EULAText', 'sCol1', 'sCol2', 'sCol3', 'sCol4', 'sCol5', '1900-01-01', '1900-01-01', '', '1900-01-01')

