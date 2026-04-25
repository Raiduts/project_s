using UnityEngine;

public class FitSpriteToScreen : MonoBehaviour
{
    public bool doLeft = true;

    void Start()
    {
        FitToScreen();
    }

    void FitToScreen()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        float worldHeight = Camera.main.orthographicSize * 2f;
        float worldWidth = worldHeight * Screen.width / Screen.height;

        Vector2 spriteSize = sr.sprite.bounds.size;

        float scaleX = worldWidth / spriteSize.x;
        float scaleY = worldHeight / spriteSize.y;

        float scale = Mathf.Max(scaleX, scaleY);

        transform.localScale = new Vector3(scale, scale, 1f);

        if (!doLeft)
        {
            return;
        }

        // 🔥 Anchor ke kiri
        float spriteWorldWidth = spriteSize.x * scale;

        float leftEdgeScreen = -worldWidth / 2f;
        float spriteLeftOffset = spriteWorldWidth / 2f;

        transform.position = new Vector3(
            leftEdgeScreen + spriteLeftOffset,
            transform.position.y,
            transform.position.z
        );
    }
}