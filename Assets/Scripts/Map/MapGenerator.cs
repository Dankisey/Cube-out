using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private Transform _mapParent;
    [SerializeField] private Cube _prefab;
    [SerializeField] private Vector3Int _mapSize;

    private CubeDraft[,,] _mapDraft;

    private void OnValidate()
    {
        _mapSize.x = Mathf.Abs(_mapSize.x);
        _mapSize.y = Mathf.Abs(_mapSize.y);
        _mapSize.z = Mathf.Abs(_mapSize.z);
    }

    [ProButton]
    public void Generate()
    {
        if (_mapSize.x == 0 || _mapSize.y == 0 || _mapSize.z == 0)
            throw new ArgumentOutOfRangeException(nameof(_mapSize));

        if (_mapParent.childCount != 0)
        {
            List<Collider> children = new();
            _mapParent.GetComponentsInChildren(children);

            foreach (Collider child in children)
                DestroyImmediate(child.gameObject);
        }

        ResetDraft();
        FillDraft();
        InstantiateMap();
    }

    private void InstantiateMap()
    {
        Vector3 center = new Vector3(_mapSize.x - 1, _mapSize.y - 1, _mapSize.z - 1) / 2;

        for (int x = 0; x < _mapSize.x; x++)
        {
            for (int y = 0; y < _mapSize.y; y++)
            {
                for (int z = 0; z < _mapSize.z; z++)
                {
                    Cube cube = Instantiate(_prefab, _mapParent);
                    cube.transform.localPosition = GetPosition(center, new Vector3(x, y, z));
                    cube.SetRotation(_mapDraft[x, y, z].Direction);
                }
            }
        }
    }

    private Vector3 GetPosition(Vector3 center, Vector3 arrayCoordinate)
    {
        return -(center - arrayCoordinate);
    }

    private void FillDraft()
    {
        bool isFailedDraft = false;
        HashSet<CubeDraft> visited = new();

        for (int x = 0; x < _mapSize.x; x++)
        {
            for (int y = 0; y < _mapSize.y; y++)
            {
                for (int z = 0; z < _mapSize.z; z++)
                {
                    List<Directions> directions = GetAvailableDirections(new Vector3Int(x, y, z)).ToList();
                    _mapDraft[x, y, z].Coordinates = new Vector3Int(x, y, z);

                    while (isFailedDraft == false)
                    {
                        visited.Clear();
                        int randomIndex = UnityEngine.Random.Range(0, directions.Count);
                        _mapDraft[x, y, z].Direction = directions[randomIndex];

                        if (IsCycled(_mapDraft[x, y, z], visited))
                        {
                            directions.RemoveAt(randomIndex);

                            if (directions.Count == 0)
                                isFailedDraft = true;
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (isFailedDraft)
                        break;
                }

                if (isFailedDraft)
                    break;
            }

            if (isFailedDraft)
                break;
        }

        if (isFailedDraft)
        {
            ResetDraft();
            FillDraft();
        }
    }

    private void ResetDraft()
    {
        _mapDraft = new CubeDraft[_mapSize.x, _mapSize.y, _mapSize.z];
    }

    private Directions[] GetAvailableDirections(Vector3Int coordinate)
    {
        List<Directions> _directions = new();
        _directions.AddRange(GetDirectionsX(coordinate));
        _directions.AddRange(GetDirectionsY(coordinate));
        _directions.AddRange(GetDirectionsZ(coordinate));

        return _directions.ToArray();
    }

    private Directions[] GetDirectionsX(Vector3Int coordinate)
    {
        for (int x = 0; x < coordinate.x; x++)
        {
            if (_mapDraft[x, coordinate.y, coordinate.z].Direction == Directions.Right)
                return new Directions[] { Directions.Right };
        }

        return new Directions[] { Directions.Left, Directions.Right };
    }

    private Directions[] GetDirectionsY(Vector3Int coordinate)
    {
        for (int y = 0; y < coordinate.y; y++)
        {
            if (_mapDraft[coordinate.x, y, coordinate.z].Direction == Directions.Up)
                return new Directions[] { Directions.Up };
        }

        return new Directions[] { Directions.Up, Directions.Down };
    }

    private Directions[] GetDirectionsZ(Vector3Int coordinate)
    {
        for (int z = 0; z < coordinate.z; z++)
        {
            if (_mapDraft[coordinate.x, coordinate.y, z].Direction == Directions.Forward)
                return new Directions[] { Directions.Forward };
        }

        return new Directions[] { Directions.Forward, Directions.Backward };
    }

    private bool IsCycled(CubeDraft toCheck, HashSet<CubeDraft> visited)
    {
        visited.Add(toCheck);
        Vector3Int nextCubeCoordinates = GetNextCubeCoordinates(toCheck);

        if (IsOutOfBounds(nextCubeCoordinates))
            return false;

        CubeDraft next = _mapDraft[nextCubeCoordinates.x, nextCubeCoordinates.y, nextCubeCoordinates.z];

        if (next.Direction == Directions.None)
            return false;
        else if (visited.Contains(next))
            return true;
        else
            return IsCycled(next, visited);
    }

    private Vector3Int GetNextCubeCoordinates(CubeDraft cubeDraft)
    {
        Vector3Int toSumm = cubeDraft.Direction switch
        {
            Directions.Forward => Vector3Int.forward,
            Directions.Backward => Vector3Int.back,
            Directions.Up => Vector3Int.up,
            Directions.Down => Vector3Int.down,
            Directions.Left => Vector3Int.left,
            Directions.Right => Vector3Int.right,
            _ => Vector3Int.zero,
        };

        return cubeDraft.Coordinates + toSumm;
    }

    private bool IsOutOfBounds(Vector3Int coordinates)
    {
        return coordinates.x < 0 || coordinates.x >= _mapSize.x || coordinates.y < 0 || 
            coordinates.y >= _mapSize.y || coordinates.z < 0 || coordinates.z >= _mapSize.z;
    }
}

public struct CubeDraft
{
    public Directions Direction;
    public Vector3Int Coordinates;
}

public enum Directions
{
    None = 0,
    Up = 1,
    Down = -1,
    Right = 2,
    Left = -2,
    Forward = 3,
    Backward = -3
}