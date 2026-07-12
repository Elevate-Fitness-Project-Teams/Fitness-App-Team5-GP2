using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Fitness.Nutrition.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MealPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TargetCalorieRangeMin = table.Column<double>(type: "float", nullable: false),
                    TargetCalorieRangeMax = table.Column<double>(type: "float", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Meals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Calories = table.Column<double>(type: "float", nullable: false),
                    Protein = table.Column<double>(type: "float", nullable: false),
                    Carbs = table.Column<double>(type: "float", nullable: false),
                    Fats = table.Column<double>(type: "float", nullable: false),
                    PrepTimeInMinutes = table.Column<int>(type: "int", nullable: false),
                    Difficulty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IngredientsJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstructionsJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VariationsJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AllergensJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TagsJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MealIngredients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MealId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IngredientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealIngredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MealIngredients_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealIngredients_Meals_MealId",
                        column: x => x.MealId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MealPlanItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MealPlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MealId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DayOfWeek = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MealTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealPlanItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MealPlanItems_MealPlans_MealPlanId",
                        column: x => x.MealPlanId,
                        principalTable: "MealPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealPlanItems_Meals_MealId",
                        column: x => x.MealId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NutritionFacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Calories = table.Column<int>(type: "int", nullable: false),
                    Protein = table.Column<double>(type: "float", nullable: false),
                    Carbs = table.Column<double>(type: "float", nullable: false),
                    Fats = table.Column<double>(type: "float", nullable: false),
                    Fiber = table.Column<double>(type: "float", nullable: false),
                    MealId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NutritionFacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NutritionFacts_Meals_MealId",
                        column: x => x.MealId,
                        principalTable: "Meals",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("12345678-1234-1234-1234-123456789aaa"), new DateTime(2026, 7, 11, 14, 54, 7, 368, DateTimeKind.Utc).AddTicks(9122), "System", "Eggs", null, null },
                    { new Guid("23456789-2345-2345-2345-23456789bbbb"), new DateTime(2026, 7, 11, 14, 54, 7, 368, DateTimeKind.Utc).AddTicks(9122), "System", "Oats", null, null },
                    { new Guid("3456789a-3456-3456-3456-3456789acccc"), new DateTime(2026, 7, 11, 14, 54, 7, 368, DateTimeKind.Utc).AddTicks(9122), "System", "Greek Yogurt", null, null },
                    { new Guid("456789ab-4567-4567-4567-456789abdddd"), new DateTime(2026, 7, 11, 14, 54, 7, 368, DateTimeKind.Utc).AddTicks(9122), "System", "Avocado", null, null },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateTime(2026, 7, 11, 14, 54, 7, 368, DateTimeKind.Utc).AddTicks(9122), "System", "Chicken Breast", null, null },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new DateTime(2026, 7, 11, 14, 54, 7, 368, DateTimeKind.Utc).AddTicks(9122), "System", "Brown Rice", null, null },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new DateTime(2026, 7, 11, 14, 54, 7, 368, DateTimeKind.Utc).AddTicks(9122), "System", "Broccoli", null, null },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new DateTime(2026, 7, 11, 14, 54, 7, 368, DateTimeKind.Utc).AddTicks(9122), "System", "Salmon", null, null },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), new DateTime(2026, 7, 11, 14, 54, 7, 368, DateTimeKind.Utc).AddTicks(9122), "System", "Quinoa", null, null },
                    { new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"), new DateTime(2026, 7, 11, 14, 54, 7, 368, DateTimeKind.Utc).AddTicks(9122), "System", "Spinach", null, null }
                });

            migrationBuilder.InsertData(
                table: "MealPlans",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "Name", "TargetCalorieRangeMax", "TargetCalorieRangeMin", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("12345678-1234-1234-1234-123456789abc"), new DateTime(2026, 7, 11, 14, 54, 7, 368, DateTimeKind.Utc).AddTicks(9122), "System", "A balanced meal plan designed for steady weight loss with high protein and moderate carbs.", "Weekly Weight Loss Plan", 1800.0, 1500.0, null, null },
                    { new Guid("23456789-2345-2345-2345-23456789abcd"), new DateTime(2026, 7, 11, 14, 54, 7, 368, DateTimeKind.Utc).AddTicks(9122), "System", "High protein meal plan for muscle growth and recovery.", "Muscle Building Plan", 2800.0, 2500.0, null, null },
                    { new Guid("3456789a-3456-3456-3456-3456789abcde"), new DateTime(2026, 7, 11, 14, 54, 7, 368, DateTimeKind.Utc).AddTicks(9122), "System", "Low carb, high fat meal plan for ketosis.", "Keto Diet Plan", 2200.0, 2000.0, null, null }
                });

            migrationBuilder.InsertData(
                table: "Meals",
                columns: new[] { "Id", "AllergensJson", "Calories", "Carbs", "CreatedAt", "CreatedBy", "Difficulty", "Fats", "ImageUrl", "IngredientsJson", "InstructionsJson", "Name", "PrepTimeInMinutes", "Protein", "TagsJson", "Type", "UpdatedAt", "UpdatedBy", "VariationsJson" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "[\"None\"]", 350.0, 25.0, new DateTime(2026, 7, 11, 14, 54, 7, 368, DateTimeKind.Utc).AddTicks(9122), "System", "Easy", 12.0, "https://example.com/images/chicken-rice.jpg", "[\"2 Chicken Breasts\", \"1 cup Brown Rice\", \"1 cup Broccoli\"]", "[\"Season chicken with salt and pepper\", \"Grill chicken for 6-8 minutes per side\", \"Cook rice according to package directions\", \"Steam broccoli until tender\"]", "Grilled Chicken with Brown Rice", 25, 32.0, "[\"High Protein\", \"Low Fat\", \"Healthy\"]", "Lunch", null, null, "[\"Use quinoa instead of rice\", \"Add bell peppers\"]" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "[\"Fish\"]", 420.0, 45.0, new DateTime(2026, 7, 11, 14, 54, 7, 368, DateTimeKind.Utc).AddTicks(9122), "System", "Medium", 14.0, "https://example.com/images/salmon-quinoa.jpg", "[\"2 Salmon Fillets\", \"1 cup Quinoa\", \"2 cups Spinach\", \"1 Avocado\"]", "[\"Season salmon with lemon and herbs\", \"Bake at 375°F for 15-20 minutes\", \"Cook quinoa as directed\", \"Serve over spinach with sliced avocado\"]", "Salmon Quinoa Bowl", 30, 28.0, "[\"Omega-3\", \"Heart Healthy\", \"Gluten-Free\"]", "Dinner", null, null, "[\"Add roasted vegetables\", \"Use wild rice instead of quinoa\"]" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "[\"Eggs\", \"Dairy\"]", 480.0, 30.0, new DateTime(2026, 7, 11, 14, 54, 7, 368, DateTimeKind.Utc).AddTicks(9122), "System", "Easy", 22.0, "https://example.com/images/egg-oat-bowl.jpg", "[\"3 Eggs\", \"1/2 cup Oats\", \"1/2 cup Greek Yogurt\"]", "[\"Cook oats with water or milk\", \"Scramble eggs separately\", \"Combine in bowl\", \"Top with Greek yogurt\"]", "Egg and Oat Breakfast Bowl", 15, 35.0, "[\"High Protein\", \"Breakfast\", \"Quick\"]", "Breakfast", null, null, "[\"Add berries\", \"Use almond milk\"]" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "[\"Soy\"]", 300.0, 20.0, new DateTime(2026, 7, 11, 14, 54, 7, 368, DateTimeKind.Utc).AddTicks(9122), "System", "Medium", 10.0, "https://example.com/images/stir-fry.jpg", "[\"2 Chicken Breasts\", \"2 cups Mixed Vegetables\", \"2 tbsp Soy Sauce\"]", "[\"Cut chicken into strips\", \"Stir-fry chicken in hot pan\", \"Add vegetables and cook until tender\", \"Add soy sauce and season\"]", "Chicken and Vegetable Stir-fry", 20, 25.0, "[\"Quick\", \"Healthy\", \"Low Calorie\"]", "Dinner", null, null, "[\"Use tofu instead of chicken\", \"Add ginger and garlic\"]" },
                    { new Guid("55555555-5555-5555-5555-555555555555"), "[\"Dairy\"]", 280.0, 35.0, new DateTime(2026, 7, 11, 14, 54, 7, 368, DateTimeKind.Utc).AddTicks(9122), "System", "Easy", 8.0, "https://example.com/images/parfait.jpg", "[\"1 cup Greek Yogurt\", \"1/2 cup Berries\", \"1/4 cup Granola\"]", "[\"Layer yogurt in a glass\", \"Add berries\", \"Top with granola\"]", "Greek Yogurt Parfait", 5, 18.0, "[\"Quick\", \"Healthy\", \"Snack\"]", "Snack", null, null, "[\"Use different fruits\", \"Add honey\"]" },
                    { new Guid("66666666-6666-6666-6666-666666666666"), "[\"Fish\"]", 550.0, 15.0, new DateTime(2026, 7, 11, 14, 54, 7, 368, DateTimeKind.Utc).AddTicks(9122), "System", "Easy", 35.0, "https://example.com/images/salmon-salad.jpg", "[\"2 Salmon Fillets\", \"1 Avocado\", \"2 cups Spinach\"]", "[\"Bake or pan-sear salmon\", \"Slice avocado\", \"Arrange over spinach\", \"Add light dressing\"]", "Avocado Salmon Salad", 15, 40.0, "[\"Keto\", \"High Protein\", \"Healthy Fats\"]", "Lunch", null, null, "[\"Add nuts\", \"Use mixed greens\"]" }
                });

            migrationBuilder.InsertData(
                table: "MealIngredients",
                columns: new[] { "Id", "Amount", "CreatedAt", "CreatedBy", "IngredientId", "MealId", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d"), "2 breasts", new DateTime(2026, 7, 11, 14, 54, 7, 368, DateTimeKind.Utc).AddTicks(9122), "System", new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new Guid("11111111-1111-1111-1111-111111111111"), null, null },
                    { new Guid("a3b4c5d6-e7f8-4a9b-0c1d-2e3f4a5b6c7d"), "1 cup", new DateTime(2026, 7, 11, 14, 54, 7, 368, DateTimeKind.Utc).AddTicks(9122), "System", new Guid("3456789a-3456-3456-3456-3456789acccc"), new Guid("55555555-5555-5555-5555-555555555555"), null, null },
                    { new Guid("a7b8c9d0-e1f2-4a3b-4c5d-6e7f8a9b0c1d"), "1", new DateTime(2026, 7, 11, 14, 54, 7, 368, DateTimeKind.Utc).AddTicks(9122), "System", new Guid("456789ab-4567-4567-4567-456789abdddd"), new Guid("22222222-2222-2222-2222-222222222222"), null, null },
                    { new Guid("b2c3d4e5-f6a7-4b8c-9d0e-1f2a3b4c5d6e"), "1 cup", new DateTime(2026, 7, 11, 14, 54, 7, 368, DateTimeKind.Utc).AddTicks(9122), "System", new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new Guid("11111111-1111-1111-1111-111111111111"), null, null },
                    { new Guid("b4c5d6e7-f8a9-4b0c-1d2e-3f4a5b6c7d8e"), "2 fillets", new DateTime(2026, 7, 11, 14, 54, 7, 368, DateTimeKind.Utc).AddTicks(9122), "System", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new Guid("66666666-6666-6666-6666-666666666666"), null, null },
                    { new Guid("b8c9d0e1-f2a3-4b4c-5d6e-7f8a9b0c1d2e"), "3", new DateTime(2026, 7, 11, 14, 54, 7, 368, DateTimeKind.Utc).AddTicks(9122), "System", new Guid("12345678-1234-1234-1234-123456789aaa"), new Guid("33333333-3333-3333-3333-333333333333"), null, null },
                    { new Guid("c3d4e5f6-a7b8-4c9d-0e1f-2a3b4c5d6e7f"), "1 cup", new DateTime(2026, 7, 11, 14, 54, 7, 368, DateTimeKind.Utc).AddTicks(9122), "System", new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new Guid("11111111-1111-1111-1111-111111111111"), null, null },
                    { new Guid("c5d6e7f8-a9b0-4c1d-2e3f-4a5b6c7d8e9f"), "1", new DateTime(2026, 7, 11, 14, 54, 7, 368, DateTimeKind.Utc).AddTicks(9122), "System", new Guid("456789ab-4567-4567-4567-456789abdddd"), new Guid("66666666-6666-6666-6666-666666666666"), null, null },
                    { new Guid("c9d0e1f2-a3b4-4c5d-6e7f-8a9b0c1d2e3f"), "1/2 cup", new DateTime(2026, 7, 11, 14, 54, 7, 368, DateTimeKind.Utc).AddTicks(9122), "System", new Guid("23456789-2345-2345-2345-23456789bbbb"), new Guid("33333333-3333-3333-3333-333333333333"), null, null },
                    { new Guid("d0e1f2a3-b4c5-4d6e-7f8a-9b0c1d2e3f4a"), "1/2 cup", new DateTime(2026, 7, 11, 14, 54, 7, 368, DateTimeKind.Utc).AddTicks(9122), "System", new Guid("3456789a-3456-3456-3456-3456789acccc"), new Guid("33333333-3333-3333-3333-333333333333"), null, null },
                    { new Guid("d4e5f6a7-b8c9-4d0e-1f2a-3b4c5d6e7f8a"), "2 fillets", new DateTime(2026, 7, 11, 14, 54, 7, 368, DateTimeKind.Utc).AddTicks(9122), "System", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new Guid("22222222-2222-2222-2222-222222222222"), null, null },
                    { new Guid("d6e7f8a9-b0c1-4d2e-3f4a-5b6c7d8e9f0a"), "2 cups", new DateTime(2026, 7, 11, 14, 54, 7, 368, DateTimeKind.Utc).AddTicks(9122), "System", new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"), new Guid("66666666-6666-6666-6666-666666666666"), null, null },
                    { new Guid("e1f2a3b4-c5d6-4e7f-8a9b-0c1d2e3f4a5b"), "2 breasts", new DateTime(2026, 7, 11, 14, 54, 7, 368, DateTimeKind.Utc).AddTicks(9122), "System", new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new Guid("44444444-4444-4444-4444-444444444444"), null, null },
                    { new Guid("e5f6a7b8-c9d0-4e1f-2a3b-4c5d6e7f8a9b"), "1 cup", new DateTime(2026, 7, 11, 14, 54, 7, 368, DateTimeKind.Utc).AddTicks(9122), "System", new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), new Guid("22222222-2222-2222-2222-222222222222"), null, null },
                    { new Guid("f2a3b4c5-d6e7-4f8a-9b0c-1d2e3f4a5b6c"), "2 cups", new DateTime(2026, 7, 11, 14, 54, 7, 368, DateTimeKind.Utc).AddTicks(9122), "System", new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new Guid("44444444-4444-4444-4444-444444444444"), null, null },
                    { new Guid("f6a7b8c9-d0e1-4f2a-3b4c-5d6e7f8a9b0c"), "2 cups", new DateTime(2026, 7, 11, 14, 54, 7, 368, DateTimeKind.Utc).AddTicks(9122), "System", new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"), new Guid("22222222-2222-2222-2222-222222222222"), null, null }
                });

            migrationBuilder.InsertData(
                table: "MealPlanItems",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DayOfWeek", "MealId", "MealPlanId", "MealTime", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("a5b6c7d8-e9f0-4a1b-2c3d-4e5f6a7b8c9d"), new DateTime(2026, 7, 11, 14, 54, 7, 368, DateTimeKind.Utc).AddTicks(9122), "System", "Monday", new Guid("22222222-2222-2222-2222-222222222222"), new Guid("23456789-2345-2345-2345-23456789abcd"), "Dinner", null, null },
                    { new Guid("a9b0c1d2-e3f4-4a5b-6c7d-8e9f0a1b2c3d"), new DateTime(2026, 7, 11, 14, 54, 7, 368, DateTimeKind.Utc).AddTicks(9122), "System", "Monday", new Guid("44444444-4444-4444-4444-444444444444"), new Guid("12345678-1234-1234-1234-123456789abc"), "Dinner", null, null },
                    { new Guid("b0c1d2e3-f4a5-4b6c-7d8e-9f0a1b2c3d4e"), new DateTime(2026, 7, 11, 14, 54, 7, 368, DateTimeKind.Utc).AddTicks(9122), "System", "Tuesday", new Guid("55555555-5555-5555-5555-555555555555"), new Guid("12345678-1234-1234-1234-123456789abc"), "Breakfast", null, null },
                    { new Guid("b6c7d8e9-f0a1-4b2c-3d4e-5f6a7b8c9d0e"), new DateTime(2026, 7, 11, 14, 54, 7, 368, DateTimeKind.Utc).AddTicks(9122), "System", "Tuesday", new Guid("66666666-6666-6666-6666-666666666666"), new Guid("23456789-2345-2345-2345-23456789abcd"), "Lunch", null, null },
                    { new Guid("c1d2e3f4-a5b6-4c7d-8e9f-0a1b2c3d4e5f"), new DateTime(2026, 7, 11, 14, 54, 7, 368, DateTimeKind.Utc).AddTicks(9122), "System", "Tuesday", new Guid("22222222-2222-2222-2222-222222222222"), new Guid("12345678-1234-1234-1234-123456789abc"), "Lunch", null, null },
                    { new Guid("c7d8e9f0-a1b2-4c3d-4e5f-6a7b8c9d0e1f"), new DateTime(2026, 7, 11, 14, 54, 7, 368, DateTimeKind.Utc).AddTicks(9122), "System", "Monday", new Guid("66666666-6666-6666-6666-666666666666"), new Guid("3456789a-3456-3456-3456-3456789abcde"), "Breakfast", null, null },
                    { new Guid("d2e3f4a5-b6c7-4d8e-9f0a-1b2c3d4e5f6a"), new DateTime(2026, 7, 11, 14, 54, 7, 368, DateTimeKind.Utc).AddTicks(9122), "System", "Tuesday", new Guid("66666666-6666-6666-6666-666666666666"), new Guid("12345678-1234-1234-1234-123456789abc"), "Dinner", null, null },
                    { new Guid("d8e9f0a1-b2c3-4d4e-5f6a-7b8c9d0e1f2a"), new DateTime(2026, 7, 11, 14, 54, 7, 368, DateTimeKind.Utc).AddTicks(9122), "System", "Monday", new Guid("66666666-6666-6666-6666-666666666666"), new Guid("3456789a-3456-3456-3456-3456789abcde"), "Lunch", null, null },
                    { new Guid("e3f4a5b6-c7d8-4e9f-0a1b-2c3d4e5f6a7b"), new DateTime(2026, 7, 11, 14, 54, 7, 368, DateTimeKind.Utc).AddTicks(9122), "System", "Monday", new Guid("33333333-3333-3333-3333-333333333333"), new Guid("23456789-2345-2345-2345-23456789abcd"), "Breakfast", null, null },
                    { new Guid("e7f8a9b0-c1d2-4e3f-4a5b-6c7d8e9f0a1b"), new DateTime(2026, 7, 11, 14, 54, 7, 368, DateTimeKind.Utc).AddTicks(9122), "System", "Monday", new Guid("33333333-3333-3333-3333-333333333333"), new Guid("12345678-1234-1234-1234-123456789abc"), "Breakfast", null, null },
                    { new Guid("e9f0a1b2-c3d4-4e5f-6a7b-8c9d0e1f2a3b"), new DateTime(2026, 7, 11, 14, 54, 7, 368, DateTimeKind.Utc).AddTicks(9122), "System", "Monday", new Guid("22222222-2222-2222-2222-222222222222"), new Guid("3456789a-3456-3456-3456-3456789abcde"), "Dinner", null, null },
                    { new Guid("f4a5b6c7-d8e9-4f0a-1b2c-3d4e5f6a7b8c"), new DateTime(2026, 7, 11, 14, 54, 7, 368, DateTimeKind.Utc).AddTicks(9122), "System", "Monday", new Guid("11111111-1111-1111-1111-111111111111"), new Guid("23456789-2345-2345-2345-23456789abcd"), "Lunch", null, null },
                    { new Guid("f8a9b0c1-d2e3-4f4a-5b6c-7d8e9f0a1b2c"), new DateTime(2026, 7, 11, 14, 54, 7, 368, DateTimeKind.Utc).AddTicks(9122), "System", "Monday", new Guid("11111111-1111-1111-1111-111111111111"), new Guid("12345678-1234-1234-1234-123456789abc"), "Lunch", null, null }
                });

            migrationBuilder.InsertData(
                table: "NutritionFacts",
                columns: new[] { "Id", "Calories", "Carbs", "CreatedAt", "CreatedBy", "Fats", "Fiber", "MealId", "Protein", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-1111111111a1"), 350, 25.0, new DateTime(2026, 7, 11, 14, 54, 7, 368, DateTimeKind.Utc).AddTicks(9122), "System", 12.0, 4.0, new Guid("11111111-1111-1111-1111-111111111111"), 32.0, null, null },
                    { new Guid("22222222-2222-2222-2222-2222222222b2"), 420, 45.0, new DateTime(2026, 7, 11, 14, 54, 7, 368, DateTimeKind.Utc).AddTicks(9122), "System", 14.0, 6.0, new Guid("22222222-2222-2222-2222-222222222222"), 28.0, null, null },
                    { new Guid("33333333-3333-3333-3333-3333333333c3"), 480, 30.0, new DateTime(2026, 7, 11, 14, 54, 7, 368, DateTimeKind.Utc).AddTicks(9122), "System", 22.0, 3.0, new Guid("33333333-3333-3333-3333-333333333333"), 35.0, null, null },
                    { new Guid("44444444-4444-4444-4444-4444444444d4"), 300, 20.0, new DateTime(2026, 7, 11, 14, 54, 7, 368, DateTimeKind.Utc).AddTicks(9122), "System", 10.0, 5.0, new Guid("44444444-4444-4444-4444-444444444444"), 25.0, null, null },
                    { new Guid("55555555-5555-5555-5555-5555555555e5"), 280, 35.0, new DateTime(2026, 7, 11, 14, 54, 7, 368, DateTimeKind.Utc).AddTicks(9122), "System", 8.0, 7.0, new Guid("55555555-5555-5555-5555-555555555555"), 18.0, null, null },
                    { new Guid("66666666-6666-6666-6666-6666666666f6"), 550, 15.0, new DateTime(2026, 7, 11, 14, 54, 7, 368, DateTimeKind.Utc).AddTicks(9122), "System", 35.0, 8.0, new Guid("66666666-6666-6666-6666-666666666666"), 40.0, null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MealIngredients_IngredientId",
                table: "MealIngredients",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_MealIngredients_MealId",
                table: "MealIngredients",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_MealPlanItems_MealId",
                table: "MealPlanItems",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_MealPlanItems_MealPlanId",
                table: "MealPlanItems",
                column: "MealPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_NutritionFacts_MealId",
                table: "NutritionFacts",
                column: "MealId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MealIngredients");

            migrationBuilder.DropTable(
                name: "MealPlanItems");

            migrationBuilder.DropTable(
                name: "NutritionFacts");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "MealPlans");

            migrationBuilder.DropTable(
                name: "Meals");
        }
    }
}
