using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScr : MonoBehaviour
{
    public static GameObject startPosition = null;

    public static GameObject endPosition = null;

    [SerializeField]
    private List<Cell> graph = new List<Cell>();

    private List<List<GameObject>> findPathes = new List<List<GameObject>>();
    // Start is called before the first frame update
    void Start()
    {
        makeGraph();
        DijkstraAlgorithm = new DijkstraAlgorithm<GameObject>();
    }

    private void makeGraph()
    {
        otherGraph = new Dictionary<GameObject, Dictionary<GameObject, List<GameObject>>>();
        foreach (var cell in graph)
        {
            otherGraph.Add(cell.startVertex, new Dictionary<GameObject, List<GameObject>>());
            foreach (InnerVertex cellInnerVertex in cell.innerVertices)
            {
                otherGraph[cell.startVertex].Add(cellInnerVertex.innerTop, cellInnerVertex.path);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && oldPath != null)
        {
            if (oldPath != null)
            {
                coloredPath(oldPath, Color.white);
            }
            startPosition = null;
            endPosition = null;
            oldStartPosition = null;
            oldEndPosition = null;
            oldPath = null;
        }
        
        if (startPosition != null && startPosition != oldStartPosition)
        {
            makeGraph();
            findPathes = DijkstraAlgorithm.FindPathes(startPosition, otherGraph);
            oldStartPosition = startPosition;
        }

        if (endPosition != null && endPosition != oldEndPosition && startPosition != endPosition)
        {
            var path = findPathes.Find(e => e.Count > 0 && Equals(e[e.Count - 1], endPosition));
            if (path != null)
            {
                if (oldPath != null)
                {
                    coloredPath(oldPath, Color.white);
                    for (int j = 0; j < oldEndPosition.transform.childCount; j++)
                    {
                        oldEndPosition.transform.GetChild(j).GetComponent<SpriteRenderer>().color = Color.white;
                    }
                }

                fillOldPath(path);
                coloredPath(path, new Color(0, 255, 0, 0.5f));
                oldEndPosition = endPosition;
            }

        }
    }

    private void fillOldPath(List<GameObject> path)
    {
        oldPath = new List<GameObject>(path);
    }

    private void coloredPath(List<GameObject> path, Color color)
    {
        for (var i = 0; i < path.Count - 1; i++)
        {
            List<GameObject> objects = graph.Find(e => Equals(e.startVertex, path[i])).
                innerVertices.Find(e => Equals(e.innerTop, path[i + 1])).path;
            List<GameObject> gameObjects = new List<GameObject>(objects);
            gameObjects.Add(path[i]);
            gameObjects.Add(endPosition);
            ColoredCell(gameObjects, color);
        }
    }

    private static void ColoredCell(List<GameObject> gameObjects, Color color)
    {
        foreach (var pos in gameObjects)
        {
            for (int j = 0; j < pos.transform.childCount; j++)
            {
                pos.transform.GetChild(j).GetComponent<SpriteRenderer>().color = color;
            }
        }
    }

    private Dictionary<GameObject, Dictionary<GameObject, List<GameObject>>> otherGraph;
    private DijkstraAlgorithm<GameObject> DijkstraAlgorithm;
    private GameObject oldStartPosition;
    private GameObject oldEndPosition;
    private List<GameObject> oldPath;
}
