namespace TowerOfHanoi {
    class StackUtilities {
        public static Stack<string> BackupToStack(string[] arr) {
            Stack<string> res = new Stack<string>();
            arr = ArrayUtilities.Reverse(arr);
            for (int i = 0; i < arr.Length; i++) {
                if (arr[i].Equals("0")) {
                    continue;
                }
                res.Push(arr[i]);
            }

            return res;
        }

        public static void Display(Stack<string> stack) {
            foreach (string s in stack) {
                Console.WriteLine(s);
            }
        }

        public static void Display(Stack<string> stack1, Stack<string> stack2, Stack<string> stack3) {

            //max counts of all 3
            Console.WriteLine();
            int maxCount = Math.Max(Math.Max(stack1.Count, stack2.Count), stack3.Count);

            for (int i = 0; i < maxCount; i++) {

                string item1 = stack1.Count > 0 ? stack1.Pop() : "0";
                string item2 = stack2.Count > 0 ? stack2.Pop() : "0";
                string item3 = stack3.Count > 0 ? stack3.Pop() : "0";

                Console.WriteLine("{0}\t\t{1}\t\t{2}", item1, item2, item3);
                if (i < Activity.startBackup.Length) { Activity.startBackup[i] = item1; }
                if (i < Activity.spareBackup.Length) { Activity.spareBackup[i] = item2; }
                if (i < Activity.targetBackup.Length) { Activity.targetBackup[i] = item3; }
            }

#pragma warning disable CS8604 // Possible null reference argument.
            Activity.start = BackupToStack(Activity.startBackup);
            Activity.spare = BackupToStack(Activity.spareBackup);
            Activity.target = BackupToStack(Activity.targetBackup);
#pragma warning restore CS8604 // Possible null reference argument.
        }
    }
}
