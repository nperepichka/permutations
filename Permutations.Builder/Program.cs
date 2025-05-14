using System.Diagnostics;

namespace Permutations.Builder;

public static class Program
{
    public static void Main()
    {
        Console.Write("n: ");
        var val = Console.ReadLine();
        if (!byte.TryParse(val, out var n) || n == 0)
        {
            Console.WriteLine("Invalid value.");
            Console.ReadLine();
            return;
        }

        var stopwatch = Stopwatch.StartNew();

        var permutations = GetPermutations(n);

        var processed = new bool[permutations.Count];
        for (var i = 0; i < processed.Length; i++)
        {
            processed[i] = false;
        }

        var chains = new List<List<int>>();

        for (var i = 0; i < permutations.Count; i++)
        {
            if (processed[i])
                continue;

            var canBreak = false;
            var p = permutations[i];
            processed[i] = true;
            var chain = new List<int>() { i };

            while (true)
            {
                p = MultiplyPermutations(p, p);

                for (var j = 0; j < permutations.Count; j++)
                {
                    if (p.SequenceEqual(permutations[j]))
                    {
                        chain.Add(j);
                        if (processed[j])
                        {
                            canBreak = true;
                            break;
                        }
                        else
                        {
                            processed[j] = true;
                            break;
                        }
                    }
                }

                if (canBreak)
                    break;
            }

            chains.Add(chain);
        }

        foreach (var chain in chains)
        {
            var isFirst = true;
            foreach (var i in chain)
            {
                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    Console.Write(" -> ");
                }

                foreach (var j in permutations[i])
                {
                    Console.Write(j + 1);
                }
            }
            Console.WriteLine();
        }

        stopwatch.Stop();
        Console.WriteLine();
        Console.WriteLine($"Processing elapsed in {stopwatch.ElapsedMilliseconds * 0.001:0.00}s");
        Console.WriteLine("Done. Press <Enter> to exit...");
        Console.ReadLine();
    }

    private static List<byte[]> GetPermutations(int n)
    {
        var result = new List<byte[]>();
        var nums = new byte[n];
        for (byte i = 0; i < n; i++)
        {
            nums[i] = i;
        }

        Permute(nums, 0, result);
        return result;
    }

    private static void Permute(byte[] nums, int start, List<byte[]> result)
    {
        if (start == nums.Length)
        {
            result.Add([.. nums]);
            return;
        }

        for (var i = start; i < nums.Length; i++)
        {
            (nums[i], nums[start]) = (nums[start], nums[i]);
            Permute(nums, start + 1, result);
            (nums[i], nums[start]) = (nums[start], nums[i]);
        }
    }

    private static byte[] MultiplyPermutations(byte[] a, byte[] b)
    {
        var result = new byte[a.Length];
        for (byte i = 0; i < a.Length; i++)
        {
            result[i] = b[a[i]];
        }
        return result;
    }
}