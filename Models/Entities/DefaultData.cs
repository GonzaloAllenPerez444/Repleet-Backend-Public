using System.Reflection;
using System.Security.Policy;

namespace Repleet.Models.Entities
{
    public static class DefaultData
    {
        public static ProblemSet GetDefaultProblemSet()
        {
            var arrayProblems = new List<Problem>
            {
                new Problem { Title = "Contains Duplicate", Url = "https://leetcode.com/problems/contains-duplicate/", IsCompleted = false, Difficulty = QuestionDifficulty.Easy, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Valid Anagram", Url = "https://leetcode.com/problems/valid-anagram/description/", IsCompleted = false, Difficulty = QuestionDifficulty.Easy, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Two Sum", Url = "https://leetcode.com/problems/two-sum/description/", IsCompleted = false, Difficulty = QuestionDifficulty.Easy, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Group Anagrams", Url = "https://leetcode.com/problems/group-anagrams/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Top K Frequent Elements", Url = "https://leetcode.com/problems/top-k-frequent-elements/description/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Product of Array Except Self", Url = "https://leetcode.com/problems/product-of-array-except-self/description/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Valid Sudoku", Url = "https://leetcode.com/problems/valid-sudoku/description/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Longest Consecutive Sequence", Url = "https://leetcode.com/problems/longest-consecutive-sequence/description/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright }


            };

            var twoPointersProblems = new List<Problem>
            {
                new Problem { Title = "Valid Palindrome", Url = "https://leetcode.com/problems/valid-palindrome/", IsCompleted = false, Difficulty = QuestionDifficulty.Easy, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Two Sum 2 Input Array is Sorted", Url = "https://leetcode.com/problems/two-sum-ii-input-array-is-sorted/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "3 Sum", Url = "https://leetcode.com/problems/3sum/description/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Container with Most Water", Url = "https://leetcode.com/problems/container-with-most-water/description/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Trapping Rain Water", Url = "https://leetcode.com/problems/trapping-rain-water/description/", IsCompleted = false, Difficulty = QuestionDifficulty.Hard, SkillLevel = SkillLevel.alright }


            };
            var slidingWindowProblems = new List<Problem>
            {
                new Problem { Title = "Best Time to Buy and Sell Stock", Url = "https://leetcode.com/problems/best-time-to-buy-and-sell-stock/", IsCompleted = false, Difficulty = QuestionDifficulty.Easy, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Longest Substring Without Repeating Characters", Url = "https://leetcode.com/problems/longest-substring-without-repeating-characters/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Longest Repeating Character Replacement", Url = "https://leetcode.com/problems/longest-repeating-character-replacement/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Permutation In String", Url = "https://leetcode.com/problems/permutation-in-string/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Minimum Window Substring", Url = "https://leetcode.com/problems/minimum-window-substring/", IsCompleted = false, Difficulty = QuestionDifficulty.Hard, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Sliding Window Maximum", Url = "https://leetcode.com/problems/sliding-window-maximum/description/", IsCompleted = false, Difficulty = QuestionDifficulty.Hard, SkillLevel = SkillLevel.alright }

            };
            var stackProblems = new List<Problem>
            {
                new Problem { Title = "Valid Parentheses", Url = "https://leetcode.com/problems/valid-parentheses/description/", IsCompleted = false, Difficulty = QuestionDifficulty.Easy, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Min Stack", Url = "https://leetcode.com/problems/min-stack/description/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Evaluate Reverse Polish Notation", Url = "https://leetcode.com/problems/evaluate-reverse-polish-notation/description/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Generate Parentheses", Url = "https://leetcode.com/problems/generate-parentheses/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Daily Temperatures", Url = "https://leetcode.com/problems/daily-temperatures/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Car Fleet", Url = "https://leetcode.com/problems/car-fleet/description/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Largest Rectangle in Histogram", Url = "https://leetcode.com/problems/largest-rectangle-in-histogram/description/", IsCompleted = false, Difficulty = QuestionDifficulty.Hard, SkillLevel = SkillLevel.alright }

            };
            var binarySearchProblems = new List<Problem>
            {
                new Problem { Title = "Binary Search", Url = "https://leetcode.com/problems/binary-search/", IsCompleted = false, Difficulty = QuestionDifficulty.Easy, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Search a 2D Matrix", Url = "https://leetcode.com/problems/search-a-2d-matrix/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Koko Eating Bananas", Url = "https://leetcode.com/problems/koko-eating-bananas/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Find Minimum in Rotated Sorted Array", Url = "https://leetcode.com/problems/find-minimum-in-rotated-sorted-array/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Time Based Key Value Store", Url = "https://leetcode.com/problems/time-based-key-value-store/description/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Median of Two Sorted Arrays", Url = "https://leetcode.com/problems/median-of-two-sorted-arrays/", IsCompleted = false, Difficulty = QuestionDifficulty.Hard, SkillLevel = SkillLevel.alright }

            };

            var linkedListProblems = new List<Problem>
            {
                new Problem { Title = "Reverse Linked List", Url = "https://leetcode.com/problems/reverse-linked-list/", IsCompleted = false, Difficulty = QuestionDifficulty.Easy, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Merge Two Sorted Lists", Url = "https://leetcode.com/problems/merge-two-sorted-lists/", IsCompleted = false, Difficulty = QuestionDifficulty.Easy, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Reorder List", Url = "https://leetcode.com/problems/reorder-list/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Remove Nth Node from End of List", Url = "https://leetcode.com/problems/remove-nth-node-from-end-of-list/description/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Copy List with Random Pointer", Url = "https://leetcode.com/problems/copy-list-with-random-pointer/description/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Add Two Numbers", Url = "https://leetcode.com/problems/add-two-numbers/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Linked List Cycle", Url = "https://leetcode.com/problems/linked-list-cycle/description/", IsCompleted = false, Difficulty = QuestionDifficulty.Easy, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Find the Duplicate Number", Url = "https://leetcode.com/problems/find-the-duplicate-number/description/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "LRU Cache", Url = "https://leetcode.com/problems/lru-cache/description/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Merge K Sorted Lists", Url = "https://leetcode.com/problems/merge-k-sorted-lists/description/", IsCompleted = false, Difficulty = QuestionDifficulty.Hard, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Reverse Nodes in K Group", Url = "https://leetcode.com/problems/reverse-nodes-in-k-group/description/", IsCompleted = false, Difficulty = QuestionDifficulty.Hard, SkillLevel = SkillLevel.alright }

            };
            var treeProblems = new List<Problem>
            {
                new Problem { Title = "Invert Binary Tree", Url = "https://leetcode.com/problems/invert-binary-tree/", IsCompleted = false, Difficulty = QuestionDifficulty.Easy, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Maximum Depth of Binary Tree", Url = "https://leetcode.com/problems/maximum-depth-of-binary-tree/", IsCompleted = false, Difficulty = QuestionDifficulty.Easy, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Diameter of Binary Tree", Url = "https://leetcode.com/problems/diameter-of-binary-tree/description/", IsCompleted = false, Difficulty = QuestionDifficulty.Easy, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Balanced Binary Tree", Url = "https://leetcode.com/problems/balanced-binary-tree/", IsCompleted = false, Difficulty = QuestionDifficulty.Easy, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Same Tree", Url = "https://leetcode.com/problems/same-tree/", IsCompleted = false, Difficulty = QuestionDifficulty.Easy, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Subtree of Another Tree", Url = "https://leetcode.com/problems/subtree-of-another-tree/description/", IsCompleted = false, Difficulty = QuestionDifficulty.Easy, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Lowest Common Ancestor of Binary Search Tree", Url = "https://leetcode.com/problems/lowest-common-ancestor-of-a-binary-search-tree/description/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Binary Search Tree Level Order Traversal", Url = "https://leetcode.com/problems/binary-tree-level-order-traversal/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Right Side View of Binary Tree", Url = "https://leetcode.com/problems/binary-tree-right-side-view/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Count Good Nodes in Binary Tree", Url = "https://leetcode.com/problems/count-good-nodes-in-binary-tree/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Validate Binary Search Tree", Url = "https://leetcode.com/problems/validate-binary-search-tree/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Kth Smallest Element in a BST", Url = "https://leetcode.com/problems/kth-smallest-element-in-a-bst/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Consruct BST from Preorder and Inorder Traversal", Url = "https://leetcode.com/problems/construct-binary-tree-from-preorder-and-inorder-traversal/description/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Binary Tree Maximum Path Sum", Url = "https://leetcode.com/problems/binary-tree-maximum-path-sum/description/", IsCompleted = false, Difficulty = QuestionDifficulty.Hard, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Serialize and Deserialize Binary Tree", Url = "https://leetcode.com/problems/serialize-and-deserialize-binary-tree/description/", IsCompleted = false, Difficulty = QuestionDifficulty.Hard, SkillLevel = SkillLevel.alright }

            };
            var heapProblems = new List<Problem>
            {
                new Problem { Title = "Kth Largest Element in a Stream", Url = "https://leetcode.com/problems/kth-largest-element-in-a-stream/", IsCompleted = false, Difficulty = QuestionDifficulty.Easy, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Last Stone Weight", Url = "https://leetcode.com/problems/last-stone-weight/", IsCompleted = false, Difficulty = QuestionDifficulty.Easy, SkillLevel = SkillLevel.alright },
                new Problem { Title = "K Closest Points to Origin", Url = "https://leetcode.com/problems/k-closest-points-to-origin/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Kth Largest Element in an Array", Url = "https://leetcode.com/problems/kth-largest-element-in-an-array/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Task Scheduler", Url = "https://leetcode.com/problems/task-scheduler/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Design Twitter", Url = "https://leetcode.com/problems/design-twitter/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Find Median From Data Source", Url = "https://leetcode.com/problems/find-median-from-data-stream/", IsCompleted = false, Difficulty = QuestionDifficulty.Hard, SkillLevel = SkillLevel.alright }

            };

            var backtrackingProblems = new List<Problem> {
            new Problem { Title = "Subsets", Url = "https://leetcode.com/problems/subsets/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
            new Problem { Title = "Combination Sum", Url = "https://leetcode.com/problems/combination-sum/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
            new Problem { Title = "Permutations", Url = "https://leetcode.com/problems/permutations/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
            new Problem { Title = "Subsets 2", Url = "https://leetcode.com/problems/subsets-ii/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
            new Problem { Title = "Combination Sum 2", Url = "https://leetcode.com/problems/combination-sum-ii/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
            new Problem { Title = "Word Search", Url = "https://leetcode.com/problems/word-search/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
            new Problem { Title = "Palindrome Partitioning", Url = "https://leetcode.com/problems/palindrome-partitioning/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
            new Problem { Title = "Letter Combinations of a Phone Number", Url = "https://leetcode.com/problems/letter-combinations-of-a-phone-number/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
            new Problem { Title = "N Queens", Url = "https://leetcode.com/problems/n-queens/", IsCompleted = false, Difficulty = QuestionDifficulty.Hard, SkillLevel = SkillLevel.alright }

            };

            var trieProblems = new List<Problem>
            {
                new Problem { Title = "Implement Trie (Prefix Tree)", Url = "https://leetcode.com/problems/implement-trie-prefix-tree/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Design Add and Search Words Data Structure", Url = "https://leetcode.com/problems/design-add-and-search-words-data-structure/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Word Search II", Url = "https://leetcode.com/problems/word-search-ii/", IsCompleted = false, Difficulty = QuestionDifficulty.Hard, SkillLevel = SkillLevel.alright }

            };

            var GraphProblems = new List<Problem>
            {
                new Problem { Title = "Number of Islands", Url = "https://leetcode.com/problems/number-of-islands/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Max Area of Island", Url = "https://leetcode.com/problems/max-area-of-island/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Clone Graph", Url = "https://leetcode.com/problems/clone-graph/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Rotting Oranges", Url = "https://leetcode.com/problems/rotting-oranges/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Pacific Atlantic Water Flow", Url = "https://leetcode.com/problems/pacific-atlantic-water-flow/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Surrounded Regions", Url = "https://leetcode.com/problems/surrounded-regions/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Course Schedule", Url = "https://leetcode.com/problems/course-schedule/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Course Schedule II", Url = "https://leetcode.com/problems/course-schedule-ii/description/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Redundant Connection", Url = "https://leetcode.com/problems/redundant-connection/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Word Ladder", Url = "https://leetcode.com/problems/word-ladder/", IsCompleted = false, Difficulty = QuestionDifficulty.Hard, SkillLevel = SkillLevel.alright }

            };
            var advancedGraphProblems = new List<Problem>
            {
                new Problem { Title = "Reconstruct Itinerary", Url = "https://leetcode.com/problems/reconstruct-itinerary/", IsCompleted = false, Difficulty = QuestionDifficulty.Hard, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Min Cost to Connect All Points", Url = "https://leetcode.com/problems/min-cost-to-connect-all-points/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Network Delay Time", Url = "https://leetcode.com/problems/network-delay-time/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Swim in Rising Water", Url = "https://leetcode.com/problems/swim-in-rising-water/", IsCompleted = false, Difficulty = QuestionDifficulty.Hard, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Cheapest Flights Within K Stops", Url = "https://leetcode.com/problems/cheapest-flights-within-k-stops/description/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright }

            };
            var dynamic1 = new List<Problem>
            {
                new Problem { Title = "Climbing Stairs", Url = "https://leetcode.com/problems/climbing-stairs/", IsCompleted = false, Difficulty = QuestionDifficulty.Easy, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Min Cost Climbing Stairs", Url = "https://leetcode.com/problems/min-cost-climbing-stairs/", IsCompleted = false, Difficulty = QuestionDifficulty.Easy, SkillLevel = SkillLevel.alright },
                new Problem { Title = "House Robber", Url = "https://leetcode.com/problems/house-robber/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "House Robber 2", Url = "https://leetcode.com/problems/house-robber-ii/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Longest Palindromic Substring", Url = "https://leetcode.com/problems/longest-palindromic-substring/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Palindromic Substrings", Url = "https://leetcode.com/problems/palindromic-substrings/description/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Decode Ways", Url = "https://leetcode.com/problems/decode-ways/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Coin Change", Url = "https://leetcode.com/problems/coin-change/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Maximum Product Subarray", Url = "https://leetcode.com/problems/maximum-product-subarray/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Word Break", Url = "https://leetcode.com/problems/word-break/description/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Longest Increasing Subsequence", Url = "https://leetcode.com/problems/longest-increasing-subsequence/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Partition Equal Subset Sum", Url = "https://leetcode.com/problems/partition-equal-subset-sum/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright }

            };
            var dynamic2 = new List<Problem>
            {
                new Problem { Title = "Unique Paths", Url = "https://leetcode.com/problems/unique-paths/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Longest Common Subsequence", Url = "https://leetcode.com/problems/longest-common-subsequence/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Best Time to Buy and Sell Stock with Cooldown", Url = "https://leetcode.com/problems/best-time-to-buy-and-sell-stock-with-cooldown/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Coin Change 2", Url = "https://leetcode.com/problems/coin-change-ii/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Target Sum", Url = "https://leetcode.com/problems/target-sum/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Interleaving String", Url = "https://leetcode.com/problems/interleaving-string/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Longest Increasing Path in a Matrix", Url = "https://leetcode.com/problems/longest-increasing-path-in-a-matrix/", IsCompleted = false, Difficulty = QuestionDifficulty.Hard, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Direct Subsequences", Url = "https://leetcode.com/problems/distinct-subsequences/", IsCompleted = false, Difficulty = QuestionDifficulty.Hard, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Edit Distance", Url = "https://leetcode.com/problems/edit-distance/description/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Burst Balloons", Url = "https://leetcode.com/problems/burst-balloons/", IsCompleted = false, Difficulty = QuestionDifficulty.Hard, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Regular Expression Matching", Url = "https://leetcode.com/problems/regular-expression-matching/", IsCompleted = false, Difficulty = QuestionDifficulty.Hard, SkillLevel = SkillLevel.alright }

            };
            var greedyProblems = new List<Problem>
            {
                new Problem { Title = "Maximum Subarray", Url = "https://leetcode.com/problems/maximum-subarray/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Jump Game", Url = "https://leetcode.com/problems/jump-game/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Gas Station", Url = "https://leetcode.com/problems/gas-station/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Hand of Straights", Url = "https://leetcode.com/problems/hand-of-straights/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Merge Triplets to Form Target Triplet", Url = "https://leetcode.com/problems/merge-triplets-to-form-target-triplet/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Partition Labels", Url = "https://leetcode.com/problems/partition-labels/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Valid Parentheses String", Url = "https://leetcode.com/problems/valid-parenthesis-string/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Jump Game 2", Url = "https://leetcode.com/problems/jump-game-ii/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright }

            };
            var intervalProblems = new List<Problem>
            {
                new Problem { Title = "Insert Interval", Url = "https://leetcode.com/problems/insert-interval/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Merge Intervals", Url = "https://leetcode.com/problems/merge-intervals/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Non-Overlapping Intervals", Url = "https://leetcode.com/problems/non-overlapping-intervals/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Minimum Interval to Include Each Query", Url = "https://leetcode.com/problems/minimum-interval-to-include-each-query/description/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright }

            };
            var mathProblems = new List<Problem>
            {
                new Problem { Title = "Rotate Image", Url = "https://leetcode.com/problems/rotate-image/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Spiral Matrix", Url = "https://leetcode.com/problems/spiral-matrix/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Set Matrix Zeros", Url = "https://leetcode.com/problems/set-matrix-zeroes/description/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Happy Number", Url = "https://leetcode.com/problems/happy-number/", IsCompleted = false, Difficulty = QuestionDifficulty.Easy, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Plus One", Url = "https://leetcode.com/problems/plus-one/", IsCompleted = false, Difficulty = QuestionDifficulty.Easy, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Pow (X,N)", Url = "https://leetcode.com/problems/powx-n/description/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Multiply Strings", Url = "https://leetcode.com/problems/multiply-strings/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Detect Squares", Url = "https://leetcode.com/problems/detect-squares/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright }

            };
            var bitProblems = new List<Problem>
            {
                new Problem { Title = "Single Number", Url = "https://leetcode.com/problems/single-number/", IsCompleted = false, Difficulty = QuestionDifficulty.Easy, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Number of 1 Bits", Url = "https://leetcode.com/problems/number-of-1-bits/", IsCompleted = false, Difficulty = QuestionDifficulty.Easy, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Counting Bits", Url = "https://leetcode.com/problems/counting-bits/", IsCompleted = false, Difficulty = QuestionDifficulty.Easy, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Reverse Bits", Url = "https://leetcode.com/problems/reverse-bits/description/", IsCompleted = false, Difficulty = QuestionDifficulty.Easy, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Missing Number", Url = "https://leetcode.com/problems/missing-number/", IsCompleted = false, Difficulty = QuestionDifficulty.Easy, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Sum of Two Integers", Url = "https://leetcode.com/problems/sum-of-two-integers/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright },
                new Problem { Title = "Reverse Integer", Url = "https://leetcode.com/problems/reverse-integer/", IsCompleted = false, Difficulty = QuestionDifficulty.Medium, SkillLevel = SkillLevel.alright }

            };
            var categories = new List<Category>
            {
                new Category { Name = "Arrays & Hashing", Problems = arrayProblems },
                new Category { Name = "Two Pointers", Problems = twoPointersProblems },
                new Category{ Name = "Sliding Window", Problems = slidingWindowProblems },
                new Category {Name = "Stack", Problems = stackProblems },
                new Category {Name = "Binary Search", Problems = binarySearchProblems },
                new Category {Name = "Linked Lists", Problems = linkedListProblems},
                new Category {Name = "Heap/Priority Queue", Problems = heapProblems},
                new Category {Name = "Trees", Problems = treeProblems},
                new Category { Name = "Backtracking", Problems = backtrackingProblems },
                new Category {Name = "Tries", Problems = trieProblems },
                new Category  {Name = "Graphs", Problems = GraphProblems},
                new Category {Name = "Advanced Graphs", Problems = advancedGraphProblems },
                new Category {Name = "1D Dynamic Programming", Problems = dynamic1},
                new Category {Name = "2D Dynamic Programming", Problems = dynamic2},
                new Category {Name = "Greedy", Problems = greedyProblems},
                new Category {Name = "Intervals", Problems = intervalProblems },
                new Category {Name = "Math & Geometry",Problems = mathProblems },
                new Category {Name = "Bit Manipulation", Problems = bitProblems},
            };

            return new ProblemSet
            {
                Categories = categories
            };
        }
    }
}
