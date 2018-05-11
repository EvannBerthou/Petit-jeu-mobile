using UnityEngine;
using UnityEngine.UI;

public class ShopCategories : MonoBehaviour {

	[SerializeField] private GameObject[] CategoriesItems;
	[SerializeField] private Image[] CategoriesBackground;

	[Space]
	[SerializeField] private Sprite ActiveSprite;
	[SerializeField] private Sprite InactiveSprite;

	void Start(){ SetCategorie(0); }

	public void SetCategorie (int id) {
		for(int i = 0; i < CategoriesItems.Length; i++){
			CategoriesItems[i].SetActive(i == id);
			if(i == id) CategoriesBackground[i].sprite = ActiveSprite;
			else CategoriesBackground[i].sprite = InactiveSprite;
		}
	}
}