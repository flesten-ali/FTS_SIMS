using FalastinShop.Simple_Inventory_Management_System;
using FalastinShop.Simple_Inventory_Management_System.InventoryManagment;
using FalastinShop.Simple_Inventory_Management_System.ProductManagment;
printWelcome();














Console.ReadLine();
static void printWelcome()
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine(@"
______    _           _   _         _____ _                 
|  ___|  | |         | | (_)       /  ___| |                
| |_ __ _| | __ _ ___| |_ _ _ __   \ `--.| |__   ___  _ __  
|  _/ _` | |/ _` / __| __| | '_ \   `--. \ '_ \ / _ \| '_ \ 
| || (_| | | (_| \__ \ |_| | | | | /\__/ / | | | (_) | |_) |
\_| \__,_|_|\__,_|___/\__|_|_| |_| \____/|_| |_|\___/| .__/ 
                                                     | |    
                                                     |_|    
");
    Console.ResetColor();
    Console.WriteLine("Press enter to start... ");
    Console.ReadLine();
    Console.Clear();

}