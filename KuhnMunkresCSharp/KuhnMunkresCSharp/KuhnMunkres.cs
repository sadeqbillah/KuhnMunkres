///////////////////////////////////////////////////////////////////////////////
// KuhnMunkres.cs: Implementation file for Class HungarianAlgorithm.
// 
// This is a C# implementation  of a hungarian algorithm implementation by Intel.
// Intel OpenVINO Pedestrian Detection Sample 
// https://docs.openvino.ai/latest/omz_demos_pedestrian_tracker_demo_cpp.html
// Original CPP source file is located at[]
// The original implementation can be found here:
// https://github.com/openvinotoolkit/open_model_zoo/tree/master/demos/common/cpp/utils
// 
// Meer Sadeq Billah, sadeqbillah@gmail.com, 2021
// 
using System;
using System.Drawing;
using System.Linq;

namespace KuhnMunkresCSharp
{
    internal class KuhnMunkres
    {
        private const int kStar = 1;
        private const int kPrime = 2;

        private float[,] dm_; // MEER

        private int[,] marked_; // MEER

        private Point[] points_;
        private int[] is_row_visited_;
        private int[] is_col_visited_;

        private int n_;


        public int[] Solve(float[,] dissimilarity_matrix)
        {
            float min_val = dissimilarity_matrix.Cast<float>().Min();

            int rows = dissimilarity_matrix.GetLength(0);
            int cols = dissimilarity_matrix.GetLength(1);

            n_ = Math.Max(rows, cols);

            dm_ = new float[n_, n_];
            marked_ = new int[n_, n_];

            points_ = new Point[n_ * 2];

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    dm_[i, j] = dissimilarity_matrix[i, j];

            is_row_visited_ = new int[n_];
            is_col_visited_ = new int[n_];

            Run();

            int[] results = new int[rows];
            Array.ForEach(results, x => x = -1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (marked_[i, j] == kStar)
                    {
                        results[i] = j;
                    }
                }
            }
            return results;
        }

        private void TrySimpleCase()
        {
            int[] is_row_visited = new int[n_];
            int[] is_col_visited = new int[n_];

            for (int row = 0; row < n_; row++)
            {
                float min_val = dm_[row, 0];
                for (int col = 1; col < n_; col++)
                {
                    if (dm_[row, col] < min_val)// && dm_[row, col] != 0)
                        min_val = dm_[row, col];
                }
                for (int col = 0; col < n_; col++)
                {
                    //if(dm_[row, col] != 0)
                    dm_[row, col] -= min_val;
                    if (dm_[row, col] == 0 && is_col_visited[col] == 0 && is_row_visited[row] == 0)
                    {
                        marked_[row, col] = kStar;
                        is_col_visited[col] = 1;
                        is_row_visited[row] = 1;
                    }
                }
            }
        }

        private bool CheckIfOptimumIsFound()
        {
            int count = 0;
            for (int i = 0; i < n_; i++)
            {
                for (int j = 0; j < n_; j++)
                {
                    if (marked_[i, j] == kStar)
                    {
                        is_col_visited_[j] = 1;
                        count++;
                    }
                }
            }
            return count >= n_;
        }


        private Point FindUncoveredMinValPos()
        {
            float min_val = float.MaxValue;
            Point min_val_pos = new Point(-1, -1);
            for (int i = 0; i < n_; i++)
            {
                if (is_row_visited_[i] == 0)
                {
                    for (int j = 0; j < n_; j++)
                    {
                        if (is_col_visited_[j] == 0 && dm_[i, j] < min_val)
                        {
                            min_val = dm_[i, j];
                            min_val_pos = new Point(j, i);
                        }
                    }
                }
            }
            return min_val_pos;

        }
        private void UpdateDissimilarityMatrix(float val)
        {
            for (int i = 0; i < n_; i++)
            {
                for (int j = 0; j < n_; j++)
                {
                    if (is_row_visited_[i] != 0) dm_[i, j] += val;
                    if (is_col_visited_[j] == 0) dm_[i, j] -= val;
                }
            }
        }
        private int FindInRow(int row, int what)
        {
            for (int j = 0; j < n_; j++)
            {
                if (marked_[row, j] == what)
                {
                    return j;
                }
            }
            return -1;
        }
        private int FindInCol(int col, int what)
        {
            for (int i = 0; i < n_; i++)
            {
                if (marked_[i, col] == what)
                {
                    return i;
                }
            }
            return -1;
        }
        private void Run()
        {
            TrySimpleCase();
            while (!CheckIfOptimumIsFound())
            {
                while (true)
                {
                    Point point = FindUncoveredMinValPos();
                    float min_val = dm_[point.Y, point.X];
                    if (min_val > 0)
                    {
                        UpdateDissimilarityMatrix(min_val);
                    }
                    else
                    {
                        marked_[point.Y, point.X] = kPrime;
                        int col = FindInRow(point.Y, kStar);
                        if (col >= 0)
                        {
                            is_row_visited_[point.Y] = 1;
                            is_col_visited_[col] = 0;
                        }
                        else
                        {
                            int count = 0;
                            points_[count] = point;

                            while (true)
                            {
                                int row = FindInCol(points_[count].X, kStar);
                                if (row >= 0)
                                {
                                    count++;
                                    points_[count] = new Point(points_[count - 1].X, row);
                                    int col2 = FindInRow(points_[count].Y, kPrime);
                                    count++;
                                    points_[count] = new Point(col2, points_[count - 1].Y);
                                }
                                else
                                {
                                    break;
                                }
                            }

                            for (int i = 0; i < count + 1; i++)
                            {
                                marked_[points_[i].Y, points_[i].X] = marked_[points_[i].Y, points_[i].X] == kStar ? 0 : kStar;
                            }

                            is_row_visited_ = new int[n_];
                            is_col_visited_ = new int[n_];

                            for (int i = 0; i < marked_.GetLength(0); i++)
                                for (int j = 0; j < marked_.GetLength(1); j++)
                                    if (marked_[i, j] == kPrime)
                                        marked_[i, j] = 0;

                            break;
                        }
                    }
                }
            }
        }

        public KuhnMunkres() { }

    }
}
