USE [GoodbyeFields-QA]
GO
/****** Object:  Table [dbo].[PaymentHistory]    Script Date: 9/11/2020 11:24:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentHistory](
	[PaymentHistoryId] [bigint] IDENTITY(1,1) NOT NULL,
	[Id] [nvarchar](250) NULL,
	[CreateTime] [datetime] NULL,
	[UpdateTime] [datetime] NULL,
	[Intent] [nvarchar](250) NULL,
	[PaymentMethod] [nvarchar](250) NULL,
	[PayerId] [nvarchar](250) NULL,
	[Amount] [money] NULL,
	[Currency] [nvarchar](250) NULL,
	[Description] [nvarchar](250) NULL,
	[Status] [nvarchar](250) NULL,
	[InvoiceNumber] [nvarchar](250) NULL,
	[FullResponse] [nvarchar](max) NULL,
	[PlayerId] [nvarchar](250) NULL,
 CONSTRAINT [PK_PaymentHistory] PRIMARY KEY CLUSTERED 
(
	[PaymentHistoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Player]    Script Date: 9/11/2020 11:24:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Player](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[PlayerId] [nvarchar](250) NULL,
	[Name] [nvarchar](250) NULL,
	[Address1] [nvarchar](250) NULL,
	[Email] [nvarchar](250) NULL,
	[Password] [nvarchar](250) NULL,
	[Username] [nvarchar](50) NULL,
 CONSTRAINT [PK_tblPlayer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transaction]    Script Date: 9/11/2020 11:24:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transaction](
	[TransactionId] [bigint] IDENTITY(1,1) NOT NULL,
	[TransactionTypeId] [int] NOT NULL,
	[TransactionAmount] [money] NOT NULL,
	[PlayerId] [nvarchar](500) NOT NULL,
	[TransactionDescription] [varchar](500) NULL,
	[TransactionCode] [varchar](50) NOT NULL,
	[TransactionStatus] [varchar](50) NULL,
	[TransactionTime] [datetime] NOT NULL,
	[IsVoid] [bit] NULL,
	[VoidDate] [datetime] NULL,
	[VoidDescription] [nvarchar](250) NULL,
	[PayerId] [nvarchar](250) NULL,
 CONSTRAINT [PK_tblTransaction] PRIMARY KEY CLUSTERED 
(
	[TransactionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TransactionType]    Script Date: 9/11/2020 11:24:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransactionType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TransactionTypeName] [nvarchar](50) NULL,
	[TransactionTypeDescription] [nvarchar](50) NULL,
 CONSTRAINT [PK_tblTransactionType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK__tblTransa__Trans__4D94879B] FOREIGN KEY([TransactionTypeId])
REFERENCES [dbo].[TransactionType] ([Id])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK__tblTransa__Trans__4D94879B]
GO
/****** Object:  StoredProcedure [dbo].[USP_CheckPlayerExistence]    Script Date: 9/11/2020 11:24:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_CheckPlayerExistence]
	@PlayerId nvarchar(500)
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @playerCount int=0;
	BEGIN TRY
		SET @playerCount = ISNULL((SELECT COUNT(*) FROM [Transaction] WHERE PlayerId=@PlayerId),0);
		SELECT @playerCount AS playerCount;
	END TRY
	BEGIN CATCH
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[USP_GetBalance]    Script Date: 9/11/2020 11:24:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GetBalance] 
	@PlayerId nvarchar(500)
AS
BEGIN
SET NOCOUNT ON
BEGIN TRY
	DECLARE  @TotalDr decimal(18,2)=0.0;
	DECLARE  @TotalCr decimal(18,2)=0.0;
	DECLARE  @Balance decimal(18,2)=0.0;

	SET @TotalDr = isnull((SELECT sum(TransactionAmount) FROM [Transaction] WHERE PlayerId=@PlayerId AND IsVoid=0 AND TransactionTypeId=2),0.0);
	SET @TotalCr = isnull((SELECT sum(TransactionAmount) FROM [Transaction] WHERE PlayerId=@PlayerId AND IsVoid=0 AND TransactionTypeId=1),0.0);
	
	SET @Balance=@TotalCr-@TotalDr;
	SELECT @Balance AS PlayerBalance;

	END TRY
	BEGIN CATCH
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[USP_GetPlayerList]    Script Date: 9/11/2020 11:24:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GetPlayerList]
AS
BEGIN
	SET NOCOUNT ON
	BEGIN TRY
		SELECT 
		PlayerId,
		[Name],
		Address1,
		Email,
		[Password],
		Username
		FROM Player 		
	END TRY
	BEGIN CATCH
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[USP_GetPlayerTransactionHistory]    Script Date: 9/11/2020 11:24:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GetPlayerTransactionHistory]
	@PlayerId nvarchar(500),
	@TransactionFromDate datetime,
	@TransactionToDate datetime
AS
BEGIN
	SET NOCOUNT ON
	BEGIN TRY
		SELECT 
		TransactionId	,
		TransactionTypeId	,
		TransactionAmount	,
		PlayerId	,
		TransactionDescription	,
		TransactionCode	,
		TransactionStatus	,
		TransactionTime	,
		IsVoid,
		VoidDescription,
		VoidDate
		FROM [Transaction] 
		WHERE 
		PlayerId=@PlayerId AND 
		CONVERT(DATE, TransactionTime)>=CONVERT(DATE, @TransactionFromDate)  AND 
		CONVERT(DATE, TransactionTime)<=CONVERT(DATE, @TransactionToDate) ORDER BY TransactionId DESC;
	END TRY
	BEGIN CATCH
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[USP_SavePaypalResponse]    Script Date: 9/11/2020 11:24:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_SavePaypalResponse]
	@Id	nvarchar(250),
	@CreateTime	datetime,
	@UpdateTime	datetime,
	@Intent	nvarchar(250),
	@PaymentMethod	nvarchar(250),
	@PayerId	nvarchar(250),
	@Amount	decimal(18,2),
	@Currency	nvarchar(250),
	@Description	nvarchar(250),
	@Status	nvarchar(250),
	@InvoiceNumber nvarchar(250),
	@FullResponse nvarchar(max)
AS
BEGIN
SET NOCOUNT ON
	BEGIN TRY
		BEGIN TRAN
	
			INSERT PaymentHistory(Id,
			CreateTime,
			UpdateTime,
			Intent,
			PaymentMethod,
			PayerId,
			Amount,
			Currency,
			[Description],
			[Status],
			InvoiceNumber,
			FullResponse) 
			VALUES(@Id,
			@CreateTime,
			@UpdateTime,
			@Intent,
			@PaymentMethod,
			@PayerId,
			@Amount,
			@Currency,
			@Description,
			@Status,
			@InvoiceNumber,
			@FullResponse);

		COMMIT TRAN
	
	END TRY
		BEGIN CATCH
			ROLLBACK TRAN
		END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[USP_VoidATransaction]    Script Date: 9/11/2020 11:24:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_VoidATransaction]
	@TransactionId	bigint,
	@IsVoid bit,
	@VoidDescription	nvarchar(250)	
AS
BEGIN
SET NOCOUNT ON	
	BEGIN TRY
		BEGIN TRAN
		DECLARE @Message nvarchar(250);
			DECLARE @TRANS int = ISNULL((SELECT COUNT(*) FROM [Transaction] WHERE TransactionId=@TransactionId),0);
			IF(@TRANS!=0)
			BEGIN
				DECLARE @transactionType int=(SELECT TransactionTypeId FROM [Transaction] WHERE TransactionId=@TransactionId);
				
				--check balance
				DECLARE  @TotalDr decimal(18,2)=0.0;
				DECLARE  @TotalCr decimal(18,2)=0.0;
				DECLARE  @Balance decimal(18,2)=0.0;

				
				DECLARE @VoidBalance decimal(18,2)=(SELECT TransactionAmount FROM [Transaction] WHERE TransactionId=@TransactionId);
				DECLARE @PlayerId nvarchar(500)=(SELECT PlayerId FROM [Transaction] WHERE TransactionId=@TransactionId);

				SET @TotalDr = isnull((SELECT sum(TransactionAmount) FROM [Transaction] WHERE PlayerId=@PlayerId AND IsVoid=0 AND TransactionTypeId=2),0.0);
				SET @TotalCr = isnull((SELECT sum(TransactionAmount) FROM [Transaction] WHERE PlayerId=@PlayerId AND IsVoid=0 AND TransactionTypeId=1),0.0);
	
				SET @Balance=@TotalCr-@TotalDr;

				IF(@transactionType=2 or @Balance >= @VoidBalance)
				begin
					DECLARE @sts bit=(SELECT IsVoid FROM [Transaction] WHERE TransactionId=@TransactionId);
					if(@sts='false')
					begin
					UPDATE [Transaction] SET IsVoid=@IsVoid, VoidDate=GETDATE(), VoidDescription=@VoidDescription WHERE TransactionId=@TransactionId;
					SET @Message= 'Transaction void successfully';
					end
				
					else
					begin
						SET @Message= 'Transaction already void';
					end
				end

				else
				begin
					SET @Message= 'You can not void this transaction';
				end
			END
			ELSE
			BEGIN
				SET @Message='Transaction not found';
			END
		SELECT @Message AS Message;
		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[USP_VoidTransactionStatus]    Script Date: 9/11/2020 11:24:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_VoidTransactionStatus]
	@TransactionId bigint
AS
BEGIN
	SET NOCOUNT ON
	BEGIN TRY
		SELECT 
		TransactionId	,
		TransactionTypeId	,
		TransactionAmount	,
		PlayerId	,
		TransactionDescription	,
		TransactionCode	,
		TransactionStatus	,
		TransactionTime	,
		IsVoid,
		VoidDescription,
		VoidDate
		FROM [Transaction] 
		WHERE TransactionId=TransactionId;
	END TRY
	BEGIN CATCH
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[USP_WalletDeposit]    Script Date: 9/11/2020 11:24:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_WalletDeposit]
	@TransactionAmount	decimal(18, 2)	,
	@PlayerId nvarchar(500),	
	@TransactionDescription	varchar(500),	
	@TransactionCode	varchar(50),
	@TransactionType int,
	@IsVoid bit,
	@TransactionStatus varchar(50)
AS
BEGIN
SET NOCOUNT ON
	BEGIN TRY
		BEGIN TRAN
	
			INSERT [Transaction](TransactionTypeId,
			TransactionAmount,
			PlayerId,
			TransactionDescription,
			TransactionCode,
			TransactionTime,
			IsVoid,
			TransactionStatus,
			VoidDate,
			VoidDescription) 
			VALUES(@TransactionType,
			@TransactionAmount,
			@PlayerId,
			@TransactionDescription,
			@TransactionCode,
			GETDATE(),
			@IsVoid,
			@TransactionStatus,
			null,
			'');

		COMMIT TRAN
	    UPDATE PaymentHistory set PlayerId=trim(@PlayerId) where Id=(@TransactionCode)
	END TRY
		BEGIN CATCH
			ROLLBACK TRAN
		END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[USP_WalletWithdrawal]    Script Date: 9/11/2020 11:24:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_WalletWithdrawal]
	@TransactionAmount	decimal(18, 2)	,
	@PlayerId nvarchar(500),	
	@TransactionDescription	varchar(500),	
	@TransactionCode	varchar(50),
	@TransactionType int,
	@IsVoid bit,
	@TransactionStatus varchar(50)
AS
BEGIN
SET NOCOUNT ON
	BEGIN TRY
		BEGIN TRAN
	
			INSERT [Transaction](TransactionTypeId,
			TransactionAmount,
			PlayerId,
			TransactionDescription,
			TransactionCode,
			TransactionTime,
			IsVoid,
			TransactionStatus,
			VoidDate,
			VoidDescription) 
			VALUES(@TransactionType,
			@TransactionAmount,
			@PlayerId,
			@TransactionDescription,
			@TransactionCode,
			GETDATE(),
			@IsVoid,
			@TransactionStatus,
			null,
			'');

		COMMIT TRAN
	
	END TRY
		BEGIN CATCH
			ROLLBACK TRAN
		END CATCH
END
GO
