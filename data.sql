USE [BookStore]
GO
SET IDENTITY_INSERT [dbo].[Brand] ON 

INSERT [dbo].[Brand] ([brand_id], [name], [image]) VALUES (1, N'Action and Adventure', NULL)
INSERT [dbo].[Brand] ([brand_id], [name], [image]) VALUES (2, N'Fairy Tale', NULL)
INSERT [dbo].[Brand] ([brand_id], [name], [image]) VALUES (3, N'Science Fiction', NULL)
INSERT [dbo].[Brand] ([brand_id], [name], [image]) VALUES (4, N'Fantasy', NULL)
INSERT [dbo].[Brand] ([brand_id], [name], [image]) VALUES (5, N'Romance', NULL)
INSERT [dbo].[Brand] ([brand_id], [name], [image]) VALUES (6, N'Horror', NULL)
INSERT [dbo].[Brand] ([brand_id], [name], [image]) VALUES (7, N'Mystery', NULL)
INSERT [dbo].[Brand] ([brand_id], [name], [image]) VALUES (8, N'Historical Fiction', NULL)
INSERT [dbo].[Brand] ([brand_id], [name], [image]) VALUES (9, N'Urban Fantasy', NULL)
INSERT [dbo].[Brand] ([brand_id], [name], [image]) VALUES (10, N'Modern Fantasy', NULL)
INSERT [dbo].[Brand] ([brand_id], [name], [image]) VALUES (11, N'Biography and Memoir', NULL)
INSERT [dbo].[Brand] ([brand_id], [name], [image]) VALUES (12, N'Autobiography', NULL)
INSERT [dbo].[Brand] ([brand_id], [name], [image]) VALUES (13, N'History', NULL)
INSERT [dbo].[Brand] ([brand_id], [name], [image]) VALUES (14, N'Science', NULL)
INSERT [dbo].[Brand] ([brand_id], [name], [image]) VALUES (15, N'Sociology', NULL)
INSERT [dbo].[Brand] ([brand_id], [name], [image]) VALUES (16, N'Political Science', NULL)
INSERT [dbo].[Brand] ([brand_id], [name], [image]) VALUES (17, N'Philosophy', NULL)
INSERT [dbo].[Brand] ([brand_id], [name], [image]) VALUES (18, N'Religion and Spirituality', NULL)
INSERT [dbo].[Brand] ([brand_id], [name], [image]) VALUES (19, N'Textbook', NULL)
INSERT [dbo].[Brand] ([brand_id], [name], [image]) VALUES (20, N'Psychology', NULL)
INSERT [dbo].[Brand] ([brand_id], [name], [image]) VALUES (21, N'Dictionary', NULL)
INSERT [dbo].[Brand] ([brand_id], [name], [image]) VALUES (22, N'Encyclopedia', NULL)
INSERT [dbo].[Brand] ([brand_id], [name], [image]) VALUES (23, N'Manual', NULL)
INSERT [dbo].[Brand] ([brand_id], [name], [image]) VALUES (24, N'Handbook', NULL)
INSERT [dbo].[Brand] ([brand_id], [name], [image]) VALUES (25, N'Comics', NULL)
INSERT [dbo].[Brand] ([brand_id], [name], [image]) VALUES (26, N'Picture Books', NULL)
INSERT [dbo].[Brand] ([brand_id], [name], [image]) VALUES (27, N'Children''s Fiction', NULL)
INSERT [dbo].[Brand] ([brand_id], [name], [image]) VALUES (28, N'Young Adult Fiction', NULL)
INSERT [dbo].[Brand] ([brand_id], [name], [image]) VALUES (29, N'Programming Books', NULL)
INSERT [dbo].[Brand] ([brand_id], [name], [image]) VALUES (30, N'Medical Books', NULL)
INSERT [dbo].[Brand] ([brand_id], [name], [image]) VALUES (31, N'Engineering Books', NULL)
INSERT [dbo].[Brand] ([brand_id], [name], [image]) VALUES (32, N'Business Books', NULL)
INSERT [dbo].[Brand] ([brand_id], [name], [image]) VALUES (33, N'Finance Books', NULL)
INSERT [dbo].[Brand] ([brand_id], [name], [image]) VALUES (34, N'Personal Development', NULL)
INSERT [dbo].[Brand] ([brand_id], [name], [image]) VALUES (35, N'Skill Development', NULL)
INSERT [dbo].[Brand] ([brand_id], [name], [image]) VALUES (36, N'Nutrition and Health', NULL)
INSERT [dbo].[Brand] ([brand_id], [name], [image]) VALUES (37, N'Cookbooks', NULL)
INSERT [dbo].[Brand] ([brand_id], [name], [image]) VALUES (38, N'Travel Guides', NULL)
INSERT [dbo].[Brand] ([brand_id], [name], [image]) VALUES (39, N'Music Books', NULL)
INSERT [dbo].[Brand] ([brand_id], [name], [image]) VALUES (40, N'Art Books', NULL)
INSERT [dbo].[Brand] ([brand_id], [name], [image]) VALUES (41, N'Film Books', NULL)
INSERT [dbo].[Brand] ([brand_id], [name], [image]) VALUES (42, N'Sports Books', NULL)
INSERT [dbo].[Brand] ([brand_id], [name], [image]) VALUES (43, N'Tragedy', NULL)
INSERT [dbo].[Brand] ([brand_id], [name], [image]) VALUES (44, N'Light Novel', NULL)
INSERT [dbo].[Brand] ([brand_id], [name], [image]) VALUES (45, N'Education', NULL)
SET IDENTITY_INSERT [dbo].[Brand] OFF
GO
SET IDENTITY_INSERT [dbo].[TypeBook] ON 

INSERT [dbo].[TypeBook] ([type_book_id], [type_book_name]) VALUES (1, N'nbook')
INSERT [dbo].[TypeBook] ([type_book_id], [type_book_name]) VALUES (2, N'ebook')
SET IDENTITY_INSERT [dbo].[TypeBook] OFF
GO
SET IDENTITY_INSERT [dbo].[Book] ON 

