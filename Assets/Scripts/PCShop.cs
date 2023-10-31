using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PCShop : MonoBehaviour
{
    [SerializeField] ProductSO[] productsList;
    [SerializeField] GameObject productsUI;
    [SerializeField] GameObject productCard;
    [SerializeField] GameObject deliveryPoint;
    void Start(){
        foreach (ProductSO product in productsList)
        {
            GameObject productCardInstance = Instantiate(productCard);
            productCardInstance.transform.SetParent(productsUI.transform, false);
            productCardInstance.GetComponentsInChildren<TMP_Text>()[0].text = product.nameProduct;
            productCardInstance.GetComponentsInChildren<TMP_Text>()[1].text = product.price.ToString() + " €";
            if (product.image) productCardInstance.GetComponentInChildren<Image>().sprite = product.image;
            productCardInstance.GetComponentInChildren<Button>().onClick.AddListener(() => BuyProduct(product.product));
        }
    }

    public void BuyProduct(GameObject product){
        print("buyProduct = " + product.name);
        Instantiate(product,deliveryPoint.transform.position,Quaternion.identity);
    }

    public void ExitShop(){
        FindObjectOfType<Pc>().Exit();
    }
}
