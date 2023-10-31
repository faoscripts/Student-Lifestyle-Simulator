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
    [SerializeField] TMP_Text moneyTxt;
    PlayerMovement player;
    void Start(){
        player = FindObjectOfType<PlayerMovement>();
        moneyTxt.text = player.money.ToString() + " €";
        foreach (ProductSO product in productsList)
        {
            GameObject productCardInstance = Instantiate(productCard);
            productCardInstance.transform.SetParent(productsUI.transform, false);
            productCardInstance.GetComponentsInChildren<TMP_Text>()[0].text = product.nameProduct;
            productCardInstance.GetComponentsInChildren<TMP_Text>()[1].text = product.price.ToString() + " €";
            if (product.image) productCardInstance.GetComponentInChildren<Image>().sprite = product.image;
            productCardInstance.GetComponentInChildren<Button>().onClick.AddListener(() => BuyProduct(product));
        }
    }

    public void BuyProduct(ProductSO product){
        if (player.money - product.price <= 0) return;
        player.money -= product.price;
        moneyTxt.text = player.money.ToString() + " €";
        Instantiate(product.product,deliveryPoint.transform.position,Quaternion.identity);
    }

    public void ExitShop(){
        FindObjectOfType<Pc>().Exit();
    }
}
