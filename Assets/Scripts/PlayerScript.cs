using UnityEngine;
using System.Collections;
using System;

public class PlayerScript : MonoBehaviour {
    private int health;
    private Rigidbody r;
    private bool canMove;
    private bool lockDoor;
    private bool slow;
    private float slowTimer;
    public Vector3 start;
    private bool canSprint;
    private bool isSprinting;
    private float sprintTimer;
    private float sprintCooldown;
    private float mouseSensitivity;
    private float controllerSensitivity;
    private float verticleLook;
    private Transform cameraTransform;
    private LevelLoader level;
    private float restartTimer = 0f;
    private DisplayButton dp;
    public GameObject deathScreen;
    static public Renderer deathRenderer;
    private int levelsWithoutDying = -1;
    private int maxStreak = 0;


    private Texture2D sprintBar;
    private GUIStyle style;

    // Use this for initialization
    void Start() {

        deathRenderer = deathScreen.GetComponent<Renderer>();
        deathRenderer.enabled = false;

        health = 1;
        r = GetComponent<Rigidbody>();
        canMove = true;
        lockDoor = true;
        slow = false;
        slowTimer = 5.0f;
        start = transform.position;
        Cursor.lockState = CursorLockMode.Locked;
        mouseSensitivity = 300f;
        controllerSensitivity = 300f;
        canSprint = true;
        isSprinting = false;
        sprintTimer = 2.0f;
        sprintCooldown = 3.0f;
        cameraTransform = transform.Find("MainCamera").transform;
        level = GameObject.Find("Level").GetComponent<LevelLoader>();
        dp = GetComponent<DisplayButton>();
        sprintBar = new Texture2D(1, 1);
        style = new GUIStyle();
    }

    void ApplyDamage(int i) {
        health -= i;
    }

    void toggleSlow() {
        slow = !slow;
    }

    void toggleTrap() {
        canMove = !canMove;
    }

    public void ResetPlayer() {
        if (canMove) {
            levelsWithoutDying++;
            maxStreak = Mathf.Max(maxStreak, levelsWithoutDying);
        } else {
            levelsWithoutDying = 0;
        }
        dp.Reset();
        health = 1;
        canMove = true;
        transform.position = start;
        r.isKinematic = false;

    }


    void Update() {

        if (Input.GetKeyDown(KeyCode.R) || Input.GetButtonDown("Select")) {
            levelsWithoutDying = -1;
            level.LoadNextLevel();
        }

        if (health <= 0 && canMove) {
            canMove = false;
            r.isKinematic = true;
            restartTimer = 2f;
        }

        restartTimer -= Time.deltaTime;
        if (restartTimer < 0f && !canMove) {
            level.LoadLevel();
        }


        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float controllerX = Input.GetAxis("Right Horizontal") * controllerSensitivity * Time.deltaTime;

        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        float controllerY = -Input.GetAxis("Right Vertical") * controllerSensitivity * Time.deltaTime;

        float rot = mouseX == 0 ? controllerX : mouseX;
        verticleLook -= mouseY == 0 ? controllerY : mouseY;
        verticleLook = Mathf.Clamp(verticleLook, -80f, 80f);
        cameraTransform.localRotation = Quaternion.Euler(verticleLook, 0, 0);
        transform.Rotate(0, rot, 0);
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (canMove) {
            float speed = 1.2f;

            if (slow && slowTimer > 0) {
                speed *= .5f;
                slowTimer -= Time.deltaTime;
            } else if (slow && slowTimer <= 0) {
                slow = false;
                slowTimer = 5.0f;
            }
            if (canSprint && (Input.GetKey(KeyCode.LeftShift) || Input.GetButton("Left Analog"))) {
                speed *= 2f;
                sprintTimer -= Time.deltaTime;
                isSprinting = true;
            } else {
                isSprinting = false;
            }
            if (sprintTimer <= 0) {
                canSprint = false;
            }
            if (canSprint && sprintTimer < 2f && !isSprinting) {
                sprintTimer += Time.deltaTime / 2;
            }
            if (sprintCooldown <= 0) {
                canSprint = true;
                sprintCooldown = 3f;
                sprintTimer = 2f;
            }
            if (!canSprint) {
                sprintCooldown -= Time.deltaTime;
            }
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            r.velocity = transform.TransformDirection(new Vector3(x * speed, 0, y * speed));

        } else {
            r.velocity = Vector3.zero;
        }
    }

    void OnGUI() {

        if (canSprint) {
            if (slow) {
                sprintBar.SetPixel(0, 0, new Color(.5f , 0f, 1f));
            } else {
                sprintBar.SetPixel(0, 0, Color.yellow);
            }
            sprintBar.Apply();
            style.normal.background = sprintBar;
            GUI.Box(new Rect(0, Screen.height - 25, Screen.width * sprintTimer / 2, 25), GUIContent.none, style);
        } else {
            sprintBar.SetPixel(0, 0, Color.red);
            sprintBar.Apply();
            style.normal.background = sprintBar;
            GUI.Box(new Rect(0, Screen.height - 25, Screen.width * (1 - sprintCooldown / 3), 25), GUIContent.none, style);
        }

        if (!canMove) {
            GUI.skin.label.fontSize = 100;
            deathRenderer.enabled = true;
        }

        GUI.skin.label.fontSize = 30;
        GUIStyle s = GUI.skin.GetStyle("Label");
        s.normal.textColor = Color.white;
        s.fontStyle = FontStyle.Bold;
        s.alignment = TextAnchor.UpperLeft;
        GUI.Label(new Rect(0, 0, 500, 100), "Current Streak: " + levelsWithoutDying + " Max: " + maxStreak);


    }

}
