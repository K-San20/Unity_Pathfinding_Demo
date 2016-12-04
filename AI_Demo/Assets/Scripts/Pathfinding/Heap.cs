using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Heap<T> where T : IHeapItem<T>
{
    T[] m_items;
    int m_currItemCount;

    public Heap(int maxHeapSize)
    {
        m_items = new T[maxHeapSize];
    }

    public void Add(T toAdd)
    {
        toAdd.HeapIndex = m_currItemCount;
        m_items[m_currItemCount] = toAdd;
        SortUp(toAdd);
        m_currItemCount++;
    }

    public T RemoveFirst()
    {
        T firstItem = m_items[0];
        m_currItemCount--;
        m_items[0] = m_items[m_currItemCount];
        m_items[0].HeapIndex = 0;
        SortDown(m_items[0]);
        return firstItem;
    }

    public void UpdateItem(T toUpdate)
    {
        SortUp(toUpdate);
    }

    public bool Contains(T toCheck)
    {
        return Equals(m_items[toCheck.HeapIndex], toCheck);
    }

    public int Count { get { return m_currItemCount; } }

    void SortUp(T toSort)
    {
        int parentIndex = (toSort.HeapIndex - 1) / 2;

        while(true)
        {
            T parentItem = m_items[parentIndex];
            if (toSort.CompareTo(parentItem) > 0)
            {
                Swap(toSort, parentItem);
            }
            else
            {
                break;
            }

            parentIndex = (toSort.HeapIndex - 1) / 2;
        }
    }

    void SortDown(T toSort)
    {
        while(true)
        {
            int leftChildIndex = toSort.HeapIndex * 2 + 1;
            int rightChildIndex = toSort.HeapIndex * 2 + 2;
            int swapIndex = 0;

            if(leftChildIndex < m_currItemCount)
            {
                swapIndex = leftChildIndex;

                if(rightChildIndex < m_currItemCount)
                    if (m_items[leftChildIndex].CompareTo(m_items[rightChildIndex]) < 0)
                        swapIndex = rightChildIndex;

                if (toSort.CompareTo(m_items[swapIndex]) < 0)
                    Swap(toSort, m_items[swapIndex]);
                else
                    return;
            }
            else
                return;
        }
    }

    void Swap(T itemA, T itemB)
    {
        m_items[itemA.HeapIndex] = itemB;
        m_items[itemB.HeapIndex] = itemA;
        int itemAIndex = itemA.HeapIndex;
        itemA.HeapIndex = itemB.HeapIndex;
        itemB.HeapIndex = itemAIndex;
    }
}

public interface IHeapItem<T> : IComparable<T>
{
    int HeapIndex { get; set; }
}
