using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayButton : MonoBehaviour {


	string ObjInView;
	GameObject ObjectTemp;
	static public GameObject[] inventoryArray;
	Vector3 originalPosition;
	Vector3 forwardPos;
	//float distance = 0.01f;
	static int i;
	//int exitLoop = 0 ;
	int k;
	int l;
	Camera cameraFacing; //the left or right camera
	GameObject objectFacing; //the middle anchor
	GameObject obj1;
	//int InvOpen = 0;
	public int index;


    public bool isLocked = true;
    //private char[] pattern = {'W','S','A','D'};
	private char[] pattern = {'A','B','X','Y'};
    private string key = "";
	public GameObject A;
	public GameObject B;
	public GameObject X;
	public GameObject Y;
	//private GameObject temp;
	private Renderer Ar;
	private Renderer Br;
	private Renderer Xr;
	private Renderer Yr;
	//private Renderer tempr;
	private GameObject g;
	private Collider c;


	//private int count=0;
	//private int i=0;

	void Start () {

		//Hide();

		for (int i = 0; i < 6; ++i) {
			key += pattern [Random.Range (0, 4)];
		}
		//tempr = temp.GetComponent<Renderer> ();


		c = GetComponent<Collider> ();
		//X = g.transform.GetChild (2);
		g = GetComponent<GameObject> ();
		Ar = A.GetComponent<Renderer> ();
		Br = B.GetComponent<Renderer> ();
		Xr = X.GetComponent<Renderer> ();
		Yr = Y.GetComponent<Renderer> ();
		//tempr = temp.GetComponent<Renderer> ();
		Ar.enabled = false;
		Br.enabled = false;
		Xr.enabled = false;
		Yr.enabled = false;
		//tempr.enabled = false;

	}
	
	// Update is called once per frame
	void Update()
	{
		//Debug.Log (name);
		//if (count == 1 && i < 6 && BearTrapScript.inTrap == true) {
		if (i < 6 && TrapdoorScript.inTrap == true) {


			if (key [i] == 'A') {
				//Debug.Log("in1");
				//Ar.transform.position = new Vector3(1,1,1);

				//g.transform.GetChild(0).gameObject.SetActive(true);
				Ar.enabled = true;

				if (Input.GetKeyDown (KeyCode.A) || Input.GetButtonDown("Fire1")) {
					i++;
					Ar.enabled = false;
					//g.transform.GetChild(0).gameObject.SetActive(false);

				}

			} else if (key [i] == 'B') {

				//g.transform.GetChild(1).gameObject.SetActive(true);

				Br.enabled = true;
				if (Input.GetKeyDown (KeyCode.B) || Input.GetButtonDown("Fire2")) {
					i++;
					//g.transform.GetChild(1).gameObject.SetActive(false);
					Br.enabled = false;
				}

			} else if (key [i] == 'X') {


				//g.transform.GetChild(2).gameObject.SetActive(true);

				Xr.enabled = true;
				if (Input.GetKeyDown (KeyCode.X) || Input.GetButtonDown("Fire3")) {
					i++;
					//g.transform.GetChild(2).gameObject.SetActive(false);
					Xr.enabled = false;
				}
			} else if (key [i] == 'Y') {


				//g.transform.GetChild(3).gameObject.SetActive(true);

				Yr.enabled = true;
				if (Input.GetKeyDown (KeyCode.Y) || Input.GetButtonDown("Jump")) {
					i++;
					//g.transform.GetChild(3).gameObject.SetActive(false);

					Yr.enabled = false;
				}
			}	
		}

		if (i == 6) {

			c.gameObject.BroadcastMessage("toggleTrap");
			//gameObject.SetActive(false);
			//g.SetActive(false);
			//tempr.enabled = true;
			isLocked = false;
			i=0;
			TrapdoorScript.inTrap = false;
		}

	}

	/*IEnumerator DelayFunction()
	{
		yield return new WaitForSeconds (5);
	}


	void OnTriggerEnter(Collider col)
    {
		if (col.gameObject.tag == "Player") {
			Debug.Log ("IN TRAP");
			count = 1;
		
		}
	}*/

/*static public void viewInventory()
{
	if (inventoryCount <= 2) {
			Debug.Log ("count is " + inventoryCount.ToString ());
			Debug.Log ("Updated Inventory is ");

		}
}

void Hide()
{
	transform.GetChild(0).gameObject.SetActive(false);
	transform.GetChild(1).gameObject.SetActive(false);
	transform.GetChild(2).gameObject.SetActive(false);
}

void displayObj(string ObjInView)
{	
	
	if(ObjInView=="rocket2")
		index=0;
	else if(ObjInView=="shelf_girls1_doll32")
		index=1;
	else if(ObjInView=="shelf_boys1_toy52")
		index=2;
	
	transform.GetChild(index).gameObject.SetActive(true);
	//transform.GetChild(index).localPosition= new Vector3(0,0.5,1);    // ROTATE THESE OBJECTS !!
	rotation = true;
	
}


void DropObject(string ObjInView)
{
	if(ObjInView=="rocket2")
		index=0;
	else if(ObjInView=="shelf_girls1_doll32")
		index=1;
	else if(ObjInView=="shelf_boys1_toy52")
		index=2;
	
	//transform.GetChild(index).localPosition= Vector3(0,0,0); 
	transform.GetChild(index).gameObject.SetActive(false);

	
	//inventoryArray.RemoveAt(inventoryTracker);
	inventoryCount--;
	Debug.Log("Object Dropped ");
	if(inventoryCount>0)
	{Debug.Log("Object Dropped 22222 "); 
		UpdateObjectInView();
	}
	
}

void UpdateObjectInView()
{
	ObjectTemp=inventoryArray[inventoryTracker];
	ObjInView= ObjectTemp.name.ToString()+"2";
	displayObj(ObjInView);
}


void InventoryTracker()
{
	
	if(inventoryTracker<inventoryCount)
		inventoryTracker+= 1;
	
	if(inventoryTracker==inventoryCount)
		inventoryTracker=0;
	
	Debug.Log("From inventoryOwn Script : Current item is "+inventoryArray[inventoryTracker].name.ToString()); 
	
}

function Update()
{
	
		if (rotation) {
			transform.GetChild (index).Rotate (0, Time.deltaTime * 20, 0);
		}
		if (Input.GetKeyDown ("e")) {
			if (eTracker == 1)	
			if (inventoryCount > 0) { 
				//if(InvOpen==0)
				InvOpen = 1;
				Hide ();
				InventoryTracker ();
				UpdateObjectInView ();
			
			} else
				Debug.Log ("eTracker - inventory is empty");
		
		}

	
		if (Input.GetMouseButtonDown (1)) {
			if (InvOpen == 1)
			if (inventoryCount == 1) {
				DropObject (ObjInView);
				// Hide();       IS IT REQUIRED ??
				InvOpen = 0;
				rotation = false;
			} else if (inventoryCount > 1) {
				DropObject (ObjInView);
			} else
				Debug.Log ("inventory is empty");
		}
	
	
		if (Input.GetKeyDown ("q")) {
			if (InvOpen == 1) {
				Hide ();
				InvOpen = 0;
				rotation = false;
			}
		}
	}
*/
}