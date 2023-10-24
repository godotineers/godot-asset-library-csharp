CREATE TABLE dbo.as_licenses (
    -- Defining the 'Id' column
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,

    -- Defining the 'Name' column
    [Name] NVARCHAR(255) NOT NULL,

    [Tag] NVARCHAR(255) NOT NULL,

    -- Defining the 'Description' column
    [Description] NVARCHAR(MAX) NULL -- or NOT NULL, based on your requirement
);

GO
