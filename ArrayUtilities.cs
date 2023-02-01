namespace TowerOfHanoi {
    class ArrayUtilities {
        public static T[] Reverse<T>(T[] o) {
            int i = 0; int j = o.Length - 1;
            return Reverse(o, i, j);
        }

        private static T[] Reverse<T>(T[] o, int i, int j) {
            if (i < j) {
                T temp = o[i];
                o[i] = o[j];
                o[j] = temp;
                return Reverse(o, i + 1, j - 1);
            } else {
                return o;
            }
        }

        public static void DisplayArray<T>(T[] arr) {
            string array = "{";

            array += string.Join(", ", arr);

            array += "}";

            Console.WriteLine(array);
        }
    }
}
