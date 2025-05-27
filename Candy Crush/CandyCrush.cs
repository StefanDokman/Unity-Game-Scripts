

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UIElements;


public class CandyCrush : MonoBehaviour
{
    public int gridX;
    public int gridY;
    public float scale = 1;
    public float spacingX = 30;
    public float spacingY = 30;
    public GameObject[,] grid;
    public GameObject[] blocks;
    public GameObject blockForChange1 = null;
    public GameObject blockForChange2 = null;
    public bool isGrid1 = true;
    public int[,] matrix;





    void Start()
    {
        System.String s = "";
        grid = new GameObject[gridX, gridY];
        matrix = GenerateMatrix(gridX, gridY, new int[] { 1, 2, 3 });

        for (int i = 0; i < matrix.GetLength(0); i++) { 
            for (int j = 0; j < matrix.GetLength(1); j++)
            { s += matrix[i, j].ToString() + ","; }
        s += "\n";}
        Debug.Log(s);
        fill();

    }

    private void Update()
    {
        if (blockForChange1 != null && blockForChange2 != null)
        {

            int index1x = 0, index1y = 0, index2x = 0, index2y = 0;
            GameObject tmp;

            for (int i = 0; i < gridX; i++)
            {
                for (int j = 0; j < gridY; j++)
                {
                    if (grid[i, j] != null)
                    {
                        if (grid[i, j] == blockForChange1)
                        {
                            index1x = i;
                            index1y = j;
                        }

                        if (grid[i, j] == blockForChange2)
                        {
                            index2x = i;
                            index2y = j;
                        }
                    }
                }
            }



            if ((index1x == index2x && index1y == index2y + 1) || (index1y == index2y && index1x == index2x + 1) || (index1x == index2x && index1y == index2y - 1) || (index1y == index2y && index1x == index2x - 1))
            {
                tmp = grid[index1x, index1y];
                grid[index1x, index1y] = grid[index2x, index2y];
                grid[index2x, index2y] = tmp;
                StartCoroutine(Swaper(grid[index1x, index1y], grid[index2x, index2y], 0.5f, true));



            }
            blockForChange1.GetComponent<Candy>().StartReturn();
                blockForChange2.GetComponent<Candy>().StartReturn();
                blockForChange1 = null;
                blockForChange2 = null;

        }
    }


    public IEnumerator Swaper(GameObject block1, GameObject block2, float duration, bool wasItSwap)
    {
        Debug.Log("Swaper Start");
        Vector3 b1Position = block1.transform.position;
        Vector3 b2Position = block2.transform.position;

        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            block1.transform.position = Vector3.Lerp(b1Position, b2Position, t / duration);
            block2.transform.position = Vector3.Lerp(b2Position, b1Position, t / duration);
            yield return null;
        }

        block1.transform.position = b2Position;
        block2.transform.position = b1Position;

