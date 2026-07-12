using Fitness.Nutrition.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Nutrition.Infrastructure.Persistence.Contexts;

public class NutritionDbContext : DbContext
{
    public NutritionDbContext(DbContextOptions<NutritionDbContext> options)
        : base(options)
    {
    }

    public DbSet<MealPlan> MealPlans => Set<MealPlan>();
    public DbSet<Meal> Meals => Set<Meal>();
    public DbSet<NutritionFacts> NutritionFacts => Set<NutritionFacts>();
    public DbSet<Ingredient> Ingredients => Set<Ingredient>();
    public DbSet<MealIngredient> MealIngredients => Set<MealIngredient>();
    public DbSet<MealPlanItem> MealPlanItems => Set<MealPlanItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NutritionDbContext).Assembly);
        SeedData(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        // Generate all GUIDs first
        var mealPlan1Id = Guid.Parse("12345678-1234-1234-1234-123456789abc");
        var mealPlan2Id = Guid.Parse("23456789-2345-2345-2345-23456789abcd");
        var mealPlan3Id = Guid.Parse("3456789a-3456-3456-3456-3456789abcde");

        var meal1Id = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var meal2Id = Guid.Parse("22222222-2222-2222-2222-222222222222");
        var meal3Id = Guid.Parse("33333333-3333-3333-3333-333333333333");
        var meal4Id = Guid.Parse("44444444-4444-4444-4444-444444444444");
        var meal5Id = Guid.Parse("55555555-5555-5555-5555-555555555555");
        var meal6Id = Guid.Parse("66666666-6666-6666-6666-666666666666");

        var ingredient1Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
        var ingredient2Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
        var ingredient3Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc");
        var ingredient4Id = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd");
        var ingredient5Id = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee");
        var ingredient6Id = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff");
        var ingredient7Id = Guid.Parse("12345678-1234-1234-1234-123456789aaa");
        var ingredient8Id = Guid.Parse("23456789-2345-2345-2345-23456789bbbb");
        var ingredient9Id = Guid.Parse("3456789a-3456-3456-3456-3456789acccc");
        var ingredient10Id = Guid.Parse("456789ab-4567-4567-4567-456789abdddd");

        var now = DateTime.UtcNow;
        var systemUser = "System";

        // STEP 1: Seed Ingredients (no foreign key dependencies)
        modelBuilder.Entity<Ingredient>().HasData(
            new Ingredient { Id = ingredient1Id, Name = "Chicken Breast", CreatedAt = now, CreatedBy = systemUser },
            new Ingredient { Id = ingredient2Id, Name = "Brown Rice", CreatedAt = now, CreatedBy = systemUser },
            new Ingredient { Id = ingredient3Id, Name = "Broccoli", CreatedAt = now, CreatedBy = systemUser },
            new Ingredient { Id = ingredient4Id, Name = "Salmon", CreatedAt = now, CreatedBy = systemUser },
            new Ingredient { Id = ingredient5Id, Name = "Quinoa", CreatedAt = now, CreatedBy = systemUser },
            new Ingredient { Id = ingredient6Id, Name = "Spinach", CreatedAt = now, CreatedBy = systemUser },
            new Ingredient { Id = ingredient7Id, Name = "Eggs", CreatedAt = now, CreatedBy = systemUser },
            new Ingredient { Id = ingredient8Id, Name = "Oats", CreatedAt = now, CreatedBy = systemUser },
            new Ingredient { Id = ingredient9Id, Name = "Greek Yogurt", CreatedAt = now, CreatedBy = systemUser },
            new Ingredient { Id = ingredient10Id, Name = "Avocado", CreatedAt = now, CreatedBy = systemUser }
        );

        // STEP 2: Seed Meals (depends on nothing)
        modelBuilder.Entity<Meal>().HasData(
            new Meal
            {
                Id = meal1Id,
                Name = "Grilled Chicken with Brown Rice",
                Type = "Lunch",
                Calories = 350,
                Protein = 32,
                Carbs = 25,
                Fats = 12,
                PrepTimeInMinutes = 25,
                Difficulty = "Easy",
                ImageUrl = "https://example.com/images/chicken-rice.jpg",
                IngredientsJson = "[\"2 Chicken Breasts\", \"1 cup Brown Rice\", \"1 cup Broccoli\"]",
                InstructionsJson = "[\"Season chicken with salt and pepper\", \"Grill chicken for 6-8 minutes per side\", \"Cook rice according to package directions\", \"Steam broccoli until tender\"]",
                VariationsJson = "[\"Use quinoa instead of rice\", \"Add bell peppers\"]",
                AllergensJson = "[\"None\"]",
                TagsJson = "[\"High Protein\", \"Low Fat\", \"Healthy\"]",
                CreatedAt = now,
                CreatedBy = systemUser
            },
            new Meal
            {
                Id = meal2Id,
                Name = "Salmon Quinoa Bowl",
                Type = "Dinner",
                Calories = 420,
                Protein = 28,
                Carbs = 45,
                Fats = 14,
                PrepTimeInMinutes = 30,
                Difficulty = "Medium",
                ImageUrl = "https://example.com/images/salmon-quinoa.jpg",
                IngredientsJson = "[\"2 Salmon Fillets\", \"1 cup Quinoa\", \"2 cups Spinach\", \"1 Avocado\"]",
                InstructionsJson = "[\"Season salmon with lemon and herbs\", \"Bake at 375°F for 15-20 minutes\", \"Cook quinoa as directed\", \"Serve over spinach with sliced avocado\"]",
                VariationsJson = "[\"Add roasted vegetables\", \"Use wild rice instead of quinoa\"]",
                AllergensJson = "[\"Fish\"]",
                TagsJson = "[\"Omega-3\", \"Heart Healthy\", \"Gluten-Free\"]",
                CreatedAt = now,
                CreatedBy = systemUser
            },
            new Meal
            {
                Id = meal3Id,
                Name = "Egg and Oat Breakfast Bowl",
                Type = "Breakfast",
                Calories = 480,
                Protein = 35,
                Carbs = 30,
                Fats = 22,
                PrepTimeInMinutes = 15,
                Difficulty = "Easy",
                ImageUrl = "https://example.com/images/egg-oat-bowl.jpg",
                IngredientsJson = "[\"3 Eggs\", \"1/2 cup Oats\", \"1/2 cup Greek Yogurt\"]",
                InstructionsJson = "[\"Cook oats with water or milk\", \"Scramble eggs separately\", \"Combine in bowl\", \"Top with Greek yogurt\"]",
                VariationsJson = "[\"Add berries\", \"Use almond milk\"]",
                AllergensJson = "[\"Eggs\", \"Dairy\"]",
                TagsJson = "[\"High Protein\", \"Breakfast\", \"Quick\"]",
                CreatedAt = now,
                CreatedBy = systemUser
            },
            new Meal
            {
                Id = meal4Id,
                Name = "Chicken and Vegetable Stir-fry",
                Type = "Dinner",
                Calories = 300,
                Protein = 25,
                Carbs = 20,
                Fats = 10,
                PrepTimeInMinutes = 20,
                Difficulty = "Medium",
                ImageUrl = "https://example.com/images/stir-fry.jpg",
                IngredientsJson = "[\"2 Chicken Breasts\", \"2 cups Mixed Vegetables\", \"2 tbsp Soy Sauce\"]",
                InstructionsJson = "[\"Cut chicken into strips\", \"Stir-fry chicken in hot pan\", \"Add vegetables and cook until tender\", \"Add soy sauce and season\"]",
                VariationsJson = "[\"Use tofu instead of chicken\", \"Add ginger and garlic\"]",
                AllergensJson = "[\"Soy\"]",
                TagsJson = "[\"Quick\", \"Healthy\", \"Low Calorie\"]",
                CreatedAt = now,
                CreatedBy = systemUser
            },
            new Meal
            {
                Id = meal5Id,
                Name = "Greek Yogurt Parfait",
                Type = "Snack",
                Calories = 280,
                Protein = 18,
                Carbs = 35,
                Fats = 8,
                PrepTimeInMinutes = 5,
                Difficulty = "Easy",
                ImageUrl = "https://example.com/images/parfait.jpg",
                IngredientsJson = "[\"1 cup Greek Yogurt\", \"1/2 cup Berries\", \"1/4 cup Granola\"]",
                InstructionsJson = "[\"Layer yogurt in a glass\", \"Add berries\", \"Top with granola\"]",
                VariationsJson = "[\"Use different fruits\", \"Add honey\"]",
                AllergensJson = "[\"Dairy\"]",
                TagsJson = "[\"Quick\", \"Healthy\", \"Snack\"]",
                CreatedAt = now,
                CreatedBy = systemUser
            },
            new Meal
            {
                Id = meal6Id,
                Name = "Avocado Salmon Salad",
                Type = "Lunch",
                Calories = 550,
                Protein = 40,
                Carbs = 15,
                Fats = 35,
                PrepTimeInMinutes = 15,
                Difficulty = "Easy",
                ImageUrl = "https://example.com/images/salmon-salad.jpg",
                IngredientsJson = "[\"2 Salmon Fillets\", \"1 Avocado\", \"2 cups Spinach\"]",
                InstructionsJson = "[\"Bake or pan-sear salmon\", \"Slice avocado\", \"Arrange over spinach\", \"Add light dressing\"]",
                VariationsJson = "[\"Add nuts\", \"Use mixed greens\"]",
                AllergensJson = "[\"Fish\"]",
                TagsJson = "[\"Keto\", \"High Protein\", \"Healthy Fats\"]",
                CreatedAt = now,
                CreatedBy = systemUser
            }
        );

        // STEP 3: Seed NutritionFacts (depends on Meals)
        modelBuilder.Entity<NutritionFacts>().HasData(
            new NutritionFacts
            {
                Id = Guid.Parse("11111111-1111-1111-1111-1111111111a1"),
                Calories = 350,
                Protein = 32,
                Carbs = 25,
                Fats = 12,
                Fiber = 4,
                MealId = meal1Id,
                CreatedAt = now,
                CreatedBy = systemUser
            },
            new NutritionFacts
            {
                Id = Guid.Parse("22222222-2222-2222-2222-2222222222b2"),
                Calories = 420,
                Protein = 28,
                Carbs = 45,
                Fats = 14,
                Fiber = 6,
                MealId = meal2Id,
                CreatedAt = now,
                CreatedBy = systemUser
            },
            new NutritionFacts
            {
                Id = Guid.Parse("33333333-3333-3333-3333-3333333333c3"),
                Calories = 480,
                Protein = 35,
                Carbs = 30,
                Fats = 22,
                Fiber = 3,
                MealId = meal3Id,
                CreatedAt = now,
                CreatedBy = systemUser
            },
            new NutritionFacts
            {
                Id = Guid.Parse("44444444-4444-4444-4444-4444444444d4"),
                Calories = 300,
                Protein = 25,
                Carbs = 20,
                Fats = 10,
                Fiber = 5,
                MealId = meal4Id,
                CreatedAt = now,
                CreatedBy = systemUser
            },
            new NutritionFacts
            {
                Id = Guid.Parse("55555555-5555-5555-5555-5555555555e5"),
                Calories = 280,
                Protein = 18,
                Carbs = 35,
                Fats = 8,
                Fiber = 7,
                MealId = meal5Id,
                CreatedAt = now,
                CreatedBy = systemUser
            },
            new NutritionFacts
            {
                Id = Guid.Parse("66666666-6666-6666-6666-6666666666f6"),
                Calories = 550,
                Protein = 40,
                Carbs = 15,
                Fats = 35,
                Fiber = 8,
                MealId = meal6Id,
                CreatedAt = now,
                CreatedBy = systemUser
            }
        );

        // STEP 4: Seed MealIngredients (depends on Meals and Ingredients)
        modelBuilder.Entity<MealIngredient>().HasData(
            // Meal 1 ingredients
            new MealIngredient
            {
                Id = Guid.Parse("a1b2c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d"),
                MealId = meal1Id,
                IngredientId = ingredient1Id,
                Amount = "2 breasts",
                CreatedAt = now,
                CreatedBy = systemUser
            },
            new MealIngredient
            {
                Id = Guid.Parse("b2c3d4e5-f6a7-4b8c-9d0e-1f2a3b4c5d6e"),
                MealId = meal1Id,
                IngredientId = ingredient2Id,
                Amount = "1 cup",
                CreatedAt = now,
                CreatedBy = systemUser
            },
            new MealIngredient
            {
                Id = Guid.Parse("c3d4e5f6-a7b8-4c9d-0e1f-2a3b4c5d6e7f"),
                MealId = meal1Id,
                IngredientId = ingredient3Id,
                Amount = "1 cup",
                CreatedAt = now,
                CreatedBy = systemUser
            },

            // Meal 2 ingredients
            new MealIngredient
            {
                Id = Guid.Parse("d4e5f6a7-b8c9-4d0e-1f2a-3b4c5d6e7f8a"),
                MealId = meal2Id,
                IngredientId = ingredient4Id,
                Amount = "2 fillets",
                CreatedAt = now,
                CreatedBy = systemUser
            },
            new MealIngredient
            {
                Id = Guid.Parse("e5f6a7b8-c9d0-4e1f-2a3b-4c5d6e7f8a9b"),
                MealId = meal2Id,
                IngredientId = ingredient5Id,
                Amount = "1 cup",
                CreatedAt = now,
                CreatedBy = systemUser
            },
            new MealIngredient
            {
                Id = Guid.Parse("f6a7b8c9-d0e1-4f2a-3b4c-5d6e7f8a9b0c"),
                MealId = meal2Id,
                IngredientId = ingredient6Id,
                Amount = "2 cups",
                CreatedAt = now,
                CreatedBy = systemUser
            },
            new MealIngredient
            {
                Id = Guid.Parse("a7b8c9d0-e1f2-4a3b-4c5d-6e7f8a9b0c1d"),
                MealId = meal2Id,
                IngredientId = ingredient10Id,
                Amount = "1",
                CreatedAt = now,
                CreatedBy = systemUser
            },

            // Meal 3 ingredients
            new MealIngredient
            {
                Id = Guid.Parse("b8c9d0e1-f2a3-4b4c-5d6e-7f8a9b0c1d2e"),
                MealId = meal3Id,
                IngredientId = ingredient7Id,
                Amount = "3",
                CreatedAt = now,
                CreatedBy = systemUser
            },
            new MealIngredient
            {
                Id = Guid.Parse("c9d0e1f2-a3b4-4c5d-6e7f-8a9b0c1d2e3f"),
                MealId = meal3Id,
                IngredientId = ingredient8Id,
                Amount = "1/2 cup",
                CreatedAt = now,
                CreatedBy = systemUser
            },
            new MealIngredient
            {
                Id = Guid.Parse("d0e1f2a3-b4c5-4d6e-7f8a-9b0c1d2e3f4a"),
                MealId = meal3Id,
                IngredientId = ingredient9Id,
                Amount = "1/2 cup",
                CreatedAt = now,
                CreatedBy = systemUser
            },

            // Meal 4 ingredients
            new MealIngredient
            {
                Id = Guid.Parse("e1f2a3b4-c5d6-4e7f-8a9b-0c1d2e3f4a5b"),
                MealId = meal4Id,
                IngredientId = ingredient1Id,
                Amount = "2 breasts",
                CreatedAt = now,
                CreatedBy = systemUser
            },
            new MealIngredient
            {
                Id = Guid.Parse("f2a3b4c5-d6e7-4f8a-9b0c-1d2e3f4a5b6c"),
                MealId = meal4Id,
                IngredientId = ingredient3Id,
                Amount = "2 cups",
                CreatedAt = now,
                CreatedBy = systemUser
            },

            // Meal 5 ingredients
            new MealIngredient
            {
                Id = Guid.Parse("a3b4c5d6-e7f8-4a9b-0c1d-2e3f4a5b6c7d"),
                MealId = meal5Id,
                IngredientId = ingredient9Id,
                Amount = "1 cup",
                CreatedAt = now,
                CreatedBy = systemUser
            },

            // Meal 6 ingredients
            new MealIngredient
            {
                Id = Guid.Parse("b4c5d6e7-f8a9-4b0c-1d2e-3f4a5b6c7d8e"),
                MealId = meal6Id,
                IngredientId = ingredient4Id,
                Amount = "2 fillets",
                CreatedAt = now,
                CreatedBy = systemUser
            },
            new MealIngredient
            {
                Id = Guid.Parse("c5d6e7f8-a9b0-4c1d-2e3f-4a5b6c7d8e9f"),
                MealId = meal6Id,
                IngredientId = ingredient10Id,
                Amount = "1",
                CreatedAt = now,
                CreatedBy = systemUser
            },
            new MealIngredient
            {
                Id = Guid.Parse("d6e7f8a9-b0c1-4d2e-3f4a-5b6c7d8e9f0a"),
                MealId = meal6Id,
                IngredientId = ingredient6Id,
                Amount = "2 cups",
                CreatedAt = now,
                CreatedBy = systemUser
            }
        );

        // STEP 5: Seed MealPlans (depends on nothing)
        modelBuilder.Entity<MealPlan>().HasData(
            new MealPlan
            {
                Id = mealPlan1Id,
                Name = "Weekly Weight Loss Plan",
                Description = "A balanced meal plan designed for steady weight loss with high protein and moderate carbs.",
                TargetCalorieRangeMin = 1500,
                TargetCalorieRangeMax = 1800,
                CreatedAt = now,
                CreatedBy = systemUser
            },
            new MealPlan
            {
                Id = mealPlan2Id,
                Name = "Muscle Building Plan",
                Description = "High protein meal plan for muscle growth and recovery.",
                TargetCalorieRangeMin = 2500,
                TargetCalorieRangeMax = 2800,
                CreatedAt = now,
                CreatedBy = systemUser
            },
            new MealPlan
            {
                Id = mealPlan3Id,
                Name = "Keto Diet Plan",
                Description = "Low carb, high fat meal plan for ketosis.",
                TargetCalorieRangeMin = 2000,
                TargetCalorieRangeMax = 2200,
                CreatedAt = now,
                CreatedBy = systemUser
            }
        );

        // STEP 6: Seed MealPlanItems (depends on MealPlans and Meals)
        modelBuilder.Entity<MealPlanItem>().HasData(
            // Meal Plan 1 - Weight Loss
            new MealPlanItem
            {
                Id = Guid.Parse("e7f8a9b0-c1d2-4e3f-4a5b-6c7d8e9f0a1b"),
                MealPlanId = mealPlan1Id,
                MealId = meal3Id,
                DayOfWeek = "Monday",
                MealTime = "Breakfast",
                CreatedAt = now,
                CreatedBy = systemUser
            },
            new MealPlanItem
            {
                Id = Guid.Parse("f8a9b0c1-d2e3-4f4a-5b6c-7d8e9f0a1b2c"),
                MealPlanId = mealPlan1Id,
                MealId = meal1Id,
                DayOfWeek = "Monday",
                MealTime = "Lunch",
                CreatedAt = now,
                CreatedBy = systemUser
            },
            new MealPlanItem
            {
                Id = Guid.Parse("a9b0c1d2-e3f4-4a5b-6c7d-8e9f0a1b2c3d"),
                MealPlanId = mealPlan1Id,
                MealId = meal4Id,
                DayOfWeek = "Monday",
                MealTime = "Dinner",
                CreatedAt = now,
                CreatedBy = systemUser
            },
            new MealPlanItem
            {
                Id = Guid.Parse("b0c1d2e3-f4a5-4b6c-7d8e-9f0a1b2c3d4e"),
                MealPlanId = mealPlan1Id,
                MealId = meal5Id,
                DayOfWeek = "Tuesday",
                MealTime = "Breakfast",
                CreatedAt = now,
                CreatedBy = systemUser
            },
            new MealPlanItem
            {
                Id = Guid.Parse("c1d2e3f4-a5b6-4c7d-8e9f-0a1b2c3d4e5f"),
                MealPlanId = mealPlan1Id,
                MealId = meal2Id,
                DayOfWeek = "Tuesday",
                MealTime = "Lunch",
                CreatedAt = now,
                CreatedBy = systemUser
            },
            new MealPlanItem
            {
                Id = Guid.Parse("d2e3f4a5-b6c7-4d8e-9f0a-1b2c3d4e5f6a"),
                MealPlanId = mealPlan1Id,
                MealId = meal6Id,
                DayOfWeek = "Tuesday",
                MealTime = "Dinner",
                CreatedAt = now,
                CreatedBy = systemUser
            },

            // Meal Plan 2 - Muscle Building
            new MealPlanItem
            {
                Id = Guid.Parse("e3f4a5b6-c7d8-4e9f-0a1b-2c3d4e5f6a7b"),
                MealPlanId = mealPlan2Id,
                MealId = meal3Id,
                DayOfWeek = "Monday",
                MealTime = "Breakfast",
                CreatedAt = now,
                CreatedBy = systemUser
            },
            new MealPlanItem
            {
                Id = Guid.Parse("f4a5b6c7-d8e9-4f0a-1b2c-3d4e5f6a7b8c"),
                MealPlanId = mealPlan2Id,
                MealId = meal1Id,
                DayOfWeek = "Monday",
                MealTime = "Lunch",
                CreatedAt = now,
                CreatedBy = systemUser
            },
            new MealPlanItem
            {
                Id = Guid.Parse("a5b6c7d8-e9f0-4a1b-2c3d-4e5f6a7b8c9d"),
                MealPlanId = mealPlan2Id,
                MealId = meal2Id,
                DayOfWeek = "Monday",
                MealTime = "Dinner",
                CreatedAt = now,
                CreatedBy = systemUser
            },
            new MealPlanItem
            {
                Id = Guid.Parse("b6c7d8e9-f0a1-4b2c-3d4e-5f6a7b8c9d0e"),
                MealPlanId = mealPlan2Id,
                MealId = meal6Id,
                DayOfWeek = "Tuesday",
                MealTime = "Lunch",
                CreatedAt = now,
                CreatedBy = systemUser
            },

            // Meal Plan 3 - Keto
            new MealPlanItem
            {
                Id = Guid.Parse("c7d8e9f0-a1b2-4c3d-4e5f-6a7b8c9d0e1f"),
                MealPlanId = mealPlan3Id,
                MealId = meal6Id,
                DayOfWeek = "Monday",
                MealTime = "Breakfast",
                CreatedAt = now,
                CreatedBy = systemUser
            },
            new MealPlanItem
            {
                Id = Guid.Parse("d8e9f0a1-b2c3-4d4e-5f6a-7b8c9d0e1f2a"),
                MealPlanId = mealPlan3Id,
                MealId = meal6Id,
                DayOfWeek = "Monday",
                MealTime = "Lunch",
                CreatedAt = now,
                CreatedBy = systemUser
            },
            new MealPlanItem
            {
                Id = Guid.Parse("e9f0a1b2-c3d4-4e5f-6a7b-8c9d0e1f2a3b"),
                MealPlanId = mealPlan3Id,
                MealId = meal2Id,
                DayOfWeek = "Monday",
                MealTime = "Dinner",
                CreatedAt = now,
                CreatedBy = systemUser
            }
        );
    }
}
