USE [master]
GO
/****** Object:  Database [SimpleQuestionnaire]    Script Date: 2021/11/16 18:53:57 ******/
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
/****** Object:  Table [dbo].[RespondentInfo]    Script Date: 2021/11/16 18:53:57 ******/
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
/****** Object:  Table [dbo].[ResponseTable]    Script Date: 2021/11/16 18:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResponseTable](
	[RID] [int] IDENTITY(1,1) NOT NULL,
	[QAID] [int] NOT NULL,
	[AnswerDate] [datetime] NOT NULL,
	[RespondentID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_ResponseTable] PRIMARY KEY CLUSTERED 
(
	[RID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Respondent_answer]    Script Date: 2021/11/16 18:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Respondent_answer](
	[RespondentID] [uniqueidentifier] NOT NULL,
	[QAID] [int] NOT NULL,
	[QuestionID] [int] NOT NULL,
	[ChoiceID] [int] NULL,
	[Answer] [nvarchar](100) NULL,
	[RID] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[All_Answer_View]    Script Date: 2021/11/16 18:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[All_Answer_View]
AS
SELECT                   dbo.ResponseTable.AnswerDate, dbo.ResponseTable.RID, dbo.ResponseTable.RespondentID, 
                                 dbo.RespondentInfo.Name, dbo.Respondent_answer.QAID, dbo.Respondent_answer.QuestionID, 
                                 dbo.Respondent_answer.ChoiceID, dbo.Respondent_answer.Answer
FROM                     dbo.Respondent_answer INNER JOIN
                                 dbo.ResponseTable ON dbo.Respondent_answer.RID = dbo.ResponseTable.RID INNER JOIN
                                 dbo.RespondentInfo ON dbo.Respondent_answer.RespondentID = dbo.RespondentInfo.RespondentID AND 
                                 dbo.ResponseTable.RespondentID = dbo.RespondentInfo.RespondentID
GO
/****** Object:  View [dbo].[CSVOutput_View]    Script Date: 2021/11/16 18:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[CSVOutput_View]
AS
SELECT                   dbo.RespondentInfo.Name, dbo.ResponseTable.RespondentID, dbo.ResponseTable.AnswerDate, 
                                 dbo.ResponseTable.QAID
FROM                     dbo.RespondentInfo INNER JOIN
                                 dbo.ResponseTable ON dbo.RespondentInfo.RespondentID = dbo.ResponseTable.RespondentID
GO
/****** Object:  Table [dbo].[QA_Question]    Script Date: 2021/11/16 18:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QA_Question](
	[QAID] [int] NOT NULL,
	[QuestionID] [int] NOT NULL,
	[MustKey] [bit] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuestionsTable]    Script Date: 2021/11/16 18:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuestionsTable](
	[QuestionID] [int] IDENTITY(1,1) NOT NULL,
	[QuestionTitle] [nvarchar](50) NOT NULL,
	[QuestionType] [nvarchar](50) NOT NULL,
	[ChoiceID] [int] NULL,
	[CommonQuestion] [bit] NULL,
 CONSTRAINT [PK_QuestionsTable] PRIMARY KEY CLUSTERED 
(
	[QuestionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[QA_Question_View]    Script Date: 2021/11/16 18:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[QA_Question_View]
AS
SELECT                   dbo.QA_Question.MustKey, dbo.QuestionsTable.QuestionType, dbo.QuestionsTable.QuestionTitle, 
                                 dbo.QuestionsTable.QuestionID, dbo.QA_Question.QAID
FROM                     dbo.QA_Question INNER JOIN
                                 dbo.QuestionsTable ON dbo.QA_Question.QuestionID = dbo.QuestionsTable.QuestionID
GO
/****** Object:  Table [dbo].[ChoiceTable]    Script Date: 2021/11/16 18:53:58 ******/
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
/****** Object:  Table [dbo].[QAInfo]    Script Date: 2021/11/16 18:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QAInfo](
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
/****** Object:  Table [dbo].[UserInfo]    Script Date: 2021/11/16 18:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserInfo](
	[Account] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_UserInfo] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[RespondentInfo] ADD  CONSTRAINT [DF_RespondentInfo_RespondentID]  DEFAULT (newid()) FOR [RespondentID]
GO
ALTER TABLE [dbo].[UserInfo] ADD  CONSTRAINT [DF_UserInfo_UserID]  DEFAULT (newid()) FOR [UserID]
GO
ALTER TABLE [dbo].[QA_Question]  WITH CHECK ADD  CONSTRAINT [FK_QA_Question_QAInfo] FOREIGN KEY([QAID])
REFERENCES [dbo].[QAInfo] ([QAID])
GO
ALTER TABLE [dbo].[QA_Question] CHECK CONSTRAINT [FK_QA_Question_QAInfo]
GO
ALTER TABLE [dbo].[QA_Question]  WITH CHECK ADD  CONSTRAINT [FK_QA_Question_QuestionsTable] FOREIGN KEY([QuestionID])
REFERENCES [dbo].[QuestionsTable] ([QuestionID])
GO
ALTER TABLE [dbo].[QA_Question] CHECK CONSTRAINT [FK_QA_Question_QuestionsTable]
GO
ALTER TABLE [dbo].[Respondent_answer]  WITH CHECK ADD  CONSTRAINT [FK_Respondent_answer_ChoiceTable] FOREIGN KEY([RespondentID])
REFERENCES [dbo].[RespondentInfo] ([RespondentID])
GO
ALTER TABLE [dbo].[Respondent_answer] CHECK CONSTRAINT [FK_Respondent_answer_ChoiceTable]
GO
ALTER TABLE [dbo].[Respondent_answer]  WITH CHECK ADD  CONSTRAINT [FK_Respondent_answer_QAInfo] FOREIGN KEY([QAID])
REFERENCES [dbo].[QAInfo] ([QAID])
GO
ALTER TABLE [dbo].[Respondent_answer] CHECK CONSTRAINT [FK_Respondent_answer_QAInfo]
GO
ALTER TABLE [dbo].[Respondent_answer]  WITH CHECK ADD  CONSTRAINT [FK_Respondent_answer_ResponseTable] FOREIGN KEY([RID])
REFERENCES [dbo].[ResponseTable] ([RID])
GO
ALTER TABLE [dbo].[Respondent_answer] CHECK CONSTRAINT [FK_Respondent_answer_ResponseTable]
GO
ALTER TABLE [dbo].[ResponseTable]  WITH CHECK ADD  CONSTRAINT [FK_ResponseTable_QAInfo] FOREIGN KEY([QAID])
REFERENCES [dbo].[QAInfo] ([QAID])
GO
ALTER TABLE [dbo].[ResponseTable] CHECK CONSTRAINT [FK_ResponseTable_QAInfo]
GO
ALTER TABLE [dbo].[ResponseTable]  WITH CHECK ADD  CONSTRAINT [FK_ResponseTable_RespondentInfo] FOREIGN KEY([RespondentID])
REFERENCES [dbo].[RespondentInfo] ([RespondentID])
GO
ALTER TABLE [dbo].[ResponseTable] CHECK CONSTRAINT [FK_ResponseTable_RespondentInfo]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Respondent_answer"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 210
               Right = 242
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ResponseTable"
            Begin Extent = 
               Top = 109
               Left = 340
               Bottom = 267
               Right = 534
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "RespondentInfo"
            Begin Extent = 
               Top = 7
               Left = 628
               Bottom = 165
               Right = 822
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 10
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'All_Answer_View'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'All_Answer_View'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "RespondentInfo"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 165
               Right = 242
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ResponseTable"
            Begin Extent = 
               Top = 7
               Left = 290
               Bottom = 165
               Right = 484
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 1716
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'CSVOutput_View'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'CSVOutput_View'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "QA_Question"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 144
               Right = 239
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "QuestionsTable"
            Begin Extent = 
               Top = 7
               Left = 287
               Bottom = 165
               Right = 510
            End
            DisplayFlags = 280
            TopColumn = 1
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 13
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1176
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1350
         Filter = 1356
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'QA_Question_View'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'QA_Question_View'
GO
USE [master]
GO
ALTER DATABASE [SimpleQuestionnaire] SET  READ_WRITE 
GO
