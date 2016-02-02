DELETE FROM [dbo].[BulkLoadTestTable1]
INSERT [dbo].[BulkLoadTestTable1] ([ID], [varchar1], [int1], [missing]) VALUES (1, N'Alpha', 11, N'missing 1')
INSERT [dbo].[BulkLoadTestTable1] ([ID], [varchar1], [int1], [missing]) VALUES (2, N'Bravo', 22, N'missing 2')
INSERT [dbo].[BulkLoadTestTable1] ([ID], [varchar1], [int1], [missing]) VALUES (3, N'Charlie', 33, N'missing 3')
