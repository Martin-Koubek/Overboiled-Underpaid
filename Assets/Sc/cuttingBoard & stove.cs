using UnityEngine;
using UnityEngine.UI;

public class cuttingBoard : MonoBehaviour
{
    public float cuttingTime;
    public float cookingTime;
    //reference na objeky
    public GameObject PlacedIngredienceA;
    public GameObject PlacedIngredienceB;
    private GameObject ItemToDestroy;
    public Transform PSpot;

    [SerializeField]
    private Image progress;
    [SerializeField]
    private Image Image;
    [SerializeField]
    private Image burnImage;
    [SerializeField]
    private Canvas worldCanvas;

    public bool isPlaced;
    public bool isStove;
    public bool isCuttingBoard;

    [SerializeField] private GameObject playerCamera;

    public void Start()
    {
        if (isCuttingBoard)
        {
            isPlaced = false;
            cuttingTime = 0;
            Image.fillAmount = 0;
            progress.gameObject.SetActive(false);
        }
        else if (isStove)
        {
            isPlaced = false;
            cuttingTime = 0;
            cookingTime = 0;
            Image.fillAmount = 0;
            burnImage.fillAmount = 0;
            progress.gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        worldCanvas.transform.rotation = Quaternion.LookRotation(worldCanvas.transform.position - playerCamera.transform.position);
        if (!isPlaced)
        {
            if (isCuttingBoard)
            {
                cuttingTime = 0;
                Image.fillAmount = 0;
                progress.gameObject.SetActive(false);
            }
            else if (isStove)
            {
                cookingTime = 0;
                Image.fillAmount = 0;
                burnImage.fillAmount = 0;
                progress.gameObject.SetActive(false);
            }

        }

        if (isPlaced)
        {
            if (isCuttingBoard)
            {
                progress.gameObject.SetActive(true);
                cuttingTime += Time.deltaTime;
                Image.fillAmount = cuttingTime / 5;

                if (PlacedIngredienceA.TryGetComponent<ingred>(out ingred ingred) && ingred.isCuttable)
                {
                    cutting(ingred);
                }
            }

            else if (isStove)
            {
                if (PlacedIngredienceA.TryGetComponent<ingred>(out ingred ing) && ing.isBurnable)
                {
                    cookingTime = 5f;
                    Image.fillAmount = 1;
                }
                progress.gameObject.SetActive(true);
                cookingTime += Time.deltaTime;
                if (cookingTime <= 5f)
                {
                    Image.fillAmount += Time.deltaTime / 5;
                }
                else if (cookingTime > 5f)
                {
                    burnImage.fillAmount += Time.deltaTime / 5;
                }
                if (PlacedIngredienceA.TryGetComponent<ingred>(out ingred Ingredience) && Ingredience.isCookable || Ingredience.isBurnable)
                {
                    cook(Ingredience);
                }
            }
        }

    }


    private void cutting(ingred I)
    {
        if (cuttingTime >= 5f)
        {
            if (I.isBun)
            {
                ItemToDestroy = PlacedIngredienceA;
                PlacedIngredienceA = Instantiate(I.CuttVersion, PSpot);
                PlacedIngredienceB = Instantiate(I.CookingVersion, PSpot);
                Destroy(ItemToDestroy);
                PlacedIngredienceA.transform.localPosition = Vector3.zero;
                PlacedIngredienceB.transform.localPosition = Vector3.zero;
            }
            else
            {
                ItemToDestroy = PlacedIngredienceA;
                PlacedIngredienceA = Instantiate(I.CuttVersion, PSpot);
                Destroy(ItemToDestroy);
                PlacedIngredienceA.transform.localPosition = Vector3.zero;
            }
        }
    }
    private void cook(ingred I)
    {
        if (cookingTime >= 5f && I.isCookable)
        {
            ItemToDestroy = PlacedIngredienceA;
            PlacedIngredienceA = Instantiate(I.CookingVersion, PSpot);
            Destroy(ItemToDestroy);
            PlacedIngredienceA.transform.localPosition = Vector3.zero;
        }

        else if (cookingTime >= 10f && I.isBurnable)
        {
            ItemToDestroy = PlacedIngredienceA;
            PlacedIngredienceA = Instantiate(I.BurdenVersion, PSpot);
            Destroy(ItemToDestroy);
            PlacedIngredienceA.transform.localPosition = Vector3.zero;

        }
    }
}
