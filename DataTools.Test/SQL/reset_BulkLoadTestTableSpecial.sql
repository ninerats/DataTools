SET IDENTITY_INSERT [dbo].[BulkLoadTestTableSpecial] ON 
INSERT [dbo].[BulkLoadTestTableSpecial] ([ID], [HasDefault], [TextColumn], [varcharMax]) VALUES (1, N'actual value', NULL, NULL)
INSERT [dbo].[BulkLoadTestTableSpecial] ([ID], [HasDefault], [TextColumn], [varcharMax]) VALUES (2, N'', N'text data', N'HasDefault should be blank')
INSERT [dbo].[BulkLoadTestTableSpecial] ([ID], [HasDefault], [TextColumn], [varcharMax]) VALUES (3, N'default', N'default triggered', NULL)
SET IDENTITY_INSERT [dbo].[BulkLoadTestTableSpecial] OFF
