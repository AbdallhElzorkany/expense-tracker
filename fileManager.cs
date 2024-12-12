using ExpenseTracker;
using System.Text.Json;

public class FileManager
{
    private const string FilePath = "expenses.json";

    public void SaveToJson(List<Expense> expenses)
    {
        string json = JsonSerializer.Serialize(expenses, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(FilePath, json);
    }

    public List<Expense> LoadFromJson()
    {
        if (File.Exists(FilePath))
        {
            string json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<Expense>>(json) ?? new List<Expense>();
        }
        return new List<Expense>();
    }
}