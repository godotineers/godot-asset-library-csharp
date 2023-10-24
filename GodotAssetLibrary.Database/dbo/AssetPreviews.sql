-- Create the new table
CREATE TABLE dbo.as_asset_previews (
    preview_id int IDENTITY(1,1) NOT NULL, -- Auto-incremental primary key
    asset_id int NOT NULL,
    type varchar(10) NOT NULL, -- Changed from ENUM to VARCHAR; ensure to validate data in application
    link varchar(1024) NOT NULL,
    thumbnail varchar(1024) NOT NULL,
    CONSTRAINT PK_as_asset_previews PRIMARY KEY CLUSTERED (preview_id)  -- Defining the primary key constraint
);
GO
-- Create indices
CREATE INDEX IX_asset_id ON dbo.as_asset_previews (asset_id);
GO

CREATE INDEX IX_type ON dbo.as_asset_previews (type);
