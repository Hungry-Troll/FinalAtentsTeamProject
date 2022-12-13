using UnityEngine;
using System.Collections;

// 각 노드에 적용될 클래스
//
// 주요 멤버
// 1. 이동 가능한 노드인가?(bool)
// 2. 해당 노드의 좌표(Vector3)
// 3. 노드의 기준값(== 거리값)(int)

public class Node : IHeapItem<Node>
{

	public bool walkable;
	public Vector3 worldPosition;
	public int gridX;
	public int gridY;

	// start 지점부터의 거리
	public int gCost;
	// target 지점까지의 거리
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

	// 각 노드의 기준값
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