USE [master]
GO
/****** Object:  Database [RestAPIToDoList]    Script Date: 12/14/2021 7:41:42 AM ******/
CREATE DATABASE [RestAPIToDoList]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'RestAPIToDoList', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\RestAPIToDoList.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'RestAPIToDoList_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\RestAPIToDoList_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [RestAPIToDoList] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RestAPIToDoList].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [RestAPIToDoList] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [RestAPIToDoList] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [RestAPIToDoList] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [RestAPIToDoList] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [RestAPIToDoList] SET ARITHABORT OFF 
GO
ALTER DATABASE [RestAPIToDoList] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [RestAPIToDoList] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [RestAPIToDoList] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [RestAPIToDoList] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [RestAPIToDoList] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [RestAPIToDoList] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [RestAPIToDoList] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [RestAPIToDoList] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [RestAPIToDoList] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [RestAPIToDoList] SET  DISABLE_BROKER 
GO
ALTER DATABASE [RestAPIToDoList] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [RestAPIToDoList] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [RestAPIToDoList] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [RestAPIToDoList] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [RestAPIToDoList] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [RestAPIToDoList] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [RestAPIToDoList] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [RestAPIToDoList] SET RECOVERY FULL 
GO
ALTER DATABASE [RestAPIToDoList] SET  MULTI_USER 
GO
ALTER DATABASE [RestAPIToDoList] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [RestAPIToDoList] SET DB_CHAINING OFF 
GO
ALTER DATABASE [RestAPIToDoList] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [RestAPIToDoList] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [RestAPIToDoList] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [RestAPIToDoList] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'RestAPIToDoList', N'ON'
GO
ALTER DATABASE [RestAPIToDoList] SET QUERY_STORE = OFF
GO
USE [RestAPIToDoList]
GO
/****** Object:  User [root]    Script Date: 12/14/2021 7:41:42 AM ******/
CREATE USER [root] FOR LOGIN [root] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[Account_Todo]    Script Date: 12/14/2021 7:41:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account_Todo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccountId] [int] NOT NULL,
	[TodoId] [int] NOT NULL,
 CONSTRAINT [PK_Assignees] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 12/14/2021 7:41:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](100) NOT NULL,
	[Email] [nchar](30) NOT NULL,
	[Password] [nchar](100) NOT NULL,
	[Phone] [nchar](10) NOT NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_Accounts] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Groups]    Script Date: 12/14/2021 7:41:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Groups](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
	[Description] [nvarchar](50) NULL,
	[AccountId] [int] NOT NULL,
 CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Status]    Script Date: 12/14/2021 7:41:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Status](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Todos]    Script Date: 12/14/2021 7:41:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Todos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StatusId] [int] NOT NULL,
	[CreateAt] [date] NOT NULL,
	[UpdateAt] [date] NULL,
	[Name] [nvarchar](255) NOT NULL,
	[EndDate] [date] NOT NULL,
	[GroupId] [int] NOT NULL,
 CONSTRAINT [PK_Todos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Todos] ADD  CONSTRAINT [DF_Todos_isCompleted]  DEFAULT ((0)) FOR [StatusId]
