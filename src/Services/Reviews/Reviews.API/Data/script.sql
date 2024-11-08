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

CREATE TABLE [Products] (
    [Id] int NOT NULL,
    [DeletedAt] datetimeoffset NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Users] (
    [Id] int NOT NULL,
    [DisplayName] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Reviews] (
    [Id] int NOT NULL IDENTITY,
    [ProductId] int NOT NULL,
    [UserId] int NOT NULL,
    [Rating] int NOT NULL,
    [Text] nvarchar(1000) NOT NULL,
    [CreatedAt] datetimeoffset NOT NULL,
    [ModifiedAt] datetimeoffset NOT NULL,
    [IsApproved] bit NOT NULL,
    CONSTRAINT [PK_Reviews] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Reviews_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Reviews_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE UNIQUE INDEX [IX_Reviews_ProductId] ON [Reviews] ([ProductId]);
GO

CREATE UNIQUE INDEX [IX_Reviews_UserId] ON [Reviews] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241102192249_InitialReviews', N'8.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DROP INDEX [IX_Reviews_ProductId] ON [Reviews];
GO

DROP INDEX [IX_Reviews_UserId] ON [Reviews];
GO

CREATE INDEX [IX_Reviews_ProductId] ON [Reviews] ([ProductId]);
GO

CREATE INDEX [IX_Reviews_UserId] ON [Reviews] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241102194138_InitialReviewsV2', N'8.0.10');
GO

COMMIT;
GO

