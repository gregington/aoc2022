public class Progran {
    public static void Main(string[] args) {
        var caloriesPerElf = CalorieCounter.CountCaloriesPerElf(args[0]);

        Console.WriteLine($"Top Calories: {CalorieCounter.GetMaxCalories(caloriesPerElf)}");
        Console.WriteLine($"Top 3 Elf Calories: {CalorieCounter.GetTopCalories(caloriesPerElf, 3).Sum()}");
    }
}