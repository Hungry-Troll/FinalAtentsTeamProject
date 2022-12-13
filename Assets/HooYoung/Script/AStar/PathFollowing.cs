using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollowing : MonoBehaviour
{
    public static List<Node> way = new List<Node>();

    void Start()
    {

    }

    void Update()
    {
            FollowingPath(transform.position);
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //Vector3.MoveTowards(transform.position, point, Time.deltaTime * 5f);
        }
    }

    public void FollowingPath(Vector3 startPos)
    {
        if(way != null)
        {
            Vector3 tmpTargetPos = Vector3.zero;
            for(int i = 0; i < way.Count; i++)
            {
                tmpTargetPos = way[i].worldPosition;
                Debug.Log("["+ way[i].gridX + ", " + way[i].gridY + "]");
            }
            transform.position = Vector3.MoveTowards(startPos, tmpTargetPos, Time.deltaTime * 5f);
            if(startPos == tmpTargetPos)
            {
                startPos = tmpTargetPos;
            }
            /*foreach(Node one in way)
            {
                if(startPos == one.worldPosition)
                    break;

                return one.worldPosition;
            }*/
        }
    }

    public static Vector3 FollowingPath(Vector3 startPos, Vector3 targetPos)
    {
        return Vector3.MoveTowards(startPos, targetPos, Time.deltaTime * 0.2f);
    }
}
