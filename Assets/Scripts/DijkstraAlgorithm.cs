using System;
using System.Collections.Generic;
using UnityEngine;

public class DijkstraAlgorithm<T> {
    private Dictionary<GameObject, GameObject> _prev;
    private Dictionary<GameObject, Dictionary<GameObject, List<GameObject>>> sourceGraph;
    

    public List<List<GameObject>> FindPathes(GameObject u, Dictionary<GameObject, Dictionary<GameObject, List<GameObject>>> sourceGraph)
    {
        this.sourceGraph = sourceGraph;
        HashSet<GameObject> used = new HashSet<GameObject>();
        Dictionary<GameObject, int> distance = new Dictionary<GameObject, int>();

        InitializeCollections(u, distance);
        DijkstraAlgo(used, distance);
        var andPrintPath = findAndGetPathes();
        return andPrintPath;
    }

    private void InitializeCollections(GameObject u, Dictionary<GameObject, int> distance) {
        _prev = new Dictionary<GameObject, GameObject>();
        foreach (var sourceGraphKey in sourceGraph.Keys)
        {
            distance.Add(sourceGraphKey, int.MaxValue);
        }
        distance[u] = 0;
        _prev.Add(u, null);
    }

    private void DijkstraAlgo(HashSet<GameObject> used, Dictionary<GameObject, int> distance) {
        for(;;)
        {
            GameObject v = null;
            foreach (GameObject e in sourceGraph.Keys) {
                if(!used.Contains(e) && distance[e] < int.MaxValue
                                     && (v == null || distance[v] > distance[e])){
                    v = e;
                }
            }
            if(v == null)
                break;
            used.Add(v);
            foreach (GameObject e in sourceGraph.Keys) {
                if (!used.Contains(e) && sourceGraph[v].ContainsKey(e)) {
                    int min = Math.Min(distance[e], distance[v] + sourceGraph[v][e].Count);
                    if(min != distance[e])
                    {
                        _prev[e] = v;
                    } 
                    distance[e] = min;
                }
            }
        }
    }

    private List<List<GameObject>> findAndGetPathes() {
        List<List<GameObject>> pathes = new List<List<GameObject>>();
        foreach (var key in sourceGraph.Keys)
        {
            List<GameObject> path = new List<GameObject>();
            GameObject a = key;
            while (a != null)
            {
                path.Add(a);
                a = _prev[a];
            }
            path.Reverse();
            pathes.Add(path);
        }
        return pathes;
    }
}