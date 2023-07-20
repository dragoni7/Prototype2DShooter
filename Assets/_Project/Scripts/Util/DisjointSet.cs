using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Mathematics;

namespace Util
{
    public class DisjointSet
    {
        int[] parent;
        int[] rank; // tree height

        /// <summary>
        /// Initialize n natural numbers
        /// </summary>
        /// <param name="length">value of n</param>
        public DisjointSet(int length)
        {
            parent = new int[length];
            rank = new int[length];

            // make parent of element itself
            for (int i = 0; i < parent.Length; i++)
            {
                parent[i] = i;
            }
        }

        /// <summary>
        /// Making set with one element
        /// </summary>
        /// <param name="x">element</param>
        public void MakeSet(int x)
        {
            parent[x] = x;
            rank[x] = 0;
        }

        /// <summary>
        /// Unions two sets by rank
        /// </summary>
        /// <param name="x">set 1</param>
        /// <param name="y">set 2</param>
        public void Union(int set1, int set2)
        {
            int representative1 = FindSet(set1);
            int representative2 = FindSet(set2);

            // if both have same rank making set2 set1's parent
            if (rank[representative1] == rank[representative2])
            {
                rank[representative2]++;
                parent[representative1] = representative2;
            }
            else if (rank[representative1] > rank[representative2])
            {
                parent[representative2] = representative1;
            }
            else
            {
                parent[representative1] = representative2;
            }
        }

        /// <summary>
        /// Finds representative of a set
        /// </summary>
        /// <param name="x">element of a set</param>
        /// <returns></returns>
        public int FindSet(int x)
        {
            if (parent[x] != x)
            {
                parent[x] = FindSet(parent[x]); // path compression
            }

            return parent[x];
        }

        /// <summary>
        /// Finds immediate parent
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public int FindImmediateParent(int x)
        {
            return parent[x];
        }

        /// <summary>
        /// Finds rank
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public int FindRank(int x)
        {
            return rank[x];
        }
    }
}
