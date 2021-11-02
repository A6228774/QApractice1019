USE [master]
GO
/****** Object:  Database [SimpleQuestionnaire]    Script Date: 2021/11/01 14:01:25 ******/
CREATE DATABASE [SimpleQuestionnaire]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SimpleQuestionnaire', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\SimpleQuestionnaire.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SimpleQuestionnaire_log', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\SimpleQuestionnaire_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [SimpleQuestionnaire] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SimpleQuestionnaire].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SimpleQuestionnaire] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SimpleQuestionnaire] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SimpleQuestionnaire] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SimpleQuestionnaire] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SimpleQuestionnaire] SET ARITHABORT OFF 
GO
ALTER DATABASE [SimpleQuestionnaire] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SimpleQuestionnaire] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SimpleQuestionnaire] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SimpleQuestionnaire] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SimpleQuestionnaire] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SimpleQuestionnaire] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SimpleQuestionnaire] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SimpleQuestionnaire] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SimpleQuestionnaire] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SimpleQuestionnaire] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SimpleQuestionnaire] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SimpleQuestionnaire] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SimpleQuestionnaire] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SimpleQuestionnaire] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SimpleQuestionnaire] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SimpleQuestionnaire] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SimpleQuestionnaire] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SimpleQuestionnaire] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [SimpleQuestionnaire] SET  MULTI_USER 
GO
ALTER DATABASE [SimpleQuestionnaire] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SimpleQuestionnaire] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SimpleQuestionnaire] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SimpleQuestionnaire] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SimpleQuestionnaire] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SimpleQuestionnaire] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [SimpleQuestionnaire] SET QUERY_STORE = OFF
GO
USE [SimpleQuestionnaire]
GO
/****** Object:  Table [dbo].[ChoiceTable]    Script Date: 2021/11/01 14:01:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChoiceTable](
	[ChoiceID] [int] IDENTITY(1,1) NOT NULL,
	[FirstChoice] [nvarchar](20) NULL,
	[SecondChoice] [nvarchar](20) NULL,
	[ThirdChoice] [nvarchar](20) NULL,
	[ForthChoice] [nvarchar](20) NULL,
	[FifthChoice] [nvarchar](20) NULL,
	[SixthChoice] [nvarchar](20) NULL,
	[ChoiceCount] [int] NOT NULL,
 CONSTRAINT [PK_ChoiceTable_1] PRIMARY KEY CLUSTERED 
(
	[ChoiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomizeQA]    Script Date: 2021/11/01 14:01:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomizeQA](
	[QAID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NULL,
	[Summary] [nvarchar](1000) NOT NULL,
	[IsEnabled] [bit] NOT NULL,
 CONSTRAINT [PK_QATable] PRIMARY KEY CLUSTERED 
(
	[QAID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QADesign]    Script Date: 2021/11/01 14:01:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QADesign](
	[QAID] [int] NOT NULL,
	[QuestionID] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuestionsTable]    Script Date: 2021/11/01 14:01:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuestionsTable](
	[QuestionID] [int] IDENTITY(1,1) NOT NULL,
	[QuestionTitle] [nvarchar](50) NOT NULL,
	[QuestionType] [nvarchar](50) NOT NULL,
	[MustKey] [bit] NOT NULL,
	[ChoiceID] [int] NULL,
	[CommonQuestion] [bit] NULL,
 CONSTRAINT [PK_QuestionsTable] PRIMARY KEY CLUSTERED 
(
	[QuestionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Respondent_answer]    Script Date: 2021/11/01 14:01:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Respondent_answer](
	[RespondentID] [uniqueidentifier] NOT NULL,
	[QAID] [int] NOT NULL,
	[QuestionID] [int] NOT NULL,
	[ChoiceID] [int] NULL,
	[Answer] [nvarchar](100) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RespondentInfo]    Script Date: 2021/11/01 14:01:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RespondentInfo](
	[RespondentID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](10) NOT NULL,
	[Age] [int] NOT NULL,
 CONSTRAINT [PK_RespondentInfo] PRIMARY KEY CLUSTERED 
(
	[RespondentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[RespondentInfo] ADD  CONSTRAINT [DF_RespondentInfo_RespondentID]  DEFAULT (newid()) FOR [RespondentID]
GO
ALTER TABLE [dbo].[QADesign]  WITH CHECK ADD  CONSTRAINT [FK_QADesign_CustomizeQA] FOREIGN KEY([QuestionID])
REFERENCES [dbo].[QuestionsTable] ([QuestionID])
GO
ALTER TABLE [dbo].[QADesign] CHECK CONSTRAINT [FK_QADesign_CustomizeQA]
GO
USE [master]
GO
ALTER DATABASE [SimpleQuestionnaire] SET  READ_WRITE 
GO
