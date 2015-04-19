using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour
{
	void Start()
	{
		//Player player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
	}

	void OnGUI()
	{


		Player player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
		const int buttonWidth = 120;
		const int buttonHeight = 60;

		if(player.gameOver == true){
			if (GUI.Button(new Rect(Screen.width / 2 - (buttonWidth / 2),(1 * Screen.height / 3) - (buttonHeight / 2),buttonWidth,buttonHeight),"Retry!"))
			{

				this.transform.position = player.savePoint;
				player.gameOver = false;
				//Application.LoadLevel("Main");
	//			player.transform.position.x = player.savePoint.x;
	//			player.transform.position.y = player.savePoint.y;
	//			player.transform.position.z = player.savePoint.z;

			}
			
			if (GUI.Button(
				// Center in X, 2/3 of the height in Y
				new Rect(
				Screen.width / 2 - (buttonWidth / 2),
				(2 * Screen.height / 3) - (buttonHeight / 2),
				buttonWidth,
				buttonHeight
				),
				"Credits"
				)
				)
			{
				// Reload the level
				Application.LoadLevel("Credits");

			}
		}
	}
}