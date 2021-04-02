using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace HWCuzImBored
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Test with UnitTest!");
            }
        }
    }

    public static class HW
    {
        public static void QuickSort(int[] Arr)
        {
            QuickSort(ref MemoryMarshal.GetArrayDataReference(Arr), 0, unchecked(Arr.Length - 1));
        }
        
        private static void QuickSort(ref int First, int Start, int End)
        {
            if (Start < End)
            {
                var Pivot = QS_Partition(ref First, Start, End);
                
                QuickSort(ref First, Start, Pivot);
                
                QuickSort(ref First, unchecked(Pivot + 1), End); 
            }
        }

        private static int QS_Partition(ref int First, int Start, int End)
        {
            var Pivot = Unsafe.Add(ref First, unchecked(((Start + End) & ~1) / 2));

            //Console.WriteLine(Pivot);
            
            ref var Left = ref Unsafe.Add(ref First, unchecked(Start - 1));
            
            ref var Right = ref Unsafe.Add(ref First, unchecked(End + 1));

            while (true)
            {
                do
                {
                    Left = ref Unsafe.Add(ref Left, 1);
                } while (Left < Pivot);
            
                do
                {
                    Right = ref Unsafe.Subtract(ref Right, 1);
                } while (Right > Pivot);

                if (Unsafe.IsAddressGreaterThan(ref Left, ref Unsafe.Subtract(ref Right, 1))) //Left >= Right is the same as Left > Right - 1
                {
                    return (int) Unsafe.ByteOffset(ref First, ref Right) / sizeof(int);
                }

                var Temp = Left;

                Left = Right;

                Right = Temp;
            }
        }

        public static void AppendStart<T>(ref T[] Arr, T Item)
        {
            var NewArr = GC.AllocateUninitializedArray<T>(unchecked(Arr.Length + 1));

            MemoryMarshal.GetArrayDataReference(NewArr) = Item;
            
            Arr.AsSpan().CopyTo(NewArr.AsSpan(1));

            Arr = NewArr;
        }
        
        public static void AppendEnd<T>(ref T[] Arr, T Item)
        {
            var NewArr = GC.AllocateUninitializedArray<T>(unchecked(Arr.Length + 1));

            Unsafe.Add(ref MemoryMarshal.GetArrayDataReference(NewArr), Arr.Length) = Item;
            
            Arr.AsSpan().CopyTo(NewArr);

            Arr = NewArr;
        }
        
        public static void InsertAt<T>(ref T[] Arr, T Item, int Pos)
        {
            if ((uint) Pos >= (uint) Arr.Length)
            {
                throw new ArgumentOutOfRangeException();
            }

            var Left = Arr.AsSpan(0, Pos);

            var Right = Arr.AsSpan(Pos);
            
            var NewArr = GC.AllocateUninitializedArray<T>(unchecked(Arr.Length + 1));

            ref var First = ref MemoryMarshal.GetArrayDataReference(NewArr);

            ref var InsertPos = ref Unsafe.Add(ref First, Pos);

            InsertPos = Item;
            
            Left.CopyTo(MemoryMarshal.CreateSpan(ref First, Left.Length));
            
            Right.CopyTo(MemoryMarshal.CreateSpan(ref Unsafe.Add(ref InsertPos, 1), Right.Length));

            Arr = NewArr;
        }
        
        public static void RemoveStart<T>(ref T[] Arr)
        {
            var NewArr = GC.AllocateUninitializedArray<T>(unchecked(Arr.Length - 1));
            
            Arr.AsSpan(1).CopyTo(NewArr);

            Arr = NewArr;
        }

        public static void RemoveEnd<T>(ref T[] Arr)
        {
            var NewSize = unchecked(Arr.Length - 1);
            
            var NewArr = GC.AllocateUninitializedArray<T>(NewSize);
            
            Arr.AsSpan(0, NewSize).CopyTo(NewArr);
            
            Arr = NewArr;
        }

        public static void RemoveAt<T>(ref T[] Arr, int Pos)
        {
            if ((uint) Pos >= (uint) Arr.Length)
            {
                throw new ArgumentOutOfRangeException();
            }
            
            if (Arr.Length == 1)
            {
                Arr = Array.Empty<T>();

                return;
            }

            var Right = Arr.AsSpan(unchecked(Pos + 1));
            
            var NewArr = GC.AllocateUninitializedArray<T>(unchecked(Arr.Length - 1));
            
            Right.CopyTo(NewArr.AsSpan(Pos));

            if (Pos != 0)
            {
                var Left = Arr.AsSpan(0, Pos);
                
                Left.CopyTo(NewArr);
            }
            
            Arr = NewArr;
        }
        
        private static readonly HashSet<string> Usernames, Passwords;

        static HW()
        {
            Usernames = new HashSet<string>() { "budgetdevv", "felix" };
            
            Passwords = new HashSet<string>() { "Dumbass", "Troll" };
        }

        public static bool LogMeInHamachi(string Username, string Password)
        {
            var Success = Usernames.Contains(Username.ToLower()) && Passwords.Contains(Password);

            if (Success)
            {
                Console.WriteLine("Logged In!");
            }

            return Success;
        }
    }
}