-- SmartDMS schema + seed data (SQL Server)
-- Run ក្នុង SSMS ឬ Azure Data Studio (បង្កើត database "smartdms" មុនសិន)
IF OBJECT_ID('documents', 'U') IS NULL
BEGIN
    CREATE TABLE documents (
        id            INT IDENTITY(1,1) PRIMARY KEY,
        title         NVARCHAR(200) NOT NULL,
        document_type NVARCHAR(50)  NOT NULL,
        description   NVARCHAR(MAX) NULL,
        created_by    NVARCHAR(100) NOT NULL,
        created_at    DATETIME2     NOT NULL DEFAULT GETDATE(),
        is_archived   BIT           NOT NULL DEFAULT 0
    );

    CREATE INDEX idx_documents_type    ON documents(document_type);
    CREATE INDEX idx_documents_created ON documents(created_at);

    -- Seed data សម្រាប់សាកល្បង
    INSERT INTO documents (title, document_type, description, created_by) VALUES
    ('Invoice #0001',       'INVOICE',  'Monthly service invoice', 'sok'),
    ('Supplier Contract A', 'CONTRACT', '2-year supply agreement', 'dara'),
    ('Q2 Sales Report',     'REPORT',   'Quarterly sales summary', 'sok'),
    ('Office Lease Letter', 'LETTER',   NULL,                      'vanna'),
    ('Invoice #0002',       'INVOICE',  'Hardware purchase',       'dara');
END
