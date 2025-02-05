using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CardFlip : MonoBehaviour
{
    public GameObject frontSide;
    public GameObject backSide;

    public Button button;

    private bool isFlipped = false;
    private bool isAnimating = false; // To prevent multiple flips at once
    private float flipDuration = 0.5f; // Duration of the flip animation

    private void Start(){
        backSide.transform.Rotate(new Vector3(0,180,0));
        if(!frontSide.activeSelf){
        frontSide.SetActive(true);
        }
        if(backSide.activeSelf){
        backSide.SetActive(false);    
        }
        
        button = GetComponent<Button>();
        button.onClick.AddListener(OnCardClicked);
    }

    public void OnCardClicked()
    {
        if (!isAnimating)
        {
            isFlipped = !isFlipped;
            AnimateFlip();
        }
    }

    private void AnimateFlip()
    {
        isAnimating = true;

        // Rotate the card
        transform.DORotate(new Vector3(0, isFlipped ? 180f : 0f, 0), flipDuration)
            .SetEase(Ease.InOutQuad) // Smooth easing
            .OnUpdate(() =>
            {
                // Toggle visibility at the halfway point (90 degrees)
                if (transform.eulerAngles.y >= 90f && transform.eulerAngles.y <= 270f)
                {
                    frontSide.SetActive(false);
                    backSide.SetActive(true);
                }
                else
                {
                    frontSide.SetActive(true);
                    backSide.SetActive(false);


                }
            })
            .OnComplete(() =>
            {
                isAnimating = false;
            });
    }
}