GO
ALTER TABLE [dbo].[Todos] ADD  CONSTRAINT [DF_Todos_CreateAt]  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[Todos] ADD  CONSTRAINT [DF_Todos_UpdateAt]  DEFAULT (NULL) FOR [UpdateAt]
GO
ALTER TABLE [dbo].[Todos] ADD  CONSTRAINT [DF_Todos_SoftDelete]  DEFAULT ((0)) FOR [Name]
GO
ALTER TABLE [dbo].[Account_Todo]  WITH CHECK ADD  CONSTRAINT [FK_Assignees_Accounts] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Accounts] ([Id])
GO
ALTER TABLE [dbo].[Account_Todo] CHECK CONSTRAINT [FK_Assignees_Accounts]
GO
ALTER TABLE [dbo].[Account_Todo]  WITH CHECK ADD  CONSTRAINT [FK_Assignees_Todos] FOREIGN KEY([TodoId])
REFERENCES [dbo].[Todos] ([Id])
GO
ALTER TABLE [dbo].[Account_Todo] CHECK CONSTRAINT [FK_Assignees_Todos]
GO
ALTER TABLE [dbo].[Groups]  WITH CHECK ADD  CONSTRAINT [FK_Groups_Accounts] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Accounts] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Groups] CHECK CONSTRAINT [FK_Groups_Accounts]
GO
ALTER TABLE [dbo].[Todos]  WITH CHECK ADD  CONSTRAINT [FK_Todos_Groups1] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Groups] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Todos] CHECK CONSTRAINT [FK_Todos_Groups1]
GO
ALTER TABLE [dbo].[Todos]  WITH CHECK ADD  CONSTRAINT [FK_Todos_Status] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Status] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Todos] CHECK CONSTRAINT [FK_Todos_Status]
GO
/****** Object:  StoredProcedure [dbo].[Add_AccountTodo]    Script Date: 12/14/2021 7:41:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Add_AccountTodo]
	-- Add the parameters for the stored procedure here
	@accountId int,
	@todoId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [dbo].[Account_Todo]
           ([AccountId]
           ,[TodoId])
     VALUES
           (@accountId, @todoId)
END
GO
/****** Object:  StoredProcedure [dbo].[Create_Account]    Script Date: 12/14/2021 7:41:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Hoang
-- Create date: 10/12/2021
-- Description:	Create new account
-- =============================================
CREATE PROCEDURE [dbo].[Create_Account]
	-- Add the parameters for the stored procedure here
	@id int output,
	@fullName nvarchar(100),
	@email nchar(30),
	@password nchar(100),
	@phone nchar(10)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [dbo].[Accounts]
           ([FullName]
           ,[Email]
           ,[Password]
           ,[Phone])
     VALUES
           (@fullName, @email, @password, @phone)
		   SET @id = SCOPE_IDENTITY()
END
GO
/****** Object:  StoredProcedure [dbo].[Create_Group_ByAccountId]    Script Date: 12/14/2021 7:41:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Create_Group_ByAccountId]
	-- Add the parameters for the stored procedure here
	@accountId int,
	@name nvarchar(20),
	@desc nvarchar(50),
	@id int output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [dbo].[Groups]
           ([Name]
           ,[Description]
           ,[AccountId])
     VALUES
           (@name, @desc, @accountId)
	 SET @id = SCOPE_IDENTITY()
END
GO
/****** Object:  StoredProcedure [dbo].[Create_NewTodo]    Script Date: 12/14/2021 7:41:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Create_NewTodo]
	-- Add the parameters for the stored procedure here
	@content nvarchar(max),
	@id int output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO Todos(Content) values (@content)
	set @id = SCOPE_IDENTITY()
END
GO
/****** Object:  StoredProcedure [dbo].[Create_Todo_ByGroupId]    Script Date: 12/14/2021 7:41:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Create_Todo_ByGroupId]
	-- Add the parameters for the stored procedure here
	@name nvarchar(25),
	@endDate date,
	@statusId int,
	@groupId int,
	@id int output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [dbo].[Todos]
           ([StatusId]
           ,[CreateAt]
  
           ,[Name]
           ,[EndDate]
           ,[GroupId])
     VALUES
           (@statusId, GETDATE(),@name, @endDate, @groupId)
		   SET @id = SCOPE_IDENTITY()
END
GO
/****** Object:  StoredProcedure [dbo].[Delete_Account_ByEmail]    Script Date: 12/14/2021 7:41:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Delete_Account_ByEmail]
	-- Add the parameters for the stored procedure here
	@email nchar(30)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DELETE FROM [dbo].[Accounts]
      WHERE Email = @email
END
GO
/****** Object:  StoredProcedure [dbo].[Delete_By_Id]    Script Date: 12/14/2021 7:41:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Delete_By_Id]
	@id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DELETE FROM Todos
      WHERE Id = @id

END
GO
/****** Object:  StoredProcedure [dbo].[Get_Account_ByEmail]    Script Date: 12/14/2021 7:41:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Hoang
-- Create date: 10/12/2021
-- Description:	Lay account tu email
-- =============================================
CREATE PROCEDURE [dbo].[Get_Account_ByEmail]
	-- Add the parameters for the stored procedure here
	@email varchar(30)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [Id]
      ,[FullName]
      ,[Email]
      ,[Password]
      ,[Phone]
  FROM [dbo].[Accounts] WHERE [Email] = @email
END
GO
/****** Object:  StoredProcedure [dbo].[Get_Account_ByEmailAndPassword]    Script Date: 12/14/2021 7:41:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Get_Account_ByEmailAndPassword]
	-- Add the parameters for the stored procedure here
	@email nchar(30),
	@password nchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [Id]
      ,[FullName]
      ,[Email]
      ,[Password]
      ,[Phone]
  FROM [dbo].[Accounts] WHERE [Email] = @email AND [Password] = @password
END
GO
/****** Object:  StoredProcedure [dbo].[Get_Account_ByGroupId]    Script Date: 12/14/2021 7:41:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Get_Account_ByGroupId]
	-- Add the parameters for the stored procedure here
	@groupId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [Id]
      ,[Name]
      ,[Description]
      ,[AccountId]
  FROM [dbo].[Groups] WHERE [Id] = @groupId
END
GO
/****** Object:  StoredProcedure [dbo].[Get_Accounts]    Script Date: 12/14/2021 7:41:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Hoang
-- Create date: 10/12/2021
-- Description:	Lay toan bo danh sach nguoi dung
-- =============================================
CREATE PROCEDURE [dbo].[Get_Accounts]
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM Accounts
END
GO
/****** Object:  StoredProcedure [dbo].[Get_All_Todo]    Script Date: 12/14/2021 7:41:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Get_All_Todo]
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM Todos WHERE Todos.SoftDelete = 0
END
GO
/****** Object:  StoredProcedure [dbo].[Get_Groups_ByAccountId]    Script Date: 12/14/2021 7:41:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Get_Groups_ByAccountId]
	-- Add the parameters for the stored procedure here
	@accountId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [Id]
      ,[Name]
      ,[Description]
	  , [AccountId]
  FROM [dbo].[Groups]
  WHERE AccountId = @accountId
END
GO
/****** Object:  StoredProcedure [dbo].[Get_Status]    Script Date: 12/14/2021 7:41:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Get_Status] 
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM Status
END
GO
/****** Object:  StoredProcedure [dbo].[Get_Todo_By_Id]    Script Date: 12/14/2021 7:41:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Get_Todo_By_Id]
	-- Add the parameters for the stored procedure here
	@id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM Todos WHERE ID = @id
END
GO
/****** Object:  StoredProcedure [dbo].[Get_Todo_ByGroupId]    Script Date: 12/14/2021 7:41:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Get_Todo_ByGroupId]
	-- Add the parameters for the stored procedure here
	@groupId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Todos.[Id] as id
      ,
	  Status.[Name] as trangThai
      ,Todos.[Name] as todoName
      ,[EndDate] as ngayKetThuc
      ,
	  FullName as tenNguoiThamDu
  FROM Todos
  INNER JOIN [RestAPIToDoList].[dbo].Account_Todo 
  ON Todos.Id = Account_Todo.TodoId 
  INNER JOIN Accounts
  ON Account_Todo.AccountId = Accounts.Id
  INNER JOIN Status
  ON Todos.StatusId = Status.Id
  WHERE GroupId = @groupId
END
GO
/****** Object:  StoredProcedure [dbo].[Update_Account_ByEmail]    Script Date: 12/14/2021 7:41:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Update_Account_ByEmail]
	@fullName nvarchar(100),
	@email nchar(30),
	@password nchar(100),
	@phone nchar(10)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [dbo].[Accounts]
   SET [FullName] = @fullName
      ,[Email] = @email
      ,[Password] = @password
      ,[Phone] = @phone
 WHERE Email = @email
END
GO
/****** Object:  StoredProcedure [dbo].[Update_Todo_ById]    Script Date: 12/14/2021 7:41:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Update_Todo_ById]
	@isCompleted bit,
	@id int
	
AS
BEGIN

	SET NOCOUNT ON;

	UPDATE Todos
   SET isCompleted = @isCompleted,
	   UpdateAt =  GETDATE()
WHERE Id = @id
END
GO
USE [master]
GO
ALTER DATABASE [RestAPIToDoList] SET  READ_WRITE 
GO
