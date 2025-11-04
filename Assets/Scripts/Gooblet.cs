using UnityEngine;

public class Gooblet : MonoBehaviour
{

    [Header("Inscribed")]
    public float floatAmp = 0.25f;
    public float floatFrequency = 2f;
    public float lifeTime = 10f;

    private Vector3 startPos;
    void Start()
    {
        startPos = transform.position;
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {

        float yOffset = Mathf.Sin(Time.time * floatFrequency) * floatAmp;
        transform.position = startPos + new Vector3(0, yOffset, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null) 
        { 
            player.goobletCount++;
            Destroy(this.gameObject);
        }
    }
}
