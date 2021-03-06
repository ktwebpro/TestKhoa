/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 14/09/2021 10:20:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Account]    Script Date: 14/09/2021 10:20:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[AccountId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL DEFAULT (N''),
	[Password] [nvarchar](100) NOT NULL DEFAULT (N''),
	[FullName] [nvarchar](50) NULL,
	[Address] [nvarchar](200) NULL,
	[Email] [nvarchar](50) NULL,
	[Phone] [nvarchar](15) NULL,
	[Gender] [tinyint] NOT NULL DEFAULT (CONVERT([tinyint],(0))),
	[Status] [smallint] NOT NULL DEFAULT (CONVERT([smallint],(0))),
	[Avatar] [nvarchar](50) NULL,
	[UserCode] [nvarchar](10) NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AccountRole]    Script Date: 14/09/2021 10:20:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountRole](
	[AccountRoleId] [int] IDENTITY(1,1) NOT NULL,
	[AccountId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_AccountRole] PRIMARY KEY CLUSTERED 
(
	[AccountRoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Role]    Script Date: 14/09/2021 10:20:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](max) NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210912094003_DB1', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210913034356_db2', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210913035217_db3', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210913040018_db4', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210913040247_db5', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210913042259_db6', N'5.0.9')
SET IDENTITY_INSERT [dbo].[Account] ON 

INSERT [dbo].[Account] ([AccountId], [UserName], [Password], [FullName], [Address], [Email], [Phone], [Gender], [Status], [Avatar], [UserCode]) VALUES (1, N'superadmin@gmail.com', N'8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', N'Admin', N'TPHCM', N'superadmin@gmail.com', N'0909000192', 0, 1, N'no-avatar.jpg', N'7QBmbX')
INSERT [dbo].[Account] ([AccountId], [UserName], [Password], [FullName], [Address], [Email], [Phone], [Gender], [Status], [Avatar], [UserCode]) VALUES (2, N'admin@gmail.com', N'B7158B64A98516B31D0C23609F69265A868C594DDA5B3C8DA9E13159E209C9B6', N'admin', N'HN', N'admin@gmail.com', N'0978741541', 1, 0, N'185.jpg', N'N4TWuQ')
INSERT [dbo].[Account] ([AccountId], [UserName], [Password], [FullName], [Address], [Email], [Phone], [Gender], [Status], [Avatar], [UserCode]) VALUES (3, N'user@gmail.com', N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'User', N'HN', N'user@gmail.com', N'0974589634', 1, 1, N'star-0.jpg', N'L267mG')
INSERT [dbo].[Account] ([AccountId], [UserName], [Password], [FullName], [Address], [Email], [Phone], [Gender], [Status], [Avatar], [UserCode]) VALUES (7, N'user_s@gmail.com', N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'user_s', N'user_s', N'user_s@gmail.com', N'0909457412', 0, 1, N'820.jpg', N'10c2ab96')
SET IDENTITY_INSERT [dbo].[Account] OFF
SET IDENTITY_INSERT [dbo].[AccountRole] ON 

INSERT [dbo].[AccountRole] ([AccountRoleId], [AccountId], [RoleId]) VALUES (1, 1, 1)
INSERT [dbo].[AccountRole] ([AccountRoleId], [AccountId], [RoleId]) VALUES (6, 5, 3)
INSERT [dbo].[AccountRole] ([AccountRoleId], [AccountId], [RoleId]) VALUES (16, 2, 2)
INSERT [dbo].[AccountRole] ([AccountRoleId], [AccountId], [RoleId]) VALUES (17, 3, 3)
INSERT [dbo].[AccountRole] ([AccountRoleId], [AccountId], [RoleId]) VALUES (19, 7, 3)
SET IDENTITY_INSERT [dbo].[AccountRole] OFF
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([RoleId], [RoleName]) VALUES (1, N'SuperAdmin')
INSERT [dbo].[Role] ([RoleId], [RoleName]) VALUES (2, N'Admin')
INSERT [dbo].[Role] ([RoleId], [RoleName]) VALUES (3, N'User')
SET IDENTITY_INSERT [dbo].[Role] OFF
