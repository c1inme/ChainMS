USE [ChainMS]
GO
/****** Object:  Table [dbo].[Gallery]    Script Date: 05/02/2013 17:24:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Gallery]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Gallery](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CreateBy] [uniqueidentifier] NULL,
	[ModifyBy] [uniqueidentifier] NULL,
	[ModifyDate] [datetime] NULL,
	[CreateDate] [datetime] NULL,
	[VersionNumber] [numeric](18, 0) NULL,
	[GuidId] [uniqueidentifier] NOT NULL,
	[Deleted] [bit] NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Description] [nvarchar](250) NULL,
	[Link] [nvarchar](250) NULL,
	[SourceUrl] [nvarchar](250) NULL,
	[SortOrder] [int] NULL,
	[IsActive] [bit] NULL,
	[TypeEnum] [int] NULL,
 CONSTRAINT [PK_Gallery] PRIMARY KEY CLUSTERED 
(
	[GuidId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Comment]    Script Date: 05/02/2013 17:24:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Comment]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Comment](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CreateBy] [uniqueidentifier] NULL,
	[ModifyBy] [uniqueidentifier] NULL,
	[ModifyDate] [datetime] NULL,
	[CreateDate] [datetime] NULL,
	[VersionNumber] [numeric](18, 0) NULL,
	[GuidId] [uniqueidentifier] NOT NULL,
	[Deleted] [bit] NULL,
	[IdFather] [uniqueidentifier] NULL,
	[IdBelong] [uniqueidentifier] NOT NULL,
	[Discriminator] [int] NULL,
	[Content] [nvarchar](max) NULL,
	[UserId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED 
(
	[GuidId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Users]    Script Date: 05/02/2013 17:24:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Users](
	[Id] [bigint] NOT NULL,
	[CreateBy] [uniqueidentifier] NULL,
	[ModifyBy] [uniqueidentifier] NULL,
	[ModifyDate] [datetime] NULL,
	[CreateDate] [datetime] NULL,
	[VersionNumber] [numeric](18, 0) NULL,
	[GuidId] [uniqueidentifier] NOT NULL,
	[Deleted] [bit] NULL,
	[Alias] [nvarchar](50) NULL,
	[Password] [nvarchar](500) NULL,
	[ShortName] [nvarchar](50) NULL,
	[FullName] [nvarchar](500) NULL,
	[EmailUser] [nvarchar](500) NULL,
	[MobileUser] [nvarchar](500) NULL,
	[IsAdmin] [bit] NULL,
	[ImagePath] [nvarchar](max) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[GuidId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[TraceChanges]    Script Date: 05/02/2013 17:24:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TraceChanges]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TraceChanges](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CreateBy] [uniqueidentifier] NULL,
	[ModifyBy] [uniqueidentifier] NULL,
	[ModifyDate] [datetime] NULL,
	[CreateDate] [datetime] NULL,
	[VersionNumber] [numeric](18, 0) NULL,
	[GuidId] [uniqueidentifier] NOT NULL,
	[Deleted] [bit] NULL,
	[TableChange] [nvarchar](50) NULL,
	[PropertyChange] [nvarchar](50) NULL,
	[OldValue] [nvarchar](max) NULL,
	[NewValue] [nvarchar](max) NULL,
	[VersionChange] [int] NOT NULL,
	[GuiIdChange] [uniqueidentifier] NULL,
 CONSTRAINT [PK_TraceChanges_1] PRIMARY KEY CLUSTERED 
(
	[GuidId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[TraceChanges] ON
INSERT [dbo].[TraceChanges] ([Id], [CreateBy], [ModifyBy], [ModifyDate], [CreateDate], [VersionNumber], [GuidId], [Deleted], [TableChange], [PropertyChange], [OldValue], [NewValue], [VersionChange], [GuiIdChange]) VALUES (15, NULL, NULL, CAST(0x0000A1B2011C9E17 AS DateTime), CAST(0x0000A1B2011C9E17 AS DateTime), CAST(1 AS Numeric(18, 0)), N'88c7d76d-06f3-4d9d-b4bb-7f82032a212c', NULL, N'News', N'Title', N'Vai vai ~~~', N' sdsadad zzzz', 3, N'ddc9e35f-db27-439d-93ec-14691d7e1b07')
INSERT [dbo].[TraceChanges] ([Id], [CreateBy], [ModifyBy], [ModifyDate], [CreateDate], [VersionNumber], [GuidId], [Deleted], [TableChange], [PropertyChange], [OldValue], [NewValue], [VersionChange], [GuiIdChange]) VALUES (14, NULL, NULL, CAST(0x0000A1B2011C9282 AS DateTime), CAST(0x0000A1B2011C9282 AS DateTime), CAST(1 AS Numeric(18, 0)), N'e99b58df-7607-4f60-b7c5-bcf138a7beab', NULL, N'News', N'Title', N'fffffffffffff', N'Vai vai ~~~', 2, N'ddc9e35f-db27-439d-93ec-14691d7e1b07')
INSERT [dbo].[TraceChanges] ([Id], [CreateBy], [ModifyBy], [ModifyDate], [CreateDate], [VersionNumber], [GuidId], [Deleted], [TableChange], [PropertyChange], [OldValue], [NewValue], [VersionChange], [GuiIdChange]) VALUES (13, NULL, NULL, CAST(0x0000A1B2011C7639 AS DateTime), CAST(0x0000A1B2011C7639 AS DateTime), CAST(1 AS Numeric(18, 0)), N'764c7719-13ed-4890-ac26-d46f9d6f2e26', NULL, N'News', N'Title', N'65645', N'fffffffffffff', 1, N'ddc9e35f-db27-439d-93ec-14691d7e1b07')
INSERT [dbo].[TraceChanges] ([Id], [CreateBy], [ModifyBy], [ModifyDate], [CreateDate], [VersionNumber], [GuidId], [Deleted], [TableChange], [PropertyChange], [OldValue], [NewValue], [VersionChange], [GuiIdChange]) VALUES (16, NULL, NULL, CAST(0x0000A1B2011CE0E3 AS DateTime), CAST(0x0000A1B2011CE0E3 AS DateTime), CAST(1 AS Numeric(18, 0)), N'9adea3d6-b8f0-462a-8fc2-ea9519fe8e91', NULL, N'News', N'Title', N' sdsadad zzzz', N' sdsadad zzzz test te le', 4, N'ddc9e35f-db27-439d-93ec-14691d7e1b07')
SET IDENTITY_INSERT [dbo].[TraceChanges] OFF
/****** Object:  Table [dbo].[TableLastModified]    Script Date: 05/02/2013 17:24:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TableLastModified]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TableLastModified](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CreateBy] [uniqueidentifier] NULL,
	[ModifyBy] [uniqueidentifier] NULL,
	[ModifyDate] [datetime] NULL,
	[CreateDate] [datetime] NULL,
	[VersionNumber] [numeric](18, 0) NULL,
	[GuidId] [uniqueidentifier] NOT NULL,
	[Deleted] [bit] NULL,
	[TableName] [nvarchar](50) NULL,
 CONSTRAINT [PK_TableLastModified] PRIMARY KEY CLUSTERED 
(
	[GuidId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[TableLastModified] ON
INSERT [dbo].[TableLastModified] ([Id], [CreateBy], [ModifyBy], [ModifyDate], [CreateDate], [VersionNumber], [GuidId], [Deleted], [TableName]) VALUES (2, NULL, NULL, CAST(0x0000A1B2011CE0FB AS DateTime), CAST(0x0000A1B20101AD39 AS DateTime), CAST(6 AS Numeric(18, 0)), N'5d4b46af-9246-410a-af5c-0099d108d321', NULL, N'TraceChange')
INSERT [dbo].[TableLastModified] ([Id], [CreateBy], [ModifyBy], [ModifyDate], [CreateDate], [VersionNumber], [GuidId], [Deleted], [TableName]) VALUES (1, NULL, NULL, CAST(0x0000A1B2011DB0D0 AS DateTime), CAST(0x0000A1B200EDECAD AS DateTime), CAST(19 AS Numeric(18, 0)), N'4f8a0ca9-c4be-47d7-bb6c-0485b0fe7837', NULL, N'News')
SET IDENTITY_INSERT [dbo].[TableLastModified] OFF
/****** Object:  Table [dbo].[SEOContent]    Script Date: 05/02/2013 17:24:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SEOContent]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SEOContent](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CreateBy] [uniqueidentifier] NULL,
	[ModifyBy] [uniqueidentifier] NULL,
	[ModifyDate] [datetime] NULL,
	[CreateDate] [datetime] NULL,
	[VersionNumber] [numeric](18, 0) NULL,
	[GuidId] [uniqueidentifier] NOT NULL,
	[Deleted] [bit] NULL,
	[Title] [nvarchar](150) NOT NULL,
	[Description] [nvarchar](250) NULL,
 CONSTRAINT [PK_SEOContent] PRIMARY KEY CLUSTERED 
(
	[GuidId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Rating]    Script Date: 05/02/2013 17:24:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Rating]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Rating](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CreateBy] [uniqueidentifier] NULL,
	[ModifyBy] [uniqueidentifier] NULL,
	[ModifyDate] [datetime] NULL,
	[CreateDate] [datetime] NULL,
	[VersionNumber] [numeric](18, 0) NULL,
	[GuidId] [uniqueidentifier] NOT NULL,
	[Deleted] [bit] NULL,
	[IdBelong] [uniqueidentifier] NULL,
	[Discriminator] [int] NULL,
	[SumRating] [int] NULL,
	[CountCurrent] [int] NULL,
 CONSTRAINT [PK_Rating] PRIMARY KEY CLUSTERED 
(
	[GuidId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ProductCategory]    Script Date: 05/02/2013 17:24:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductCategory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProductCategory](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CreateBy] [uniqueidentifier] NULL,
	[ModifyBy] [uniqueidentifier] NULL,
	[ModifyDate] [datetime] NULL,
	[CreateDate] [datetime] NULL,
	[VersionNumber] [numeric](18, 0) NULL,
	[GuidId] [uniqueidentifier] NOT NULL,
	[Deleted] [bit] NULL,
	[Name] [nvarchar](150) NOT NULL,
	[Description] [nvarchar](250) NULL,
 CONSTRAINT [PK_ProductCategory] PRIMARY KEY CLUSTERED 
(
	[GuidId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Product]    Script Date: 05/02/2013 17:24:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Product]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Product](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CreateBy] [uniqueidentifier] NULL,
	[ModifyBy] [uniqueidentifier] NULL,
	[ModifyDate] [datetime] NULL,
	[CreateDate] [datetime] NULL,
	[VersionNumber] [numeric](18, 0) NULL,
	[GuidId] [uniqueidentifier] NOT NULL,
	[Deleted] [bit] NULL,
	[Code] [nvarchar](50) NULL,
	[Name] [nvarchar](150) NOT NULL,
	[Description] [nvarchar](250) NULL,
	[ProductCategoryId] [uniqueidentifier] NULL,
	[SellPrice] [money] NULL,
	[BuyPrice] [money] NULL,
	[ManufactureId] [int] NULL,
	[CurrentStock] [int] NULL,
	[Stock] [int] NULL,
	[IsAvailable] [bit] NULL,
	[Rating] [float] NULL,
	[ImagePath] [nvarchar](250) NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[GuidId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[PermissionDefinition]    Script Date: 05/02/2013 17:24:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PermissionDefinition]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PermissionDefinition](
	[Id] [bigint] NOT NULL,
	[CreateBy] [uniqueidentifier] NULL,
	[ModifyBy] [uniqueidentifier] NULL,
	[ModifyDate] [datetime] NULL,
	[CreateDate] [datetime] NULL,
	[VersionNumber] [numeric](18, 0) NULL,
	[GuidId] [uniqueidentifier] NOT NULL,
	[Deleted] [bit] NULL,
	[CodePermision] [nvarchar](50) NULL,
	[NamePermission] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[ActionType] [nvarchar](max) NULL,
	[SortNumber] [int] NULL,
 CONSTRAINT [PK_PermissionDefinition] PRIMARY KEY CLUSTERED 
(
	[GuidId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[News]    Script Date: 05/02/2013 17:24:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[News]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[News](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CreateBy] [uniqueidentifier] NULL,
	[ModifyBy] [uniqueidentifier] NULL,
	[ModifyDate] [datetime] NULL,
	[CreateDate] [datetime] NULL,
	[VersionNumber] [numeric](18, 0) NULL,
	[GuidId] [uniqueidentifier] NOT NULL,
	[Deleted] [bit] NULL,
	[Title] [nvarchar](250) NOT NULL,
	[MenuId] [uniqueidentifier] NULL,
	[ImagePath] [nchar](10) NULL,
	[Description] [nvarchar](max) NULL,
	[Content] [nvarchar](max) NOT NULL,
	[Tags] [nvarchar](250) NULL,
	[IsActive] [bit] NULL,
	[IsHot] [bit] NULL,
	[ViewNumber] [int] NULL,
	[PublishDate] [datetime] NULL,
	[DateExpired] [datetime] NULL,
	[Link] [nvarchar](250) NULL,
	[Discriminator] [nvarchar](50) NULL,
 CONSTRAINT [PK_News] PRIMARY KEY CLUSTERED 
(
	[GuidId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[News] ON
INSERT [dbo].[News] ([Id], [CreateBy], [ModifyBy], [ModifyDate], [CreateDate], [VersionNumber], [GuidId], [Deleted], [Title], [MenuId], [ImagePath], [Description], [Content], [Tags], [IsActive], [IsHot], [ViewNumber], [PublishDate], [DateExpired], [Link], [Discriminator]) VALUES (8, NULL, NULL, CAST(0x0000A1B200F84A47 AS DateTime), CAST(0x0000A1A300D6B9F0 AS DateTime), CAST(10 AS Numeric(18, 0)), N'd4513b55-5336-47d8-b69b-0c54aa2785d8', NULL, N'jhfjgjg', NULL, NULL, NULL, N'Content 2', N'Tag2', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[News] ([Id], [CreateBy], [ModifyBy], [ModifyDate], [CreateDate], [VersionNumber], [GuidId], [Deleted], [Title], [MenuId], [ImagePath], [Description], [Content], [Tags], [IsActive], [IsHot], [ViewNumber], [PublishDate], [DateExpired], [Link], [Discriminator]) VALUES (22, NULL, NULL, CAST(0x0000A1B2011CED13 AS DateTime), CAST(0x0000A1B2011CED13 AS DateTime), CAST(1 AS Numeric(18, 0)), N'e8cf302e-269d-4f16-a18f-0eac1c763349', NULL, N'zzzzzzzzzzzzzz', NULL, NULL, NULL, N'Content 2', N'Tag2', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[News] ([Id], [CreateBy], [ModifyBy], [ModifyDate], [CreateDate], [VersionNumber], [GuidId], [Deleted], [Title], [MenuId], [ImagePath], [Description], [Content], [Tags], [IsActive], [IsHot], [ViewNumber], [PublishDate], [DateExpired], [Link], [Discriminator]) VALUES (20, NULL, NULL, CAST(0x0000A1B2011CE108 AS DateTime), CAST(0x0000A1B2011CE108 AS DateTime), CAST(1 AS Numeric(18, 0)), N'f8d1d7d3-d7c3-44fb-a41b-1102d78c001d', NULL, N'zzzzzzzzzzzzzz', NULL, NULL, NULL, N'Content 2', N'Tag2', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[News] ([Id], [CreateBy], [ModifyBy], [ModifyDate], [CreateDate], [VersionNumber], [GuidId], [Deleted], [Title], [MenuId], [ImagePath], [Description], [Content], [Tags], [IsActive], [IsHot], [ViewNumber], [PublishDate], [DateExpired], [Link], [Discriminator]) VALUES (11, NULL, NULL, CAST(0x0000A1B2011DB0CF AS DateTime), CAST(0x0000A1B200FA4913 AS DateTime), CAST(8 AS Numeric(18, 0)), N'ddc9e35f-db27-439d-93ec-14691d7e1b07', NULL, N'fsfa', NULL, NULL, NULL, N'Content 1 Test trace', N'Tag1www cccccccc', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[News] ([Id], [CreateBy], [ModifyBy], [ModifyDate], [CreateDate], [VersionNumber], [GuidId], [Deleted], [Title], [MenuId], [ImagePath], [Description], [Content], [Tags], [IsActive], [IsHot], [ViewNumber], [PublishDate], [DateExpired], [Link], [Discriminator]) VALUES (19, NULL, NULL, CAST(0x0000A1B2011C9E18 AS DateTime), CAST(0x0000A1B2011C9E18 AS DateTime), CAST(1 AS Numeric(18, 0)), N'acaa1a54-1ac2-49f5-a051-1a713f28aa59', NULL, N'zzzzzzzzzzzzzz', NULL, NULL, NULL, N'Content 2', N'Tag2', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[News] ([Id], [CreateBy], [ModifyBy], [ModifyDate], [CreateDate], [VersionNumber], [GuidId], [Deleted], [Title], [MenuId], [ImagePath], [Description], [Content], [Tags], [IsActive], [IsHot], [ViewNumber], [PublishDate], [DateExpired], [Link], [Discriminator]) VALUES (24, NULL, NULL, CAST(0x0000A1B2011DB008 AS DateTime), CAST(0x0000A1B2011DB008 AS DateTime), CAST(1 AS Numeric(18, 0)), N'4eeeb7bf-007e-42e1-8178-2b475d686f4b', NULL, N'ssssssssss', NULL, NULL, NULL, N'Content 2', N'Tag2', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[News] ([Id], [CreateBy], [ModifyBy], [ModifyDate], [CreateDate], [VersionNumber], [GuidId], [Deleted], [Title], [MenuId], [ImagePath], [Description], [Content], [Tags], [IsActive], [IsHot], [ViewNumber], [PublishDate], [DateExpired], [Link], [Discriminator]) VALUES (3, NULL, NULL, CAST(0x0000A1B200F84A49 AS DateTime), CAST(0x0000A1A3009CED8F AS DateTime), CAST(11 AS Numeric(18, 0)), N'45f9d5aa-4b4c-4398-a5fa-2d6d80f92a66', NULL, N'zzzzzzzzzzzz', NULL, NULL, NULL, N'zzzzzzzzzzz', N'zzzzzzzzzzzzz', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[News] ([Id], [CreateBy], [ModifyBy], [ModifyDate], [CreateDate], [VersionNumber], [GuidId], [Deleted], [Title], [MenuId], [ImagePath], [Description], [Content], [Tags], [IsActive], [IsHot], [ViewNumber], [PublishDate], [DateExpired], [Link], [Discriminator]) VALUES (12, NULL, NULL, CAST(0x0000A1B200FAC1DE AS DateTime), CAST(0x0000A1B200FAC1DE AS DateTime), CAST(1 AS Numeric(18, 0)), N'a69cb291-7e54-446e-9f8c-3075ca5cd1bc', NULL, N'zzzzzzzz', NULL, NULL, NULL, N'Content 2', N'Tag2', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[News] ([Id], [CreateBy], [ModifyBy], [ModifyDate], [CreateDate], [VersionNumber], [GuidId], [Deleted], [Title], [MenuId], [ImagePath], [Description], [Content], [Tags], [IsActive], [IsHot], [ViewNumber], [PublishDate], [DateExpired], [Link], [Discriminator]) VALUES (18, NULL, NULL, CAST(0x0000A1B2011C9283 AS DateTime), CAST(0x0000A1B2011C9283 AS DateTime), CAST(1 AS Numeric(18, 0)), N'a9e6d915-02b5-43f6-9fed-420066c3a71d', NULL, N'zzzzzzzzzzzzzz', NULL, NULL, NULL, N'Content 2', N'Tag2', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[News] ([Id], [CreateBy], [ModifyBy], [ModifyDate], [CreateDate], [VersionNumber], [GuidId], [Deleted], [Title], [MenuId], [ImagePath], [Description], [Content], [Tags], [IsActive], [IsHot], [ViewNumber], [PublishDate], [DateExpired], [Link], [Discriminator]) VALUES (14, NULL, NULL, CAST(0x0000A1B200FD7A63 AS DateTime), CAST(0x0000A1B200FD7A63 AS DateTime), CAST(1 AS Numeric(18, 0)), N'1edabb47-08a3-41fe-87e6-48db45b565ea', NULL, N'dddd', NULL, NULL, NULL, N'Content 2', N'Tag2', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[News] ([Id], [CreateBy], [ModifyBy], [ModifyDate], [CreateDate], [VersionNumber], [GuidId], [Deleted], [Title], [MenuId], [ImagePath], [Description], [Content], [Tags], [IsActive], [IsHot], [ViewNumber], [PublishDate], [DateExpired], [Link], [Discriminator]) VALUES (16, NULL, NULL, CAST(0x0000A1B20118542B AS DateTime), CAST(0x0000A1B20118542B AS DateTime), CAST(1 AS Numeric(18, 0)), N'6f41c35c-ca16-49da-9688-4c60187c8f26', NULL, N'6546464', NULL, NULL, NULL, N'Content 2', N'Tag2', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[News] ([Id], [CreateBy], [ModifyBy], [ModifyDate], [CreateDate], [VersionNumber], [GuidId], [Deleted], [Title], [MenuId], [ImagePath], [Description], [Content], [Tags], [IsActive], [IsHot], [ViewNumber], [PublishDate], [DateExpired], [Link], [Discriminator]) VALUES (10, NULL, NULL, CAST(0x0000A1B200F923C2 AS DateTime), CAST(0x0000A1B200F923C2 AS DateTime), CAST(1 AS Numeric(18, 0)), N'b27aea12-132a-4a1f-a4bb-7acfa662a9db', NULL, N'zzzzzzzz', NULL, NULL, NULL, N'Content 2', N'Tag2', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[News] ([Id], [CreateBy], [ModifyBy], [ModifyDate], [CreateDate], [VersionNumber], [GuidId], [Deleted], [Title], [MenuId], [ImagePath], [Description], [Content], [Tags], [IsActive], [IsHot], [ViewNumber], [PublishDate], [DateExpired], [Link], [Discriminator]) VALUES (7, NULL, NULL, CAST(0x0000A1B200F84A49 AS DateTime), CAST(0x0000A1A300D6B9EF AS DateTime), CAST(10 AS Numeric(18, 0)), N'552e42a5-3240-4b64-8236-7e5507f8e807', NULL, N'cfgfdg', NULL, NULL, NULL, N'Content 1', N'Tag1', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[News] ([Id], [CreateBy], [ModifyBy], [ModifyDate], [CreateDate], [VersionNumber], [GuidId], [Deleted], [Title], [MenuId], [ImagePath], [Description], [Content], [Tags], [IsActive], [IsHot], [ViewNumber], [PublishDate], [DateExpired], [Link], [Discriminator]) VALUES (15, NULL, NULL, CAST(0x0000A1B200FFF888 AS DateTime), CAST(0x0000A1B200FFF888 AS DateTime), CAST(1 AS Numeric(18, 0)), N'f2c390b6-8a21-4083-aabf-809fa768cdb6', NULL, N'ssssssssssssssss', NULL, NULL, NULL, N'Content 2', N'Tag2', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[News] ([Id], [CreateBy], [ModifyBy], [ModifyDate], [CreateDate], [VersionNumber], [GuidId], [Deleted], [Title], [MenuId], [ImagePath], [Description], [Content], [Tags], [IsActive], [IsHot], [ViewNumber], [PublishDate], [DateExpired], [Link], [Discriminator]) VALUES (23, NULL, NULL, CAST(0x0000A1B2011DAB14 AS DateTime), CAST(0x0000A1B2011DAB14 AS DateTime), CAST(1 AS Numeric(18, 0)), N'7ba29464-abb1-424c-8c5c-819fc6a0e9eb', NULL, N'ssssssssss', NULL, NULL, NULL, N'Content 2', N'Tag2', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[News] ([Id], [CreateBy], [ModifyBy], [ModifyDate], [CreateDate], [VersionNumber], [GuidId], [Deleted], [Title], [MenuId], [ImagePath], [Description], [Content], [Tags], [IsActive], [IsHot], [ViewNumber], [PublishDate], [DateExpired], [Link], [Discriminator]) VALUES (25, NULL, NULL, CAST(0x0000A1B2011DB0CF AS DateTime), CAST(0x0000A1B2011DB0CF AS DateTime), CAST(1 AS Numeric(18, 0)), N'524229bd-be14-4293-bb96-9df303483633', NULL, N'ssssssssss', NULL, NULL, NULL, N'Content 2', N'Tag2', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[News] ([Id], [CreateBy], [ModifyBy], [ModifyDate], [CreateDate], [VersionNumber], [GuidId], [Deleted], [Title], [MenuId], [ImagePath], [Description], [Content], [Tags], [IsActive], [IsHot], [ViewNumber], [PublishDate], [DateExpired], [Link], [Discriminator]) VALUES (21, NULL, NULL, CAST(0x0000A1B2011CEAA5 AS DateTime), CAST(0x0000A1B2011CEAA5 AS DateTime), CAST(1 AS Numeric(18, 0)), N'bf55ef1d-d632-4cee-9351-c84405c8f466', NULL, N'zzzzzzzzzzzzzz', NULL, NULL, NULL, N'Content 2', N'Tag2', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[News] ([Id], [CreateBy], [ModifyBy], [ModifyDate], [CreateDate], [VersionNumber], [GuidId], [Deleted], [Title], [MenuId], [ImagePath], [Description], [Content], [Tags], [IsActive], [IsHot], [ViewNumber], [PublishDate], [DateExpired], [Link], [Discriminator]) VALUES (9, NULL, NULL, CAST(0x0000A1B200F84A49 AS DateTime), CAST(0x0000A1B200EDE5E5 AS DateTime), CAST(3 AS Numeric(18, 0)), N'6987e54a-89e7-4e8f-8585-e2bd99e5a507', NULL, N'Modify before save', NULL, NULL, NULL, N'sssssssssszdsa ', N'ssssssssd das ', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[News] ([Id], [CreateBy], [ModifyBy], [ModifyDate], [CreateDate], [VersionNumber], [GuidId], [Deleted], [Title], [MenuId], [ImagePath], [Description], [Content], [Tags], [IsActive], [IsHot], [ViewNumber], [PublishDate], [DateExpired], [Link], [Discriminator]) VALUES (17, NULL, NULL, CAST(0x0000A1B2011C76B2 AS DateTime), CAST(0x0000A1B2011C76B2 AS DateTime), CAST(1 AS Numeric(18, 0)), N'63e72a6d-0c2b-4694-ba54-e326924ac380', NULL, N'zzzzzzzzzzzzzz', NULL, NULL, NULL, N'Content 2', N'Tag2', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[News] ([Id], [CreateBy], [ModifyBy], [ModifyDate], [CreateDate], [VersionNumber], [GuidId], [Deleted], [Title], [MenuId], [ImagePath], [Description], [Content], [Tags], [IsActive], [IsHot], [ViewNumber], [PublishDate], [DateExpired], [Link], [Discriminator]) VALUES (13, NULL, NULL, CAST(0x0000A1B200FCF06D AS DateTime), CAST(0x0000A1B200FCF06D AS DateTime), CAST(1 AS Numeric(18, 0)), N'1b2ca0b6-0b36-4f97-bb4c-f6893e6b62be', NULL, N'dddd', NULL, NULL, NULL, N'Content 2', N'Tag2', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[News] ([Id], [CreateBy], [ModifyBy], [ModifyDate], [CreateDate], [VersionNumber], [GuidId], [Deleted], [Title], [MenuId], [ImagePath], [Description], [Content], [Tags], [IsActive], [IsHot], [ViewNumber], [PublishDate], [DateExpired], [Link], [Discriminator]) VALUES (6, NULL, NULL, CAST(0x0000A1B200F84A49 AS DateTime), CAST(0x0000A1A300D16ED6 AS DateTime), CAST(11 AS Numeric(18, 0)), N'dd87c819-41cf-4d68-bab8-f7e8277ccd85', NULL, N'Te2', NULL, NULL, NULL, N'Content 2', N'Tag2', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[News] OFF
/****** Object:  Table [dbo].[NameDictionary]    Script Date: 05/02/2013 17:24:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NameDictionary]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[NameDictionary](
	[Id] [bigint] NOT NULL,
	[CreateBy] [uniqueidentifier] NULL,
	[ModifyBy] [uniqueidentifier] NULL,
	[ModifyDate] [datetime] NULL,
	[CreateDate] [datetime] NULL,
	[VersionNumber] [numeric](18, 0) NULL,
	[GuidId] [uniqueidentifier] NOT NULL,
	[Deleted] [bit] NULL,
	[InternalName] [nvarchar](50) NULL,
	[DisplayName] [nvarchar](max) NULL,
	[TableName] [nvarchar](max) NULL,
	[IsLookup] [bit] NULL,
	[TableLookup] [nvarchar](50) NULL,
	[PropertyLookupDisplay] [nvarchar](50) NULL,
 CONSTRAINT [PK_NameDictionary] PRIMARY KEY CLUSTERED 
(
	[GuidId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[MenuCategory]    Script Date: 05/02/2013 17:24:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MenuCategory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MenuCategory](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CreateBy] [uniqueidentifier] NULL,
	[ModifyBy] [uniqueidentifier] NULL,
	[ModifyDate] [datetime] NULL,
	[CreateDate] [datetime] NULL,
	[VersionNumber] [numeric](18, 0) NULL,
	[GuidId] [uniqueidentifier] NOT NULL,
	[Deleted] [bit] NULL,
	[MenuName] [nvarchar](100) NOT NULL,
	[ParentId] [uniqueidentifier] NULL,
	[Order] [int] NULL,
	[IconImage] [nvarchar](250) NULL,
	[Description] [nvarchar](250) NULL,
	[IsShowHome] [bit] NULL,
	[IsActive] [bit] NULL,
	[Link] [nvarchar](250) NULL,
 CONSTRAINT [PK_MenuCategory] PRIMARY KEY CLUSTERED 
(
	[GuidId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Manufacture]    Script Date: 05/02/2013 17:24:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Manufacture]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Manufacture](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CreateBy] [uniqueidentifier] NULL,
	[ModifyBy] [uniqueidentifier] NULL,
	[ModifyDate] [datetime] NULL,
	[CreateDate] [datetime] NULL,
	[VersionNumber] [numeric](18, 0) NULL,
	[GuidId] [uniqueidentifier] NOT NULL,
	[Deleted] [bit] NULL,
	[Name] [nvarchar](150) NOT NULL,
	[Description] [nvarchar](250) NULL,
	[HomePage] [nvarchar](250) NULL,
 CONSTRAINT [PK_Manufacture] PRIMARY KEY CLUSTERED 
(
	[GuidId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Image]    Script Date: 05/02/2013 17:24:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Image]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Image](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CreateBy] [uniqueidentifier] NULL,
	[ModifyBy] [uniqueidentifier] NULL,
	[ModifyDate] [datetime] NULL,
	[CreateDate] [datetime] NULL,
	[VersionNumber] [numeric](18, 0) NULL,
	[GuidId] [uniqueidentifier] NOT NULL,
	[Deleted] [bit] NULL,
	[IdBelong] [uniqueidentifier] NULL,
	[Discriminator] [int] NULL,
	[Description] [nvarchar](250) NULL,
	[FullHdPath] [nvarchar](250) NULL,
	[ThumpnailPath] [nvarchar](250) NULL,
 CONSTRAINT [PK_Image] PRIMARY KEY CLUSTERED 
(
	[GuidId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[GroupPermission]    Script Date: 05/02/2013 17:24:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GroupPermission]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[GroupPermission](
	[Id] [bigint] NOT NULL,
	[CreateBy] [uniqueidentifier] NULL,
	[ModifyBy] [uniqueidentifier] NULL,
	[ModifyDate] [datetime] NULL,
	[CreateDate] [datetime] NULL,
	[VersionNumber] [numeric](18, 0) NULL,
	[GuidId] [uniqueidentifier] NOT NULL,
	[Deleted] [bit] NULL,
	[CodeGroup] [nvarchar](50) NULL,
	[NameGroup] [nvarchar](255) NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_GroupPermission] PRIMARY KEY CLUSTERED 
(
	[GuidId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[GroupMemberPermission]    Script Date: 05/02/2013 17:24:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GroupMemberPermission]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[GroupMemberPermission](
	[Id] [bigint] NOT NULL,
	[CreateBy] [uniqueidentifier] NULL,
	[ModifyBy] [uniqueidentifier] NULL,
	[ModifyDate] [datetime] NULL,
	[CreateDate] [datetime] NULL,
	[VersionNumber] [numeric](18, 0) NULL,
	[GuidId] [uniqueidentifier] NOT NULL,
	[Deleted] [bit] NULL,
	[IDUser] [uniqueidentifier] NULL,
	[IDGroupPermission] [uniqueidentifier] NULL,
 CONSTRAINT [PK_GroupMemberPermission] PRIMARY KEY CLUSTERED 
(
	[GuidId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[GrantPermission]    Script Date: 05/02/2013 17:24:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GrantPermission]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[GrantPermission](
	[Id] [bigint] NOT NULL,
	[CreateBy] [uniqueidentifier] NULL,
	[ModifyBy] [uniqueidentifier] NULL,
	[ModifyDate] [datetime] NULL,
	[CreateDate] [datetime] NULL,
	[VersionNumber] [numeric](18, 0) NULL,
	[GuidId] [uniqueidentifier] NOT NULL,
	[Deleted] [bit] NULL,
	[IDGranted] [uniqueidentifier] NULL,
	[IDDefinitionPermission] [uniqueidentifier] NULL,
	[Discriminator] [nvarchar](50) NULL,
 CONSTRAINT [PK_GrantPermission] PRIMARY KEY CLUSTERED 
(
	[GuidId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'GrantPermission', N'COLUMN',N'Discriminator'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'For User or Group' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GrantPermission', @level2type=N'COLUMN',@level2name=N'Discriminator'
GO
/****** Object:  Default [DF_Comment_GuidId]    Script Date: 05/02/2013 17:24:29 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Comment_GuidId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Comment]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Comment_GuidId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Comment] ADD  CONSTRAINT [DF_Comment_GuidId]  DEFAULT (newid()) FOR [GuidId]
END


End
GO
/****** Object:  Default [DF_Gallery_GuidId]    Script Date: 05/02/2013 17:24:29 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Gallery_GuidId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Gallery]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Gallery_GuidId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Gallery] ADD  CONSTRAINT [DF_Gallery_GuidId]  DEFAULT (newid()) FOR [GuidId]
END


End
GO
/****** Object:  Default [DF_GrantPermission_GuidId]    Script Date: 05/02/2013 17:24:29 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_GrantPermission_GuidId]') AND parent_object_id = OBJECT_ID(N'[dbo].[GrantPermission]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GrantPermission_GuidId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GrantPermission] ADD  CONSTRAINT [DF_GrantPermission_GuidId]  DEFAULT (newid()) FOR [GuidId]
END


End
GO
/****** Object:  Default [DF_GroupMemberPermission_GuidId]    Script Date: 05/02/2013 17:24:29 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_GroupMemberPermission_GuidId]') AND parent_object_id = OBJECT_ID(N'[dbo].[GroupMemberPermission]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GroupMemberPermission_GuidId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GroupMemberPermission] ADD  CONSTRAINT [DF_GroupMemberPermission_GuidId]  DEFAULT (newid()) FOR [GuidId]
END


End
GO
/****** Object:  Default [DF_GroupPermission_GuidId]    Script Date: 05/02/2013 17:24:29 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_GroupPermission_GuidId]') AND parent_object_id = OBJECT_ID(N'[dbo].[GroupPermission]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GroupPermission_GuidId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GroupPermission] ADD  CONSTRAINT [DF_GroupPermission_GuidId]  DEFAULT (newid()) FOR [GuidId]
END


End
GO
/****** Object:  Default [DF_TableLastModified_GuidId]    Script Date: 05/02/2013 17:24:29 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_TableLastModified_GuidId]') AND parent_object_id = OBJECT_ID(N'[dbo].[TableLastModified]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TableLastModified_GuidId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TableLastModified] ADD  CONSTRAINT [DF_TableLastModified_GuidId]  DEFAULT (newid()) FOR [GuidId]
END


End
GO
/****** Object:  Default [DF_TraceChanges_GuidId]    Script Date: 05/02/2013 17:24:29 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_TraceChanges_GuidId]') AND parent_object_id = OBJECT_ID(N'[dbo].[TraceChanges]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TraceChanges_GuidId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TraceChanges] ADD  CONSTRAINT [DF_TraceChanges_GuidId]  DEFAULT (newid()) FOR [GuidId]
END


End
GO
/****** Object:  ForeignKey [FK_GrantPermission_PermissionDefinition]    Script Date: 05/02/2013 17:24:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GrantPermission_PermissionDefinition]') AND parent_object_id = OBJECT_ID(N'[dbo].[GrantPermission]'))
ALTER TABLE [dbo].[GrantPermission]  WITH CHECK ADD  CONSTRAINT [FK_GrantPermission_PermissionDefinition] FOREIGN KEY([IDDefinitionPermission])
REFERENCES [dbo].[PermissionDefinition] ([GuidId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GrantPermission_PermissionDefinition]') AND parent_object_id = OBJECT_ID(N'[dbo].[GrantPermission]'))
ALTER TABLE [dbo].[GrantPermission] CHECK CONSTRAINT [FK_GrantPermission_PermissionDefinition]
GO
/****** Object:  ForeignKey [FK_GroupMemberPermission_GroupPermission]    Script Date: 05/02/2013 17:24:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GroupMemberPermission_GroupPermission]') AND parent_object_id = OBJECT_ID(N'[dbo].[GroupMemberPermission]'))
ALTER TABLE [dbo].[GroupMemberPermission]  WITH CHECK ADD  CONSTRAINT [FK_GroupMemberPermission_GroupPermission] FOREIGN KEY([IDGroupPermission])
REFERENCES [dbo].[GroupPermission] ([GuidId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GroupMemberPermission_GroupPermission]') AND parent_object_id = OBJECT_ID(N'[dbo].[GroupMemberPermission]'))
ALTER TABLE [dbo].[GroupMemberPermission] CHECK CONSTRAINT [FK_GroupMemberPermission_GroupPermission]
GO
/****** Object:  ForeignKey [FK_GroupMemberPermission_Users]    Script Date: 05/02/2013 17:24:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GroupMemberPermission_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[GroupMemberPermission]'))
ALTER TABLE [dbo].[GroupMemberPermission]  WITH CHECK ADD  CONSTRAINT [FK_GroupMemberPermission_Users] FOREIGN KEY([IDUser])
REFERENCES [dbo].[Users] ([GuidId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GroupMemberPermission_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[GroupMemberPermission]'))
ALTER TABLE [dbo].[GroupMemberPermission] CHECK CONSTRAINT [FK_GroupMemberPermission_Users]
GO
