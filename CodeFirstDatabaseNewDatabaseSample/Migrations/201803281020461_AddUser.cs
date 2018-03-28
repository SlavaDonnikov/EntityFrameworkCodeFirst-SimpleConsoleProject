namespace CodeFirstDatabaseNewDatabaseSample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tb_User",
                c => new
                    {
                        Username = c.String(nullable: false, maxLength: 128),
                        DisplayName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Username);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tb_User");
        }
    }
}
