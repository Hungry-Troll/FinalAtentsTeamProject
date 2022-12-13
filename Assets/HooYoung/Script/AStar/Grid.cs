using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// 3차원 공간을 격자(그리드)로 나타내주는 스크립트

public class Grid : MonoBehaviour
{
	// 에디터에서 체크하면 기즈모 그림
	public bool displayGridGizmos;
	// 겹치는 콜라이더 검출할 때 사용할 layerMask, 장애물에 적용
	public LayerMask unwalkableMask;
	// 전체 공간 크기. 현재 에디터에서 설정
	public Vector2 gridWorldSize;
	// 각 노드의 반경. 현재 에디터에서 설정
	public float nodeRadius;
	// 전체 공간에 채워질 노드 배열(집합)
	Node[,] grid;

	// 노드 한 변의 길이
	float nodeDiameter;
	// 전체 공간 가로(gridSizeX), 세로(gridSizeY) 길이 == 노드의 개수
	int gridSizeX, gridSizeY;

	void Awake()
	{
		// 노드의 길이 = 반경 x 2
		nodeDiameter = nodeRadius * 2;
		// 그리드의 가로 크기 == (전체 공간 가로길이 / 노드 한 변의 길이) 의 반올림 값
		gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
		// 그리드의 세로 크기 == (전체 공간 세로길이 / 노드 한 변의 길이) 의 반올림 값
		gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
		// 그리드 생성
		CreateGrid();
	}

	public int MaxSize
	{
		get
		{
			return gridSizeX * gridSizeY;
		}
	}

	// 그리드 생성하는 함수
	void CreateGrid()
	{
		// 전체 그리드 생성 [그리드의 가로 노드 개수 x 그리드의 세로 노드 개수]
		grid = new Node[gridSizeX, gridSizeY];
		
		// 좌하단 값 구하기/ 전체 공간의 (0, 0, 0) 값
		// 현재 위치(== 정중앙) - (전체 가로 / 2) - (전체 세로 / 2)
		Vector3 worldBottomLeft = 
			transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

		// 그리드 개수만큼 반복(가로 x 세로)
		for (int x = 0; x < gridSizeX; x++)
		{
			for (int y = 0; y < gridSizeY; y++)
			{
				// 노드의 중점 계산
				// 그리드의 (0, 0, 0) + (1 x 한 변의 길이 + 가로 반경) + (1 x 한 변의 길이 + 세로 반경)
				Vector3 worldPoint = 
					worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) 
											+ Vector3.forward * (y * nodeDiameter + nodeRadius);

				/*
                */
                worldPoint.y += 100f;
                RaycastHit hitInfo;
                int layerMask = 1 << LayerMask.NameToLayer("Terrain");
				bool walkable = true;
				// 경사 파악해서 걸을 수 있는 곳인지 아닌지 판단하는 코드, 수정필요할 수 있음
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

				// walkable 판단 : CheckSphere(중심점, 반경) : 콜라이더 겹치면 true 반환
				// 겹치지 않을 때만 walkable 에 true 대입
				walkable = walkable ? !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask)) : false;
				// 노드 생성(이동 가능 판단 값, 중점, 그리드X 값, 그리드Y 값)
				grid[x, y] = new Node(walkable, worldPoint, x, y);
			}
		}
	}

	// 이웃 노드 구하는 함수
	public List<Node> GetNeighbours(Node node)
	{
		// 노드 담을 List
		List<Node> neighbours = new List<Node>();

		// 현재 위치에서 주위에 있는 노드 가져오기 위해서 더해줄 좌표값
		// 현재 노드에서 각각 + (-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1)
		for (int x = -1; x <= 1; x++)
		{
			for (int y = -1; y <= 1; y++)
			{
				// 자기 자신 노드이면 continue
				if (x == 0 && y == 0)
					continue;

				// 이웃이 될 x, y 좌표 계산
				int checkX = node.gridX + x;
				int checkY = node.gridY + y;

				// 각 x, y 값은 당연히 그리드 안에 포함되어야 한다
				if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
				{
					// 리스트에 추가
					neighbours.Add(grid[checkX, checkY]);
				}
			}
		}

		return neighbours;
	}


	// 월드좌표를 노드로 바꾸는 함수
	public Node NodeFromWorldPoint(Vector3 worldPosition)
	{
		float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
		float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;
		// Clamp01(float value) : value가 0보다 작으면 0리턴, 1보다 크면 1리턴, 사이값은 그대로 리턴
		percentX = Mathf.Clamp01(percentX);
		percentY = Mathf.Clamp01(percentY);

		// RoundToInt(float value) : 반올림 함수
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