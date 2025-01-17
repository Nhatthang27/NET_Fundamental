namespace InterleaveString
{
    public class FourSum1
    {
        public IList<IList<int>> FourSum(int[] nums, int target)
        {
            IList<IList<int>> result = new List<IList<int>>();
            Dictionary<string, bool> dict = new Dictionary<string, bool>();
            if (nums.Length < 4) return result;
            for (int i = 0; i < nums.Length - 3; i++)
            {
                for (int j = i + 1; j < nums.Length - 2; j++)
                {
                    for (int k = j + 1; k < nums.Length - 1; k++)
                    {
                        for (int l = k + 1; l < nums.Length; l++)
                        {
                            if (nums[i] + nums[j] + nums[k] + nums[l] == target)
                            {
                                List<int> temp = new List<int> { nums[i], nums[j], nums[k], nums[l] };
                                temp.Sort();
                                string key = string.Join(",", temp);
                                if (!dict.ContainsKey(key))
                                {
                                    dict.Add(key, true);
                                    result.Add(temp);
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }

        public bool WordPattern(string pattern, string s)
        {
            string[] list = s.Split(' ');
            Dictionary<char, string> dict = new Dictionary<char, string>();
            for (int i = 0; i < pattern.Length; i++)
            {
                char c = pattern[i];
                if (dict.ContainsKey(c))
                {
                    if (dict[c] != list[i]) return false;
                }
                else
                {
                    if (dict.ContainsValue(list[i])) return false;
                    dict.Add(c, list[i]);
                }
            }
            return true;
        }
    }
}
