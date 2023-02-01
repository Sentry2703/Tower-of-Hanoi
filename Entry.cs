
using TowerOfHanoi;

Console.WriteLine("How many rings do you want in your tower? (Integer Only)");
int rings = 3;
try {
    rings = Int32.Parse(Console.ReadLine());
} catch (FormatException e) {
    Console.WriteLine(e.Message);
    Console.WriteLine("We'll just give you a defualt of 3 rings");
}

Console.WriteLine($"Okay, Now, you have {rings} rings.");

Activity.Game(rings);

Console.WriteLine("\nThanks for playing!");
