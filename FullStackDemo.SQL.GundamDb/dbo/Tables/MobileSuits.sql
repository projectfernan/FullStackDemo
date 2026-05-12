CREATE TABLE [dbo].[MobileSuits]
(
	[Id] INT IDENTITY (1,1) NOT NULL PRIMARY KEY, 
    [ModelCode] NVARCHAR(50) NULL, 
    [ModelName] VARCHAR(50) NULL, 
    [OperatingSystem] VARCHAR(50) NULL, 
    [PowerOutput] VARCHAR(50) NULL, 
    [Armor] VARCHAR(50) NULL, 
    [Height] VARCHAR(50) NULL, 
    [Weight] VARCHAR(50) NULL, 
    [Manufacturer] VARCHAR(50) NULL, 
    [DateCreated] DATETIME2 NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [DateEdited] DATETIME2 NULL, 
    [EditedBy] VARCHAR(50) NULL, 
    [Deleted] BIT NULL, 
    [DateDeleted] DATETIME2 NULL, 
    [DeletedBy] VARCHAR(50) NULL
)
