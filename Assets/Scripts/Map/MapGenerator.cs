using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;
using System;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private Transform _mapParent;
    [SerializeField] private Cube _prefab;
    [SerializeField] private Vector3Int _mapSize;

    private Direction[,,] _mapDraft;

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

        _mapDraft = new Direction[_mapSize.x, _mapSize.y, _mapSize.z];

        FillDraft();
        InstantiateMap();
    }

    private void InstantiateMap()
    {
        Vector3 center = new Vector3(_mapSize.x -1, _mapSize.y -1, _mapSize.z - 1) / 2;

        for (int x = 0; x < _mapSize.x; x++)
        {
            for (int y = 0; y < _mapSize.y; y++)
            {
                for (int z = 0; z < _mapSize.z; z++)
                {
                    Cube cube = Instantiate(_prefab, _mapParent);
                    cube.transform.localPosition = GetPosition(center, new Vector3(x, y, z));
                    cube.SetRotation(_mapDraft[x, y, z]);
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
        for (int x = 0; x < _mapSize.x; x++)
        {
            for (int y = 0; y < _mapSize.y; y++)
            {
                for (int z = 0; z < _mapSize.z; z++)
                {
                    Direction[] directions = GetAvailableDirections(new Vector3Int(x, y, z));
                    _mapDraft[x, y, z] = directions[UnityEngine.Random.Range(0, directions.Length)];
                }
            }
        }
    }

    private Direction[] GetAvailableDirections(Vector3Int coordinate)
    {
        List<Direction> _directions = new();
        _directions.AddRange(GetDirectionsX(coordinate));
        _directions.AddRange(GetDirectionsY(coordinate));
        _directions.AddRange(GetDirectionsZ(coordinate));

        return _directions.ToArray();
    }

    private Direction[] GetDirectionsX(Vector3Int coordinate)
    {
        for (int x = 0; x < coordinate.x; x++)
        {
            if (_mapDraft[x, coordinate.y, coordinate.z] == Direction.Right)
                return new Direction[] { Direction.Right };
        }

        return new Direction[] { Direction.Left, Direction.Right };
    }

    private Direction[] GetDirectionsY(Vector3Int coordinate)
    {
        for (int y = 0; y < coordinate.y; y++)
        {
            if (_mapDraft[coordinate.x, y, coordinate.z] == Direction.Up)
                return new Direction[] { Direction.Up };
        }

        return new Direction[] { Direction.Up, Direction.Down };
    }

    private Direction[] GetDirectionsZ(Vector3Int coordinate)
    {
        for (int z = 0; z < coordinate.z; z++)
        {
            if (_mapDraft[coordinate.x, coordinate.y, z] == Direction.Forward)
                return new Direction[] { Direction.Forward };
        }

        return new Direction[] { Direction.Forward, Direction.Backward };
    }
}

public enum Direction
{
    None = 0,
    Up = 1,
    Down = -1,
    Right = 2,
    Left = -2,
    Forward = 3,
    Backward = -3
}