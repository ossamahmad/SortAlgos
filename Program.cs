using System;
using System.Diagnostics;

namespace myApp
{
    class Program
    {
        static void Main(string[] args)
        {
         int arraySize;
         int randomSeed;
         Stopwatch watch = new Stopwatch ();
         double elapsedTime;  // time in second, accurate to about millseconds
         if (args.Length < 2) {
            Console.WriteLine("Usage: myApp arraySize  randomSeed");
            return;
         } else {
            arraySize = int.Parse (args [0]);
            randomSeed = int.Parse (args [1]);
         }
         int[] data = new int[arraySize];
         IntArrayGenerate (data, randomSeed);
         int[] dataS = (int[])data.Clone();
         int[] dataBS = (int[])data.Clone();
         int[] dataI = (int[])data.Clone();
         int[] dataSe = (int[])data.Clone();
         int[] dataB = (int[])data.Clone();

         watch.Reset ();
         watch.Start ();
         IntArrayQuickSort (data);  // the other experiments call a different method
         watch.Stop ();
         elapsedTime = watch.ElapsedMilliseconds/1000.0;
         Console.WriteLine ("Quick Sort: {0:F3}", elapsedTime);
         watch.Reset ();
         watch.Start ();
         IntArrayShellSortNaive (dataS);  // the other experiments call a different method
         watch.Stop ();
         elapsedTime = watch.ElapsedMilliseconds/1000.0;
         Console.WriteLine ("Shell Sort: {0:F3}", elapsedTime);
         watch.Reset ();
         watch.Start ();
         IntArrayShellSortBetter (dataBS);  // the other experiments call a different method
         watch.Stop ();
         elapsedTime = watch.ElapsedMilliseconds/1000.0;
         Console.WriteLine ("Better Shell Sort: {0:F3}", elapsedTime);
         watch.Reset ();
         watch.Start ();
         IntArrayInsertionSort (dataI);  // the other experiments call a different method
         watch.Stop ();
         elapsedTime = watch.ElapsedMilliseconds/1000.0;
         Console.WriteLine ("Insertion Sort: {0:F3}", elapsedTime);
         watch.Reset ();
         watch.Start ();
         IntArraySelectionSort (dataSe);  // the other experiments call a different method
         watch.Stop ();
         elapsedTime = watch.ElapsedMilliseconds/1000.0;
         Console.WriteLine ("Selection Sort: {0:F3}", elapsedTime);
         watch.Reset ();
         watch.Start ();
         IntArrayBubbleSort (dataB);  // the other experiments call a different method
         watch.Stop ();
         elapsedTime = watch.ElapsedMilliseconds/1000.0;
         Console.WriteLine ("Bubble Sort: {0:F3}", elapsedTime);
        }
        public static void exchange (int[] data, int m, int n)
      {
         int temporary;

         temporary = data [m];
         data [m] = data [n];
         data [n] = temporary;
      }
      public static void IntArrayBubbleSort (int[] data)
      {
         int i, j;
         int N = data.Length;

         for (j=N-1; j>0; j--) {
            for (i=0; i<j; i++) {
               if (data [i] > data [i + 1])
                  exchange (data, i, i + 1);
            }
         }
      }
      public static int IntArrayMin (int[] data, int start)
      {
         int minPos = start; 
         for (int pos=start+1; pos < data.Length; pos++)
            if (data [pos] < data [minPos])
               minPos = pos;
         return minPos; 
      }

      public static void IntArraySelectionSort (int[] data)
      {
         int i;
         int N = data.Length;

         for (i=0; i < N-1; i++) {
            int k = IntArrayMin (data, i);
            if (i != k)
               exchange (data, i, k);
         }
      }
      public static void IntArrayInsertionSort (int[] data)
      {
         int i, j;
         int N = data.Length;

         for (j=1; j<N; j++) {
            for (i=j; i>0 && data[i] < data[i-1]; i--) {
               exchange (data, i, i - 1);
            }
         }
      }
      public static void IntArrayShellSort (int[] data, int[] intervals)
      {
         int i, j, k, m;
         int N = data.Length;

         // The intervals for the shell sort must be sorted, ascending

         for (k=intervals.Length-1; k>=0; k--) {
            int interval = intervals [k];
            for (m=0; m<interval; m++) {
               for (j=m+interval; j<N; j+=interval) {
                  for (i=j; i>=interval && data[i]<data[i-interval]; i-=interval) {
                     exchange (data, i, i - interval);
                  }
               }
            }
         }
      }
      public static void IntArrayShellSortNaive (int[] data)
      {
         int[] intervals = { 1, 2, 4, 8 };
         IntArrayShellSort (data, intervals);
      }
      static int[] GenerateIntervals (int n)
      {
         if (n < 2) {  // no sorting will be needed
            return new int[0];
         }
         int t = Math.Max (1, (int)Math.Log (n, 3) - 1);
         int[] intervals = new int[t];       
         intervals [0] = 1;
         for (int i=1; i < t; i++)
            intervals [i] = 3 * intervals [i - 1] + 1;
         return intervals;
      }

      public static void IntArrayShellSortBetter (int[] data)
      {
         int[] intervals = GenerateIntervals (data.Length);
         IntArrayShellSort (data, intervals);
      }      public static void IntArrayQuickSort (int[] data, int l, int r)
      {
         int i, j;
         int x;
 
         i = l;
         j = r;

         x = data [(l + r) / 2]; /* find pivot item */
         while (true) {
            while (data[i] < x)
               i++;
            while (x < data[j])
               j--;
            if (i <= j) {
               exchange (data, i, j);
               i++;
               j--;
            }
            if (i > j)
               break;
         }
         if (l < j)
            IntArrayQuickSort (data, l, j);
         if (i < r)
            IntArrayQuickSort (data, i, r);
      }

      public static void IntArrayQuickSort (int[] data)
      {
         IntArrayQuickSort (data, 0, data.Length - 1);
      }
      public static void IntArrayGenerate (int[] data, int randomSeed)
      {
         Random r = new Random (randomSeed);
         for (int i=0; i < data.Length; i++)
            data [i] = r.Next ();
      }

    }
}
