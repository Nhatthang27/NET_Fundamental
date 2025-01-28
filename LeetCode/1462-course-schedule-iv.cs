public class Solution
{
    public IList<bool> CheckIfPrerequisite(int numCourses, int[][] prerequisites, int[][] queries)
    {
        var adj = new List<int>[numCourses];
        var preList = new HashSet<int>[numCourses];
        var inDegree = new int[numCourses];
        foreach (var p in prerequisites)
        {
            int u = p[0], v = p[1];
            adj[u].Add(v);
            inDegree[v]++;
        }
        Queue<int> q = new Queue<int>();
        for (int u = 0; u < numCourses; u++)
        {
            if (inDegree[u] == 0)
            {
                q.Enqueue(u);
            }
        }

        while (!q.Count())
        {
            int u = q.Dequeue();
            foreach (int v in adj[u])
            {
                inDegree[v]--;
                preList[v].Add(u);
                preList[v].UnionWith(preList[u]);
                if (inDegree[v] == 0)
                {
                    q.Enqueue(v);
                }
            }
        }

        var res = new List<bool>();
        foreach (var q in queries)
        {
            var (u, v) = (q[0], q[1]);
            res.Add(preList[v].Contains(u));
        }
        return res;
    }
}