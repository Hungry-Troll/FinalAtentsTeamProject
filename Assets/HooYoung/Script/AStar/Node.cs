using UnityEngine;
using System.Collections;

// �� ��忡 ����� Ŭ����
//
// �ֿ� ���
// 1. �̵� ������ ����ΰ�?(bool)
// 2. �ش� ����� ��ǥ(Vector3)
// 3. ����� ���ذ�(== �Ÿ���)(int)

public class Node : IHeapItem<Node>
{

	public bool walkable;
	public Vector3 worldPosition;
	public int gridX;
	public int gridY;

	// start ���������� �Ÿ�
	public int gCost;
	// target ���������� �Ÿ�
	public int hCost;
	public Node parent;
	int heapIndex;

	public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY)
	{
		walkable = _walkable;
		worldPosition = _worldPos;
		gridX = _gridX;
		gridY = _gridY;
	}

	// �� ����� ���ذ�
	// fCost = gCost + hCost
	public int fCost
	{
		get
		{
			return gCost + hCost;
		}
	}

	public int HeapIndex
	{
		get
		{
			return heapIndex;
		}
		set
		{
			heapIndex = value;
		}
	}

	public int CompareTo(Node nodeToCompare)
	{
		int compare = fCost.CompareTo(nodeToCompare.fCost);
		if (compare == 0)
		{
			compare = hCost.CompareTo(nodeToCompare.hCost);
		}
		return -compare;
	}
}