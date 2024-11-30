using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeapPQ
{
    public class Heap<TValue, TPriority>
        where TPriority : IComparable<TPriority>
    {
        public struct PriorityQueueItem
        {
            public TValue Value { get; set; }
            public TPriority Priority { get; set; }
            public PriorityQueueItem(TValue value, TPriority priority)
            {
                Value = value;
                Priority = priority;
            }

        }

        private List<PriorityQueueItem> items = [];
        public int Count => items.Count;
        public PriorityQueueItem RootNode => items[0];
        public PriorityQueueItem LastNode => items[^1];

        public Heap() { }
        public Heap(TValue value, TPriority priority) => items.Add(new PriorityQueueItem(value, priority));

        private int LeftChildIndex(int parentIndex) => 2 * parentIndex + 1;
        private int RightChildIndex(int parentIndex) => 2 * parentIndex + 2;
        private int ParentIndex(int childIndex) => (childIndex - 1) / 2;

        public void Push(TValue value, TPriority priority)
        {
            int newNodeIndex = items.Count;
            items.Add(new PriorityQueueItem(value, priority));

            // Comparing the new value to the parent value
            while (newNodeIndex > 0
                && items[newNodeIndex].Priority.CompareTo(items[ParentIndex(newNodeIndex)].Priority) > 0)
            {
                // If the new node is greater than its parent, a swap must occur

                PriorityQueueItem temp = items[newNodeIndex];
                items[newNodeIndex] = items[ParentIndex(newNodeIndex)];
                items[ParentIndex(newNodeIndex)] = temp;

                // Trickle up the heap
                newNodeIndex = ParentIndex(newNodeIndex);
            }
        }

        public TValue Pop()
        {
            PriorityQueueItem root = items[0];
            items[0] = items[^1];
            items.RemoveAt(items.Count - 1);

            int trickleNodeIndex = 0;

            while (HasGreaterChild(trickleNodeIndex))
            {
                int largerChildIndex = CalculateLargerChildIndex(trickleNodeIndex);

                PriorityQueueItem temp = items[trickleNodeIndex];
                items[trickleNodeIndex] = items[largerChildIndex];
                items[largerChildIndex] = temp;

                trickleNodeIndex = largerChildIndex;
            }

            return root.Value;
        }

        private bool HasGreaterChild(int index)
        {
            int leftChildIndex = LeftChildIndex(index);
            int rightChildIndex = RightChildIndex(index);

            // if leftChildIndex > items.Count, the next part would throw an exception
            // The two && sections are checked, then the ||
            return (leftChildIndex < items.Count && items[index].Priority.CompareTo(items[leftChildIndex].Priority) < 0 ||
                    rightChildIndex < items.Count && items[index].Priority.CompareTo(items[rightChildIndex].Priority) < 0);
        }

        private int CalculateLargerChildIndex(int index)
        {
            // Checks if the right child exists
            if (RightChildIndex(index) <= items.Count)
            {
                return LeftChildIndex(index);
            }

            // Child on the right has the larger value
            if (items[RightChildIndex(index)].Priority.CompareTo(items[LeftChildIndex(index)].Priority) > 0)
            {
                return RightChildIndex(index);
            }

            return LeftChildIndex(index);
        }

        public override String ToString()
        {
            return ToString(0, 0);
        }

        private String ToString(int index, int level)
        {
            // Base Case
            if (index >= items.Count)
            {
                return String.Empty;
            }

            StringBuilder result = new StringBuilder();

            // Right Subtree
            result.Append(ToString(RightChildIndex(index), level + 1));

            result.Append(new String('\t', level));

            // Current Node
            result.AppendLine(items[index].Value.ToString());

            // Left Subtree
            result.Append(ToString(LeftChildIndex(index), level + 1));

            return result.ToString();
        }
    }
}
