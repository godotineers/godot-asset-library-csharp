-- Create the new table
CREATE TABLE dbo.as_asset_edit_previews (
    edit_preview_id int IDENTITY(1,1) NOT NULL, -- Auto-incremental primary key
    edit_id int NOT NULL,
    preview_id int NOT NULL,
    type varchar(10) NULL, -- Changed from ENUM to VARCHAR; ensure to validate data in application
    link varchar(1024) NULL, -- DEFAULT is not included since NULL is the default
    thumbnail varchar(1024) NULL, -- DEFAULT is not included since NULL is the default
    operation tinyint NOT NULL, -- Ensure this is managed correctly as it's no longer an ENUM
    CONSTRAINT PK_as_asset_edit_previews PRIMARY KEY CLUSTERED (edit_preview_id)  -- Defining the primary key constraint
);
GO
-- Create indices
CREATE INDEX IX_edit_id ON dbo.as_asset_edit_previews (edit_id);
GO
CREATE INDEX IX_type ON dbo.as_asset_edit_previews (type);
GO
CREATE INDEX IX_preview_id ON dbo.as_asset_edit_previews (preview_id);
