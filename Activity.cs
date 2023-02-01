using System.Net.Quic;
using System.Runtime.CompilerServices;

namespace TowerOfHanoi {
    class Activity {

        static int rings;
        public Activity(int rings) {
            Activity.rings = rings;
        }

        public static Stack<string>? start;
        public static Stack<string>? spare;
        public static Stack<string>? target;
        public static string[]? startBackup;
        public static string[]? spareBackup;
        public static string[]? targetBackup;
        public static Stack<string>[] stacks = new Stack<string>[]{start, spare, target};

        public static void Game(int rings) {
            Activity.rings = rings;
            Game();
        }
        public static void Game() {
            start = new Stack<string>(rings);
            startBackup = new string[start.Count];
            spare = new Stack<string>(rings);
            spareBackup = new string[start.Count];
            target = new Stack<string>(rings);
            targetBackup = new string[start.Count];

            bool complete = false;

            //Fill up start
            Fill(start, rings);

            startBackup = new string[start.Count];

            //Display start position
            StackUtilities.Display(start, spare, target);

            //Give options for playing
            Instructions();

            while (!complete) {
                string? instruct = Console.ReadLine();
                switch(instruct?.ToLower().Trim()) {
                    case "mv":
                        Move();
                        Console.WriteLine();
                        StackUtilities.Display(start, spare, target);
                        break;
                    case "ck":
                        if (CheckIfDone(target)) {
                            complete = true;
                        } else {
                            Console.WriteLine("\nNot quite right");
                            target = StackUtilities.BackupToStack(targetBackup);
                            Instructions();
                        }
                        break;
                    case "ls":
                        StackUtilities.Display(start, spare, target);
                        Console.WriteLine();
                        break;
                    case "help":
                        Instructions();
                        Console.WriteLine();
                        break;
                    case "quit":
                        Console.WriteLine("\nCoward!");
                        return;
                    default:
                        Console.WriteLine("\nInvalid input.");
                        Instructions();
                        break;
                }

                if (target.Count == rings) {
                    if (CheckIfDone(target)) {
                        complete = true;
                    } else {
                        Console.WriteLine("\nNot quite the right arrangement");
                        target = StackUtilities.BackupToStack(targetBackup);
                        Instructions();
                    }
                }
            }
        }

        private static void Fill(Stack<string> stack, int rings) {
            for (int i = rings; i > 0; i--) {
                stack.Push($"{i}");
            }
        }

        private static void Instructions() {
            Console.WriteLine("\n\"mv\" to move from one stack to another stack," +
                "\n\"ck\" to check if you're correct." +
                "\n\"ls\" to show what the stacks look like." +
                "\n\"help\" to show instructions." +
                "\n\"quit\" to end the game prematurely." +
                "\n");
        }

        private static Boolean CheckIfDone(Stack<string> stack) {
            for (int i = 1; i <= rings; i++) {
                if (i != Int32.Parse(stack.Pop())) { return false; }
            }
            Console.WriteLine("Congrats! You won the tower of Hanoi!");
            return true;
        }

        public static void Move() {
            Console.WriteLine("\nInput your start stack (1, 2, 3) a space \" \", and your target stack (1, 2, 3).");
            string? response = Console.ReadLine();
            string[]? moveData = response?.Split(' ');

            bool v = Int32.TryParse(moveData[0], out int remove);
            bool x = Int32.TryParse(moveData[1], out int place);

            if ((v && x) && InRange(remove, 1, 3) && InRange(place, 1, 3)) {
                Console.WriteLine($"\nSo you're moving from {remove} to {place}");
            } else {
                Console.WriteLine("\nInvalid input. Read the instructions again. I recommend typing \"help\".");
                return;
            }

            int move, current;

            if (remove == 1) {
                if (start.Count > 0) { move = Int32.Parse(start.Peek()); }
                else { move = 0; }
            }else if (remove == 2) {
                if (spare.Count > 0) { move = Int32.Parse(spare.Peek());  }
                else { move = 0; }
            } else if (remove == 3) {
                if (target.Count > 0) { move = Int32.Parse(target.Peek());  }
                else { move = 0; }
            } else {
                move = 0;
            }

            if (move == 0) {
                Console.WriteLine("\nInvalid operation. Can't move from an empty stack");
                return;
            } 

            if (place == 1) {
                if (start.Count > 0) { current = Int32.Parse(start.Peek()); } else { current = 0; }
            } else if (place == 2) {
                if (spare.Count > 0) { current = Int32.Parse(spare.Peek()); } else { current= 0; }
            } else if (place == 3) {
                if (target.Count > 0) { current = Int32.Parse(target.Peek()); } else { current = 0; }
            } else {
                current = 0;
            }

            if (move > current && current != 0) {
                Console.WriteLine("\nInvalid operation. Can't move from an empty stack");
                return;
            }

            switch(remove) {
                case 1:
                    current = Int32.Parse(start.Pop());
                    startBackup = start.ToArray();
                    break;
                case 2:
                    current = Int32.Parse(spare.Pop());
                    spareBackup = spare.ToArray();
                    break;
                case 3:
                    current = Int32.Parse(target.Pop());
                    targetBackup = target.ToArray();
                    break;
                default:
                    Console.WriteLine($"\nMove from stack {remove} to stack {place} was unsuccessful.");
                    return;
            }

            switch(place) {
                case 1:
                    start.Push($"{current}");
                    startBackup= start.ToArray();
                    break;
                case 2:
                    spare.Push($"{current}");
                    spareBackup= spare.ToArray();
                    break;
                case 3:
                    target.Push($"{current}");
                    targetBackup= target.ToArray();
                    break;
                default:
                    Console.WriteLine($"\nMove from stack {remove} to stack {place} was unsuccessful.");
                    return;
            }

            Console.WriteLine($"\nMove from stack {remove} to stack {place} was successful!");

        }

        private static bool InRange(int number, int lower, int upper) {
            if (number <= upper && number >= lower) {
                return true;
            }
            return false;
        }
    }
}

