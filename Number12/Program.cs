using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Number12
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Задание #12: Сортировка и сравнение методов блочной и пирамидаидальной сортировки");
            //int[] SortedDown = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };
            //int[] SortedUp = new int[] { 30, 29, 28, 27, 26, 25, 24, 23, 22, 21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };
            //int[] Mixed = new int[] { 30, 1, 29, 2, 28, 3, 27, 4, 26, 5, 25, 6, 24, 7, 23, 8, 22, 9, 21, 10, 20, 11, 19, 12, 18, 13, 17, 14, 16, 15 };
            int[] SortedDown = CreateSortedArray(100);
            int[] SortedUp = CreateSortedArray(100, false);
            int[] Mixed = CreateRandomArray(100);

            Console.WriteLine("\nСортировка упорядоченного по возрастанию массива методом блочной сортировки:");
            int[] BlockSorted = (int[])SortedDown.Clone();
            Console.WriteLine("Исходный массив:");
            PrintArray(BlockSorted);
            BucketSort(ref BlockSorted);
            Console.WriteLine("Результат сортировки:");
            PrintArray(BlockSorted);
            Console.WriteLine("\nСортировка упорядоченного по возрастанию массива методом пирамидальной сортировки:");
            int[] PyramydSortedDown = (int[])SortedDown.Clone();
            Console.WriteLine("Исходный массив:");
            PrintArray(PyramydSortedDown);
            HeapSort(ref PyramydSortedDown);
            Console.WriteLine("Результат сортировки:");
            PrintArray(PyramydSortedDown);
            Console.WriteLine("\nСортировка упорядоченного по убыванию массива методом блочной сортировки:");
            int[] BlockSortedUP = (int[])SortedUp.Clone();
            Console.WriteLine("Исходный массив:");
            PrintArray(BlockSortedUP);
            BucketSort(ref BlockSortedUP);
            Console.WriteLine("Результат сортировки:");
            PrintArray(BlockSortedUP);

            Console.WriteLine("\nСортировка упорядоченного по убыванию массива методом пирамидальной сортировки:");
            int[] PyramydSortedUp = (int[])SortedUp.Clone();
            Console.WriteLine("Исходный массив:");
            PrintArray(PyramydSortedUp);
            HeapSort(ref PyramydSortedUp);
            Console.WriteLine("Результат сортировки:");
            PrintArray(PyramydSortedUp);
            Console.WriteLine("\nСортировка неупорядоченного  массива методом блочной сортировки:");
            int[] Blockunsorted = (int[])Mixed.Clone();
            Console.WriteLine("Исходный массив:");
            PrintArray(Blockunsorted);
            BucketSort(ref Blockunsorted);
            Console.WriteLine("Результат сортировки:");
            PrintArray(Blockunsorted);
            Console.WriteLine("\nСортировка неупорядоченного массива методом пирамидальной сортировки:");
            int[] PyramydUnsorted = (int[])Mixed.Clone();
            Console.WriteLine("Исходный массив:");
            PrintArray(PyramydUnsorted);
            HeapSort(ref PyramydUnsorted);
            Console.WriteLine("Результат сортировки:");
            PrintArray(PyramydUnsorted);
            Console.ReadLine();
        }
        /// <summary>
        /// Заполняет массив случайными данными
        /// </summary>
        /// <param name="Length">длина массива</param>
        /// <returns></returns>
        static int[] CreateRandomArray(int Length)
        {
            int[] retVal = new int[Length];
            Random ran = new Random();
            for (int i = 0; i < Length; i++)
            {
                retVal[i] = ran.Next(0, Length);
            }
            return retVal;
        }
        /// <summary>
        /// создает упорядоченный массив заданной длины
        /// </summary>
        /// <param name="Length">длина массива</param>
        /// <param name="Up">по возрастанию/убыванию</param>
        /// <returns></returns>
        static int[] CreateSortedArray(int Length, bool Up = true)
        {
            int[] retVal = new int[Length];
            if (Up)
            {
                for (int i = 0; i < Length; i++)
                    retVal[i] = i;
            }
            else
            {
                for (int i = 0; i < Length; i++)
                    retVal[i] = Length - i;
            }
            return retVal;
        }


        /// <summary>
        /// Печать массива на экран
        /// </summary>
        /// <param name="data"></param>
        static void PrintArray(int[] data)
        {
            Console.WriteLine("Массив из " + data.Length + " элементов:");
            for (int i = 0; i < data.Length; i++)
            {
                Console.Write(data[i] + " ");
            }
            Console.WriteLine();
        }
        /// <summary>
        /// Пирамидальная сортировка
        /// </summary>
        /// <param name="data"></param>
        public static void HeapSort(ref int[] data)
        {
            int heapSize = data.Length;
            int sCount = 0;
            int pCount = 0;
            for (int p = (heapSize - 1) / 2; p >= 0; --p)
                MaxHeapify(ref data, heapSize, p, ref sCount, ref pCount);
            for (int i = data.Length - 1; i > 0; --i)
            {
                pCount++;
                int temp = data[i];
                data[i] = data[0];
                data[0] = temp;
                --heapSize;
                MaxHeapify(ref data, heapSize, 0, ref sCount, ref pCount);
            }
            Console.WriteLine("Количество пересылок:" + pCount);
            Console.WriteLine("Количество сравнений:" + sCount);
        }
        /// <summary>
        /// Сортирующее дерево
        /// </summary>
        /// <param name="data"></param>
        /// <param name="heapSize">размер дерева</param>
        /// <param name="index"></param>
        private static void MaxHeapify(ref int[] data, int heapSize, int index, ref int sCount, ref int pCount)
        {
            int left = (index + 1) * 2 - 1;
            int right = (index + 1) * 2;
            int largest = 0;
            sCount++;
            if (left < heapSize && data[left] > data[index])
            {
                //pCount++;
                largest = left;
            }
            else
            {
                //pCount++;
                largest = index;
            }
            sCount++;
            if (right < heapSize && data[right] > data[largest])
            {
                largest = right;
                //pCount++;
            }
            //sCount++;
            if (largest != index)
            {
                int temp = data[index];
                data[index] = data[largest];
                data[largest] = temp;
                pCount++;
                MaxHeapify(ref data, heapSize, largest, ref sCount, ref pCount);
            }
        }
        /// <summary>
        /// Блочная сортировка
        /// </summary>
        /// <param name="data"></param>
        public static void BucketSort(ref int[] data)
        {
            // Минимальное значение
            int minValue = data[0];
            // Максимальное значение
            int maxValue = data[0];
            // Количество сравнений
            int sCount = 0;
            // Количество перестановок
            int pCount = 0;
            for (int i = 1; i < data.Length; i++)
            {
                if (data[i] > maxValue)
                {
                    maxValue = data[i];
                    pCount++;
                }
                sCount++;
                if (data[i] < minValue)
                {
                    minValue = data[i];
                    pCount++;
                }
                sCount++;
            }
            // Блок
            List<int>[] bucket = new List<int>[maxValue - minValue + 1];
            for (int i = 0; i < bucket.Length; i++)
            {
                bucket[i] = new List<int>();
            }
            for (int i = 0; i < data.Length; i++)
            {
                bucket[data[i] - minValue].Add(data[i]);
                pCount++;
            }
            int k = 0;
            for (int i = 0; i < bucket.Length; i++)
            {
                if (bucket[i].Count > 0)
                {
                    for (int j = 0; j < bucket[i].Count; j++)
                    {
                        data[k] = bucket[i][j];
                        pCount++;
                        k++;
                    }
                }
            }
            Console.WriteLine("Количество пересылок:" + pCount);
            Console.WriteLine("Количество сравнений:" + sCount);
        }
    }
}
