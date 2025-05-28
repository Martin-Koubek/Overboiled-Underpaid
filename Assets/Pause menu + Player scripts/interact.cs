using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class interact : MonoBehaviour
{
    //reference na držený pøedmìt, mýsto držení, bod poèátku raycastu 
    public GameObject heldItem;
    public Transform RayCastPoint;
    public GameObject holdSpot;
    public TextMeshProUGUI txt;

    public LayerMask PickUpMask;
    public LayerMask InteractMask;

    //dosah hráèe
    public float HitRange = 5f;
    //drží hráè nìco ?
    public bool holding;
    public bool maHasicak;
    //raycast info
    public RaycastHit hit;

    public GameObject healtM;

    int score = 0;

    public void Start()
    {
        
    }

    void Update()
    {
        healtM.TryGetComponent<health>(out health h);
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

                else if (hit.collider.gameObject.TryGetComponent<cuttingboard>(out cuttingboard c))
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
                else if (hit.collider.gameObject.TryGetComponent<stove>(out stove st))
                {
                    heldItem = st.PlacedIngredienceA;
                    st.PlacedIngredienceA = null;
                    st.isPlaced = false;
                    take();
                    st.sound.Stop();
                }
                else if (hit.collider.gameObject.TryGetComponent<fire>(out fire f))
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
                if (heldItem.TryGetComponent<ingred>(out ingred ing))
                {
                    if (ing.isCookable)
                    {
                        if (hit.collider.TryGetComponent<stove>(out stove s))
                        {
                            int a = Random.Range(0, 10);
                            if (a <= 6)
                            {
                                s.PlacedIngredienceA = heldItem;
                                s.isPlaced = true;
                                Place(s.PSpot);
                                s.sound.Play();
                            }
                            else
                            {
                                s.PlacedIngredienceA = heldItem;
                                s.isPlaced = true;
                                s.setFire();
                                holding = false;
                            }
                        }
                    }
                    else if (ing.isCuttable)
                    {
                        if (hit.collider.TryGetComponent<cuttingboard>(out cuttingboard c))
                        {
                            c.PlacedIngredienceA = heldItem;
                            c.isPlaced = true;
                            Place(c.PSpot);
                            c.sound.Play();
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


                    else if (hit.collider.TryGetComponent<trash>(out trash Trash))
                    {
                        Destroy(heldItem);
                        holding = false;
                    }
                }
               

                else if (heldItem.TryGetComponent<dish>(out dish dish))
                {
                    if (hit.collider.TryGetComponent<Table>(out Table table))
                    {
                        if (!table.isPlaced)
                        {
                            table.placedItem = heldItem;
                            table.isPlaced = true;
                            Place(table.placeSpot);
                        }
                    }
                    if (hit.collider.TryGetComponent<customer>(out customer cust))
                    {
                        cust.zvonek.Play();
                        if (cust.Order.Count == dish.PlacedIngredience.Count)
                        {
                            
                            for (int i = 0; i < cust.Order.Count; i++)
                            {
                                if (cust.Order[i].GetComponent<ingred>().Name != dish.PlacedIngredience[i].GetComponent<ingred>().Name)
                                {
                                    Debug.Log("Wrong Order");
                                    cust.orderBg.color = Color.red;
                                    h.healthLevel --;
                                    break;
                                }
                            }
                           
                            Destroy(heldItem);
                            holding = false;
                            Debug.Log("Good");
                            score++;
                            txt.text = score.ToString();
                            cust.orderBg.color = Color.green;
                            cust.removeViz();
                            cust.newOrd();

                        }
                        else
                        {

                            Debug.Log("Wrong length");
                            cust.orderBg.color = Color.red;
                            h.healthLevel--;
                        }
                    }
                    else if (hit.collider.TryGetComponent<trash>(out trash Trash))
                    {
                        foreach (GameObject placed in dish.PlacedIngredience)
                        {
                            Destroy(placed);
                        }
                        dish.PlacedIngredience.Clear();
                    }

                   
                }

                if (hit.collider.gameObject.TryGetComponent<stove>(out stove st) && maHasicak && st.isOnFire)
                {
                    st.isOnFire = false;
                    st.fireEfect.Stop();
                }

                if (maHasicak && hit.collider.gameObject.GetComponent<fire>())
                {
                    Destroy(heldItem);
                    holding = false;
                    maHasicak = false;
                }
            }
        }

        
    }
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
        heldItem = null;
        holding = false;
    }
}


