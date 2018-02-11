USE [imdb]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 12-02-2018 01:54:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Actor]    Script Date: 12-02-2018 01:54:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Actor](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[DOB] [datetime] NOT NULL,
	[Sex] [nvarchar](1) NOT NULL,
 CONSTRAINT [PK_dbo.Actor] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ActorMovie]    Script Date: 12-02-2018 01:54:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActorMovie](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ActorID] [int] NOT NULL,
	[MovieID] [int] NOT NULL,
 CONSTRAINT [PK_dbo.ActorMovie] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Movie]    Script Date: 12-02-2018 01:54:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Movie](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Plot] [nvarchar](500) NOT NULL,
	[Poster] [nvarchar](50) NOT NULL,
	[ProducerID] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Movie] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Producer]    Script Date: 12-02-2018 01:54:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Producer](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[DOB] [datetime] NOT NULL,
	[Sex] [nvarchar](1) NOT NULL,
 CONSTRAINT [PK_dbo.Producer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Actor] ADD  DEFAULT ('') FOR [Sex]
GO
ALTER TABLE [dbo].[Producer] ADD  DEFAULT ('') FOR [Sex]
GO
ALTER TABLE [dbo].[ActorMovie]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ActorMovie_dbo.Actor_ActorID] FOREIGN KEY([ActorID])
REFERENCES [dbo].[Actor] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ActorMovie] CHECK CONSTRAINT [FK_dbo.ActorMovie_dbo.Actor_ActorID]
GO
ALTER TABLE [dbo].[ActorMovie]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ActorMovie_dbo.Movie_MovieID] FOREIGN KEY([MovieID])
REFERENCES [dbo].[Movie] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ActorMovie] CHECK CONSTRAINT [FK_dbo.ActorMovie_dbo.Movie_MovieID]
GO
ALTER TABLE [dbo].[Movie]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Movie_dbo.Producer_ProducerID] FOREIGN KEY([ProducerID])
REFERENCES [dbo].[Producer] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Movie] CHECK CONSTRAINT [FK_dbo.Movie_dbo.Producer_ProducerID]
GO
