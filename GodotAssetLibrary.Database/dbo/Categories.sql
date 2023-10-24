-- Create the new table
CREATE TABLE dbo.as_categories (
    category_id int IDENTITY(1,1) NOT NULL, -- Auto-incremental primary key
    category varchar(25) NOT NULL,
    category_type tinyint NOT NULL,
    CONSTRAINT PK_as_categories PRIMARY KEY CLUSTERED (category_id)  -- Defining the primary key constraint
);
GO
-- Create unique index for 'category' to ensure no duplicates
CREATE UNIQUE INDEX UX_category ON dbo.as_categories (category);
