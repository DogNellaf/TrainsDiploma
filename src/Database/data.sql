USE [TrainsDiploma]
GO
SET IDENTITY_INSERT [dbo].[City] ON 
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (1, N'Москва                                                                                              ')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (2, N'Санкт-Петербург                                                                                     ')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (3, N'Оренбург                                                                                            ')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (5, N'Казань                                                                                              ')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (6, N'Москва                                                                                              ')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (7, N'Самара                                                                                              ')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (8, N'Уфа                                                                                                 ')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (13, N'Екатеринбург                                                                                        ')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (14, N'Челябинск                                                                                           ')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (15, N'Сочи                                                                                                ')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (16, N'Краснодар                                                                                           ')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (17, N'Воронеж                                                                                             ')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (18, N'Тула                                                                                                ')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (19, N'Адлер                                                                                               ')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (20, N'Смоленск                                                                                            ')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (21, N'Астрахань                                                                                           ')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (22, N'Иркутск                                                                                             ')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (23, N'Волгоград                                                                                           ')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (24, N'Пенза                                                                                               ')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (25, N'Нижний Новгород                                                                                     ')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (26, N'Красноярск                                                                                          ')
GO
INSERT [dbo].[City] ([Id], [Name]) VALUES (27, N'Омск                                                                                                ')
GO
SET IDENTITY_INSERT [dbo].[City] OFF
GO
SET IDENTITY_INSERT [dbo].[Route] ON 
GO
INSERT [dbo].[Route] ([Id], [DepartureTime], [DepartureCityId], [Duration], [ArrivalCityId], [Price]) VALUES (2, CAST(N'2023-06-14T00:00:00.000' AS DateTime), 1, 900, 2, 1000)
GO
INSERT [dbo].[Route] ([Id], [DepartureTime], [DepartureCityId], [Duration], [ArrivalCityId], [Price]) VALUES (4, CAST(N'2023-06-14T00:00:00.000' AS DateTime), 1, 1080, 3, 1000)
GO
INSERT [dbo].[Route] ([Id], [DepartureTime], [DepartureCityId], [Duration], [ArrivalCityId], [Price]) VALUES (5, CAST(N'2023-06-14T11:26:00.000' AS DateTime), 1, 600, 2, 2321)
GO
INSERT [dbo].[Route] ([Id], [DepartureTime], [DepartureCityId], [Duration], [ArrivalCityId], [Price]) VALUES (6, CAST(N'2023-06-14T03:00:00.000' AS DateTime), 1, 1740, 3, 4341)
GO
INSERT [dbo].[Route] ([Id], [DepartureTime], [DepartureCityId], [Duration], [ArrivalCityId], [Price]) VALUES (7, CAST(N'2023-06-15T07:14:00.000' AS DateTime), 27, 1440, 8, 3518)
GO
INSERT [dbo].[Route] ([Id], [DepartureTime], [DepartureCityId], [Duration], [ArrivalCityId], [Price]) VALUES (8, CAST(N'2023-06-15T11:28:00.000' AS DateTime), 5, 660, 1, 2476)
GO
INSERT [dbo].[Route] ([Id], [DepartureTime], [DepartureCityId], [Duration], [ArrivalCityId], [Price]) VALUES (9, CAST(N'2023-06-16T10:00:00.000' AS DateTime), 3, 420, 7, 2738)
GO
INSERT [dbo].[Route] ([Id], [DepartureTime], [DepartureCityId], [Duration], [ArrivalCityId], [Price]) VALUES (10, CAST(N'2023-06-16T02:50:00.000' AS DateTime), 1, 360, 25, 4219)
GO
INSERT [dbo].[Route] ([Id], [DepartureTime], [DepartureCityId], [Duration], [ArrivalCityId], [Price]) VALUES (11, CAST(N'2023-06-17T07:47:00.000' AS DateTime), 15, 420, 16, 1614)
GO
INSERT [dbo].[Route] ([Id], [DepartureTime], [DepartureCityId], [Duration], [ArrivalCityId], [Price]) VALUES (12, CAST(N'2023-06-17T09:15:00.000' AS DateTime), 3, 2760, 15, 5042)
GO
INSERT [dbo].[Route] ([Id], [DepartureTime], [DepartureCityId], [Duration], [ArrivalCityId], [Price]) VALUES (13, CAST(N'2023-06-18T03:37:00.000' AS DateTime), 1, 780, 17, 3078)
GO
INSERT [dbo].[Route] ([Id], [DepartureTime], [DepartureCityId], [Duration], [ArrivalCityId], [Price]) VALUES (14, CAST(N'2023-06-18T06:40:00.000' AS DateTime), 1, 120, 18, 1424)
GO
INSERT [dbo].[Route] ([Id], [DepartureTime], [DepartureCityId], [Duration], [ArrivalCityId], [Price]) VALUES (15, CAST(N'2023-06-19T09:15:00.000' AS DateTime), 3, 2760, 19, 5042)
GO
INSERT [dbo].[Route] ([Id], [DepartureTime], [DepartureCityId], [Duration], [ArrivalCityId], [Price]) VALUES (16, CAST(N'2023-06-19T04:30:00.000' AS DateTime), 14, 840, 3, 2685)
GO
INSERT [dbo].[Route] ([Id], [DepartureTime], [DepartureCityId], [Duration], [ArrivalCityId], [Price]) VALUES (17, CAST(N'2023-06-20T08:57:00.000' AS DateTime), 1, 300, 20, 2670)
GO
INSERT [dbo].[Route] ([Id], [DepartureTime], [DepartureCityId], [Duration], [ArrivalCityId], [Price]) VALUES (18, CAST(N'2023-06-20T04:07:00.000' AS DateTime), 13, 1560, 1, 4351)
GO
INSERT [dbo].[Route] ([Id], [DepartureTime], [DepartureCityId], [Duration], [ArrivalCityId], [Price]) VALUES (19, CAST(N'2023-06-21T04:00:00.000' AS DateTime), 13, 1260, 3, 3310)
GO
INSERT [dbo].[Route] ([Id], [DepartureTime], [DepartureCityId], [Duration], [ArrivalCityId], [Price]) VALUES (21, CAST(N'2023-06-21T04:26:00.000' AS DateTime), 8, 1740, 21, 4351)
GO
INSERT [dbo].[Route] ([Id], [DepartureTime], [DepartureCityId], [Duration], [ArrivalCityId], [Price]) VALUES (22, CAST(N'2023-06-22T08:41:00.000' AS DateTime), 26, 1260, 22, 3102)
GO
INSERT [dbo].[Route] ([Id], [DepartureTime], [DepartureCityId], [Duration], [ArrivalCityId], [Price]) VALUES (23, CAST(N'2023-06-22T04:33:00.000' AS DateTime), 3, 1440, 23, 3426)
GO
INSERT [dbo].[Route] ([Id], [DepartureTime], [DepartureCityId], [Duration], [ArrivalCityId], [Price]) VALUES (24, CAST(N'2023-06-23T11:47:00.000' AS DateTime), 7, 420, 24, 1785)
GO
INSERT [dbo].[Route] ([Id], [DepartureTime], [DepartureCityId], [Duration], [ArrivalCityId], [Price]) VALUES (25, CAST(N'2023-06-23T09:25:00.000' AS DateTime), 14, 2040, 1, 7369)
GO
INSERT [dbo].[Route] ([Id], [DepartureTime], [DepartureCityId], [Duration], [ArrivalCityId], [Price]) VALUES (26, CAST(N'2023-06-24T10:00:00.000' AS DateTime), 3, 1320, 1, 4923)
GO
INSERT [dbo].[Route] ([Id], [DepartureTime], [DepartureCityId], [Duration], [ArrivalCityId], [Price]) VALUES (27, CAST(N'2023-06-24T00:50:00.000' AS DateTime), 16, 780, 23, 2321)
GO
INSERT [dbo].[Route] ([Id], [DepartureTime], [DepartureCityId], [Duration], [ArrivalCityId], [Price]) VALUES (28, CAST(N'2023-06-10T00:00:00.000' AS DateTime), 3, 2100, 2, 3000)
GO
SET IDENTITY_INSERT [dbo].[Route] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 
GO
INSERT [dbo].[Role] ([Id], [Name], [IsWorker], [IsAdmin]) VALUES (1, N'Клиент              ', 0, 0)
GO
INSERT [dbo].[Role] ([Id], [Name], [IsWorker], [IsAdmin]) VALUES (2, N'Диспетчер           ', 1, 0)
GO
INSERT [dbo].[Role] ([Id], [Name], [IsWorker], [IsAdmin]) VALUES (3, N'Администратор       ', 1, 1)
GO
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 
GO
INSERT [dbo].[User] ([Id], [Login], [Token], [Balance], [RoleId], [PassportSeries], [PassportNumber]) VALUES (1, N'admin                                             ', N'8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918', 5000, 3, N'    ', N'      ')
GO
INSERT [dbo].[User] ([Id], [Login], [Token], [Balance], [RoleId], [PassportSeries], [PassportNumber]) VALUES (2, N'VadimP                                            ', N'760efa4788d2340090cacbfad37249d836ced2e3677ad15003c25dd251b65742', 5000, 1, N'5320', N'095486')
GO
INSERT [dbo].[User] ([Id], [Login], [Token], [Balance], [RoleId], [PassportSeries], [PassportNumber]) VALUES (3, N'Pasha0                                            ', N'c024c43d0c8726d05ce96f7769d0685f421594c3a0d5434d957d093c46b6d49f', 1482, 1, N'5467', N'678908')
GO
INSERT [dbo].[User] ([Id], [Login], [Token], [Balance], [RoleId], [PassportSeries], [PassportNumber]) VALUES (4, N'GlotovaM                                          ', N'1b7fd00eaf082bd72b68e824ac759ac98cd63803fb01e3c0b49d04f67971f07c', 1961, 1, N'7684', N'860576')
GO
INSERT [dbo].[User] ([Id], [Login], [Token], [Balance], [RoleId], [PassportSeries], [PassportNumber]) VALUES (5, N'MarinaA                                           ', N'24597489cadfd836a1c7bd0c6265cbe4db02b0a241f4d4492c553293b92c3945', 2601, 1, N'5415', N'080509')
GO
INSERT [dbo].[User] ([Id], [Login], [Token], [Balance], [RoleId], [PassportSeries], [PassportNumber]) VALUES (6, N'VeraV                                             ', N'0ee8e3c29ee01607966ae32bae4ce1b0b881f3f8b52316755bbf3e36ab984953', 9941, 1, N'6505', N'934567')
GO
INSERT [dbo].[User] ([Id], [Login], [Token], [Balance], [RoleId], [PassportSeries], [PassportNumber]) VALUES (7, N'TlegenovaT                                        ', N'976a23ed16dfe90f1b8b85c44bf5d93062daf73d66758b7484828fe5dddde1ad', 3958, 1, N'1456', N'678934')
GO
INSERT [dbo].[User] ([Id], [Login], [Token], [Balance], [RoleId], [PassportSeries], [PassportNumber]) VALUES (8, N'VolodyaV                                          ', N'b968870ca6dcddc82483053ff8e77f318d19a8870054cc84f2ee4712f4950a2f', 0, 1, N'    ', N'      ')
GO
INSERT [dbo].[User] ([Id], [Login], [Token], [Balance], [RoleId], [PassportSeries], [PassportNumber]) VALUES (9, N'AnreyA                                            ', N'db5ac115a419162fddbc1b4b2fa605c64e38638fa2d62ffe4de6bc92cd64de2b', 0, 1, N'    ', N'      ')
GO
INSERT [dbo].[User] ([Id], [Login], [Token], [Balance], [RoleId], [PassportSeries], [PassportNumber]) VALUES (10, N'ValeriaB                                          ', N'2cfccd42f3d13fdce65839f88c0560fcf0d0bc1af5f91ba852e46a04fc28d943', 0, 2, N'    ', N'      ')
GO
INSERT [dbo].[User] ([Id], [Login], [Token], [Balance], [RoleId], [PassportSeries], [PassportNumber]) VALUES (11, N'DmitryN                                           ', N'c87b76a82410fa3ea28216f2d6df6516be63fa4ae34ffd4b7220f87232c6f7a2', 0, 1, N'    ', N'      ')
GO
INSERT [dbo].[User] ([Id], [Login], [Token], [Balance], [RoleId], [PassportSeries], [PassportNumber]) VALUES (12, N'OlgaK                                             ', N'303c7e48d6685640e8dc6e5e38d9ba2d221cff30c2afa666143d11a46d099bec', 0, 1, N'    ', N'      ')
GO
INSERT [dbo].[User] ([Id], [Login], [Token], [Balance], [RoleId], [PassportSeries], [PassportNumber]) VALUES (13, N'IgorT                                             ', N'b0e2f4ee2cb2307ead8762cc76baaeeba29c8d84bc2308a1c206622ca78b88a5', 0, 1, N'    ', N'      ')
GO
INSERT [dbo].[User] ([Id], [Login], [Token], [Balance], [RoleId], [PassportSeries], [PassportNumber]) VALUES (14, N'MarkD                                             ', N'7e219ccb93e717054cc6bd162039e27e4e67aab90add6be1138ebe517a6c9bb5', 0, 1, N'    ', N'      ')
GO
INSERT [dbo].[User] ([Id], [Login], [Token], [Balance], [RoleId], [PassportSeries], [PassportNumber]) VALUES (15, N'GenaD                                             ', N'7b59bfdb6abd734ff77f991bc72f95f40e19523ff3750da7280c470874b3c41a', 0, 1, N'    ', N'      ')
GO
INSERT [dbo].[User] ([Id], [Login], [Token], [Balance], [RoleId], [PassportSeries], [PassportNumber]) VALUES (16, N'ArtemB                                            ', N'50fbfc02e8961ea91f9a691bbde962c1e78b1538aac3f00dab3a3d7bebc59e04', 0, 1, N'    ', N'      ')
GO
INSERT [dbo].[User] ([Id], [Login], [Token], [Balance], [RoleId], [PassportSeries], [PassportNumber]) VALUES (17, N'GlebM                                             ', N'6ee8ccfd6cd26a9be008f3244098a21170ad70ce3ba152bef794727d9ecdbed6', 0, 1, N'    ', N'      ')
GO
INSERT [dbo].[User] ([Id], [Login], [Token], [Balance], [RoleId], [PassportSeries], [PassportNumber]) VALUES (18, N'SanyaK                                            ', N'78c98e277561180f2fb94b50674fd58da8ef6182b65951d20b9be61625e5b307', 0, 1, N'    ', N'      ')
GO
INSERT [dbo].[User] ([Id], [Login], [Token], [Balance], [RoleId], [PassportSeries], [PassportNumber]) VALUES (19, N'MishaN                                            ', N'7c3aa2e2e68ea99636884c34368d95766c6c323c25329fd6db300ba23737ed84', 0, 1, N'    ', N'      ')
GO
INSERT [dbo].[User] ([Id], [Login], [Token], [Balance], [RoleId], [PassportSeries], [PassportNumber]) VALUES (20, N'KarinaK                                           ', N'2f7b29e02b0d8d95f41dd73c730c76c29a55215ec8732f3ef9cd2a753661297b', 3000, 1, N'6754', N'674537')
GO
INSERT [dbo].[User] ([Id], [Login], [Token], [Balance], [RoleId], [PassportSeries], [PassportNumber]) VALUES (21, N'NatashaC                                          ', N'e4f19c9d9be7e64d444de52fac83173bb76d4eafec5b97973f60d997b46d8489', 0, 1, N'    ', N'      ')
GO
INSERT [dbo].[User] ([Id], [Login], [Token], [Balance], [RoleId], [PassportSeries], [PassportNumber]) VALUES (22, N'AnyaG                                             ', N'dd940c768af0b86f35c955ead40c645d0f68b4ca1ea063ec2b4e214746afe74c', 0, 1, N'    ', N'      ')
GO
INSERT [dbo].[User] ([Id], [Login], [Token], [Balance], [RoleId], [PassportSeries], [PassportNumber]) VALUES (23, N'DianaP                                            ', N'e5d35b4d833783235b145a66ba778f32d1f3e5687497bb6f965410902de8fcd8', 0, 1, N'    ', N'      ')
GO
INSERT [dbo].[User] ([Id], [Login], [Token], [Balance], [RoleId], [PassportSeries], [PassportNumber]) VALUES (24, N'NadyaA                                            ', N'625ba6d32cfc286a1829632bc24a6e2793ed45c4c663b8e96bbe6cec1ef42e5c', 0, 1, N'    ', N'      ')
GO
INSERT [dbo].[User] ([Id], [Login], [Token], [Balance], [RoleId], [PassportSeries], [PassportNumber]) VALUES (25, N'ArturG                                            ', N'902930beedab7e312279e818a49c3eea9aafd12cd5036a95757796679070b850', 0, 1, N'    ', N'      ')
GO
INSERT [dbo].[User] ([Id], [Login], [Token], [Balance], [RoleId], [PassportSeries], [PassportNumber]) VALUES (26, N'NikitaA                                           ', N'bf45f393394cbc98d9d4897b99c5b2a0d44e649a7ec78c3d0758b5d5bf05a725', 0, 1, N'    ', N'      ')
GO
INSERT [dbo].[User] ([Id], [Login], [Token], [Balance], [RoleId], [PassportSeries], [PassportNumber]) VALUES (27, N'RodionF                                           ', N'f0a6fb43d8aa27e6a6477b0478e36ac9554dd0f47d4e5cb98f7e5fc045b0fbee', 0, 1, N'    ', N'      ')
GO
INSERT [dbo].[User] ([Id], [Login], [Token], [Balance], [RoleId], [PassportSeries], [PassportNumber]) VALUES (28, N'FidanB                                            ', N'd2598ee11d45d26a39d30635422b7d0ab55c0061787ccb985e9b187642802724', 0, 1, N'    ', N'      ')
GO
INSERT [dbo].[User] ([Id], [Login], [Token], [Balance], [RoleId], [PassportSeries], [PassportNumber]) VALUES (29, N'Olyashya                                          ', N'a176b816229154b635ff5b8b767ee359628e8c82dc33c247759ca7f213923870', 3386, 1, N'5678', N'675467')
GO
INSERT [dbo].[User] ([Id], [Login], [Token], [Balance], [RoleId], [PassportSeries], [PassportNumber]) VALUES (30, N'test                                              ', N'9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08', 4000, 1, N'1111', N'222222')
GO
INSERT [dbo].[User] ([Id], [Login], [Token], [Balance], [RoleId], [PassportSeries], [PassportNumber]) VALUES (31, N'VadimPolyakov                                     ', N'ffee7dae58dadd4176b4743a15ee02855766165929098b83721251e6d09eb51b', 42347, 1, N'5678', N'456778')
GO
INSERT [dbo].[User] ([Id], [Login], [Token], [Balance], [RoleId], [PassportSeries], [PassportNumber]) VALUES (32, N'DanilaB                                           ', N'45acdda675119982feed14cadea4a4b0e919de5ff502aedbffb26eccf2bf2799', 4958, 1, N'9876', N'768754')
GO
INSERT [dbo].[User] ([Id], [Login], [Token], [Balance], [RoleId], [PassportSeries], [PassportNumber]) VALUES (33, N'Puskin1                                           ', N'6b047fc7953bf518ec405b7debb5f3a8f5dfec9b1cb5720e9ccf63dcc840086d', 0, 1, N'6754', N'458798')
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET IDENTITY_INSERT [dbo].[Status] ON 
GO
INSERT [dbo].[Status] ([Id], [Name]) VALUES (1, N'Создан                                            ')
GO
INSERT [dbo].[Status] ([Id], [Name]) VALUES (2, N'Возвращен                                         ')
GO
INSERT [dbo].[Status] ([Id], [Name]) VALUES (3, N'Завершен                                          ')
GO
SET IDENTITY_INSERT [dbo].[Status] OFF
GO
SET IDENTITY_INSERT [dbo].[Ticket] ON 
GO
INSERT [dbo].[Ticket] ([Id], [BuyTime], [Price], [RouteId], [StatusId], [UserId]) VALUES (1, CAST(N'2023-06-13T14:43:13.773' AS DateTime), 1000, 2, 1, 2)
GO
INSERT [dbo].[Ticket] ([Id], [BuyTime], [Price], [RouteId], [StatusId], [UserId]) VALUES (2, CAST(N'2023-06-13T15:02:03.413' AS DateTime), 1000, 4, 1, 3)
GO
INSERT [dbo].[Ticket] ([Id], [BuyTime], [Price], [RouteId], [StatusId], [UserId]) VALUES (3, CAST(N'2023-06-14T19:35:11.197' AS DateTime), 4923, 26, 1, 5)
GO
INSERT [dbo].[Ticket] ([Id], [BuyTime], [Price], [RouteId], [StatusId], [UserId]) VALUES (4, CAST(N'2023-06-14T19:35:26.497' AS DateTime), 2476, 8, 1, 5)
GO
INSERT [dbo].[Ticket] ([Id], [BuyTime], [Price], [RouteId], [StatusId], [UserId]) VALUES (5, CAST(N'2023-06-14T19:42:09.443' AS DateTime), 2738, 9, 1, 6)
GO
INSERT [dbo].[Ticket] ([Id], [BuyTime], [Price], [RouteId], [StatusId], [UserId]) VALUES (6, CAST(N'2023-06-14T19:42:37.773' AS DateTime), 2321, 27, 1, 6)
GO
INSERT [dbo].[Ticket] ([Id], [BuyTime], [Price], [RouteId], [StatusId], [UserId]) VALUES (7, CAST(N'2023-06-14T19:47:50.713' AS DateTime), 2670, 17, 1, 4)
GO
INSERT [dbo].[Ticket] ([Id], [BuyTime], [Price], [RouteId], [StatusId], [UserId]) VALUES (8, CAST(N'2023-06-14T19:48:08.707' AS DateTime), 7369, 25, 1, 4)
GO
INSERT [dbo].[Ticket] ([Id], [BuyTime], [Price], [RouteId], [StatusId], [UserId]) VALUES (9, CAST(N'2023-06-14T19:50:56.167' AS DateTime), 5042, 12, 1, 7)
GO
INSERT [dbo].[Ticket] ([Id], [BuyTime], [Price], [RouteId], [StatusId], [UserId]) VALUES (10, CAST(N'2023-06-14T20:48:21.477' AS DateTime), 2738, 9, 1, 20)
GO
INSERT [dbo].[Ticket] ([Id], [BuyTime], [Price], [RouteId], [StatusId], [UserId]) VALUES (11, CAST(N'2023-06-14T20:49:41.287' AS DateTime), 1000, 4, 1, 20)
GO
INSERT [dbo].[Ticket] ([Id], [BuyTime], [Price], [RouteId], [StatusId], [UserId]) VALUES (12, CAST(N'2023-06-14T20:52:56.657' AS DateTime), 3000, 28, 1, 3)
GO
INSERT [dbo].[Ticket] ([Id], [BuyTime], [Price], [RouteId], [StatusId], [UserId]) VALUES (13, CAST(N'2023-06-14T23:42:43.177' AS DateTime), 3518, 7, 1, 3)
GO
INSERT [dbo].[Ticket] ([Id], [BuyTime], [Price], [RouteId], [StatusId], [UserId]) VALUES (14, CAST(N'2023-06-14T23:56:26.623' AS DateTime), 1614, 11, 1, 29)
GO
INSERT [dbo].[Ticket] ([Id], [BuyTime], [Price], [RouteId], [StatusId], [UserId]) VALUES (15, CAST(N'2023-06-14T23:53:55.867' AS DateTime), 1000, 4, 1, 30)
GO
INSERT [dbo].[Ticket] ([Id], [BuyTime], [Price], [RouteId], [StatusId], [UserId]) VALUES (16, CAST(N'2023-06-15T00:07:38.120' AS DateTime), 4923, 26, 1, 31)
GO
INSERT [dbo].[Ticket] ([Id], [BuyTime], [Price], [RouteId], [StatusId], [UserId]) VALUES (17, CAST(N'2023-06-15T00:32:11.493' AS DateTime), 2476, 8, 1, 31)
GO
INSERT [dbo].[Ticket] ([Id], [BuyTime], [Price], [RouteId], [StatusId], [UserId]) VALUES (18, CAST(N'2023-06-15T00:32:17.660' AS DateTime), 2476, 8, 1, 31)
GO
INSERT [dbo].[Ticket] ([Id], [BuyTime], [Price], [RouteId], [StatusId], [UserId]) VALUES (19, CAST(N'2023-06-15T00:34:14.203' AS DateTime), 1614, 11, 1, 31)
GO
INSERT [dbo].[Ticket] ([Id], [BuyTime], [Price], [RouteId], [StatusId], [UserId]) VALUES (20, CAST(N'2023-06-15T00:53:33.770' AS DateTime), 5042, 15, 1, 32)
GO
INSERT [dbo].[Ticket] ([Id], [BuyTime], [Price], [RouteId], [StatusId], [UserId]) VALUES (21, CAST(N'2023-06-15T11:32:18.853' AS DateTime), 3426, 23, 1, 31)
GO
INSERT [dbo].[Ticket] ([Id], [BuyTime], [Price], [RouteId], [StatusId], [UserId]) VALUES (22, CAST(N'2023-06-15T11:35:52.337' AS DateTime), 2738, 9, 1, 31)
GO
INSERT [dbo].[Ticket] ([Id], [BuyTime], [Price], [RouteId], [StatusId], [UserId]) VALUES (23, CAST(N'2023-06-15T11:40:41.897' AS DateTime), 4219, 10, 1, 33)
GO
SET IDENTITY_INSERT [dbo].[Ticket] OFF
GO
SET IDENTITY_INSERT [dbo].[Transaction] ON 
GO
INSERT [dbo].[Transaction] ([Id], [UserId], [Value], [IsComplited], [PaymentTime], [PaymentType], [Comment]) VALUES (1, 2, 1000, 1, CAST(N'2023-06-13T14:20:03.257' AS DateTime), N'Баланс              ', N'Пополнение баланса')
GO
INSERT [dbo].[Transaction] ([Id], [UserId], [Value], [IsComplited], [PaymentTime], [PaymentType], [Comment]) VALUES (2, 2, -1000, 1, CAST(N'2023-06-13T14:43:13.773' AS DateTime), N'Баланс              ', N'Оплата билета')
GO
INSERT [dbo].[Transaction] ([Id], [UserId], [Value], [IsComplited], [PaymentTime], [PaymentType], [Comment]) VALUES (3, 3, 1000, 1, CAST(N'2023-06-13T14:58:50.790' AS DateTime), N'Баланс              ', N'Пополнение баланса')
GO
INSERT [dbo].[Transaction] ([Id], [UserId], [Value], [IsComplited], [PaymentTime], [PaymentType], [Comment]) VALUES (4, 3, -1000, 1, CAST(N'2023-06-13T15:02:03.413' AS DateTime), N'Баланс              ', N'Оплата билета')
GO
INSERT [dbo].[Transaction] ([Id], [UserId], [Value], [IsComplited], [PaymentTime], [PaymentType], [Comment]) VALUES (5, 2, 5000, 1, CAST(N'2023-06-14T14:39:15.550' AS DateTime), N'Баланс              ', N'Пополнение баланса')
GO
INSERT [dbo].[Transaction] ([Id], [UserId], [Value], [IsComplited], [PaymentTime], [PaymentType], [Comment]) VALUES (6, 5, 10000, 1, CAST(N'2023-06-14T19:34:50.893' AS DateTime), N'Баланс              ', N'Пополнение баланса')
GO
INSERT [dbo].[Transaction] ([Id], [UserId], [Value], [IsComplited], [PaymentTime], [PaymentType], [Comment]) VALUES (7, 5, -4923, 1, CAST(N'2023-06-14T19:35:11.197' AS DateTime), N'Баланс              ', N'Оплата билета')
GO
INSERT [dbo].[Transaction] ([Id], [UserId], [Value], [IsComplited], [PaymentTime], [PaymentType], [Comment]) VALUES (8, 5, -2476, 1, CAST(N'2023-06-14T19:35:26.497' AS DateTime), N'Баланс              ', N'Оплата билета')
GO
INSERT [dbo].[Transaction] ([Id], [UserId], [Value], [IsComplited], [PaymentTime], [PaymentType], [Comment]) VALUES (9, 6, 15000, 1, CAST(N'2023-06-14T19:41:45.047' AS DateTime), N'Баланс              ', N'Пополнение баланса')
GO
INSERT [dbo].[Transaction] ([Id], [UserId], [Value], [IsComplited], [PaymentTime], [PaymentType], [Comment]) VALUES (10, 6, -2738, 1, CAST(N'2023-06-14T19:42:09.443' AS DateTime), N'Баланс              ', N'Оплата билета')
GO
INSERT [dbo].[Transaction] ([Id], [UserId], [Value], [IsComplited], [PaymentTime], [PaymentType], [Comment]) VALUES (11, 6, -2321, 1, CAST(N'2023-06-14T19:42:37.773' AS DateTime), N'Баланс              ', N'Оплата билета')
GO
INSERT [dbo].[Transaction] ([Id], [UserId], [Value], [IsComplited], [PaymentTime], [PaymentType], [Comment]) VALUES (12, 4, 12000, 1, CAST(N'2023-06-14T19:47:23.830' AS DateTime), N'Баланс              ', N'Пополнение баланса')
GO
INSERT [dbo].[Transaction] ([Id], [UserId], [Value], [IsComplited], [PaymentTime], [PaymentType], [Comment]) VALUES (13, 4, -2670, 1, CAST(N'2023-06-14T19:47:50.713' AS DateTime), N'Баланс              ', N'Оплата билета')
GO
INSERT [dbo].[Transaction] ([Id], [UserId], [Value], [IsComplited], [PaymentTime], [PaymentType], [Comment]) VALUES (14, 4, -7369, 1, CAST(N'2023-06-14T19:48:08.707' AS DateTime), N'Баланс              ', N'Оплата билета')
GO
INSERT [dbo].[Transaction] ([Id], [UserId], [Value], [IsComplited], [PaymentTime], [PaymentType], [Comment]) VALUES (15, 7, 9000, 1, CAST(N'2023-06-14T19:50:31.243' AS DateTime), N'Баланс              ', N'Пополнение баланса')
GO
INSERT [dbo].[Transaction] ([Id], [UserId], [Value], [IsComplited], [PaymentTime], [PaymentType], [Comment]) VALUES (16, 7, -5042, 1, CAST(N'2023-06-14T19:50:56.167' AS DateTime), N'Баланс              ', N'Оплата билета')
GO
INSERT [dbo].[Transaction] ([Id], [UserId], [Value], [IsComplited], [PaymentTime], [PaymentType], [Comment]) VALUES (17, 20, 2738, 1, CAST(N'2023-06-14T20:48:21.477' AS DateTime), N'Наличные            ', N'Пополнение баланса наличными через диспетчера')
GO
INSERT [dbo].[Transaction] ([Id], [UserId], [Value], [IsComplited], [PaymentTime], [PaymentType], [Comment]) VALUES (18, 20, -2738, 1, CAST(N'2023-06-14T20:48:21.477' AS DateTime), N'Наличные            ', N'Оплата билета')
GO
INSERT [dbo].[Transaction] ([Id], [UserId], [Value], [IsComplited], [PaymentTime], [PaymentType], [Comment]) VALUES (19, 20, 4000, 1, CAST(N'2023-06-14T20:49:18.093' AS DateTime), N'Баланс              ', N'Пополнение баланса')
GO
INSERT [dbo].[Transaction] ([Id], [UserId], [Value], [IsComplited], [PaymentTime], [PaymentType], [Comment]) VALUES (20, 20, -1000, 1, CAST(N'2023-06-14T20:49:41.287' AS DateTime), N'Баланс              ', N'Оплата билета')
GO
INSERT [dbo].[Transaction] ([Id], [UserId], [Value], [IsComplited], [PaymentTime], [PaymentType], [Comment]) VALUES (21, 3, 5000, 1, CAST(N'2023-06-14T20:51:32.123' AS DateTime), N'Баланс              ', N'Пополнение баланса')
GO
INSERT [dbo].[Transaction] ([Id], [UserId], [Value], [IsComplited], [PaymentTime], [PaymentType], [Comment]) VALUES (22, 3, -3000, 1, CAST(N'2023-06-14T20:52:56.657' AS DateTime), N'Баланс              ', N'Оплата билета')
GO
INSERT [dbo].[Transaction] ([Id], [UserId], [Value], [IsComplited], [PaymentTime], [PaymentType], [Comment]) VALUES (23, 3, 3000, 1, CAST(N'2023-06-14T23:41:52.533' AS DateTime), N'Баланс              ', N'Пополнение баланса')
GO
INSERT [dbo].[Transaction] ([Id], [UserId], [Value], [IsComplited], [PaymentTime], [PaymentType], [Comment]) VALUES (24, 3, -3518, 1, CAST(N'2023-06-14T23:42:43.177' AS DateTime), N'Баланс              ', N'Оплата билета')
GO
INSERT [dbo].[Transaction] ([Id], [UserId], [Value], [IsComplited], [PaymentTime], [PaymentType], [Comment]) VALUES (25, 29, 5000, 1, CAST(N'2023-06-14T23:56:16.677' AS DateTime), N'Баланс              ', N'Пополнение баланса')
GO
INSERT [dbo].[Transaction] ([Id], [UserId], [Value], [IsComplited], [PaymentTime], [PaymentType], [Comment]) VALUES (26, 29, -1614, 1, CAST(N'2023-06-14T23:56:26.623' AS DateTime), N'Баланс              ', N'Оплата билета')
GO
INSERT [dbo].[Transaction] ([Id], [UserId], [Value], [IsComplited], [PaymentTime], [PaymentType], [Comment]) VALUES (27, 30, 5000, 1, CAST(N'2023-06-14T23:53:49.627' AS DateTime), N'Баланс              ', N'Пополнение баланса')
GO
INSERT [dbo].[Transaction] ([Id], [UserId], [Value], [IsComplited], [PaymentTime], [PaymentType], [Comment]) VALUES (28, 30, -1000, 1, CAST(N'2023-06-14T23:53:55.867' AS DateTime), N'Баланс              ', N'Оплата билета')
GO
INSERT [dbo].[Transaction] ([Id], [UserId], [Value], [IsComplited], [PaymentTime], [PaymentType], [Comment]) VALUES (29, 31, 10000, 1, CAST(N'2023-06-15T00:07:12.347' AS DateTime), N'Баланс              ', N'Пополнение баланса')
GO
INSERT [dbo].[Transaction] ([Id], [UserId], [Value], [IsComplited], [PaymentTime], [PaymentType], [Comment]) VALUES (30, 31, -4923, 1, CAST(N'2023-06-15T00:07:38.120' AS DateTime), N'Баланс              ', N'Оплата билета')
GO
INSERT [dbo].[Transaction] ([Id], [UserId], [Value], [IsComplited], [PaymentTime], [PaymentType], [Comment]) VALUES (31, 31, 50000, 1, CAST(N'2023-06-15T00:31:48.680' AS DateTime), N'Баланс              ', N'Пополнение баланса')
GO
INSERT [dbo].[Transaction] ([Id], [UserId], [Value], [IsComplited], [PaymentTime], [PaymentType], [Comment]) VALUES (32, 31, -2476, 1, CAST(N'2023-06-15T00:32:11.493' AS DateTime), N'Баланс              ', N'Оплата билета')
GO
INSERT [dbo].[Transaction] ([Id], [UserId], [Value], [IsComplited], [PaymentTime], [PaymentType], [Comment]) VALUES (33, 31, -2476, 1, CAST(N'2023-06-15T00:32:17.660' AS DateTime), N'Баланс              ', N'Оплата билета')
GO
INSERT [dbo].[Transaction] ([Id], [UserId], [Value], [IsComplited], [PaymentTime], [PaymentType], [Comment]) VALUES (34, 31, -1614, 1, CAST(N'2023-06-15T00:34:14.203' AS DateTime), N'Баланс              ', N'Оплата билета')
GO
INSERT [dbo].[Transaction] ([Id], [UserId], [Value], [IsComplited], [PaymentTime], [PaymentType], [Comment]) VALUES (35, 32, 10000, 1, CAST(N'2023-06-15T00:53:04.923' AS DateTime), N'Баланс              ', N'Пополнение баланса')
GO
INSERT [dbo].[Transaction] ([Id], [UserId], [Value], [IsComplited], [PaymentTime], [PaymentType], [Comment]) VALUES (36, 32, -5042, 1, CAST(N'2023-06-15T00:53:33.770' AS DateTime), N'Баланс              ', N'Оплата билета')
GO
INSERT [dbo].[Transaction] ([Id], [UserId], [Value], [IsComplited], [PaymentTime], [PaymentType], [Comment]) VALUES (37, 31, -3426, 1, CAST(N'2023-06-15T11:32:18.853' AS DateTime), N'Баланс              ', N'Оплата билета')
GO
INSERT [dbo].[Transaction] ([Id], [UserId], [Value], [IsComplited], [PaymentTime], [PaymentType], [Comment]) VALUES (38, 31, -2738, 1, CAST(N'2023-06-15T11:35:52.337' AS DateTime), N'Баланс              ', N'Оплата билета')
GO
INSERT [dbo].[Transaction] ([Id], [UserId], [Value], [IsComplited], [PaymentTime], [PaymentType], [Comment]) VALUES (39, 33, 4219, 1, CAST(N'2023-06-15T11:40:41.897' AS DateTime), N'Наличные            ', N'Пополнение баланса наличными через диспетчера')
GO
INSERT [dbo].[Transaction] ([Id], [UserId], [Value], [IsComplited], [PaymentTime], [PaymentType], [Comment]) VALUES (40, 33, -4219, 1, CAST(N'2023-06-15T11:40:41.897' AS DateTime), N'Наличные            ', N'Оплата билета')
GO
SET IDENTITY_INSERT [dbo].[Transaction] OFF
GO
