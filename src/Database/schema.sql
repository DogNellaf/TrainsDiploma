CREATE TABLE [dbo].[Role](
  [Id] [int] IDENTITY(1,1) NOT NULL,
  [Name] [nchar](20) NOT NULL,
  [IsWorker] [bit] NOT NULL,
  [IsAdmin] [bit] NOT NULL,
  CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[User](
  [Id] [int] IDENTITY(1,1) NOT NULL,
  [Login] [nchar](50) NOT NULL,
  [Token] [ntext] NULL,
  [Balance] [float] NOT NULL,
  [RoleId] [int] NOT NULL,
  [PassportSeries] [nchar](4) NOT NULL,
  [PassportNumber] [nchar](6) NOT NULL,
  CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC),
  CONSTRAINT [DF_User_Balance]  DEFAULT ((0)) FOR [Balance],
  CONSTRAINT [FK_User_Role] FOREIGN KEY([RoleId])
);

CREATE TABLE [dbo].[Transaction](
  [Id] [int] IDENTITY(1,1) NOT NULL,
  [UserId] [int] NOT NULL,
  [Value] [float] NOT NULL,
  [IsComplited] [bit] NOT NULL,
  [PaymentTime] [datetime] NOT NULL,
  [PaymentType] [nchar](20) NOT NULL,
  [Comment] [ntext] NOT NULL,
  CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED ([Id] ASC),
  CONSTRAINT [DF_Transaction_IsComplited]  DEFAULT ((0)) FOR [IsComplited],
  CONSTRAINT [FK_Transaction_User] FOREIGN KEY([UserId])
);

CREATE TABLE [dbo].[City](
  [Id] [int] IDENTITY(1,1) NOT NULL,
  [Name] [nchar](100) NOT NULL,
  CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Status](
  [Id] [int] IDENTITY(1,1) NOT NULL,
  [Name] [nchar](50) NOT NULL,
  CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Route](
  [Id] [int] IDENTITY(1,1) NOT NULL,
  [DepartureTime] [datetime] NOT NULL,
  [DepartureCityId] [int] NOT NULL,
  [Duration] [int] NOT NULL,
  [ArrivalCityId] [int] NOT NULL,
  [Price] [float] NOT NULL,
  CONSTRAINT [PK_Route] PRIMARY KEY CLUSTERED ([Id] ASC),
  CONSTRAINT [FK_Route_City] FOREIGN KEY([ArrivalCityId]),
);

CREATE TABLE [dbo].[Ticket](
  [Id] [int] IDENTITY(1,1) NOT NULL,
  [BuyTime] [datetime] NOT NULL,
  [Price] [float] NOT NULL,
  [RouteId] [int] NOT NULL,
  [StatusId] [int] NOT NULL,
  [UserId] [int] NOT NULL,
  CONSTRAINT [PK_Ticket] PRIMARY KEY CLUSTERED ([Id] ASC),
  CONSTRAINT [FK_Ticket_Route] FOREIGN KEY([RouteId]),
  CONSTRAINT [FK_Ticket_Status] FOREIGN KEY([StatusId]),
  CONSTRAINT [FK_Ticket_User] FOREIGN KEY([UserId])
);