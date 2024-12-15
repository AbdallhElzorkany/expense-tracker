using ExpenseTracker;

public class ExpenseFeatures
{
    private List<Expense> expenses = new List<Expense>();
    public void AddExpense(Expense expense)
    {
        expense.Id = expenses[expenses.Count -1].Id + 1;
        expenses.Add(expense);
    }

    public void EditExpense(int id, Expense updatedExpense)
    {
        for (int i = 0; i < expenses.Count; i++)
        {
            if (expenses[i].Id == id)
            {
                updatedExpense.Id = id;
                expenses[i] = updatedExpense;
                break;
            }
        }
    }

    public void DeleteExpense(int id)
    {
        for (int i = 0; i < expenses.Count; i++)
        {
            if (expenses[i].Id == id)
            {
                expenses.RemoveAt(i);
                break;
            }
        }
    }

    public List<Expense> GetExpenses()
    {
        return expenses;
    }

}


