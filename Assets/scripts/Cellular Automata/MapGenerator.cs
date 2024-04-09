using UnityEngine;
using System;
namespace CellularAutomata
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] private int _width;
        [SerializeField] private int _height;
        [Range(0f, 100f)]
        [SerializeField] private float   _fillPercent;
        
        [Range(1, 10)]
        [SerializeField] private int _smoothMinAmount;

        [Range(1, 10)]
        [SerializeField] private int _smoothMaxAmount;

        private int[,] _mapPoints;
        [SerializeField] private bool    _useSeed;
        [SerializeField] private string  _seed;



        private void Start()
        {
            GenerateMap();
        }
        private void GenerateMap()
        {
            _mapPoints = new int[_width,_height];
            FillMap();

            for (int i = 0; i < 5; i++)
                SmoothMaps();
        }
        private void FillMap()
        {
            if (_useSeed)
                _seed = Time.time.ToString();

            System.Random pseudoRandom  = new System.Random(_seed.GetHashCode());
            for (int x = 0; x < _width; x++)
                for (int y = 0; y < _height; y++)
                {
                    if (x == 0 || y == 0 || x == _width - 1 || y == _height - 1)
                        _mapPoints[x, y] = 1;
                    else
                        _mapPoints[x, y] = (pseudoRandom.Next(0,100)<_fillPercent?1:0);
                }
        }
        private void SmoothMaps()
        {
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    int neighborswalltiles = GetWallsCount(x, y);
                    if (neighborswalltiles > _smoothMaxAmount)
                        _mapPoints[x, y] = 1;
                    else if (neighborswalltiles < _smoothMinAmount)
                        _mapPoints[x, y] = 0;
                }
            }
               
                    
        }

        private int GetWallsCount (int _gridX, int _gridY)
        {
            int _wallsCount = 0;
            for(int neighbourx = _gridX -1; neighbourx <=_gridX+1; neighbourx++)
                for (int neighboury = _gridY - 1; neighboury <= _gridY + 1; neighboury++)
                    if(neighbourx >=0 && neighbourx< _width && neighboury >=0 && neighboury< _height)
                    {
                        if(neighbourx != _gridX || neighboury != _gridY)
                        {
                            _wallsCount += _mapPoints[neighbourx, neighboury];
                        }
                    }
                    else
                    {
                        _wallsCount++;
                    }

            return _wallsCount;
        }

        private void OnDrawGizmos()
        {
            if(_mapPoints != null)
            {
                for (int x = 0; x < _width; x++)
                    for (int y = 0; y < _height; y++)
                    {
                        Gizmos.color = (_mapPoints[x, y] == 1) ? Color.black : Color.white;
                        Vector3 m_pos = new Vector3(-_width / 2 + x + .5f, 0, -_height / 2 + y + .5f);
                        Gizmos.DrawCube(m_pos, Vector3.one);
                    }
            }
            
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                GenerateMap();
        }
    }

}
