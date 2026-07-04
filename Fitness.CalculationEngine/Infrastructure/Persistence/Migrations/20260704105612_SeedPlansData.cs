using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Fitness.CalculationEngine.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedPlansData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "FitnessPlanConfigs",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "EstimatedDuration", "Goal", "MaxCalorie", "MinCalorie", "Name", "ProgramType", "Status", "UpdatedAt", "UpdatedBy", "WorkoutsPerWeek" },
                values: new object[,]
                {
                    { new Guid("053f6252-a964-411f-a053-64588a629801"), new DateTime(2026, 7, 4, 10, 56, 9, 323, DateTimeKind.Utc).AddTicks(9304), "", "Maximum endurance training with long-distance sessions and high-volume workouts.", "90 minutes", 3, 2500.0, 2200.0, "Advanced Endurance", "High Volume Cardio", 3, null, null, 5 },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "", "A gentle introduction to weight loss with manageable workouts and gradual calorie reduction.", "45 minutes", 1, 1800.0, 1500.0, "Beginner Weight Loss", "Cardio & Light Strength", 1, null, null, 3 },
                    { new Guid("2322d375-7887-4e6d-ab66-716954bdebbb"), new DateTime(2026, 7, 4, 10, 56, 9, 323, DateTimeKind.Utc).AddTicks(9294), "", "Intensive muscle building program with advanced techniques and optimal nutrition timing.", "80 minutes", 4, 3200.0, 2900.0, "Advanced Muscle Gain", "Push/Pull/Legs Split", 3, null, null, 5 },
                    { new Guid("25092462-c3de-4177-81fe-0580e173c4c6"), new DateTime(2026, 7, 4, 10, 56, 9, 323, DateTimeKind.Utc).AddTicks(9311), "", "Increase strength with progressive overload and accessory exercises.", "70 minutes", 2, 2600.0, 2400.0, "Intermediate Strength", "Powerlifting Focus", 2, null, null, 4 },
                    { new Guid("2c19efe0-38fe-463e-a514-a6f3bb1b7302"), new DateTime(2026, 7, 4, 10, 56, 9, 323, DateTimeKind.Utc).AddTicks(9290), "", "Structured strength program with split routines and increased volume.", "65 minutes", 4, 3000.0, 2700.0, "Intermediate Muscle Gain", "Upper/Lower Split", 2, null, null, 4 },
                    { new Guid("3da095c8-f789-4029-af61-0fac5bbc0215"), new DateTime(2026, 7, 4, 10, 56, 9, 323, DateTimeKind.Utc).AddTicks(9307), "", "Develop fundamental strength with basic compound movements and proper technique.", "50 minutes", 2, 2400.0, 2200.0, "Beginner Strength", "Compound Lifts", 1, null, null, 3 },
                    { new Guid("3ecc659f-25ef-4b33-80df-4fd01f061e58"), new DateTime(2026, 7, 4, 10, 56, 9, 323, DateTimeKind.Utc).AddTicks(9297), "", "Build cardiovascular endurance with steady-state cardio and basic conditioning.", "40 minutes", 3, 2200.0, 2000.0, "Beginner Endurance", "Steady State Cardio", 1, null, null, 3 },
                    { new Guid("4e8acb80-daca-4c12-9172-ba4e575bec39"), new DateTime(2026, 7, 4, 10, 56, 9, 323, DateTimeKind.Utc).AddTicks(9175), "", "A gentle introduction to weight loss with manageable workouts and gradual calorie reduction.", "45 minutes", 1, 1800.0, 1500.0, "Beginner Weight Loss", "Cardio & Light Strength", 1, null, null, 3 },
                    { new Guid("73a8486b-2029-43b7-92ab-596d6b1ca588"), new DateTime(2026, 7, 4, 10, 56, 9, 323, DateTimeKind.Utc).AddTicks(9277), "", "Foundation building program focused on proper form and progressive overload.", "50 minutes", 4, 2800.0, 2500.0, "Beginner Muscle Gain", "Full Body Strength", 1, null, null, 3 },
                    { new Guid("8cc6da4d-a98d-4dca-b905-cf06f6a8c60c"), new DateTime(2026, 7, 4, 10, 56, 9, 323, DateTimeKind.Utc).AddTicks(9325), "", "Maximal strength development with periodization and peak performance training.", "90 minutes", 2, 2800.0, 2600.0, "Advanced Strength", "Strength & Power", 3, null, null, 4 },
                    { new Guid("95a68010-8e00-4746-8666-4625c900830f"), new DateTime(2026, 7, 4, 10, 56, 9, 323, DateTimeKind.Utc).AddTicks(9270), "", "Accelerated weight loss plan with increased intensity and stricter calorie management.", "60 minutes", 1, 1700.0, 1400.0, "Intermediate Weight Loss", "HIIT & Strength Training", 2, null, null, 4 },
                    { new Guid("98332ee4-8420-4257-825e-503da0488608"), new DateTime(2026, 7, 4, 10, 56, 9, 323, DateTimeKind.Utc).AddTicks(9274), "", "Intensive weight loss program with high-intensity workouts and aggressive calorie deficit.", "75 minutes", 1, 1600.0, 1300.0, "Advanced Weight Loss", "Advanced HIIT & Compound Exercises", 3, null, null, 5 },
                    { new Guid("fa49f170-2092-4833-a7b8-fe3175ef0a92"), new DateTime(2026, 7, 4, 10, 56, 9, 323, DateTimeKind.Utc).AddTicks(9300), "", "Improve endurance with varied intensity training and longer sessions.", "55 minutes", 3, 2300.0, 2100.0, "Intermediate Endurance", "Interval Training", 2, null, null, 4 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FitnessPlanConfigs",
                keyColumn: "Id",
                keyValue: new Guid("053f6252-a964-411f-a053-64588a629801"));

            migrationBuilder.DeleteData(
                table: "FitnessPlanConfigs",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "FitnessPlanConfigs",
                keyColumn: "Id",
                keyValue: new Guid("2322d375-7887-4e6d-ab66-716954bdebbb"));

            migrationBuilder.DeleteData(
                table: "FitnessPlanConfigs",
                keyColumn: "Id",
                keyValue: new Guid("25092462-c3de-4177-81fe-0580e173c4c6"));

            migrationBuilder.DeleteData(
                table: "FitnessPlanConfigs",
                keyColumn: "Id",
                keyValue: new Guid("2c19efe0-38fe-463e-a514-a6f3bb1b7302"));

            migrationBuilder.DeleteData(
                table: "FitnessPlanConfigs",
                keyColumn: "Id",
                keyValue: new Guid("3da095c8-f789-4029-af61-0fac5bbc0215"));

            migrationBuilder.DeleteData(
                table: "FitnessPlanConfigs",
                keyColumn: "Id",
                keyValue: new Guid("3ecc659f-25ef-4b33-80df-4fd01f061e58"));

            migrationBuilder.DeleteData(
                table: "FitnessPlanConfigs",
                keyColumn: "Id",
                keyValue: new Guid("4e8acb80-daca-4c12-9172-ba4e575bec39"));

            migrationBuilder.DeleteData(
                table: "FitnessPlanConfigs",
                keyColumn: "Id",
                keyValue: new Guid("73a8486b-2029-43b7-92ab-596d6b1ca588"));

            migrationBuilder.DeleteData(
                table: "FitnessPlanConfigs",
                keyColumn: "Id",
                keyValue: new Guid("8cc6da4d-a98d-4dca-b905-cf06f6a8c60c"));

            migrationBuilder.DeleteData(
                table: "FitnessPlanConfigs",
                keyColumn: "Id",
                keyValue: new Guid("95a68010-8e00-4746-8666-4625c900830f"));

            migrationBuilder.DeleteData(
                table: "FitnessPlanConfigs",
                keyColumn: "Id",
                keyValue: new Guid("98332ee4-8420-4257-825e-503da0488608"));

            migrationBuilder.DeleteData(
                table: "FitnessPlanConfigs",
                keyColumn: "Id",
                keyValue: new Guid("fa49f170-2092-4833-a7b8-fe3175ef0a92"));
        }
    }
}
