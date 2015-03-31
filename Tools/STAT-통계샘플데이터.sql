
--USE [CloudBreadDB]
--GO

--/****** Object:  Table [CloudBread].[StatsData]    Script Date: 2015-02-26 오후 9:04:43 ******/
--SET ANSI_NULLS ON
--GO

--SET QUOTED_IDENTIFIER ON
--GO

--CREATE TABLE [CloudBread].[StatsData](
--	[StatID] [nvarchar](50) NOT NULL primary key,
--	[CategoryName] [nvarchar](max) NOT NULL,
--	[CountNum] [int] NOT NULL,
--	[Fields] [nvarchar](max) NOT NULL,
--	[CreatedAt] [datetimeoffset](7) NOT NULL,
--	[UpdatedAt] [datetimeoffset](7) NOT NULL
--) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

--GO

--ALTER TABLE [CloudBread].[StatsData] ADD  CONSTRAINT [DF_StatsData_StatID]  DEFAULT (newid()) FOR [StatID]
--GO

--ALTER TABLE [CloudBread].[StatsData] ADD  CONSTRAINT [DF_Table_1_Group]  DEFAULT ('') FOR [CategoryName]
--GO

--ALTER TABLE [CloudBread].[StatsData] ADD  CONSTRAINT [DF_Table_1_Count]  DEFAULT ('') FOR [CountNum]
--GO

--ALTER TABLE [CloudBread].[StatsData] ADD  CONSTRAINT [DF_StatsData_Fileds]  DEFAULT ('') FOR [Fields]
--GO

--ALTER TABLE [CloudBread].[StatsData] ADD  CONSTRAINT [DF_StatsData_CreatedAt]  DEFAULT (sysutcdatetime()) FOR [CreatedAt]
--GO

--ALTER TABLE [CloudBread].[StatsData] ADD  CONSTRAINT [DF_StatsData_UpdatedAt]  DEFAULT (sysutcdatetime()) FOR [UpdatedAt]
--GO




--truncate table CloudBread.StatsData
select * from CloudBread.StatsData where CategoryName like 'HDAU' order by CreatedAt asc

-- 통계 데이터 삽입 쿼리
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('HDAU', 110, '022601')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('HDAU', 150, '022602')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('HDAU', 130, '022603')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('HDAU', 200, '022604')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('HDAU', 210, '022605')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('HDAU', 300, '022606')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('HDAU', 400, '022607')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('HDAU', 550, '022608')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('HDAU', 780, '022609')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('HDAU', 500, '0226010')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('HDAU', 200, '0226011')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('HDAU', 400, '0226012')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('HDAU', 450, '0226013')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('HDAU', 430, '0226014')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('HDAU', 330, '0226015')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('HDAU', 230, '0226016')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('HDAU', 210, '0226017')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('HDAU', 430, '0226018')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('HDAU', 490, '0226019')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('HDAU', 620, '0226020')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('HDAU', 660, '0226021')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('HDAU', 710, '0226022')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('HDAU', 650, '0226023')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('HDAU', 430, '0226024')
GO



select * from CloudBread.StatsData where CategoryName like 'DDAU' order by CreatedAt asc
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('DDAU', 1102, '0201')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('DDAU', 1590, '0202')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('DDAU', 2012, '0203')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('DDAU', 3453, '0204')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('DDAU', 2345, '0205')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('DDAU', 5645, '0206')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('DDAU', 3998, '0207')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('DDAU', 4320, '0208')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('DDAU', 3832, '0209')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('DDAU', 5032, '0210')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('DDAU', 4210, '0211')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('DDAU', 5432, '0212')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('DDAU', 6436, '0213')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('DDAU', 7543, '0214')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('DDAU', 8865, '0215')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('DDAU', 7768, '0216')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('DDAU', 9974, '0217')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('DDAU', 7112, '0218')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('DDAU', 7787, '0219')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('DDAU', 7324, '0220')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('DDAU', 8802, '0221')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('DDAU', 9943, '0222')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('DDAU', 10032, '0223')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('DDAU', 11202, '0224')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('DDAU', 9768, '0225')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('DDAU', 7789, '0226')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('DDAU', 8876, '0227')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('DDAU', 7644, '0228')
GO 




select * from CloudBread.StatsData where CategoryName like 'CASHITEM' order by CreatedAt asc
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('CASHITEM', 210, '0201')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('CASHITEM', 232, '0202')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('CASHITEM', 244, '0203')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('CASHITEM', 265, '0204')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('CASHITEM', 276, '0205')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('CASHITEM', 299, '0206')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('CASHITEM', 302, '0207')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('CASHITEM', 283, '0208')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('CASHITEM', 299, '0209')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('CASHITEM', 321, '0210')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('CASHITEM', 211, '0211')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('CASHITEM', 299, '0212')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('CASHITEM', 341, '0213')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('CASHITEM', 331, '0214')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('CASHITEM', 259, '0215')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('CASHITEM', 292, '0216')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('CASHITEM', 320, '0217')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('CASHITEM', 329, '0218')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('CASHITEM', 353, '0219')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('CASHITEM', 398, '0220')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('CASHITEM', 406, '0221')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('CASHITEM', 429, '0222')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('CASHITEM', 449, '0223')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('CASHITEM', 476, '0224')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('CASHITEM', 465, '0225')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('CASHITEM', 430, '0226')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('CASHITEM', 410, '0227')
GO 
insert into CloudBread.StatsData(CategoryName, CountNum, Fields) values('CASHITEM', 432, '0228')
GO 