/****** Object:  Table [dbo].[Account]    Script Date: 10/09/2021 10:16:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[AccountId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[FullName] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Phone] [nvarchar](max) NULL,
	[Gender] [int] NULL,
	[Status] [int] NULL,
	[Avatar] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AccountRole]    Script Date: 10/09/2021 10:16:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountRole](
	[AccountRoleId] [int] IDENTITY(1,1) NOT NULL,
	[AccountId] [int] NULL,
	[RoleId] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Role]    Script Date: 10/09/2021 10:16:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Account] ON 

INSERT [dbo].[Account] ([AccountId], [UserName], [Password], [FullName], [Address], [Email], [Phone], [Gender], [Status], [Avatar]) VALUES (1, N'superadmin@gmail.com', N'8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', N'Admin', N'TPHCM', N'superadmin@gmail.com', N'0909000192', 0, 0, N'no-avatar.jpg')
INSERT [dbo].[Account] ([AccountId], [UserName], [Password], [FullName], [Address], [Email], [Phone], [Gender], [Status], [Avatar]) VALUES (3, N'admin@gmail.com', N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'admin', N'HN', N'admin@gmail.com', N'0978741541', 1, 1, N'star-1.jpg')
INSERT [dbo].[Account] ([AccountId], [UserName], [Password], [FullName], [Address], [Email], [Phone], [Gender], [Status], [Avatar]) VALUES (6, N'user@gmail.com', N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'User', N'HN', N'user@gmail.com', N'0974589634', 1, 1, N'star-0.jpg')
SET IDENTITY_INSERT [dbo].[Account] OFF
SET IDENTITY_INSERT [dbo].[AccountRole] ON 

INSERT [dbo].[AccountRole] ([AccountRoleId], [AccountId], [RoleId]) VALUES (11, 1, 0)
INSERT [dbo].[AccountRole] ([AccountRoleId], [AccountId], [RoleId]) VALUES (8, 6, 3)
INSERT [dbo].[AccountRole] ([AccountRoleId], [AccountId], [RoleId]) VALUES (7, 3, 2)
SET IDENTITY_INSERT [dbo].[AccountRole] OFF
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([RoleId], [RoleName]) VALUES (1, N'SuperAdmin')
INSERT [dbo].[Role] ([RoleId], [RoleName]) VALUES (2, N'Admin')
INSERT [dbo].[Role] ([RoleId], [RoleName]) VALUES (3, N'User')
SET IDENTITY_INSERT [dbo].[Role] OFF
