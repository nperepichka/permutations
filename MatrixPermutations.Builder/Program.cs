namespace MatrixPermutations.Builder;

public static class Program
{
    static void Main()
    {
        bool[,] matrix = new bool[,]
        {
            { true, true, false, false, false, false, false, false },
            { false, true, true, false, false, false, false, false },
            { false, false, true, true, false, false, false, false},
            { false, false, false, true, true, false, false, false },
            { false, false, false, false, true, true, false, false },
            { false, false, false, false, false, true, true, false },
            { false, false, false, false, false, false, true, true },
            { true, false, false, false, false, false, false, true },
        };

        int count = CountUniqueMatrices(matrix);
        Console.WriteLine($"Unique matrices: {count}");
    }

    static int CountUniqueMatrices(bool[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);

        var unique = new HashSet<string>();
        var rowPerms = GetPermutations([.. Enumerable.Range(0, rows)]);
        var colPerms = GetPermutations([.. Enumerable.Range(0, cols)]);

        foreach (var rowOrder in rowPerms)
        {
            foreach (var colOrder in colPerms)
            {
                string hash = MatrixToHash(matrix, rowOrder, colOrder);
                if (unique.Add(hash))
                {
                    /*foreach (int i in rowOrder)
                    {
                        foreach (int j in colOrder)
                        {
                            Console.Write(matrix[i, j] ? "1 " : "0 ");
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine(new string('-', 20));*/
                }
            }
        }

        return unique.Count;
    }

    static string MatrixToHash(bool[,] matrix, int[] rowOrder, int[] colOrder)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        char[] result = new char[rows * cols];
        int idx = 0;

        foreach (int i in rowOrder)
        {
            foreach (int j in colOrder)
            {
                result[idx++] = matrix[i, j] ? '1' : '0';
            }
        }

        return new string(result);
    }

    static List<int[]> GetPermutations(int[] elements)
    {
        var result = new List<int[]>();
        Permute(elements, 0, result);
        return result;
    }

    static void Permute(int[] array, int start, List<int[]> result)
    {
        if (start >= array.Length)
        {
            result.Add((int[])array.Clone());
            return;
        }

        for (int i = start; i < array.Length; i++)
        {
            Swap(ref array[start], ref array[i]);
            Permute(array, start + 1, result);
            Swap(ref array[start], ref array[i]);
        }
    }

    static void Swap(ref int a, ref int b)
    {
        if (a == b) return;
        (b, a) = (a, b);
    }
}