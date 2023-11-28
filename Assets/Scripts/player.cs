using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


public class player : MonoBehaviour {
    public float moveSpeed = 5.0f;
    public Rigidbody spawning;
    private bool DestroyingEnabled = false;
    private bool RotateEnabled = false;
    private GameObject model;
    private IEnumerator coroutine;

    private bool oscillatingEnabled = false;
    private bool oscillatingInProgress = false;


    private int points = 0;
    public int Points {
        set {
            points = value;
        }
        get {
            return points;
        }
    }

    // Start is called before the first frame update
    void Start() {
        coroutine = CoDestroyAllTarget();
        transform.position = new Vector3(0, Random.Range(10f, 20f), 0);
        model = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Instantiate(spawning, new Vector3(Random.Range(-50, 50), Random.Range(10f, 10F + (20127114 % 10)), Random.Range(-50, 50)), Quaternion.identity);
        }

        Vector3 move = Vector3.zero;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
            move += Vector3.left;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
            move += Vector3.right;
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
            move += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
            move -= Vector3.forward;
        }
        if (Input.GetKeyDown(KeyCode.Q)) {
            DestroyingEnabled = !DestroyingEnabled;

            if (DestroyingEnabled) {
                StartCoroutine(coroutine);
            } else {
                StopCoroutine(coroutine);
            }

        }
        if (Input.GetKeyDown(KeyCode.R)) {
            RotateEnabled = !RotateEnabled;
        }
        if (Input.GetKeyDown(KeyCode.M)) {
            oscillatingEnabled = !oscillatingEnabled;

        }


        if (oscillatingEnabled && !oscillatingInProgress) {
            oscillatingInProgress = true;
            StartCoroutine(CoOscillate());
        }
        move = Vector3.ClampMagnitude(move, 1f);

        if (RotateEnabled) {
            model.transform.Rotate(0, 50 * Time.deltaTime, 0);
        }
        transform.Translate(moveSpeed * Time.deltaTime * move);
    }

   
    private void OnGUI() {
        GUIStyle style = new GUIStyle();
        style.fontSize = 50;
        style.normal.textColor = Color.white;
        GUI.Label(new Rect(10, 10, 100, 25), "Points: " + points, style);
    }

    IEnumerator CoDestroyAllTarget() {

        while (DestroyingEnabled) {
            yield return new WaitForSeconds(2);

            GameObject target = GameObject.FindGameObjectWithTag("Target");
            if (!target) {
                break;
            }
            GameObject.Destroy(target);
        }
        DestroyingEnabled = false;
        yield return null;
    }

    IEnumerator CoOscillate() {
        float angle = 0;
        float angularSpeed = (2 + (20127114 % 10)) * 10;
        Vector3 fixedPos = transform.position;
        do {
            angle += angularSpeed * Time.deltaTime;
            Vector3 offset = new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad)-1);

            model.transform.position = fixedPos + offset * 2;
            yield return null;
        } while (angle < 360);
        oscillatingInProgress = false;
        if (!oscillatingEnabled) {
            model.transform.position = fixedPos;
        }
    }
}
