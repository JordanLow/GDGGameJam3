using UnityEngine;

public abstract class Farm
{
    private int xIndexPos;
    private int yIndexPos;
    private int sizeX;
    private int sizeY;
    
    private IslandManager islandManager;

    public void setUsedTiles(int x, int y, int dx, int dy) {
        this.xIndexPos = x;
        this.yIndexPos = y;
        this.sizeX = dx;
        this.sizeY = dy;
        islandManager.setTilesUsed(this, xIndexPos, yIndexPos, sizeX, sizeY);
    }

    public void setIslandManager(IslandManager islandManager) {
        this.islandManager = islandManager;
    }


    public void demolish() {
        islandManager.setTilesFree(xIndexPos, yIndexPos, sizeX, sizeY);
    }

    public abstract void harvest(int[] fruitCount);

}