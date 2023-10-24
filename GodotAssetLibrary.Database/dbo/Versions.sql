-- Create the new table
CREATE TABLE dbo.as_versions (
    Id int IDENTITY(1,1) NOT NULL, -- Auto-incremental primary key
    Tag varchar(255) NOT NULL, -- Assuming a maximum of 255 characters for the version title
    CONSTRAINT PK_as_versions PRIMARY KEY CLUSTERED (Id)
);

GO;
