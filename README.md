# KuhnMunkres
C# Implementation of Kuhn Munkres Algorithm. Widely known as Hungarian Algorithm.

The Hungarian method is a combinatorial optimization algorithm that solves the assignment problem in polynomial time and which anticipated later primal–dual methods. It was developed and published in 1955 by Harold Kuhn, who gave the name "Hungarian method" because the algorithm was largely based on the earlier works of two Hungarian mathematicians: Dénes Kőnig and Jenő Egerváry.

This is a method for solving the assignment problem, for example assigning workers to jobs, which goal is to compute the optimal assignment that minimizes the total cost, and the like.

This is a ported version from Intel OpenVINO Pedestrian Detection Sample [https://docs.openvino.ai/latest/omz_demos_pedestrian_tracker_demo_cpp.html] 

Original CPP source file is located at [https://github.com/openvinotoolkit/open_model_zoo/tree/master/demos/common/cpp/utils]

**Development Environment:** Visual Studio 2015 Community Edition<br/>
**References:** System.Drawing, System, System.Linq<br/>

Input: 2 Dimensional Array of Distance between each pairs (x, y) of candidate data <br/>
Output: 1 Dimensional Array of indexes of matching (x, y)  
