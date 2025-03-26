using TMPro;
using UnityEngine;

public class interact : MonoBehaviour
{
    //reference na držený pøedmìt, mýsto držení, bod poèátku raycastu 
    public GameObject heldItem;
    public Transform RayCastPoint;
    public GameObject holdSpot;
    public TextMeshProUGUI txt;
    [SerializeField]
    private GameObject OManager;

    public LayerMask PickUpMask;
    public LayerMask InteractMask;

    //dosah hráèe
    public float HitRange = 5f;
    //drží hráè nìco ?
    public bool holding;
    public bool maHasicak;
    //raycast info
    public RaycastHit hit;

    public float tillFreez = 2f;

    void Update()
    {
        tillFreez -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(RayCastPoint.position, RayCastPoint.forward);
            if (!holding && Physics.Raycast(ray, out hit, HitRange))
            {
                Debug.DrawRay(RayCastPoint.position, RayCastPoint.forward * HitRange, Color.red);
                if (hit.collider.gameObject.TryGetComponent<storage>(out storage s))
                {
                    heldItem = Instantiate(s.FoodRef, holdSpot.transform.position, Quaternion.identity);
                    take();
                }

                else if (hit.collider.gameObject.TryGetComponent<dishes>(out dishes d))
                {
                    heldItem = Instantiate(d.dish, holdSpot.transform.position, Quaternion.identity);
                    take();
                }

                else if (hit.collider.gameObject.TryGetComponent<Table>(out Table table) && table.isPlaced)
                {
                    heldItem = table.placedItem;
                    table.placedItem.transform.SetParent(null);
                    take();
                    table.isPlaced = false;
                }

                else if (hit.collider.gameObject.TryGetComponent<cuttingBoard>(out cuttingBoard c))
                {
                    if (c.PlacedIngredienceB == null && c.PlacedIngredienceA != null)
                    {
                        heldItem = c.PlacedIngredienceA;
                        c.PlacedIngredienceA = null;
                        c.isPlaced = false;
                        take();
                    }

                    else if (c.PlacedIngredienceB != null && c.PlacedIngredienceA != null)
                    {
                        heldItem = c.PlacedIngredienceA;
                        take();
                        c.PlacedIngredienceA = c.PlacedIngredienceB;
                        c.PlacedIngredienceB = null;

                    }
                    else return;
                }
                else if(hit.collider.gameObject.TryGetComponent<fire>(out fire f))
                {
                    if (!maHasicak)
                    {
                        heldItem = Instantiate(f.hasicak); ;
                        take();
                        maHasicak = true;
                    }
                }
            }
            else if (holding && Physics.Raycast(ray, out hit, HitRange, InteractMask))
            {
                if (maHasicak && hit.collider.gameObject.GetComponent<fire>())
                {
                    Destroy(heldItem);
                    holding = false;
                    maHasicak = false;
                }
                else if (maHasicak && hit.collider.gameObject.TryGetComponent<cuttingBoard>(out cuttingBoard c))
                {
                    if (c.isOnFire)
                    {
                        c.isOnFire = false;
                    }
                }

                if (hit.collider.gameObject.TryGetComponent<Table>(out Table table))
                {
                    if (!table.isPlaced)
                    {
                        table.placedItem = heldItem;
                        table.isPlaced = true;
                        Place(table.placeSpot);
                    }

                    else if (table.placedItem.TryGetComponent<dish>(out dish Dish))
                    {
                        Dish.PlacedIngredience.Add(heldItem);
                        Place(Dish.dropSpot);
                        heldItem.transform.localPosition = Vector3.zero;
                        heldItem.TryGetComponent<Rigidbody>(out Rigidbody R);
                        R.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                    }

                }

                if (heldItem.TryGetComponent<ingred>(out ingred ing))
                {
                    if (hit.collider.TryGetComponent<cuttingBoard>(out cuttingBoard c) && !c.isPlaced && (c.isStove && ing.isCookable || ing.isBurnable || (c.isCuttingBoard && ing.isCuttable)))
                    {
                        int s = Random.Range(0, 10);
                        if (s <= 5)
                        {
                        c.PlacedIngredienceA = heldItem;
                        c.isPlaced = true;
                        Place(c.PSpot);
                        }
                        else
                        {
                            c.isOnFire = true;
                        }
                    }

                    else if (hit.collider.TryGetComponent<trash>(out trash Trash))
                    {
                        Destroy(heldItem);
                        holding = false;
                    }
                }

                else if (heldItem.TryGetComponent<dish>(out dish dish))
                {
                    if (hit.collider.TryGetComponent<customer>(out customer cust))
                    {
                        //OManager.TryGetComponent<recepty>(out recepty r);
                        if (cust.Order.Count == dish.PlacedIngredience.Count)
                        {
                            for (int i = 0; i < cust.Order.Count - 1; i++)
                            {
                                if (cust.Order[i].GetComponent<ingred>().Name != dish.PlacedIngredience[i].GetComponent<ingred>().Name)
                                {
                                    Debug.Log("Wrong Order");
                                    cust.orderBg.color = Color.red;
                                    return;
                                }
                            }
                            int score = 0;
                            Destroy(heldItem);
                            Destroy(cust);
                            holding=false;
                            Debug.Log("Good");
                            score++;
                            txt.text = score.ToString();
                            cust.orderBg.color = Color.green;

                        }
                        else
                        {
                            Debug.Log("Wrong length");
                            cust.orderBg.color = Color.red;
                        }
                    }
                    else if (hit.collider.TryGetComponent<trash>(out trash Trash))
                    {
                        foreach(GameObject placed in dish.PlacedIngredience)
                        {
                            Destroy(placed);
                        }   
                        dish.PlacedIngredience.Clear();
                    }
                }

            }
        }
        //else if (holding && Input.GetKeyDown(KeyCode.Q))
        //{
        //    drop();
        //}
    }

    //public void drop()
    //{
    //    heldItem.transform.SetParent(null);
    //    holding = false;
    //    heldItem.GetComponent<Rigidbody>().isKinematic = false;
    //}
    public void take()
    {
        holding = true;
        heldItem.transform.SetParent(holdSpot.transform, false);
        heldItem.transform.localPosition = Vector3.zero;
        heldItem.transform.rotation = Quaternion.identity;
        heldItem.GetComponent<Rigidbody>().isKinematic = true;
    }
    public void Place(Transform transform)
    {
        heldItem.transform.SetParent(transform, true);
        heldItem.transform.localPosition = Vector3.zero;
        heldItem.GetComponent<Rigidbody>().isKinematic = false;
        holding = false;
    }
    //public void placeOnPlate()
    //{

    //}
}


