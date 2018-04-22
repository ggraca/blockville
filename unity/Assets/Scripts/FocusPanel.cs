using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;


public class FocusPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{
	public Button buyBtn;
	public Button farmBtn;
	public Button forestBtn;
	public Game game;

    void Start(){
		buyBtn.onClick.AddListener(ActionBuy);
	}

	public void OnPointerEnter(PointerEventData eventData)
    {
		game.hoveringUi(true);
    }

	public void OnPointerExit(PointerEventData eventData)
    {
		game.hoveringUi(false);
    }

	public void ActionBuy(){
		StartCoroutine(game.buyTile());
	}

	public void ActionBuyForest(){
		StartCoroutine(game.buildTile(1));
	}
	
	public void ActionBuyFarm(){
		StartCoroutine(game.buildTile(2));
	}

}