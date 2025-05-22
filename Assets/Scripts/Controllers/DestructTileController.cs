using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DestructTileController : MonoBehaviour
{
    private Tilemap tilemap;

    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
        InvokeRepeating("ToggleTiles", 0f, 10f); // Gọi hàm ToggleTiles mỗi 10 giây
    }

    private void ToggleTiles()
    {
        // Kiểm tra trạng thái hiện tại của Tilemap
        bool isActive = tilemap.gameObject.activeSelf;

        // Chuyển đổi trạng thái
        tilemap.gameObject.SetActive(!isActive);
    }
}