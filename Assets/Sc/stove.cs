using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class stove : MonoBehaviour
{
    public float cookingTime;
    //reference na objeky
    public GameObject PlacedIngredienceA;
    public GameObject player;
    private GameObject ItemToDestroy;
    public Transform PSpot;
    public ParticleSystem fireEfect;

    private float firstTime;

    public AudioSource sound;
    //public AudioSource fireS;

    [SerializeField]
    private Image progress;
    [SerializeField]
    private Image Image;
    [SerializeField]
    private Image burnImage;
    [SerializeField]
    private Canvas worldCanvas;

   

    public bool isPlaced;
    public bool isOnFire;


    private void Start()
    {
        cookingTime = 0;
        Image.fillAmount = 0;
        burnImage.fillAmount = 0;
        progress.gameObject.SetActive(false);
        fireEfect.Stop();
    }

    private void Update()
    {
        worldCanvas.transform.rotation = Quaternion.LookRotation(worldCanvas.transform.position - player.transform.position);
        if (!isPlaced)
        {
            cookingTime = 0;
            Image.fillAmount = 0;
            burnImage.fillAmount = 0;
            progress.gameObject.SetActive(false);
        }

        if (isPlaced)
        {
            if (isOnFire && PlacedIngredienceA.TryGetComponent<ingred>(out ingred i))
            {
                burn(i);
                sound.Play();
                //isOnFire = true;
                //fireEfect.Play();
                //fireS.Play();
            }
            else
            {
                if (PlacedIngredienceA.TryGetComponent<ingred>(out ingred Ingredience) && Ingredience.isCookable || Ingredience.isBurnable)
                {
                    progress.gameObject.SetActive(true);
                    cookingTime += Time.deltaTime;
                    cook(Ingredience);
                    if (cookingTime <= Ingredience.prepTime && Ingredience.isCookable)
                    {
                        firstTime = Ingredience.prepTime;
                        Image.fillAmount += Time.deltaTime / Ingredience.prepTime;
                    }
                    else if (cookingTime < Ingredience.burnTime && Ingredience.isBurnable)
                    {
                        burnImage.fillAmount += Time.deltaTime / Ingredience.burnTime;
                    }
                }
                else if ( !Ingredience.isCookable || !Ingredience.isBurnable)
                {
                    progress.gameObject.SetActive(false);
                }
            }

        }
        
    }

    public void setFire()
    {
        isOnFire = true;
        fireEfect.Play();
    }
    private void cook(ingred I)
    {

        if (cookingTime >= I.prepTime && I.isCookable)
        {
            ItemToDestroy = PlacedIngredienceA;
            PlacedIngredienceA = Instantiate(I.CookingVersion, PSpot);
            Destroy(ItemToDestroy);
            PlacedIngredienceA.transform.localPosition = Vector3.zero;
            cookingTime = 0;
        }

        else if (cookingTime >= I.prepTime && I.isBurnable)
        {
            ItemToDestroy = PlacedIngredienceA;
            PlacedIngredienceA = Instantiate(I.BurdenVersion, PSpot);
            Destroy(ItemToDestroy);
            PlacedIngredienceA.transform.localPosition = Vector3.zero;
            sound.Stop();

        }
    }
    private void burn(ingred i)
    {
        ItemToDestroy = PlacedIngredienceA;
        PlacedIngredienceA = Instantiate(i.BurdenVersion, PSpot);
        Destroy(ItemToDestroy);
        PlacedIngredienceA.transform.position = PSpot.transform.position;
    }
}