        if (wasItSwap)
        {
            yield return StartCoroutine(ProcessMatchesAndPushDown());
        }
    }


    public void fill()
    {
        GameObject spawnObject = new GameObject();
        spawnObject.transform.parent = this.transform;
        spawnObject.transform.localPosition = new Vector3(0, 0, 0);
        spawnObject.transform.localRotation = Quaternion.identity;
        for (int i = 0; i < gridX; i++)
        {
            for (int j = 0; j < gridY; j++)
            { 
                GameObject block = blocks[matrix[i,j]];
                grid[i,j]= Instantiate(block, spawnObject.transform.position, spawnObject.transform.rotation, transform);
                

                spawnObject.transform.localPosition += new Vector3(spacingX, 0, 0);
            }
            spawnObject.transform.localPosition += new Vector3(0, spacingY, 0);
            spawnObject.transform.localPosition = new Vector3(0, spawnObject.transform.localPosition.y, spawnObject.transform.localPosition.z);

            

        }
        Destroy(spawnObject);
    }



    int[,] GenerateMatrix(int rows, int cols, int[] values)
    {
        int[,] matrix = new int[rows, cols];
        

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                int value;
                do
                {
                    value = UnityEngine.Random.Range(0, blocks.Length);
                } while (IsInvalid(matrix, i, j, value));

                matrix[i, j] = value;
            }
        }

        return matrix;
    }

    static bool IsInvalid(int[,] matrix, int row, int col, int value)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);

        if (row > 1 && matrix[row - 1, col] == value && matrix[row - 2, col] == value)
            return true;

        if (col > 1 && matrix[row, col - 1] == value && matrix[row, col - 2] == value)
            return true;

        return false;
    }
    private IEnumerator ProcessMatchesAndPushDown()
    {
        bool hasChanges = true;

        while (hasChanges)
        {
            bool replaced = false;
            yield return StartCoroutine(ReplaceConsecutive(result => replaced = result));
            if (replaced)
            {
                bool pushedDown = false;
                yield return StartCoroutine(PushDown(result => pushedDown = result));

                hasChanges = replaced || pushedDown;
            }
            else
            {
                hasChanges = false;
            }
        }
    }
    IEnumerator ReplaceConsecutive(System.Action<bool> callback)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        int minMatch = 3;
        bool hasReplaced = false;
        bool emptyBlocksExist = false;
        HashSet<(int, int)> toDestroy = new HashSet<(int, int)>();

        for (int i = 0; i < rows; i++)
        {
            
            int count = 1;
            int matchValue = grid[i, 0].GetComponent<Candy>().value;
            int startCol = 0;

            for (int j = 1; j <= cols; j++)
            {
                int currentValue = j < cols ? grid[i, j].GetComponent<Candy>().value : -999;

                if (currentValue == matchValue && currentValue!=-1)
                {
                    count++;
                }
                else
                {
                    if (count >= minMatch)
                    {
                        for (int k = startCol; k < startCol + count; k++)
                            toDestroy.Add((i, k));

                        TriggerMatchEffect(i, startCol, count, horizontal: true);
                        hasReplaced = true;
                    }

                    count = 1;
                    startCol = j;
                    matchValue = currentValue;
                }
                if (j < cols && grid[i, j].GetComponent<Candy>().value == -1)
                {
                    emptyBlocksExist = true;
                }
            }
        }

        for (int j = 0; j < cols; j++)
        {
            int count = 1;
            int matchValue = grid[0, j].GetComponent<Candy>().value;
            int startRow = 0;

            for (int i = 1; i <= rows; i++)
            {
                int currentValue = i < rows ? grid[i, j].GetComponent<Candy>().value : -999;

                if (currentValue == matchValue && currentValue != -1)
                {
                    count++;
                }
                else
                {
                    if (count >= minMatch)
                    {
                        for (int k = startRow; k < startRow + count; k++)
                            toDestroy.Add((k, j));

                        TriggerMatchEffect(startRow, j, count, horizontal: false);
                        hasReplaced = true;
                    }

                    count = 1;
                    startRow = i;
                    matchValue = currentValue;
                }
                if (i < rows && grid[i, j].GetComponent<Candy>().value == -1)
                {
                    emptyBlocksExist = true;
                }
            }
        }

        foreach (var (x, y) in toDestroy)
        {
            StartCoroutine(DestroyBlock(x, y,0.3f));
        }

        callback(hasReplaced || emptyBlocksExist);
        yield break;
    }

    IEnumerator DestroyBlock(int x,int y, float duration)
    {
        Vector3 normalScale = grid[x, y].transform.localScale;
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            grid[x,y].transform.localScale = Vector3.Lerp(normalScale, Vector3.zero, t / duration);
            yield return null;
        }
        grid[x, y].GetComponent<Candy>().value = -1;
        grid[x, y].transform.localScale = normalScale;
    }

    void TriggerMatchEffect(int x, int y, int count, bool horizontal)
    {
        if (count == 4)
        {
            Debug.Log($"Spawn rocket at {(horizontal ? $"({x},{y + 1})" : $"({x + 1},{y})")}");
        }
        else if (count >= 5)
        {
            Debug.Log($"Spawn bomb at {(horizontal ? $"({x},{y + count / 2})" : $"({x + count / 2},{y})")}");
        }
    }

    public IEnumerator PushDown(System.Action<bool> callback)
    {
        
        bool movedBlocks = false;
        bool movedBlocksThisRound = true;
        while (movedBlocksThisRound)
        {
            movedBlocksThisRound = false;
            Dictionary<GameObject, Vector2Int> blockPositions = new Dictionary<GameObject, Vector2Int>();
            for (int row = 1; row < gridX; row++)
            {
                for (int col = 0; col < gridY; col++)
                {
                    GameObject current = grid[row, col];
                    GameObject above = grid[row - 1, col];

                    if (current != null && current.GetComponent<Candy>().value > -1 &&
                        (above == null || above.GetComponent<Candy>().value == -1))
                    {
                        blockPositions[current] = new Vector2Int(row, col);
                        movedBlocks = true;
                        movedBlocksThisRound = true;
                    }
                }
            }

            List<IEnumerator> swapEnumerators = new List<IEnumerator>();

            foreach (var kvp in blockPositions)
            {
                Vector2Int pos = kvp.Value;
                int index1x = pos.x;
                int index1y = pos.y;
                int index2x = index1x - 1;
                int index2y = index1y;

                GameObject tmp = grid[index1x, index1y];
                grid[index1x, index1y] = grid[index2x, index2y];
                grid[index2x, index2y] = tmp;

                swapEnumerators.Add(Swaper(grid[index1x, index1y], grid[index2x, index2y], 0.2f, false));
            }

            foreach (var swapCoroutine in swapEnumerators)
            {
                StartCoroutine(swapCoroutine);

            }

            yield return new WaitForSeconds(0.3f);

            for (int col = 0; col < gridY; col++)
            {
                int topRow = gridX - 1;
                GameObject top = grid[topRow, col];

                if (top == null || top.GetComponent<Candy>().value == -1)
                {
                    float cellSize = 1f;
                    Vector3 spawnPos = top != null ? top.transform.position : new Vector3(col * cellSize, topRow * cellSize, 0);

                    if (top != null) Destroy(top);

                    int randIndex = UnityEngine.Random.Range(0, blocks.Length);
                    GameObject newBlock = Instantiate(blocks[randIndex], spawnPos, Quaternion.identity, transform);

                    grid[topRow, col] = newBlock;
                    matrix[topRow, col] = newBlock.GetComponent<Candy>().value;
                }
            }
        }
        callback(movedBlocks);
    }
}














