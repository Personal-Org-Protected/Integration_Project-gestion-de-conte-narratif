IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Roles] (
    [IdRole] int NOT NULL IDENTITY,
    [Role] nvarchar(max) NOT NULL,
    [Admin] bit NOT NULL DEFAULT CAST(0 AS bit),
    [Auth0IdReference] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY ([IdRole])
);
GO

CREATE TABLE [Story] (
    [IdStory] int NOT NULL IDENTITY,
    [Chapitre] nvarchar(max) NOT NULL,
    [Fond du Chapitre] nvarchar(max) NOT NULL,
    [Date de Creation] date NOT NULL,
    [Date de Modification] date NOT NULL,
    CONSTRAINT [PK_Story] PRIMARY KEY ([IdStory])
);
GO

CREATE TABLE [Tag] (
    [IdTag] int NOT NULL IDENTITY,
    [Libelle] nvarchar(max) NOT NULL,
    [Nombre de reference] float NOT NULL DEFAULT 0.0E0,
    CONSTRAINT [PK_Tag] PRIMARY KEY ([IdTag])
);
GO

CREATE TABLE [User] (
    [user_id] nvarchar(450) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [UserName] nvarchar(15) NOT NULL,
    [Password] nvarchar(20) NOT NULL,
    [Region] nvarchar(max) NOT NULL,
    [Birth Date] datetime2 NOT NULL,
    [phoneNumber] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NULL,
    CONSTRAINT [PK_User] PRIMARY KEY ([user_id])
);
GO

CREATE TABLE [Zone de Commentaire] (
    [IdZone] int NOT NULL IDENTITY,
    [Actif] bit NOT NULL DEFAULT CAST(0 AS bit),
    CONSTRAINT [PK_Zone de Commentaire] PRIMARY KEY ([IdZone])
);
GO

CREATE TABLE [Forfait] (
    [IdForfait] int NOT NULL IDENTITY,
    [nom de Forfait] nvarchar(max) NOT NULL,
    [valeur de forfait] float NOT NULL,
    [Author] bit NOT NULL,
    [reduction forfait] float NOT NULL DEFAULT 0.0E0,
    [RoleId] int NOT NULL,
    CONSTRAINT [PK_Forfait] PRIMARY KEY ([IdForfait]),
    CONSTRAINT [FK_Forfait_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([IdRole])
);
GO

CREATE TABLE [Bibliotheque] (
    [IdLibrary] nvarchar(450) NOT NULL,
    [Nom de Bibliotheque] nvarchar(max) NOT NULL,
    [Proprietaire] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_Bibliotheque] PRIMARY KEY ([IdLibrary]),
    CONSTRAINT [FK_Bibliotheque_User_Proprietaire] FOREIGN KEY ([Proprietaire]) REFERENCES [User] ([user_id])
);
GO

CREATE TABLE [Image] (
    [IdImage] int NOT NULL IDENTITY,
    [Nom] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [Path] nvarchar(max) NOT NULL,
    [uri] nvarchar(max) NOT NULL,
    [Proprietaire] nvarchar(450) NOT NULL,
    [IdTag] int NOT NULL,
    [Date de Creation] date NOT NULL,
    [Date de Modification] date NOT NULL,
    CONSTRAINT [PK_Image] PRIMARY KEY ([IdImage]),
    CONSTRAINT [FK_Image_User_Proprietaire] FOREIGN KEY ([Proprietaire]) REFERENCES [User] ([user_id]),
    CONSTRAINT [Tag_FK] FOREIGN KEY ([IdTag]) REFERENCES [Tag] ([IdTag]) ON DELETE CASCADE
);
GO

