using System;
using System.Collections.Generic;
using System.Linq;

class MenuItem
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Calories { get; set; }
    public int EnergyValue { get; set; }
    public List<string> Allergens { get; set; }
    public List<string> Ingredients { get; set; }

    public MenuItem(string name, decimal price, int calories, int energyValue, List<string> allergens, List<string> ingredients)
    {
        Name = name;
        Price = price;
        Calories = calories;
        EnergyValue = energyValue;
        Allergens = allergens;
        Ingredients = ingredients;
    }
}

class Menu
{
    private List<MenuItem> items;

    public Menu()
    {
        items = new List<MenuItem>
        {
            new MenuItem("Салат", 350m, 150, 200, new List<string> { "орехи" }, new List<string> { "овощи", "салат", "орехи", "масло" }),
            new MenuItem("Бургер", 450m, 500, 600, new List<string> { "глютен", "молочные продукты" }, new List<string> { "булка", "котлета", "сыр", "соус" }),
            new MenuItem("Пицца", 600m, 700, 800, new List<string> { "глютен", "молочные продукты" }, new List<string> { "тесто", "сыр", "томат", "пепперони" }),
            new MenuItem("Паста", 300m, 600, 700, new List<string> { "глютен" }, new List<string> { "макароны", "сливки", "сыр", "специи" }),
            new MenuItem("Суши", 800m, 300, 350, new List<string> { "рыба", "соевый соус" }, new List<string> { "рис", "рыба", "нори", "соус" }),
            new MenuItem("Суп", 250m, 150, 180, new List<string>(), new List<string> { "вода", "овощи", "мясо" }),
            new MenuItem("Стейк", 1200m, 700, 750, new List<string>(), new List<string> { "мясо", "специи", "масло" }),
            new MenuItem("Десерт", 200m, 400, 500, new List<string> { "орехи", "молочные продукты" }, new List<string> { "сахар", "молоко", "шоколад", "орехи" }),
            new MenuItem("Кофе", 150m, 0, 100, new List<string>(), new List<string> { "кофе", "вода" }),
            new MenuItem("Сок", 100m, 50, 60, new List<string>(), new List<string> { "фрукты", "вода" })
        };
    }

    public void ShowMenu(List<MenuItem> menuItems = null)
    {
        Console.WriteLine("\nМеню:");
        Console.WriteLine("---------------------------------------------------------------------------------");
        Console.WriteLine("{0,-15} {1,-10} {2,-10} {3,-15} {4,-20}", "Название", "Цена", "Калории", "Энерг. ценность", "Ингредиенты");
        Console.WriteLine("---------------------------------------------------------------------------------");

        var displayItems = menuItems ?? items;
        foreach (var item in displayItems)
        {
            Console.WriteLine("{0,-15} {1,-10} {2,-10} {3,-15} {4,-20}",
                item.Name,
                item.Price + " руб",
                item.Calories + " ккал",
                item.EnergyValue + " кДж",
                string.Join(", ", item.Ingredients));
        }
    }

    public void FilterByPrice(decimal maxPrice)
    {
        var filteredItems = items.Where(i => i.Price <= maxPrice).ToList();
        Console.WriteLine($"\nБлюда с ценой до {maxPrice} руб:");
        ShowMenu(filteredItems);
    }

    public void FilterByCalories(int maxCalories)
    {
        var filteredItems = items.Where(i => i.Calories <= maxCalories).ToList();
        Console.WriteLine($"\nБлюда с калорийностью до {maxCalories} ккал:");
        ShowMenu(filteredItems);
    }

    public void FilterByAllergen(string allergen)
    {
        var filteredItems = items.Where(i => !i.Allergens.Contains(allergen)).ToList();
        Console.WriteLine($"\nБлюда без аллергена '{allergen}':");
        ShowMenu(filteredItems);
    }

    public void FilterByIngredients(string ingredientsInput)
    {
        var ingredients = ingredientsInput.Split(',').Select(i => i.Trim().ToLower()).ToList();
        var filteredItems = items.Where(item => item.Ingredients.Any(ingredient => ingredients.Contains(ingredient.ToLower()))).ToList();

        Console.WriteLine($"\nБлюда, содержащие один или несколько ингредиентов: {ingredientsInput}");
        ShowMenu(filteredItems);
    }
}

class Program
{
    static void Main()
    {
        Menu menu = new Menu();
        menu.ShowMenu();

        Console.WriteLine("\nВведите максимальную цену (руб):");
        decimal maxPrice = decimal.Parse(Console.ReadLine());
        menu.FilterByPrice(maxPrice);

        Console.WriteLine("\nВведите максимальное количество калорий (ккал):");
        int maxCalories = int.Parse(Console.ReadLine());
        menu.FilterByCalories(maxCalories);

        Console.WriteLine("\nВведите аллерген для исключения (например, 'глютен'):");
        string allergen = Console.ReadLine();
        menu.FilterByAllergen(allergen);

        Console.WriteLine("\nВведите ингредиенты для поиска (например, 'сыр, мясо, кофе'):");
        string ingredients = Console.ReadLine();
        menu.FilterByIngredients(ingredients);
    }
}
