namespace HeapPQ
{
    public class Program
    {
        public static void Main()
        {
            TestPQ();
        }
        private static void TestPQ()
        {
            Heap<char, int> pq = new Heap<char, int>();
            pq.Push('A', 6);
            pq.Push('B', 4);
            pq.Push('C', 10);
            pq.Push('D', 2);
            pq.Push('E', 3);
            Console.WriteLine(pq);
            char patient = pq.Pop();
            Console.WriteLine($"Patient {patient} is next to be seen.");
            Console.WriteLine(pq);
        }
    }
}