INSERT [dbo].[Book] ([book_id], [title], [author_name], [image], [type_book_id], [upload_date], [price], [quantity], [description], [rating], [link_ebook], [is_sale]) VALUES (1, N'Romeo and Juliet', N'William Shakespeare', N'https://firebasestorage.googleapis.com/v0/b/bookstore-59884.appspot.com/o/images%2Fdeb00511-c5f3-413a-ab4f-f57b91552ea5.webp?alt=media&token=99ceda6e-157d-4b6e-a4f4-606af3fabaad', 1, CAST(N'2024-11-15T21:58:23.600' AS DateTime), CAST(43500.00 AS Decimal(10, 2)), 7, N'Tình yêu bị ngăn cấm', NULL, NULL, 1)
INSERT [dbo].[Book] ([book_id], [title], [author_name], [image], [type_book_id], [upload_date], [price], [quantity], [description], [rating], [link_ebook], [is_sale]) VALUES (2, N'The Adventures of a curious cat', N'Zelda', N'https://firebasestorage.googleapis.com/v0/b/bookstore-59884.appspot.com/o/images%2F26fc6f6d-1b26-4b2c-9c0c-1a8cc1efe8dd.webp?alt=media&token=d3ccc6ca-2ccd-4144-a778-f4c28d94d116', 1, CAST(N'2024-11-15T22:00:19.547' AS DateTime), CAST(105600.00 AS Decimal(10, 2)), 11, N'Hành trình khám phá cuộc sống dưới góc nhìn của một chú mèo đáng yêu, tinh nghịch', NULL, NULL, 1)
INSERT [dbo].[Book] ([book_id], [title], [author_name], [image], [type_book_id], [upload_date], [price], [quantity], [description], [rating], [link_ebook], [is_sale]) VALUES (3, N'No Family', N'Hector Malot', N'https://firebasestorage.googleapis.com/v0/b/bookstore-59884.appspot.com/o/images%2F768274a2-b8d4-4172-b210-6c4e86c8327f.webp?alt=media&token=5cdf2d10-7d61-48cd-a696-060cdd16ee1d', 1, CAST(N'2024-11-11T12:37:00.247' AS DateTime), CAST(34500.00 AS Decimal(10, 2)), 9, N'Thế nào mới là gia đình thực sự', NULL, NULL, 1)
INSERT [dbo].[Book] ([book_id], [title], [author_name], [image], [type_book_id], [upload_date], [price], [quantity], [description], [rating], [link_ebook], [is_sale]) VALUES (4, N'Black cat and Demon''s eyes', N'Chloe Rhodes', N'https://firebasestorage.googleapis.com/v0/b/bookstore-59884.appspot.com/o/images%2Fe3352a23-61f7-4a4e-9d05-c2d28f863374.webp?alt=media&token=d0fe99a8-9a6b-4f8b-bf6b-93eec8aad988', 2, CAST(N'2024-11-05T10:56:48.967' AS DateTime), CAST(67000.00 AS Decimal(10, 2)), 30, N'Bách khoa thư về những mê tín cổ xưa', NULL, N'https://firebasestorage.googleapis.com/v0/b/bookstore-59884.appspot.com/o/images%2F6c17bd77-11b9-4b36-a131-ec43209c7b5c.txt?alt=media&token=3311519d-eff8-45ff-90fb-cf755d391721', 1)
INSERT [dbo].[Book] ([book_id], [title], [author_name], [image], [type_book_id], [upload_date], [price], [quantity], [description], [rating], [link_ebook], [is_sale]) VALUES (5, N'Crime scene cleaner', N'LapLap', N'https://firebasestorage.googleapis.com/v0/b/bookstore-59884.appspot.com/o/images%2F09b731ac-1998-4e2f-b24c-740c4ed39b8c.webp?alt=media&token=245fe202-b84f-4903-8e61-085da6a7357c', 2, CAST(N'2024-11-05T11:00:59.353' AS DateTime), CAST(89500.00 AS Decimal(10, 2)), 40, N'Mô tả chân thật kinh hoàng ? hiện trường về án mạng', NULL, N'https://firebasestorage.googleapis.com/v0/b/bookstore-59884.appspot.com/o/images%2F0c177886-a42a-4b92-87b1-09913e051fd9.txt?alt=media&token=8c3dc1f1-e8be-4f37-ab64-761c92064922', 1)
INSERT [dbo].[Book] ([book_id], [title], [author_name], [image], [type_book_id], [upload_date], [price], [quantity], [description], [rating], [link_ebook], [is_sale]) VALUES (6, N'Don''t worry ! you''re still fine', N'Jason Adam Katzenstein', N'https://firebasestorage.googleapis.com/v0/b/bookstore-59884.appspot.com/o/images%2F5b6f821b-e582-4a5a-a548-5db4617f56f2.webp?alt=media&token=49d40d95-58cf-4d63-95c7-65b21a7e95a9', 1, CAST(N'2024-11-11T11:45:29.603' AS DateTime), CAST(45000.00 AS Decimal(10, 2)), 18, N'Ðừng đợi cho nuớc đến chân mới nhảy', NULL, NULL, 1)
INSERT [dbo].[Book] ([book_id], [title], [author_name], [image], [type_book_id], [upload_date], [price], [quantity], [description], [rating], [link_ebook], [is_sale]) VALUES (7, N'Better than the movies', N'Lynn Painter', N'https://firebasestorage.googleapis.com/v0/b/bookstore-59884.appspot.com/o/images%2F86726041-4d54-4dea-9fee-c692196d6743.webp?alt=media&token=3360862a-3bb4-4aef-a392-3fd575bcac08', 1, CAST(N'2024-11-15T13:14:32.103' AS DateTime), CAST(261000.00 AS Decimal(10, 2)), 3, N'Tình yêu tuyệt vời còn hơn cả trong phim', NULL, NULL, 1)
INSERT [dbo].[Book] ([book_id], [title], [author_name], [image], [type_book_id], [upload_date], [price], [quantity], [description], [rating], [link_ebook], [is_sale]) VALUES (8, N'Complete IELTS B1 Student''s Book with answer with CD-ROM', N'Cambridge University', N'https://firebasestorage.googleapis.com/v0/b/bookstore-59884.appspot.com/o/images%2F26c36e26-7aa9-4a8f-a642-b98e161b50b0.webp?alt=media&token=fa44549b-d683-4976-9941-a8d7cf937e06', 2, CAST(N'2024-11-05T11:12:10.043' AS DateTime), CAST(251100.00 AS Decimal(10, 2)), 40, N'Tiếng anh dành cho bands 4-5', NULL, N'https://firebasestorage.googleapis.com/v0/b/bookstore-59884.appspot.com/o/images%2F08e88c09-4bbd-4add-b593-8897d65c4f77.txt?alt=media&token=374c1131-0bef-487f-8702-fc762148f0e8', 1)
INSERT [dbo].[Book] ([book_id], [title], [author_name], [image], [type_book_id], [upload_date], [price], [quantity], [description], [rating], [link_ebook], [is_sale]) VALUES (9, N'Suzume No Tojimari', N'Makoto Shinkai', N'https://firebasestorage.googleapis.com/v0/b/bookstore-59884.appspot.com/o/images%2F8296fc89-6aab-4805-be8c-0a3ed2a5a8a8.webp?alt=media&token=1c990dad-d11a-4486-b121-f559fc344177', 1, CAST(N'2024-11-11T23:29:13.273' AS DateTime), CAST(522000.00 AS Decimal(10, 2)), 14, N'Don''t Open the door, Suzume !', NULL, NULL, 1)
INSERT [dbo].[Book] ([book_id], [title], [author_name], [image], [type_book_id], [upload_date], [price], [quantity], [description], [rating], [link_ebook], [is_sale]) VALUES (10, N'Attack on Titan 15', N'Hajime Isayama', N'https://firebasestorage.googleapis.com/v0/b/bookstore-59884.appspot.com/o/images%2F4b56721a-734d-4ecd-979d-110853795657.webp?alt=media&token=db437b04-0bda-44d8-ba4e-741450e39ae2', 1, CAST(N'2024-11-15T14:16:18.993' AS DateTime), CAST(45000.00 AS Decimal(10, 2)), 22, N'Liệu chúng ta có thấy được tự do bên kia biển không ?', NULL, NULL, 1)
INSERT [dbo].[Book] ([book_id], [title], [author_name], [image], [type_book_id], [upload_date], [price], [quantity], [description], [rating], [link_ebook], [is_sale]) VALUES (11, N'Sakamoto Days', N'Yuto Suzuki', N'https://firebasestorage.googleapis.com/v0/b/bookstore-59884.appspot.com/o/images%2Fa43ffee9-d84b-4e77-8c26-e673b62b79bc.webp?alt=media&token=fcc3fe07-0f93-43b9-ae94-b6d5a021e93c', 1, CAST(N'2024-11-05T11:20:48.323' AS DateTime), CAST(40050.00 AS Decimal(10, 2)), 12, N'Một ngày bình thuờng nhất của Sakamoto', NULL, NULL, 1)
INSERT [dbo].[Book] ([book_id], [title], [author_name], [image], [type_book_id], [upload_date], [price], [quantity], [description], [rating], [link_ebook], [is_sale]) VALUES (12, N'Re:zero 13', N'Tappei Nagatsuki', N'https://firebasestorage.googleapis.com/v0/b/bookstore-59884.appspot.com/o/images%2Fae2918ac-7455-4cf1-a09d-11e609dd628f.webp?alt=media&token=f1e33b1e-180e-4b40-8e4c-305f1e06495e', 1, CAST(N'2024-11-10T17:26:17.760' AS DateTime), CAST(108000.00 AS Decimal(10, 2)), 10, N'Bắt đầu mới tại thế giới khác', NULL, NULL, 1)
INSERT [dbo].[Book] ([book_id], [title], [author_name], [image], [type_book_id], [upload_date], [price], [quantity], [description], [rating], [link_ebook], [is_sale]) VALUES (13, N'Maria Beetle', N'Isaka Kotaro', N'https://firebasestorage.googleapis.com/v0/b/bookstore-59884.appspot.com/o/images%2Fb842e0bd-98f2-44c9-981b-828880795226.webp?alt=media&token=91a64726-7d5a-4141-a1cc-2793eb6550d8', 1, CAST(N'2024-11-10T17:26:17.773' AS DateTime), CAST(122000.00 AS Decimal(10, 2)), 34, N'Assassin Maria', NULL, NULL, 1)
INSERT [dbo].[Book] ([book_id], [title], [author_name], [image], [type_book_id], [upload_date], [price], [quantity], [description], [rating], [link_ebook], [is_sale]) VALUES (14, N'Startup Science', N'Masayuki Tadokoro', N'https://firebasestorage.googleapis.com/v0/b/bookstore-59884.appspot.com/o/images%2F6b6aba52-e9bc-4854-a03d-4a53fef605ba.webp?alt=media&token=de73bde3-e2e6-402e-872c-dfe9a168c351', 2, CAST(N'2024-11-05T21:47:28.090' AS DateTime), CAST(119000.00 AS Decimal(10, 2)), 23, N'How to startup ?', NULL, N'https://firebasestorage.googleapis.com/v0/b/bookstore-59884.appspot.com/o/images%2F47daa00b-e622-4d7f-bf90-f9f669c914c4.txt?alt=media&token=9fc4b077-046d-4f5a-a5e2-7cd0f2ec1bc0', 1)
INSERT [dbo].[Book] ([book_id], [title], [author_name], [image], [type_book_id], [upload_date], [price], [quantity], [description], [rating], [link_ebook], [is_sale]) VALUES (15, N'Messy', N'Tim Harford', N'https://firebasestorage.googleapis.com/v0/b/bookstore-59884.appspot.com/o/images%2F50e6c8d0-4cca-4358-9612-64d316b3f6e6.webp?alt=media&token=37544b19-2134-4190-9104-152d4067bae9', 1, CAST(N'2024-11-15T13:13:39.957' AS DateTime), CAST(79500.00 AS Decimal(10, 2)), 20, N'Creative in a mess', NULL, NULL, 1)
INSERT [dbo].[Book] ([book_id], [title], [author_name], [image], [type_book_id], [upload_date], [price], [quantity], [description], [rating], [link_ebook], [is_sale]) VALUES (16, N'Man''s search for meaning', N'Viktor E. Frankl', N'https://firebasestorage.googleapis.com/v0/b/bookstore-59884.appspot.com/o/images%2F572ed273-12d5-40e6-9324-cc01a6ce1000.webp?alt=media&token=b7cd1c5d-b46e-452f-97cd-8249a5a20095', 1, CAST(N'2024-11-07T21:43:34.093' AS DateTime), CAST(70400.00 AS Decimal(10, 2)), 20, N'Meaning of Life ? We''re finding that answer', NULL, NULL, 1)
INSERT [dbo].[Book] ([book_id], [title], [author_name], [image], [type_book_id], [upload_date], [price], [quantity], [description], [rating], [link_ebook], [is_sale]) VALUES (17, N'PET practice tests 1', N'Barbara Thomas', N'https://firebasestorage.googleapis.com/v0/b/bookstore-59884.appspot.com/o/images%2F664ad22f-c486-4405-a6f4-6ee3be35c708.png?alt=media&token=2c2f533a-43e6-4a76-9125-a8643a6555aa', 2, CAST(N'2024-08-11T18:54:53.670' AS DateTime), CAST(500000.00 AS Decimal(10, 2)), 20, N'Need test ?', NULL, N'https://firebasestorage.googleapis.com/v0/b/bookstore-59884.appspot.com/o/images%2F374a111f-3fcb-4e72-b0e7-82e2b5dd2e74.pdf?alt=media&token=6ae0e6df-5c46-4a2f-9aea-5b50ab024c7d', 1)
INSERT [dbo].[Book] ([book_id], [title], [author_name], [image], [type_book_id], [upload_date], [price], [quantity], [description], [rating], [link_ebook], [is_sale]) VALUES (18, N'10% happier', N'Dan Harris', N'https://firebasestorage.googleapis.com/v0/b/bookstore-59884.appspot.com/o/images%2Fe91d21a6-b5f5-438d-aa3a-fb6d8bdd059b.webp?alt=media&token=88d92764-8e6b-4253-a06c-31fdbb4248c9', 2, CAST(N'2024-11-08T16:10:22.290' AS DateTime), CAST(74500.00 AS Decimal(10, 2)), 20, N'Finding happy ?', NULL, N'https://firebasestorage.googleapis.com/v0/b/bookstore-59884.appspot.com/o/images%2F596c8b6f-3800-4d9b-be83-f36eb046c9b4.txt?alt=media&token=cd023fcb-e63a-48c6-afa7-2c911d083d59', 1)
INSERT [dbo].[Book] ([book_id], [title], [author_name], [image], [type_book_id], [upload_date], [price], [quantity], [description], [rating], [link_ebook], [is_sale]) VALUES (19, N'How we decide', N'Jonah Lehrer', N'https://firebasestorage.googleapis.com/v0/b/bookstore-59884.appspot.com/o/images%2Fd8b29a2f-b533-469e-aba4-5e173c884165.webp?alt=media&token=89fa3ea7-a672-4587-8f7a-2d6f06b849c4', 2, CAST(N'2024-11-08T16:15:28.197' AS DateTime), CAST(78200.00 AS Decimal(10, 2)), 20, N'Decide ?', NULL, N'https://firebasestorage.googleapis.com/v0/b/bookstore-59884.appspot.com/o/images%2F443af5af-c40b-493e-ad03-23465deb2fd8.txt?alt=media&token=5b4acae2-46c3-4d45-9d79-21f9143d14b3', 1)
INSERT [dbo].[Book] ([book_id], [title], [author_name], [image], [type_book_id], [upload_date], [price], [quantity], [description], [rating], [link_ebook], [is_sale]) VALUES (20, N'Change for success', N'Ap-ra-ham', N'https://firebasestorage.googleapis.com/v0/b/bookstore-59884.appspot.com/o/images%2F5cd8db86-e097-441c-89c3-ca87499db5e4.webp?alt=media&token=14f44b26-1a52-4d99-b30a-72c9ec5c5249', 2, CAST(N'2024-11-08T16:27:10.470' AS DateTime), CAST(69000.00 AS Decimal(10, 2)), 20, N'Change for success', NULL, N'https://firebasestorage.googleapis.com/v0/b/bookstore-59884.appspot.com/o/images%2F0b1a0160-68e7-414e-9605-df5961479fb2.txt?alt=media&token=7f745ad5-a624-43d8-8390-830612296250', 1)
INSERT [dbo].[Book] ([book_id], [title], [author_name], [image], [type_book_id], [upload_date], [price], [quantity], [description], [rating], [link_ebook], [is_sale]) VALUES (21, N'Sức mạnh tiềm thức', N'Dr. Joseph Murphy', N'https://firebasestorage.googleapis.com/v0/b/bookstore-59884.appspot.com/o/images%2F49f2ae61-8815-46d7-9220-9cb90e1faf7b.webp?alt=media&token=b9bc339f-516e-40a2-8cd3-6ee8a3ea84d4', 1, CAST(N'2024-11-09T23:36:22.457' AS DateTime), CAST(96000.00 AS Decimal(10, 2)), 19, N'tìm hiểu và khai mở thế giới quan của bản thân', NULL, NULL, 1)
INSERT [dbo].[Book] ([book_id], [title], [author_name], [image], [type_book_id], [upload_date], [price], [quantity], [description], [rating], [link_ebook], [is_sale]) VALUES (22, N'GetName', N'GET', N'https://firebasestorage.googleapis.com/v0/b/bookstore-59884.appspot.com/o/images%2F193b1433-e52b-4596-88fa-a75e3b6a174e.png?alt=media&token=d4fc3127-239b-45f1-abf8-be0f3cb2dacf', 1, CAST(N'2024-07-19T22:13:41.257' AS DateTime), CAST(30000.00 AS Decimal(10, 2)), 3, N'GG', NULL, NULL, 0)
INSERT [dbo].[Book] ([book_id], [title], [author_name], [image], [type_book_id], [upload_date], [price], [quantity], [description], [rating], [link_ebook], [is_sale]) VALUES (23, N'Think Clean', N'Kim Oanh', N'https://firebasestorage.googleapis.com/v0/b/bookstore-59884.appspot.com/o/images%2F0326d4eb-d31a-4e2f-8012-6e3a3edfe4f3.webp?alt=media&token=d36c18cf-3610-4675-a6d5-5a148e280442', 1, CAST(N'2024-11-13T15:32:27.230' AS DateTime), CAST(161000.00 AS Decimal(10, 2)), 1, N'Nghĩ sạch - sống khôn ngoan hơn', NULL, NULL, 1)
INSERT [dbo].[Book] ([book_id], [title], [author_name], [image], [type_book_id], [upload_date], [price], [quantity], [description], [rating], [link_ebook], [is_sale]) VALUES (24, N'Pháp luật đại cương và nhà nước pháp quyền', N'Triệu Quốc Mạnh', N'https://firebasestorage.googleapis.com/v0/b/bookstore-59884.appspot.com/o/images%2F7e6b4c0e-b086-4629-84cb-5486492950ae.webp?alt=media&token=312c8085-bd1a-4d84-b9dc-ed0631d8a965', 1, CAST(N'2024-11-15T13:41:01.827' AS DateTime), CAST(246000.00 AS Decimal(10, 2)), 20, N'Sách này không viết cho những người trong nghề, mà chỉ dành cho những người ngoài nghề hoặc những ai không đủ thời gian theo ngành luật học nhưng tự thấy không thể thiếu khái niệm cơ bản về pháp luật trong kiến thức của mình. Bảy phần đầu trong sách với nội dung đại cương có thể giúp người đọc hiểu dễ dàng hơn khi đọc các văn kiện lập pháp hoặc lập quy hiện hành, và có một phần nào so sánh với khái niệm hay thuật ngữ về pháp luật của các nước phát triển.

Có thể xem nội dung của cuốn sách này như phần nhập môn của chương trình tự học luật, hoặc cũng có thể xem là một phần đóng góp khiêm tốn vào việc thực hiện chủ trương xây dựng kiến thức pháp luật phổ thông cho người dân.', NULL, NULL, 1)
INSERT [dbo].[Book] ([book_id], [title], [author_name], [image], [type_book_id], [upload_date], [price], [quantity], [description], [rating], [link_ebook], [is_sale]) VALUES (25, N'lmao', N'lmao', N'https://firebasestorage.googleapis.com/v0/b/bookstore-59884.appspot.com/o/images%2F7a6a0eaf-f387-4672-8cf8-46f6e30162d5.png?alt=media&token=e69aeedc-4e6e-4dcf-8dd0-c337c14660ca', 1, CAST(N'2024-11-15T13:27:14.787' AS DateTime), CAST(20000.00 AS Decimal(10, 2)), 30, N'a', NULL, NULL, 0)
INSERT [dbo].[Book] ([book_id], [title], [author_name], [image], [type_book_id], [upload_date], [price], [quantity], [description], [rating], [link_ebook], [is_sale]) VALUES (26, N'Chia sẻ từ trái tim', N'Thích Pháp Hòa', N'https://firebasestorage.googleapis.com/v0/b/bookstore-59884.appspot.com/o/images%2Fd07f89fb-b3cf-4a24-b91e-7a9999c02da5.webp?alt=media&token=0942d633-8846-486c-80d1-a9c545aaca21', 1, CAST(N'2024-11-19T22:17:30.887' AS DateTime), CAST(120960.00 AS Decimal(10, 2)), 12, N'50 bài giảng nhân quả thiết thực trong cuộc sống', NULL, NULL, 1)
INSERT [dbo].[Book] ([book_id], [title], [author_name], [image], [type_book_id], [upload_date], [price], [quantity], [description], [rating], [link_ebook], [is_sale]) VALUES (27, N'Tư duy ngược', N'Nguyễn Anh Dũng', N'https://firebasestorage.googleapis.com/v0/b/bookstore-59884.appspot.com/o/images%2F8f201b51-4d35-46cf-89ce-755e6fb68170.webp?alt=media&token=c2a14bec-e245-4336-b3f4-eb76e445d97c', 1, CAST(N'2024-11-19T22:20:54.110' AS DateTime), CAST(65900.00 AS Decimal(10, 2)), 20, N'Tư duy khác đi sẽ dẫn đến nhiều điều mới hơn', NULL, NULL, 1)
INSERT [dbo].[Book] ([book_id], [title], [author_name], [image], [type_book_id], [upload_date], [price], [quantity], [description], [rating], [link_ebook], [is_sale]) VALUES (28, N'Hiểu về trái tim', N'Minh Niệm', N'https://firebasestorage.googleapis.com/v0/b/bookstore-59884.appspot.com/o/images%2Fcf800646-d011-4a77-9a7e-40bb6dfb32a2.webp?alt=media&token=fcf5aa3f-e5eb-49f0-b37e-4aa1d15ba34a', 1, CAST(N'2024-11-19T22:30:35.140' AS DateTime), CAST(134000.00 AS Decimal(10, 2)), 54, N'Nghệ thuật của cuộc sống hạnh phúc', NULL, NULL, 1)
INSERT [dbo].[Book] ([book_id], [title], [author_name], [image], [type_book_id], [upload_date], [price], [quantity], [description], [rating], [link_ebook], [is_sale]) VALUES (29, N'lmao', N'lmao', N'https://firebasestorage.googleapis.com/v0/b/bookstore-59884.appspot.com/o/images%2F6477c872-9c1f-445e-b464-e2005043635e.webp?alt=media&token=ef98f3d1-bd2b-457b-9edb-60123d7952bf', 1, CAST(N'2024-11-23T13:31:53.333' AS DateTime), CAST(12000.00 AS Decimal(10, 2)), 50, N'lmao', NULL, NULL, 1)
INSERT [dbo].[Book] ([book_id], [title], [author_name], [image], [type_book_id], [upload_date], [price], [quantity], [description], [rating], [link_ebook], [is_sale]) VALUES (30, N'test', N'test', N'https://firebasestorage.googleapis.com/v0/b/bookstore-59884.appspot.com/o/images%2F11b1b0c4-331e-4222-888a-b05b805b6cb3.webp?alt=media&token=a420f47b-2f8b-47f8-ad33-a4d86fd44995', 2, CAST(N'2024-11-23T13:47:45.820' AS DateTime), CAST(12000.00 AS Decimal(10, 2)), 20, N'test', NULL, N'https://firebasestorage.googleapis.com/v0/b/bookstore-59884.appspot.com/o/images%2F1dff3f38-3471-4d38-b8c7-55c5bb13f4cf.pdf?alt=media&token=bb5f75a6-d1a9-44ef-a61c-08ef94e55a05', 1)
SET IDENTITY_INSERT [dbo].[Book] OFF
GO
SET IDENTITY_INSERT [dbo].[BookBrand] ON 

INSERT [dbo].[BookBrand] ([book_brand_id], [book_id], [band_id]) VALUES (1, 1, 43)
INSERT [dbo].[BookBrand] ([book_brand_id], [book_id], [band_id]) VALUES (2, 2, 1)
INSERT [dbo].[BookBrand] ([book_brand_id], [book_id], [band_id]) VALUES (3, 2, 26)
INSERT [dbo].[BookBrand] ([book_brand_id], [book_id], [band_id]) VALUES (4, 3, 5)
INSERT [dbo].[BookBrand] ([book_brand_id], [book_id], [band_id]) VALUES (5, 3, 1)
INSERT [dbo].[BookBrand] ([book_brand_id], [book_id], [band_id]) VALUES (6, 4, 6)
INSERT [dbo].[BookBrand] ([book_brand_id], [book_id], [band_id]) VALUES (7, 4, 7)
INSERT [dbo].[BookBrand] ([book_brand_id], [book_id], [band_id]) VALUES (8, 5, 6)
INSERT [dbo].[BookBrand] ([book_brand_id], [book_id], [band_id]) VALUES (9, 5, 14)
INSERT [dbo].[BookBrand] ([book_brand_id], [book_id], [band_id]) VALUES (10, 6, 20)
INSERT [dbo].[BookBrand] ([book_brand_id], [book_id], [band_id]) VALUES (11, 6, 17)
INSERT [dbo].[BookBrand] ([book_brand_id], [book_id], [band_id]) VALUES (15, 8, 35)
INSERT [dbo].[BookBrand] ([book_brand_id], [book_id], [band_id]) VALUES (16, 9, 4)
INSERT [dbo].[BookBrand] ([book_brand_id], [book_id], [band_id]) VALUES (17, 9, 5)
INSERT [dbo].[BookBrand] ([book_brand_id], [book_id], [band_id]) VALUES (20, 11, 1)
INSERT [dbo].[BookBrand] ([book_brand_id], [book_id], [band_id]) VALUES (21, 11, 25)
INSERT [dbo].[BookBrand] ([book_brand_id], [book_id], [band_id]) VALUES (22, 12, 44)
INSERT [dbo].[BookBrand] ([book_brand_id], [book_id], [band_id]) VALUES (23, 12, 1)
INSERT [dbo].[BookBrand] ([book_brand_id], [book_id], [band_id]) VALUES (24, 13, 25)
INSERT [dbo].[BookBrand] ([book_brand_id], [book_id], [band_id]) VALUES (25, 13, 23)
INSERT [dbo].[BookBrand] ([book_brand_id], [book_id], [band_id]) VALUES (26, 14, 9)
INSERT [dbo].[BookBrand] ([book_brand_id], [book_id], [band_id]) VALUES (32, 16, 17)
INSERT [dbo].[BookBrand] ([book_brand_id], [book_id], [band_id]) VALUES (33, 16, 20)
INSERT [dbo].[BookBrand] ([book_brand_id], [book_id], [band_id]) VALUES (36, 18, 17)
INSERT [dbo].[BookBrand] ([book_brand_id], [book_id], [band_id]) VALUES (37, 18, 20)
INSERT [dbo].[BookBrand] ([book_brand_id], [book_id], [band_id]) VALUES (38, 19, 17)
INSERT [dbo].[BookBrand] ([book_brand_id], [book_id], [band_id]) VALUES (39, 20, 14)
INSERT [dbo].[BookBrand] ([book_brand_id], [book_id], [band_id]) VALUES (40, 20, 17)
INSERT [dbo].[BookBrand] ([book_brand_id], [book_id], [band_id]) VALUES (41, 20, 20)
INSERT [dbo].[BookBrand] ([book_brand_id], [book_id], [band_id]) VALUES (63, 21, 17)
INSERT [dbo].[BookBrand] ([book_brand_id], [book_id], [band_id]) VALUES (64, 21, 20)
INSERT [dbo].[BookBrand] ([book_brand_id], [book_id], [band_id]) VALUES (65, 21, 14)
INSERT [dbo].[BookBrand] ([book_brand_id], [book_id], [band_id]) VALUES (75, 23, 17)
INSERT [dbo].[BookBrand] ([book_brand_id], [book_id], [band_id]) VALUES (80, 15, 14)
INSERT [dbo].[BookBrand] ([book_brand_id], [book_id], [band_id]) VALUES (81, 15, 20)
INSERT [dbo].[BookBrand] ([book_brand_id], [book_id], [band_id]) VALUES (82, 7, 5)
INSERT [dbo].[BookBrand] ([book_brand_id], [book_id], [band_id]) VALUES (83, 7, 20)
INSERT [dbo].[BookBrand] ([book_brand_id], [book_id], [band_id]) VALUES (84, 7, 17)
INSERT [dbo].[BookBrand] ([book_brand_id], [book_id], [band_id]) VALUES (86, 25, 1)
INSERT [dbo].[BookBrand] ([book_brand_id], [book_id], [band_id]) VALUES (87, 24, 45)
INSERT [dbo].[BookBrand] ([book_brand_id], [book_id], [band_id]) VALUES (92, 10, 25)
INSERT [dbo].[BookBrand] ([book_brand_id], [book_id], [band_id]) VALUES (93, 10, 1)
INSERT [dbo].[BookBrand] ([book_brand_id], [book_id], [band_id]) VALUES (106, 22, 1)
INSERT [dbo].[BookBrand] ([book_brand_id], [book_id], [band_id]) VALUES (107, 22, 2)
INSERT [dbo].[BookBrand] ([book_brand_id], [book_id], [band_id]) VALUES (108, 26, 9)
INSERT [dbo].[BookBrand] ([book_brand_id], [book_id], [band_id]) VALUES (109, 26, 15)
INSERT [dbo].[BookBrand] ([book_brand_id], [book_id], [band_id]) VALUES (110, 26, 17)
INSERT [dbo].[BookBrand] ([book_brand_id], [book_id], [band_id]) VALUES (111, 27, 10)
INSERT [dbo].[BookBrand] ([book_brand_id], [book_id], [band_id]) VALUES (112, 28, 5)
INSERT [dbo].[BookBrand] ([book_brand_id], [book_id], [band_id]) VALUES (113, 28, 20)
INSERT [dbo].[BookBrand] ([book_brand_id], [book_id], [band_id]) VALUES (114, 17, 45)
INSERT [dbo].[BookBrand] ([book_brand_id], [book_id], [band_id]) VALUES (115, 29, 1)
INSERT [dbo].[BookBrand] ([book_brand_id], [book_id], [band_id]) VALUES (116, 30, 1)
SET IDENTITY_INSERT [dbo].[BookBrand] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([role_id], [role_name]) VALUES (1, N'admin')
INSERT [dbo].[Role] ([role_id], [role_name]) VALUES (2, N'manager')
INSERT [dbo].[Role] ([role_id], [role_name]) VALUES (3, N'user')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([user_id], [profile_image], [username], [password], [email], [phone], [fullname], [address], [role_id], [is_active]) VALUES (1, N'https://firebasestorage.googleapis.com/v0/b/bookstore-59884.appspot.com/o/images%2Fbf4d9ffc-8333-4a29-b3e5-a06a3b441423.webp?alt=media&token=666b84f9-f767-4d25-9c6d-2377bae8903d', N'lexuanthanh190503', N'$2a$11$oW2BDVKV7WcyBi1oqzc8Ve.1Cy8coP7d8P2JJsxc.OYjSKhjSBDwi', N'lexuanthanh190503@gmail.com', N'0901687315', N'Le Xuan Thanh', N'274 Nguyen Van Tao', 1, 1)
INSERT [dbo].[User] ([user_id], [profile_image], [username], [password], [email], [phone], [fullname], [address], [role_id], [is_active]) VALUES (2, N'https://firebasestorage.googleapis.com/v0/b/bookstore-59884.appspot.com/o/images%2Fc169521a-797d-46e3-96a5-78a4631c7022.png?alt=media&token=1cee3cd6-da87-4a13-905f-cf8a3c61ee06', N'user1', N'$2a$11$x0RgFfn/y/TIAynVGfLfGeYcP9VI9DX9UXxMLgS67gRO6eFIXBCe6', N'canhnguyen123@gmail.com', N'0908765278', N'Đặng Kim Huy', N'Xã Trà Côn, Huyện Trà Ôn, TP.Vĩnh Long', 3, 1)
INSERT [dbo].[User] ([user_id], [profile_image], [username], [password], [email], [phone], [fullname], [address], [role_id], [is_active]) VALUES (3, N'https://firebasestorage.googleapis.com/v0/b/bookstore-59884.appspot.com/o/images%2Fea132ff5-1bc6-44c2-b8e9-14c0c77fc272.png?alt=media&token=715846df-add8-41af-808a-21d7ca152106', N'user2', N'$2a$11$DZzwwcKSmsso1fvyejY.Lediv4mPh3ECH64eGeeCDah9XP.JLijBu', N'lexuanchung2511@gmail.com', N'0908286753', N'Lê Xuân Chung', N'08 Bế Văn Cấm, Phường Tân Kiểng, Q.7, TP.HCM', 3, 1)
INSERT [dbo].[User] ([user_id], [profile_image], [username], [password], [email], [phone], [fullname], [address], [role_id], [is_active]) VALUES (4, N'https://firebasestorage.googleapis.com/v0/b/bookstore-59884.appspot.com/o/images%2Fc16e2860-d165-48c4-8591-cf7402ea8d75.webp?alt=media&token=2bce63c4-d06f-4722-846c-ed58f722d4fd', N'user3', N'$2a$11$r3KfOmkfJj5pE6D3JTYnQeCF51/muaamhGBu8MvYHv7U.0VzW.RzS', N'tuongankk@gmail.com', N'0906574253', N'Nguyễn Tường An', N'12 Công xã Paris, Bến Nghé, Quận 1, Hồ Chí Minh', 3, 1)
INSERT [dbo].[User] ([user_id], [profile_image], [username], [password], [email], [phone], [fullname], [address], [role_id], [is_active]) VALUES (5, N'https://firebasestorage.googleapis.com/v0/b/bookstore-59884.appspot.com/o/images%2F16a5ee0d-7d60-4fb9-a3fb-c2a196ebdd37.webp?alt=media&token=c6a5b42d-5d55-49b0-a41f-c97deff2b9ca', N'user4', N'$2a$11$J.2Uu5yxi70TljLMBbqw8uNqz7eUZOZ0KmrZJYNbH3V.7I1M89Gzu', N'phantibo03@gmail.com', N'0728567435', N'Phan Minh Chí', N'C14/24 Phạm Hùng nối dài, Bình Hưng, Bình Chánh', 3, 1)
INSERT [dbo].[User] ([user_id], [profile_image], [username], [password], [email], [phone], [fullname], [address], [role_id], [is_active]) VALUES (6, N'https://firebasestorage.googleapis.com/v0/b/bookstore-59884.appspot.com/o/images%2F59ac3d55-8226-4678-998e-a1cc0562191b.png?alt=media&token=2c1ff18c-38a7-4a0f-b3e8-c55dcda8f16a', N'admin', N'$2a$11$X1ZHOwm7kN2Dj3viA/7z3uy1bjBLdLPbCmytkI5wwlpqnLD7uI.eW', N'admin@gmail.com', N'string', N'string', N'string', 1, 1)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET IDENTITY_INSERT [dbo].[Voucher] ON 

INSERT [dbo].[Voucher] ([voucher_id], [voucher_code], [release_date], [expired_date], [min_cost], [discount], [quantity]) VALUES (1, N'MNJAJD0I', CAST(N'2024-11-10T00:00:00.000' AS DateTime), CAST(N'2024-11-19T00:00:00.000' AS DateTime), CAST(100000.00 AS Decimal(10, 2)), CAST(1.00 AS Decimal(10, 2)), 100)
INSERT [dbo].[Voucher] ([voucher_id], [voucher_code], [release_date], [expired_date], [min_cost], [discount], [quantity]) VALUES (2, N'F1KJM2TC', CAST(N'2024-11-10T00:00:00.000' AS DateTime), CAST(N'2024-11-19T00:00:00.000' AS DateTime), CAST(100000.00 AS Decimal(10, 2)), CAST(0.40 AS Decimal(10, 2)), 200)
INSERT [dbo].[Voucher] ([voucher_id], [voucher_code], [release_date], [expired_date], [min_cost], [discount], [quantity]) VALUES (3, N'T9NU5VZT', CAST(N'2024-11-10T00:00:00.000' AS DateTime), CAST(N'2024-11-20T00:00:00.000' AS DateTime), CAST(500000.00 AS Decimal(10, 2)), CAST(0.20 AS Decimal(10, 2)), 43)
INSERT [dbo].[Voucher] ([voucher_id], [voucher_code], [release_date], [expired_date], [min_cost], [discount], [quantity]) VALUES (4, N'HBYULFO3', CAST(N'2024-11-15T00:00:00.000' AS DateTime), CAST(N'2024-11-22T00:00:00.000' AS DateTime), CAST(600000.00 AS Decimal(10, 2)), CAST(0.20 AS Decimal(10, 2)), 45)
INSERT [dbo].[Voucher] ([voucher_id], [voucher_code], [release_date], [expired_date], [min_cost], [discount], [quantity]) VALUES (5, N'2LBDKNBX', CAST(N'2024-11-19T00:00:00.000' AS DateTime), CAST(N'2024-11-20T00:00:00.000' AS DateTime), CAST(100000.00 AS Decimal(10, 2)), CAST(0.10 AS Decimal(10, 2)), 25)
SET IDENTITY_INSERT [dbo].[Voucher] OFF
GO
SET IDENTITY_INSERT [dbo].[VoucherUser] ON 

INSERT [dbo].[VoucherUser] ([voucher_user_id], [voucher_id], [user_id], [is_used]) VALUES (1, 3, 3, 0)
SET IDENTITY_INSERT [dbo].[VoucherUser] OFF
GO
SET IDENTITY_INSERT [dbo].[Order] ON 

INSERT [dbo].[Order] ([order_id], [user_id], [order_date], [status], [total_amount], [name], [phone], [address]) VALUES (1, 2, CAST(N'2024-11-10T17:26:17.783' AS DateTime), N'Shipping', CAST(338000.00 AS Decimal(10, 2)), N'Nguyễn Thị Mỹ Dung', N'0901416370', N'274 Nguyễn Văn Tạo')
INSERT [dbo].[Order] ([order_id], [user_id], [order_date], [status], [total_amount], [name], [phone], [address]) VALUES (2, 3, CAST(N'2024-11-10T20:10:34.743' AS DateTime), N'Completed', CAST(376200.00 AS Decimal(10, 2)), N'Lê Xuân Chung', N'0908286753', N'08 Bế Văn Cấm, Phường Tân Kiểng, Q.7, TP.HCM')
INSERT [dbo].[Order] ([order_id], [user_id], [order_date], [status], [total_amount], [name], [phone], [address]) VALUES (3, 4, CAST(N'2024-10-11T11:45:29.613' AS DateTime), N'Canceled', CAST(264000.00 AS Decimal(10, 2)), N'Phạm Tuyết Nhi', N'0906574253', N'01 Công xã Paris, Bến Nghé, Quận 1, Hồ Chí Minh')
INSERT [dbo].[Order] ([order_id], [user_id], [order_date], [status], [total_amount], [name], [phone], [address]) VALUES (4, 4, CAST(N'2024-11-11T12:37:00.253' AS DateTime), N'Completed', CAST(172500.00 AS Decimal(10, 2)), N'Đặng Minh Thắng', N'0901416370', N'274/8 Long thới, Nhà bè')
INSERT [dbo].[Order] ([order_id], [user_id], [order_date], [status], [total_amount], [name], [phone], [address]) VALUES (5, 4, CAST(N'2024-12-11T13:49:48.490' AS DateTime), N'Completed', CAST(950400.00 AS Decimal(10, 2)), N'Phạm Tuyết Nhi', N'0906574253', N'01 Công xã Paris, Bến Nghé, Quận 1, Hồ Chí Minh')
INSERT [dbo].[Order] ([order_id], [user_id], [order_date], [status], [total_amount], [name], [phone], [address]) VALUES (6, 3, CAST(N'2024-10-11T23:29:13.287' AS DateTime), N'Completed', CAST(1443200.00 AS Decimal(10, 2)), N'Bùi Đức Long', N'0907865214', N'5D/8, Nguyễn Duy')
INSERT [dbo].[Order] ([order_id], [user_id], [order_date], [status], [total_amount], [name], [phone], [address]) VALUES (7, 2, CAST(N'2024-09-15T21:58:23.613' AS DateTime), N'Completed', CAST(87000.00 AS Decimal(10, 2)), N'Đinh Văn Nhân', N'0393640357', N'19 Đ. Nguyễn Hữu Thọ, Tân Hưng, Quận 7, Hồ Chí Minh')
INSERT [dbo].[Order] ([order_id], [user_id], [order_date], [status], [total_amount], [name], [phone], [address]) VALUES (8, 2, CAST(N'2024-08-15T22:00:19.550' AS DateTime), N'Completed', CAST(211200.00 AS Decimal(10, 2)), N'Đinh Văn Nhân', N'0393640357', N'19 Đ. Nguyễn Hữu Thọ, Tân Hưng, Quận 7, Hồ Chí Minh')
INSERT [dbo].[Order] ([order_id], [user_id], [order_date], [status], [total_amount], [name], [phone], [address]) VALUES (9, 3, CAST(N'2024-07-15T22:24:20.693' AS DateTime), N'Completed', CAST(234600.00 AS Decimal(10, 2)), N'Trần Minh Quang', N'0394196009', N'469 Đ. Nguyễn Hữu Thọ')
SET IDENTITY_INSERT [dbo].[Order] OFF
GO
SET IDENTITY_INSERT [dbo].[PurchasedEbook] ON 

INSERT [dbo].[PurchasedEbook] ([purchased_ebook_id], [user_id], [book_id], [purchase_date]) VALUES (1, 3, 14, CAST(N'2024-11-11T16:29:13.253' AS DateTime))
INSERT [dbo].[PurchasedEbook] ([purchased_ebook_id], [user_id], [book_id], [purchase_date]) VALUES (2, 3, 19, CAST(N'2024-11-15T15:24:20.673' AS DateTime))
SET IDENTITY_INSERT [dbo].[PurchasedEbook] OFF
GO
SET IDENTITY_INSERT [dbo].[Review] ON 

INSERT [dbo].[Review] ([review_id], [user_id], [book_id], [rating], [comment]) VALUES (1, 3, 19, CAST(4.00 AS Decimal(3, 2)), N'tôi bị hấp dẫn bởi cuốn sách này')
INSERT [dbo].[Review] ([review_id], [user_id], [book_id], [rating], [comment]) VALUES (2, 4, 19, CAST(3.00 AS Decimal(3, 2)), N'Cuốn sách này dạy mình nhiều điều bổ ích lắm đó')
SET IDENTITY_INSERT [dbo].[Review] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderItem] ON 

