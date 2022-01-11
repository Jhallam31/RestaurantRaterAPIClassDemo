namespace RestaurantRaterAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedRatings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RestaurantId = c.Int(nullable: false),
                        FoodScore = c.Double(nullable: false),
                        CleanlinessScore = c.Double(nullable: false),
                        EnvironmentScore = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Restaurants", t => t.RestaurantId, cascadeDelete: true)
                .Index(t => t.RestaurantId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ratings", "RestaurantId", "dbo.Restaurants");
            DropIndex("dbo.Ratings", new[] { "RestaurantId" });
            DropTable("dbo.Ratings");
        }
    }
}
