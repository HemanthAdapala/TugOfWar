using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private Vector2 originalPosition;
    [SerializeField] private LayerMask cubeLayer; // Assign the cube's layer in the Inspector

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = rectTransform.anchoredPosition;
        // Disable raycast blocking to detect 3D objects underneath
        canvasGroup.blocksRaycasts = false;
    }
    

    public void OnDrag(PointerEventData eventData)
    {
        // Update position to follow the pointer
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        // Check if the card is dropped over the 3D cube
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, cubeLayer))
        {
            if (hit.collider.CompareTag("Cube")) // Ensure the cube has the "Cube" tag
            {
                Debug.Log("Card dropped on cube!");
                // Trigger cube logic here (e.g., hit.collider.GetComponent<Cube>().OnCardDropped());
            }
        }
        else
        {
            // Reset position if not dropped on the cube
            rectTransform.anchoredPosition = originalPosition;
        }
    }
}