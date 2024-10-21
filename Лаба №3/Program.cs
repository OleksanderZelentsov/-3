using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаба__3
{
    class GraphAlgorithms
    {
        static (int[] dist, int[] previous) DykstreeAlgorithm(List<(int, int, int)> edges, int b, int a)
        {
            int[] dist = new int[b];
            int[] previous = new int[b];
            bool[] Set = new bool[b];

            for (int i = 0; i < b; i++)
            {
                dist[i] = int.MaxValue;
                previous[i] = -1;
                Set[i] = false;
            }

            dist[a] = 0;

            for (int count = 0; count < b - 1; count++)
            {
                int u = minDistance(dist, Set, b);
                Set[u] = true;

                foreach (var edge in edges)
                {
                    int v = edge.Item2;
                    int weight = edge.Item3;

                    if (edge.Item1 == u && !Set[v] && dist[u] != int.MaxValue && dist[u] + weight < dist[v])
                    {
                        dist[v] = dist[u] + weight;
                        previous[v] = u;
                    }
                }
            }

            return (dist, previous);
        }
        static void PrintDijkstraResults(int[] dist, int[] previous, int b, int a)
        {
            Console.WriteLine("\nАлгоритм Дейкстри: Найкоротші шляхи від вершини " + a);
            for (int i = 0; i < b; i++)
            {
                if (i != a)
                {
                    Console.Write("Вершина " + i + ": ");
                    if (dist[i] == int.MaxValue)
                    {
                        Console.WriteLine("Шлях відсутній");
                    }
                    else
                    {
                        Print(previous, i);
                        Console.WriteLine(", довжина : " + dist[i]);
                    }
                }
            }
        }
        static int minDistance(int[] dist, bool[] Set, int B)
        {
            int min = int.MaxValue, minIndex = -1;
            for (int b = 0; b < B; b++)
                if (!Set[b] && dist[b] <= min)
                {
                    min = dist[b];
                    minIndex = b;
                }

            return minIndex;
        }
        static void Print(int[] previous, int i)
        {
            if (previous[i] == -1)
            {
                Console.Write("v" + i);
                return;
            }

            Print(previous, previous[i]);
            Console.Write(" -> v" + i);
        }
        static int[,] FloydAlgorithm(int[,] graph, int b)
        {
            int[,] dist = new int[b, b];
            for (int i = 0; i < b; i++)
                for (int j = 0; j < b; j++)
                    dist[i, j] = graph[i, j];

            for (int k = 0; k < b; k++)
            {
                for (int i = 0; i < b; i++)
                {
                    for (int j = 0; j < b; j++)
                    {
                        if (dist[i, k] != int.MaxValue && dist[k, j] != int.MaxValue && dist[i, k] + dist[k, j] < dist[i, j])
                        {
                            dist[i, j] = dist[i, k] + dist[k, j];
                        }
                    }
                }
            }

            return dist;
        }

        static void PrintFloydResults(int[,] dist, int b)
        {
            Console.WriteLine("\nАлгоритм Флойда-Уоршелла: Найкоротші шляхи між всіма парами вершин:");
            for (int i = 0; i < b; i++)
            {
                for (int j = 0; j < b; j++)
                {
                    if (dist[i, j] == int.MaxValue)
                        Console.Write("INF".PadRight(7));
                    else
                        Console.Write(dist[i, j].ToString().PadRight(7));
                }
                Console.WriteLine();
            }
        }

        public static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;
            var edges = new List<(int, int, int)>
            {
            (0, 1, 5),
            (0, 3, 5),
            (0, 4, 16),
            (1, 2, 5),
            (1, 4, 3),
            (1, 5, 2),
            (2, 4, 5),
            (2, 5, 5),
            (3, 2, 3),
            (3, 4, 4),
            (4, 5, 2)
            };

            int b = 6; 

            int[,] graph = {
            { 0, 5, int.MaxValue, 5, 16, int.MaxValue },
            { int.MaxValue, 0, 5, int.MaxValue, 3, 2 },
            { int.MaxValue, int.MaxValue, 0, int.MaxValue, 5, 5 },
            { int.MaxValue, int.MaxValue, 3, 0, 4, int.MaxValue },
            { int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue, 0, 2 },
            { int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue, 0 }
            };
            bool continueProgram = true;
            while (continueProgram)
            {
                Console.WriteLine("Виберіть алгоритм:");
                Console.WriteLine("1. Алгоритм Дейкстри");
                Console.WriteLine("2. Алгоритм Беллмана-Форда");
                Console.WriteLine("3: Вийти");
                int choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Console.WriteLine($"Виберіть з якої вершини починати:");
                        int startVertex = int.Parse(Console.ReadLine());
                        var (dist, previous) = DykstreeAlgorithm(edges, b, startVertex);
                        PrintDijkstraResults(dist, previous, b, startVertex);
                        break;
                    case 2:
                        var floydDistances = FloydAlgorithm(graph, b); 
                        PrintFloydResults(floydDistances, b); 
                        break;
                    case 3:
                        continueProgram = false;
                        break;
                    default:
                        Console.WriteLine("Невірний вибір. Спробуйте знову.");
                        break;
                }
            }
        }
    }
}
