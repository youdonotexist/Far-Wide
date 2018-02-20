//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using UnityEngine;
using Gamelogic;
using Gamelogic.Grids;
using Gamelogic.Grids.Examples;
using UnityEngine.Networking;

public class ServerGridBuilder : NetworkBehaviour
{
	// This is the prefab that will be used for each cell in the grid. 
	// Normally, you would use your own version that has information
	// related to your game.
	public Cell cellPrefab;

	// All cells will be parented to this object.
	public GameObject root;

	// The grid data structure that contains all cell.
    private RectGrid<Cell> grid;

	// The map (that converts between world and grid coordinates).
	private IMap3D<RectPoint> map;

	public void BuildGrid()
	{
		// Creates a grid in a rectangular shape.
        grid = RectGrid<Cell>.Rectangle(7, 7);

		// Creates a map...
        map = new RectMap(cellPrefab.Dimensions) // The cell dimensions usually correspond to the visual 
			// part of the sprite in pixels. Here we use the actual 
			// sprite size, which causes a border between cells.
			.WithWindow(ExampleUtils.ScreenRect) // ...that is centered in the rectangle provided
			.AlignMiddleCenter(grid) // by this and the previous line.
			.To3DXY(); // This makes the 2D map returned by the last function into a 3D map
		// This map assumes the grid is in the XY plane.
		// To3DXZ assumes the grid is in the XZ plane (you have to make sure 
		//your tiles are similarly aligned / rotated).

        //Iterates over all points (coordinates) contained in the
		foreach (RectPoint point in grid)  {
            Cell cell = (Cell) GameObject.Instantiate(cellPrefab, Vector3.zero, Quaternion.identity); // Instantiate a cell from the given prefab.
			Vector3 worldPoint = map[point]; //Calculate the world point of the current grid point

			cell.transform.parent = root.transform; //Parent the cell to the root
			cell.transform.localScale = Vector3.one; //Readjust the scale - the re-parenting above may have changed it.
			cell.transform.localPosition = worldPoint; //Set the localPosition of the cell.

			cell.name = point.ToString(); // Makes it easier to identify cells in the editor.
			grid[point] = cell; // Finally, put the cell in the grid.

            NetworkServer.Spawn(cell.gameObject);
		}
	}

    public Cell[] getCorners() {
        Cell[] cells = new Cell[4];
        cells[0] = grid.GetCell(new RectPoint(0, 0)); //P1 start
        cells[1] = grid.GetCell(new RectPoint(grid.Width - 1, grid.Height - 1)); //P2 start
        cells[2] = grid.GetCell(new RectPoint(0, grid.Height - 1));
        cells[3] = grid.GetCell(new RectPoint(grid.Width - 1, 0));

        return cells;
    }

    public Cell[] GetMagicTiles() {
        Cell[] cells = new Cell[4];

        cells[0] = grid.GetCell(new RectPoint(1, 1));
        cells[1] = grid.GetCell(new RectPoint(grid.Width - 2, grid.Height - 2));
        cells[2] = grid.GetCell(new RectPoint(1, grid.Height - 2));
        cells[3] = grid.GetCell(new RectPoint(grid.Width - 2, 1));

        return cells;
    }

    public Cell GetCenterCell() {
        int centerX = Mathf.FloorToInt((float)grid.Width * 0.5f);
        int centerY = Mathf.FloorToInt((float)grid.Height * 0.5f);

        return grid.GetCell(new RectPoint(centerX, centerY));
    }

    public Cell GetCell(Vector3 current, Vector2 direction) {
        // Calculates the grid point that corresponds to the given world coordinate.
        RectPoint point = map[current];

        // The point may in fact lie outside the grid as we defined it when we built it.
        // So we first check whether the grid contains the point...
        if (grid.Contains(point))
        {
            return grid.GetCell(point.MoveBy(new RectPoint(Mathf.FloorToInt(direction.x), Mathf.FloorToInt(direction.y))));
        }

        return null;
    }

    public Cell GetCell(Vector3 current) {
        // Calculates the grid point that corresponds to the given world coordinate.
        RectPoint point = map[current];
        return grid.GetCell(point);
    }
}