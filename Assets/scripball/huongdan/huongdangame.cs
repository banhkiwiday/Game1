using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class huongdangame : MonoBehaviour
{
    public Image displayImage;
    public Sprite[] images;
    private int currentImageIndex = 0;

    public Button nextButton;
    public Button previousButton;
    public Button loadScene1Button;

    void Start()
    {

        if (displayImage == null)
        {
            Debug.LogError("Display Image is not assigned!");
            return;
        }

        if (images == null || images.Length == 0)
        {
            Debug.LogError("Images array is empty or not assigned!");
            return;
        }

        if (nextButton != null)
        {
            nextButton.onClick.AddListener(NextImage);
        }

        if (previousButton != null)
        {
            previousButton.onClick.AddListener(PreviousImage);
        }

        if (loadScene1Button != null)
        {
            loadScene1Button.onClick.AddListener(LoadScene1);
        }

        UpdateImage();
    }

    public void NextImage()
    {
        if (images.Length == 0) return;

        currentImageIndex = (currentImageIndex + 1) % images.Length;
        UpdateImage();
    }

    public void PreviousImage()
    {
        if (images.Length == 0) return;

        currentImageIndex--;
        if (currentImageIndex < 0)
        {
            currentImageIndex = images.Length - 1;
        }
        UpdateImage();
    }

    public void LoadScene1()
    {
        SceneManager.LoadScene("SceneVip");
    }

    private void UpdateImage()
    {
        displayImage.sprite = images[currentImageIndex];
    }
}