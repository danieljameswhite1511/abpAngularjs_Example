namespace abp_angularjs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAssetEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Assets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpTenants", t => t.TenantId, cascadeDelete: true)
                .Index(t => t.TenantId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Assets", "TenantId", "dbo.AbpTenants");
            DropIndex("dbo.Assets", new[] { "TenantId" });
            DropTable("dbo.Assets");
        }
    }
}
