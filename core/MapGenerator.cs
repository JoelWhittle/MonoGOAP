using Microsoft.Xna.Framework;
using MonoEngine.core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MonoEngine
{
    public static class MapGenerator
    {
        public static Tile[,] Map = new Tile[40, 40];
        public static void GenerateMap(int x, int y)
        {
            using (var reader = new StreamReader("../Debug/Content/map.csv"))
            {

                int curX = 1;
                int curY = 1;
                int width = 50;
                int height = 50;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    foreach (var t in values)
                    {
                        int n = 0;
                        int.TryParse(t, out n);
                        Tile tile = new Tile(new Vector2(curX * width, curY * height), (Tile.TileType)n);
                        tile.GridX = curX - 1;
                        tile.GridY = curY - 1;
                        Map[curX - 1, curY - 1] = tile;
                        
                        Game1.AddGameObject(tile);

                        curX++;
                    }
                    curX = 1;
                    curY++;

                }
            }

            foreach (var tile in Map)
            {
                tile.Neighbours = GetTileNeighbours(tile);
            }
        }

        public static Tile GetTileAtPosition(Vector2 pos)
        {
            Tile returnTile = Map[0, 0];
            pos.X -= 25;
            pos.Y -= 25;
            var x = (int)pos.X / 50;
            var y = (int)pos.Y / 50;
            if (x >= 0 && x < 40 && y >= 0 && y < 40 )
            {
                returnTile = Map[x, y];
            }
            return returnTile;

        }

        public static List<Tile> GetTileNeighbours(Tile tile)
        {
            List<Tile> neighbours = new List<Tile>();
            if (tile.GridX > 0)
            {
                neighbours.Add(Map[tile.GridX - 1, tile.GridY]);
            }
            if (tile.GridX < 39)
            {
                neighbours.Add(Map[tile.GridX + 1, tile.GridY]);
            }

            if (tile.GridY > 0)
            {
                neighbours.Add(Map[tile.GridX, tile.GridY - 1]);
            }
            if (tile.GridY < 39)
            {
                neighbours.Add(Map[tile.GridX, tile.GridY + 1]);
            }

            return neighbours;
        }

       

     
        



        public static List<Tile> AStarPathFinder(Tile start,Tile  end,bool  allowDiagonals)
        {
            List<Tile> path = new List<Tile>();

         

            //clear tiles prev stuff
            foreach (var t in Map)
            {
                t.g = 0;
            }

            var map = Map;
            var lastCheckedNode = start;
            List<Tile> openSet = new List<Tile>();
            // openSet starts with beginning node only
            openSet.Add(start);
            List<Tile> closedSet = new List<Tile>();

            //This function returns a measure of aesthetic preference for
            //use when ordering the openSet. It is used to prioritise
            //between equal standard heuristic scores. It can therefore
            //be anything you like without affecting the ability to find
            //a minimum cost path.



            //Run one finding step.
            //returns 0 if search ongoing
            //returns 1 if goal reached
            //returns -1 if no solution

            bool searching = true;
                while (searching == true)
                {
                if (openSet.Count() > 0)
                {
                    // Best next option
                    var winner = 0;
                    for (var i = 1; i < openSet.Count(); i++)
                    {
                        if (openSet[i].GetF(end) < openSet[winner].GetF(end))
                        {
                            winner = i;
                        }
                        //if we have a tie according to the standard heuristic
                        if (openSet[i].GetF(end) == openSet[winner].GetF(end))
                        {
                            //Prefer to explore options with longer known paths (closer to goal)
                            if (openSet[i].g > openSet[winner].g)
                            {
                                winner = i;
                            }

                            if (!allowDiagonals)
                            {
                                if (openSet[i].g == openSet[winner].g)
                                {
                                    winner = i;
                                }
                            }
                        }
                    }
                    var current = openSet[winner];
                    lastCheckedNode = current;
                    // Did I finish?
                    if (current == end)
                    {

                        path.Add(end);

                        Tile cur = end;
                        while(cur != start)
                        {
                            path.Add(cur.parent);
                            cur = cur.parent;
                        }
                        path.Reverse();
                        return path;
                    }

                    // Best option moves from openSet to closedSet
                    openSet.Remove(current);
                    closedSet.Add(current);

                    // Check all the neighbors
                    var neighbors = GetTileNeighbours(current);

                    neighbors = neighbors.Where(o => o.Walkable == true).ToList();

                    for (var i = 0; i < neighbors.Count(); i++)
                    {
                        var neighbor = neighbors[i];

                        // Valid next spot?
                        if (!closedSet.Contains(neighbor))
                        {
                            // Is this a better path than before?
                            var tempG = current.g + GetManhattanDistance(neighbor, current) + current.MovementCost;

                            // Is this a better path than before?
                            if (!openSet.Contains(neighbor))
                            {

                                openSet.Add(neighbor);
                            }
                            else if (tempG >= neighbor.g)
                            {
                                // No, it's not a better path
                                continue;
                            }

                            neighbor.g = tempG;
                            if (!allowDiagonals)
                            {
                            }
                            neighbor.parent = current;
                        }

                    }
                  
                }
                else
                {
                    searching = false;

                    return path;
                }
                    // Uh oh, no solution
                }
            return path;
            }
        
        public static float GetManhattanDistance(Tile a, Tile b)
        {
            return Math.Abs(a.Position.X - b.Position.X) + Math.Abs(a.Position.Y - b.Position.Y);
        }

    }
}
