﻿using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using DbUp.Engine;
using DbUp.Engine.Output;
using DbUp.Engine.Transactions;
using DbUp.Support.SqlServer;

namespace DbUp.Support.SQLite
{
    /// <summary>
    /// An implementation of the <see cref="IJournal"/> interface which tracks version numbers for a 
    /// SQLite database using a table called SchemaVersions.
    /// </summary>
    public sealed class SQLiteTableJournal : SqlTableJournal
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SQLiteTableJournal"/> class.
        /// </summary>
        public SQLiteTableJournal(Func<IConnectionManager> connectionManager, Func<IUpgradeLog> logger, string table) : 
            base(connectionManager, logger, null, table)
        {}

        /// <summary>
        /// Create table sql for SQLite
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        protected override string CreateTableSql(string schemaTableName, string tableName)
        {
            return string.Format(
                            @"CREATE TABLE {0} (
	SchemaVersionID INTEGER CONSTRAINT 'PK_{1}_SchemaVersionID' PRIMARY KEY AUTOINCREMENT NOT NULL,
	ScriptName TEXT NOT NULL,
	Applied DATETIME NOT NULL
)", schemaTableName, tableName);
        }

        protected override string DoesTableExistsSql(string tableName)
        {
            return string.Format("select count(*) from {0}", tableName);
        }
    }
}