CREATE TABLE [Users_Roles] (
    [user_id] nvarchar(450) NOT NULL,
    [idRole] int NOT NULL,
    CONSTRAINT [PK_Users_Roles] PRIMARY KEY ([user_id], [idRole]),
    CONSTRAINT [FK_Users_Roles_Roles_idRole] FOREIGN KEY ([idRole]) REFERENCES [Roles] ([IdRole]) ON DELETE CASCADE,
    CONSTRAINT [FK_Users_Roles_User_user_id] FOREIGN KEY ([user_id]) REFERENCES [User] ([user_id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Commentaire] (
    [IdCommentaire] int NOT NULL IDENTITY,
    [Proprietaire] nvarchar(max) NOT NULL,
    [Signalement] int NOT NULL,
    [commentaire] nvarchar(max) NOT NULL,
    [Zone de Commenataire] int NOT NULL,
    [Date de creation] datetime2 NOT NULL,
    CONSTRAINT [PK_Commentaire] PRIMARY KEY ([IdCommentaire]),
    CONSTRAINT [Zone Comm] FOREIGN KEY ([Zone de Commenataire]) REFERENCES [Zone de Commentaire] ([IdZone]) ON DELETE CASCADE
);
GO

CREATE TABLE [Histoire] (
    [IdStoryTelling] int NOT NULL IDENTITY,
    [image vignette] nvarchar(max) NULL,
    [Nom Histoire] nvarchar(max) NOT NULL,
    [Proprietaire] nvarchar(450) NOT NULL,
    [Prix] float NOT NULL,
    [Resume] nvarchar(max) NOT NULL,
    [Vendable] bit NOT NULL DEFAULT CAST(0 AS bit),
    [nombre de vente] int NOT NULL DEFAULT 0,
    [idTag] int NULL,
    [Date de creation de l'Histoire] datetime2 NOT NULL,
    [IdZone] int NOT NULL,
    CONSTRAINT [PK_Histoire] PRIMARY KEY ([IdStoryTelling]),
    CONSTRAINT [FK_Histoire_Tag_idTag] FOREIGN KEY ([idTag]) REFERENCES [Tag] ([IdTag]),
    CONSTRAINT [FK_Histoire_User_Proprietaire] FOREIGN KEY ([Proprietaire]) REFERENCES [User] ([user_id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Histoire_Zone de Commentaire_IdZone] FOREIGN KEY ([IdZone]) REFERENCES [Zone de Commentaire] ([IdZone])
);
GO

CREATE TABLE [Forfait_User] (
    [user_id] nvarchar(450) NOT NULL,
    [IdForfait] int NOT NULL,
    CONSTRAINT [PK_Forfait_User] PRIMARY KEY ([user_id], [IdForfait]),
    CONSTRAINT [FK_Forfait_User_Forfait_IdForfait] FOREIGN KEY ([IdForfait]) REFERENCES [Forfait] ([IdForfait]) ON DELETE CASCADE,
    CONSTRAINT [FK_Forfait_User_User_user_id] FOREIGN KEY ([user_id]) REFERENCES [User] ([user_id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Box d'Histoire] (
    [IdBox] int NOT NULL IDENTITY,
    [Dernier chapitre lu] int NOT NULL,
    [IdStoryTell] int NOT NULL,
    [IdLibrary] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_Box d'Histoire] PRIMARY KEY ([IdBox]),
    CONSTRAINT [FK_Box d'Histoire_Histoire_IdStoryTell] FOREIGN KEY ([IdStoryTell]) REFERENCES [Histoire] ([IdStoryTelling]),
    CONSTRAINT [Library_ref] FOREIGN KEY ([IdLibrary]) REFERENCES [Bibliotheque] ([IdLibrary])
);
GO

CREATE TABLE [Chapitre] (
    [IdChapitre] int NOT NULL IDENTITY,
    [Idimage] int NOT NULL,
    [Idstory] int NOT NULL,
    [IdstoryTelling] int NOT NULL,
    [order] int NOT NULL,
    CONSTRAINT [PK_Chapitre] PRIMARY KEY ([IdChapitre]),
    CONSTRAINT [Histoire_FK] FOREIGN KEY ([IdstoryTelling]) REFERENCES [Histoire] ([IdStoryTelling]),
    CONSTRAINT [Image_FK] FOREIGN KEY ([Idimage]) REFERENCES [Image] ([IdImage]),
    CONSTRAINT [Story_FK] FOREIGN KEY ([Idstory]) REFERENCES [Story] ([IdStory]) ON DELETE CASCADE
);
GO

CREATE TABLE [Idees] (
    [IdIdee] int NOT NULL IDENTITY,
    [Idea] nvarchar(max) NOT NULL,
    [IdStoryTell] int NOT NULL,
    CONSTRAINT [PK_Idees] PRIMARY KEY ([IdIdee]),
    CONSTRAINT [History_FK] FOREIGN KEY ([IdStoryTell]) REFERENCES [Histoire] ([IdStoryTelling]) ON DELETE CASCADE
);
GO

CREATE TABLE [Transactions] (
    [TransactionId] int NOT NULL IDENTITY,
    [Nom du livre] nvarchar(max) NOT NULL,
    [price] float NOT NULL,
    [Date de la transaction] datetime2 NOT NULL,
    [IdStory] int NOT NULL,
    [user_id] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_Transactions] PRIMARY KEY ([TransactionId]),
    CONSTRAINT [FK_Transactions_Histoire_IdStory] FOREIGN KEY ([IdStory]) REFERENCES [Histoire] ([IdStoryTelling]),
    CONSTRAINT [FK_Transactions_User_user_id] FOREIGN KEY ([user_id]) REFERENCES [User] ([user_id])
);
GO

CREATE UNIQUE INDEX [IX_Bibliotheque_Proprietaire] ON [Bibliotheque] ([Proprietaire]);
GO

CREATE INDEX [IX_Box d'Histoire_IdLibrary] ON [Box d'Histoire] ([IdLibrary]);
GO

CREATE INDEX [IX_Box d'Histoire_IdStoryTell] ON [Box d'Histoire] ([IdStoryTell]);
GO

CREATE INDEX [IX_Chapitre_Idimage] ON [Chapitre] ([Idimage]);
GO

CREATE UNIQUE INDEX [IX_Chapitre_Idstory] ON [Chapitre] ([Idstory]);
GO

CREATE INDEX [IX_Chapitre_IdstoryTelling] ON [Chapitre] ([IdstoryTelling]);
GO

CREATE INDEX [IX_Commentaire_Zone de Commenataire] ON [Commentaire] ([Zone de Commenataire]);
GO

CREATE INDEX [IX_Forfait_RoleId] ON [Forfait] ([RoleId]);
GO

CREATE INDEX [IX_Forfait_User_IdForfait] ON [Forfait_User] ([IdForfait]);
GO

CREATE INDEX [IX_Histoire_idTag] ON [Histoire] ([idTag]);
GO

CREATE UNIQUE INDEX [IX_Histoire_IdZone] ON [Histoire] ([IdZone]);
GO

CREATE INDEX [IX_Histoire_Proprietaire] ON [Histoire] ([Proprietaire]);
GO

CREATE INDEX [IX_Idees_IdStoryTell] ON [Idees] ([IdStoryTell]);
GO

CREATE INDEX [IX_Image_IdTag] ON [Image] ([IdTag]);
GO

CREATE INDEX [IX_Image_Proprietaire] ON [Image] ([Proprietaire]);
GO

CREATE INDEX [IX_Transactions_IdStory] ON [Transactions] ([IdStory]);
GO

CREATE INDEX [IX_Transactions_user_id] ON [Transactions] ([user_id]);
GO

CREATE INDEX [IX_Users_Roles_idRole] ON [Users_Roles] ([idRole]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221217151609_InitializeDatabase', N'8.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[User]') AND [c].[name] = N'Birth Date');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [User] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [User] ALTER COLUMN [Birth Date] datetime2(0) NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221218110956_NewUpdates1_0', N'8.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[User]') AND [c].[name] = N'Birth Date');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [User] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [User] ALTER COLUMN [Birth Date] date NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221218111311_NewUpdates1_1', N'8.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Histoire]') AND [c].[name] = N'Date de creation de l''Histoire');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Histoire] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Histoire] ALTER COLUMN [Date de creation de l'Histoire] date NOT NULL;
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Commentaire]') AND [c].[name] = N'Date de creation');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Commentaire] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Commentaire] ALTER COLUMN [Date de creation] date NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221218111508_NewUpdates1_2', N'8.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Commentaire]') AND [c].[name] = N'Signalement');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Commentaire] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Commentaire] ADD DEFAULT 0 FOR [Signalement];
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Commentaire]') AND [c].[name] = N'Date de creation');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Commentaire] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [Commentaire] ALTER COLUMN [Date de creation] SMALLDATETIME NOT NULL;
GO

