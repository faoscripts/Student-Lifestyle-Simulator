using UnityEngine;

[CreateAssetMenu(fileName = "Product", menuName = "New Product")]
public class ProductSO : ScriptableObject
{
    public string nameProduct;
    public GameObject product;
    public float price;
    public Sprite image;
}
