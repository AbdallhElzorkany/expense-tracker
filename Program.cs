using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ExpenseTracker
{
    public class Expense
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
    }

    class Program
    {
        private static ExpenseFeatures expenseManager = new ExpenseFeatures();
        private static FileManager fileManager = new FileManager();

        static void Main()
        {
            expenseManager.GetExpenses().AddRange(fileManager.LoadFromJson());
            try
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Expense Tracker");
                    Console.WriteLine("1. Add Expense");
                    Console.WriteLine("2. View Expenses");
                    Console.WriteLine("3. Edit Expense");
                    Console.WriteLine("4. Delete Expense");
                    Console.WriteLine("5. Save and Exit");

                    string? input = Console.ReadLine();

                    // Try to parse the input to an integer
                    if (int.TryParse(input, out int option))
                    {
                        switch (option)
                        {
                            case 1: AddExpenseUI(); break;
                            case 2: ViewExpensesUI(); break;
                            case 3: EditExpenseUI(); break;
                            case 4: DeleteExpenseUI(); break;
                            case 5:
                                fileManager.SaveToJson(expenseManager.GetExpenses());
                                return;
                            default:
                                Console.WriteLine("Invalid option. Please enter a number between 1 and 5.");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid number.");
                    }

                    Console.ReadLine();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }



        private static void AddExpenseUI()
        {
            Console.Write("Enter date (yyyy-MM-dd): ");
            DateTime date = DateTime.Parse(Console.ReadLine());
            Console.Write("Enter amount: ");
            decimal amount = decimal.Parse(Console.ReadLine());
            Console.Write("Enter category: ");
            string category = Console.ReadLine();
            Console.Write("Enter description: ");
            string description = Console.ReadLine();

            expenseManager.AddExpense(new Expense
            {
                Id = expenseManager.GetExpenses().Count + 1,
                Date = date,
                Amount = amount,
                Category = category,
                Description = description
            });

            Console.WriteLine("Expense added. Press Enter to continue.");
            Console.ReadLine();
        }

        private static void ViewExpensesUI()
        {
            Console.WriteLine("Expenses:");
            foreach (var expense in expenseManager.GetExpenses())
            {
                Console.WriteLine($"ID: {expense.Id}, Date: {expense.Date:yyyy-MM-dd}, Amount: {expense.Amount:C}, Category: {expense.Category}, Description: {expense.Description}");
            }
            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();
        }

        private static void EditExpenseUI()
        {
            Console.Write("Enter the ID of the expense to edit: ");
            int id = int.Parse(Console.ReadLine());
            var expense = expenseManager.GetExpenses().Find(e => e.Id == id);

            if (expense != null)
            {
                Console.Write("Enter new date (yyyy-MM-dd): ");
                DateTime date = DateTime.Parse(Console.ReadLine());
                Console.Write("Enter new amount: ");
                decimal amount = decimal.Parse(Console.ReadLine());
                Console.Write("Enter new category: ");
                string category = Console.ReadLine();
                Console.Write("Enter new description: ");
                string description = Console.ReadLine();

                expenseManager.EditExpense(id, new Expense
                {
                    Date = date,
                    Amount = amount,
                    Category = category,
                    Description = description
                });

                Console.WriteLine("Expense updated. Press Enter to continue.");
            }
            else
            {
                Console.WriteLine("Expense not found. Press Enter to try again.");
            }
            Console.ReadLine();
        }

        private static void DeleteExpenseUI()
        {
            Console.Write("Enter the ID of the expense to delete: ");
            int id = int.Parse(Console.ReadLine());
            expenseManager.DeleteExpense(id);

            Console.WriteLine("Expense deleted. Press Enter to continue.");
            Console.ReadLine();
        }
    }
}