ALTER TABLE [Commentaire] ADD [Like] int NULL DEFAULT 0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221220152238_NewUpdates1_3', N'8.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [User] ADD [avatar] nvarchar(max) NULL;
GO

ALTER TABLE [Histoire] ADD [Popularite] float NULL;
GO

CREATE TABLE [Notification] (
    [idNotification] int NOT NULL IDENTITY,
    [title] nvarchar(max) NOT NULL,
    [message] nvarchar(max) NOT NULL,
    [date de notif] date NOT NULL,
    [target] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_Notification] PRIMARY KEY ([idNotification]),
    CONSTRAINT [FK_Notification_User_target] FOREIGN KEY ([target]) REFERENCES [User] ([user_id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Panier] (
    [basket_id] nvarchar(450) NOT NULL,
    [Proprietaire] nvarchar(450) NOT NULL,
    [Panier vide] bit NOT NULL DEFAULT CAST(0 AS bit),
    CONSTRAINT [PK_Panier] PRIMARY KEY ([basket_id]),
    CONSTRAINT [FK_Panier_User_Proprietaire] FOREIGN KEY ([Proprietaire]) REFERENCES [User] ([user_id])
);
GO

CREATE TABLE [BasketItems] (
    [basket_id] nvarchar(450) NOT NULL,
    [item_id] int NOT NULL,
    [StoryTellingIdStoryTelling] int NOT NULL,
    CONSTRAINT [PK_BasketItems] PRIMARY KEY ([item_id], [basket_id]),
    CONSTRAINT [FK_BasketItems_Histoire_StoryTellingIdStoryTelling] FOREIGN KEY ([StoryTellingIdStoryTelling]) REFERENCES [Histoire] ([IdStoryTelling]) ON DELETE CASCADE,
    CONSTRAINT [FK_BasketItems_Panier_basket_id] FOREIGN KEY ([basket_id]) REFERENCES [Panier] ([basket_id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_BasketItems_basket_id] ON [BasketItems] ([basket_id]);
GO

CREATE INDEX [IX_BasketItems_StoryTellingIdStoryTelling] ON [BasketItems] ([StoryTellingIdStoryTelling]);
GO

CREATE INDEX [IX_Notification_target] ON [Notification] ([target]);
GO

CREATE UNIQUE INDEX [IX_Panier_Proprietaire] ON [Panier] ([Proprietaire]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230302155848_NewUpdates1_4', N'8.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Notification] ADD [Lu] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230303182438_NewUpdates1_5', N'8.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Notes] (
    [user_id] nvarchar(450) NOT NULL,
    [storyTellId] int NOT NULL,
    CONSTRAINT [PK_Notes] PRIMARY KEY ([user_id], [storyTellId]),
    CONSTRAINT [FK_Notes_Histoire_storyTellId] FOREIGN KEY ([storyTellId]) REFERENCES [Histoire] ([IdStoryTelling]) ON DELETE CASCADE,
    CONSTRAINT [FK_Notes_User_user_id] FOREIGN KEY ([user_id]) REFERENCES [User] ([user_id])
);
GO

CREATE INDEX [IX_Notes_storyTellId] ON [Notes] ([storyTellId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230311100232_NewUpdates1_6', N'8.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Notes] ADD [note] int NOT NULL DEFAULT 0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230311112645_NewUpdates1_7', N'8.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [BasketItems] DROP CONSTRAINT [FK_BasketItems_Histoire_StoryTellingIdStoryTelling];
GO

ALTER TABLE [BasketItems] DROP CONSTRAINT [PK_BasketItems];
GO

DROP INDEX [IX_BasketItems_StoryTellingIdStoryTelling] ON [BasketItems];
GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[BasketItems]') AND [c].[name] = N'item_id');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [BasketItems] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [BasketItems] DROP COLUMN [item_id];
GO

EXEC sp_rename N'[BasketItems].[StoryTellingIdStoryTelling]', N'IdStoryTelling', N'COLUMN';
GO

ALTER TABLE [BasketItems] ADD CONSTRAINT [PK_BasketItems] PRIMARY KEY ([IdStoryTelling], [basket_id]);
GO

ALTER TABLE [BasketItems] ADD CONSTRAINT [FK_BasketItems_Histoire_IdStoryTelling] FOREIGN KEY ([IdStoryTelling]) REFERENCES [Histoire] ([IdStoryTelling]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230317235246_Newupdates1_8', N'8.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DROP TABLE [Forfait_User];
GO

DROP TABLE [Users_Roles];
GO

DROP TABLE [Forfait];
GO

DROP TABLE [Roles];
GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[User]') AND [c].[name] = N'Birth Date');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [User] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [User] DROP COLUMN [Birth Date];
GO

DECLARE @var8 sysname;
SELECT @var8 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[User]') AND [c].[name] = N'Description');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [User] DROP CONSTRAINT [' + @var8 + '];');
ALTER TABLE [User] DROP COLUMN [Description];
GO

DECLARE @var9 sysname;
SELECT @var9 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[User]') AND [c].[name] = N'Password');
IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [User] DROP CONSTRAINT [' + @var9 + '];');
ALTER TABLE [User] DROP COLUMN [Password];
GO

DECLARE @var10 sysname;
SELECT @var10 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[User]') AND [c].[name] = N'Region');
IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [User] DROP CONSTRAINT [' + @var10 + '];');
ALTER TABLE [User] DROP COLUMN [Region];
GO

DECLARE @var11 sysname;
SELECT @var11 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[User]') AND [c].[name] = N'phoneNumber');
IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [User] DROP CONSTRAINT [' + @var11 + '];');
ALTER TABLE [User] DROP COLUMN [phoneNumber];
GO

DECLARE @var12 sysname;
SELECT @var12 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Histoire]') AND [c].[name] = N'Prix');
IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [Histoire] DROP CONSTRAINT [' + @var12 + '];');
ALTER TABLE [Histoire] DROP COLUMN [Prix];
GO

ALTER TABLE [Histoire] ADD [nombre de signalement] int NOT NULL DEFAULT 0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240715205042_StoryTelling_2.1', N'8.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

EXEC sp_rename N'[User].[avatar]', N'Profile Image', N'COLUMN';
GO

ALTER TABLE [User] ADD [Azure Id] nvarchar(max) NOT NULL DEFAULT N'';
GO

ALTER TABLE [User] ADD [Role of user] nvarchar(max) NOT NULL DEFAULT N'member';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240722115155_change_user_entity', N'8.0.7');
GO

COMMIT;
GO

