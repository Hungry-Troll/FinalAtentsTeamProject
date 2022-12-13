using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// 3���� ������ ����(�׸���)�� ��Ÿ���ִ� ��ũ��Ʈ

public class Grid : MonoBehaviour
{
	// �����Ϳ��� üũ�ϸ� ����� �׸�
	public bool displayGridGizmos;
	// ��ġ�� �ݶ��̴� ������ �� ����� layerMask, ��ֹ��� ����
	public LayerMask unwalkableMask;
	// ��ü ���� ũ��. ���� �����Ϳ��� ����
	public Vector2 gridWorldSize;
	// �� ����� �ݰ�. ���� �����Ϳ��� ����
	public float nodeRadius;
	// ��ü ������ ä���� ��� �迭(����)
	Node[,] grid;

	// ��� �� ���� ����
	float nodeDiameter;
	// ��ü ���� ����(gridSizeX), ����(gridSizeY) ���� == ����� ����
	int gridSizeX, gridSizeY;

	void Awake()
	{
		// ����� ���� = �ݰ� x 2
		nodeDiameter = nodeRadius * 2;
		// �׸����� ���� ũ�� == (��ü ���� ���α��� / ��� �� ���� ����) �� �ݿø� ��
		gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
		// �׸����� ���� ũ�� == (��ü ���� ���α��� / ��� �� ���� ����) �� �ݿø� ��
		gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
		// �׸��� ����
		CreateGrid();
	}

	public int MaxSize
	{
		get
		{
			return gridSizeX * gridSizeY;
		}
	}

	// �׸��� �����ϴ� �Լ�
	void CreateGrid()
	{
		// ��ü �׸��� ���� [�׸����� ���� ��� ���� x �׸����� ���� ��� ����]
		grid = new Node[gridSizeX, gridSizeY];
		
		// ���ϴ� �� ���ϱ�/ ��ü ������ (0, 0, 0) ��
		// ���� ��ġ(== ���߾�) - (��ü ���� / 2) - (��ü ���� / 2)
		Vector3 worldBottomLeft = 
			transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

		// �׸��� ������ŭ �ݺ�(���� x ����)
		for (int x = 0; x < gridSizeX; x++)
		{
			for (int y = 0; y < gridSizeY; y++)
			{
				// ����� ���� ���
				// �׸����� (0, 0, 0) + (1 x �� ���� ���� + ���� �ݰ�) + (1 x �� ���� ���� + ���� �ݰ�)
				Vector3 worldPoint = 
					worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) 
											+ Vector3.forward * (y * nodeDiameter + nodeRadius);

				/*
                */
                worldPoint.y += 100f;
                RaycastHit hitInfo;
                int layerMask = 1 << LayerMask.NameToLayer("Terrain");
				bool walkable = true;
				// ��� �ľ��ؼ� ���� �� �ִ� ������ �ƴ��� �Ǵ��ϴ� �ڵ�, �����ʿ��� �� ����
                if(Physics.Raycast(worldPoint, -Vector3.up, out hitInfo, Mathf.Infinity, layerMask))
                {
                    worldPoint.y = hitInfo.point.y;
					float terrainNormal = Mathf.Abs(hitInfo.normal.y);
					if(terrainNormal <= 0.5f)
                    {
						Debug.Log("normal : " + hitInfo.normal);
						walkable = false;
                    }
                }

				// walkable �Ǵ� : CheckSphere(�߽���, �ݰ�) : �ݶ��̴� ��ġ�� true ��ȯ
				// ��ġ�� ���� ���� walkable �� true ����
				walkable = walkable ? !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask)) : false;
				// ��� ����(�̵� ���� �Ǵ� ��, ����, �׸���X ��, �׸���Y ��)
				grid[x, y] = new Node(walkable, worldPoint, x, y);
			}
		}
	}

	// �̿� ��� ���ϴ� �Լ�
	public List<Node> GetNeighbours(Node node)
	{
		// ��� ���� List
		List<Node> neighbours = new List<Node>();

		// ���� ��ġ���� ������ �ִ� ��� �������� ���ؼ� ������ ��ǥ��
		// ���� ��忡�� ���� + (-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1)
		for (int x = -1; x <= 1; x++)
		{
			for (int y = -1; y <= 1; y++)
			{
				// �ڱ� �ڽ� ����̸� continue
				if (x == 0 && y == 0)
					continue;

				// �̿��� �� x, y ��ǥ ���
				int checkX = node.gridX + x;
				int checkY = node.gridY + y;

				// �� x, y ���� �翬�� �׸��� �ȿ� ���ԵǾ�� �Ѵ�
				if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
				{
					// ����Ʈ�� �߰�
					neighbours.Add(grid[checkX, checkY]);
				}
			}
		}

		return neighbours;
	}


	// ������ǥ�� ���� �ٲٴ� �Լ�
	public Node NodeFromWorldPoint(Vector3 worldPosition)
	{
		float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
		float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;
		// Clamp01(float value) : value�� 0���� ������ 0����, 1���� ũ�� 1����, ���̰��� �״�� ����
		percentX = Mathf.Clamp01(percentX);
		percentY = Mathf.Clamp01(percentY);

		// RoundToInt(float value) : �ݿø� �Լ�
		int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
		int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
		return grid[x, y];
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));
		if (grid != null && displayGridGizmos)
		{
			foreach (Node n in grid)
			{
				Gizmos.color = (n.walkable) ? Color.white : Color.red;
				Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
			}
		}
	}
}