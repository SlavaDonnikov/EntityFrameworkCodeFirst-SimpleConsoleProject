namespace CodeFirstDatabaseNewDatabaseSample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDisplayName : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Tb_User", name: "DisplayName", newName: "display_name");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.Tb_User", name: "display_name", newName: "DisplayName");
        }
    }
}
