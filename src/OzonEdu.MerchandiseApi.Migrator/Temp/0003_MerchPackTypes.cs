﻿using FluentMigrator;

namespace OzonEdu.MerchandiseApi.Migrator.Temp
{
    [Migration(3)]
    public class MerchPackTypes : Migration
    {
        private const string TableName = "merch_pack_types";
        
        public override void Up()
        {
            Create
                .Table(TableName)
                .WithColumn("id").AsInt32().PrimaryKey()
                .WithColumn("name").AsString().NotNullable();
        }

        public override void Down()
        {
            Delete.Table(TableName);
        }
    }
}