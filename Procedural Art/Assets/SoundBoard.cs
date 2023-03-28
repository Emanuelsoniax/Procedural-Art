using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundBoard : MonoBehaviour
{
    [SerializeField]
    private Transform gridContainer;
    private float width;
    private float height;
    [SerializeField]
    private int columns;
    [SerializeField]
    private int rows;

    private List<GameObject> tiles = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        width = gridContainer.GetComponent<RectTransform>().sizeDelta.x;
        height = gridContainer.GetComponent<RectTransform>().sizeDelta.y;
        SpawnGrid();
    }

    void SpawnGrid()
    {
       // grid = new int[columns, rows];

        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                float xPos = (i * (width / columns)) + (width / columns) *0.5f ;
                float yPos = (j * (height / rows)) + + (height/ rows) * 0.5f;
                SpawnTiles(xPos, yPos);
            }
        }
        ConfigureTiles();
    }

    private void ConfigureTiles()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            tiles[i].AddComponent<AudioGenerator>();
            AudioGenerator audioGen = tiles[i].GetComponent<AudioGenerator>();
            audioGen.index = i;
            audioGen.SetFrequency();
            tiles[i].AddComponent<Button>();
            tiles[i].GetComponent<Button>().onClick.AddListener(audioGen.PlayAudio);
            //tiles[i].GetComponent<Image>().color = Color.clear;
        }
    }

    private void SpawnTiles(float _x, float _y)
    {
        GameObject tile = new GameObject(_x + "," + _y, typeof(Image));
        tile.transform.SetParent(gridContainer, false);

        RectTransform rectTransform = tile.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(_x, _y);
        rectTransform.sizeDelta = new Vector2(width/columns, height/rows);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);

        tiles.Add(tile);
    }
}
