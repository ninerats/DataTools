
delete from job
delete from person

INSERT [dbo].[Person] ([person], [age]) VALUES (N'Alice', 34)
INSERT [dbo].[Person] ([person], [age]) VALUES (N'Bob', 32)
INSERT [dbo].[Person] ([person], [age]) VALUES (N'Ted', 12)

INSERT [dbo].[Job] ([JobID], [person], [title]) VALUES (1, N'Bob', N'Janitor')
INSERT [dbo].[Job] ([JobID], [person], [title]) VALUES (2, N'Bob', N'Fry Cook')
INSERT [dbo].[Job] ([JobID], [person], [title]) VALUES (3, N'Alice', N'Head Sushi Roll Designer')
INSERT [dbo].[Job] ([JobID], [person], [title]) VALUES (4, NULL, N'vacant position')

