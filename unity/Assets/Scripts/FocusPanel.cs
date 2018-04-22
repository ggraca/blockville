using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;


public class FocusPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{
	public Button buyBtn;
	public Game game;

    void Start(){
		buyBtn.onClick.AddListener(ActionBuy);
	}

	public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("The cursor entered the selectable UI element.");
		game.hoveringUi(true);
    }

	public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("The cursor left the selectable UI element.");
		game.hoveringUi(false);
    }

	public void ActionBuy(){
		Debug.Log("Buy button clicked");
		StartCoroutine(game.buyTile());
	}

}