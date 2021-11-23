using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuhnMunkresCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            KuhnMunkres kh = new KuhnMunkres();

            float[,] dissimilarity = new float[,] 
                                        { 
                                            { 1.7f, 2.3f, 1.2f }, 
                                            { 1.9f, 0.2f, 3.3f }, 
                                            { 2.0f, 0.9f, 1.2f }
                                        };
            int[] res = kh.Solve(dissimilarity);
            
            for(int i  = 0; i < res.Count(); i++)
                Console.WriteLine(string.Format("({0}, {1})", i, res[i]));
            Console.WriteLine("");

            dissimilarity = new float[,] 
                                    { 
                                        { 1.7f, 2.3f }, 
                                        { 2.0f, 0.9f }, 
                                        { 1.9f, 0.2f }
                                    };
            res = kh.Solve(dissimilarity);

            for (int i = 0; i < res.Count(); i++)
                Console.WriteLine(string.Format("({0}, {1})", i, res[i]));
            Console.WriteLine("");

            dissimilarity = new float[,] 
                                    { 
                                        { 1.7f, 2.3f, 1.2f }, 
                                        { 1.9f, 0.2f, 3.3f }
                                    };
            res = kh.Solve(dissimilarity);

            for (int i = 0; i < res.Count(); i++)
                Console.WriteLine(string.Format("({0}, {1})", i, res[i]));
            Console.WriteLine("");

            dissimilarity = new float[,] 
                                    { 
                                        { 1.9f, 0.2f }, 
                                        { 2.0f, 0.9f }
                                    };
            res = kh.Solve(dissimilarity);

            for (int i = 0; i < res.Count(); i++)
                Console.WriteLine(string.Format("({0}, {1})", i, res[i]));
            Console.WriteLine("");
        }
    }
}
