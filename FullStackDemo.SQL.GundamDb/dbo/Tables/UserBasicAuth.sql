CREATE TABLE [dbo].[UserBasicAuth]
(
	[Id] INT Identity (1,1) NOT NULL PRIMARY KEY, 
    [Username] VARCHAR(50) NULL, 
    [Password] VARCHAR(150) NULL, 
    [DateCreated] DATETIME2 NULL,
    [CreatedBy] VARCHAR(50) NULL, 
    [DateEdited] DATETIME2 NULL, 
    [EditedBy] VARCHAR(50) NULL, 
    [Deleted] BIT NULL, 
    [DateDeleted] DATETIME2 NULL, 
    [DeletedBy] VARCHAR(50) NULL
)
