using UnityEngine;
using UnityEngine.UI;
public class cuttingboard : MonoBehaviour
{

    public float cuttingTime;
    public float cookingTime;
    //reference na objeky
    public GameObject PlacedIngredienceA;
    public GameObject PlacedIngredienceB;
    private GameObject ItemToDestroy;
    public Transform PSpot;

    private float firstTime;

    public AudioSource sound;

    [SerializeField]
    private Image progress;
    [SerializeField]
    private Image Image;
    [SerializeField]
    private Canvas worldCanvas;

    [SerializeField] private GameObject playerCamera;

    public bool isPlaced;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cuttingTime = 0;
        Image.fillAmount = 0;
        progress.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        worldCanvas.transform.rotation = Quaternion.LookRotation(worldCanvas.transform.position - playerCamera.transform.position);
        if (!isPlaced)
        {
            cuttingTime = 0;
            Image.fillAmount = 0;
            progress.gameObject.SetActive(false);
        }

        if (isPlaced)
        {
            if (PlacedIngredienceA.TryGetComponent<ingred>(out ingred Ingred) && !Ingred.isCuttable)
            {
                progress.gameObject.SetActive(false);
            }

            else if (PlacedIngredienceA.TryGetComponent<ingred>(out ingred ingred) && ingred.isCuttable)
            {
                progress.gameObject.SetActive(true);
                cuttingTime += Time.deltaTime;
                Image.fillAmount = cuttingTime / ingred.prepTime;
                cutting(ingred);

            }

        }
    }

    private void cutting(ingred I)
    {
        if (cuttingTime >= I.prepTime)
        {
            if (I.isBun)
            {
                ItemToDestroy = PlacedIngredienceA;
                PlacedIngredienceA = Instantiate(I.CuttVersion, PSpot);
                PlacedIngredienceB = Instantiate(I.CookingVersion, PSpot);
                Destroy(ItemToDestroy);
                PlacedIngredienceA.transform.localPosition = Vector3.zero;
                PlacedIngredienceB.transform.localPosition = Vector3.zero;
                sound.Stop();
            }
            else
            {
                ItemToDestroy = PlacedIngredienceA;
                PlacedIngredienceA = Instantiate(I.CuttVersion, PSpot);
                Destroy(ItemToDestroy);
                PlacedIngredienceA.transform.localPosition = Vector3.zero;
                sound.Stop();
            }
        }
    }
}