INSERT [dbo].[OrderItem] ([order_item_id], [order_id], [book_id], [quantity]) VALUES (1, 1, 12, 2)
INSERT [dbo].[OrderItem] ([order_item_id], [order_id], [book_id], [quantity]) VALUES (2, 1, 13, 1)
INSERT [dbo].[OrderItem] ([order_item_id], [order_id], [book_id], [quantity]) VALUES (3, 2, 1, 3)
INSERT [dbo].[OrderItem] ([order_item_id], [order_id], [book_id], [quantity]) VALUES (4, 2, 2, 2)
INSERT [dbo].[OrderItem] ([order_item_id], [order_id], [book_id], [quantity]) VALUES (5, 2, 3, 1)
INSERT [dbo].[OrderItem] ([order_item_id], [order_id], [book_id], [quantity]) VALUES (6, 3, 1, 4)
INSERT [dbo].[OrderItem] ([order_item_id], [order_id], [book_id], [quantity]) VALUES (7, 3, 6, 2)
INSERT [dbo].[OrderItem] ([order_item_id], [order_id], [book_id], [quantity]) VALUES (8, 4, 3, 5)
INSERT [dbo].[OrderItem] ([order_item_id], [order_id], [book_id], [quantity]) VALUES (9, 5, 2, 9)
INSERT [dbo].[OrderItem] ([order_item_id], [order_id], [book_id], [quantity]) VALUES (10, 6, 14, 2)
INSERT [dbo].[OrderItem] ([order_item_id], [order_id], [book_id], [quantity]) VALUES (11, 6, 9, 3)
INSERT [dbo].[OrderItem] ([order_item_id], [order_id], [book_id], [quantity]) VALUES (12, 7, 1, 2)
INSERT [dbo].[OrderItem] ([order_item_id], [order_id], [book_id], [quantity]) VALUES (13, 8, 2, 2)
INSERT [dbo].[OrderItem] ([order_item_id], [order_id], [book_id], [quantity]) VALUES (14, 9, 19, 3)
SET IDENTITY_INSERT [dbo].[OrderItem] OFF
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241102071426_InitialCreate', N'6.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241111030627_BookHub', N'6.0.0')
GO
